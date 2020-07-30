using System.Collections.Generic;
using UnityEngine;

//this is attached to the checkpoint event panel for sake of ease
//the checkpoint panel will get populated with this class directly since it is attacked to the checkpoint panel
//from there, the checkpioint event does soemthing to the seleceted car using a method in this class based on the node number and...
//the option being pressed
public class checkpointEvent : MonoBehaviour
{
    public UIGroupManager UIManager;
    public GameObject managerRef;
    private int nodeOfSelectedCar;

    //fields for the chckpointEventBox
    public TMPro.TextMeshProUGUI checkPointDescription;
    public TMPro.TextMeshProUGUI buttonBottomText;
    public TMPro.TextMeshProUGUI buttonTopText;

    //this is the lsit of all the event text (you see why I want to offload this kind of thing to an ini)
    List<string> checkpointText;
    List<string> checkpointButtonTextTop;
    List<string> checkpointButtonTextBottom;


    private void Awake()
    {
        checkpointText = new List<string>();
        checkpointButtonTextTop = new List<string>();
        checkpointButtonTextBottom = new List<string>();
        nodeOfSelectedCar = 0;

        //text node 0
        checkpointText.Add("0");
        checkpointButtonTextBottom.Add("0");
        checkpointButtonTextTop.Add("0");

        //textnodes 1
        checkpointText.Add("A deep, muddy canyon dressed with waterfalls unfolds infront of the caravan. Seems likely your tires will get bogged-down." +
                            "With some hunger-inducing work and light construction, you can get the CAR accross. " +
                            "Or you might be able to over-charge the battery to deal with the mud.");
        checkpointButtonTextBottom.Add("Remove some metal and food");
        checkpointButtonTextTop.Add("Reduce the battery charge to zero and  mess up its capacity by a bit");

    }


    //will set its node number to whatever the uimanager told it. 
    //also will set the text of the window when it's called based on the node number
    public void callCheckpointEvent(int nodeNumber)
    {
        Debug.Log("Calling event for node: " + nodeNumber);
        nodeOfSelectedCar = nodeNumber;
        checkPointDescription.text = checkpointText[nodeOfSelectedCar];
        buttonBottomText.text = checkpointButtonTextBottom[nodeOfSelectedCar];
        buttonTopText.text = checkpointButtonTextTop[nodeOfSelectedCar];
    }

    //the selected option is 0 for left, 1 for right, 2 for "build settlement"
    //as of right now we're not concerned with settlements so don't worry about 2 so much!

    //VERRY IMPORTANT: the node numbers start at 1 and not zero, it gets passed that way from the uiGroupManager already too.
    public void resolveCheckpointEvent(int selectedOption)
    {
        //for now we will handel this with switch case events.... probably not the best but hey
        switch (nodeOfSelectedCar)
        {
            case 1:
                {
                    if (selectedOption == 0)
                    {
                        //remove some metal and food
                        managerRef.GetComponent<GameManager>().existingCAR.addMetal(-1);
                        managerRef.GetComponent<GameManager>().existingCAR.addFood(-8);
                        managerRef.GetComponent<GameManager>().incrementSelectedCarEventCounter();
                        UIManager.resolveCheckpointEvent();
                    }

                    else if (selectedOption == 1) 
                    {
                        //reduce the battery charge to zero and  mess up its capacity by a bit
                        managerRef.GetComponent<GameManager>().existingCAR.addBatteryCharge(-managerRef.GetComponent<GameManager>().existingCAR.getBattCharge());
                        managerRef.GetComponent<GameManager>().existingCAR.addBatteryCapacity(-3);
                        managerRef.GetComponent<GameManager>().incrementSelectedCarEventCounter();
                        UIManager.resolveCheckpointEvent();
                    }

                    else if (selectedOption == 2)
                    {

                    }

                    else
                    {
                        //error case
                        Debug.Log("out of bounds logic error in switch 1 of chechpointEvent class in resolveCheckpointEvent ((somehow???))");
                    }
                    break;
                }

            default:
                {
                    break;
                }

        }
    }
}
