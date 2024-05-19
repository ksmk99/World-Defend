using System.Threading;

namespace Gameplay
{
    public class LevelProgressModel : ILevelProgressModel
    {
        public CancellationTokenSource CancellationTokenSource { get; set; }

        public int Level { get; }
        public float AnimationTime { get; }

        public int KillCount { get; set; }
        public int Percent { get; set; }

        public LevelProgressModel(float animationTime, int level)
        {
            AnimationTime = animationTime;
            Level = level;
        }
    }
}
