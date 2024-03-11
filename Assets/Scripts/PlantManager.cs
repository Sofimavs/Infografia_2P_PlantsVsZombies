using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlantManager : MonoBehaviour
{
    public PlantCardScrOb thisSO;
    public Transform shootPoint;
    public GameObject Bullet;
    public float health;
    public float damage;
    public float range;
    public float speed;
    public string myname;
    public LayerMask zombieLayer;
    public float fireRate;

    public bool isDragging = true;

    [Header("Animator Parameters")]
    public bool isPeaShooter;
    public bool isRepeater;
    public bool isWallnut;

    private void Start()
    {
        this.GetComponent<Animator>().enabled = false;

        health = thisSO.health;
        damage = thisSO.damage;
        range = thisSO.range;
        speed = thisSO.speed;
        Bullet = thisSO.Bullet;
        zombieLayer = thisSO.zombieLayer;
        fireRate = thisSO.fireRate;
        myname = thisSO.myname;
        StartCoroutine(Animate());
        StartCoroutine(Attack());
    }


    private void Update()
    {
        if ( health<=0)
        {
            Destroy(this.gameObject);
        }

    }

    public IEnumerator Animate()
    {
        yield return new WaitUntil(() => !isDragging);

        if(thisSO.isSunFlower)
        {
            this.GetComponent<Animator>().SetBool("isSunFlower", thisSO.isSunFlower);
        }

        if (myname == "PeaShooter")
        {
            isPeaShooter = true;
            this.GetComponent<Animator>().SetBool("isPeaShooter", isPeaShooter);
        }
        else if (myname == "Repeater")
        {
            isRepeater = true;
            this.GetComponent<Animator>().SetBool("isRepeater", isRepeater);
        }
        else if(myname == "Wallnut")
        {
            isWallnut = true;
            this.GetComponent<Animator>().SetBool("isWallnut", isWallnut);
        }

        this.GetComponent<Animator>().enabled = true;

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
                    this.GetComponent<Animator>().SetTrigger("shoot");
                }
            }
            StartCoroutine(Attack());
        }
    }

    public void Damage(float amnt)
    {
        health -=amnt;   
    }
}
