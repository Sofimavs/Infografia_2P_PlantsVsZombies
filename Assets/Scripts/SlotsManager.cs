using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    public GameObject plant;
    public bool isOccupied = false;

    void OnMouseOver()
    {
        foreach (CardManager item in GameObject.FindObjectsOfType<CardManager>())
        {
            item.colliderName = this.GetComponent<SlotsManager>();
            item.isOverSlot = true;
        }
        if (plant == null)
        { 
            if (GameObject.FindGameObjectWithTag("Plant") != null)
            {
                plant = GameObject.FindGameObjectWithTag("Plant");
                plant.transform.SetParent(this.transform);
                plant.transform.localPosition = Vector3.zero;
            }
        }
    }

    void OnMouseExit()
    {
        //Destroy(plant);
    }
}
