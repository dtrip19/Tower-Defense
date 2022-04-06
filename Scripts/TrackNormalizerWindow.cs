using UnityEngine;
using UnityEditor;

public class TrackNormalizer : EditorWindow
{
    [MenuItem("Tools/Normalize Path")]
    public static void NormalizePath()
    {
        var lineRenderer = FindObjectOfType<LineRenderer>();
        int positionCount = lineRenderer.positionCount;
        for (int i = 1; i < positionCount - 1; i++)
        {
            var newPosition = (lineRenderer.GetPosition(i - 1) + lineRenderer.GetPosition(i + 1)) / 2;
            lineRenderer.SetPosition(i, newPosition);
        }
    }
}
