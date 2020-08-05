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
        checkpointText.Add("A deep, muddy canyon dressed with waterfalls unfolds infront of the caravan. Seems likely your tires will get bogged-down. " +
                            "With some hunger-inducing work and light construction, you can get the CAR accross. " +
                            "Or you might be able to over-charge the battery to deal with the mud.");
        checkpointButtonTextBottom.Add("Remove some metal and food");
        checkpointButtonTextTop.Add("Reduce the battery charge to zero and  mess up its capacity by a bit");

        //textnodes 2 
        checkpointText.Add("A large river completely prevents your party's advance. Your CAR can float but needs some help accross. " +
                           "Your party thinks it could use some metal to help float the CAR. " +
                           "Or they can get in the water themselves to assist the CAR accross. ");
        checkpointButtonTextBottom.Add("Spend metal to help fjord.");
        checkpointButtonTextTop.Add("Two settlers get sick helping the CAR accross.");

        //text 3
        checkpointText.Add("Dangerous Animal Nest");
        checkpointButtonTextBottom.Add("Kill a settler");
        checkpointButtonTextTop.Add("Ppend rations top distract animals.");

        //text 4
        checkpointText.Add("A dense patch of giant fungus prevents your progress. Your party thinks they can use medical supplies to "+
                            "prevent breathing in spores. Or maybe just supercharge the bettery to rush through it.");                          
        checkpointButtonTextBottom.Add("Use medical supplies to stop spores.");
        checkpointButtonTextTop.Add("Supercharge the battery and blast on through.");

        //text 5
        checkpointText.Add("A large and vine-coated bog is keeping your caravan from advancing. " +
                           "Cutting throught he vines will be hunger inducing work that will probably end up getting someone sick. " +
                           "Your party thinks it could affix a kind of plow to the car with some metal aswell...");
        checkpointButtonTextBottom.Add("Work on cutting the vines and get a settler sick.");
        checkpointButtonTextTop.Add("Expend metal and batter integrety to plow through.");

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
        //SWITCH OVER THESE EVENTS TO INTEGRATE TRAIL EVENT FUNCTIONS SINCE THEY ALREADY TALK TO THE UI
        //for now we will handel this with switch case events.... probably not the best but hey
        switch (nodeOfSelectedCar)
        {
            //canyon
            case 1:
                {
                    if (selectedOption == 0)
                    {
                        //remove some metal and food
                        managerRef.GetComponent<GameManager>().existingCAR.addMetal(-1);
                        managerRef.GetComponent<GameManager>().existingCAR.addFood(-8);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
                        UIManager.resolveCheckpointEvent();
                    }

                    else if (selectedOption == 1) 
                    {
                        //reduce the battery charge to zero and  mess up its capacity by a bit
                        managerRef.GetComponent<GameManager>().existingCAR.addBatteryCharge(-managerRef.GetComponent<GameManager>().existingCAR.getBattCharge());
                        managerRef.GetComponent<GameManager>().existingCAR.addBatteryCapacity(-4);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
                        UIManager.resolveCheckpointEvent();
                        //can you call the ui event here to explain what the fuck happened?
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
            //river
            case 2:
                {
                    if (selectedOption == 0)
                    {
                        //use metal to patch up the car
                        managerRef.GetComponent<GameManager>().existingCAR.addMetal(-1);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
                        UIManager.resolveCheckpointEvent();
                    }

                    else if (selectedOption == 1)
                    {
                        //get two settlers sick
                        managerRef.GetComponent<GameManager>().existingCAR.makeRandomSettlersSick(2);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
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
            //nest ((maybe put this one later?))
            case 3:
                {
                    if (selectedOption == 0)
                    {
                        //kill a settler (needs function written)
                        managerRef.GetComponent<GameManager>().existingCAR.killRandomSettler(1);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
                        UIManager.resolveCheckpointEvent();
                    }

                    else if (selectedOption == 1)
                    {
                        //distract with all of the car's rations
                        managerRef.GetComponent<GameManager>().existingCAR.addFood(managerRef.GetComponent<GameManager>().existingCAR.getFoodCount());
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
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
            //fungus
            case 4:
                {
                    if (selectedOption == 0)
                    {
                        //spend medi for inhlation
                        managerRef.GetComponent<GameManager>().existingCAR.addMedi(-1);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
                        UIManager.resolveCheckpointEvent();
                    }

                    else if (selectedOption == 1)
                    {
                        //spend power to ram on thru
                        managerRef.GetComponent<GameManager>().existingCAR.addBatteryCharge(-managerRef.GetComponent<GameManager>().existingCAR.getBattCharge());
                        managerRef.GetComponent<GameManager>().existingCAR.addBatteryCapacity(-3);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
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


            //swamp
            case 5:
                {
                    if (selectedOption == 0)
                    {
                        //slahs vines
                        managerRef.GetComponent<GameManager>().existingCAR.makeRandomSettlersSick(1);
                        managerRef.GetComponent<GameManager>().existingCAR.addFood(-8);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
                        UIManager.resolveCheckpointEvent();
                    }

                    else if (selectedOption == 1)
                    {
                        //plow on through
                        managerRef.GetComponent<GameManager>().existingCAR.addBatteryCharge(-8);
                        managerRef.GetComponent<GameManager>().existingCAR.addBatteryCapacity(-1);
                        managerRef.GetComponent<GameManager>().existingCAR.addMetal(-1);
                        managerRef.GetComponent<GameManager>().decrementSelectedCarEventCounter();
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
                    Debug.Log("Did not have a case for the node number: " + nodeOfSelectedCar + " in checkpoint event class."); 
                    break;
                }

        }
    }
}
