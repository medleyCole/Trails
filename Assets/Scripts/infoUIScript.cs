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
        foodMod.text = "+" + tempStats[0].ToString();
        battSpendMod.text = "+" + tempStats[1].ToString();
        rechargeMod.text = "+"  + tempStats[2].ToString();
        repairMod.text = "+" + tempStats[3].ToString();
        healMod.text = "+" + tempStats[4].ToString();
        animalHandlingMod.text = "+" + tempStats[5].ToString();
    }

}
