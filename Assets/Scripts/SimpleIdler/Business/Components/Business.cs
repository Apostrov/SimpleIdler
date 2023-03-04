using System.Collections.Generic;

namespace SimpleIdler.Business.Components
{
    public struct Business
    {
        public int Id;
        public Configs.BusinessConfig Config;
        public int Level;
        public float TimePassedAfterIncome;
        public HashSet<int> UpgradeBought;
    }

    public struct SpawnedSignal
    {
    }
}