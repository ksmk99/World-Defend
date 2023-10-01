using System;
using Unit;
using Unit.Bullet;
using UnityEngine;
using Zenject;
using static Zenject.CheatSheet;

public class MobsInstaller : MonoInstaller
{
    [SerializeField] private GameObject prefab;

    public override void InstallBindings()
    {
        InstallMobFactory();
    }

    private void InstallMobFactory()
    {
        Container.BindFactory<MobView, MobView.Factory>()
            .FromMonoPoolableMemoryPool(
                    x => x.WithInitialSize(2)
                    .FromComponentInNewPrefab(prefab)
                    .UnderTransformGroup("Mobs"));

        Container.BindInterfacesAndSelfTo<MobSpawner>().AsSingle();
    }
}
