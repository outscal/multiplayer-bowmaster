using MultiplayerSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LInput : MonoBehaviour
{
    public bool sp = false;
    InputData dat=new InputData();
    
    // Start is called before the first frame update
    void Start()
    {
        sp = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)&&!sp)
        {
            sp = true;
            dat.spos = new Vector2(-5, 2);
            GameObject.FindObjectOfType<CommunicationManager>().setData(dat);
        }
    }
    
}
