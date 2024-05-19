using System.Threading;

namespace Gameplay
{
    public interface ILevelProgressModel
    {
        int Level { get; }
        float AnimationTime { get; }
        CancellationTokenSource CancellationTokenSource { get; set; }
        int KillCount { get; set; }
        int Percent { get; set; }
    }
}