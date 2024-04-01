using Helpers;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class HealthFollower
    {
        private readonly HealthParentFlag flag;
        private readonly Transform target;
        private readonly SignalBus signalBus;

        public HealthFollower(HealthParentFlag flag, Transform target, SignalBus signalBus)
        {
            this.flag = flag;
            this.target = target;
            this.signalBus = signalBus;
        }

        public void Init(HealthView view)
        {
            view.transform.SetParent(flag.transform);
        }

        public void Update(HealthView view, bool isDead)
        {
            if (isDead || !CheckInBounds())
            {
                view.gameObject.SetActive(false);
                return;
            }

            Follow(view.Rect, view);
            view.gameObject.SetActive(true);
        }

        public void Disable(HealthView view)
        {
            view.gameObject.SetActive(false);
        }

        public void Follow(RectTransform rect, HealthView view)
        {
            Vector3 viewportPos = Camera.main.WorldToViewportPoint(target.position);

            float width = flag.Rect.sizeDelta.x;
            float height = flag.Rect.sizeDelta.y;
            var pos = new Vector3(width * viewportPos.x - width / 2, height * viewportPos.y - height / 2);

            viewportPos = pos;
            viewportPos.x = Mathf.FloorToInt(viewportPos.x);
            viewportPos.y = Mathf.FloorToInt(viewportPos.y);
            viewportPos.z = 0f;

            rect.anchoredPosition = viewportPos + view.Offset;
        }

        public bool CheckInBounds()
        {
            float width = flag.Rect.sizeDelta.x;
            float height = flag.Rect.sizeDelta.y;

            Vector3 viewportPos = Camera.main.WorldToViewportPoint(target.position);
            var pos = new Vector3(width * viewportPos.x - width / 2, height * viewportPos.y - height / 2);

            bool xCheck = -width / 2f < pos.x && width / 2f > pos.x;
            bool yCheck = -height / 2f < pos.y && height / 2f > pos.y;

            return xCheck && yCheck;
        }
    }
}
