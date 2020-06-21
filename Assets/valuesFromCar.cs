using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valuesFromCar : MonoBehaviour
{
    public CAR selectedCar;
    public TMPro.TextMeshProUGUI food;
    public TMPro.TextMeshProUGUI battNum;
    public TMPro.TextMeshProUGUI battCharge;

    public TMPro.TextMeshProUGUI mediNum;
    public TMPro.TextMeshProUGUI metalNum;

    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI rationText;

    public GameObject settlerUI0;
    public GameObject settlerUI1;
    public GameObject settlerUI;
    public GameObject settlerUI3;

    //also move speed, ration level, distance travel and next landmark here

    private void Awake()
    {
        //set the defaults
        //###Stats
        turnUpdate();

        //###Settlers

    }

    //updates for the buttons on the ui to call
    public void updateSpeed()
    {
        speedText.text = selectedCar.GetComponent<CAR>().getSpeed().ToString();
    }

    public void updateRation()
    {
        rationText.text = selectedCar.GetComponent<CAR>().getRationLevel().ToString();
    }

    public void turnUpdate()
    {
        food.text = selectedCar.getFoodCount().ToString();
        battNum.text = selectedCar.getBattCount().ToString();
        battCharge.text = selectedCar.getBattCharge();
        mediNum.text = selectedCar.getmediCount().ToString();
        metalNum.text = selectedCar.getMetalCount().ToString();
        speedText.text = selectedCar.GetComponent<CAR>().getSpeed().ToString();
        rationText.text = selectedCar.GetComponent<CAR>().getRationLevel().ToString();
    }
}
