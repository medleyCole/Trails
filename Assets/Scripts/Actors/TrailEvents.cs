﻿using System.Collections;
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
    public string[,] rollForEvents(CAR targetCar, int numToCheck, float checkpointScale)
    {
        string[,] eventText = new string[numToCheck,2];
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


        //###Actually Doing the events (I know it's ugly)
        //this should be in a for loop but an iterator variable outside of a while is just the same
        //just understand that's why we have an i: I'm just lazy and won't switch the While to a For.
        int i = 0;
        while (eventCheckingList.Count > 0)
        {          
            Debug.Log("running event " + eventCheckingList[0]);
            switch (eventCheckingList[0])
            {
                case 0:
                    {
                        eventText[i,0] = "Breakdown";
                        eventText[i,1] = "Your carvan is unable to move until repair.";
                        i++;
                        breakdownEvent(ref targetCar);
                        break;
                    }

                case 1:
                    {
                        eventText[i,0] = "Lost";
                        eventText[i,1] = "You lose time finding your way back to the trail";
                        i++;
                        lostEvent(ref targetCar);
                        break;
                    }

                case 2:
                    {
                        eventText[i,0] = "Found a resource";
                        eventText[i,1] = "This text isn't formatted yet";
                        i++;
                        findResourceEvent(ref targetCar);
                        break;
                    }

                case 3:
                    {
                        eventText[i,0] = "Lost a Resource";
                        eventText[i,1] = "This text isn't formatted yet";
                        i++;
                        loseResourceEvent(ref targetCar);
                        break;
                    }

                case 4:
                    {
                        eventText[i,0] = "Animal attack!";
                        eventText[i,1] = "Man this event will sure be scary when it works";
                        i++;
                        animalAttackEvent(ref targetCar);
                        break;
                    }

                case 5:
                    {
                        eventText[i, 0] = "Animal Tame!";
                        eventText[i, 1] = "This exists.";
                        i++;
                        animalTameEvent(ref targetCar);
                        break;
                    }

                case 6:
                    {
                        eventText[i,0] = "Module broken!";
                        eventText[i,1] = "Your module won't have an effect until it's repaired.";
                        i++;
                        moduleBreakEvent(ref targetCar);
                        break;
                    }

                case 7:
                    {
                        eventText[i,0] = "Recharger broke!";
                        eventText[i,1] = "Your batteries won't recharge until your recharger is repaired.";
                        i++;
                        rechargeBreakEvent(ref targetCar);
                        break;
                    }

                case 8:
                    {
                        eventText[i,0] = "Recharge Boost";
                        eventText[i,1] = "Get a bost to your recharging battery!";
                        i++;
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
        return eventText;
    }

    //breakdown
    public void breakdownEvent(ref CAR targetCar)
    {
        //message that you broke down
        targetCar.setIsBroken(true);
        //Debug.Log("Breakdown!");
        return;
    }

    //getting lost
    public void lostEvent(ref CAR targetCar)
    {
        //message that you got lost
        //increment the game time here but don't move the car
        //Debug.Log("lost!");
        return;
    }

    //find a resource
    public void findResourceEvent(ref CAR targetCar)
    {
        //roll for a resource, for now we're using the jungle values
        //give it to the car
        //Debug.Log("found resource");
        return;
    }

    //lose a resource
    public void loseResourceEvent(ref CAR targetCar)
    {
        //roll for a resource, for now we're using the jungle values
        //take it from the car
        //Debug.Log("lost resource!");
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
        //Debug.Log("animal attack!");
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
}
