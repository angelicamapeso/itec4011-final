using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Code Adapted from:
 * [Daniel]. (2018, Jan 10).
 * Unity - A Star Pathfinding Tutorial [Video].
 * YouTube. https://www.youtube.com/watch?v=AKKpPmxx07w
 */
public class AStarGrid : MonoBehaviour
{
    public static AStarGrid current;

    public Transform startPosition;
    public LayerMask wallMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    public Node[,] grid;
    public List<Node> finalPath;

    float nodeDiameter;
    int gridSizeX, gridSizeY;
    Vector2 bottomLeft;

    private void Awake()
    {
        if (current == null)
        {
            current = this;
        }
    }

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;
        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        bottomLeft = new Vector2(transform.position.x, transform.position.y) - Vector2.right * gridWorldSize.x / 2 - Vector2.up * gridWorldSize.y / 2;
        CreateGrid();
    }

    void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        int idCounter = 0;
        
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector2 worldPoint = GetWorldPoint(x, y);
                bool isWall = false;

                if (Physics2D.OverlapCircle(worldPoint, nodeRadius, wallMask))
                {
                    isWall = true;
                }

                grid[x, y] = new Node(idCounter, isWall, worldPoint, x, y);
                idCounter += 1;
            }
        }
    }

    Vector2 GetWorldPoint(int x, int y)
    {
        return bottomLeft + Vector2.right * (x * nodeDiameter + nodeRadius) + Vector2.up * (y * nodeDiameter + nodeRadius);
    }

    int[] GetIndex(Vector2 worldPoint)
    {
        int x = Mathf.RoundToInt((worldPoint.x - bottomLeft.x - nodeRadius) / nodeDiameter);
        int y = Mathf.RoundToInt((worldPoint.y - bottomLeft.y - nodeRadius) / nodeDiameter);
        
        return new int[] { x, y };
    }

    public Node NodeFromWorldPosition(Vector2 worldPosition)
    {
        int[] gridIndexes = GetIndex(worldPosition);

        // Requesting an off-grid point!
        if (gridIndexes[0] >= gridSizeX
            || gridIndexes[0] < 0
            || gridIndexes[1] >= gridSizeY
            || gridIndexes[1] < 0)
        {
            return null;
        }

        return grid[gridIndexes[0], gridIndexes[1]].clone();
    }

    public List<Node> GetNeighboringNodes(Node current)
    {
        List<Node> neighbors = new List<Node>();

        // Right Side
        int rightX = current.gridX + 1;
        if (rightX >= 0 && rightX < gridSizeX)
        {
            neighbors.Add(grid[rightX, current.gridY].clone());
        }

        // Left Side
        int leftX = current.gridX - 1;
        if (leftX >= 0 && leftX < gridSizeX)
        {
            neighbors.Add(grid[leftX, current.gridY].clone());
        }

        // Up
        int upY = current.gridY + 1;
        if (upY >= 0 && upY < gridSizeY)
        {
            neighbors.Add(grid[current.gridX, upY].clone());
        }

        // Down
        int downY = current.gridY - 1;
        if (downY >= 0 && downY < gridSizeY)
        {
            neighbors.Add(grid[current.gridX, downY].clone());
        }

        return neighbors;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));

        if (grid != null)
        {
            foreach(Node node in grid)
            {
                if (node.isWall)
                {
                    Gizmos.color = Color.yellow;
                } else
                {
                    Gizmos.color = Color.green;
                }

                if (finalPath != null && finalPath.Contains(node))
                {
                    Gizmos.color = Color.red;
                }

                Gizmos.DrawWireCube(new Vector3(node.position.x, node.position.y, 0), Vector3.one * (nodeDiameter - distance));
            }
        }
    }
}
