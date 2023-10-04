using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    public class EnemyMovement : IMovement
    {
        private readonly PlayerPresenter player;
        private readonly EnemyMovementSettings settings;

        private readonly Transform transform;
        private readonly NavMeshAgent agent;

        public EnemyMovement(EnemyView view, EnemyMovementSettings settings, PlayerPresenter player)
        {
            this.player = player;
            this.transform = view.transform;
            this.settings = settings;
            this.agent = transform.GetComponent<NavMeshAgent>();
            agent.speed = settings.MoveSpeed;

            ClampPos();
        }

        public void Move(bool isDead, Transform target = null)
        {
            if (isDead)
            {
                agent.velocity = Vector3.zero;
                return;
            }

            var distance = Vector3.Distance(transform.position, player.Transform.position);
            if (distance < settings.MaxDistance && distance > agent.stoppingDistance)
            {
                Vector3 direction = (player.Transform.position - transform.position).normalized;
                Rotate(direction);
                NavMeshHit hit;
                if (NavMesh.SamplePosition(player.Transform.position, out hit, float.MaxValue, NavMesh.AllAreas))
                {
                    agent.SetDestination(hit.position);
                }
            }
        }

        private void Rotate(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var value = Mathf.Lerp(transform.rotation.y, angle, settings.RotateSpeed);
            transform.rotation = Quaternion.Euler(0, value, 0);
        }

        private void ClampPos()
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(transform.position, out hit, float.MaxValue, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
        }
    }
}
