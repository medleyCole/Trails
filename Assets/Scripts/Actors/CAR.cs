//I don't think I need  to inherent from thse but I don't know about libraries so I won't touch
//sorry whoever sees this!
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAR : MonoBehaviour
{
    //going to declare members a la c++ for conventions sake
    //settlers
    private Settler[] settlers;
    //the order of settlers by profession here is: Ecologist, Electrician, Mechanic, Doctor
    private int[] professionCount = new int[4];

    private string module;
    private bool isModuleBroken;

    //catelouges all of our modifiers for the car for easy math.
    private struct modifires { };

    //resources
    private int metalCount;
    private int foodCount;
    private int mediCount;

    //battery usage
    private Battery batteryInUse;
    private Battery chargingBattery;
    private List<Battery> batteryStorage;
    private bool isRechargerBroken;

    //moving logic
    private bool isMoving;
    private bool isBroken;

    //use this for event and checkpoitn calculation
    private float eventMilesMoved;
    //the frequency at which events happen
    public int eventMiles;

    private float milesMoved;
    private int checkpointMiles;
    

    //keeps track of consecutive days. we need this data for
    //math regarding disease rolls. used agains settler;s daysSick
    private int daysMoving;
    private int daysStopped;

    //this should only ever be a whole number because I want people to not
    //have a high mental load sorting out modifier math.
    private int speed;

    private void Awake()
    {
        //this way of setting is veyr much for the prototype
        //just set some basic paramaters here
        //also manage initial customization here

    }

    private void Update()
    {
        //move and event stuff

        //per tick:
        //log the amount you've'd move
        milesMoved += speed / 12;
        eventMilesMoved += speed / 12;

        if (eventMilesMoved >= eventMiles)
        {
            eventMilesMoved = eventMilesMoved - eventMiles;
            //play the event here
        }

        if (milesMoved >= checkpointMiles)
        {
            milesMoved = checkpointMiles;
            //assign checkpointMiles to the next one in the array that's..... somewhere
            //do the checkpoint event that's also... somewhere``````````````````````````````````````````````
        }

        //once that day is over, do your health and medical stuff.

        //for breaking down later: if you break down and fail to repair, mmediatly go to night
        //do those calculations then try another repair
    }

    //these functions are called by events in updated to manage uhh stuff
    //ideally there would be a seperate manager so multiple cars can reference it
    //realistically, this is a prototype and I'm a dumb ass. so 1 car, 1 event manager, works.


    //setters
    public void setSettler(int settler, string operation)
    {
        //this function will select a settler by number and perform some operaiton on them
        //might change settler to public access... but that's an awful idea
        //ohwell
        return;
    }

    public void setModule(string module)
    {
        return;
    }

    public void setResource(string resource, int amount)
    {
        return;
    }

    public void setMoving(bool moving)
    {
        isMoving = moving;
        return;
    }

    public void setIsBroken(bool broken)
    {
        isBroken = broken;
        return;
    }

    public void setModuleIsBroken(bool broken)
    {
        isModuleBroken = broken;
        return;
    }

    public void setRechargerIsBroken(bool broken)
    {
        isRechargerBroken = broken;
        return;
    }   

    private void carEvent()
    {
        //roll logic at each stop (values are ajusted to work in the jungle biome first)
    }

    //functions for the individual events go here

    private void nightOperations()
    {

    }  

    private void diseaseCheck()
    {

    }

}
