using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventBoxDestroy : MonoBehaviour
{
    public GameObject toKill;
    public void kill()
    {
        toKill.SetActive(false);
        Destroy(toKill);
    }
}
