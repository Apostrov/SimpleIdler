using Leopotam.Ecs;
using UnityEngine;

namespace SimpleIdler.Business.Systems
{
    public class UpdateBusinessProgress : IEcsRunSystem
    {
        private EcsFilter<Components.Business, Components.BusinessView> _business;

        private Wallet.Model.WalletsFacade _wallets;
        private readonly float _saveProgressEachSec;

        // runtime data
        private float _lastSaveTime;
        private bool _canSave;

        public UpdateBusinessProgress(float saveProgressEachSec)
        {
            _saveProgressEachSec = saveProgressEachSec;
            _lastSaveTime = -999f;
        }

        public void Run()
        {
            if (Time.time - _lastSaveTime > _saveProgressEachSec)
            {
                _canSave = true;
                _lastSaveTime = Time.time;
            }
            else
            {
                _canSave = false;
            }

            foreach (var bizIdx in _business)
            {
                ref var business = ref _business.Get1(bizIdx);
                if (business.Level < 1)
                    continue;

                business.TimePassedAfterIncome += Time.deltaTime;
                float progress = business.TimePassedAfterIncome / business.Config.IncomeDelay;
                if (progress >= 1.0f)
                {
                    progress = 0f;
                    business.TimePassedAfterIncome = 0f;
                    _wallets.Player.Amount +=
                        Mathf.RoundToInt(business.Config.GetIncome(business.Level, business.UpgradeBought));
                }

                _business.Get2(bizIdx).View.SetProgress(progress);

                if (_canSave)
                {
                    Model.BusinessDataSaver.SaveProgress(business.Id, progress);
                }
            }
        }
    }
}