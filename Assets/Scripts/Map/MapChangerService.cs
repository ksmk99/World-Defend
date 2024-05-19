using Helpers;
using UnityEngine;
using UnityEngine.AI;
using UnityEditor.AI;

namespace Map
{
    public class MapChangerService : IMapChangerService
    {
        public GameObject[] Levels { get; }

        public MapChangerService(GameObject[] levels)
        {
            Levels = levels;
        }

        public void ChangeMap(SignalOnRoomReset signal)
        {
            var index = Random.Range(0, Levels.Length); 
            for (int i = 0; i < Levels.Length; i++) 
            {
                Levels[i].SetActive(index == i);
            }

            UnityEditor.AI.NavMeshBuilder.ClearAllNavMeshes();
            UnityEditor.AI.NavMeshBuilder.BuildNavMesh();
        }
    }
}
