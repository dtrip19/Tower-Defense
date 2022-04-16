using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextWaveButton : MonoBehaviour
{
    [SerializeField] EnemySpawner spawner;

    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartWave);
    }

    private void StartWave()
    {
        spawner.StartWave();
        button.interactable = false;
        spawner.OnEndWave += SetButtonInteractable;
    }

    private void SetButtonInteractable()
    {
        button.interactable = true;
    }
}
