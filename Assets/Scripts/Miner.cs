using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Miner : MonoBehaviour
{
    public MapGrid grid;
    public MapVisualizer mapVisualizer;
    public UIManager uIManager;
    public Camera cam;
    private Vector2 boxStartPos = Vector2.zero;
    private Vector2 boxEndPos = Vector2.zero;
    public Texture selectTexture;
    public GameObject chamberPrefab;
    private Boolean IsDigging;
    private Boolean IsInsertingChamber;
    private GameObject chamberInstance;
    void Awake()
    {
        IsDigging = false;
        IsInsertingChamber = false;
    }
    void Update()
    {
        if (IsDigging)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                // Called on the first update where the user has pressed the mouse button.
                if (Input.GetKeyDown(KeyCode.Mouse0))
                    boxStartPos = Input.mousePosition;
                else  // Else we must be in "drag" mode.
                    boxEndPos = Input.mousePosition;
            }
            else
            {
                // Handle the case where the player had been drawing a box but has now released.
                if (boxEndPos != Vector2.zero && boxStartPos != Vector2.zero)
                    HandleUnitSelection();
                // Reset box positions.
                boxEndPos = boxStartPos = Vector2.zero;
            }
        }
        if (IsInsertingChamber)
        {
            Vector3 mouseScreenPosition = Input.mousePosition;
            Vector3 mouseWorldPosition = cam.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, cam.nearClipPlane + 1));
            //The +1 is there so you don't overlap the object and the camera, otherwise the object is drawn "inside" of the camera, and therefore you're not able to see it!
            chamberInstance.transform.position = mouseWorldPosition;
        }
        if (Input.GetKey(KeyCode.Mouse0))
        {
            IsInsertingChamber = false;
            uIManager.OnChamberInserted();
            chamberInstance = null;
        }
    }

    public void Dig()
    {
        IsDigging = true;
    }
    public void InsertChamber()
    {
        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1);
        chamberInstance = Instantiate(chamberPrefab, position, Quaternion.identity);
        IsInsertingChamber = true;
    }
    private void HandleUnitSelection()
    {

        int startX = -1;
        int startY = -1;
        int endX = -1;
        int endY = -1;

        Ray ray = cam.ScreenPointToRay(boxStartPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            int x = (int)hit.transform.position.x;
            int y = (int)hit.transform.position.y;
            Cell c = grid.GetCell(x, y);
            if (c != null)
            {
                startX = x;
                startY = y;
            }
        }

        ray = cam.ScreenPointToRay(boxEndPos);
        if (Physics.Raycast(ray, out hit))

        {
            int x = (int)hit.transform.position.x;
            int y = (int)hit.transform.position.y;
            Cell c = grid.GetCell(x, y);
            if (c != null)
            {
                endX = x;
                endY = y;
            }
        }
        int minX = -1;
        int maxX = -1;
        int minY = -1;
        int maxY = -1;

        if (startX > endX) { minX = endX; maxX = startX; } else { minX = startX; maxX = endX; }
        if (startY > endY) { minY = endY; maxY = startY; } else { minY = startY; maxY = endY; }

        for (int i = minX; i <= maxX; i++)
        {
            for (int j = minY; j <= maxY; j++)
            {
                // grid.SetCellTaken(i, j);
                mapVisualizer.SetTileTaken(new Vector3(i, j, -5));
            }
        }

        uIManager.OnDigFinished();
        IsDigging = false;
    }

    void OnGUI()
    {
        // If we are in the middle of a selection draw the texture.
        if (boxStartPos != Vector2.zero && boxEndPos != Vector2.zero)
        {
            // Create a rectangle object out of the start and end position while transforming it
            // to the screen's cordinates.
            var rect = new Rect(boxStartPos.x, Screen.height - boxStartPos.y,
                                boxEndPos.x - boxStartPos.x,
                                -1 * (boxEndPos.y - boxStartPos.y));
            // Draw the texture.
            GUI.DrawTexture(rect, selectTexture);
        }
    }
}
