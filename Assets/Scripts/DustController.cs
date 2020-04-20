using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustController : MonoBehaviour
{
    private GameController gameController;
    private GameObject roomba;
    private bool isSucked = false;
    private Transform transform;
    private int rotationSpeedX;
    private int rotationSpeedZ;
    private int rotationSpeedY;

    // Start is called before the first frame update
    void Start()
    {
        roomba = GameObject.FindWithTag("Roomba");
        transform = GetComponent<Transform>();
        Debug.Log(transform);

        rotationSpeedX = Random.Range(-7, 7);
        rotationSpeedZ = Random.Range(-7, 7);
        rotationSpeedY = Random.Range(-7, 7);
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    public void getSucked()
    {
        isSucked = true;
        if (gameController != null) {
            gameController.addDust();
        }

        Destroy(this.gameObject, 1); // destroy after a delay
    }

    private Transform getTransform()
    {
        if (transform == null) {
            transform = GetComponent<Transform>();
        }

        return transform;
    }

    private GameObject getRoomba()
    {
        if (roomba == null) {
            roomba = GameObject.FindWithTag("Roomba");
        }

        return roomba;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSucked) {
            transform = getTransform();
            roomba = getRoomba();
            // shrink it
            if (transform.localScale.x > 0.02 && transform.localScale.y > 0.02 && transform.localScale.z > 0.02) {
                transform.localScale -= new Vector3(0.01f,0.01f,0.01f);
            }

            // move into roomba
            float step =  10 * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, roomba.transform.position, step);

            // spin it
            transform.Rotate(rotationSpeedX, rotationSpeedY, rotationSpeedZ);
        }
    }
}
