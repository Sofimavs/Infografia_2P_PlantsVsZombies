using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public ZombieScrOb[] zombieScrObs;
    public ZombieScrOb selectedSO;
    public float timeInterval;
    public bool randomizeTimes;
    public float minTime;
    public float maxTime;
    public Transform[] columns;
    public int selectedColumns;

    private void Start()
    {
        StartCoroutine(ZombieSpawn());
    }

    public IEnumerator ZombieSpawn()
    {
        timeInterval = randomizeTimes ? Random.Range(minTime, maxTime) : timeInterval;

        yield return new WaitForSeconds(timeInterval);

        selectedSO = zombieScrObs[Random.Range(0, zombieScrObs.Length)];

        int columnID = Random.Range(0, columns.Length);
        GameObject zombie = Instantiate(selectedSO.zombieDefault,columns[columnID]);

        zombie.transform.SetParent(columns[columnID]);
        zombie.transform.position = new Vector3(0,0,0);
        zombie.transform.localPosition = new Vector3(0,0,0);

        if(selectedSO.zombieAccessory != null)
        {
            GameObject accessory =  Instantiate(selectedSO.zombieAccessory, zombie.transform);
            zombie.GetComponent<ZombieController>().accessory = accessory;
            zombie.GetComponent<ZombieController>().zombieAcc = accessory.GetComponent<ZombieAccManager>();
            zombie.GetComponent<ZombieController>().zombieAcc.accessoryHealth = selectedSO.accessoryHealth;
            zombie.GetComponent<ZombieController>().zombieAcc.accessoryHealthCurrent = selectedSO.accessoryHealth;

        }

        StartCoroutine(ZombieSpawn());
    }
}
