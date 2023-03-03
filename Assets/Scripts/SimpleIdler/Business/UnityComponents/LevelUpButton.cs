using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace SimpleIdler.Business.UnityComponents
{
    public class LevelUpButton : MonoBehaviour
    {
        [SerializeField] private string _name;
        [SerializeField] private string _costPrefix;
        [SerializeField] private string _costPostfix;

        [Header("Technical")]
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Button _button;

        public void SetText(float cost)
        {
            _text.text = $"{_name}\n{_costPrefix}{cost}{_costPostfix}";
        }

        public void OnClick(UnityAction callback)
        {
            _button.onClick.AddListener(callback);
        }
    }
}