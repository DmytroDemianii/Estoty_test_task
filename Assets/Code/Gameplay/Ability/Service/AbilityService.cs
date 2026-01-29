using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Abilities.Configs;
using Code.Gameplay.Characters.Heroes.Services;
using Code.Gameplay.Projectiles.Services;
using Code.Gameplay.Teams;
using Code.Gameplay.UnitStats;
using Code.Infrastructure.ConfigsManagement;
using UnityEngine;
using Zenject;

namespace Code.Gameplay.Abilities.Services
{
    public class AbilityService : IAbilityService
    {
        private IConfigsService _configs;
        private IHeroProvider _heroProvider;
        private IProjectileFactory _projectileFactory;
        
        private readonly List<AbilityId> _acquiredOneTimeAbilities = new();

        [Inject]
        private void Construct(
            IHeroProvider heroProvider,
            IConfigsService configs,
            IProjectileFactory projectileFactory
            )
        {
            _heroProvider = heroProvider;
            _configs = configs;
            _projectileFactory = projectileFactory;
        }

        public void ApplyAbility(AbilityId id)
        {
            AbilityConfig config = _configs.Abilities.First(x => x.Id == id);
            if (!config.IsReplicable) _acquiredOneTimeAbilities.Add(id);
            
            switch (id)
            {
                case AbilityId.OrbitingProjectiles:
                    float damage = _heroProvider.Stats.GetStat(StatType.Damage);
                    float speed = _heroProvider.Stats.GetStat(StatType.MovementSpeed);
                    float radius = _heroProvider.Stats.GetStat(StatType.VisionRange) / 3f;    

                    _projectileFactory.CreateOrbitingShield(_heroProvider.OrbitingPivot, damage, speed, radius);
                    
                    return;

                case AbilityId.BouncingProjectiles:
                    _projectileFactory.MakeBouncingProjectile(true);
                    return;
            }

            StatModifier modifier = new StatModifier(config.TargetStat, config.BoostAmount);
            _heroProvider.Stats.AddStatModifier(modifier);
            
            Debug.Log($"Applied ability: {id}");
        }

        public List<AbilityConfig> GetRandomAbilities(int count)
        {
            return _configs.Abilities
                .Where(x => x.IsReplicable || !_acquiredOneTimeAbilities.Contains(x.Id))
                .OrderBy(x => Random.value)
                .Take(count)
                .ToList();
        }
    }
}