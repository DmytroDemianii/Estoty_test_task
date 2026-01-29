using UnityEngine;

namespace Code.Gameplay.Projectiles.Behaviours
{
	public class Projectile : MonoBehaviour
	{
        void Start()
        {
			Destroy(gameObject, 5f);
        }
    }
}