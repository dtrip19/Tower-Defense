using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DescriptionText : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        Describable.OnInspect += FillDescriptionBox;
    }
    void FillDescriptionBox(TowerData data){
        //textMesh.text = data.description + "AS: "+ data.attackSpeed + " Da: " + " Sp: " + data.description;
        textMesh.text = $"{data.description}\n\nAttack Speed: {data.attackDelay} | Damage: {data.damage} \nSpeed: {data.bulletSpeed}              | LifeTime: {data.lifeTime}";
    }
}
