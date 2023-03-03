using Leopotam.Ecs;

namespace SimpleIdler.Balance.Systems
{
    public class UpdateBalanceViewProcessing : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter<Components.Balance, Components.BalanceView> _balance;
        private EcsFilter<Components.BalanceView>.Exclude<Components.BalanceView> _view;

        private EcsWorld _world;

        public void Init()
        {
            if (_balance.IsEmpty())
            {
                foreach (var idx in _view)
                {
                    _view.GetEntity(idx).Get<Components.Balance>().Value = 0f;
                    _view.Get1(idx).View.SetBalance(0f);
                }
            }
        }

        public void Run()
        {
            foreach (var idx in _balance)
            {
                _balance.Get2(idx).View.SetBalance(_balance.Get1(idx).Value);
            }
        }
    }
}