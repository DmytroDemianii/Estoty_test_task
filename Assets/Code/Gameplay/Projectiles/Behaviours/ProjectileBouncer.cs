using System.Linq;
using Code.Gameplay.Teams;
using Code.Gameplay.Teams.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Movement.Behaviours
{
    public class ProjectileBouncer : MonoBehaviour
    {
        [SerializeField] private float _findTargetRadius = 10f;
        private IMovementDirectionProvider _directionProvider;
        private TeamType _enemyTeam;
        private bool _hasBounced;

        private void Awake()
        {
            _directionProvider = GetComponent<IMovementDirectionProvider>();
        }

        public void Setup(TeamType myTeam)
        {
            _enemyTeam = myTeam == TeamType.Hero ? TeamType.Enemy : TeamType.Hero;
        }

        public bool TryBounce()
        {
            if (_hasBounced) return false;

            Transform target = FindNearestEnemy();
            if (target != null)
            {
                Vector2 newDirection = (target.position - transform.position).normalized;
                _directionProvider.SetDirection(newDirection);

                float angle = Mathf.Atan2(newDirection.y, newDirection.x) * Mathf.Rad2Deg;
                angle -= 90f;
                transform.rotation = Quaternion.Euler(0, 0, angle);

                _hasBounced = true;
                return true;
            }

            return false;
        }

        private Transform FindNearestEnemy()
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, _findTargetRadius);
            
            return hitColliders
                .Select(x => x.GetComponent<Team>())
                .Where(x => x != null && x.Type == _enemyTeam)
                .OrderBy(x => Vector2.Distance(transform.position, x.transform.position))
                .Select(x => x.transform)
                .FirstOrDefault();
        }
    }
}