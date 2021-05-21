using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    

    /*
    public enum RodzajOgrodu
    {
        NONE,
        OGROD,
        PLANTRACJA,
    }
    */
    

    //praca
    //private short[] getSurowce = new short[2] { 40, 90 };
    //private short czasPracy = 12;

  
    //Budowa/upgrade
    //rivate short[,] costUpgrade = new short[2, 3] { { 20, 0, 0 }, { 80, 30, 12 } };
    //private short[] timeToEndBuilding = new short[2] { 2, 12 };
    //public static short pozostaleTuryDoBudowy = 0;
    //private int minusStamina = 2;

    public short tierOgrodu;
    public GameManager gM = GameManager.Instance;
    private short czasPracyDoKonca;
    public bool dostalemJedzenie;

    public GameObject ogrodPrefab;
    public GameObject plantacjaPrefab;

    public GameObject garden;
    

    #region Metody pomocnicze

    /// <summary>
    /// Metoda do zrwracania pozycji 
    /// Jeżeli budynek bedzie potrzebował dodania to pozycji wartości 
    /// </summary>
    /// <returns>Zwraca pozycje</returns>
    public Vector3 GetBuildPostion()
    {
        return transform.position;
    }

    //na razie nie używana 
    public Quaternion ObrotBudynku()
    {
        return transform.rotation = Quaternion.identity;

    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PracaWOgrodzie(GameObject pracownik)
    {
        CharacterCreator worker = pracownik.GetComponent<CharacterCreator>();
        if (!dostalemJedzenie || worker.tagPracy != Ogrod.tagPracy)
        {
            switch (tierOgrodu)
            {
                case (short)Ogrod.RodzajOgrodu.OGROD:
                    if (gM.woda >= (short)Ogrod.KosztDzialania.WODA_OGROD)
                    {
                        worker.czyPracuje = true;
                        gM.woda -= (short)Ogrod.KosztDzialania.WODA_OGROD;
                        gM.stamina -= (short)Ogrod.KosztDzialania.STAMINA_OGROD;
                        czasPracyDoKonca = (short)Ogrod.Praca.CZAS_PRACY_OGROD;
                        dostalemJedzenie = true;
                        worker.tagPracy = Ogrod.tagPracy;
                    }
                    break;
                case (short)Ogrod.RodzajOgrodu.PLANTACJA:
                    if (gM.woda >= (short)Ogrod.KosztDzialania.WODA_PLANTACJA)
                    {
                        worker.czyPracuje = true;
                        gM.woda -= (short)Ogrod.KosztDzialania.WODA_PLANTACJA;
                        gM.stamina -= (short)Ogrod.KosztDzialania.STAMIN_PLANTACJA;
                        czasPracyDoKonca = (short)Ogrod.Praca.CZAS_PRACY_PLANTACJA;
                        dostalemJedzenie = true;
                        worker.tagPracy = Ogrod.tagPracy;
                    }
                    break;
                default:
                    break;
            }
        }

        else
        {
            switch (tierOgrodu)
            {
                case (short)Ogrod.RodzajOgrodu.OGROD:
                    if (gM.pora_dnia != 3 &&
                        gM.stamina >= (short)Ogrod.KosztDzialania.STAMINA_OGROD)
                    {
                        czasPracyDoKonca--;
                        gM.woda -= (short)Ogrod.KosztDzialania.WODA_OGROD;
                        gM.stamina -= (short)Ogrod.KosztDzialania.STAMINA_OGROD;
                    }

                    else if (gM.stamina < (short)Ogrod.KosztDzialania.STAMINA_OGROD)
                    {
                        worker.czyPracuje = false;
                        Debug.Log("Brakuje staminy");
                    }

                    if (czasPracyDoKonca == 0)
                    {
                        if (worker.trait == Trait.ZAWODOWY_OGRODNIK)
                        {
                            gM.jedzenie += (int)((short)Ogrod.Praca.PRODUKCJA_OGROD * Trait.MODYFIKATOR_OGRODNIK_TRAIT);
                        }
                        else
                        {
                            gM.jedzenie += (short)Ogrod.Praca.PRODUKCJA_OGROD;
                        }
                        dostalemJedzenie = false;
                        worker.czyPracuje = false;
                        Debug.Log("Robotnik " + worker.name + " wyprodukował: " + (short)Ogrod.Praca.PRODUKCJA_OGROD);
                        Debug.Log("Poziom drewna wynosi:" + gM.jedzenie);
                    }
                    break;
                case (short)Ogrod.RodzajOgrodu.PLANTACJA:
                    if (gM.pora_dnia != 3 &&
                        gM.stamina >= (short)Ogrod.KosztDzialania.STAMIN_PLANTACJA)
                    {
                        czasPracyDoKonca--;
                        gM.woda -= tierOgrodu;
                        gM.stamina -= (short)Ogrod.KosztDzialania.STAMIN_PLANTACJA;
                    }

                    else if (gM.stamina < (short)Ogrod.KosztDzialania.STAMIN_PLANTACJA)
                    {
                        worker.czyPracuje = false;
                        Debug.Log("Brakuje staminy");
                    }

                    if (czasPracyDoKonca == 0)
                    {
                        if (worker.trait == Trait.ZAWODOWY_OGRODNIK)
                        {
                            gM.jedzenie += (int)((short)Ogrod.Praca.PRODUKCJA_PLANTACJA * Trait.MODYFIKATOR_OGRODNIK_TRAIT);
                        }
                        else
                        {
                            gM.jedzenie += (short)Ogrod.Praca.PRODUKCJA_PLANTACJA;
                        }
                        
                        dostalemJedzenie = false;
                        worker.czyPracuje = false;
                        Debug.Log("Robotnik " + worker.name + " wyprodukował: " + (short)Ogrod.Praca.PRODUKCJA_PLANTACJA);
                        Debug.Log("Poziom drewna wynosi:" + gM.jedzenie);
                    }
                    worker.tagPracy = "Nie pracuje";
                    break;
                default:
                    break;
            }
            
        }
    }//PracaWOgrodzie
    #region Budowa i Upgrade

    public void BudowaOgrodu()
    {
        if (tierOgrodu == (short)Ogrod.RodzajOgrodu.NONE)
        {
            if (Ogrod.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)Ogrod.BudowaOgrodu.DREWNO &&
                    gM.kamien < (short)Ogrod.BudowaOgrodu.KAMIEN &&
                    gM.zelazo < (short)Ogrod.BudowaOgrodu.ZELAZO)
                {
                    // Dla ludzi tworzących UI zrobić powiadomienie 
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }
                gM.drewno -= (short)Ogrod.BudowaOgrodu.DREWNO;
                gM.kamien -= (short)Ogrod.BudowaOgrodu.KAMIEN;
                gM.zelazo -= (short)Ogrod.BudowaOgrodu.ZELAZO;
                Ogrod.pozostaleTuryDoBudowy = (short)Ogrod.BudowaOgrodu.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (Ogrod.pozostaleTuryDoBudowy == 1)
            {
                garden = Instantiate(ogrodPrefab, GetBuildPostion(), transform.rotation);
                tierOgrodu = (short)Ogrod.RodzajOgrodu.OGROD;
                Ogrod.pozostaleTuryDoBudowy = 0;
            }

            else
            {
                Ogrod.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Zbierak na wode jest już zbudowany");
        }

    }//BudowaZbierakaNaWode

    public void UpgradeNaPlantacje()
    {
        if (tierOgrodu == (short)Ogrod.RodzajOgrodu.OGROD)
        {
            if (Ogrod.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)Ogrod.BudowaPlantacji.DREWNO &&
                    gM.kamien < (short)Ogrod.BudowaPlantacji.KAMIEN &&
                    gM.zelazo < (short)Ogrod.BudowaPlantacji.ZELAZO)
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)Ogrod.BudowaPlantacji.DREWNO;
                gM.kamien -= (short)Ogrod.BudowaPlantacji.KAMIEN;
                gM.zelazo -= (short)Ogrod.BudowaPlantacji.ZELAZO;
                Ogrod.pozostaleTuryDoBudowy = (short)Ogrod.BudowaPlantacji.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (Ogrod.pozostaleTuryDoBudowy == 1)
            {
                Destroy(garden);
                GameObject plantacja = Instantiate(plantacjaPrefab, GetBuildPostion(), transform.rotation);
                garden = plantacja;
                tierOgrodu = (short)Ogrod.RodzajOgrodu.PLANTACJA;
                Ogrod.pozostaleTuryDoBudowy = 0;
            }

            else
            {
                Ogrod.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Studnia jest już zbudowana");
        }

    }//UpgradeNaPlantacje

    public void SprzedajBudynek()
    {
        switch (tierOgrodu)
        {
            case (short)Ogrod.RodzajOgrodu.OGROD:

                Destroy(garden);
                gM.drewno -= (short)Ogrod.SprzedazOgrodu.DREWNO;
                gM.kamien -= (short)Ogrod.SprzedazOgrodu.KAMIEN;
                gM.zelazo -= (short)Ogrod.SprzedazOgrodu.ZELAZO;

                break;

            case (short)Ogrod.RodzajOgrodu.PLANTACJA:

                Destroy(garden);
                gM.drewno -= (short)Ogrod.SprzedazPlantacji.DREWNO;
                gM.kamien -= (short)Ogrod.SprzedazPlantacji.KAMIEN;
                gM.zelazo -= (short)Ogrod.SprzedazPlantacji.ZELAZO;

                break;
            default:
                Debug.LogWarning("Budynek już nie istnieje lub tier się nie zgadza");
                break;

        }
    }//SprzedajBudynek

    #endregion

}//class
