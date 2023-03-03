using Leopotam.Ecs;
using SimpleIdler.ViewCollector.UnityComponents;
using TMPro;
using UnityEngine;

namespace SimpleIdler.Wallet.UnityComponents
{
    public class BalanceView : AViewElement
    {
        [SerializeField] private TMP_Text _balance;
        [SerializeField] private string _prefix;
        [SerializeField] private string _postfix;

        private int _currentBalance;

        public override void OnSpawn(EcsEntity entity, EcsWorld world)
        {
            entity.Get<Components.WalletUIView>().View = this;
            _currentBalance = -999;
            SetBalance(0);
            world.NewEntity().Get<Components.WalletChangedSignal>();
        }

        public void SetBalance(int value)
        {
            if (value == _currentBalance)
                return;
            _currentBalance = value;
            _balance.text = $"{_prefix}{_currentBalance}{_postfix}";
        }
    }
}