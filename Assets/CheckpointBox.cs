using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointBox : MonoBehaviour
{
    public TMPro.TextMeshProUGUI checkpointNumber;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showEvent(int nodeNum)
    {
        checkpointNumber.text = nodeNum.ToString();
    }
}
