using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Maze : MonoBehaviour
{
    [Header("Maze configuration variables")]
    [SerializeField] private int _width = 10;
    [SerializeField] private int _height = 10;
    private float _cellSize = 1f;

    [Header("Prefabs variables")]
    [SerializeField] private GameObject _cellPrefab;

    [Header("NavMesh")]
    [SerializeField] private NavMeshSurface _navMeshSurface;

    private MazeCell[,] grid;
    private bool _generated = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (_generated) return;

        _generated = true;
        StartDungeon();
    }

    private void StartDungeon()
    {
        InitializeGrid();
        GenerateMaze(0, 0);
        OpenEntryAndExit();
        StartCoroutine(BakeNavMeshDelayed());
    }

    private IEnumerator BakeNavMeshDelayed()
    {
        yield return new WaitForSeconds(0.2f); // da tiempo a que todo esté instanciado
        if (_navMeshSurface != null)
        {
            _navMeshSurface.BuildNavMesh();
            Debug.Log("NavMesh baked.");
        }
    }

    private void OpenEntryAndExit()
    {
        int centerX = _width / 2;
        grid[centerX, 0].RemoveWall("Wall_S");
        grid[centerX, _height - 1].RemoveWall("Wall_N");
    }

    private void GenerateMaze(int x, int y)
    {
        grid[x, y].isVisited = true;

        List<(int destinationX, int destinationY, string wallA, string wallB)> directions =
            new List<(int, int, string, string)>
        {
            (0,  1, "Wall_N", "Wall_S"),
            (0, -1, "Wall_S", "Wall_N"),
            (1,  0, "Wall_E", "Wall_W"),
            (-1, 0, "Wall_W", "Wall_E")
        };

        for (int i = 0; i < directions.Count; i++)
        {
            var temp = directions[i];
            int randomRange = Random.Range(i, directions.Count);
            directions[i] = directions[randomRange];
            directions[randomRange] = temp;
        }

        foreach (var direction in directions)
        {
            int newX = x + direction.destinationX;
            int newY = y + direction.destinationY;

            if (newX >= 0 && newY >= 0 && newX < _width && newY < _height && !grid[newX, newY].isVisited)
            {
                grid[x, y].RemoveWall(direction.wallA);
                grid[newX, newY].RemoveWall(direction.wallB);
                GenerateMaze(newX, newY);
            }
        }
    }

    private void InitializeGrid()
    {
        grid = new MazeCell[_width, _height];

        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                Vector3 position = new Vector3(x * _cellSize, 0, y * _cellSize);
                GameObject newCell = Instantiate(_cellPrefab, position, Quaternion.identity, transform);
                grid[x, y] = new MazeCell(x, y, newCell);
            }
        }
    }
}