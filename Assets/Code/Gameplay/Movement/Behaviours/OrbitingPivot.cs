using Code.Gameplay.UnitStats.Behaviours;
using Code.Gameplay.UnitStats;
using UnityEngine;

namespace Code.Gameplay.Movement.Behaviours
{
    public class OrbitingPivot : MonoBehaviour
    {
        private Stats _stats;

        private void Awake()
        {
            _stats = transform.GetComponentInParent<Stats>();
        }

        private void Update()
        {
            float speed = _stats.GetStat(StatType.RotationSpeed) * 40;
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}