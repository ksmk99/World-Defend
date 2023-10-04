using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class AnimationsController : IAnimationsController
    {
        private readonly Animator animator;
        private readonly AnimationData data;
        private readonly Transform transform;

        public AnimationsController(Animator animator, AnimationData data, Transform transform)
        {
            this.animator = animator;
            this.data = data;
            this.transform = transform;
        }

        public void SetMovement(SignalOnMove signal)
        {
            if (transform.Equals(signal.Sender))
            {
                var id = data.GetAnimationID(data.IsMoving);
                animator.SetBool(id, signal.IsMoving);
            }
        }

        public void TriggerAttack(SignalOnAttack signal)
        {
            var id = data.GetAnimationID(data.ShootTrigger);
            animator.SetTrigger(id);
        }

        public void TriggerDeath()
        {
            var id = data.GetAnimationID(data.Death);
            animator.SetTrigger(id);
        }

        public void TriggerRespawn()
        {
            var id = data.GetAnimationID(data.Respawn);
            animator.SetTrigger(id);
        }
    }
}