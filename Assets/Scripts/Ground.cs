using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public GameObject groundPrefab;
    public Transform groundSpawnPt;
    bool hasSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasSpawned)
        {
            GameObject groundObj = Instantiate(groundPrefab, groundSpawnPt.transform.position, Quaternion.identity);
            hasSpawned = true;
        }
    }
}
