using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dateTimeDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dateTimeText;
    public GameObject timeRef;

    void Awake()
    {
        dateTimeText.text = ("|" + timeRef.GetComponent<myTime>().getMonthNum() + " / " + timeRef.GetComponent<myTime>().getDate()) + "| " + timeRef.GetComponent<myTime>().getHour();
    }

    public void updateTime()
    {
        dateTimeText.text = ("|" + timeRef.GetComponent<myTime>().getMonthNum() + " / " + timeRef.GetComponent<myTime>().getDate()) + "| " + timeRef.GetComponent<myTime>().getHour();

    }
}
