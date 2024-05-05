﻿using System;
using Random = UnityEngine.Random;

namespace Unit
{
    public class SpawnManager : ISpawnManager
    {
        private readonly SpawnerSettings settings;

        public SpawnManager(SpawnerSettings settings)
        {
            this.settings = settings;
        }

        public UnitView GetPrefab()
        {
            var prefab = settings.Types[Random.Range(0, settings.Types.Length)].Prefab;
            return prefab;
        }
    }

    [Serializable]
    public struct SpawnerSettings
    {
        public SpawnType[] Types;
    }

    [Serializable]
    public struct SpawnType
    {
        public int Index;
        public UnitView Prefab;
    }
}
