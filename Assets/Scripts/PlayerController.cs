using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PlayerController instance;

    

    public void OnYellow(InputValue value)
    {
        //if (Input.GetButtonDown("Yellow"))
        //{
        Debug.Log("yellow pressed");
        RumbleManager.instance.RumblePulse(.25f, .75f, .75f);
        GameManager.instance.addToSequence(1);
        //}
    }

    public void OnRed(InputValue value)
    {
        Debug.Log("red pressed");
        GameManager.instance.addToSequence(0);
    }

    public void OnGreen(InputValue value)
    {
        Debug.Log("green pressed");
        GameManager.instance.addToSequence(2);
    }

    public void OnBlue(InputValue value)
    {
        Debug.Log("blue pressed");
        GameManager.instance.addToSequence(3);
    }

    public void OnRumbleAction(InputValue value){
        Debug.Log("RUMBLE TIME");
        RumbleManager.instance.RumblePulse(.25f, .75f, .75f);
    }
}
