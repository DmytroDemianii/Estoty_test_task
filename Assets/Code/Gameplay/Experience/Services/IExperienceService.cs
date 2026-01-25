using System;

namespace Code.Gameplay.Experience.Services
{
    public interface IExperienceService
    {
        event Action OnExperienceChanged;
        event Action OnLevelUp;
        float CurrentExperience { get; }
        float ExperienceToNextLevel { get; }
        int CurrentLevel { get; }
        void AddExperience(float amount);
    }
}