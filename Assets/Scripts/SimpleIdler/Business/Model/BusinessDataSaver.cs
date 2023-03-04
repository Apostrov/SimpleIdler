using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleIdler.Business.Model
{
    public static class BusinessDataSaver
    {
        private const string LEVEL_KEY = "si_business_level_"; // save as int
        private const string UPGRADES_KEY = "si_business_upgrades_"; // save as int[] with id of upgrades
        private const string PROGRESS_KEY = "si_business_progress_"; // save as float

        public static void SaveLevel(int id, int level)
        {
            PlayerPrefs.SetInt(LEVEL_KEY + id, level);
            PlayerPrefs.Save();
        }

        public static int LoadLevel(int id)
        {
            return PlayerPrefs.GetInt(LEVEL_KEY + id, 0);
        }

        public static void SaveUpgrades(int id, LinkedList<int> upgrades)
        {
            PlayerPrefsX.SetIntArray(UPGRADES_KEY + id, upgrades.ToArray());
            PlayerPrefs.Save();
        }

        public static LinkedList<int> LoadUpgrades(int id)
        {
            int[] upgradesId = PlayerPrefsX.GetIntArray(UPGRADES_KEY + id);
            return new LinkedList<int>(upgradesId);
        }

        public static void SaveProgress(int id, float progress)
        {
            PlayerPrefs.SetFloat(PROGRESS_KEY + id, progress);
            PlayerPrefs.Save();
        }

        public static float LoadProgress(int id)
        {
            return PlayerPrefs.GetFloat(PROGRESS_KEY + id, 0f);
        }
    }
}