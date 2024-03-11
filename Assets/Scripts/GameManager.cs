using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TMP_Text sunDisp;
    public int startSunAmnt;
    public int SunAmnt = 0;

    private void Start()
    {
        AddSun(startSunAmnt);
    }
    public void AddSun(int amnt)
    {
        SunAmnt += amnt;
        sunDisp.text = "" + SunAmnt;
    }

    public void DeductSun(int amnt) 
    { 
        SunAmnt -= amnt;
        sunDisp.text = "" + SunAmnt;
    }
}
