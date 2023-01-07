using UnityEngine;
using Zenject;
using static UnityEngine.UI.CanvasScaler;

namespace Unit
{
    public class PlayerMovement : IMovement
    {
        private readonly InputService inputService;
        private readonly PlayerModel playerModel;
        private readonly MovementSettings movementModel;

        public PlayerMovement(InputService inputService, PlayerModel playerModel, MovementSettings movementModel)
        {
            this.inputService = inputService;
            this.playerModel = playerModel;
            this.movementModel = movementModel;
        }

        public void Move()
        {
            Vector3 direction = inputService.GetMoveDirection();
            //Debug.Log(direction);
            Rotate(direction);
            playerModel.Transform.position += direction * movementModel.MoveSpeed * Time.deltaTime;
        }

        private void Rotate(Vector3 direction)
        {
            var transform = playerModel.Transform;
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var value = Mathf.Lerp(transform.rotation.y, angle, movementModel.RotateSpeed);
            transform.rotation = Quaternion.Euler(0, value, 0);
        }
    }
}