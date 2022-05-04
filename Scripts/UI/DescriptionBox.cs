using UnityEngine;
using TMPro;

public class DescriptionBox : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI price;
    [SerializeField] TextMeshProUGUI damage;
    [SerializeField] TextMeshProUGUI delay;
    [SerializeField] TextMeshProUGUI pierce;
    [SerializeField] TextMeshProUGUI speed;
    [SerializeField] TextMeshProUGUI lifetime;
    [SerializeField] TextMeshProUGUI range;
    [SerializeField] TextMeshProUGUI size;
    
    private void Start()
    {
        Describable.OnInspect += FillDescriptionBox;
        Describable.OnUninspect += EmptyDescription;
    }

    private void FillDescriptionBox(TowerData data)
    {
        description.text = data.towerName + "\n"+ data.description;
        price.text = data.price.ToString();
        damage.text = data.damage.ToString();
        delay.text = data.attackDelay.ToString();
        pierce.text = data.pierce.ToString();
        speed.text = data.bulletSpeed.ToString();
        lifetime.text = data.lifeTime.ToString();
        range.text = data.range.ToString();
        size.text = data.size.ToString();
    }

    private void EmptyDescription()
    {
        description.text = "";
        price.text = "";
        damage.text = "";
        delay.text = "";
        pierce.text = "";
        speed.text = "";
        lifetime.text = "";
        range.text = "";
        size.text = "";
    }

    private void OnDestroy()
    {
        Describable.OnInspect -= FillDescriptionBox;
        Describable.OnUninspect -= EmptyDescription;
    }
}
