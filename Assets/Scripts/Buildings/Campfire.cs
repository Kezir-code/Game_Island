using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : MonoBehaviour
{
    #region Zmienne

    // praca
    //public short[] kosztDziałania = new short[3] { 1, 1, 5};
    //public short iloscKamieniaDoPieca = 40;
    //public short iloscZelaza = 8;
    //public short czasPracyKuznia = 4;
    //private short[] timeToEndBuilding = new short[2] { 1, 8};
    //public static short pozostaleTuryDoBudowy = 0;

    
    /*
    public enum RodzajOgniska
    {
        NONE,       //0
        OGNISKO,    //1
        KUCHNIA,    //2
        PIEC        //3
    }
    */
    public short tierOgniska = 0;
    //praca
    private bool kamieninSkonsumowany;
    private short iloscTurDoKoncaPracyPieca;
    // upgrade
    public GameObject ogniskoPrefab;    //1
    public GameObject kuchniaPrefab;    //2
    public GameObject piecPrefab;       //3

    public GameObject ognisko;

    public GameManager gM = GameManager.Instance;
    #endregion

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

    #region Praca w Ognisku

    public void PracaWOgnisko()
    {
        switch (tierOgniska)
        {
            case (short)Ognisko.RodzajOgniska.OGNISKO:
                if (gM.drewno >= (short)Ognisko.KosztDzialnia.OGNISKO) // zmiana tury
                {
                    gM.drewno--;
                    //gM.ogniskoBonus = true;
                    Ognisko.ogniskoBonus = true;
                }
                else
                {
                    Ognisko.ogniskoBonus = false;
                }
                break;
         
            case (short)Ognisko.RodzajOgniska.PIEC:
                if (gM.drewno >= (short)Ognisko.KosztDzialnia.PIEC)
                {
                    if (kamieninSkonsumowany == false)
                    {
                        gM.kamien -= Ognisko.iloscKamieniaDoPieca;
                        iloscTurDoKoncaPracyPieca = Ognisko.czasPracyKuznia;
                        kamieninSkonsumowany = true;
                    }

                    else
                    {
                        iloscTurDoKoncaPracyPieca--;
                    }

                    if (iloscTurDoKoncaPracyPieca == 0)
                    {
                        gM.zelazo += Ognisko.iloscZelaza;
                        kamieninSkonsumowany = false;
                    }

                }
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Skrypt do kuchni.
    /// Ważne!!! Do kolesia który będzie używał ten skrypt ustaw wartosc 
    /// pracownika czyParcuje na false w CharacterCreator po zakonczeniu pracy
    /// </summary>
    /// <param name="pracownik">
    /// Pracownik jest wymagany do kuchni. Zmiena wartosc CharacterCreator.czyPracuje na true.
    /// </param>
    public void PracaWKuchni(GameObject pracownik)
    {
        if (tierOgniska == (short)Ognisko.RodzajOgniska.KUCHNIA)
        {
            CharacterCreator worker = pracownik.GetComponent<CharacterCreator>();
            
            if (gM.drewno >= (short)Ognisko.KosztDzialnia.KUCHNIA) // zmiana tury
            {
                if (worker.tagPracy != Ognisko.tagPracy)
                {
                    worker.czyPracuje = true;
                    gM.drewno--;
                    Ognisko.kuchniaBonus = true;
                    worker.tagPracy = Ognisko.tagPracy;
                }
                else
                {
                    worker.czyPracuje = true;
                    gM.drewno--;
                    Ognisko.kuchniaBonus = true;
                }
            }
            else
            {
                worker.czyPracuje = false;
                Ognisko.kuchniaBonus = false;
                worker.tagPracy = "NIE PRACUJE";
            }
        }

        else
        {
            Debug.LogWarning("Skrypt jest tylko do kuchni. Tier się nie zgadza");
        }
                
    }// Praca W kuchni
    #endregion


    public void BudowaOgniska()
    {
        if (tierOgniska == (short)Ognisko.RodzajOgniska.NONE)
        {
            if (Ognisko.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)Ognisko.BudowaOgnisko.DREWNO &&
                    gM.kamien < (short)Ognisko.BudowaOgnisko.KAMIEN &&
                    gM.zelazo < (short)Ognisko.BudowaOgnisko.ZELAZO)
                {
                    // Dla ludzi tworzących UI zrobić powiadomienie 
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)Ognisko.BudowaOgnisko.DREWNO;
                gM.drewno -= (short)Ognisko.BudowaOgnisko.KAMIEN;
                gM.drewno -= (short)Ognisko.BudowaOgnisko.ZELAZO;
                Ognisko.pozostaleTuryDoBudowy = (short)Ognisko.BudowaOgnisko.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (Ognisko.pozostaleTuryDoBudowy == 1)
            {
                ognisko = Instantiate(ogniskoPrefab, GetBuildPostion(), transform.rotation);
                tierOgniska = (short)Ognisko.RodzajOgniska.OGNISKO;
                Ognisko.pozostaleTuryDoBudowy = 0;
            }
            else
            {
                Ognisko.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.Log("Ognisko jest już zbudowane");
        }
    }// budowa ogniska

    public void UlepszenieDoKuchni()
    {
        if (tierOgniska == (short)Ognisko.RodzajOgniska.OGNISKO)
        {
            if (Ognisko.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)Ognisko.UpgradeKuchnia.DREWNO &&
                    gM.kamien < (short)Ognisko.UpgradeKuchnia.KAMIEN &&
                    gM.zelazo < (short)Ognisko.UpgradeKuchnia.ZELAZO)
                {
                    // Dla ludzi tworzących UI zrobić powiadomienie 
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)Ognisko.UpgradeKuchnia.DREWNO;
                gM.drewno -= (short)Ognisko.UpgradeKuchnia.KAMIEN;
                gM.drewno -= (short)Ognisko.UpgradeKuchnia.ZELAZO;
                Ognisko.pozostaleTuryDoBudowy = (short)Ognisko.UpgradeKuchnia.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (Ognisko.pozostaleTuryDoBudowy == 1)
            {
                Destroy(ognisko);
                GameObject campfireTier2 = Instantiate(kuchniaPrefab, GetBuildPostion(), transform.rotation);
                ognisko = campfireTier2;
                tierOgniska = (short)Ognisko.RodzajOgniska.KUCHNIA;
                Ognisko.pozostaleTuryDoBudowy = 0;
            }

            else
            {
                Ognisko.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.Log("Kuchnia jest już zbudowana lub nie ma ogniska");
        }
    }//Ulepszenie Do Kuchni

    public void UlepszenieDoPieca()
    {
        if (tierOgniska == (short)Ognisko.RodzajOgniska.OGNISKO)
        {
            if (Ognisko.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)Ognisko.UpgradePiec.DREWNO &&
                    gM.kamien < (short)Ognisko.UpgradePiec.KAMIEN &&
                    gM.zelazo < (short)Ognisko.UpgradePiec.ZELAZO)
                {
                    // Dla ludzi tworzących UI zrobić powiadomienie 
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)Ognisko.UpgradePiec.DREWNO;
                gM.drewno -= (short)Ognisko.UpgradePiec.KAMIEN;
                gM.drewno -= (short)Ognisko.UpgradePiec.ZELAZO;
                Ognisko.pozostaleTuryDoBudowy = (short)Ognisko.UpgradePiec.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (Ognisko.pozostaleTuryDoBudowy == 1)
            {
                Destroy(ognisko);
                GameObject campfireTier3 = Instantiate(kuchniaPrefab, GetBuildPostion(), transform.rotation);
                ognisko = campfireTier3;
                tierOgniska = (short)Ognisko.RodzajOgniska.PIEC;
                Ognisko.pozostaleTuryDoBudowy = 0;
            }
            else
            {
                Ognisko.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.Log("Piec jest już zbudowany lub nie ma ogniska");
        }
    }//Ulepszenie Do Pieca

    public void SprzedajBudynek()
    {
        switch (tierOgniska)
        {
            case (short)Ognisko.RodzajOgniska.OGNISKO:

                Destroy(ognisko);
                gM.drewno -= (short)Ognisko.SprzedazOgnisko.DREWNO;
                gM.kamien -= (short)Ognisko.SprzedazOgnisko.KAMIEN;
                gM.zelazo -= (short)Ognisko.SprzedazOgnisko.ZELAZO;

                break;

            case (short)Ognisko.RodzajOgniska.KUCHNIA:

                Destroy(ognisko);
                gM.drewno -= (short)Ognisko.SprzedazKuchnia.DREWNO;
                gM.kamien -= (short)Ognisko.SprzedazKuchnia.KAMIEN;
                gM.zelazo -= (short)Ognisko.SprzedazKuchnia.ZELAZO;

                break;

            case (short)Ognisko.RodzajOgniska.PIEC:

                Destroy(ognisko);
                gM.drewno -= (short)Ognisko.SprzedazPiec.DREWNO;
                gM.kamien -= (short)Ognisko.SprzedazPiec.KAMIEN;
                gM.zelazo -= (short)Ognisko.SprzedazPiec.ZELAZO;

                break;
            default:
                Debug.LogWarning("Budynek już nie istnieje lub tier się nie zgadza");
                break;

        }
    }//SprzedajBudynek

}//class
