using Code.Infrastructure.ConfigsManagement;
using Code.Gameplay.UnitStats;
using UnityEngine;
using Code.Gameplay.Characters.Enemies.Configs;
using Zenject;

namespace Code.Gameplay.Characters.Enemies.Services
{
    public class DifficultyService : IDifficultyService, IInitializable
    {
        private readonly IConfigsService _configsService;
        private readonly float _startTime;
        private int _lastLoggedInterval = 0;
        private DifficultyConfig config => _configsService.DifficultyConfig;

        public DifficultyService(IConfigsService configsService)
        {
            _configsService = configsService;
            _startTime = Time.time;
        }

        public void Initialize()
        {
            if(config.DebugMode)
                Debug.Log($"<color=#FF8000>[Difficulty Up]</color> Level: 0 | HP: +0% | DMG: +0%");
        }

        public bool IsDebugMode => _configsService.DifficultyConfig.DebugMode;

        public float GetModifiedStat(float baseValue, StatType statType)
        {
            float timeSinceStart = Time.time - _startTime;
            int intervalsPassed = Mathf.FloorToInt(timeSinceStart / config.IntervalInSeconds);

            if (intervalsPassed > _lastLoggedInterval)
            {
                _lastLoggedInterval = intervalsPassed;
                Debug.Log($"<color=#FF8000>[Difficulty Up]</color> Level: {intervalsPassed} | Time: {timeSinceStart:F1}s | HP: +{config.HpIncreasePercent * intervalsPassed}% | DMG: +{config.DamageIncreasePercent * intervalsPassed}%");
            }

            if (intervalsPassed <= 0) return baseValue;

            float multiplier = 1f;

            if (statType == StatType.MaxHealth && config.IncreaseHp)
                multiplier = 1f + (config.HpIncreasePercent / 100f * intervalsPassed);
            
            if (statType == StatType.Damage && config.IncreaseDamage)
                multiplier = 1f + (config.DamageIncreasePercent / 100f * intervalsPassed);

            return baseValue * multiplier;
        }

        
    }
}