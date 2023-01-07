using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using Zenject;

namespace Installers
{
    public class EnemyInstaller : Installer<EnemyInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<IMovement>().To<EnemyMovement>().AsSingle();
            Container.Bind<EnemyModel>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyController>().AsSingle();
        }
    }
}
