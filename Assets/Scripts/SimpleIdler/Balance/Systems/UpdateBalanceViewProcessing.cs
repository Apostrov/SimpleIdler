using Leopotam.Ecs;

namespace SimpleIdler.Balance.Systems
{
    public class UpdateBalanceViewProcessing : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<Components.Balance> _balance;
        private EcsFilter<Components.BalanceView> _view;

        private EcsWorld _world;

        public void Init()
        {
            if (_balance.IsEmpty())
            {
                _world.NewEntity().Get<Components.Balance>().Value = 0f;

                foreach (var idx in _view)
                {
                    _view.Get1(idx).View.SetBalance(0f);
                }
            }
        }

        public void Run()
        {
            foreach (var balanceIdx in _balance)
            {
                var balance = _balance.Get1(balanceIdx).Value;

                foreach (var viewIdx in _view)
                {
                    _view.Get1(viewIdx).View.SetBalance(balance);
                }
            }
        }
    }
}