using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GraphNodeInfoDisplay : MonoBehaviour, IPointerClickHandler
{
    public NodeData myData;

    public void OnPointerClick(PointerEventData eventData)
    {
        NodeInfoUI.Instance.Show(myData);
    }
}
