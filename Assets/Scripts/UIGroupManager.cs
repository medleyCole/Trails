﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//###This class LITERALLY fucking exists to be called by the manager to turn on and off
//ui groups in the scene. It's just CLEANER this way. 
//no seriously, I have no idea how big the GameManager is gonan get so we're just gonna divvy stuff up this way.
public class UIGroupManager : MonoBehaviour
{
    public GameObject turnButton;
    public GameObject eventPanel;
    public GameObject carInfo;

    //I'm so fucking stupid but we're hitting this with our gucci on
    //Event window stuff
    public TMPro.TextMeshProUGUI eventTitle;
    public TMPro.TextMeshProUGUI eventText;


    //((hey I can make this more generic but fuck that for the time being)
    public void toggleTurnButton(bool toggleTo)
    {
        turnButton.SetActive(toggleTo);
    }

    //A CAR object will look through the list of events it got from TrailEvents and ask this UI manager
    //to make an event window for each. 
    //event windows keep a reference to the CAR object so the CAr can check if there are any of them active.
    //the car knows how many are out and how many left need to close. this is how it tells the manager
    //weather or not it can show the turn button. (or, in the future, weather or not the turn button takes you to a CAR instead)
    public void callEvent(string name, string text, CAR askingCar)
    {
        GameObject newPanel = Instantiate(eventPanel, this.transform);
        newPanel.SetActive(true);

        newPanel.transform.Find("EventName").GetComponent<TMPro.TextMeshProUGUI>().text = name;
        newPanel.transform.Find("DescriptionText").GetComponent<TMPro.TextMeshProUGUI>().text = text;
        newPanel.transform.SetParent(this.transform);
    }

    public void refreshCarInfo()
    {
        carInfo.GetComponent<valuesFromCar>().turnUpdate();
    }
}
