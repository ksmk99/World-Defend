using UI;
using UnityEngine;
using Zenject;
using Helpers;

namespace Installers
{
    public class TimerInstaller : MonoInstaller<TimerInstaller>
    {
        [SerializeField] private TimerView view;

        public override void InstallBindings()
        {
            Container.Bind<TimerView>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.Bind<TimerModel>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<TimerPresenter>()
                .AsSingle();

            Container
                .BindSignal<SignalOnRoomReset>()
                .ToMethod<TimerPresenter>(x => x.Reset)
                .FromResolve();
        }
    }
}
