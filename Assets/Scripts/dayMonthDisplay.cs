using System.Collections;
using System.Collections.Generic;

using UnityEngine.UI;
using UnityEngine;


public class dayMonthDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dayMonthText;
    public GameObject timeRef;

    void Awake()
    {
        dayMonthText.text = (timeRef.GetComponent<myTime>().getDay() + ",  " + timeRef.GetComponent<myTime>().getMonth());
    }

    public void updateTime()
    {
        dayMonthText.text = (timeRef.GetComponent<myTime>().getDay() + ",  " + timeRef.GetComponent<myTime>().getMonth());
    }
}
