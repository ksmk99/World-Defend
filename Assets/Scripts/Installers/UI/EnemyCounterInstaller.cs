using UI;
using UnityEngine;
using Zenject;
using Helpers;

namespace Installers
{
    public class EnemyCounterInstaller : MonoInstaller<EnemyCounterInstaller>
    {
        [SerializeField] private EnemyCounterView view;

        public override void InstallBindings()
        {
            Container.Bind<EnemyCounterView>()
                .FromComponentInHierarchy()
                .AsSingle();
            Container.Bind<EnemyCounterModel>()
                .AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyCounterPresenter>()
                .AsSingle();

            Container
                .BindSignal<SignalOnRoomReset>()
                .ToMethod<EnemyCounterPresenter>(x => x.Reset)
                .FromResolve();

            Container
                .BindSignal<SignalOnEnemyDeath>()
                .ToMethod<EnemyCounterPresenter>(x => x.Death)
                .FromResolve();
        }
    }
}
