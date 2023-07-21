using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header ("Objects")]
    [SerializeField] private CellNode cellPrefab;
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private GameObject killZone;

    [Header ("Sizes")]
    [SerializeField] private int mazeSize;
    [SerializeField] private float cellSize = 2;

    private CellNode[,] maze;

    private void Start()
    {
        maze = new CellNode[mazeSize, mazeSize];

        CreateCells();
        GenerateMaze(null, maze[0,0]);

        GenerateKillZones();

        CreateObject(maze[mazeSize - 1,mazeSize - 1],goalPrefab);
    }

    private void CreateCells()
    {
        for (int x = 0; x < mazeSize; x++)
        {
            for (int z = 0; z < mazeSize; z++)
            {
                maze[x, z] = Instantiate(cellPrefab, new Vector3(x * cellSize, 0, z * cellSize), Quaternion.identity);
                maze[x, z].name = x + " " + z;
            }
        }
    }

    private void GenerateMaze(CellNode previosCell, CellNode currentCell)
    {
        currentCell.Visit();
        DisableWalls(previosCell, currentCell);

        CellNode nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }

        } while (nextCell != null);
    }

    private void DisableWalls(CellNode previosCell, CellNode currentCell)
    {
        if (previosCell == null)
        {
            return;
        }

        if (currentCell.transform.position.x < previosCell.transform.position.x)
        {
            currentCell.deactivateRightWall();
            previosCell.deactivateLeftWall();
            return;
        }

        if (currentCell.transform.position.x > previosCell.transform.position.x)
        {
            currentCell.deactivateLeftWall();
            previosCell.deactivateRightWall();
            return;
        }

        if (currentCell.transform.position.z < previosCell.transform.position.z)
        {
            currentCell.deactivateUpWall();
            previosCell.deactivateDownWall();
            return;
        }

        if (currentCell.transform.position.z > previosCell.transform.position.z)
        {
            currentCell.deactivateDownWall();
            previosCell.deactivateUpWall();
            return;
        }
    }

    private CellNode GetNextUnvisitedCell(CellNode currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(x => Random.Range(1, 10)).FirstOrDefault();
    }

    private IEnumerable<CellNode> GetUnvisitedCells(CellNode currentCell)
    {
        int x = (int)(currentCell.transform.position.x / cellSize);
        int z = (int)(currentCell.transform.position.z / cellSize);

        if (x + 1 < mazeSize)
        {
            var cellToRight = maze[x + 1, z];

            if (!cellToRight.isVisited)
            {
                yield return cellToRight;
            }
        }

        if (x - 1 >= 0)
        {
            var cellToLeft = maze[x - 1, z];

            if (!cellToLeft.isVisited)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < mazeSize)
        {
            var cellToFront = maze[x, z + 1];

            if (!cellToFront.isVisited)
            {
                yield return cellToFront;
            }
        }

        if (z - 1 >= 0)
        {
            var cellToBack = maze[x, z - 1];

            if (!cellToBack.isVisited)
            {
                yield return cellToBack;
            }
        }
    }

    private void GenerateKillZones()
    {
        for(int x = 0; x < mazeSize; x++)
        {
            for(int z = 0; z < mazeSize; z++)
            {
                if (Random.value > 0.8 && (x != 0 && z != 0) && (x != mazeSize - 1 && z != mazeSize - 1))
                    CreateObject(maze[x, z], killZone);
            }
        }
    }

    private void CreateObject(CellNode mazeNode, GameObject toCreate)
    {
        int x = (int)mazeNode.transform.position.x;
        int z = (int)mazeNode.transform.position.z;
        Instantiate(toCreate, new Vector3(x, 0, z), Quaternion.identity);
    }
}
