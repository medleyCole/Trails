using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class valuesFromCar : MonoBehaviour
{
    public CAR selectedCar;
    public TMPro.TextMeshProUGUI food;
    public TMPro.TextMeshProUGUI battNum;

    public TMPro.TextMeshProUGUI mediNum;
    public TMPro.TextMeshProUGUI metalNum;

    public TMPro.TextMeshProUGUI speedText;
    public TMPro.TextMeshProUGUI rationText;

    public GameObject settlerUI0;
    public GameObject settlerUI1;
    public GameObject settlerUI2;
    public GameObject settlerUI3;

    //also move speed, ration level, distance travel and next landmark here

    private void Awake()
    {
        turnUpdate();
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

    //call this every turn but also anytime we need to update the rightside fo the screen
    public void turnUpdate()
    {
        food.text = selectedCar.getFoodCount().ToString();
        battNum.text = selectedCar.getBattCharge().ToString() + " / " + selectedCar.getBattCapacity().ToString();
        mediNum.text = selectedCar.getMediCount().ToString();
        metalNum.text = selectedCar.getMetalCount().ToString();
        speedText.text = selectedCar.GetComponent<CAR>().getSpeed().ToString();
        rationText.text = selectedCar.GetComponent<CAR>().getRationLevel().ToString();

        //right now we're assumting that there are 4 settlers and they won't die.
        //having settlers die is gonna be it's own ish
        settlerUI0.GetComponent<settlerQuadInfo>().updateSettler();
        settlerUI1.GetComponent<settlerQuadInfo>().updateSettler();
        settlerUI2.GetComponent<settlerQuadInfo>().updateSettler();
        settlerUI3.GetComponent<settlerQuadInfo>().updateSettler();


    }
}
