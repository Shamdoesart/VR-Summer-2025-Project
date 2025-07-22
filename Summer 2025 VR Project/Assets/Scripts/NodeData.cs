using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData 
{
   public string name;
        public string title;
        public string reportsTo;
        public int level;
        public GraphNode nodeRef; //Will be set later when the node is created
}
