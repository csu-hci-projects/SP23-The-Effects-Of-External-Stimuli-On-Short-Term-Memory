using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public void OnYellow(InputValue value)
    {
        GameManager.instance.addToSequence(1);
    }

    public void OnRed(InputValue value)
    {
        GameManager.instance.addToSequence(0);
    }

    public void OnGreen(InputValue value)
    {
        GameManager.instance.addToSequence(2);
    }

    public void OnBlue(InputValue value)
    {
        GameManager.instance.addToSequence(3);
    }
}
