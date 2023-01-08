using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace Unit
{
    public class EnemyMovement : IMovement
    {
        private readonly PlayerModel player;
        private readonly EnemyModel enemyModel;
        private readonly EnemyMovementSettings settings;

        public EnemyMovement(PlayerModel player, EnemyModel enemyModel, EnemyMovementSettings settings)
        {
            this.player = player;
            this.enemyModel = enemyModel;
            this.settings = settings;

            ClampPos();
        }

        public void Move()
        {
            var distance = Vector3.Distance(enemyModel.Position, player.Position);
            if (distance < settings.MaxDistance && distance > enemyModel.Agent.stoppingDistance)
            {
                Vector3 direction = (player.Position - enemyModel.Position).normalized;
                Rotate(direction);
                NavMeshHit hit;
                if (NavMesh.SamplePosition(player.Position, out hit, float.MaxValue, NavMesh.AllAreas))
                {
                    enemyModel.Agent.SetDestination(hit.position);
                }
            }
        }

        private void Rotate(Vector3 direction)
        {
            var transform = enemyModel.Transform;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var value = Mathf.Lerp(transform.rotation.y, angle, settings.RotateSpeed);
            transform.rotation = Quaternion.Euler(0, value, 0);
        }

        private void ClampPos()
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(enemyModel.Position, out hit, float.MaxValue, NavMesh.AllAreas))
            {
                enemyModel.Transform.position = hit.position;
            }
        }
    }
}
