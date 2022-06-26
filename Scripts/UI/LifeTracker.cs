using System;
using UnityEngine;
using TMPro;

public class LifeTracker : MonoBehaviour
{
    private TextMeshProUGUI textMesh;
    private int life;

    public static Action OnPlayerLose;

    private void Start()
    {
        life = 500;
        Enemy.OnReachEndPath += LoseLife;
        textMesh = GetComponent<TextMeshProUGUI>();
        textMesh.text = life.ToString();
    }

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.L)) return;

        LoseLife(-100);
    }

    private void LoseLife(int enemyHealth)
    {
        life -= enemyHealth;
        textMesh.text = life.ToString();

        if (life <= 0)
            OnPlayerLose?.Invoke();
    }  
}