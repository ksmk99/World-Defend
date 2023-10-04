using UnityEngine;
using Zenject;

namespace Unit
{
    public class PlayerMovement : IMovement
    {
        private readonly InputService inputService;
        private readonly Transform transform;
        private readonly SignalBus signalBus;
        private readonly MovementSettings movementModel;

        public PlayerMovement(InputService inputService, MovementSettings movementModel, Transform transform, SignalBus signalBus)
        {
            this.inputService = inputService;
            this.transform = transform;
            this.signalBus = signalBus;
            this.movementModel = movementModel;
        }

        public void Move(bool isDead, Transform target = null)
        {
            Vector3 direction = inputService.GetMoveDirection();
            transform.position += direction.normalized * movementModel.MoveSpeed * Time.deltaTime;

            SendMoveSignal(direction);
            Rotate(direction, target);
        }

        private void Rotate(Vector3 direction, Transform target)
        {
            direction = target == null ? direction : (target.position - transform.position).normalized;

            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var value = Mathf.Lerp(transform.rotation.y, angle, movementModel.RotateSpeed);
            transform.rotation = Quaternion.Euler(0, value, 0);
        }

        private void SendMoveSignal(Vector3 direction)
        {
            var signal = new SignalOnMove(direction.sqrMagnitude > 0);
            signalBus.TryFire(signal);
        }

    }
}