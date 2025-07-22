using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReader : MonoBehaviour
{
    [Header("CSV Source")]
    public TextAsset csvFile; //Drag your CSV file here in the Inspector

    public List<NodeData> parsedNodes = new List<NodeData>();

    void Start()
    {
        LoadCSV();
        Debug.Log("Parsed " + parsedNodes.Count + " nodes from CSV.");
    }

    void LoadCSV()
    {
        if (csvFile == null)
        {
            Debug.LogError("No CSV file assigned!"); return;
        }

        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++) // skip header
        {
            string line = lines[i].Trim();
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] parts = line.Split(',');

            if (parts.Length < 4)
            {
                Debug.LogWarning("Malformed line: " + line);
                continue;
            }

            NodeData node = new NodeData
            {
                name = parts[0].Trim(),
                title = parts[1].Trim(),
                reportsTo = parts[2].Trim(),
                level = int.Parse(parts[3].Trim())
            };

            parsedNodes.Add(node);
        }
    }
}
