using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Code.Gameplay.Abilities.Configs;
using System;
using Code.Gameplay.Abilities;

namespace Code.UI
{
    public class AbilityCard : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Button _button;

        private Action<AbilityId> _onSelected;
        private AbilityId _id;

        public void Setup(AbilityConfig config, Action<AbilityId> onSelected)
        {
            _id = config.Id;
            _icon.sprite = config.Icon;
            _title.text = config.Title;
            _onSelected = onSelected;

            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(() => _onSelected?.Invoke(_id));
        }
    }
}