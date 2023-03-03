using System.Collections.Generic;
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
        public Business.UnityComponents.UpgradeButton UpgradePrefab;

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
        public UpgradeConfig[] Upgrades;

        public float GetIncome(int level, LinkedList<int> upgradesId = null)
        {
            float income = level * IncomeValue;
            float multiplier = 1f;
            if (upgradesId == null)
                return income;

            foreach (var id in upgradesId)
            {
                multiplier += Upgrades[id].IncomeMultiplier;
            }

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