using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infoUIScript : MonoBehaviour
{
    public CAR selectedCar;

    public TMPro.TextMeshProUGUI foodMod;
    public TMPro.TextMeshProUGUI battSpendMod;
    public TMPro.TextMeshProUGUI rechargeMod;
    public TMPro.TextMeshProUGUI repairMod;
    public TMPro.TextMeshProUGUI healMod;
    public TMPro.TextMeshProUGUI animalHandlingMod;

    public void Awake()
    {
        refreshCaravanInfo(); 
    }

    public void refreshCaravanInfo()
    {
        float[] tempStats = selectedCar.getStats();
        //the car's stat for food consumption is just the modifiers. To show how much food the player will lose per turn, 
        //you need to combine that with -rationLevel*livingSettlercount
        foodMod.text = (tempStats[0] - selectedCar.getRationLevel() * selectedCar.getLivingSettlerCount()).ToString();
        if(selectedCar.getSpeed() == 0)
        {
            foodMod.text = (tempStats[0] - selectedCar.getRationLevel() * selectedCar.getLivingSettlerCount() + selectedCar.getLivingSettlerCount() * 2).ToString();
        }
        //you don't use battery while moving and you don't recharge while moving. the UI reflects that here
        if (selectedCar.getSpeed() == 0)
        {
            //recharging has a base of 1/day, the car code already knows that, so the ui needds to reflect that.
            if (tempStats[2] <= 0)
            {
                rechargeMod.text = "+1";              
            }
            else
            {
                rechargeMod.text = "+" + (tempStats[2] + 1).ToString();
            }

            battSpendMod.text = "0";
        }

        else
        {
            battSpendMod.text = "-" + (tempStats[1] + selectedCar.getBatteryChargeForSpeed()).ToString();
            rechargeMod.text = "0";

        }
        
        repairMod.text = "+" + tempStats[3].ToString();
        healMod.text = "+" + tempStats[4].ToString();
        animalHandlingMod.text = "+" + tempStats[5].ToString();
    }

}
