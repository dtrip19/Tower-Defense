using UnityEngine;
using UnityEditor;

public class EditorTools : EditorWindow
{
    [MenuItem("Tools/Normalize Path")]
    public static void NormalizePath()
    {
        var lineRenderer = FindObjectOfType<LineRenderer>();
        int positionCount = lineRenderer.positionCount;
        for (int i = 1; i < positionCount - 1; i++)
        {
            float originalY = lineRenderer.GetPosition(i).y;
            var newPosition = (lineRenderer.GetPosition(i - 1) + lineRenderer.GetPosition(i + 1)) / 2;
            newPosition = new Vector3(newPosition.x, originalY, newPosition.z);
            lineRenderer.SetPosition(i, newPosition);
        }
    }

    [MenuItem("Tools/WaveAnalysis/EmojiMap")]
    public static void AnalyzeEmojiMapWave() => AnalyzeWave("EnemyWAves/EmojiMap/EmojiMapWave", FindObjectOfType<WaveAnalysisNumber>().waveNumber);


    public static void AnalyzeWave(string filePath, int waveNumber)
    {
        var wave = Resources.Load<EnemyWaveScriptableObject>(filePath + waveNumber);
        var packs = wave.enemyPacks;

        int enemyCount = 0;
        foreach (var pack in packs)
        {
            enemyCount += pack.numEnemies;
        }

        int pointValue = 0;
        foreach (var pack in packs)
        {
            pointValue += pack.numEnemies * pack.enemySO.points;
        }

        float duration = 0;
        foreach (var pack in packs)
        {
            float packDuration = pack.spawnStartTime + pack.numEnemies * pack.spawnInterval;
            if (packDuration > duration)
                duration = packDuration;
        }

        int enemyHealthTotal = 0;
        foreach (var pack in packs)
        {
            enemyHealthTotal += pack.numEnemies * pack.enemySO.health;
        }
        Debug.Log(wave.ToString() + " Analysis:");
        Debug.Log("Enemy Count: " + enemyCount);
        Debug.Log("Total Point Value: " + pointValue);
        Debug.Log("Duration: " + duration + " seconds");
        Debug.Log("Total Enemy Health: " + enemyHealthTotal);
        Debug.Log("Point Value to Total Enemy Health Ratio: " + (float)pointValue / enemyHealthTotal);
        Debug.Log("Estimated Difficulty: " + enemyHealthTotal / duration);
    }

    [MenuItem("Tools/EnemyDifficulty")]
    public static void EnemyDifficulty()
    {
        var enemies = Resources.LoadAll<EnemyScriptableObject>("Enemies");
        foreach(var enemy in enemies)
        {
            int attributeDifficulty = 1;
            enemy.difficulty = enemy.health * enemy.moveSpeed * attributeDifficulty;
        }

    }
}
