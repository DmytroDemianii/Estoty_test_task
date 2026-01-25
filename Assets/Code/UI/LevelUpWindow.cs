using System.Collections.Generic;
using Code.Gameplay.Abilities;
using Code.Gameplay.Abilities.Configs;
using Code.Gameplay.Abilities.Services;
using Code.Gameplay.Experience.Services;
using Code.Infrastructure.UIManagement;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

namespace Code.UI
{
    public class LevelUpWindow : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _contentPanel;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private AbilityCard[] _cards;

        private IExperienceService _experienceService;
        private IAbilityService _abilityService;
        private bool _canInteract;


        [Inject]
        private void Construct(IExperienceService experienceService, IAbilityService abilityService)
        {
            _experienceService = experienceService;
            _abilityService = abilityService;
        }

        void Start()
        {
            _contentPanel.localScale = Vector3.zero;
            _canvasGroup.alpha = 0;
        }

        public void OpenWindow()
        {
            gameObject.SetActive(true);
            _canInteract = false;
            Time.timeScale = 0;
            
            _levelText.text = $"LEVEL {_experienceService.CurrentLevel}";
            SetupCards();
            AnimateIn();
        }

        private void SetupCards()
        {
            List<AbilityConfig> randomAbilities = _abilityService.GetRandomAbilities(3);
            for (int i = 0; i < _cards.Length; i++)
            {
                _cards[i].Setup(randomAbilities[i], OnAbilitySelected);
            }
        }

        private void OnAbilitySelected(AbilityId id)
        {
            if (!_canInteract) return;
            
            _canInteract = false;
            _abilityService.ApplyAbility(id);
            AnimateOut(() => CloseWindow());
        }

        private void CloseWindow()
        {
            gameObject.SetActive(false);
        }

        private void AnimateIn()
        {
            _contentPanel.localScale = Vector3.zero;
            _canvasGroup.alpha = 0;

            _canvasGroup.DOFade(1, 0.5f).SetUpdate(true);
            _contentPanel.DOScale(1, 0.5f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true)
                .OnComplete(() => _canInteract = true);
        }

        private void AnimateOut(System.Action onComplete)
        {
            _canvasGroup.DOFade(0, 0.3f).SetUpdate(true);
            _contentPanel.DOScale(0, 0.3f)
                .SetEase(Ease.InBack)
                .SetUpdate(true)
                .OnComplete(() => {
                    Time.timeScale = 1;
                    onComplete?.Invoke();
                });
        }
    }
}