using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.Business.Systems
{
    public class UpdateBusinessProgress : IEcsRunSystem
    {
        private EcsFilter<Components.Business, Components.BusinessView> _business;

        private Wallet.Model.WalletsFacade _wallets;

        public void Run()
        {
            foreach (var bizIdx in _business)
            {
                ref var business = ref _business.Get1(bizIdx);
                if (business.Level < 1)
                    continue;

                float progress = (Time.time - business.LastIncomeTime) / business.Config.IncomeDelay;
                if (progress >= 1.0f)
                {
                    progress = 0f;
                    business.LastIncomeTime = Time.time;
                    _wallets.Player.Amount +=
                        Mathf.RoundToInt(business.Config.GetIncome(business.Level, business.UpgradeBought));
                }

                _business.Get2(bizIdx).View.SetProgress(progress);
            }
        }
    }
}