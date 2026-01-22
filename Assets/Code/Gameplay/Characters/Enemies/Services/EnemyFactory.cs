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

		public EnemyFactory(
			IConfigsService configsService, 
			IInstantiateService instantiateService,
			IIdentifierService identifiers)
		{
			_configsService = configsService;
			_instantiateService = instantiateService;
			_identifiers = identifiers;
		}
		
		public Enemy CreateEnemy(EnemyId id, Vector3 at, Quaternion rotation)
		{
			EnemyConfig enemyConfig = _configsService.GetEnemyConfig(id);
			Enemy enemy = _instantiateService.InstantiatePrefabForComponent(enemyConfig.Prefab, at, rotation);
			
			enemy.GetComponent<Id>()
				.Setup(_identifiers.Next());
			
			enemy.GetComponent<Stats>()
				.SetBaseStat(StatType.MaxHealth, enemyConfig.Health)
				.SetBaseStat(StatType.MovementSpeed, enemyConfig.MovementSpeed)
				.SetBaseStat(StatType.Damage, enemyConfig.Damage);

			enemy.GetComponent<Health>()
				.Setup(enemyConfig.Health, enemyConfig.Health);
			
			return enemy;
		}
	}
}