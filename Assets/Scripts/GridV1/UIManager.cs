using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridSystemV1
{
    public class UIManager : MonoBehaviour
    {
        public Button DigButton;
        public Button ChamberButton;

        public void OnDigClick()
        {
            DigButton.interactable = false;
        }
        public void OnChamberClick()
        {
            ChamberButton.interactable = false;
        }
        public void OnDigFinished()
        {
            DigButton.interactable = true;
        }
        public void OnChamberInserted()
        {
            ChamberButton.interactable = true;
        }
    }
}