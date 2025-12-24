using System;

namespace Game.Data
{
    [Serializable]
    public sealed class Order
    {
        public EModelType modelType;
        public int count;
    }
}