using Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unit;

namespace UI
{
    public class EnemyCounterModel
    {
        public EnemySpawnerSettings SpawnerSettings { get; }
        public int Count { get; set; }

        public EnemyCounterModel(EnemySpawnerSettings settings)
        {
            SpawnerSettings = settings;
        }        
    }
}
