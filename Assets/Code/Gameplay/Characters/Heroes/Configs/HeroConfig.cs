using UnityEngine;

namespace Code.Gameplay.Characters.Heroes.Configs
{
	[CreateAssetMenu(fileName = "HeroConfig", menuName = Constants.GameName + "/Configs/Hero")]
	public class HeroConfig : ScriptableObject
	{
		public Behaviours.Hero Prefab;
		
		public float Health = 100f;
		public float MovementSpeed = 5f;
		public float ProjectileSpeed = 10f;
		public float GunRotationSpeed = 5f;
		public float Damage = 10f;
		public float VisionRange = 10f;
		public float ShootCooldown = 1f;
	}
}