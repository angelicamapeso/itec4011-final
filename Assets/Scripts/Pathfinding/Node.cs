using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Code Adapted from:
 * [Daniel]. (2018, Jan 10).
 * Unity - A Star Pathfinding Tutorial [Video].
 * YouTube. https://www.youtube.com/watch?v=AKKpPmxx07w
 */
public class Node
{
    public int id;

    public int gridX;
    public int gridY;

    public bool isWall;
    public Vector2 position;

    public Node parent;

    public int gCost;
    public int hCost;
    public int FCost { get { return gCost + hCost;  } }

    public Node(int _id, bool _isWall, Vector2 _position, int _gridX, int _gridY)
    {
        id = _id;
        isWall = _isWall;
        position = _position;
        gridX = _gridX;
        gridY = _gridY;
    }

    public Node clone()
    {
        Node clone = new Node(this.id, this.isWall, this.position, this.gridX, this.gridY)
            {
                id = this.id,
                parent = this.parent,
                gCost = this.gCost,
                hCost = this.hCost
            };

        return clone;
    }
}
