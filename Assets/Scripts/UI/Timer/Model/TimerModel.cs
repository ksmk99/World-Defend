using Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public class TimerModel
    {
        public LevelSettingsData LevelSettingsData { get; }
        public float StartTime { get; set; }    
        public float EndTime { get; set; }

        public TimerModel(LevelSettingsData levelSettingsData)
        {
            LevelSettingsData = levelSettingsData;
        }
    }
}
