using UnityEngine;
using TMPro;

public class LifeTracker : MonoBehaviour
{
    TextMeshProUGUI textMesh;
    int life = 50;

    // Start is called before the first frame update
    void Start()
    {
        Enemy.OnReachEndPath += LoseLife;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = life.ToString();
    }

    void LoseLife(int enemyHealth)
    {
        life -= enemyHealth;
        textMesh.text = life.ToString();
    }
    
}
