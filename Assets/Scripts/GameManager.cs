using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{

    [Header("Zmienne")]
    public static int kamien = 0;
    public static int drewno = 0;
    public static int woda = 0;
    public static int dzien = 0;
    public static int jedzenie = 0;
    public static int grupowaStamina = 100;
    //      0 - rano
    //      1 - popoludnie
    //      2 - wieczor
    //      3 - noc    
    public static int pora_dnia = 0;
    public GameObject[] ludzie;

    // Start is called before the first frame update
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

    public void Zmiana_jedzenia(int jedzenie_update)
    {
        int result = jedzenie + jedzenie_update;
        if (result >= 0)
        {
            jedzenie = result;
        }
    }

    public void Zmiana_dnia()
    {
        dzien++;
    }



}
