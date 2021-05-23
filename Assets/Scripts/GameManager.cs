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
    public int pojemnoscJedzenia = 250;   // default 100
    public int pojemnoscWody = 300;       // default 200  
    public int pojemnoscSurowcow = 100;   // default 300

    //Listy pomocnicze do przenoszenia ludzi z people 
    public List<GameObject> pracownicDoTartaku;
    public List<GameObject> pracownicyDoPomostu;
    public List<GameObject> pracownicyDoOgrodu;
    public List<GameObject> pracownikDoKuchni;

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

    private Sawmill sawmill;
    private FishingPier fishingPier;
    private Garden garden;
    private Campfire campfire;

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
        //ponizsze funkcje przyjmuja List<CharacterController> zamiast List<GameObject>
        WorkManagerSawmill();
        WorkManageKitchen();
        WorkManagerFishingPier();
        WorkManagerGarden();
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

    public void SprawdzPojemnoscMaxSurowcow()
    {
        //Sprawdza warunek dla wody
        if (woda > pojemnoscWody)
        {
            woda = pojemnoscWody;
        }

        //Sprawdza warunek dla jedzenia
        if (jedzenie > pojemnoscJedzenia)
        {
            jedzenie = pojemnoscJedzenia;
        }

        //Sprawdza warunek dla kamiena
        if (kamien > pojemnoscSurowcow)
        {
            kamien = pojemnoscSurowcow;
        }

        //Sprawdza warunek dla drewna
        if (drewno > pojemnoscSurowcow)
        {
            drewno = pojemnoscSurowcow;
        }

        //Sprawdza warunek dla zelaza
        if (zelazo > pojemnoscSurowcow)
        {
            zelazo = pojemnoscSurowcow;
        }
    }//SprawdzPojemnoscMaxSurowcow

    public void WorkManagerSawmill()
    {
        if (sawmill.tierTartaku > 0 &&
            pracownicDoTartaku.Count > 0)
        {
            foreach (var pracownik in pracownicDoTartaku)
            {
                sawmill.characters.Add(pracownik.GetComponent<CharacterCreator>());
            }
            sawmill.PracaWTartaku();
            byte count = 0;
            foreach (var pracownik in pracownicDoTartaku) //zmienna do pracy w tartaku
            {

                if (!sawmill.characters[count].czyPracuje)
                {
                    sawmill.characters.RemoveAt(count);
                    pracownicDoTartaku.RemoveAt(count);
                }
                else
                {
                    count++;
                }
            }
        }
    }//WorkManagerSawmill

    public void WorkManagerFishingPier()
    {
        if (fishingPier.tierPomostRybacki > 0 &&
            pracownicyDoPomostu.Count > 0)
        {
            foreach (var pracownik in pracownicyDoPomostu)
            {
                fishingPier.pracownicy.Add(pracownik.GetComponent<CharacterCreator>());
            }
           fishingPier.PracaWPomoscieRybackim();
            byte count = 0;
            foreach (var pracownik in pracownicyDoPomostu) 
            {
                if (!fishingPier.pracownicy[count].czyPracuje) // szukanie po pracownikach czy pracują
                {
                    fishingPier.pracownicy.RemoveAt(count);
                    pracownicyDoPomostu.RemoveAt(count);
                }
                else
                {
                    count++;
                }
            }
        }
    }//WorkManagerFishingPier

    public void WorkManagerGarden()
    {
        if (garden.tierOgrodu > 0 &&
            pracownicyDoOgrodu.Count > 0)
        {
            foreach (var pracownik in pracownicyDoOgrodu)
            {
                garden.pracownik.Add(pracownik.GetComponent<CharacterCreator>());
            }
            garden.PracaWOgrodzie();
            byte count = 0;
            foreach (var pracownik in pracownicyDoOgrodu)
            {
                if (!garden.pracownik[count].czyPracuje) // szukanie po pracownikach czy pracują
                {
                    garden.pracownik.RemoveAt(count);
                    pracownicyDoOgrodu.RemoveAt(count);
                }
                else
                {
                    count++;
                }
            }
        }
    }//WorkManagerGarden

    public void WorkManageKitchen()
    {
        if (campfire.tierOgniska == (short)Ognisko.RodzajOgniska.KUCHNIA &&
            pracownikDoKuchni.Count == 1)
        {
            foreach (var item in pracownikDoKuchni)
            {
                campfire.pracownik.Add(item);
            }

            campfire.PracaWKuchni();
            byte count = 0;
            foreach (var pracownik in pracownikDoKuchni)
            {
                if (!garden.pracownik[count].czyPracuje) // szukanie po pracownikach czy pracują
                {
                    campfire.pracownik.RemoveAt(count);
                    pracownikDoKuchni.RemoveAt(count);
                }
                else
                {
                    count++;
                }
            }
        }
    }//WorkManagerGarden


}// class
