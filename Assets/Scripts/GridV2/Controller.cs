using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridSystemV2
{
    public class Controller : MonoBehaviour
    {
        public GridSystem grid;
        public Button ChamberButton;
        public GameObject chamberPrefab;
        public Transform ChamberParent;
        private bool IsInsertingChamber;
        private GameObject chamberInstance;
        void Awake()
        {
            IsInsertingChamber = false;
        }
        private void Update()
        {
            if (IsInsertingChamber)
            {
                Vector3 mouseScreenPosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane + 1));
                //The +1 is there so you don't overlap the object and the camera, otherwise the object is drawn "inside" of the camera, and therefore you're not able to see it!
                chamberInstance.transform.position = mouseWorldPosition;
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    var finalPosition = grid.GetNearestPointOnGrid(mouseWorldPosition);
                    chamberInstance.transform.position = finalPosition;
                    IsInsertingChamber = false;
                    OnChamberInserted();
                    chamberInstance = null;
                }
            }
        }
        public void OnChamberClick()
        {
            ChamberButton.interactable = false;
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1);
            chamberInstance = Instantiate(chamberPrefab, position, Quaternion.identity);
            chamberInstance.transform.localScale = new Vector3(grid.tileSize, grid.tileSize, 1);
            chamberInstance.transform.parent = ChamberParent;
            IsInsertingChamber = true;
        }
        private void OnChamberInserted()
        {
            ChamberButton.interactable = true;
        }
    }
}