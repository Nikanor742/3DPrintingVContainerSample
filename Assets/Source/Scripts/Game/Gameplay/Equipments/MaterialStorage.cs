using System.Collections.Generic;
using Game.Data;
using Game.Interfaces;
using TMPro;
using UnityEngine;

namespace Game.Gameplay
{
    public class MaterialStorage : Equipment, IInteractable
    {
        public EInteractType InteractType => EInteractType.PurchaseMaterial;
        public Transform Transform => transform;

        [SerializeField] private TMP_Text _countText;
        [SerializeField] private float _maxMaterialCount;
        [SerializeField] private int _addCount = 5;

        [SerializeField] private Transform _startPoint;
        [SerializeField] private GameObject _cylinderPrefab;
        [SerializeField] private Outline _outline;

        [SerializeField] private int _maxPerX = 5;
        [SerializeField] private int _maxPerZ = 4;

        [SerializeField] private Vector3 _spacing;

        private readonly List<GameObject> _materialInstances = new();

        private void Awake()
        {
            _outline.OutlineWidth = 0;
        }

        private void OnMouseEnter()
        {
            if(interactable)
                _outline.OutlineWidth = 3;
        }

        private void OnMouseExit()
        {
            if(interactable)
                _outline.OutlineWidth = 0;
        }

        public bool Interact()
        {
            if(!interactable)
                return false;
            
            var currentCount = _materialInstances.Count;
            var maxCount = Mathf.FloorToInt(_maxMaterialCount);

            if (currentCount >= maxCount)
                return false;

            var allowedCount = Mathf.Min(_addCount, maxCount - currentCount);
            var total = currentCount + allowedCount;
            var layerSize = _maxPerX * _maxPerZ;

            for (var i = currentCount; i < total; i++)
            {
                var layer = i / layerSize;
                var indexInLayer = i % layerSize;

                var z = indexInLayer / _maxPerX;
                var x = indexInLayer % _maxPerX;

                var localOffset = new Vector3(
                    -x * _spacing.x,
                    layer * _spacing.y,
                    z * _spacing.z
                );

                var localPosition = _startPoint.localPosition + localOffset;
                
                var obj = Lean.Pool.LeanPool.Spawn(_cylinderPrefab, transform);
                obj.transform.localPosition = localPosition;
                obj.transform.localRotation = Quaternion.identity;

                _materialInstances.Add(obj);
            }
            _countText.text = _materialInstances.Count.ToString();
            return true;
        }
    }
}