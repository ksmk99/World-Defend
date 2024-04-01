using System;

namespace Unit
{
    public interface IBulletSettings
    {
        float Speed { get; set; }
        Type BulletType { get; }
    }
}
