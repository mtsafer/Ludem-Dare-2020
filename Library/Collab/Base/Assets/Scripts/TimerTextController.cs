using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerTextController : MonoBehaviour
{
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        if (gameController == null)
        {
            gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(gameController.timer);
        //Debug.Log(GetComponent<Text>().text);
        GetComponent<Text>().text = "Battery: " + Mathf.Round(gameController.timer).ToString();
    }
}
