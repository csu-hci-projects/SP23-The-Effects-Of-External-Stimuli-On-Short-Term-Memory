using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RumbleManager : MonoBehaviour
{
    public static RumbleManager instance;

    private Gamepad pad;
    private Coroutine stopRumble;

    private void Awake(){
        instance = this;
    }

    public void RumblePulse(float low, float high, float duration){
        pad = Gamepad.current;

        if (pad != null){
            Debug.Log(low);
            Debug.Log(high);
            Debug.Log(pad);
            pad.SetMotorSpeeds(low, high);
        }

        stopRumble = StartCoroutine(endRumble(duration, pad));
    }

    private IEnumerator endRumble(float duration, Gamepad pad){
        float timeElapsed = 0f;
        while(timeElapsed < duration){
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        pad.SetMotorSpeeds(0f, 0f);
    }
}
