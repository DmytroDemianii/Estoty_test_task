using System.Collections.Generic;
using Code.Gameplay.Abilities.Configs;
using Code.Gameplay.Characters.Enemies;
using Code.Gameplay.Characters.Enemies.Configs;
using Code.Gameplay.Characters.Heroes.Configs;
using Code.Gameplay.Experience.Configs;
using Code.Gameplay.PickUps;
using Code.Gameplay.PickUps.Configs;

namespace Code.Infrastructure.ConfigsManagement
{
	public interface IConfigsService
	{
		HeroConfig HeroConfig { get; }
		DifficultyConfig DifficultyConfig { get; }
		List<AbilityConfig> Abilities { get; }
		ExperienceConfig ExperienceConfig { get; }
		void Load();
		EnemyConfig GetEnemyConfig(EnemyId id);
		PickUpConfig GetPickUpConfig(PickUpId id);
	}
}