using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour, IInteractable
{
    public string nodeName;
    public List<GraphNode> neighbors;

    public void Interact()
    {
        Debug.Log($"Node {nodeName} was clicked!");

        // TODO: Add whatever logic you want (e.g. highlight, info popup, etc.)
        // Example: change color
        GetComponent<Renderer>().material.color = Color.yellow;
    }
}

