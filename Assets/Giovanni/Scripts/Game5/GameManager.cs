using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gridSize = 10; // Set the size of the grid
    private Cell[,] grid;
    private bool simulationRunning = false;
    public float updateInterval = 1.0f; // Interval in seconds between each update step
    public Button eraseAllLifeButton; // Reference to the "Erase all life" button
    public Button startStopButton; // Reference to the Start/Stop button
    public TextMeshProUGUI startStopButtonText; // Reference to the Start/Stop button text
    public Button randomLifeButton; // Reference to the "Random Life" button

    private int lastLiveCellCount;
    private int stableCycleCount;
    private Coroutine simulationCoroutine;
    private bool isDragging = false;
    private HashSet<Cell> toggledCells = new HashSet<Cell>(); // Track toggled cells during drag
    public float dragCooldown = 0.1f; // Cooldown period for drag-to-draw
    private float lastDragTime;

    void Start()
    {
        grid = new Cell[gridSize, gridSize];
        InitializeGrid();
        UpdateButtonStates();
        UpdateStartStopButtonText();
    }

    void Update()
    {
        if (!simulationRunning && Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            HandleCellClick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            toggledCells.Clear(); // Clear toggled cells after drag
        }

        if (isDragging && !simulationRunning)
        {
            if (Time.time >= lastDragTime + dragCooldown)
            {
                HandleCellClick();
                lastDragTime = Time.time;
            }
        }
    }

    void InitializeGrid()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                string cellName = "Cell_" + x + "_" + y;
                GameObject cellObject = GameObject.Find(cellName);
                if (cellObject != null)
                {
                    Cell cell = cellObject.GetComponent<Cell>();
                    if (cell != null)
                    {
                        grid[x, y] = cell;
                    }
                    else
                    {
                        Debug.LogError("Cell script not found on: " + cellName);
                    }
                }
                else
                {
                    Debug.LogError("Cell not found: " + cellName);
                }
            }
        }
    }

    IEnumerator SimulationCoroutine()
    {
        while (simulationRunning)
        {
            UpdateSimulation();
            yield return new WaitForSeconds(updateInterval);
        }
    }

    void UpdateSimulation()
    {
        bool[,] newStates = new bool[gridSize, gridSize];
        int currentLiveCellCount = 0;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                int aliveNeighbors = CountAliveNeighbors(x, y);
                bool currentState = grid[x, y].isAlive;
                bool newState = currentState;

                // Rule 1 and Rule 3: Underpopulation and Overpopulation
                if (currentState && (aliveNeighbors < 2 || aliveNeighbors > 3))
                {
                    newState = false;
                }
                // Rule 2: Survival
                else if (currentState && (aliveNeighbors == 2 || aliveNeighbors == 3))
                {
                    newState = true;
                }
                // Rule 4: Reproduction
                else if (!currentState && aliveNeighbors == 3)
                {
                    newState = true;
                }

                newStates[x, y] = newState;
                if (newState)
                {
                    currentLiveCellCount++;
                }
            }
        }

        // Apply the new states to the grid
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                grid[x, y].SetAlive(newStates[x, y]);
            }
        }

        // Check for known patterns and update colors
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                if (grid[x, y].isAlive)
                {
                    string pattern = PatternRecognition.DetectPattern(grid, x, y);
                    if (pattern != null)
                    {
                        Color patternColor = GetPatternColor(pattern);
                        ApplyPatternColor(x, y, pattern, patternColor);
                    }
                    else
                    {
                        grid[x, y].UpdateColor(); // Reset to default color if no pattern
                    }
                }
            }
        }

        // Check if the live cell count has been stable for 10 cycles
        if (currentLiveCellCount == lastLiveCellCount)
        {
            stableCycleCount++;
            if (stableCycleCount >= 10)
            {
                Debug.Log("Simulation stopped due to stability.");
                StopSimulation();
            }
        }
        else
        {
            stableCycleCount = 0;
        }

        lastLiveCellCount = currentLiveCellCount;
    }

    int CountAliveNeighbors(int x, int y)
    {
        int count = 0;

        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;

                int nx = x + dx;
                int ny = y + dy;

                if (nx >= 0 && nx < gridSize && ny >= 0 && ny < gridSize)
                {
                    if (grid[nx, ny].isAlive)
                    {
                        count++;
                    }
                }
            }
        }

        return count;
    }

    public void ToggleSimulation()
    {
        simulationRunning = !simulationRunning;
        Debug.Log("Simulation Running: " + simulationRunning);

        if (simulationRunning)
        {
            lastLiveCellCount = -1; // Reset the live cell count tracking
            stableCycleCount = 0;
            simulationCoroutine = StartCoroutine(SimulationCoroutine());
        }
        else
        {
            StopSimulation();
        }

        UpdateStartStopButtonText();
        UpdateButtonStates();
    }

    void StopSimulation()
    {
        simulationRunning = false;
        if (simulationCoroutine != null)
        {
            StopCoroutine(simulationCoroutine); // Stop the coroutine to ensure the simulation halts correctly
        }
        UpdateStartStopButtonText();
        UpdateButtonStates();
    }

    void UpdateStartStopButtonText()
    {
        if (startStopButtonText != null)
        {
            startStopButtonText.text = simulationRunning ? "Stop" : "Start";
        }
    }

    void UpdateButtonStates()
    {
        bool buttonsInteractable = !simulationRunning;

        if (eraseAllLifeButton != null)
        {
            eraseAllLifeButton.interactable = buttonsInteractable;
        }

        if (randomLifeButton != null)
        {
            randomLifeButton.interactable = buttonsInteractable;
        }
    }

    public void EraseAllLife()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                grid[x, y].SetAlive(false);
            }
        }
    }

    public void RandomizeLife()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                grid[x, y].SetAlive(Random.value > 0.5f);
            }
        }
    }

    Color GetPatternColor(string pattern)
    {
        switch (pattern)
        {
            case "Block":
            case "Tub":
                return Color.green; // Still life
            case "Blinker":
            case "Toad":
                return Color.red; // Oscillator
            default:
                return Color.white; // Default color
        }
    }

    void ApplyPatternColor(int x, int y, string pattern, Color color)
    {
        foreach (var offset in PatternRecognition.patterns[pattern])
        {
            int newX = x + offset.x;
            int newY = y + offset.y;
            if (newX >= 0 && newY >= 0 && newX < gridSize && newY < gridSize)
            {
                grid[newX, newY].SetColor(color);
            }
        }
    }

    void HandleCellClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Cell cell = hit.collider.GetComponent<Cell>();
            if (cell != null && !toggledCells.Contains(cell))
            {
                cell.SetAlive(!cell.isAlive);
                toggledCells.Add(cell); // Add to toggled cells to prevent fast toggling
            }
        }
    }
}
