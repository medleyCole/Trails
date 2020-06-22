

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showSpeedScript : MonoBehaviour
{
    public GameObject selectedCAR;
    public TMPro.TextMeshProUGUI speedText;

    //hey so for the sake of this script, we assume when we select a car that the manager knows how to 
    //pass in the correct thing as a reference and has set up the UI correctly
    public void updateSpeed()
    {
        speedText.text = selectedCAR.GetComponent<CAR>().getSpeed().ToString();
    }
}
