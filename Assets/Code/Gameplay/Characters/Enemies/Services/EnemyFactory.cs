using Code.Gameplay.Characters.Enemies.Behaviours;
using Code.Gameplay.Characters.Enemies.Configs;
using Code.Gameplay.Identification.Behaviours;
using Code.Gameplay.Lifetime.Behaviours;
using Code.Gameplay.UnitStats;
using Code.Gameplay.UnitStats.Behaviours;
using Code.Infrastructure.ConfigsManagement;
using Code.Infrastructure.Identification;
using Code.Infrastructure.Instantiation;
using UnityEngine;

namespace Code.Gameplay.Characters.Enemies.Services
{
	public class EnemyFactory : IEnemyFactory
	{
		private readonly IConfigsService _configsService;
		private readonly IInstantiateService _instantiateService;
		private readonly IIdentifierService _identifiers;
		private readonly IDifficultyService _difficultyService;

		public EnemyFactory(
			IConfigsService configsService, 
			IInstantiateService instantiateService,
			IIdentifierService identifiers,
			IDifficultyService difficultyService)
		{
			_configsService = configsService;
			_instantiateService = instantiateService;
			_identifiers = identifiers;
			_difficultyService = difficultyService;
		}
		
		public Enemy CreateEnemy(EnemyId id, Vector3 at, Quaternion rotation)
		{
			EnemyConfig enemyConfig = _configsService.GetEnemyConfig(id);
			Enemy enemy = _instantiateService.InstantiatePrefabForComponent(enemyConfig.Prefab, at, rotation);
			
			float hp = _difficultyService.GetModifiedStat(enemyConfig.Health, StatType.MaxHealth);
			Log("Enemy name: " + enemyConfig.name + " HP: " + hp);
			float damage = _difficultyService.GetModifiedStat(enemyConfig.Damage, StatType.Damage);
			Log("Enemy name: " + enemyConfig.name + " DMG: " + damage);

			enemy.GetComponent<Id>().Setup(_identifiers.Next());
			
			enemy.GetComponent<Stats>()
				.SetBaseStat(StatType.MaxHealth, hp)
				.SetBaseStat(StatType.MovementSpeed, enemyConfig.MovementSpeed)
				.SetBaseStat(StatType.Damage, damage);

			enemy.GetComponent<Health>()
				.Setup(hp, hp);
			
			return enemy;
		}

		private void Log(string message)
		{
			if (_difficultyService.IsDebugMode)
			{
				Debug.Log(message);
			}
		}
	}
}