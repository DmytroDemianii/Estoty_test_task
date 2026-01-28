using System;
using Code.Gameplay.Experience.Configs;
using Code.Infrastructure.ConfigsManagement;

using UnityEngine;
using Zenject;

namespace Code.Gameplay.Experience.Services
{
    public class ExperienceService : IExperienceService, ITickable
    {
        public event Action OnExperienceChanged;
        public event Action OnLevelUp;

        ExperienceConfig _experienceConfig;

        public float CurrentExperience { get; private set; }
        public float ExperienceToNextLevel { get; private set; } = 10f;
        public int CurrentLevel { get; private set; } = 1;

        [Inject]
        private void Construct(IConfigsService configs)
		{
			_experienceConfig = configs.ExperienceConfig;
			ExperienceToNextLevel = _experienceConfig.GetExperienceForLevel(CurrentLevel);
		}

        public void Tick()
        {
            if(UnityEngine.Input.GetKeyDown(KeyCode.L))
            {
                AddExperience(ExperienceToNextLevel);
            }
        }

        public void AddExperience(float amount)
        {
            CurrentExperience += amount;
            
            if (CurrentExperience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
            
            OnExperienceChanged?.Invoke();
        }

        private void LevelUp()
        {
            CurrentExperience -= ExperienceToNextLevel;
            CurrentLevel++;
            
            OnLevelUp?.Invoke();
            Debug.Log($"Leveled up to {CurrentLevel}!");
        }

        
    }
}