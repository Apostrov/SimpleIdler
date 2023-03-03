using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Data.BusinessesConfig _businessesConfig;

        private EcsWorld _world;
        private EcsSystems _systems;

        void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                // init systems
                .Add(new ViewCollector.Systems.ViewCollectorInit())

                // business
                .Add(new Business.Systems.BusinessSpawner())

                // balance
                .Add(new Balance.Systems.UpdateBalanceViewProcessing())

                // one frames
                .OneFrame<Business.Components.LevelUpSignal>()
                .OneFrame<Business.Components.Upgrade1Signal>()
                .OneFrame<Business.Components.Upgrade2Signal>()

                // injects
                .Inject(_businessesConfig)
                .Init();
        }

        void Update()
        {
            _systems?.Run();
        }

        void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}