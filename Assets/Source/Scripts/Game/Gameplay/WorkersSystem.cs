using System;
using System.Collections.Generic;
using Game.Configs;
using Game.Data;
using Game.Interfaces;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Gameplay
{
    public sealed class WorkersSystem : IDisposable
    {
        private readonly GameData _gameData;
        private readonly WorkersSO _workersSO;
        private readonly IWorkerFactory _workerFactory;
        
        private readonly CompositeDisposable  _disposables = new();
        
        private List<Worker> _workers = new();
        
        public WorkersSystem(GameData gameData, WorkersSO workersSO, IWorkerFactory workerFactory)
        {
            _gameData = gameData;
            _workersSO = workersSO;
            _workerFactory = workerFactory;
            
            _gameData.OnWorkerHiredEvent
                .Subscribe(CreateWorker)
                .AddTo(_disposables);
            
            Observable.EveryUpdate()
                .Subscribe(_ => GameLoop())
                .AddTo(_disposables);
        }

        private void GameLoop()
        {
            foreach (var w in _workers)
            {
                if (w.routine == EWorkerRoutine.Free)
                {
                    if (w.state == EWorkerState.Waiting)
                    {
                        MoveToChillPlace(w);
                    }
                }
            }
        }

        private void MoveToChillPlace(Worker worker)
        {
            foreach (var gridShopItem in _gameData.activeObjects[EEquipType.ChillPlace])
            {
                var o = (ChillPlace)gridShopItem;
                if (o.ThereIsFreePlace())
                {
                    var freePlace = o.GetFreePlace();
                    worker.MoveToPosition(freePlace.position);
                    worker.state = EWorkerState.MovingToPosition;
                    worker.OnDestinationPointEvent
                        .Take(1)
                        .Subscribe(_ =>
                        {
                            worker.state = EWorkerState.InPosition; 
                            worker.SeatAndChill(o, freePlace);
                        })
                        .AddTo(worker.actionDisposables);
                    return;
                }
            }
        }

        private void CreateWorker(WorkerData workerData)
        {
            _workers.Add(_workerFactory.Create(Vector3.zero,Quaternion.identity));
        }
        
        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}