using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class settlerQuadInfo : MonoBehaviour
{
    public CAR selectedCar;
    public int quadNumber;

    public TMPro.TextMeshProUGUI settlerName;
    public TMPro.TextMeshProUGUI settlerProfession;

    public TMPro.TextMeshProUGUI settlerTrait1;
    public TMPro.TextMeshProUGUI settlerTrait2;

    public TMPro.TextMeshProUGUI settlerIsSick;
    public TMPro.TextMeshProUGUI settlerIsInjured;

    public void Awake()
    {
        settlerName.text = selectedCar.getSettlerFromList(quadNumber).getName();
        settlerProfession.text = selectedCar.getSettlerFromList(quadNumber).getProfession();

        settlerTrait1.text = selectedCar.getSettlerFromList(quadNumber).getTrait1();
        settlerTrait2.text = selectedCar.getSettlerFromList(quadNumber).getTrait2();

        //injured/sick always starts as false
    }

    //accessed via the valuesFromCar UI manager,
    //it just looks into the car and updates appropriately
    public void updateSettler()
    {

    }
}
