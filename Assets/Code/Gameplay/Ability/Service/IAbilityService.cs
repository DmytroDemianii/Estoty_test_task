using System.Collections.Generic;
using Code.Gameplay.Abilities.Configs;

namespace Code.Gameplay.Abilities.Services
{
    public interface IAbilityService
    {
        List<AbilityConfig> GetRandomAbilities(int count);
        void ApplyAbility(AbilityId id);
    }
}