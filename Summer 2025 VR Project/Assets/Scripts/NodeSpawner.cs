using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NodeSpawner : MonoBehaviour
{
    public GameObject nodePrefab;
    public GameObject floatingText;
    public TMP_Text nodeText;

    private List<GraphNode> nodes = new List<GraphNode>();


    // Start is called before the first frame update
    void Start()
    {
        GraphNode nodeA = CreateNode("A", new Vector3(0f, 2f, 2.21f));
        GraphNode nodeB = CreateNode("B", new Vector3(0f, 1.55f, 2.21f));
        GraphNode nodeC = CreateNode("C", new Vector3(0f, 1.1f, 2.21f));

        nodeA.neighbors.Add(nodeB);
        nodeB.neighbors.Add(nodeC);

        nodes.AddRange(new[] { nodeA, nodeB, nodeC });
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

    }

   
}
