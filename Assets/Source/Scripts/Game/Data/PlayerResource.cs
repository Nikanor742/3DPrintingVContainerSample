using System;
using R3;
using UnityEngine;

namespace Game.Data
{
    [Serializable]
    public class PlayerResource
    {
        [NonSerialized] public ReactiveProperty<int> OnChangeEvent = new();
        
        [SerializeField] private int count;

        public void Init()
        {
            OnChangeEvent = new ReactiveProperty<int>(count);
        }

        public int Count
        {
            get => count;
            set
            {
                count = value;
                OnChangeEvent?.OnNext(count);
            }
        }
    }
}