using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellPlacer : MonoBehaviour
{
    public GameObject cellPrefab;
    private Camera mainCamera;
    private GameManager gameManager;

    void Start()
    {
        mainCamera = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PlaceCell();
        }
    }

    void PlaceCell()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell != null)
            {
                cell.SetAlive(!cell.isAlive);
            }
        }
    }
}
