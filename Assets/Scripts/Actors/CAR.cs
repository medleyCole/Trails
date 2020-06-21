//I don't think I need  to inherent from thse but I don't know about libraries so I won't touch
//sorry whoever sees this!
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAR : MonoBehaviour
{
    //going to declare members a la c++ for conventions sake
    //settlers
    private List<GameObject> settlerList;
    private float[] stats = new float[6];

    //module
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
    private int batteryInUseIndex;
    private Battery chargingBattery;
    private List<Battery> batteryStorage;
    private bool isRechargerBroken;

    //moving logic
    private bool isMoving;
    private bool isBroken;

    //use this for event and checkpoitn calculation
    private float nextEventMile;
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

    //Turn/Event Processing stuff
    private TrailEvents trailEventManager;
    public bool hasEventReady;
        //a reference to manager so I can- like get information from it
    public GameObject managerRef;
        //UI connection
    public GameObject morningReportScreen;
    public GameObject turnButton; //THIS IS EXTREMELY TEMPORARY
    
    private void Awake()
    {
        //in the future, object creation will be handeld by its own window to help the player
        //make their caravan, as of right now tho... nah.

        //##settle into list
        settlerList = new List<GameObject>();
        foreach (Transform obj in this.transform)
        {
            if (obj.tag == "Settler")
            {
                settlerList.Add(obj.gameObject);
            }
        }

        Debug.Log("added " + settlerList.Count + " settlers to settlerList.");

        //##Stat assignment
        for (int i = 0; i < 6; i++)
        {
            stats[i] = 0;
        }

        for (int i = 0; i < settlerList.Count; i++)
        {
            float[] tempSettlerStats = new float[6];
            tempSettlerStats = settlerList[i].GetComponent<Settler>().getStats();
            for(int j= 0; j < 6; j++)
            {
                Debug.Log("temp stat " + j + "for settler " + i + ": " + tempSettlerStats[j]);
                stats[j] += tempSettlerStats[j];
                Debug.Log("current car stat " + j + ": " + stats[j]);
            }
        }

        //debug
        Debug.Log("Stats: ");
        for(int i = 0; i < 6; i++)
        {
            Debug.Log(stats[i]);
        }
        

        //##resource allocation
        metalCount = defaultMetal;
        mediCount = defaultMedi;
        foodCount = defaultFood;

        rationLevel = defaultRationLevel;
        speed = defaultSpeed;

        batteryStorage = new List<Battery>();
        
        //##batt init
        for(int i = 0; i < defaultBatts; i++)
        {
            batteryStorage.Add(new Battery());
        }

        batteryInUse = batteryStorage[0];
        batteryInUseIndex = 0;
        chargingBattery = null;

        //##event setup
        trailEventManager = new TrailEvents();
        nextEventMile = eventMiles;
        hasEventReady = false;

        
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
    public float[] getStats()
    {
        return stats;
    }

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

    public Settler getSettlerFromList(int index)
    {
        return settlerList[index].GetComponent<Settler>();
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
            //resource consumption
            if (foodCount != 0)
            {
                foodCount -= rationLevel;
            }

            //to drain the battery it negativly recharges.... don't worry.
            int chareOverflow = 2;
            switch (speed)
            {
                case 40:
                    if (batteryInUse.getCharge() - 1 >= 0)
                    {
                        batteryInUse.recharge(-1);
                    }

                    else
                    {
                        Debug.Log("batter change event needed");
                    }

                    break;

                case 60:
                    if(batteryInUse.getCharge() - 2 >= 0)
                    {
                        batteryInUse.recharge(-2);
                    }

                    else
                    {
                        Debug.Log("batter change event needed");
                    }

                    break;

                case 80:
                    if (batteryInUse.getCharge() - 3 >= 0)
                    {
                        batteryInUse.recharge(-3);
                    }

                    else
                    {
                        Debug.Log("batter change event needed");
                    }

                    break;

                default:
                    Debug.Log("Error in the battery degredation switch case in CAR");
                    break;
            }

            //benching battery management for now so I can do it in a more thoughtful way
            //I kind of wanna religate it to player control and an event based case because... it's easier.
            milesMoved += speed / 4;

            //###EVENT CHECKING
            if(milesMoved >= nextEventMile)
            {
                Debug.Log("Event!");
                trailEventManager.rollForEvents(this, 3, .0f);
                nextEventMile += eventMiles;
                Debug.Log("Next event set for: " + nextEventMile);
            }
        }

    }




    /*##############################
     * Private managers for events and operations
     * ###########################*/

    //functions for the individual events go here

    private void nightOperations()
    {

    }

    private void diseaseCheck()
    {

    }

}
