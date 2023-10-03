using UnityEngine;
using UnityEngine.AI;

namespace Unit
{
    public class MobMovement : IMovement
    {
        private readonly PlayerPresentor player;
        private readonly MobMovementSettings settings;

        private readonly Transform transform;
        private readonly NavMeshAgent agent;

        public MobMovement(MobMovementSettings settings, Transform transform, PlayerPresentor player)
        {
            this.player = player;
            this.settings = settings;
            this.agent = transform.GetComponent<NavMeshAgent>();
            this.transform = transform;

            agent.speed = settings.MoveSpeed;

            ClampPos();
        }

        public void Move(bool isDead)
        {
            if (isDead)
            {
                agent.velocity = Vector3.zero;
                return;
            }

            agent.SetDestination(player.Transform.position);
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
