using Leopotam.Ecs;
using SimpleIdler.ViewCollector.UnityComponents;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleIdler.Business.UnityComponents
{
    public class BusinessView : AViewElement
    {
        [SerializeField] private string _levelPrefix;
        [SerializeField] private string _incomePrefix;
        [SerializeField] private string _incomePostfix;

        [Header("Technical")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _level;
        [SerializeField] private TMP_Text _income;
        [SerializeField] private Slider _progressBar;
        [field: SerializeField] public LevelUpButton LvlUpButton { get; private set; }
        [field: SerializeField] public RectTransform UpgradesSpawn { get; private set; }

        public override void OnSpawn(EcsEntity entity, EcsWorld world)
        {
            entity.Get<Components.BusinessView>().View = this;
        }

        public void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public void SetName(string businessName)
        {
            _name.text = businessName;
        }

        public void SetLevel(int level)
        {
            _level.text = $"{_levelPrefix}\n{level}";
        }

        public void SetIncome(float income)
        {
            _income.text = $"{_incomePrefix}\n{Mathf.RoundToInt(income)}{_incomePostfix}";
        }

        public void SetProgress(float value)
        {
            _progressBar.value = value;
        }
    }
}