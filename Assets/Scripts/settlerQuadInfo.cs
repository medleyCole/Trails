using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settlerQuadInfo : MonoBehaviour
{
    //reference to the car so we can read out of it
    public CAR selectedCar;

    //number of the quads we have in the UI (right now it's set to 4)
    public int quadNumber;

    //all the ui text elements in the quad
    public TMPro.TextMeshProUGUI settlerName;
    public TMPro.TextMeshProUGUI settlerProfession;
    public TMPro.TextMeshProUGUI settlerTrait1;
    public TMPro.TextMeshProUGUI settlerTrait2;

    //we only need to activate or deactivate these things so we'll treat them like game objects
    public GameObject settlerIsSick;
    public GameObject settlerIsInjured;

    public void Awake()
    {
        updateSettler();
        //injured/sick always starts as false
        
    }

    //accessed via the valuesFromCar UI manager,
    //it just looks into the car and updates appropriately
    public void updateSettler()
    {
        settlerName.text = selectedCar.getSettlerFromList(quadNumber).getName();
        settlerProfession.text = selectedCar.getSettlerFromList(quadNumber).getProfession();
        settlerTrait1.text = selectedCar.getSettlerFromList(quadNumber).getTrait1();
        settlerTrait2.text = selectedCar.getSettlerFromList(quadNumber).getTrait2();

        //setting the injured and sick indicators based on settler state
        if(selectedCar.getSettlerFromList(quadNumber).getIsSick())
        {
            settlerIsSick.SetActive(true);
        }
        else
        {
            settlerIsSick.SetActive(false);
        }

        if (selectedCar.getSettlerFromList(quadNumber).getIsInjured())
        {
            settlerIsInjured.SetActive(true);
        }
        else
        {
            settlerIsInjured.SetActive(false);
        }
    }
}
