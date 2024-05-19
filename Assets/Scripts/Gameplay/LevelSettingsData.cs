using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "Level Settings", menuName = "Game/Level Settings")]
    public class LevelSettingsData : ScriptableObject
    {
        public float Duration = 60f;
    }
}
