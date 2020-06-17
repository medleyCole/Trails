﻿//I don't think I need  to inherent from thse but I don't know about libraries so I won't touch
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
    public int defaultMetal;
    public float defaultFood;
    public int defaultMedi;

    private int metalCount;
    private float foodCount;
    private int mediCount;


    //battery usage
    public int defaultBatts;

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

    public int defaultRationLevel;
    public int defaultSpeed;

    //this should only ever be a whole number because I want people to not
    //have a high mental load sorting out modifier math.
    private int speed;
    private int rationLevel;

    //### Turn/Event Processing stuff
    //a reference to manager so I can- like get information from it
    public GameObject managerRef;
    public GameObject morningReportScreen;
    public GameObject turnButton; //THIS IS EXTREMELY TEMPORARY
    public bool hasEventReady;

    private void Awake()
    {
        //this way of setting is very much for the prototype
        //just set some basic paramaters here
        //also manage initial customization here
        hasEventReady = false;

        //in the future, object creation will be handeld by its own window to help the player
        //make their caravan, as of right now tho... nah.

        //resource allocation
        metalCount = defaultMetal;
        mediCount = defaultMedi;
        foodCount = defaultFood;

        rationLevel = defaultRationLevel;
        speed = defaultSpeed;

        batteryStorage = new List<Battery>();
        
        //batt init
        for(int i = 0; i < defaultBatts; i++)
        {
            batteryStorage.Add(new Battery());
        }

        batteryInUse = batteryStorage[0];
        chargingBattery = null;
    }

    // we are doing NOTHING with update right now
  /*  private void Update()
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
            //do the checkpoint event that's also... somewhere
        }

        //once that day is over, do your health and medical stuff.

        //for breaking down later: if you break down and fail to repair, mmediatly go to night
        //do those calculations then try another repair
    } */

    //these functions are called by events in updated to manage uhh stuff
    //ideally there would be a seperate manager so multiple cars can reference it
    //realistically, this is a prototype and I'm a dumb ass. so 1 car, 1 event manager, works.


    /*##############################
      * Public Setters
      * ###########################*/
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

    /*##############################
      * Public Getters
      * ###########################*/
    public int getSpeed()
    {
        return speed;
    }

    public float getDistanceTraveled()
    {
        return milesMoved;
    }

    public float getFoodCount()
    {
        return foodCount;
    }

    public int getBattCount()
    {
        return batteryStorage.Count;
    }

    public string getBattCharge()
    {
        return (batteryInUse.getCharge().ToString() + "/" + batteryInUse.getCapacity().ToString());
    }

    public int getmediCount()
    {
        return mediCount;
    }

    public int getMetalCount()
    {
        return metalCount;
    }

    public int getRationLevel()
    {
        return rationLevel;
    }

    /*##############################
    * UI operations.
    * ###########################*/

    public void toggleSpeed()
    {
       // Debug.Log("toggling speed!");
        if (speed == 80)
        {
            speed = 0;
        }

        else if (speed == 0)
        {
            speed = 40;
        }

        else
        {
            speed += 20;
        }
    }

    public void toggleRationLevel()
    {
        // Debug.Log("toggling rations!");
        if (rationLevel == 3)
        {
            rationLevel = 1;
        }
        else
        {
            rationLevel += 1;
        }
    }

    /*##############################
    * Turn operations.
    * ###########################*/
    public void nextTurn()
    {
        //check the time, if it's 2000, don't move
        if (managerRef.GetComponent<Time>().getHour() == 0)
        {
            Debug.Log("night time");
        }

        //if it's 800, show the morning report
        else if (managerRef.GetComponent<Time>().getHour() == 400)
        {
            hasEventReady = true;
            turnButton.SetActive(false);
            morningReportScreen.SetActive(true);
        }


        //there are 4 turns in a day
        //this also can be changed to an update and turns can be set to a start/stop
        //not sure where I am at on that design-wise
        // Debug.Log("moved: " + speed / 4 + "miles");
        else
        {
            milesMoved += speed / 4;
        }

    }




    /*##############################
     * Private managers for events and operations
     * ###########################*/

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
