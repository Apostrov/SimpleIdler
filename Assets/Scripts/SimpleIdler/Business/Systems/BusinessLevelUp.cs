using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.Business.Systems
{
    public class BusinessLevelUp : IEcsRunSystem
    {
        private EcsFilter<Components.Business, Components.BusinessView, Components.LevelUpSignal> _business;

        private Wallet.Model.WalletsFacade _wallets;

        public void Run()
        {
            foreach (var idx in _business)
            {
                ref var business = ref _business.Get1(idx);
                var cost = business.Config.GetCost(business.Level);
                if (cost > _wallets.Player.Amount)
                    continue;

                _wallets.Player.Amount -= cost;
                business.Level++;

                ref var view = ref _business.Get2(idx).View;
                view.SetLevel(business.Level);
                view.SetIncome(business.Config.GetIncome(business.Level));
                view.LvlUpButton.SetCost(business.Config.GetCost(business.Level));

                Model.BusinessDataSaver.SaveLevel(business.Id, business.Level);
            }
        }
    }
}