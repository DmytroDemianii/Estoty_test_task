using System.Collections.Generic;
using System.Linq;
using Code.Gameplay.Abilities.Configs;
using Code.Gameplay.Characters.Heroes.Services;
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
        
        private readonly List<AbilityId> _acquiredOneTimeAbilities = new();

        [Inject]
        private void Construct(IHeroProvider heroProvider, IConfigsService configs)
        {
            _heroProvider = heroProvider;
            _configs = configs;
        }

        public void ApplyAbility(AbilityId id)
        {
            AbilityConfig config = _configs.Abilities.First(x => x.Id == id);
            if (!config.IsReplicable) _acquiredOneTimeAbilities.Add(id);

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