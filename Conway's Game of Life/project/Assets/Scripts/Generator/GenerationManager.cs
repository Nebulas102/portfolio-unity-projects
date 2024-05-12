using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public bool autoGeneration = false;
    public int currentGenerationIndex = 0;

    public List<Generation> generations;
    public Generation currentGeneration;

    public static GenerationManager instance;

    private Board board;

    private void Awake()
    {
        instance = this;
        
        generations = new List<Generation>();
    }

    private void Start()
    {
        board = Board.instance;
    }

    public void NextGeneration()
    {
        bool isAllDead = IsAllDead();
        bool isChanged = IsChanged();
        
        if(currentGenerationIndex < generations.Count - 1 && !isAllDead)
        {
            currentGenerationIndex++;

            currentGeneration = generations[currentGenerationIndex];

            if(!isChanged)
            {
                UpdateCheckBoard(currentGeneration.tiles);
            }
            else
            {
                ChangeGeneration();
            }
        }
        else
        {
            CreateGeneration();
        }
    }

    public void PreviousGeneration()
    {   
        if(currentGenerationIndex > 0)
        {
            currentGenerationIndex--;

            currentGeneration = generations[currentGenerationIndex];

            UpdateCheckBoard(currentGeneration.tiles);
        }
    }

    public void ClearGenerations()
    {
        generations.Clear();

        Generation generation = new Generation();
        generation.tiles = new bool[board.width, board.height];

        generations.Add(generation);

        UpdateCheckBoard(generation.tiles);

        currentGeneration = generation;
        currentGenerationIndex = 0;
    }

    public void AllDead()
    {
        if(currentGenerationIndex > 0)
        {
            generations.RemoveRange(currentGenerationIndex, generations.Count - currentGenerationIndex - 1);
        }

        foreach(Tile tile in board.checkBoardTiles)
        {
            tile.isAlive = false;
            board.currentTiles[tile.x, tile.y] = false;
            tile.ToggleMaterial();
        }
    }

    public void UpdateCheckBoard(bool[,] _tiles)
    {
        foreach(Tile tile in board.checkBoardTiles)
        {
            tile.isAlive = _tiles[tile.x, tile.y];
            tile.ToggleMaterial();
        }

        board.currentTiles = _tiles;
    }

    public IEnumerator ToggleAutoGeneration()
    {
        autoGeneration = (autoGeneration) ? false : true;

        while(autoGeneration)
        {
            NextGeneration();

            yield return new WaitForSeconds(1f);
        }
    }

    private void ChangeGeneration()
    {
        bool[,] tiles = ApplyConwayRules();

        UpdateCheckBoard(tiles);

        generations[currentGenerationIndex - 1].tiles = tiles;
    }

    private bool IsAllDead()
    {
        bool isAllDead = true;

        for(int i = 0; i < board.width; i++)
        {
            for(int j = 0; j < board.height; j++)
            {
                if(board.currentTiles[i, j])
                {
                    isAllDead = false;
                }
            }
        }

        return isAllDead;
    }

    private bool IsChanged()
    {
        bool isChanged = true;

        for(int i = 0; i < board.width; i++)
        {
            for(int j = 0; j < board.height; j++)
            {
                if(board.currentTiles[i, j] != currentGeneration.tiles[i, j])
                {
                    isChanged = false;
                }
            }
        }

        return isChanged;
    }

    private void CreateGeneration()
    {
        Generation generation = new Generation();
        bool[,] tiles = ApplyConwayRules();

        UpdateCheckBoard(tiles);

        generation.tiles = tiles;
        board.currentTiles = tiles;

        generations.Add(generation);
        
        currentGenerationIndex++;
    }

    private bool[,] ApplyConwayRules()
    {
        bool[,] newTiles = new bool[board.width, board.height];
        
        Array.Copy(board.currentTiles, newTiles, board.currentTiles.Length);

        for(int i = 0; i < board.width; i++)
        {                
            for(int j = 0; j < board.height; j++)
            {
                Tile tile = board.checkBoardTiles.Where(t => t.x == i && t.y == j).FirstOrDefault();
                int countAlive = tile.CountSurroundingAliveTiles();

                if(tile.isAlive)
                {
                    if(countAlive < 2 || countAlive > 3)
                    {
                        newTiles[i, j] = false;
                    }
                }
                else
                {
                    if(countAlive == 3)
                    {
                        newTiles[i, j] = true;
                    } 
                }
            }
        }

        return newTiles;
    }
}
