using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadLineManager : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Zombie")
        {
            Debug.Log("Died!");
            SceneManager.LoadScene("LostScene");
            //stoping game
        }
    }
}
