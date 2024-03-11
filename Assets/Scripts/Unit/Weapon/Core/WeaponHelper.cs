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
                    if (view.GetPresenter().Team != team)
                    {
                        minDistance = distance;
                        result = view;
                    }
                }
            }

            return result;
        }
    }
}
