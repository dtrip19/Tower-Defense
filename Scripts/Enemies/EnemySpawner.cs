using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject enemyObject;
    [SerializeField] MapSpawns mapSpawns;

    private int waveIndex;
    private bool playing;
    private float timeStartedWave;
    public Vector3[] positions;

    //private int timer;
    //private int spawnIndex;
    //private string[] wave1 = new string[10];

    private float TimeSinceWaveStart => Time.time - timeStartedWave;

    public event Action OnEndWave;

    //private void Awake()
    //{
    //    for(int i = 0; i <wave1.Length; i++)
    //    {
    //        wave1[i] = "BasicEnemy";
    //    }
    //    wave1[9] = "WeakEnemy";
    //}

    public void StartWave()
    {
        if (playing) return;

        timeStartedWave = Time.time;
        playing = true;
        StartCoroutine(StartWaveCoroutine());
    }

    private IEnumerator StartWaveCoroutine()
    {
        List<EnemyPackSciptableObject> activePacks = new List<EnemyPackSciptableObject>();
        EnemyWaveScriptableObject wave = mapSpawns.waves[waveIndex];
        int numPacksFinished = 0;

        while (numPacksFinished < wave.enemyPacks.Count)
        {
            foreach (var pack in wave.enemyPacks)
            {
                if (!pack.finished && TimeSinceWaveStart >= pack.spawnStartTime && !activePacks.Contains(pack))
                {
                    activePacks.Add(pack);
                }
            }

            float time = Time.time;
            foreach (var pack in activePacks)
            {
                //print($"{pack.enemySO.name}: {pack.spawnStartTime + pack.spawnInterval * pack.numEnemies}");
                if (TimeSinceWaveStart > pack.spawnStartTime + pack.spawnInterval * pack.numEnemies)
                {
                    //print(pack.enemySO.name + " finished");
                    activePacks.Remove(pack);
                    pack.finished = true;
                    numPacksFinished++;
                    break;
                }
                else if (time >= pack.timeLastSpawn + pack.spawnInterval)
                {
                    SpawnEnemy(pack.enemySO);
                    pack.timeLastSpawn = time;
                }
            }

            yield return null;
        }

        foreach (var pack in wave.enemyPacks)
        {
            pack.finished = false; //Must undo this or else any changes made to the SO will be serialized lmao
            pack.timeLastSpawn = 0;
        }
        playing = false;
        waveIndex++;

        OnEndWave?.Invoke();
    }

    //private void FixedUpdate()
    //{
    //    timer++;
    //    if (timer >= 100 && spawnIndex <= 9)
    //    {
    //
    //        timer = 0;
    //        SpawnEnemy(wave1[spawnIndex++]);
    //    }
    //}

    private void SpawnEnemy(EnemyScriptableObject enemySO)
    {
        var enemy = Instantiate(enemyObject).GetComponent<Enemy>();
        
        enemy.Transform.position = positions[0];
        enemy.GetComponent<Collider>().isTrigger = true;
        enemy.positions = positions;
        enemy.InitEnemy(enemySO);
    }
}
