using Unity.Barracuda;
using UnityEngine;

namespace Unit
{
    public static class WeaponHelper
    {
        public static UnitView GetNearestEnemy(Vector3 position, IWeaponSettings settings, Team team)
        {
            float minDistance = settings.Distance;
            UnitView result = default;
            var enemies = Physics.OverlapSphere(position, settings.Distance, settings.TargetLayer);
            for (int i = 0; i < enemies.Length; i++)
            {
                float distance = (position - enemies[i].transform.position).magnitude;
                if (distance >= settings.MinDistance && distance <= minDistance && enemies[i].TryGetComponent(out UnitView view))
                {
                    var presenter = view.GetPresenter();
                    if (presenter.Team != team && presenter.IsActive)
                    {
                        var direction = (enemies[i].transform.position - position).normalized;
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
