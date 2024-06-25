using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject cellPrefab; // Prefab for the cell
    public int gridSize = 10; // Size of the grid
    public float cellSpacing = 1.1f; // Spacing between cells

    void Start()
    {
        SpawnGrid();
    }

    void SpawnGrid()
    {
        // Calculate the starting position to center the grid
        Vector3 startPosition = new Vector3(-(gridSize - 1) * cellSpacing / 2, 0, -(gridSize - 1) * cellSpacing / 2);

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = startPosition + new Vector3(x * cellSpacing, 0, y * cellSpacing);
                GameObject cell = Instantiate(cellPrefab, position, Quaternion.identity);
                cell.name = $"Cell_{x}_{y}";
                cell.tag = "Cell";

                // Ensure the Cell component is attached
                if (cell.GetComponent<Cell>() == null)
                {
                    cell.AddComponent<Cell>();
                }
            }
        }
    }
}
