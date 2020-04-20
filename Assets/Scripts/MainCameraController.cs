using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    private GameObject specialItemSpawner;
    private bool showSpecialItem = false;
    private Transform transform;
    private Vector3 startingLocalPosition;
    private Quaternion startingLocalRotation;
    private float minDistFromSpecialItem = 5;
    private float secondsToWaitAtSpecialItem = 1.5f;
    private Transform originalParent;
    private GameObject roomba;

    private GameController gameController;


    // Start is called before the first frame update
    void Start()
    {
        specialItemSpawner = GameObject.FindWithTag("SpecialItemSpawner");
        roomba = GameObject.FindWithTag("Roomba");
        transform = GetComponent<Transform>();
        startingLocalPosition = transform.localPosition;
        startingLocalRotation = transform.localRotation;
        originalParent = transform.parent;

        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (showSpecialItem) {
            roomba.GetComponent<RoombaMove>().enabled = false;

            // Don't decrease timer during the zoom, that'd be cruel
            gameController.timer += Time.deltaTime;
            float distFromItem = Vector3.Distance(transform.position, specialItemSpawner.transform.position);
            if (minDistFromSpecialItem < distFromItem) {
                transform.SetParent(specialItemSpawner.GetComponent<Transform>());
                transform.LookAt(specialItemSpawner.transform);
                transform.Translate(transform.forward * Time.deltaTime * distFromItem / 2);
            } else {
                secondsToWaitAtSpecialItem -= Time.deltaTime;
                if (secondsToWaitAtSpecialItem <= 0) {
                    showSpecialItem = false;
                    secondsToWaitAtSpecialItem = 3;
                    roomba.GetComponent<RoombaMove>().enabled = true;
                    transform.SetParent(originalParent);
                    transform.localPosition = startingLocalPosition;
                    transform.localRotation = startingLocalRotation;
                }
            }
        }
    }

    public void triggerSpecialItemPreview()
    {
        showSpecialItem = true;
    }
}
