using System.Linq;
using UnityEngine;

namespace Game.Gameplay
{
    public sealed class ChillPlace: Equipment
    {
        [SerializeField] private Transform[] _placePonts;

        private bool[] _freePlaces;

        private void Awake()
        {
            _freePlaces = new bool[_placePonts.Length];

            for (int i = 0; i < _freePlaces.Length; i++)
                _freePlaces[i] = true;
        }
        
        public bool ThereIsFreePlace() => _freePlaces.Any(isFree => isFree);

        public Transform GetFreePlace()
        {
            for (int i = 0; i < _freePlaces.Length; i++)
            {
                if (_freePlaces[i])
                {
                    _freePlaces[i] = false;
                    return _placePonts[i];
                }
            }
            
            Debug.LogError("No place available");
            return null;
        }

    }
}