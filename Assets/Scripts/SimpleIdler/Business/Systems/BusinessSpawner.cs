using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.Business.Systems
{
    public class BusinessSpawner : IEcsInitSystem
    {
        private EcsFilter<Components.BusinessSpawnTransform> _spawn;

        private EcsWorld _world;
        private Data.BusinessesConfig _businessesConfig;

        public void Init()
        {
            foreach (var idx in _spawn)
            {
                RectTransform transform = _spawn.Get1(idx).Transform;
                int id = 0;
                for (id = 0; id < _businessesConfig.Configs.Length; id++)
                {
                    var config = _businessesConfig.Configs[id];
                    
                    // spawn prefab
                    UnityComponents.BusinessView view = GameObject.Instantiate(_businessesConfig.Prefab, transform);
                    float prefabYPosition = (id + 1) * _businessesConfig.Spacing + id * _businessesConfig.PrefabHeight;
                    view.transform.localPosition += new Vector3(0f, -1f * prefabYPosition);
                    EcsEntity entity = _world.NewEntity();
                    entity.Get<Components.Business>() = new Components.Business
                    {
                        Id = id,
                        Config = config,
                        Level = config.StartLevel
                    };
                    view.OnSpawn(entity, _world);

                    // base init
                    view.SetActive(true);
                    view.SetName(config.Name);
                    view.SetLevel(config.StartLevel);
                    view.SetIncome(config.GetIncome(config.StartLevel));
                    view.SetProgress(0f);

                    // lvl up init
                    view.LvlUpButton.SetNotBoughtText("LVL UP", config.Cost);
                    view.LvlUpButton.OnClick(() => _world.NewEntity().Get<Components.LevelUpSignal>());

                    // upgrade 1 init
                    view.Upgrade1.SetNotBoughtText(config.Upgrade1.Name, config.Upgrade1.Cost,
                        config.Upgrade1.IncomeMultiplier);
                    view.Upgrade1.OnClick(() => _world.NewEntity().Get<Components.Upgrade1Signal>());

                    // upgrade 2 init
                    view.Upgrade2.SetNotBoughtText(config.Upgrade2.Name, config.Upgrade2.Cost,
                        config.Upgrade2.IncomeMultiplier);
                    view.Upgrade2.OnClick(() => _world.NewEntity().Get<Components.Upgrade2Signal>());
                }

                Vector2 size = transform.sizeDelta;
                size.y = (_businessesConfig.Spacing + _businessesConfig.PrefabHeight) * id;
                transform.sizeDelta = size;
                transform.localPosition = new Vector3(0f, size.y / -2f);
            }
        }
    }
}