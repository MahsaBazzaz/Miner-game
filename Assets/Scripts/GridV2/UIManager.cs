using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GridSystemV2
{
    public class UIManager : MonoBehaviour
    {
        public ChamberSpawner spawner;
        public List<Button> chamberButtons;
        void Start()
        {
            for (int i = 0; i < chamberButtons.Count; i++)
            {
                int x = i;
                chamberButtons[i].onClick.AddListener(delegate { SwitchButtonHandler(x); });
            }
        }
        void SwitchButtonHandler(int index)
        {
            chamberButtons[index].interactable = false;
            spawner.spawn(index);
        }
        public void SetButtonInteractable()
        {
            foreach (Button btn in chamberButtons)
            {
                btn.interactable = true;
            }
        }

    }
}
