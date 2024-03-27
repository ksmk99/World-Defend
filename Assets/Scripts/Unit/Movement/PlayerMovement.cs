using Helpers;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class PlayerMovement : IMovement
    {
        private readonly IInputService inputService;
        private readonly Transform transform;
        private readonly SignalBus signalBus;
        private readonly MovementSettings movementModel;

        public PlayerMovement(IInputService inputService, MovementSettings movementModel, Transform transform, SignalBus signalBus)
        {
            this.inputService = inputService;
            this.transform = transform;
            this.signalBus = signalBus;
            this.movementModel = movementModel;
        }

        public void Move(bool isDead, Transform target = null)
        {
            Vector3 direction = inputService.GetMoveDirection();
            SendMoveSignal(direction);
            Rotate(direction, target);

            transform.position += direction.normalized * movementModel.MoveSpeed * Time.deltaTime;
        }

        private void Rotate(Vector3 direction, Transform target)
        {
            direction = target == null ? direction : (target.position - transform.position).normalized;

            if(direction == Vector3.zero)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, movementModel.RotateSpeed * Time.deltaTime);
        }

        private void SendMoveSignal(Vector3 direction)
        {
            var signal = new SignalOnMove(direction.sqrMagnitude > 0, transform);
            signalBus.TryFire(signal);
        }

        public void SetTarget(UnitPresenter presenter)
        {

        }
    }
}