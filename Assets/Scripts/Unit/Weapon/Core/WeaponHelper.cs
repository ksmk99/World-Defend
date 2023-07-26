﻿using UnityEngine;

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
                float distance = (position - enemies[i].transform.position).sqrMagnitude;
                if (distance <= minDistance && enemies[i].TryGetComponent(out UnitView view))
                {
                    if (view.GetTeam() != team)
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
