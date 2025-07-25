using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodeInfoUI : MonoBehaviour
{
    public static NodeInfoUI Instance;

    public GameObject panel;
    public TMP_Text nameText;
    public TMP_Text titleText;
    public TMP_Text managerText;
    public Button closeButton;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
        closeButton.onClick.AddListener(() => panel.SetActive(false));
    }

    public void Show(NodeData data)
    {
        nameText.text = "Name: " + data.name;
        titleText.text = "Title: " + data.title;
        managerText.text = "Reports To: " + (string.IsNullOrEmpty(data.reportsTo) ? "None" : data.reportsTo);

        panel.SetActive(true);
    }
}
