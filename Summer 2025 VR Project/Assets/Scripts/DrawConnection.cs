using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawConnection : MonoBehaviour
{
    public Material lineMaterial;
    public float lineWidth = 0.05f;

    void Start()
    {
        GraphNode[] nodes = FindObjectsByType<GraphNode>(FindObjectsSortMode.None);
        foreach (GraphNode node in nodes)
        {
            foreach (GraphNode neighbor in node.neighbors)
            {
                CreateLine(node.transform.position, neighbor.transform.position);
            }
        }
    }

    void CreateLine(Vector3 start, Vector3 end)
    {
        GameObject lineObj = new GameObject("GraphLine");
        LineRenderer lr = lineObj.AddComponent<LineRenderer>();

        lr.material = lineMaterial;
        lr.positionCount = 2;
        lr.startWidth = lineWidth;
        lr.endWidth = lineWidth;
        lr.useWorldSpace = true;

        
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
    }
}
