using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Button DigButton;

    public void OnClick()
    {
        DigButton.interactable = false;
    }

    public void OnRelease()
    {
        DigButton.interactable = true;
    }
}
