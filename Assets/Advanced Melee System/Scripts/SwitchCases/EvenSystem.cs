using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EvenSystem : MonoBehaviour
{
    // Declare a UnityEvent that will be raised when the event occurs
    public UnityEvent myEvent;

    // Call this method to raise the event
    public void RaiseEvent()
    {
        myEvent.Invoke();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        RaiseEvent();
    }

    // Update is called once per frame
    void Update()
    {
        // Add any necessary update code here
    }
}