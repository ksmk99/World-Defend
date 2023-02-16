using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyModel
{
    public Vector3 Position => enemyFacade.transform.position;
    public Transform Transform => enemyFacade.transform;
    public NavMeshAgent Agent => enemyFacade.Agent; 

    private EnemyView enemyFacade;

    public readonly IMovement Movement;

    public EnemyModel(EnemyView enemyFacade, IMovement movement)
    {
        this.enemyFacade = enemyFacade;
        Movement = movement;
    }
}
