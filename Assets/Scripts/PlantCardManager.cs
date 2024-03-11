using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlantCardManager : MonoBehaviour
{

    [Header("Cards Parameters")]
    public int amtCards;
    public PlantCardScrOb[] plantCardSO;
    public GameObject cardPrefab;
    public Transform cardHolderTransform;

    [Header("Plant Parameters")]
    public GameObject[] plantCards;
    public float cooldown;
    public int cost;
    public Sprite plantCard;

    void Start()
    {
        plantCards = new GameObject[amtCards];

        for (int i = 0; i < amtCards; i++)
        {
            AddPlantCard(i);
        }
    }

    public void AddPlantCard(int index) 
    {
        GameObject card = Instantiate(cardPrefab, cardHolderTransform);
        CardManager cardManager = card.GetComponent<CardManager>();

        cardManager.plantCardScrOb = plantCardSO[index];
        cardManager.plantSprite = plantCardSO[index].plantSprite;
        cardManager.UI = GameObject.FindGameObjectWithTag("Canvas");

        plantCards[index] = card;

        plantCard = plantCardSO[index].plantCard_active;
        cost = plantCardSO[index].cost;
        cooldown = plantCardSO[index].cooldown;

        card.GetComponentInChildren<Image>().sprite = plantCard;
    }


}
