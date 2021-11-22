using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minor : MonoBehaviour
{
    public MapGrid grid;
    public MapVisualizer mapVisualizer;
    private Camera cam;
    void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                int x = (int)hit.transform.position.x;
                int y = (int)hit.transform.position.y;
                Cell c = grid.GetCell(x, y);
                if (c != null)
                {
                    // if (c.IsTaken)
                    // {
                    //     Debug.Log(c.IsTaken);
                    // }
                    // else
                    // {
                        grid.SetCellTaken(x, y);
                        mapVisualizer.SetTileTaken(Input.mousePosition);
                    // }
                }
            }
        }
    }
}
