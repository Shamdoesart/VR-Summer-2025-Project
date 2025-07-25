using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine.InputSystem;
using Unity.Android.Gradle;

public class NodeSpawner : MonoBehaviour
{

    [Header("CSV Source")]
    public TextAsset csvFile;   //Drop the File

    [Header("Prefabs")]
    public GameObject nodePrefab; //Sphere Prefab
    public GameObject floatingText;//TMP Prefab

    [Header("Layout")]
    public float horizontalSpacing = 2f; //Left-Right spacing within level
    public float verticalSpacing = 2f; //Distance between levels
    public float baseHeight = 3f; //Overall Y offset



    private Dictionary<string, NodeData> nodeLookup = new();


    // Start is called before the first frame update
    void Start()
    {
        if (!ParseCsv()) return;
        SpawnNodesByLevel();
        BuildConnections();
    }

    bool ParseCsv()
    {
        if (csvFile == null)
        {
            Debug.LogError("NodeSpawner: No CSV file assigned.");
            return false;
        }
        string[] lines = csvFile.text.Split('\n');
        if (lines.Length < 2)
        {
            Debug.LogError("CSV appears empty.");
            return false;
        }

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');
            if (parts.Length < 4)
            {
                Debug.LogWarning($"Bad CSV row: {line}");
                continue;
            }
            NodeData nd = new NodeData
            {
                name = parts[0].Trim(),
                title = parts[1].Trim(),
                reportsTo = parts[2].Trim(),
                level = int.Parse(parts[3].Trim())
            };
            nodeLookup[nd.name] = nd;
        }
        return true;
    }

    void SpawnNodesByLevel()
{
    Dictionary<int, List<NodeData>> levels = new();
    foreach (NodeData nd in nodeLookup.Values)
    {
        if (!levels.ContainsKey(nd.level))
            levels[nd.level] = new List<NodeData>();
        levels[nd.level].Add(nd);
    }

    foreach (var kvp in levels)
    {
        int level = kvp.Key;
        List<NodeData> row = kvp.Value;

        // Optional: sort each level alphabetically for consistent layout
        row.Sort((a, b) => a.name.CompareTo(b.name));

        float startX = -((row.Count - 1) * horizontalSpacing / 2f);

        for (int i = 0; i < row.Count; i++)
        {
            NodeData node = row[i];

            // 👇 Flip the Y to descend top-down
            float x = startX + i * horizontalSpacing;
            float y = baseHeight - level * verticalSpacing;
            Vector3 localPos = new Vector3(x, y, 0); // Set Z to 0 for flat layout

            Vector3 worldPos = transform.TransformPoint(localPos);

            node.nodeRef = CreateNode(node, worldPos);

            if (floatingText != null)
            {
                CreateNodeText($"{node.name}\n({node.title})", worldPos + new Vector3(0, 0.25f, 0));
            }
        }
    }
}

  GraphNode CreateNode(NodeData data, Vector3 pos)
{
    GameObject go = Instantiate(nodePrefab, pos, Quaternion.identity, transform);
    go.name = $"Node_{data.name}";

    GraphNode gn = go.AddComponent<GraphNode>();
    gn.nodeName = data.name;
    gn.neighbors = new List<GraphNode>();

    GraphNodeInfoDisplay display = go.AddComponent<GraphNodeInfoDisplay>();
    display.myData = data;

    // Optional: ensure node has a collider
    if (go.GetComponent<Collider>() == null)
    {
        go.AddComponent<SphereCollider>();
    }

    return gn;
}

    void CreateNodeText(string name, Vector3 pos)
    {
        GameObject labelObj = Instantiate(floatingText, pos, Quaternion.identity, transform);
        labelObj.name = $"Label_{name}";

        TMP_Text t = labelObj.GetComponent<TMP_Text>();
        if (t != null)
        {
            t.text = name;
        }
        else
        {
            Debug.LogWarning("FloatingText prefab lacks TMP_Text.");
        }
    }

    void BuildConnections()
    {
        foreach (NodeData nd in nodeLookup.Values)
        {
            if (string.IsNullOrWhiteSpace(nd.reportsTo)) continue;

            if (!nodeLookup.TryGetValue(nd.reportsTo, out NodeData boss))
            {
                Debug.LogWarning($"Manager '{nd.reportsTo}' not found for '{nd.name}'");
                continue;
            }

            boss.nodeRef.neighbors.Add(nd.nodeRef);
        }
    }


   
}
