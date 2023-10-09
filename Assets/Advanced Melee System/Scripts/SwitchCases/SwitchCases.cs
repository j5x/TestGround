using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SwitchCases : MonoBehaviour
{
    public enum State
    {
        IDLE = 0,
        BUILDING = 1,
        HUNTING = 2,
        FIGHTING = 3,
        SHITTING = 4,
        SITTING = 5,
        CLOSING = 6,
        RELOADING = 7,
        REMOVING = 8,
        LAUGHING = 9,
        SINUS
    }
    public KeyCode hotkey;
    public State currentState = State.BUILDING;
    // Start is called before the first frame update
    public void DoStuff()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(hotkey))
        {
            if ((int)currentState < Enum.GetValues(typeof(State)).Length - 1)
            {
                currentState++;
            }
            else
            {
                currentState = (State)Enum.GetValues(typeof(State)).GetValue(0);
            }
        }
        switch (currentState)
        {
            case State.IDLE: //if(currentState == State.IDLE){}
                if (gameObject.name == "big dick monkey")
                {
                    currentState = State.BUILDING;
                }
                break;
            case State.BUILDING:
                break;
            case State.HUNTING:
                break;
            case State.FIGHTING:
                break;
            case State.SHITTING:
                Debug.Log("Shitting");
                break;
            case State.SITTING:

                break;
            case State.CLOSING:
                break;
            case State.RELOADING:
                break;
            case State.REMOVING:
                break;
            case State.LAUGHING:
                break;
            case State.SINUS:
                transform.position = new Vector3(Mathf.Cos(Time.time * 10), Mathf.Sin(Time.time * 10), 0);
                break;

        }
    }
}

