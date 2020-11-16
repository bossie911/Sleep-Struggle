using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamFactorySelector : MonoBehaviour
{

    public Button FactoryButton;
    public Button TurretButton;

    // Start is called before the first frame update
    void Start()
    {
        Button button1 = FactoryButton.GetComponent<Button>();
        button1.onClick.AddListener(TaskOnClickFactory);

        Button button2 = TurretButton.GetComponent<Button>();
        button2.onClick.AddListener(TaskOnClickTurret);
    }
    
    public void TaskOnClickFactory()
    {
        if (FactoryButton)
        {
            Debug.Log("Factory now selected");
        }
    }

    public void TaskOnClickTurret()
    {
        if (TurretButton)
        {
            Debug.Log("Turret now selected");
        }
    }


}
