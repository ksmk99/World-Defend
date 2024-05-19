using UnityEngine;

namespace Helpers
{
    public class HealthParentFlag : PoolParentFlag
    {
        [field: SerializeField]
        public RectTransform Rect { get; private set; }
    }
}
