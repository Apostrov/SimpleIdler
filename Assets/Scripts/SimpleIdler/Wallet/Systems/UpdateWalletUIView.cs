using Leopotam.Ecs;

namespace SimpleIdler.Wallet.Systems
{
    public class UpdateWalletUIView : IEcsRunSystem
    {
        private EcsFilter<Components.WalletChangedSignal> _signal;
        private EcsFilter<Components.WalletUIView> _view;

        private EcsWorld _world;
        private Model.WalletsFacade _wallets;

        public void Run()
        {
            if (_signal.IsEmpty())
                return;

            foreach (var idx in _view)
            {
                _view.Get1(idx).View.SetBalance(_wallets.Player.Amount);
            }
        }
    }
}