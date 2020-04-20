using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialSpawn : MonoBehaviour
{
    public GameObject specialItem;

    public void spawnItem()
    {
        Instantiate(
            specialItem,
            GetComponent<Transform>()
        );
    }
}
