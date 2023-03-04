using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private Configs.BusinessesConfig _businessesConfig;

        private EcsWorld _world;
        private EcsSystems _systems;

        void Start()
        {
            // void can be switched to IEnumerator for support coroutines.

            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            var wallet = new Wallet.Model.WalletsFacade(1, _world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_systems);
#endif
            _systems
                // init systems
                .Add(new ViewCollector.Systems.ViewCollectorInit())

                // business
                .Add(new Business.Systems.BusinessSpawner())
                .Add(new Business.Systems.UpdateBusinessProgress(0.1f))
                .Add(new Business.Systems.BusinessLevelUp())
                .Add(new Business.Systems.UpgradeBuy())

                // wallet
                .Add(new Wallet.Systems.UpdateWalletUIView())

                // one frames
                .OneFrame<Business.Components.LevelUpSignal>()
                .OneFrame<Business.Components.NewUpgradeSignal>()

                // injects
                .Inject(_businessesConfig)
                .Inject(wallet)
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