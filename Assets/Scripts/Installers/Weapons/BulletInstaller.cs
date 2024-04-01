using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;


public class BulletInstaller : MonoInstaller<BulletInstaller>
{
    [SerializeField] private ABulletSettings settings;

    public override void InstallBindings()
    {
        Container.Bind<BulletView>().FromComponentOnRoot().AsSingle();
        Container.Bind<BulletModel>().AsSingle().WithArguments(settings);
        Container.Bind<ABulletPresenter>()
            .To(settings.BulletType)
            .AsTransient();

        Container.Bind<ITickable>()
            .To(settings.BulletType)
            .AsTransient();
    }
}