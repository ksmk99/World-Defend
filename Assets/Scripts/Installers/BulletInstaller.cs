using System.Runtime.InteropServices;
using Unit;
using Unit.Bullet;
using Unity.VisualScripting;
using Zenject;

namespace Installers
{
    public class BulletInstaller : Installer<BulletInstaller>
    {
        public override void InstallBindings()
        {
            //Container.Bind<BulletModel>().AsSingle();
            ////Container.Bind<IBulletSettings>().To<BulletSettings>().WhenInjectedInto<BulletModel>();
            //Container.BindInterfacesAndSelfTo<BulletPresentor>().AsSingle();
        }
    }
}
