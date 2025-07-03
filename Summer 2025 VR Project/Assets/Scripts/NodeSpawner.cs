using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEditor.Networking.PlayerConnection;

public class NodeSpawner : MonoBehaviour
{
    //Game Object
    public GameObject nodePrefab;
    public GameObject floatingText;

    //Spacing for the grpah
    public float horizontalSpacing = 2f;
    public float verticalSpacing = 2f;
    public float baseHeight = 3f;



    private Dictionary<string, GraphNode> nodeLookup = new Dictionary<string, GraphNode>();


    // Start is called before the first frame update
    void Start()
    {
        //Define nodes by level
        List<List<string>> levels = new List<List<string>>
        {
            new List<string>{ "A"},
            new List<string>{ "B", "C" },
            new List<string> { "D", "E", "F" }
        };

        for (int level = 0; level < levels.Count; level++)
        {
            List<string> row = levels[level];
            float startX = -((row.Count - 1) * horizontalSpacing) / 2f;

            for (int i = 0; i < row.Count; i++)
            {
                string nodeName = row[i];
                Vector3 position = new Vector3(startX + i * horizontalSpacing, baseHeight + -level * verticalSpacing, 2.21f);
                GraphNode node = CreateNode(nodeName, position);
                nodeLookup[nodeName] = node;
                CreateNodeText(nodeName, position + new Vector3(0, 0.2f, 0)); //Make the text slightly above the node
            }
        }

        //Making the Node connections
        Connect("A", "B");
        Connect("A", "C");
        Connect("B", "D");
        Connect("B", "E");
        Connect("C", "E");
        Connect("C", "F");


    }

    GraphNode CreateNode(string name, Vector3 position)
    {
        GameObject nodeObj = Instantiate(nodePrefab, position, Quaternion.identity);
        nodeObj.name = "Node_" + name;

        GraphNode graphNode = nodeObj.AddComponent<GraphNode>();
        graphNode.nodeName = name;
        graphNode.neighbors = new List<GraphNode>();

        return graphNode;

    }

    void CreateNodeText(string name, Vector3 position)
    {
        GameObject textObj = Instantiate(floatingText, position, Quaternion.identity);
        textObj.name = "Label_" + name;

        TMP_Text label = textObj.GetComponent<TMP_Text>();
        if (label != null)
        {
            label.text = name;
        }
        else
        {
            Debug.LogWarning("FloatingText Prefab does not contain a TMP_TExt component.");
        }
    }

    void Connect(string from, string to)
    {
        if (nodeLookup.ContainsKey(from) && nodeLookup.ContainsKey(to))
        {
            nodeLookup[from].neighbors.Add(nodeLookup[to]);
        }
        else
        {
            Debug.LogWarning($"Failed to connect {from} to {to}- one or both nodes not found.");
        }
    }


   
}
