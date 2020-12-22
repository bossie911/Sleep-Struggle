using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamFactorySelector : MonoBehaviour
{

    public Button FactoryButton;
    public Button TurretButton;
    public Button mineButton;

    private Placeables _selectedPlaceable;

    public enum Placeables
    {
        Turret,
        Factory,
        Mine
    }

    public Placeables SelectedPlaceable
    {
        get { return _selectedPlaceable; }
        private set { _selectedPlaceable = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(FactoryButton != null){
        Button button1 = FactoryButton.GetComponent<Button>();
        button1.onClick.AddListener(TaskOnClickFactory);
        }
        
        Button button2 = TurretButton.GetComponent<Button>();
        button2.onClick.AddListener(TaskOnClickTurret);

        if (mineButton != null)
        {
            Button button3 = mineButton.GetComponent<Button>();
            button3.onClick.AddListener(TaskOnClickMine);
        }
    }

    public void TaskOnClickFactory()
    {
        if (FactoryButton)
        {
            _selectedPlaceable = Placeables.Factory;
        }
    }

    public void TaskOnClickTurret()
    {
        if (TurretButton)
        {
            _selectedPlaceable = Placeables.Turret;
        }
    }

    public void TaskOnClickMine()
    {
        if (mineButton)
        {
            _selectedPlaceable = Placeables.Mine;
        }
    }


}
