using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamFactorySelector : MonoBehaviour
{
    public Button FactoryButton;
    public Button TurretButton;
    public Button mineButton;
    public Button totemButton;
    public Button candleButton;

    private Placeables _selectedPlaceable;

    public enum Placeables
    {
        Turret,
        Factory,
        Mine,
        Totem,
        Candle
    }

    public Placeables SelectedPlaceable
    {
        get { return _selectedPlaceable; }
        private set { _selectedPlaceable = value; }
    }

    void Start()
    {
        if(FactoryButton != null){
            Button button1 = FactoryButton.GetComponent<Button>();
            button1.onClick.AddListener(TaskOnClickFactory);
        }

        if (TurretButton != null)
        {
            Button button2 = TurretButton.GetComponent<Button>();
            button2.onClick.AddListener(TaskOnClickTurret);
        }

        if (mineButton != null)
        {
            Button button3 = mineButton.GetComponent<Button>();
            button3.onClick.AddListener(TaskOnClickMine);
        }

        if (totemButton != null)
        {
            Button button4 = totemButton.GetComponent<Button>();
            button4.onClick.AddListener(TaskOnClickTotem);
        }

        if (candleButton != null)
        {
            Button button5 = candleButton.GetComponent<Button>();
            button5.onClick.AddListener(TaskOnClickCandle);
        }
    }

    public void TaskOnClickFactory()
    {
        if (FactoryButton)
            _selectedPlaceable = Placeables.Factory;
    }

    public void TaskOnClickTurret()
    {
        if (TurretButton)
            _selectedPlaceable = Placeables.Turret;
    }

    public void TaskOnClickMine()
    {
        if (mineButton)
            _selectedPlaceable = Placeables.Mine;
    }

    public void TaskOnClickTotem()
    {
        if (totemButton)
            _selectedPlaceable = Placeables.Totem;
    }

    public void TaskOnClickCandle()
    {
        if (candleButton)
            _selectedPlaceable = Placeables.Candle;
    }

}
