using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamFactorySelector : MonoBehaviour
{

    public Button FactoryButton;
    public Button TurretButton;
    public Button mineButton;

    public int PlaceableType; 

    // Start is called before the first frame update
    void Start()
    {
        Button button1 = FactoryButton.GetComponent<Button>();
        button1.onClick.AddListener(TaskOnClickFactory);

        Button button2 = TurretButton.GetComponent<Button>();
        button2.onClick.AddListener(TaskOnClickTurret);

        Button button3 = mineButton.GetComponent<Button>();
        button3.onClick.AddListener(TaskOnClickMine);
    }
    
    public void TaskOnClickFactory()
    {
        if (FactoryButton)
        {
            PlaceableType = 0;
        }
    }

    public void TaskOnClickTurret()
    {
        if (TurretButton)
        {
            PlaceableType = 1;
        }
    }

    public void TaskOnClickMine() 
    {
        if (mineButton) {
            PlaceableType = 2;
        }
    }


}
