using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private const float TIME_LIMIT = 10.0f;

    private GameObject roomba;
    private GameObject gameOverPanel;

    public float timer;
    public float dustCounter = 0;


    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Test");
        timer = TIME_LIMIT;

        roomba = GameObject.FindWithTag("Roomba");

        gameOverPanel = GameObject.FindWithTag("GameOver").gameObject;
        gameOverPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(timer);
        if (timer <= 0.0f)
        {
            GameOver();
        } else
        {
            timer -= Time.deltaTime;
        }
    }

    void GameOver()
    {
        // Disable player controls
        roomba.GetComponent<RoombaMove>().enabled = false;

        // Show gameover/restart menu
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        // Restart current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
