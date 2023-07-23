﻿using UnityEngine;

namespace Unit
{
    public class PlayerMovement : IMovement
    {
        private readonly InputService inputService;
        private readonly Transform transform;
        private readonly MovementSettings movementModel;

        public PlayerMovement(InputService inputService, MovementSettings movementModel, Transform transform)
        {
            this.inputService = inputService;
            this.transform = transform;
            this.movementModel = movementModel;
        }

        public void Move(bool isDead)
        {
            Vector3 direction = inputService.GetMoveDirection();
            transform.position += direction * movementModel.MoveSpeed * Time.deltaTime;
            Rotate(direction);
        }

        private void Rotate(Vector3 direction)
        {
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            var value = Mathf.Lerp(transform.rotation.y, angle, movementModel.RotateSpeed);
            transform.rotation = Quaternion.Euler(0, value, 0);
        }
    }
}