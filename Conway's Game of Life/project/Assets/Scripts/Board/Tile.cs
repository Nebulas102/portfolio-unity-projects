using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private MousePoint mousePoint;
    private Board board;

    private Renderer renderer;

    public bool isAlive;
    public int x;
    public int y;

    [HideInInspector]
    public Material orginalMaterial;

    [HideInInspector]
    public Material currentMaterial;

    private void Start()
    {
        mousePoint = MousePoint.instance;
        board = Board.instance;

        renderer = gameObject.GetComponent<Renderer>();
    }

    private void Update()
    {
        if(mousePoint.hoveredTile == gameObject)
        {
            renderer.material = board.GetComponent<Board>().hoverMaterial;
        }
        else
        {
            renderer.material = currentMaterial;
        }
    }

    public void ToggleTileState()
    {
        if(mousePoint.selectedTile == gameObject)
        {
            if(!isAlive)
            {
                isAlive = true;
                board.currentTiles[x, y] = isAlive;
                renderer.material = board.GetComponent<Board>().aliveMaterial;
            }
            else
            {
                isAlive = false;
                board.currentTiles[x, y] = isAlive;
                renderer.material = orginalMaterial;
            }

            currentMaterial = renderer.material;
        }
    }

    public void ToggleMaterial()
    {
        if(isAlive)
        {
            renderer.material = board.GetComponent<Board>().aliveMaterial;
        }
        else
        {
            renderer.material = orginalMaterial;
        }

        currentMaterial = renderer.material;      
    }

    public List<Tile> GetSurroundingTiles()
    {
        int range = 1;
        List<Tile> surroundingTiles = new List<Tile>();

        for (int dx = -range; dx <= range; dx++)
        {
            for (int dy = -range; dy <= range; dy++)
            {
                int newX = x + dx;
                int newY = y + dy;

                if (x == newX && y == newY)
                {
                    continue;
                }
               
                if (newX >= 0 && newX < board.width && newY >= 0 && newY < board.height)
                {
                    Tile tile = board.checkBoardTiles.Where(t => t.x == newX && t.y == newY).SingleOrDefault();                        

                    surroundingTiles.Add(tile);
                }
            }
        }

        return surroundingTiles;
    }

    public int CountSurroundingAliveTiles()
    {
        int countAlive = 0;
        foreach(Tile neighbourTile in gameObject.GetComponent<Tile>().GetSurroundingTiles())
        {
            if(neighbourTile.isAlive)
            {
                countAlive++;
            }
        }

        return countAlive;
    }
}
