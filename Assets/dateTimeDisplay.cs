using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dateTimeDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI dateTimeText;
    public GameObject timeRef;

    void Awake()
    {
        dateTimeText.text = ("|" + timeRef.GetComponent<Time>().getMonthNum() + " / " + timeRef.GetComponent<Time>().getDate()) + "| " + timeRef.GetComponent<Time>().getHour();
    }

    public void updateTime()
    {
        dateTimeText.text = ("|" + timeRef.GetComponent<Time>().getMonthNum() + " / " + timeRef.GetComponent<Time>().getDate()) + "| " + timeRef.GetComponent<Time>().getHour();

    }
}
