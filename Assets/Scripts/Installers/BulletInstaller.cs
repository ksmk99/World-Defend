using System.Runtime.InteropServices;
using Unit;
using Unit.Bullet;
using Zenject;

namespace Installers
{
    public class BulletInstaller : Installer<BulletInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<BulletModel>().AsSingle();
            //Container.Bind<IBulletSettings>().To<BulletSettings>().WhenInjectedInto<BulletModel>();
            Container.Bind<BulletFactory>().AsCached(); 
            Container.BindInterfacesAndSelfTo<BulletPresentor>().AsSingle();
            Container.BindMemoryPool<BulletView, BulletView.Pool>()
                .FromFactory<BulletFactory>();

            //Container.BindFactory<UnityEngine.Object, IBulletSettings, BulletRuntimeSettings, BulletView, BulletFactory>()
            //    .FromPoolableMemoryPool<UnityEngine.Object, IBulletSettings, BulletRuntimeSettings, BulletView>()
            //         .FromFactory<BulletFactory>();
        }
    }
}
