using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private RectTransform rect;
    private Enemy followTarget;
    private Camera mainCam;

    public void SetFollowTarget(Enemy enemy, Camera mainCam)
    {
        slider = GetComponent<Slider>();
        rect = GetComponent<RectTransform>();

        followTarget = enemy;
        this.mainCam = mainCam;
        slider.maxValue = enemy.Health;
        slider.value = enemy.Health;
        StartCoroutine(FollowTargetCoroutine());
    }

    private IEnumerator FollowTargetCoroutine()
    {
        while (followTarget != null && !followTarget.Equals(null))
        {
            slider.value = followTarget.Health;
            rect.anchoredPosition = mainCam.WorldToScreenPoint(followTarget.LineOfSightPosition) + new Vector3(-Screen.width / 2, -Screen.height / 2 + 35);
            yield return null;
        }

        Destroy(gameObject);
    }
}
