using System.Collections.Generic;

namespace SimpleIdler.Business.Components
{
    public struct Business
    {
        public int Id;
        public Data.BusinessConfig Config;
        public int Level;
        public float LastIncomeTime;
        public LinkedList<int> UpgradeBought;
    }
}