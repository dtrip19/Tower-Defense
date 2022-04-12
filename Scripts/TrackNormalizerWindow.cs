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
            float originalY = lineRenderer.GetPosition(i).y;
            var newPosition = (lineRenderer.GetPosition(i - 1) + lineRenderer.GetPosition(i + 1)) / 2;
            newPosition = new Vector3(newPosition.x, originalY, newPosition.z);
            lineRenderer.SetPosition(i, newPosition);
        }
    }
}
