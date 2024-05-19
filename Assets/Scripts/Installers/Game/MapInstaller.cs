using Helpers;
using Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class MapInstaller : MonoInstaller<MapInstaller>
    {
        public GameObject[] levels;

        public override void InstallBindings()
        {
            Container
                .Bind<IMapChangerService>()
                .To<MapChangerService>()
                .AsSingle()
                .WithArguments(levels);

            Container
                .BindSignal<SignalOnRoomReset>()
                .ToMethod<IMapChangerService>(x => x.ChangeMap)
                .FromResolve();
        }
    }
}
