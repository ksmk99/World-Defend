using UnityEngine.AI;
using Zenject;

public class EnemyView : UnitView
{
    public NavMeshAgent Agent;
    public EnemyType Type;

    [Inject]
    public void Init(EnemyPresenter presenter)
    {
        this.presenter = presenter;
    }

    public override void Death()
    {
        GetComponentInChildren<UnitActivator>()?.Enable();
        gameObject.SetActive(false);
    }

    public override UnitPresenter GetPresenter()
    {
        return presenter;
    }

    public override int GetPoolID()
    {
        return (int)Type;
    }

    public class Factory : PlaceholderFactory<UnityEngine.Object, EnemyView>
    {
    }
}

public enum EnemyType
{
    None,
    Default,
    Big
}
//public class CustomEnemyFactory : IFactory<EnemyView>
//{
//    private DiContainer container;
//    private ISpawnManager spawnManager;


//    public CustomEnemyFactory(DiContainer container, ISpawnManager spawnManager)
//    {
//        this.container = container;
//        this.spawnManager = spawnManager;
//    }

//    public EnemyView Create()
//    {
//        var enemy = container.InstantiatePrefab(spawnManager.GetPrefab());
//        return enemy.GetComponent<EnemyView>();
//    }
//}
