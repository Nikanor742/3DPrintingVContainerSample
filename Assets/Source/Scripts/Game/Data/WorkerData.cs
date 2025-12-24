using System;

namespace Game.Data
{
    [Serializable]
    public class WorkerData
    {
        public string name;
        
        public float speed;
        
        public int salary;
        public int hireCost;
        public int level;

        public WorkerData(string name, float speed, int salary, int hireCost, int level)
        {
            this.name = name;
            this.speed = speed;
            this.salary = salary;
            this.hireCost = hireCost;
            this.level = level;
        }
    }
}