using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardManager : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public GameObject UI;
    public SlotsManager colliderName;
    SlotsManager prevName;
    public PlantCardScrOb plantCardScrOb;
    public Sprite plantSprite;
    public GameObject plantPrefab;
    public bool isOverSlot = false;
    GameObject plant;
    bool isHoldingPlant;

    public bool isCoolingDown;

    public void OnDrag(PointerEventData eventData)
    {
        if(isCoolingDown)
        {
            return;
        }
        //agarrar Objeto
        if (isHoldingPlant)
        {
            plant.GetComponent<SpriteRenderer>().sprite = plantSprite;

            if (prevName != colliderName || prevName == null)
            {
                isOverSlot = false;
                if (prevName != null)
                {
                    prevName.plant = null;
                }
                prevName = colliderName;
            }
            if (!colliderName.isOccupied)
            {
                plant.transform.position = new Vector3(0, 0, 0);
                plant.transform.localPosition = new Vector3(0, 0, 0);
            }

            if (!isOverSlot)
            {
                plant.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
        }
   
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isCoolingDown)
        {
            return;
        }

        if (GameObject.FindObjectOfType<GameManager>().SunAmnt >=plantCardScrOb.cost)
        {
            isHoldingPlant = true;
            plant = Instantiate(plantPrefab, Vector3.zero, Quaternion.identity);
            plant.GetComponent<PlantManager>().thisSO = plantCardScrOb;
            plant.GetComponent<PlantManager>().isDragging = true;
            plant.GetComponent<SpriteRenderer>().sprite = plantSprite;

            plant.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else
        {
            Debug.Log("Not enough sun!");
        }
        
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (isCoolingDown)
        {
            return;
        }

        if (isHoldingPlant)
        {
            if (colliderName != null && !colliderName.isOccupied)
            {
                GameObject.FindObjectOfType<GameManager>().DeductSun(plantCardScrOb.cost);
                isHoldingPlant = false;
                colliderName.isOccupied = true;
                plant.tag = "Untagged";
                plant.transform.SetParent(colliderName.transform);
                plant.transform.localPosition = new Vector3(0, 0, 0);
                plant.AddComponent<BoxCollider2D>();
                plant.AddComponent<CircleCollider2D>();
                plant.AddComponent<CircleCollider2D>().isTrigger = true;

                plant.GetComponent<PlantManager>().isDragging = false;

                if (plantCardScrOb.isSunFlower)
                {
                    SunSpawner sunSpawner = plant.AddComponent<SunSpawner>();
                    sunSpawner.isSunFlower = true;
                    sunSpawner.minTime = plantCardScrOb.sunSpawnerTemplate.minTime;
                    sunSpawner.maxTime = plantCardScrOb.sunSpawnerTemplate.maxTime;
                    sunSpawner.sun = plantCardScrOb.sunSpawnerTemplate.sun;
                }

                //Cooldown
                StartCoroutine(cardCoolDown(plantCardScrOb.cooldown));
            }
            else
            {
                isHoldingPlant = false;
                Destroy(plant);
            }
        }
        
    }

    public IEnumerator cardCoolDown(float cooldownDuration)
    {
        isCoolingDown = true;
        yield return new WaitForSeconds(cooldownDuration);
        isCoolingDown = false;
    }

}
