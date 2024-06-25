using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool isAlive;
    private Renderer renderer;

    void Start()
    {
        renderer = GetComponent<Renderer>();
        UpdateColor();
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
        UpdateColor();
    }

    public void SetColor(Color color)
    {
        if (renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }
        renderer.material.color = color;
    }

    public void UpdateColor()
    {
        if (renderer == null)
        {
            renderer = GetComponent<Renderer>();
        }
        renderer.material.color = isAlive ? Color.white : Color.black;
    }
}
