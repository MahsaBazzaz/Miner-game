using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minor : MonoBehaviour
{
    public MapGrid grid;
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
                Cell c = grid.GetCell((int)hit.transform.position.x, (int)hit.transform.position.y);
                if (c != null)
                {
                    Debug.Log(c.ObjectType);
                }
            }
        }
    }
}
