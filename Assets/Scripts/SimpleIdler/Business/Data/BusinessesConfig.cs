using UnityEngine;

namespace SimpleIdler.Data
{
    [CreateAssetMenu(fileName = "BusinessesConfig", menuName = "SimpleIdler/Data/BusinessesConfig")]
    public class BusinessesConfig : ScriptableObject
    {
        [Header("Spawner")]
        public float Spacing;
        public float PrefabHeight;
        public Business.UnityComponents.BusinessView Prefab;
        
        [Header("Configs")]
        public BusinessConfig[] Configs;
    }

    [System.Serializable]
    public class BusinessConfig
    {
        [Header("View")]
        public string Name;
        public int StartLevel;
        public float Cost;

        [Header("Income")]
        public float IncomeDelay;
        public float IncomeValue;

        [Header("Upgrades")]
        public UpgradeConfig Upgrade1;
        public UpgradeConfig Upgrade2;

        public float GetIncome(int level, bool hasUpgrade1 = false, bool hasUpgrade2 = false)
        {
            float income = level * IncomeValue;
            float multiplier = 1f;
            if (hasUpgrade1)
                multiplier += Upgrade1.IncomeMultiplier;
            if (hasUpgrade2)
                multiplier += Upgrade2.IncomeMultiplier;
            return income * multiplier;
        }
    }

    [System.Serializable]
    public class UpgradeConfig
    {
        public string Name;
        public float Cost;
        public float IncomeMultiplier;
    }
}