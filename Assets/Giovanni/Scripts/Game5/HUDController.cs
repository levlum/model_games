using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subtitle1Text;
    public TextMeshProUGUI subtitle2Text;
    public TextMeshProUGUI aliveCounterText;
    public Button eraseAllButton;
    private bool simulationRunning = false;

    void Start()
    {
        // Set initial text values
        titleText.text = "The Game of Life";
        subtitle1Text.text = "Click to make cells alive";
        subtitle2Text.text = "Press - space - to run";
        UpdateAliveCounter(0);

        // Set button listener
        eraseAllButton.onClick.AddListener(EraseAllLife);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            simulationRunning = !simulationRunning;
            subtitle2Text.text = simulationRunning ? "Press - space - to stop" : "Press - space - to run";
            eraseAllButton.interactable = !simulationRunning; // Enable/disable the button based on simulation state
        }

        UpdateAliveCounter(CountAliveCells());
    }

    void UpdateAliveCounter(int count)
    {
        aliveCounterText.text = "Alive cells: " + count;
    }

    int CountAliveCells()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
        int aliveCount = 0;

        foreach (GameObject cell in cells)
        {
            Cell cellScript = cell.GetComponent<Cell>();
            if (cellScript != null && cellScript.isAlive)
            {
                aliveCount++;
            }
        }

        return aliveCount;
    }

    public void EraseAllLife()
    {
        if (!simulationRunning)
        {
            GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell");
            foreach (GameObject cell in cells)
            {
                Cell cellScript = cell.GetComponent<Cell>();
                if (cellScript != null)
                {
                    cellScript.SetAlive(false);
                }
            }
            UpdateAliveCounter(0);
        }
    }
}
