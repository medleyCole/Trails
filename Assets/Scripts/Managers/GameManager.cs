using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this is a reference to its parent because 'transform.parent' is akward and it's literaly JUST a reference
    //also it means I can just move this script wherever which is baller
    public CAR existingCAR;
    public GameObject UIScreen;
    public UIGroupManager UIManager;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("manager up!");
        //super temporary
        Debug.Log("Manager car food: " + existingCAR.getFoodCount());       
    }

    // Update is called once per frame
    void Update()
    {
        if (existingCAR.getHasEventActive())
        {
            UIManager.toggleTurnButton(false);
        }
    }




    /*##############################
     * CAR <-> UI functions
     * note: right now this is meant to talk to ONE car which we always assume exists. 
     * this group of functions should, basically, just handle passing a selected caravan to the canvas
     * I REALLY don't want the UI to have to do too much math here
     * ###########################*/
}
