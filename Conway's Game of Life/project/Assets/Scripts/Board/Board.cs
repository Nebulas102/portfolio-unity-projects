using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Source of rules: https://playgameoflife.com/
public class Board : MonoBehaviour
{
    [Header("Board")]
    public int width;
    public int height;
    public int offset;

    [Header("Tile")]
    [SerializeField]
    private GameObject tile;
    [SerializeField]
    private Material lightMaterial;
    [SerializeField]
    private Material darkMaterial;
    
    public Material hoverMaterial;
    public Material aliveMaterial;
    
    public List<Tile> checkBoardTiles;
    public bool[,] currentTiles;

    public static Board instance;

    private GameObject checkBoard;
    private GenerationManager generationManager;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        generationManager = GenerationManager.instance;
        currentTiles = new bool[width, height];

        checkBoard = new GameObject();
        checkBoard.name = "CheckBoard";

        CreateBoard();

        Generation generation = new Generation();

        generation.tiles = currentTiles;
        generationManager.generations.Add(generation);

        generationManager.currentGeneration = generation;
    }

    private void CreateBoard()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Material currentMaterial = null;

                if((i + j) % 2 == 0)
                {
                    currentMaterial = lightMaterial;
                }
                else
                {
                    currentMaterial = darkMaterial;
                }

                PlaceTile(i, j, currentMaterial);
            }
        }
    }

    private void PlaceTile(int _width, int _height, Material _material)
    {
        Vector3 pos = new Vector3(_width * offset, 0, _height * offset);

        GameObject newTile = Instantiate(tile, pos, Quaternion.identity);

        newTile.transform.SetParent(checkBoard.transform);

        newTile.GetComponent<Renderer>().material = _material;
        newTile.GetComponent<Tile>().orginalMaterial = _material;
        newTile.GetComponent<Tile>().currentMaterial = _material;

        newTile.GetComponent<Tile>().x = _width;
        newTile.GetComponent<Tile>().y = _height;

        checkBoardTiles.Add(newTile.GetComponent<Tile>());
        currentTiles[_width, _height] = false;
    }
}
