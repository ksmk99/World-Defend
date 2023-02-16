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
            Container.BindInterfacesAndSelfTo<BulletPresentor>().AsSingle();
        }
    }
}
