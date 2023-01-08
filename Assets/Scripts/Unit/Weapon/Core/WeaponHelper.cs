using UnityEngine;

namespace Unit
{
    public static class WeaponHelper
    {
        public static EnemyView GetNearestEnemy(Vector3 position, IWeaponSettings settings)
        {
            float minDistance = float.MaxValue;
            EnemyView result = default;
            var enemies = Physics.OverlapSphere(position, settings.Distance, settings.TargetLayer);
            for (int i = 0; i < enemies.Length; i++)
            {
                float distance = (position - enemies[i].transform.position).sqrMagnitude;
                if (distance < minDistance &&
                    enemies[i].TryGetComponent(out EnemyView view))
                {
                    minDistance = distance;
                    result = view;
                }
            }

            return result;
        }
    }
}
