using Code.Gameplay.Lifetime.Behaviours;
using Code.Gameplay.UnitStats;
using Code.Gameplay.UnitStats.Behaviours;
using Code.Gameplay.Vision.Behaviours;
using UnityEngine;

namespace Code.Gameplay.Combat.Behaviours
{
	public class RotateToCloseEnemy : MonoBehaviour, IAimDirectionProvider
	{
		[SerializeField] private VisionSight _visionSight;
		[SerializeField] private Stats _stats;

		private Health _health;
		
		public Vector2 Direction { get; private set; }
		private Vector2 _currentVelocity;

		private void Awake()
		{
			_health = GetComponent<Health>();
		}

		private void Update()
		{
			if (_health != null && _health.IsDead)
			{
				return;
			}

			Rotate();
		}

		public Vector2 GetAimDirection()
		{
			if (_health != null && _health.IsDead)
			{
				return Vector2.zero;
			}

			return Direction;
		}

		private void Rotate()
		{
			GameObject closestEnemy = _visionSight.GetClosestEnemy();

			if (closestEnemy != null)
			{
				float rotationSpeed = _stats.GetStat(StatType.RotationSpeed);
				Vector3 targetDir = (closestEnemy.transform.position - transform.position).normalized;

				float smoothTime = 1f / Mathf.Max(rotationSpeed, 0.1f);
				Direction = Vector2.SmoothDamp(Direction, targetDir, ref _currentVelocity, smoothTime);
				
				ApplyRotation();
			}
		}

		private void ApplyRotation()
		{
			if (Direction.sqrMagnitude >= 0.01f)
			{
				float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0, 0, angle);
			}
		}
	}
}