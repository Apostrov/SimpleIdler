using Leopotam.Ecs;
using SimpleIdler.ViewCollector.UnityComponents;
using TMPro;
using UnityEngine;

namespace SimpleIdler.Balance.UnityComponents
{
    public class BalanceView : AViewElement
    {
        [SerializeField] private TMP_Text _balance;
        [SerializeField] private string _prefix;
        [SerializeField] private string _postfix;

        private int _currentBalance;

        public override void OnSpawn(EcsEntity entity, EcsWorld world)
        {
            entity.Get<Components.BalanceView>().View = this;
            _currentBalance = -999;
            SetBalance(0f);
        }

        public void SetBalance(float value)
        {
            int balance = Mathf.RoundToInt(value);
            if (balance == _currentBalance)
                return;
            _currentBalance = balance;
            _balance.text = $"{_prefix}{_currentBalance}{_postfix}";
        }
    }
}