using Helpers;

namespace Unit
{
    public interface IRoomResettable
    {
        void Reset(SignalOnRoomReset signal);
    }
}