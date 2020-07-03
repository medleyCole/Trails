using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //this is a reference to its parent because 'transform.parent' is akward and it's literaly JUST a reference
    //also it means I can just move this script wherever which is baller
    public CAR existingCAR;
    public GameObject UIScreen;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(existingCAR.getHasEventActive());
        if (existingCAR.getHasEventActive() || !existingCAR.getHasEnoughFood() || !existingCAR.getHasEnoughCharge())
        {
            eventsGone(false);
        }

        else
        {
            eventsGone(true);
        }
    }

    public void incrementSelectedCarEventCounter()
    {
        existingCAR.decrimentNumEventsActive(1);
    }

    public void eventsGone(bool gone)
    {
        UIScreen.GetComponent<UIGroupManager>().toggleTurnButton(gone);
    }



    /*##############################
     * CAR <-> UI functions
     * note: right now this is meant to talk to ONE car which we always assume exists. 
     * this group of functions should, basically, just handle passing a selected caravan to the canvas
     * I REALLY don't want the UI to have to do too much math here
     * ###########################*/
}
