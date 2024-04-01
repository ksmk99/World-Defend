using UnityEngine;

namespace Unit
{
    public class EnemyDetectorModel : IEnemyDetectorModel
    {
        public EnemySpawner EnemySpawner { get; }
        public Transform Transform { get; }

        public EnemyDetectorData Data { get; }

        public EnemyDetectorModel(EnemySpawner enemySpawner, Transform transform, EnemyDetectorData data)
        {
            EnemySpawner = enemySpawner;
            Transform = transform;
            Data = data;
        }
    }
}
