using Code.Gameplay.Cameras.Services;
using Code.Gameplay.Characters.Enemies.Services;
using Code.Gameplay.Characters.Heroes.Services;
using Code.Gameplay.PickUps.Services;
using Code.Gameplay.Projectiles.Services;
using Zenject;

namespace Code.Infrastructure.Installers
{
	public class BattleInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindHeroServices();
			BindEnemyServices();
			BindCameraServices();
			BindCombatServices();
			BindPickupServices();
		}

		private void BindPickupServices()
		{
			Container.BindInterfacesTo<PickUpFactory>().AsSingle();
		}

		private void BindCombatServices()
		{
			Container.BindInterfacesTo<ProjectileFactory>().AsSingle();
		}

		private void BindCameraServices()
		{
			Container.BindInterfacesTo<CameraProvider>().AsSingle();
		}

		private void BindEnemyServices()
		{
			Container.BindInterfacesTo<EnemyFactory>().AsSingle();
			Container.BindInterfacesTo<EnemyProvider>().AsSingle();
			Container.BindInterfacesTo<EnemyDeathTracker>().AsSingle();
		}

		private void BindHeroServices()
		{
			Container.BindInterfacesTo<HeroFactory>().AsSingle();
			Container.BindInterfacesTo<HeroProvider>().AsSingle();
		}
	}
}