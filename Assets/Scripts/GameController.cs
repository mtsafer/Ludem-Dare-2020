using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    private GameObject roomba;
    private GameObject gameOverPanel;
    private GameObject specialItemSpawner;
    private GameObject mainCamera;
    private bool hasTriggeredSpecialItem = false;

    public GameObject gameOverSound;
    public GameObject itemFoundSound;
    public float timer;
    private int dustCounter = 0;

    public int dustToCompleteLevel;

    private AudioSource backgroundMusic;
    private float originalBackgroundVolume;

    private bool shouldCountTimer = true;


    // Start is called before the first frame update
    void Start()
    {
        roomba = GameObject.FindWithTag("Roomba");
        specialItemSpawner = GameObject.FindWithTag("SpecialItemSpawner");
        mainCamera = GameObject.FindWithTag("MainCamera");

        gameOverPanel = GameObject.FindWithTag("GameOver").gameObject;
        gameOverPanel.SetActive(false);

        backgroundMusic = GameObject.FindWithTag("BackgroundMusic").GetComponent<AudioSource>();
    }

    public void stopCounter()
    {
        shouldCountTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0.0f)
        {
            GameOver();
        } else if (shouldCountTimer)
        {
            timer -= Time.deltaTime;
        }

        if (dustCounter >= dustToCompleteLevel && !hasTriggeredSpecialItem) {
            specialItemSpawner.GetComponent<SpecialSpawn>().spawnItem();
            mainCamera.GetComponent<MainCameraController>().triggerSpecialItemPreview();
            GameObject itemFoundSoundInstance = Instantiate(itemFoundSound);
            Destroy(itemFoundSoundInstance, 5);
            hasTriggeredSpecialItem = true;
        }

        if (backgroundMusic != null && gameOverPanel.active && backgroundMusic.volume > 0) {
            backgroundMusic.volume -= Time.deltaTime;
        } else if (backgroundMusic.volume < originalBackgroundVolume) {
            backgroundMusic.volume += Time.deltaTime;
        }
    }

    void GameOver()
    {
        if (!gameOverPanel.active) {
            // Disable player controls
            roomba.GetComponent<RoombaMove>().enabled = false;
            GameObject gameOverSoundInstance = Instantiate(gameOverSound);
            Destroy(gameOverSoundInstance, 15);

            // Show gameover/restart menu
            gameOverPanel.SetActive(true);
        }
    }

    public void addDust()
    {
        dustCounter++;
    }

    public int getDustCount()
    {
        return dustCounter;
    }

    public void Restart()
    {
        // Restart current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("TestScene");
    }
}
