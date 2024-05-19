using Helpers;
using UnityEngine;

namespace Map
{
    public interface IMapChangerService
    {
        GameObject[] Levels { get; }

        void ChangeMap(SignalOnRoomReset signal);
    }
}