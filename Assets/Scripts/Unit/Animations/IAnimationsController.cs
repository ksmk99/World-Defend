using Helpers;

namespace Unit
{
    public interface IAnimationsController
    {
        void SetMovement(SignalOnMove signal);
        void TriggerAttack(SignalOnAttack signal);
        void TriggerDeath();
        void TriggerRespawn();
    }
}