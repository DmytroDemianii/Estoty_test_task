using Code.Gameplay.Identification.Behaviours;
using Code.Gameplay.Movement.Behaviours;
using Code.Gameplay.Projectiles.Behaviours;
using Code.Gameplay.Teams;
using Code.Gameplay.Teams.Behaviours;
using Code.Gameplay.UnitStats;
using Code.Gameplay.UnitStats.Behaviours;
using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.Identification;
using Code.Infrastructure.Instantiation;
using UnityEngine;

namespace Code.Gameplay.Projectiles.Services
{
	public class ProjectileFactory : IProjectileFactory
	{	
		private readonly IInstantiateService _instantiateService;
		private readonly IIdentifierService _identifiers;
		private readonly IAssetsService _assetsService;

		public ProjectileFactory(
			IInstantiateService instantiateService,
			IIdentifierService identifiers,
			IAssetsService assetsService)
		{
			_instantiateService = instantiateService;
			_identifiers = identifiers;
			_assetsService = assetsService;
		}
		
		public Projectile CreateProjectile(Vector3 at, Vector2 direction, TeamType teamType, float damage, float movementSpeed)
        {
            if (direction == Vector2.zero)
            {
                return null;
            }

            var prefab = _assetsService.LoadAssetFromResources<Projectile>("Projectiles/Projectile");
            Projectile projectile = _instantiateService.InstantiatePrefabForComponent(prefab, at, Quaternion.FromToRotation(Vector3.up, direction));

            SetupCommonComponents(projectile, teamType, damage, movementSpeed);

            projectile.GetComponent<IMovementDirectionProvider>()
                .SetDirection(direction);

            return projectile;
        }

        public void CreateOrbitingShield(OrbitingPivot OrbitingShieldParent, float damage, float rotationSpeed, float radius)
		{
			var prefab = _assetsService.LoadAssetFromResources<Projectile>("Projectiles/OrbitingProjectile");

			for (int i = 0; i < 3; i++)
			{
				float angle = i * 120f * Mathf.Deg2Rad;
				Vector3 spawnOffset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
				
				Projectile projectile = _instantiateService.InstantiatePrefabForComponent(prefab, OrbitingShieldParent.transform.position + spawnOffset, Quaternion.identity);
				projectile.transform.SetParent(OrbitingShieldParent.transform);

				SetupCommonComponents(projectile, TeamType.Hero, damage, 0);

				float visualAngle = (i * 120f) + 90f;
				projectile.transform.localRotation = Quaternion.Euler(0, 0, visualAngle);
			}
		}

        private void SetupCommonComponents(Projectile projectile, TeamType teamType, float damage, float movementSpeed)
        {
            projectile.GetComponent<Id>()
                .Setup(_identifiers.Next());

            projectile.GetComponent<Stats>()
                .SetBaseStat(StatType.MovementSpeed, movementSpeed)
                .SetBaseStat(StatType.Damage, damage);

            projectile.GetComponent<Team>()
                .Type = teamType;
        }
	}
}