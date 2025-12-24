using System;
using DG.Tweening;
using Game.Data;
using NaughtyAttributes;
using R3;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Gameplay
{
    public class Worker : MonoBehaviour
    {
        public EWorkerRoutine routine;
        public EWorkerState state;
        
        public Subject<Unit> OnDestinationPointEvent = new();
        public CompositeDisposable actionDisposables = new();

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;

        private int _moveHash;
        private int _seatHash;

        private void Awake()
        {
            _moveHash = Animator.StringToHash("Move");
            _seatHash = Animator.StringToHash("Seat");
        }

        public void SetNavMeshSettings(float workersAngularSpeed, float workersAcceleration)
        {
            _navMeshAgent.angularSpeed = workersAngularSpeed;
            _navMeshAgent.acceleration = workersAcceleration;
        }

        public void MoveToPosition(Vector3 position)
        {
            DisposeAction();
            _navMeshAgent.SetDestination(position);
            
            Observable.EveryUpdate()
                .Where(_ => Vector3.Distance(transform.position, position) <= 0.8f)
                .Take(1)
                .Subscribe(_ =>
                {
                    OnDestinationPointEvent?.OnNext(Unit.Default);
                })
                .AddTo(actionDisposables);
        }

        public void SeatAndChill(ChillPlace place, Transform point)
        {
            _navMeshAgent.enabled = false;
            _animator.SetTrigger(_seatHash);
            transform.parent = point;
            transform.DOLocalRotate(Vector3.zero, 0.5f);
            transform.DOLocalMove(Vector3.zero, 0.5f);
        }

        private void DisposeAction()
        {
            actionDisposables?.Dispose();
            actionDisposables = new CompositeDisposable();
        }

        private bool IsMoving(float eps = 0.01f)
        {
            if(!_navMeshAgent.enabled) return false;
            if (_navMeshAgent.isStopped) return false;
            if (_navMeshAgent.pathPending) return false;

            var hasTarget = _navMeshAgent.hasPath || _navMeshAgent.desiredVelocity.sqrMagnitude > eps;
            var farFromTarget = _navMeshAgent.remainingDistance > _navMeshAgent.stoppingDistance + 0.001f;
            var hasVelocity = _navMeshAgent.velocity.sqrMagnitude > eps;

            return hasTarget && farFromTarget && hasVelocity;
        }

        private void Update()
        {
            _animator.SetBool(_moveHash, IsMoving());
        }

        private void OnDestroy()
        {
            actionDisposables?.Dispose();
        }
    }
}