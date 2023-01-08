using Installers;
using System.Collections;
using System.Collections.Generic;
using Unit;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private GameObject enemy;

    public override void InstallBindings()
    {
        Container.BindFactory<EnemyView, EnemyView.Factory>()
            .FromPoolableMemoryPool<EnemyView, EnemyFacadePool>(poolBinder => poolBinder
            .WithInitialSize(5)
            .FromSubContainerResolve()
            .ByNewPrefabInstaller<EnemyInstaller>(enemy)
            .UnderTransformGroup("Enemy"));

        Container.BindInterfacesAndSelfTo<EnemySpawner>().AsSingle();
    }

    public class EnemyFacadePool : MonoPoolableMemoryPool<IMemoryPool, EnemyView>
    {
    }
}
