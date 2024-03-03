using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Services
{
    public class MLInputService : IInputService
    {
        private Vector3 direction;

        public Vector3 GetMoveDirection()
        {
            return direction;
        }

        public void SetMoveDirection(Vector3 direction)
        {
            this.direction = direction; 
        }
    }
}
