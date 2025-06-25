using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NodeLogic : MonoBehaviour
{

    class Node
    {
        public string name;
        public List<Edge> neighbors = new List<Edge>();
    }

    class Edge
    {
        public Node target;
        public float weight;
    }


    // Start is called before the first frame update
    void Start()
    {
        //create nodes
        Node A = new Node { name = "A" };
        Node B = new Node { name = "B" };
        Node C = new Node { name = "C" };


        //Add connections
        A.neighbors.Add(new Edge { target = A, weight = 1f });
        B.neighbors.Add(new Edge { target = C, weight = 1f });

        foreach (Edge edge in A.neighbors)
        {
            Debug.Log($"{A.name} connects to {edge.target.name} with weight {edge.weight}");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
