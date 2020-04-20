using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGenerator : MonoBehaviour
{
    public List<GameObject> trashList;
    public int trashCount;

    private float sizeX;
    private float sizeZ;
    private Transform transform;
    private float slopeOfParentX;
    private float slopeOfParentZ;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
        sizeX = transform.parent.localScale.x;
        sizeZ = transform.parent.localScale.z;
        slopeOfParentX = -Mathf.Tan(transform.parent.rotation.x);
        slopeOfParentZ = Mathf.Tan(transform.parent.rotation.z);

        spawnTrash();
    }

    private void spawnTrash()
    {
        /* spawn all of trash
          1. get a random object
          2. create the object in a random place on the floor
          3. check that the object is not colliding with another object
          3.5. if the object is colliding, recreate it until it is not colliding
          4. rotate the object randomly on the y axis
        */
        for (int i = 0; i < trashCount; i++) {
            GameObject trashToSpawn = getRandomTrash();
            float xSpawn = Random.Range(-(sizeX / 2.1f), (sizeX / 2.1f)) + transform.parent.position.x;
            float zSpawn = (Random.Range(-(sizeZ / 2.1f), (sizeZ / 2.1f))) + transform.parent.position.z;

            float xPositionDiffFromParent = xSpawn - transform.parent.position.x;
            float zPositionDiffFromParent = zSpawn - transform.parent.position.z;

            // y spawn is calculated from the slopes of the spawn area
            float ySpawn = trashToSpawn.transform.position.y + transform.parent.position.y + (xPositionDiffFromParent * slopeOfParentZ * 2.1f) + (zPositionDiffFromParent * slopeOfParentX * 2.1f);
            Instantiate(
                trashToSpawn,
                new Vector3(xSpawn, ySpawn, zSpawn),
                Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f)
            );

            // TODO: make sure it only generates trash in valid areas - ie (not in roomba, not in furniture)
        }
    }

    private GameObject getRandomTrash()
    {
        return trashList[Random.Range(0, trashList.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
