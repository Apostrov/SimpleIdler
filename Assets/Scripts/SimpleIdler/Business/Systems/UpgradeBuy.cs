using Leopotam.Ecs;

namespace SimpleIdler.Business.Systems
{
    public class UpgradeBuy : IEcsRunSystem
    {
        private EcsFilter<Components.Business, Components.BusinessView, Components.NewUpgradeSignal> _business;

        private Wallet.Model.WalletsFacade _wallets;

        public void Run()
        {
            foreach (var idx in _business)
            {
                ref var business = ref _business.Get1(idx);
                int upgradeId = _business.Get3(idx).Id;
                var upgrade = business.Config.Upgrades[_business.Get3(idx).Id];
                if (upgrade.Cost > _wallets.Player.Amount || business.UpgradeBought.Contains(upgradeId))
                    continue;

                _wallets.Player.Amount -= upgrade.Cost;
                business.UpgradeBought.Add(upgradeId);

                ref var view = ref _business.Get2(idx);
                view.Upgrades[upgradeId].SetBoughtText(upgrade.Name, upgrade.IncomeMultiplier);
                view.View.SetIncome(business.Config.GetIncome(business.Level, business.UpgradeBought));

                Model.BusinessDataSaver.SaveUpgrades(business.Id, business.UpgradeBought);
            }
        }
    }
}