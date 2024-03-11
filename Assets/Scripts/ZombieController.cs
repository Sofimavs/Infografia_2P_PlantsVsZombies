using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ZombieController : MonoBehaviour
{
    public ZombieScrOb thisZombieSO;
    public ZombieAccManager zombieAcc;

    //public List<GameObject> childObjects;

    public float speed;
    public float health;
    public float handHealth;
    public float currentHealth;
    public GameObject accessory;
    public float accesoryHealth;
    public float damage;
    public float attackInterval;
    GameObject target;
    public bool isAttacking;

    //[Tooltip("Index 0: Normal Zombie, Index 1: Cone HeadZombie")]
    public float damageDelay = 2f;

    public bool isDying;

    [Header("Animator Parameters")]
    public bool isWalking;

    private void Start()
    {
        speed = thisZombieSO.zombieSpeed;
        health = thisZombieSO.zombieHealth;
        accesoryHealth = thisZombieSO.accessoryHealth;
        damage = thisZombieSO.zombieDamage;
        handHealth = thisZombieSO.zombieHandHealth;
        attackInterval = thisZombieSO.attackInterval;
        currentHealth = health;
    }

    private void Update()
    {
        if (target == null)
        {
            isAttacking = false;
        }

        if(!isAttacking && !isDying)
        {
            isWalking = true;
            this.GetComponent<Animator>().SetBool("isWalking", isWalking);
            this.transform.position += Vector3.left*speed*Time.deltaTime;
        }
        else
        {
            isWalking = false;
            this.transform.position = this.transform.position;
        }

        if(currentHealth <= 0)
        {
            //Dead
            StartCoroutine(Dies());
            
        }
    }

    public IEnumerator Dies()
    {
        isDying = true;
        this.GetComponent<Animator>().SetBool("isDying", isDying);
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Plant" || collision.gameObject.GetComponent<PlantManager>() != null)
        {
            isAttacking = true;
            target = collision.gameObject;
            StartCoroutine(Attack());
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
        isAttacking = false;
    }

    public IEnumerator Attack()
    {
        isWalking = false;

        this.GetComponent<Animator>().SetBool("isWalking", isWalking);
        this.GetComponent<Animator>().SetTrigger("Eat");
        if (target != null)
        {
            target.GetComponent<PlantManager>().Damage(damage);
        }
        
        yield return new WaitForSeconds(attackInterval);

        if (target != null)
        {
            StartCoroutine(Attack());

        }
    }

    public void DealDamage(float amnt)
    {
        currentHealth -= amnt;
        if(zombieAcc != null)
        {
            zombieAcc.TakeDamage(amnt);

        }

        StartCoroutine(DamageColor(this.gameObject.GetComponent<SpriteRenderer>()));

        foreach(Transform item in this.transform.GetComponentInChildren<Transform>())
        {
            StartCoroutine(DamageColor(item.gameObject.GetComponent<SpriteRenderer>()));
        }
    }

    public IEnumerator DamageColor(SpriteRenderer spriteRenderer)
    {
        for(int i = 0; i <= 255; i+=10)
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.color = new Color(i, i, i);
            }

            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.1f);

        for (int i = 255; i <= 0; i -= 10)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = new Color(i, i, i);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}

