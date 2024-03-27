using Unit.Bullet;
using Unit;
using Zenject;
using UnityEngine;


public class BulletInstaller : MonoInstaller<BulletInstaller>
{
    [SerializeField] private BulletSettings settings;

    public override void InstallBindings()
    {
        Container.Bind<BulletView>().FromComponentOnRoot().AsSingle();
        Container.Bind<BulletModel>().AsSingle().WithArguments(settings);
        Container.BindInterfacesAndSelfTo<BulletPresenter>().AsSingle();
    }
}