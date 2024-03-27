using Unity.Barracuda;
using UnityEngine;

namespace Unit
{
    public static class WeaponHelper
    {
        public static UnitView GetNearestEnemy(Vector3 position, IWeaponSettings settings, Team team)
        {
            float minDistance = settings.Distance;
            position.y = 0.5f;
            UnitView result = default;
            var enemies = Physics.OverlapSphere(position, settings.Distance, settings.TargetLayer);
            for (int i = 0; i < enemies.Length; i++)
            {
                var enemyPos = enemies[i].transform.position;
                enemyPos.y = 0.5f;

                float distance = (position - enemyPos).magnitude;
                if (distance >= settings.MinDistance && distance <= minDistance && enemies[i].TryGetComponent(out UnitView view))
                {
                    var presenter = view.GetPresenter();
                    if (presenter.Team != team && presenter.IsActive)
                    {
                        var direction = (enemyPos - position).normalized;
                        if (!Physics.Raycast(position, direction, distance, settings.BlockLayer))
                        {
                            minDistance = distance;
                            result = view;
                        }
                    }
                }
            }

            return result;
        }
    }
}
