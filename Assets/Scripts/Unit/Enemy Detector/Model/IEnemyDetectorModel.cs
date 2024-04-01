using UnityEngine;

namespace Unit
{
    public interface IEnemyDetectorModel
    {
        EnemyDetectorData Data { get; }
        EnemySpawner EnemySpawner { get; }
        Transform Transform { get; }
    }
}