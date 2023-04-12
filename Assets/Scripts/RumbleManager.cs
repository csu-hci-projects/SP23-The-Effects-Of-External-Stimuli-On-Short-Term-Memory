using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private Gamepad pad;

    private Coroutine stopRumbleAfterCoroutine;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void RumbleColor(int buttonID){
        switch (buttonID){
            case 0:
                RumblePulse(.25f, 1f, .5f);
                break;
            case 1:
                RumblePulse(.5f, .5f, .25f);
                break;
            case 2:
                RumblePulse(.25f, .25f, .5f);
                break;
            case 3:
                RumblePulse(1f, 1f, .25f);
                break;

        }
    }

    private IEnumerator waitSec(){
        yield return new WaitForSeconds(0.5f);
    }

    public void RumblePulse(float lowFrequency, float highFrequency, float duration)
    {
        pad = Gamepad.current;

        if (pad != null)
        {
            pad.SetMotorSpeeds(lowFrequency, highFrequency);
            stopRumbleAfterCoroutine = StartCoroutine(StopRumble(duration, pad));

        }
    }

    private IEnumerator StopRumble(float duration, Gamepad pad)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pad.SetMotorSpeeds(0f, 0f);
    }
}
