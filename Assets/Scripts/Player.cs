using System;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public enum Controller
    {
        WASD,
        Arrows
    }

    public Controller Control;

	// Use this for initialization
	void Start ()
	{
	    var joystickNames = Input.GetJoystickNames();
	    foreach (var joystickName in joystickNames)
	    {
	        Debug.Log(joystickName);
	    }

        //http://answers.unity3d.com/questions/548994/multiple-local-controllers-getting-messed-up.html

    }

    // Update is called once per frame
    void Update () {
	
	}
}
