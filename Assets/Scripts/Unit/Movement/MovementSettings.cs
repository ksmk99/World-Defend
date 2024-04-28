using System;
using UnityEngine;

namespace Unit
{
    [CreateAssetMenu()]
    public class MovementSettings : ScriptableObject
    {
        public float MoveSpeed;
        public float RotateSpeed;
    }
}