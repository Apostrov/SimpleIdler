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
                var transform = _spawn.Get1(idx).Transform;
                int id = 0;
                for (id = 0; id < _businessesConfig.Configs.Length; id++)
                {
                    var config = _businessesConfig.Configs[id];
                    float prefabYPosition = (id + 1) * _businessesConfig.Spacing + id * _businessesConfig.PrefabHeight;

                    // spawn prefab
                    var business = GameObject.Instantiate(_businessesConfig.Prefab, transform);
                    business.transform.localPosition += new Vector3(0f, -1f * prefabYPosition);
                    var entity = _world.NewEntity();
                    business.OnSpawn(entity, _world);
                    ref var view = ref entity.Get<Components.BusinessView>();

                    // load values
                    int level = Model.BusinessDataSaver.LoadLevel(id);
                    level = level == 0 ? config.StartLevel : level;
                    HashSet<int> upgrades = Model.BusinessDataSaver.LoadUpgrades(id);
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

                    // base init
                    business.SetActive(true);
                    business.SetName(config.Name);
                    business.SetLevel(level);
                    business.SetIncome(config.GetIncome(level, upgrades));
                    business.SetProgress(progress);

                    // lvl up init
                    business.LvlUpButton.SetCost(config.GetCost(level));
                    business.LvlUpButton.OnClick(() => entity.Get<Components.LevelUpSignal>());

                    // upgrades spawn
                    for (int upgradeId = 0; upgradeId < config.Upgrades.Length; upgradeId++)
                    {
                        var upgradeConfig = config.Upgrades[upgradeId];
                        var button = GameObject.Instantiate(_businessesConfig.UpgradePrefab, business.UpgradesSpawn);
                        view.Upgrades.Add(button);

                        if (upgrades.Contains(upgradeId))
                        {
                            button.SetBoughtText(upgradeConfig.Name, upgradeConfig.IncomeMultiplier);
                        }
                        else
                        {
                            button.SetSellingText(upgradeConfig.Name, upgradeConfig.Cost,
                                upgradeConfig.IncomeMultiplier);
                        }

                        int saveId = upgradeId;
                        button.OnClick(() => entity.Get<Components.NewUpgradeSignal>().Id = saveId);
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