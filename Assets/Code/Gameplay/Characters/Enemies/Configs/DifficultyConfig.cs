using UnityEngine;

namespace Code.Gameplay.Characters.Enemies.Configs
{
    [CreateAssetMenu(fileName = "DifficultyConfig", menuName = Constants.GameName + "/Configs/Difficulty")]
    public class DifficultyConfig : ScriptableObject
    {
        [Range(1, 120)]
        public int IntervalInSeconds = 60;
        
        public bool IncreaseHp;
        public float HpIncreasePercent = 10f;
        
        public bool IncreaseDamage;
        public float DamageIncreasePercent = 10f;
        [Space (1)]
        public bool DebugMode;
    }
}