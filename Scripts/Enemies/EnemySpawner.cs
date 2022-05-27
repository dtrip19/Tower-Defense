using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] MapSpawnsScriptableObject mapSpawns;

    public Vector3[] positions;
    private int waveIndex;
    private bool playing;
    private float timeStartedWave;

    private float TimeSinceWaveStart => Time.time - timeStartedWave;

    public static event Action<int> OnSkipWave;
    public event Action OnEndWave;

    public void StartWave()
    {
        if (playing) return;

        timeStartedWave = Time.time;
        playing = true;
        StartCoroutine(StartWaveCoroutine());
    }

    private IEnumerator StartWaveCoroutine()
    {
        List<EnemyPackScriptableObject> activePacks = new List<EnemyPackScriptableObject>();
        var wave = mapSpawns.waves[waveIndex];
        int numPacks = wave.enemyPacks.Count;
        bool[] finishedPacks = new bool[numPacks];
        float[] timesLastSpawned = new float[numPacks];

        while (Array.Exists(finishedPacks, b => b == false))
        {
            for (int i = 0; i < numPacks; i++)
            {
                if (!finishedPacks[i] && TimeSinceWaveStart >= wave.enemyPacks[i].spawnStartTime && !activePacks.Contains(wave.enemyPacks[i]))
                    activePacks.Add(wave.enemyPacks[i]);
            }

            float time = Time.time;
            for (int i = activePacks.Count - 1; i >= 0; i--)
            {
                var pack = activePacks[i];
                int packIndex = wave.enemyPacks.IndexOf(pack);
                if (TimeSinceWaveStart > pack.spawnStartTime + pack.spawnInterval * pack.numEnemies)
                {
                    activePacks.Remove(pack);
                    finishedPacks[packIndex] = true;
                }
                else if (time >= timesLastSpawned[packIndex] + pack.spawnInterval)
                {
                    SpawnEnemy(pack.enemySO);
                    timesLastSpawned[packIndex] = time;
                }
            }

            yield return null;
        }

        playing = false;
        waveIndex++;
        OnEndWave?.Invoke();
    }

    private void SpawnEnemy(EnemyScriptableObject enemySO)
    {
        var enemy = Instantiate(enemyObject).GetComponent<Enemy>();
        
        enemy.Transform.position = positions[0];
        enemy.GetComponent<Collider>().isTrigger = true;
        enemy.positions = positions;
        enemy.InitEnemy(enemySO);
    }

    public void SkipWave()
    {
        var wave = mapSpawns.waves[waveIndex];
        int wavePointValue = 0;
        foreach (var pack in wave.enemyPacks)
        {
            wavePointValue += pack.enemySO.points * pack.numEnemies;
        }
        OnSkipWave?.Invoke(wavePointValue);
        waveIndex++;
    }
}
