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
        Debug.Log("ui output for infor screen should be:");


        float[] tempStats = selectedCar.getStats();
        foodMod.text = tempStats[0].ToString();
        battSpendMod.text = tempStats[1].ToString();
        rechargeMod.text = tempStats[2].ToString();
        repairMod.text = tempStats[3].ToString();
        healMod.text = tempStats[4].ToString();
        animalHandlingMod.text = tempStats[5].ToString();

        //Debug.Log(tempStats[0].ToString() +  tempStats[1].ToString() + tempStats[2].ToString() +  tempStats[3].ToString() +  tempStats[4].ToString() +  tempStats[5].ToString());
    }

}
