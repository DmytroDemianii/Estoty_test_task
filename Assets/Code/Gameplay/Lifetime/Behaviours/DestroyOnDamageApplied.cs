using Code.Gameplay.Movement.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Lifetime.Behaviours
{
	[RequireComponent(typeof(IDamageApplier))]
	public class DestroyOnDamageApplied : MonoBehaviour
	{
		[SerializeField] private float _delay;
		
		private IDamageApplier _damageApplier;
		private ProjectileBouncer _bouncer;

		private void Awake()
		{
			_bouncer = TryGetComponent<ProjectileBouncer>(out var bouncer) ? bouncer : null;
			_damageApplier = GetComponent<IDamageApplier>();
		}

		private void OnEnable()
		{
			_damageApplier.OnDamageApplied += HandleDamageApplied;
		}

		private void OnDisable()
		{
			_damageApplier.OnDamageApplied -= HandleDamageApplied;
		}

		private void HandleDamageApplied(Health _)
		{
			if (_bouncer != null && _bouncer.TryBounce()) 
				return;
			
			Destroy(gameObject, _delay);
		}
	}
}