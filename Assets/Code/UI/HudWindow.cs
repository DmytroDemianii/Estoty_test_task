using Code.Gameplay.Characters.Enemies.Services;
using Code.Gameplay.Characters.Heroes.Services;
using Code.Gameplay.Experience.Services;
using Code.Infrastructure.UIManagement;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.UI
{
    public class HudWindow : WindowBase
    {
        [SerializeField] private Slider _healthBar;
		[SerializeField] private Text _killedEnemiesText;
        [SerializeField] private Slider _experienceBarFill;
        [SerializeField] private TextMeshProUGUI _currentLevelText;
        [SerializeField] private LevelUpWindow _levelUpWindow;
		
        
        private IHeroProvider _heroProvider;
        private IEnemyDeathTracker _enemyDeathTracker;
        private IExperienceService _experienceService;

        public override bool IsUserCanClose => false;

        [Inject]
        private void Construct(
            IHeroProvider heroProvider, 
            IEnemyDeathTracker enemyDeathTracker,
            IExperienceService experienceService)
        {
            _enemyDeathTracker = enemyDeathTracker;
            _heroProvider = heroProvider;
            _experienceService = experienceService;
        }

        void Start()
        {
			UpdateLevelText();
			_experienceService.OnLevelUp += LevelUp;
        }

        protected override void OnUpdate()
        {
            UpdateHealthBar();
            UpdateKilledEnemiesText();
            UpdateExperienceBar();
        }

        private void UpdateExperienceBar()
        {
            float progress = _experienceService.CurrentExperience / _experienceService.ExperienceToNextLevel;
            _experienceBarFill.value = progress;
        }

        private void LevelUp()
        {
            UpdateLevelText();
            _levelUpWindow.OpenWindow();
        }

        private void UpdateLevelText()
        {
            if (_currentLevelText != null)
                _currentLevelText.text = $"Lv.{_experienceService.CurrentLevel}";
        }

        private void UpdateKilledEnemiesText()
        {
            _killedEnemiesText.text = _enemyDeathTracker.TotalKilledEnemies.ToString();
        }

        private void UpdateHealthBar()
        {
            if (_heroProvider.Hero != null)
                _healthBar.value = _heroProvider.Health.CurrentHealth / _heroProvider.Health.MaxHealth;
            else
                _healthBar.value = 0;
        }
    }
}