using UnityEngine;

public class EnemyModel
{
    public Vector3 Position => enemyFacade.transform.position;
    public Transform Transform => enemyFacade.transform;

    private EnemyFacade enemyFacade;

    public EnemyModel(EnemyFacade enemyFacade)
    {
        this.enemyFacade = enemyFacade;
    }
}
