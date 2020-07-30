using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEvents 
{
    private List<int> eventCheckingList = new List<int>();
    //the number of event functions that can be called on
    private int numEventFunctions = 9;
    //these will all be functions that get passed a car object reference and end up doing soemthing to them
    
    //so the way this is done is this script gets a car and a number of events to check for
    //FOR NOW (until this is INI configurable) we pick randomly the events to role for
    //each event insularly handles the probability checks and (will evetnually handle) mutual exclusivity 
    //ideally in the future, we differ this work to a generic function
    //but this isn't the future 
    
    //please make this returna  list of string arrays instead
    public List<string[]> rollForEvents(CAR targetCar, int numToCheck, float checkpointScale)
    {
        List<string[]> eventTextList = new List<string[]>();
        //###picking which events to do

        //most likely case
        //this should always be true given checking for 2 and 3 for the time being
        //if (numToCheck < numEventFunctions)
        if (true) 
        {
            //Debug.Log("Event script wants to check " + numToCheck + " events.");
            //this block randomly will add a number 0 - number of event functions to a list
            //we then will remove ints from that list as we roll on the corresponding events they represent
            //so basically we say "check1 2 3" as long as that list has numbers in it and remove them as we go
            int tryAdding = 0;
            while (eventCheckingList.Count < numToCheck)
            {
                //for all events use this line:
                //tryAdding = (int)Random.Range(0, numEventFunctions);

                //for literally just the events I have muse this line:
                tryAdding = (int)Random.Range(2, 4);
                //getting rid of this if case since we're running 3 events with repeats (and that's fine) and don't nedd an if statement
                //if (!eventCheckingList.Contains(tryAdding))
               // {
                    eventCheckingList.Add(tryAdding);
               // }
            }
            //Debug.Log("Event script is checking" + eventCheckingList.Count + " events.");
        }

        /*
       //this just helps things go faster
        else if(numToCheck == numEventFunctions)
        {
            for(int j = 0; j < numToCheck; j++)
            {
                eventCheckingList.Add(j);
            }
        }

        //error case
        else
        {
            Debug.Log("Not enough events for requested event rolls");
        }
        */

        //###Actually Doing the events (I know it's ugly)
        //this should be in a for loop but an iterator variable outside of a while is just the same
        //just understand that's why we have an i: I'm just lazy and won't switch the While to a For.

        //This while loop passes back the text for an event back to car to call UI manger with
        //it then calls for the actual event to be applied to the car itself
        int i = 0;
        while (eventCheckingList.Count > 0)
        {
            switch (eventCheckingList[0])
            {
                //this case handdels breakdown events
                case 0:
                    {
                        if (rolld100() <= 12)
                        {
                            //###example of how to do the listbased method
                            string[] eventTextTemp = new string[2];
                            eventTextTemp[0] = "Breakdown";
                            eventTextTemp[1] = "Your carvan is unable to move until repair.";
                            eventTextList.Add(eventTextTemp);
                            i++;
                            breakdownEvent(ref targetCar);
                        }
                        break;
                    }

                //case for getting lost events
                case 1:
                    {
                        if (rolld100() <= 12)
                        {
                            string[] eventTextTemp = new string[2];
                            eventTextTemp[1] = "Lost!";
                            eventTextTemp[2] = lostEvent(ref targetCar);
                            eventTextList.Add(eventTextTemp);
                            i++;
                            lostEvent(ref targetCar);
                        }
                        break;
                    }

                //case for finding resource
                case 2:
                    {
                        if (rolld100() <= 12)
                        {
                            //have the find reosurce method return formatted text based on what is discovered AND do operation`
                            string[] eventTextTemp = findResourceEvent(ref targetCar);
                            eventTextList.Add(eventTextTemp);
                            i++;
                            
                        }
                        break;
                    }

                //case for losing a resource
                case 3:
                    {
                        if (rolld100() <= 12)
                        {
                            string[] eventTextTemp = loseResourceEvent(ref targetCar); 
                            eventTextList.Add(eventTextTemp);
                            i++;                          
                        }
                        break;
                    }

                //case for animal attack
                case 4:
                    {
                        if (rolld100() <= 12)
                        {
                            string[] eventTextTemp = new string[2];
                            eventTextTemp[0] = "Animal attack!";
                            eventTextTemp[1] = "Man this event will sure be scary when it works";
                            eventTextList.Add(eventTextTemp);
                            i++;
                            animalAttackEvent(ref targetCar);
                        }
                        break;
                    }

                //Case for Animal Tame
                case 5:
                    {
                        if (rolld100() <= 12)
                        {
                            string[] eventTextTemp = new string[2];
                            eventTextTemp[0] = "Animal Tame!";
                            eventTextTemp[1] = "This exists.";
                            eventTextList.Add(eventTextTemp);
                            i++;
                            animalTameEvent(ref targetCar);
                        }
                        break;
                    }

                //case for module breakage
                case 6:
                    {
                        if (rolld100() <= 12)
                        {
                            string[] eventTextTemp = new string[2];
                            eventTextTemp[0] = "Module broken!";
                            eventTextTemp[1] = "Your module won't have an effect until it's repaired.";
                            eventTextList.Add(eventTextTemp);
                            i++;
                            moduleBreakEvent(ref targetCar);
                        }
                        break;
                    }

                //case for rechager breaking ((might not even need thi event anymore))
                case 7:
                    {
                        if (rolld100() <= 12)
                        {
                            string[] eventTextTemp = new string[2];
                            eventTextTemp[0] = "Recharger broke!";
                            eventTextTemp[1] = "Your batteries won't recharge until your recharger is repaired.";
                            eventTextList.Add(eventTextTemp);
                            i++;
                            rechargeBreakEvent(ref targetCar);
                        }
                        break;
                    }
                
                //case for recharge boost (might just change this to give or take chare outright??)
                case 8:
                    {
                        if (rolld100() <= 12)
                        {
                            string[] eventTextTemp = new string[2];
                            eventTextTemp[0] = "Recharge Boost";
                            eventTextTemp[1] = "Get a bost to your recharging battery!";
                            i++;
                            eventTextList.Add(eventTextTemp);
                            rechargeBoostEvent(ref targetCar);
                        }
                        break;
                    }

                default:
                    {
                        Debug.Log("event number " + eventCheckingList[0] + " is out of range");
                        break;
                    }
            }
            eventCheckingList.RemoveAt(0);
        }
        return eventTextList;
    }

    //breakdown
    public void breakdownEvent(ref CAR targetCar)
    {
        targetCar.setIsBroken(true);
        return;
    }

    //getting lost
    public string lostEvent(ref CAR targetCar)
    {
        int setBack = (int)Random.Range(10, 40);
        targetCar.setMilesMoved(targetCar.getMilesMoved() - setBack);
        return ("Your caravan got set back by " + setBack + " miles!");
    }

    //find a resource
    public string[] findResourceEvent(ref CAR targetCar)
    {
        //roll a d4, give 1/4 crate's worth to the player (arbitrary rn)
        if (rolld4() == 1)
        {
            targetCar.addFood(16);
            return new string[] { "Find Food", "You found 16 food." };
        }

        else if (rolld4() == 2)
        {
            targetCar.addBatteryCapacity(3);
            return new string[] { "Find Batt Capacity", "You found a way to increase your battery capacity." };
        }

        else if (rolld4() == 3)
        {
            targetCar.addMedi(1);
            return new string[] { "Find Medi", "You found a box of medical supplies." };
        }

        else
        {
            targetCar.addMetal(1);
            return new string[] { "Find Metal", "You found a box of metal." };
        }
    }

    //lose a resource
    public string[] loseResourceEvent(ref CAR targetCar)
    {
        //roll a d4, remove some arbitrary amount of a resource from player
        if(rolld4() == 1)
        {
            targetCar.addFood((int) (0-(targetCar.getFoodCount() * .15)));
            return new string[] { "Lose Food", "You lost " + targetCar.getFoodCount() * .15 + " food." };
        }

        else if (rolld4() == 2)
        {
            targetCar.addBatteryCapacity(-1);
            return new string[] { "Lose Batt Capacity", "Your battery got perminately damaged." };
        }

        else if (rolld4() == 3)
        {
            targetCar.addMedi(-1);
            return new string[] { "Lose Medi", "You lost a box of medical supplies." };
        }

        else
        {
            targetCar.addMetal(-1);
            return new string[] { "Lose Metal", "You lost a box of metal." };
        }
    }

    //animal attack
    //this one is actually really complicate it you might wanna come back to it later
    public void animalAttackEvent(ref CAR targetCar)
    {
        //50% - animal handeling that a settler gets injured
        return;
    }

    //animal tame
    //this one is ALSO actually really complicate it you might wanna come back to it later
    public void animalTameEvent(ref CAR targetCar)
    {
       //50% + animal handeling that player get a +1 to ration gain
       // Debug.Log("animal tame");
        return;
    }

    //module break
    public void moduleBreakEvent(ref CAR targetCar)
    {
        //show the event message
        targetCar.setModuleIsBroken(true);
        //Debug.Log("module broke!");
        return;
    }

    //recharge break
    public void rechargeBreakEvent(ref CAR targetCar)
    {
        //show event message
        targetCar.setRechargerIsBroken(true);
       // Debug.Log("recharge break!");
        return;
    }

    //recharge bost
    public void rechargeBoostEvent(ref CAR targetCar)
    {
        //show message
        //get the battery from the cars recharging slot
        //if null, show message and return
        //else, roll an r 1 thru the batteries capacity and fill it
        //show that message
        //Debug.Log("charge boost");
        return;
    }

    //dice
    private int rolld100()
    {
        return (int)Random.Range(1, 100);
    }

    private int rolld4()
    {
        return (int)Random.Range(1, 4);
    }
}
