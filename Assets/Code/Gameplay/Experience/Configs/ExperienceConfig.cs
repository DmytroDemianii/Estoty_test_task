using System.Collections.Generic;
using UnityEngine;

namespace Code.Gameplay.Experience.Configs
{
    [CreateAssetMenu(fileName = "ExperienceConfig", menuName = Constants.GameName + "/Configs/Experience")]
    public class ExperienceConfig : ScriptableObject
    {
        public List<float> ExperienceToNextLevel = new() { 2f, 3f, 4f, 5f, 6f };
        
        public float GetExperienceForLevel(int level)
        {
            int index = level - 1;
            if (index >= ExperienceToNextLevel.Count)
                return ExperienceToNextLevel[^1];

            return ExperienceToNextLevel[index];
        }

        public int MaxLevel => ExperienceToNextLevel.Count + 1;
    }
}