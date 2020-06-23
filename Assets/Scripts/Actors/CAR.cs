//I don't think I need  to inherent from thse but I don't know about libraries so I won't touch
//sorry whoever sees this!
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is what constitutes a CAR object in the game.
//IT has a list of settler,s logic to determine resources and how they're used
//and it also knows when it needs to check events
//the GameManager has a lsit of these cars and knows from when an event is active to od things to ui
//otherwise, the CAR knows how to call in the UI base don the event it as gotten via the event class
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
    private float milesMoved;
    private int checkpointMiles;
    
    //keeps track of consecutive days. we need this data for
    //math regarding disease rolls. used agains settler;s daysSick
    private int daysMoving;
    private int daysStopped;
    public int defaultRationLevel;
    public int defaultSpeed;

    //Toggleable levels
    private int speed;
    private int rationLevel;

    //Turn/Event Processing stuff
    //use this for event and checkpoitn calculation
    public int eventsPerCheckpoint;
    private float nextEventMile;
    //the frequency at which events happen
    public int eventMiles;
    //reference to trailEventObject for calling events
    private TrailEvents trailEventManager;
    //composing text to send to UIGroupManager for window calls
    private string[,] intervalEventText;
    //logic used to tell game manager what to do with the turn button
    public bool hasEventReady;
    private int numEventsActive;
    private int numEventsClosed;

    //a reference to manager so I can- like get information from it
    public GameObject managerRef;

    //UI connection
    public GameObject UIManager;
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
               // Debug.Log("temp stat " + j + "for settler " + i + ": " + tempSettlerStats[j]);
                stats[j] += tempSettlerStats[j];
               // Debug.Log("current car stat " + j + ": " + stats[j]);
            }
        }
   
        //##resource allocation
        metalCount = defaultMetal;
        mediCount = defaultMedi;
        foodCount = defaultFood;
        rationLevel = defaultRationLevel;
        speed = defaultSpeed;

        //##batt init
        batteryStorage = new List<Battery>();
        for (int i = 0; i < defaultBatts; i++)
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
        numEventsActive = 0;
    }

    // we are doing NOTHING with update right now accept dealing with event windows etc
    private void Update()
    {
        if(hasEventReady)
        {
            if(numEventsActive == 0)
            {
                Debug.Log("No events active!");
                hasEventReady = false;
                managerRef.GetComponent<GameManager>().eventsGone(true);
            }
        }
    } 

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

    public void decrimentNumEventsActive(int num)
    {
        numEventsActive -= num;
        Debug.Log("events active now: " + numEventsActive);
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

    public bool getHasEventActive()
    {
        return hasEventReady;
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
            numEventsActive = 1;
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
                //we're doing a double array with variable dimensions
                //the trail event script also builds its text array the same way but do be careful
                Debug.Log("Event!");
                hasEventReady = true;
                numEventsActive = eventsPerCheckpoint;
                intervalEventText = trailEventManager.rollForEvents(this, eventsPerCheckpoint, .0f);
                nextEventMile += eventMiles;

                

                //checking to see if array builds via debug before differing to the UI manager:
                for(int i = 0; i < eventsPerCheckpoint; i++)
                {
                    Debug.Log(intervalEventText[i, 0]);
                    Debug.Log(intervalEventText[i, 1]);
                    UIManager.GetComponent<UIGroupManager>().callEvent(intervalEventText[i, 0], intervalEventText[i, 1], this);
                }
            }
        }

    }


    /*##############################
     * Calls to do routine events
     * ###########################*/
    //functions for the individual events go here
    private void nightOperations()
    {

    }

    private void diseaseCheck()
    {

    }
}
