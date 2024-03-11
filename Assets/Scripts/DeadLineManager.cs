using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadLineManager : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            Debug.Log("Died!");
        }
    }
}
