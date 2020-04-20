using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustTextController : MonoBehaviour
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
        //Debug.Log(gameController.timer);
        //Debug.Log(GetComponent<Text>().text);
        // TODO: This doesn't need to be in update, use a listener or something instead
        GetComponent<Text>().text = "Dust: " + gameController.getDustCount().ToString();
    }
}
