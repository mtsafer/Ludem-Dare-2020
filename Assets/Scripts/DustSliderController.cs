using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DustSliderController : MonoBehaviour
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
        this.gameObject.GetComponent<Slider>().value = gameController.getDustCount();
    }
}
