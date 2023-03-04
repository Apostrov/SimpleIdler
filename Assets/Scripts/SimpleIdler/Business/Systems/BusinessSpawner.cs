using System.Collections.Generic;
using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.Business.Systems
{
    public class BusinessSpawner : IEcsInitSystem
    {
        private EcsFilter<Components.BusinessSpawnTransform> _spawn;

        private EcsWorld _world;
        private Configs.BusinessesConfig _businessesConfig;

        public void Init()
        {
            foreach (var idx in _spawn)
            {
                RectTransform transform = _spawn.Get1(idx).Transform;
                int id = 0;
                for (id = 0; id < _businessesConfig.Configs.Length; id++)
                {
                    Configs.BusinessConfig config = _businessesConfig.Configs[id];

                    // spawn prefab
                    var view = GameObject.Instantiate(_businessesConfig.Prefab, transform);
                    float prefabYPosition = (id + 1) * _businessesConfig.Spacing + id * _businessesConfig.PrefabHeight;
                    view.transform.localPosition += new Vector3(0f, -1f * prefabYPosition);
                    EcsEntity entity = _world.NewEntity();
                    view.OnSpawn(entity, _world);

                    // load values
                    int level = Model.BusinessDataSaver.LoadLevel(id);
                    level = level == 0 ? config.StartLevel : level;
                    LinkedList<int> upgrades = Model.BusinessDataSaver.LoadUpgrades(id);
                    float progress = Model.BusinessDataSaver.LoadProgress(id);

                    // fill component
                    entity.Get<Components.Business>() = new Components.Business
                    {
                        Id = id,
                        Config = config,
                        Level = level,
                        TimePassedAfterIncome = config.IncomeDelay * progress,
                        UpgradeBought = upgrades
                    };
                    entity.Get<Components.SpawnedSignal>();

                    // base init
                    view.SetActive(true);
                    view.SetName(config.Name);
                    view.SetLevel(level);
                    view.SetIncome(config.GetIncome(level, upgrades));
                    view.SetProgress(progress);

                    // lvl up init
                    view.LvlUpButton.SetCost(config.GetCost(level));
                    view.LvlUpButton.OnClick(() => entity.Get<Components.LevelUpSignal>());

                    // upgrades spawn
                    for (int upgradeId = 0; upgradeId < config.Upgrades.Length; upgradeId++)
                    {
                        Configs.UpgradeConfig upgradeConfig = config.Upgrades[upgradeId];
                        UnityComponents.UpgradeButton button =
                            GameObject.Instantiate(_businessesConfig.UpgradePrefab, view.UpgradesSpawn);
                        button.SetSellingText(upgradeConfig.Name, upgradeConfig.Cost, upgradeConfig.IncomeMultiplier);
                        var saveId = upgradeId;
                        button.OnClick(() => entity.Get<Components.UpgradeSignal>().Id = saveId);
                    }
                }

                Vector2 size = transform.sizeDelta;
                size.y = (_businessesConfig.Spacing + _businessesConfig.PrefabHeight) * id;
                transform.sizeDelta = size;
                transform.localPosition = new Vector3(0f, size.y / -2f);
            }
        }
    }
}