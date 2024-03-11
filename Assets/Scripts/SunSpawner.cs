using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunSpawner : MonoBehaviour
{
    public bool isSunFlower;

    public float minTime;
    public float maxTime;
    public float spawnTime;

    public GameObject sun;
    public Vector2 minPos;
    public Vector2 maxPos;
    Vector3 pos;

    void Start()
    {
        spawnTime = Random.Range(minTime, maxTime);
        if (!isSunFlower)
        {
            pos.x = Random.Range(minPos.x, maxPos.x);
            pos.y = Random.Range(minPos.y, maxPos.y);
            pos.z = -1;
        }
        else
        {
            pos.x = 0;
            pos.y = 0;
            pos.z = -1;
        }
 
        StartCoroutine(SpawnSun());

    }

    public IEnumerator SpawnSun()
    {
        yield return new WaitForSeconds(spawnTime);
        GameObject SunObject = Instantiate(sun, pos, Quaternion.identity);
        
        spawnTime = Random.Range(minTime, maxTime);
        if (!isSunFlower)
        {
            pos.x = Random.Range(minPos.x, maxPos.x);
            pos.y = Random.Range(minPos.y, maxPos.y);
            pos.z = -1;
        }
        else
        {
            Destroy(SunObject.GetComponent<Rigidbody2D>());
            pos.x = 0;
            pos.y = 0;
            pos.z = -1;

            SunObject.transform.position = new Vector3(0,0,0);
            SunObject.transform.parent = this.transform;
            SunObject.transform.localPosition = new Vector3(0, 0, 0);
        }
        StartCoroutine(SpawnSun());
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Sun")
        {
            Destroy(collision.gameObject);
        }
    }
}
