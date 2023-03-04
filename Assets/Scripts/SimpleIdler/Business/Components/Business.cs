using System.Collections.Generic;

namespace SimpleIdler.Business.Components
{
    public struct Business
    {
        public int Id;
        public Configs.BusinessConfig Config;
        public int Level;
        public float TimePassedAfterIncome;
        public LinkedList<int> UpgradeBought;
    }

    public struct SpawnedSignal
    {
    }
}