using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public class CellNamer : MonoBehaviour
{
    [MenuItem("Tools/Name Cells")]
    private static void NameCells()
    {
        GameObject[] cells = GameObject.FindGameObjectsWithTag("Cell"); // Make sure each cell has the tag "Cell"
        foreach (GameObject cell in cells)
        {
            Vector3 position = cell.transform.position;
            cell.name = "Cell_" + (int)position.x + "_" + (int)position.y;
        }
    }
}
#endif
