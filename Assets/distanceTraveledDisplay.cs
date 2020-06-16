using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class distanceTraveledDisplay : MonoBehaviour
{
    public GameObject selectedCAR;
    public TMPro.TextMeshProUGUI distanceText;

    public void updateDistanceTraveledUI()
    {
        distanceText.text = selectedCAR.GetComponent<CAR>().getDistanceTraveled().ToString();
    }
}
