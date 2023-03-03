using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SimpleIdler.Business.UnityComponents
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private string _incomePrefix;
        [SerializeField] private string _incomePostfix;
        [SerializeField] private string _costPrefix;
        [SerializeField] private string _costPostfix;
        [SerializeField] private string _boughtText;

        [Header("Technical")]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;
        
        public void SetSellingText(string buttonName, float cost, float income)
        {
            _text.text = $"{buttonName}\n{_incomePrefix}{income * 100f}{_incomePostfix}\n{_costPrefix}{cost}{_costPostfix}";
        }

        public void SetBoughtText(string buttonName, float income)
        {
            _text.text = $"{buttonName}\n{_incomePrefix}{income * 100f}{_incomePostfix}\n{_boughtText}";
        }


        public void OnClick(UnityAction callback)
        {
            _button.onClick.AddListener(callback);
        }
    }
}