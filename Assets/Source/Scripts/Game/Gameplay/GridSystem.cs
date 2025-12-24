using System;
using DG.Tweening;
using Game.Configs;
using Game.Data;
using Game.Interfaces;
using R3;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace Game.Gameplay
{
    public sealed class GridSystem : MonoBehaviour, IInitializable, IDisposable
    { 
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private RectTransform canvas;
        [SerializeField] private Material gridMaterial;
        [SerializeField] private Material planeMaterial;
        [SerializeField] private Renderer _rend;
        [SerializeField] private Transform _gridPlaneTransform;
        
        [Inject] private readonly GameData _gameData;
        [Inject] private readonly ShopSO _shopConfig;
        [Inject] private readonly MoneyChangeView _moneyChangeViewPrefab;
        [Inject] private readonly IEquipmentFactory _equipmentFactory;
        
        private EquipShopItem _currentEquipShopItem;
        private Equipment _currentItem;

        private bool _rotatationProcess;
        
        private readonly CompositeDisposable _disposables = new();
        
        public void Initialize()
        {
            _gameData.OnShopEquipmentSelectedEvent?
                .Subscribe(OnSelectItem)
                .AddTo(_disposables);

            _gameData.OnInputActionEvent?
                .Subscribe(OnInputActionEvent)
                .AddTo(_disposables);

            _gameData.OnShopChangeStateEvent?
                .Subscribe(state=>
                {
                    SetGridState(state);
                    DestroyActiveItem();
                })
                .AddTo(_disposables);

            SpawnGrid(2);
        }
        
        public void SpawnGrid(float scaleMultiplier)
        {
            var planeScale = _gridPlaneTransform.localScale;
            var width = planeScale.x * 10f;
            var height = planeScale.z * 10f;

            var cellRect = cellPrefab.GetComponent<RectTransform>();
            var cellWidth = cellRect.rect.width * canvas.localScale.x * scaleMultiplier;
            var cellHeight = cellRect.rect.height * canvas.localScale.y * scaleMultiplier;

            var countX = Mathf.CeilToInt(width / cellWidth);
            var countY = Mathf.CeilToInt(height / cellHeight);

            var startPos = _gridPlaneTransform.position - new Vector3(width / 2f, 0f, height / 2f);

            for (int x = 0; x < countX; x++)
            {
                for (int y = 0; y < countY; y++)
                {
                    var worldPos = startPos + new Vector3((x + 0.5f) * cellWidth, 0f, (y + 0.5f) * cellHeight);
                    var newCell = Instantiate(cellPrefab, canvas);
                    newCell.SetActive(true);
                    newCell.transform.position = worldPos;

                    var newCellRect = newCell.GetComponent<RectTransform>();
                    newCellRect.sizeDelta = new Vector2(cellRect.rect.width * scaleMultiplier,
                        cellRect.rect.height * scaleMultiplier);
                }
            }
        }

        private void SetGridState(bool state)
        {
            _rend.material = state ? gridMaterial : planeMaterial;
        }

        private void OnInputActionEvent(EActionType actionType)
        {
            if (actionType == EActionType.RotateItemLeft)
                Rotate(true);
            else if (actionType == EActionType.RotateItemRight)
                Rotate(false);
            else if (actionType == EActionType.LeftMouseDown)
                InstallItem();
        }

        private void OnSelectItem(EEquipType equip)
        {
            DestroyActiveItem();
            CreateItem(equip);
        }

        private void DestroyActiveItem(bool state = false)
        {
            if (_currentItem != null)
            {
                _currentEquipShopItem = null;
                Destroy(_currentItem.gameObject);
                _currentItem = null;
            }
        }

        private void CreateItem(EEquipType equip, Transform copyObject = null)
        {
            _currentEquipShopItem = _shopConfig.GetItem(equip);
            var item = _equipmentFactory.Create(equip, Vector3.zero, Quaternion.identity);
            if (copyObject != null)
            {
                item.transform.position = _currentItem.transform.position;
                item.transform.rotation = _currentItem.transform.rotation;
            }
            item.installBoxRenderer.gameObject.SetActive(true);
            _currentItem = item;
            FollowMouse();
        }

        private void Update()
        {
            if (_currentItem != null)
            {
                FollowMouse();
                CheckInstallPossibility();
            }
        }

        private void Rotate(bool rightRotation)
        {
            if (_currentItem != null)
            {
                _rotatationProcess = true;
                var currentY = _currentItem.transform.eulerAngles.y;
                currentY = Mathf.Round(currentY / 90f) * 90f;
                if (rightRotation)
                {
                    DOTween.Kill(_currentItem);
                    var newRotation = new Vector3(_currentItem.transform.eulerAngles.x, currentY - 90f,
                        _currentItem.transform.eulerAngles.z);
                    _currentItem.transform.DORotate(newRotation, 0.1f).OnComplete(() => _rotatationProcess = false);
                }
                else
                {
                    DOTween.Kill(_currentItem);
                    var newRotation = new Vector3(_currentItem.transform.eulerAngles.x, currentY + 90f,
                        _currentItem.transform.eulerAngles.z);
                    _currentItem.transform.DORotate(newRotation, 0.1f).OnComplete(() => _rotatationProcess = false);
                }
            }
        }

        private void FollowMouse()
        {
            var mousePos = Input.mousePosition;
            mousePos.z = Camera.main.WorldToScreenPoint(_currentItem.transform.position).z;

            var worldPos = Camera.main.ScreenToWorldPoint(mousePos);

            worldPos.y = 0;

            worldPos.x = Mathf.Round(worldPos.x * 2f) / 2f;
            worldPos.z = Mathf.Round(worldPos.z * 2f) / 2f;

            _currentItem.transform.position = new Vector3(Mathf.Clamp(worldPos.x, -9.5f, 9.5f),
                worldPos.y, Mathf.Clamp(worldPos.z, -9.5f, 9.5f));
        }
        private bool CheckInstallPossibility()
        {
            if(_rotatationProcess)
                return false;
            
            var installPossibility = true;
            var installCollider = _currentItem.installBoxCollider;
            var interactCollider = _currentItem.interactBoxCollider;
            
            var worldCenter = installCollider.transform.TransformPoint(installCollider.center);
            var halfExtents = Vector3.Scale(installCollider.size * 0.5f, installCollider.transform.lossyScale);
            var worldRotation = installCollider.transform.rotation;
            
            Collider[] hitColliders = Physics.OverlapBox(worldCenter, halfExtents, worldRotation);

            foreach (var hit in hitColliders)
            {
                if (hit != installCollider && hit != interactCollider)
                {
                    installPossibility = false;
                    break;
                }
            }
            
            var possibleColor = new Color(0f, 1f, 0f, 0.5f);
            var noPossibleColor = new Color(1f, 0f, 0f, 0.5f);

            _currentItem.installBoxRenderer.material.color = installPossibility ? possibleColor : noPossibleColor;

            return installPossibility;
        }

        private void InstallItem()
        {
            var money = SaveExtension.player.resources[EPlayerResourceType.Coins];
            if (_currentItem != null &&
                !EventSystem.current.IsPointerOverGameObject() &&
                CheckInstallPossibility() &&
                money.Count >= _currentEquipShopItem.cost)
            {
                _gameData.activeObjects[_currentEquipShopItem.itemType].Add(_currentItem);
                _currentItem.SetActive();

                var changeMoneyViewPos = _currentItem.transform.position;
                changeMoneyViewPos.y += 2f;
                var changeMoneyView =
                    Lean.Pool.LeanPool.Spawn(_moneyChangeViewPrefab, changeMoneyViewPos, Quaternion.identity);
                changeMoneyView.SetCount(-_currentEquipShopItem.cost);
                
                money.Count-= _currentEquipShopItem.cost;
                SaveExtension.player.equipmentStorage
                    .AddEquipment(new EquipmentData(_currentEquipShopItem.itemType, 
                        _currentItem.transform.position,
                        _currentItem.transform.eulerAngles));
                
                SaveExtension.SaveData();

                if (money.Count >= _currentEquipShopItem.cost)
                {
                    CreateItem(_currentEquipShopItem.itemType, _currentItem.transform);
                }
                else
                {
                    _currentItem = null;
                    _currentEquipShopItem = null;
                }
                _gameData.OnInstallEquipment?.OnNext(Unit.Default);
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}
