using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoombaMove : MonoBehaviour
{
    public float acceleration;
    public float maxSpeed;
    public float turnSpeed;
    public float maxFallSpeed;
    public float fallRate;

    private Transform transform;
    private Rigidbody rigidBody;

    private GameObject specialItem = null;

    public GameObject victorySound;

    private bool isEndOfLevel = false;

    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        rigidBody = GetComponent<Rigidbody>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (getSpecialItem() != null && !isEndOfLevel) {
            float distToSpecial = Vector3.Distance(transform.position, getSpecialItem().transform.position);
            Debug.Log("distToSpecial: " + distToSpecial);
            if (distToSpecial < 5) {
                GameObject victorySoundInstance = Instantiate(victorySound);
                Destroy(victorySoundInstance, 5);
                GameObject.FindWithTag("BackgroundMusic").gameObject.GetComponent<AudioSource>().volume = 0;
                isEndOfLevel = true;
                gameController.stopCounter();
            }
        }

        if (Input.GetKey(KeyCode.A)) {
            transform.Rotate(0, -turnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.Rotate(0, turnSpeed, 0);
        }
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
            if (Input.GetKey(KeyCode.W) && !atMaxSpeed(rigidBody.velocity)) {
                rigidBody.velocity += transform.forward * acceleration;
            }
            if (Input.GetKey(KeyCode.S) && !atMaxSpeed(rigidBody.velocity)) {
                rigidBody.velocity -= transform.forward * acceleration;
            }
        } else {
            rigidBody.velocity = stop(rigidBody.velocity);
        }

        if (!isGrounded()) {
            float correctionRate = 0.3f;
            transform.Rotate(transform.rotation.eulerAngles.x > 0 ? -correctionRate : correctionRate, 0, transform.rotation.eulerAngles.z > 0 ? -correctionRate : correctionRate);
            if (rigidBody.velocity.y >= maxFallSpeed) {
                rigidBody.velocity = new Vector3(rigidBody.velocity.x, maxFallSpeed, rigidBody.velocity.z);
            } else {
                rigidBody.velocity += -Vector3.up * fallRate;
            }
        }

        correctFlip();
    }

    private GameObject getSpecialItem()
    {
        if (specialItem == null) {
            try {
                specialItem = GameObject.FindWithTag("SpecialItem").gameObject;
            } catch (NullReferenceException exception) {
                // do nothing
            }
        }

        return specialItem;
    }

    private void correctFlip()
    {
        if (Mathf.Abs(Mathf.Abs(transform.rotation.eulerAngles.x) - 180) < 70 || Mathf.Abs(Mathf.Abs(transform.rotation.eulerAngles.z) - 180) < 70) {
            transform.rotation = Quaternion.identity;

            // freeze rotation and then unfreeze it next frame, in order to kill all rotational velocity
            rigidBody.freezeRotation = true;
        } else {
            rigidBody.freezeRotation = false;
        }
    }

    private bool isGrounded() {
        return Physics.Raycast(new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z), -Vector3.up, 1f);
    }

    private bool atMaxSpeed(Vector3 velocity)
    {
        return Mathf.Abs(velocity.x) + Mathf.Abs(velocity.z) >= maxSpeed;
    }

    private Vector3 stop(Vector3 velocity)
    {
        velocity.z = 0;
        velocity.x = 0;
        return velocity;
    }
}
