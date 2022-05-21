using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Battle.UI
{
    public class AmmoBar : MonoBehaviour
    {
        private Slider slider;
        private RectTransform rect;
        private TowerBehaviorBase followTarget;
        private Camera mainCam;

        public void SetFollowTarget(TowerBehaviorBase behavior, Camera mainCam)
        {
            slider = GetComponent<Slider>();
            rect = GetComponent<RectTransform>();

            followTarget = behavior;
            this.mainCam = mainCam;
            slider.maxValue = behavior.maxAmmo;
            slider.value = behavior.maxAmmo;
            StartCoroutine(FollowTargetCoroutine());
        }

        private IEnumerator FollowTargetCoroutine()
        {
            while (followTarget != null && !followTarget.Equals(null))
            {
                slider.value = followTarget.ammo;
                rect.anchoredPosition = mainCam.WorldToScreenPoint(followTarget.BulletOrigin) + new Vector3(-Screen.width / 2, -Screen.height / 2 + 35);
                yield return null;
            }

            Destroy(gameObject);
        }
    }
}