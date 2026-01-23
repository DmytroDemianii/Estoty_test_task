using Code.Gameplay.UnitStats;

namespace Code.Gameplay.Characters.Enemies.Services
{
	public interface IDifficultyService
	{
		float GetModifiedStat(float baseValue, StatType statType);
        public bool IsDebugMode { get; }
	}
}