using Code.Gameplay.UnitStats;
using UnityEngine;

namespace Code.Gameplay.Abilities.Configs
{
    [CreateAssetMenu(fileName = "AbilityConfig", menuName = Constants.GameName + "/Configs/Ability")]
    public class AbilityConfig : ScriptableObject
    {
        public AbilityId Id;
        public string Title;
        public Sprite Icon;
        public bool IsReplicable;

        [Header("Stat Boost")]
        public StatType TargetStat;
        public float BoostAmount;
    }
}