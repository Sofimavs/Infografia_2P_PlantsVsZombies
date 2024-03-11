using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlantManager : MonoBehaviour
{
    public PlantCardScrOb thisSO;
    public Transform shootPoint;
    public GameObject Bullet;
    public float health;
    public float damage;
    public float range;
    public float speed;
    public LayerMask zombieLayer;
    public float fireRate;

    //public AudioClip damageAudio;
    public bool isDragging = true;

    private void Start()
    {
        health = thisSO.health;
        damage = thisSO.damage;
        range = thisSO.range;
        speed = thisSO.speed;
        Bullet = thisSO.Bullet;
        zombieLayer = thisSO.zombieLayer;
        fireRate = thisSO.fireRate;

        StartCoroutine(Attack());
    }


    private void Update()
    {
        if ( health<=0)
        {
            Destroy(this.gameObject);
        }

    }

    public IEnumerator Attack()
    {
        yield return new WaitUntil(()=>!isDragging);
        yield return new WaitForSeconds(fireRate);

        if (speed > 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.right, range, zombieLayer);

            Debug.DrawRay(shootPoint.position, shootPoint.right, Color.red);
            if (hit)
            {
                if (hit.transform.tag == "Zombie")
                {
                    Debug.Log("Hit zombie");
                    GameObject bullet = Instantiate(Bullet, shootPoint.transform.position, Quaternion.identity);
                    bullet.GetComponent<PeaManager>().damage = damage;
                    bullet.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
                }
            }
            StartCoroutine(Attack());
        }
    }

    public void Damage(float amnt)
    {
        //if(this.GetComponent<AudioSource>().isPlaying)
        //{
        //    this.GetComponent<AudioSource>().Play();
        //}
        health -=amnt;   
    }
}
