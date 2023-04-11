using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumbleTest : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameManager _gameManager;

    // Update is called once per frame
    void Update()
    {
        if(_gameManager.ShouldRumble){
            Debug.Log("HELLO PLEASE");
            RumbleManager.instance.RumblePulse(.25f, .75f, .75f);
        }
    }
}
