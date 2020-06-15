using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEvents : MonoBehaviour
{
    //these will all be functions that get passed a car object reference and end up doing soemthing to them
    //this should REALLYL interract with an event system but I am a stooy boy and do not know how to do that for right now. 

    //breakdown
    public void breakdownEvent(ref CAR targetCar)
    {
        //message that you broke down
        targetCar.setIsBroken(true);
        return;
    }

    //getting lost
    public void lostEvent(ref CAR targetCar)
    {
        //message that you got lost
        //increment the game time here but don't move the car
        return;
    }

    //find a resource
    public void findResourceEvent(ref CAR targetCar)
    {
        //roll for a resource, for now we're using the jungle values
        //give it to the car
        return;
    }

    //lose a resource
    public void loseResourceEvent(ref CAR targetCar)
    {
        //roll for a resource, for now we're using the jungle values
        //take it from the car
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
        return;
    }

    //module break
    public void moduleBreakEvent(ref CAR targetCar)
    {
        //show the event message
        targetCar.setModuleIsBroken(true);
        return;
    }

    //recharge break
    public void rechargeBreakEvent(ref CAR targetCar)
    {
        //show event message
        targetCar.setRechargerIsBroken(true);
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
        return;
    }
}
