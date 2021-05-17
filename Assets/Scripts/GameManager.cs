using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private GameObject activeScreen;
    private Camera camera;
    public List<GameObject> people;
    public int drewno;
    public int woda;
    public int dzien;
    public int pora_dnia;
    public int stamina;
    public int kamien;
    public int jedzenie;
    public int zelazo;

    // Pojemność magazynu
    public int pojemnoscJedzenia = 100;   // default 100
    public int pojemnoscWody = 200;       // default 200  
    public int pojemnoscSurowcow = 300;   // default 300

    //Przenisono do BuildManager
    //bonusy z ogniska itp.
    //public bool ogniskoBonus;
    //public bool kuchniaBonus;

    //Legowsiko
    public int tierLegowskia;
    public int dodatkowaStamina = 1;


    //kuznia/warsztat
    public bool stoneAge;
    public bool ironAge;


    void Start()
    {
        camera = CameraMovement.Instance.GetComponent<Camera>();
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

    public void ZliczStamine()
    {
        foreach(GameObject kalski in people)
        {
            CharacterCreator character = kalski.GetComponent<CharacterCreator>();
            if (!character.czyPracuje)
            {
                if (tierLegowskia == 0 && Trait.ZAWODOWY_LENIUCH == character.trait)
                {

                    stamina = (int)(dodatkowaStamina * Trait.MODYFIKATOR_LENIUCH_TRAIT);
                }
                else if (tierLegowskia == 0)
                {
                    stamina += dodatkowaStamina;
                }

                else if (tierLegowskia != 0 && Trait.ZAWODOWY_LENIUCH == character.trait)
                {
                    stamina = (int)((tierLegowskia + 1) * Trait.MODYFIKATOR_LENIUCH_TRAIT);
                }
            }
        }
    } // zlicz stamine
}// class
