using UnityEngine;
using TMPro;

namespace Battle.UI
{
    public class AmmoRefillPopup : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textMesh;

        private CanvasGroup group;
        private RectTransform rect;

        private void Awake()
        {
            group = GetComponent<CanvasGroup>();
            rect = GetComponent<RectTransform>();
            Tower.OnMouseEnter += Appear;
            Tower.OnMouseExit += Disappear;
        }

        private void Appear(int ammoRefillPrice)
        {
            group.alpha = 1;
            textMesh.text = $"Refill ammo\n{ammoRefillPrice} points";
            rect.anchoredPosition = Input.mousePosition + new Vector3(-Screen.width / 2, -Screen.height / 2 + 100);
        }

        private void Disappear()
        {
            group.alpha = 0;
        }

        private void OnDestroy()
        {
            Tower.OnMouseEnter -= Appear;
            Tower.OnMouseExit -= Disappear;
        }
    }
}