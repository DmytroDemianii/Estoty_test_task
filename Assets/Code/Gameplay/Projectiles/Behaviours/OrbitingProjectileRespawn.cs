using System.Collections;
using Code.Gameplay.Lifetime.Behaviours;
using Code.Gameplay.Movement.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Projectiles.Behaviours
{
    public class OrbitingProjectileRespawn : MonoBehaviour
    {
        [SerializeField] private GameObject _visual;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private float _respawnDelay = 3f;
        
        private DamageArea _projectile;


        private void Awake() 
        {
            _projectile = GetComponent<DamageArea>();
            if (_projectile != null)
            {
                _projectile.OnDamageApplied += (health) => OnHit();
            }
        }

        public void OnHit()
        {
            StopAllCoroutines();
            StartCoroutine(RespawnRoutine());
        }

        private IEnumerator RespawnRoutine()
        {
            _visual.SetActive(false);
            _collider.enabled = false;

            yield return new WaitForSeconds(_respawnDelay);

            _visual.SetActive(true);
            _collider.enabled = true;
        }
    }
}