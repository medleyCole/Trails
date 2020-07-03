﻿//I don't think I need  to inherent from thse but I don't know about libraries so I won't touch
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
    public int defaultFood;
    public int defaultMedi;

    private int metalCount;
    private int foodCount;
    private int mediCount;

    public bool hasEnoughFood;
    public bool hasEnoughCharge;


    //battery usage
    public int defaultBatts;
    private int batteryCharge;
    private int batteryCapacity;
    private bool isRechargerBroken;

    //moving logic
    private bool isMoving;
    private bool isBroken;
    private float milesMoved;
    private int checkpointMiles;

    //is this what I really wanna do? is there a more mathier way to do this? it's a +.05 maybe just do +.0125 per move still or moving
    private int movedYesterday;
    
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
    private List<string[]> intervalEventText;
    //logic used to tell game manager what to do with the turn button
    public bool hasEventReady;
    private int numEventsActive;
    private int numEventsClosed;

    //medical event stuff
    private int rationScore;   //used for braeting, incrimented by ration level every turn, reset in morning
    private sickCheck medicalChecks;
    private List<string[]> sickList;
    private List<string[]> killList;
    private List<string[]> cureList;
    private List<string[]> spreadList;
    private int livingSettlers;

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
        batteryCharge = defaultBatts;
        batteryCapacity = defaultBatts;

        //##event setup
        trailEventManager = new TrailEvents();
        nextEventMile = eventMiles;
        hasEventReady = false;
        numEventsActive = 0;

        //med setup
        medicalChecks = new sickCheck();
        rationScore = 0;
        livingSettlers = 4;
    }

    //use this to disable buttons based on car conditions
    private void Update()
    {
        if (hasEventReady)
        {
            if (numEventsActive == 0)
            {
                //Debug.Log("No events active!");
                hasEventReady = false;
                //managerRef.GetComponent<GameManager>().eventsGone(true);
            }
        }

        if (isBroken)
        {
            speed = 0;
        }


        //checks for showing warnings about speed and food
        int settlerBatt = (int)stats[1];
        switch (speed)
        {
            case 0:
                hasEnoughCharge = true;
                break;

            case 10:
                if (batteryCharge - 1 - settlerBatt < 0)
                {
                    hasEnoughCharge = false;
                }

                else
                {
                    hasEnoughCharge = true;
                }
                break;

            case 15:
                if (batteryCharge - 2 - settlerBatt < 0)
                {
                    hasEnoughCharge = false;
                }

                else
                {
                    hasEnoughCharge = true;
                }

                break;

            case 20:
                if (batteryCharge - 3 - settlerBatt < 0)
                {
                    hasEnoughCharge = false;
                }

                else
                {
                    hasEnoughCharge = true;
                }

                break;

            default:
                Debug.Log("Error in the battery charge switch case in CAR update for warning");
                break;
        }

        //for food: if the player isn't moving they will always be able to regain food. even if not... I mean the game would freeze here if I didn't
        //resolve this boolean like this.
        if (speed == 0)
        {
            hasEnoughFood = true;
        }

        else if (foodCount + ((int)stats[0] -  (rationLevel * livingSettlers)) < 0)
        {
            hasEnoughFood = false;
        }

        else
        {
            hasEnoughFood = true;
        }

        //actually doing the calls to the ui itself
        if(!hasEnoughFood)
        {
            UIManager.GetComponent<UIGroupManager>().toggleFoodWarning(true);
        }

        else
        {
            UIManager.GetComponent<UIGroupManager>().toggleFoodWarning(false);
        }

        if (!hasEnoughCharge)
        {
            UIManager.GetComponent<UIGroupManager>().toggleChargeWarning(true);
        }

        else
        {
            UIManager.GetComponent<UIGroupManager>().toggleChargeWarning(false);
        }


    }

   

    /*##############################
    * UI operations.
    * ###########################*/

    public void toggleSpeed()
    {
       // Debug.Log("toggling speed!");
        if (speed == 20)
        {
            speed = 0;
        }

        else if (speed == 0)
        {
            speed = 10;
        }

        else
        {
            speed += 5;
        }
    }

    public void toggleRationLevel()
    {
        // Debug.Log("toggling rations!");
        if (rationLevel == 3)
        {
            rationLevel = 0;
        }
        else
        {
            rationLevel += 1;
        }
    }

    /*##############################
    * Turn operations.
    * this turns on when the turn button is pressed
    * ###########################*/
    public void nextTurn()
    {

        //check the time, if it's 2000, don't move
        if (managerRef.GetComponent<Time>().getHour() == 0)
        {
            hasEventReady = true;
            numEventsActive = 1;
            UIManager.GetComponent<UIGroupManager>().callEvent("night time", "your dudes are sleepin', my guy.", this);
        }

        //if it's 800, show the morning report
        //we also do our disease-related checking here
        else if (managerRef.GetComponent<Time>().getHour() == 400)
        {
            hasEventReady = true;
            numEventsActive = 1;
            morningReportScreen.SetActive(true);

            //check for spread (need seltter day not moving)
            spreadList = medicalChecks.spreadRoll(this);
            if (spreadList.Count > 0)
            {
                hasEventReady = true;
                numEventsActive += spreadList.Count;
            }

            //Medical check in the morning
            //check for kill (need settler day sick)
            killList = medicalChecks.killRoll(this);
            if (killList.Count > 0)
            {
                hasEventReady = true;
                numEventsActive += killList.Count;
            }

            //check for cure (need sttler day moving sick)
            cureList = medicalChecks.cureRoll(this);
            if (cureList.Count > 0)
            {
                hasEventReady = true;
                numEventsActive += cureList.Count;
            }

            //set each settler sick/day +1 if they're sick
            sickList = medicalChecks.sickRoll(this);
            if (sickList.Count > 0)
            {
                hasEventReady = true;
                numEventsActive += sickList.Count;
            }

            //###order: spread, kill, cure, sick
            //this sends the array text it got from the sickmanager to the ui manager for display
            //then we go and tell the uimanager to refresh the whole window via the carsValueScript
            for (int i = 0; i < spreadList.Count; i++)
            {
                UIManager.GetComponent<UIGroupManager>().callEvent(spreadList[i][0], spreadList[i][1], this);
                UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
            }

            for (int i = 0; i < killList.Count; i++)
            {
                UIManager.GetComponent<UIGroupManager>().callEvent(killList[i][0], killList[i][1], this);
                UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
            }

            for (int i = 0; i < cureList.Count; i++)
            {
                UIManager.GetComponent<UIGroupManager>().callEvent(cureList[i][0], cureList[i][1], this);
                UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
            }

            for (int i = 0; i < sickList.Count; i++)
            {
                UIManager.GetComponent<UIGroupManager>().callEvent(sickList[i][0], sickList[i][1], this);
                UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
            }

            //since it's a new day, the ration score resets
            rationScore = 0;
        }

        //there are 4 turns in a day
        //this also can be changed to an update and turns can be set to a start/stop
        //not sure where I am at on that design-wise

        //this is a turn case for when the player is moving during the day
        else if(speed != 0)
        {
            //if the ration level is 0, try and kill someone 
            //5% chance a settler dies when they can't eat
            if(rationLevel == 0)
            {
                for(int i = 0; i < settlerList.Count; i++)
                { 
                     if((int)Random.Range(1,100) <= 5)
                     {
                        if (!getSettlerFromList(i).getIsDead())
                        {
                            getSettlerFromList(i).setIsDead(true);
                            hasEventReady = true;
                            numEventsActive++;

                            UIManager.GetComponent<UIGroupManager>().callEvent("Starvation!", getSettlerFromList(i).getName() + "Has starved to death!", this);
                            removeSettler(i);
                            UIManager.GetComponent<UIGroupManager>().refreshCarInfo(); 
                        }
                     }
                }
            }
            

            //to drain the battery it negativly recharges.... don't worry.
            //benching battery management for now so I can do it in a more thoughtful way
            //I kind of wanna religate it to player control and an event based case because... it's easier.
            //should be an event notifcation here
            int settlerBatt = (int)stats[1];
            switch (speed)
            {
                case 10:
                    if (batteryCharge - 1 - settlerBatt >= 0)
                    {
                        addBatteryCharge(-1 - settlerBatt);
                    }

                    else
                    {
                    }

                    break;

                case 15:
                    if(batteryCharge - 2 - settlerBatt  >= 0)
                    {
                        addBatteryCharge(-2 - settlerBatt);
                    }

                    else
                    {
                    }

                    break;

                case 20:
                    if (batteryCharge - 3 - settlerBatt >= 0)
                    {
                        addBatteryCharge(-3 - settlerBatt);
                    }

                    else
                    {
                    }

                    break;

                default:
                    Debug.Log("Error in the battery charge switch case in CAR");
                    break;
            }

            //food consumption
            foodCount += (int)stats[0];
            foodCount -= rationLevel * livingSettlers;
            rationScore += rationLevel;

            if(foodCount < 0)
            {
                foodCount = 0;
            }

            //after that, move the car
            //we don't need to check for speed0 here since this next line covers that case by default
            milesMoved += speed;

         
            //###EVENT CHECKING
            //Now that the car moved, checked to see if it needs to go through any event processing
            if (milesMoved >= nextEventMile)
            {
                //we're doing a double array with variable dimensions
                //the trail event script also builds its text array the same way but do be careful
                //this gets initialized here so, basically use the first dimension to say how many events you have
                intervalEventText = trailEventManager.rollForEvents(this, eventsPerCheckpoint, .0f);
                nextEventMile += eventMiles;
                if(intervalEventText.Count > 0)
                {
                    hasEventReady = true;
                    numEventsActive = intervalEventText.Count;
                }

                //thi sends the array text it got from the trail event manager to the ui manager for display
                //then we go and tell the uimanager to refresh the whole window via the carsValueScript
                for(int i = 0; i < intervalEventText.Count; i++)
                {
                    UIManager.GetComponent<UIGroupManager>().callEvent(intervalEventText[i][0], intervalEventText[i][1], this);
                    UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
                }
            }
        }

        //not moving case, food and energy reclimation happens here
        //I want to have a ui element display so a player knows that too
        //it has to be done in this fuggoff ugly way so that when the battery can't be drained past 0, we can access this case
        if(speed == 0 && !(managerRef.GetComponent<Time>().getHour() == 0)  && !(managerRef.GetComponent<Time>().getHour() == 400))
        {
            //during speed 0, settlers will find atleast 2 food a day each, they still eat according to ration level though
            //also only postive food find stats apply.
            foodCount += livingSettlers * 2;
            foodCount -= rationLevel * livingSettlers;
            if (stats[0] > 0)
            {
                foodCount += (int)stats[0];
            }
            rationScore += rationLevel;

            //the batts will recharge at a base of 1 day
            int batteryChargeIncrease = 1;
            if (stats[2] > 0)
            {
                batteryChargeIncrease = (int)stats[2] + 1;
            }
            
            if(batteryChargeIncrease + batteryCharge <= batteryCapacity)
            {
                batteryCharge += batteryChargeIncrease;
            }

            else
            {
                batteryCharge = batteryCapacity;
            }
            
        }

        //##SETTLER SICK DAY MOVING/STOPPED LOGIC HERE
        for (int i = 0; i < settlerList.Count; i++)
        {
            if (settlerList[i].GetComponent<Settler>().getIsSick() && speed > 0)
            {
                settlerList[i].GetComponent<Settler>().incrementDaysSickMoving();
            }

            else
            {
                settlerList[i].GetComponent<Settler>().incrementDaysSickNotMoving();
            }
        }
        UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
    }


    /*##############################
     * Calls to do routine events
     * ###########################*/
    //functions for the individual events go here
    private void nightOperations()
    {

    }

    public void removeSettler(int settler)
    {
        //first remove the settler's stats from our total
        float[] removeStats = settlerList[settler].GetComponent<Settler>().getStats();
        for(int i = 0; i < stats.Length; i++)
        {
            stats[i] -= removeStats[i];
        }

        livingSettlers--;
        UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
        //NOTE: the settler stays in the list since our ui quad calculations depend on this assumption
        //as far as this car is concerned, and as far as the events are concerned, it doesn't exist
        //the ui knows it still exists but ti just shows its box as inactive- so it isn't.
        //this can be useful later so we can memorialize the dead settler or otherwise account for their not-being-alive
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

    /*##############################
      * PublicIncrementers
      * ###########################*/
    public void decrimentNumEventsActive(int num)
    {
        numEventsActive -= num;
    }

    public void addMetal(int count)
    {
        metalCount += count;
        UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
    }

    public void addMedi(int count)
    {
        mediCount += count;
        UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
    }

    public void addFood(int count)
    {
        foodCount += count;
        UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
    }

    public void addBatteryCharge(int count)
    {
        batteryCharge += count;
        UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
    }

    public void addBatteryCapacity(int count)
    {
        batteryCapacity += count;
        UIManager.GetComponent<UIGroupManager>().refreshCarInfo();
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

    public int getFoodCount()
    {
        return foodCount;
    }

    public int getBattCapacity()
    {
        return batteryCapacity;
    }

    public int getBattCharge()
    {
        return batteryCharge;
    }

    public string getBattChargeAndCapacity()
    {
        return (batteryCharge + "/" + batteryCapacity);
    }

    public int getMediCount()
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

    public List<GameObject> getSettlerList()
    {
        return settlerList;
    }

    public int getRationScore()
    {
        return rationScore;
    }

    public bool getHasEnoughFood()
    {
        return hasEnoughFood;
    }

    public bool getHasEnoughCharge()
    {
        return hasEnoughCharge;
    }

    public int getLivingSettlerCount()
    {
        return livingSettlers;
    }

    //this is used to get the charge we're expected to use ONLY considering speed
    //so far only used in the ui updates
    public int getBatteryChargeForSpeed()
    {
        switch (speed)
        {
            case 0:
                return 0;

            case 10:
                return 1;

            case 15:
                return 2;

            case 20:
                return 3;

            default:
                Debug.Log("Error in the battery charge switch case in CAR getCharge");
                return 0;
        }
    }
}

