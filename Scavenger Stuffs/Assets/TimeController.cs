using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimeController : MonoBehaviour {

	public int secEventTime = 5;
	public int minEventTime = 0;
	public int hourEventTime = 0;
    DateTime eventTime;
    TimeSpan passedTime;
	
    // Use this for initialization
    void Start () {
        eventTime = System.DateTime.Now;
    }

	// Update is called once per frame
	void Update () {
    
        passedTime = eventTime - System.DateTime.Now;

        if (Math.Abs(passedTime.TotalSeconds) >= secEventTime && Math.Abs(passedTime.TotalMinutes) >= minEventTime && Math.Abs(passedTime.TotalHours) >= hourEventTime )
        {
            GetComponent<AnimaInfo>().adventureEvent(System.DateTime.Now.Hour, System.DateTime.Now.Minute);
            eventTime = System.DateTime.Now;
        }

    }

    

}
