using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSliderController : MonoBehaviour
{
    private GameController gameController;
    public Image Fill;

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
        this.gameObject.GetComponent<Slider>().value = gameController.timer;
        if (Fill && gameController.timer < 10.0f)
        {
            Fill.color = Color.red;
        }
    }
}
