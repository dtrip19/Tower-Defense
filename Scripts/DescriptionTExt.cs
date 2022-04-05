using UnityEngine;
using TMPro;

public class DescriptionText : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    
    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        Describable.OnInspect += FillDescriptionBox;
        Describable.OnUninspect += EmptyDescription;
    }

    private void FillDescriptionBox(TowerData data)
    {
        textMesh.text = $"{data.description}\n\nAttack Speed: {data.attackDelay} | Damage: {data.damage} \nSpeed: {data.bulletSpeed}" +
            $"              | LifeTime: {data.lifeTime}";
    }

    private void EmptyDescription()
    {
        textMesh.text = "";
    }

    private void OnDestroy()
    {
        Describable.OnInspect -= FillDescriptionBox;
        Describable.OnUninspect -= EmptyDescription;
    }
}
