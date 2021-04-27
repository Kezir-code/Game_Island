using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    [Header("Zmienne")]
    public static int kamien;
    public static int drewno;
    public static int woda;
    public static int dzien = 0;
    public GameObject Pauza;
    //    0 - rano
    //    1 - popoludnie
    //    2 - wieczor
    public int pora_dnia = 0;
    public GameObject[] ludzie;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Zmiana_Pory_Dnia()
    {
        pora_dnia = (pora_dnia + 1) % 3;
    }

    public void Zmiana_kamien(int kamien_update)
    {
        int result = kamien + kamien_update;
        if (result >= 0)
        {
            kamien = result;
        }
    }

    public void Zmiana_wody(int woda_update)
    {
        int result = woda + woda_update;
        if (result >= 0)
        {
            woda = result;
        }
    }
    public void Zmiana_drewno(int drewno_update)
    {
        int result = drewno + drewno_update;
        if (result >= 0)
        {
            drewno = result;
        }
    }
    public void Zmiana_dnia()
    {
        dzien++;
    }
   
    

}
