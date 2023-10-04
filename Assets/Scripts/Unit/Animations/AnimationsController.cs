using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unit
{
    public class AnimationsController : IAnimationsController
    {
        private readonly Animator animator;
        private readonly AnimationData data;

        public AnimationsController(Animator animator, AnimationData data)
        {
            this.animator = animator;
            this.data = data;
        }

        public void SetMovement(SignalOnMove signal)
        {
            var id = data.GetAnimationID(data.IsMoving);
            animator.SetBool(id, signal.IsMoving);
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