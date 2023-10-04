using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    public class MobMovement : IMovement
    {
        private readonly PlayerPresenter player;
        private readonly MobMovementSettings settings;

        private readonly Transform transform;
        private readonly NavMeshAgent agent;

        public MobMovement(MobMovementSettings settings, Transform transform, PlayerPresenter player)
        {
            this.player = player;
            this.settings = settings;
            this.agent = transform.GetComponent<NavMeshAgent>();
            this.transform = transform;

            agent.speed = settings.MoveSpeed;
            agent.updateRotation = false;

            ClampPos();
        }

        public void Move(bool isDead, Transform target = null)
        {
            if (isDead)
            {
                agent.velocity = Vector3.zero;
                return;
            }

            agent.SetDestination(player.Transform.position);
            var direction = (agent.nextPosition - transform.position).normalized;
            Rotate(direction, target);
        }


        private void Rotate(Vector3 direction, Transform target)
        {
            direction = target == null ? direction : (target.position - transform.position).normalized;

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
