using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Code Adapted from:
 * [Daniel]. (2018, Jan 10).
 * Unity - A Star Pathfinding Tutorial [Video].
 * YouTube. https://www.youtube.com/watch?v=AKKpPmxx07w
 */
public class Pathfinding : MonoBehaviour
{
    protected static AStarGrid grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = GetComponent<AStarGrid>();
    }

    public static List<Node> FindPath(Vector2 startPos, Vector2 endPos)
    {
        if (grid != null)
        {
            Node startNode = grid.NodeFromWorldPosition(startPos);
            Node targetNode = grid.NodeFromWorldPosition(endPos);

            if (startNode != null && targetNode != null)
            {
                // For A* Calculations
                List<Node> openList = new List<Node>();
                List<Node> closedList = new List<Node>();

                // To Store new objects created from the grid
                List<Node> consideredNodes = new List<Node>();
                consideredNodes.Add(startNode);
                consideredNodes.Add(targetNode);

                // To store the final path
                List<Node> finalPath = new List<Node>();

                // Start A* Calculations
                openList.Add(startNode);

                while (openList.Count > 0)
                {
                    Node currentNode = openList[0];

                    // Get Node in the Closer Direction to target
                    for (int i = 1; i < openList.Count; i++)
                    {
                        if (openList[i].FCost <= currentNode.FCost && openList[i].hCost < currentNode.hCost)
                        {
                            currentNode = openList[i];
                        }
                    }

                    // Finish processing this node
                    openList.Remove(currentNode);

                    if (!NodeIsInList(closedList, currentNode))
                    {
                        closedList.Add(currentNode);
                    }

                    // If Reached the end, get the final path
                    if (currentNode == targetNode)
                    {
                        finalPath = GetFinalPath(startNode, targetNode);
                        break;
                    }

                    List<Node> neighborNodes = grid.GetNeighboringNodes(currentNode);
                    for (int neighborIndex = 0; neighborIndex < neighborNodes.Count; neighborIndex++)
                    {
                        // If this node was previously considered, get the reference for it or add it
                        if (NodeIsInList(consideredNodes, neighborNodes[neighborIndex]))
                        {
                            neighborNodes[neighborIndex] = GetNodeFromList(consideredNodes, neighborNodes[neighborIndex].id);
                        }
                        else
                        {
                            consideredNodes.Add(neighborNodes[neighborIndex]);
                        }
                    }

                    // Get neighbor references
                    foreach (Node neighborNode in neighborNodes)
                    {
                        if (neighborNode.isWall || NodeIsInList(closedList, neighborNode))
                        {
                            continue;
                        }

                        int moveCost = currentNode.gCost + GetManhattenDistance(currentNode, neighborNode);

                        if (moveCost < neighborNode.gCost || !NodeIsInList(openList, neighborNode))
                        {
                            neighborNode.gCost = moveCost;
                            neighborNode.hCost = GetManhattenDistance(neighborNode, targetNode);
                            neighborNode.parent = currentNode;

                            if (!NodeIsInList(openList, neighborNode))
                            {
                                openList.Add(neighborNode);
                            }
                        }
                    }
                }
                return finalPath;
            }
        }

        return null;
    }

    public static List<Node> GetFinalPath(Node startNode, Node endNode)
    {
        List<Node> finalPath = new List<Node>();
        Node currentNode = endNode;

        while (currentNode.id != startNode.id)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }

        finalPath.Reverse();

        //// Debuging Purposes
        //Debug.Log("==== FINAL PATH CALCULATED ====");
        //foreach (Node finalNode in finalPath)
        //{
        //    Debug.Log("ID: [" + finalNode.id + "] at (" + finalNode.gridX + "," + finalNode.gridY + ")");
        //}

        return finalPath;
    }

    public static int GetManhattenDistance(Node nodeA, Node nodeB)
    {
        int ix = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int iy = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        return ix + iy;
    }

    public static bool NodeIsInList(List<Node> listToSearch, Node nodeToSearchFor)
    {
        for (int i = 0; i < listToSearch.Count; i++)
        {
            if (listToSearch[i].id == nodeToSearchFor.id)
            {
                return true;
            }
        }

        return false;
    }

    public static Node GetNodeFromList(List<Node> nodeList, int nodeId)
    {
        foreach(Node node in nodeList)
        {
            if (node.id == nodeId)
            {
                return node;
            }
        }

        return null;
    }
}
