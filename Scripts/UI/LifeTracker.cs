using UnityEngine;
using TMPro;

public class LifeTracker : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private int life = 500;

    private void Start()
    {
        Enemy.OnReachEndPath += LoseLife;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = life.ToString();
    }

    private void LoseLife(int enemyHealth)
    {
        life -= enemyHealth;
        textMesh.text = life.ToString();
    }  
}