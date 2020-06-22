using System.Collections;
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

    //I'm so fucking stupid but we're hitting this with our gucci on
    //Event window stuff
    public TMPro.TextMeshProUGUI eventTitle;
    public TMPro.TextMeshProUGUI eventText;


    //((hey I can make this more generic but fuck that for the time being)
    public void toggleTurnButton(bool toggleTo)
    {
        turnButton.SetActive(toggleTo);
    }

    public void callEvent(string name, string text)
    {
        eventPanel.transform.Find("EventName").GetComponent<TMPro.TextMeshProUGUI>().text = name;
        eventTitle.text = name;
        eventText.text = text;     
    }
}
