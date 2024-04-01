namespace Unit
{
    public class HitModel : IHitModel
    {
        public HitSettings Settings { get; private set; }
        public HitRuntimeSettings RuntimeSettings { get; private set; }

        public float EndOfLifeTime { get; set; }

        public HitModel(HitSettings settings)
        {
            Settings = settings;
        }

        public void Init(HitRuntimeSettings settings)
        {
            RuntimeSettings = settings;
        }
    }
}
