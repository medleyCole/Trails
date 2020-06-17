using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEvents 
{
    private List<int> eventCheckingList = new List<int>();
    private int numEventFunctions = 9;
    //these will all be functions that get passed a car object reference and end up doing soemthing to them
    
    //so the way this is done is this script gets a car and a number of events to check for
    //FOR NOW (until this is INI configurable) we pick randomly the events to role for
    //each event insularly handles the probability checks and (will evetnually handle) mutual exclusivity 
    //ideally in the future, we differ this work to a generic function
    //but this isn't the future 
    public void rollForEvents(CAR targetCar, int numToCheck, float checkpointScale)
    {
        //###picking which events to do

        //most likely case
        if (numToCheck < numEventFunctions)
        {
            //Debug.Log("Event script wants to check " + numToCheck + " events.");
            int tryAdding = 0;
            while (eventCheckingList.Count < numToCheck)
            {
                tryAdding = (int)Random.Range(0, numEventFunctions);
                if(!eventCheckingList.Contains(tryAdding))
                {
                    eventCheckingList.Add(tryAdding);
                }
            }
            //Debug.Log("Event script is checking" + eventCheckingList.Count + " events.");
        }

       //this just helps things go faster
        else if(numToCheck == numEventFunctions)
        {
            for(int i = 0; i < numToCheck; i++)
            {
                eventCheckingList.Add(i);
            }
        }

        //error case
        else
        {
            Debug.Log("Not enough events for requested event rolls");
        }


        //###Actually Doing the events (I know it's ugly)
        while(eventCheckingList.Count > 0)
        {
            Debug.Log("running event " + eventCheckingList[0]);
            switch (eventCheckingList[0])
            {
                case 0:
                    {
                        breakdownEvent(ref targetCar);
                        break;
                    }

                case 1:
                    {
                        lostEvent(ref targetCar);
                        break;
                    }

                case 2:
                    {
                        findResourceEvent(ref targetCar);
                        break;
                    }

                case 3:
                    {
                        loseResourceEvent(ref targetCar);
                        break;
                    }

                case 4:
                    {
                        animalAttackEvent(ref targetCar);
                        break;
                    }

                case 5:
                    {
                        animalTameEvent(ref targetCar);
                        break;
                    }

                case 6:
                    {
                        moduleBreakEvent(ref targetCar);
                        break;
                    }

                case 7:
                    {
                        rechargeBreakEvent(ref targetCar);
                        break;
                    }

                case 8:
                    {
                        rechargeBoostEvent(ref targetCar);
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

    }

    //breakdown
    public void breakdownEvent(ref CAR targetCar)
    {
        //message that you broke down
        targetCar.setIsBroken(true);
        Debug.Log("Breakdown!");
        return;
    }

    //getting lost
    public void lostEvent(ref CAR targetCar)
    {
        //message that you got lost
        //increment the game time here but don't move the car
        Debug.Log("lost!");
        return;
    }

    //find a resource
    public void findResourceEvent(ref CAR targetCar)
    {
        //roll for a resource, for now we're using the jungle values
        //give it to the car
        Debug.Log("found resource");
        return;
    }

    //lose a resource
    public void loseResourceEvent(ref CAR targetCar)
    {
        //roll for a resource, for now we're using the jungle values
        //take it from the car
        Debug.Log("lost resource!");
        return;
    }

    //animal attack
    //this one is actually really complicate it you might wanna come back to it later
    public void animalAttackEvent(ref CAR targetCar)
    {
        //pull up the screen that asks the players to commit settlers
        //once they confirm roll dice
        //resolve it
        //give food or injuries
        //move on
        Debug.Log("animal attack!");
        return;
    }

    //animal tame
    //this one is ALSO actually really complicate it you might wanna come back to it later
    public void animalTameEvent(ref CAR targetCar)
    {
        //pull up the screen that asks the players to commit settlers
        //once they confirm roll dice
        //resolve it
        //give animal or not
        //move on
        Debug.Log("animal tame");
        return;
    }

    //module break
    public void moduleBreakEvent(ref CAR targetCar)
    {
        //show the event message
        targetCar.setModuleIsBroken(true);
        Debug.Log("module broke!");
        return;
    }

    //recharge break
    public void rechargeBreakEvent(ref CAR targetCar)
    {
        //show event message
        targetCar.setRechargerIsBroken(true);
        Debug.Log("recharge break!");
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
        Debug.Log("charge boost");
        return;
    }
}
