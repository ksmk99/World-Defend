namespace Unit
{
    public class EffectModel
    {
        public float ActionTimer { get; set; }
        public float TTL { get; set; }
        public IEffectSettings Settings { get; }

        public EffectModel(IEffectSettings settings)
        {
            Settings = settings;
        }
    }
}
