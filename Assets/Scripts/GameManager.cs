using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text sunDisp;
    public int startSunAmnt;
    public int SunAmnt = 0;

    public Transform cardHolder;

    private void Start()
    {
        AddSun(startSunAmnt);

        foreach(Transform card in cardHolder)
        {
            try
            {
                card.GetComponent<CardManager>().StartLoad();
                card.GetComponent<CardManager>().updateCardStatus();
            }
            catch(System.Exception)
            {
                Debug.LogError("Card Error");
            }
        }
    }
    public void AddSun(int amnt)
    {
        SunAmnt += amnt;
        sunDisp.text = "" + SunAmnt;
        foreach (Transform card in cardHolder)
        {
            try
            {
                card.GetComponent<CardManager>().updateCardStatus();
            }
            catch (System.Exception)
            {
                Debug.LogError("Card Error");
            }
        }
    }

    public void DeductSun(int amnt) 
    { 
        SunAmnt -= amnt;
        sunDisp.text = "" + SunAmnt;
        foreach (Transform card in cardHolder)
        {
            try
            {
                card.GetComponent<CardManager>().updateCardStatus();
            }
            catch (System.Exception)
            {
                Debug.LogError("Card Error");
            }
        }
    }
}
