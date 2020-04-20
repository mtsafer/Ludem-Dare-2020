using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsInterface : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
        }
    }
}
