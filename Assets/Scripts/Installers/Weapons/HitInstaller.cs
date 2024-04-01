using Unit;
using UnityEngine;
using Zenject;


public class HitInstaller : MonoInstaller<HitInstaller>
{
    [SerializeField] private HitSettings settings;

    public override void InstallBindings()
    {
        Container.Bind<HitView>().FromComponentOnRoot().AsSingle();
        Container.Bind<HitModel>().AsSingle().WithArguments(settings);
        Container.BindInterfacesAndSelfTo<HitPresenter>().AsSingle();
    }
}