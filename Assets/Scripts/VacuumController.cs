using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumController : MonoBehaviour
{
    public List<GameObject> suckSounds;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Dust") {
            other.GetComponent<DustController>().getSucked();
            playSuckSound();
        }
    }

    private void playSuckSound()
    {
        GameObject suckSound = suckSounds[Random.Range(0, suckSounds.Count)];
        GameObject suckSoundInstance = Instantiate(suckSound);
        Destroy(suckSoundInstance, 2);
    }
}
