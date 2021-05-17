using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollector : MonoBehaviour
{
    /*
    public enum RodzajZbierakaNaWode
    {
        NONE,
        ZBIERAKNAWODE,
        STUDNIA,
        ZBIORNIK
    }
    */

    //praca
    //private short[] getSurowce = new short[3] { 15, 80, 160 };
    //private short czasPracy = 4;
    

    //Budowa/upgrade
    //private short[,] costUpgrade = new short[3, 3] { { 10, 0, 0 }, { 20, 40, 0 }, { 80, 80, 24 } };
    //private short[] timeToEndBuilding = new short[3] { 4, 8, 12 };
    //public static short pozostaleTuryDoBudowy = 0;
    public short tierZbierakaNaWode;

    private short czasPracyDoKonca;
    public bool dostalemWode;

    public GameManager gM = GameManager.Instance;

    public GameObject zbierakNaWodePrefab;
    public GameObject studniaPrefab;
    public GameObject zbiornikPrefab;

    public GameObject watterCollector;


    

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


    public void PracaWZbierakuNaWode()
    {
        switch (tierZbierakaNaWode)
        {
            case (short)ZbierakNaWode.RodzajZbierakaNaWode.ZBIERAK_NA_WODE:
                if (dostalemWode == false)
                {
                    czasPracyDoKonca = (short)ZbierakNaWode.CzasPracy.ZBIERAK_NA_WODE;
                }

                else if (czasPracyDoKonca == 0)
                {
                    gM.woda += (short)ZbierakNaWode.DostanSurowce.WODA_Z_ZBIERAKA_NA_WODE;
                }

                else
                {
                    czasPracyDoKonca--;
                }
                break;
            case (short)ZbierakNaWode.RodzajZbierakaNaWode.STUDNIA:
                if (dostalemWode == false)
                {
                    czasPracyDoKonca = (short)ZbierakNaWode.CzasPracy.STUDNIA;
                }

                else if (czasPracyDoKonca == 0)
                {
                    gM.woda += (short)ZbierakNaWode.DostanSurowce.WODA_Z_STUDNI;
                }

                else
                {
                    czasPracyDoKonca--;
                }
                break;
            case (short)ZbierakNaWode.RodzajZbierakaNaWode.ZBIORNIK:
                if (dostalemWode == false)
                {
                    czasPracyDoKonca = (short)ZbierakNaWode.CzasPracy.ZBIORNIK;
                }

                else if (czasPracyDoKonca == 0)
                {
                    gM.woda += (short)ZbierakNaWode.DostanSurowce.WODA_Z_ZBIORNIKA;
                }

                else
                {
                    czasPracyDoKonca--;
                }
                break;
            default:
                break;
        }

        
    }//praca W Zbieraku Na wode

    #region Budowa i Upgrade

    public void BudowaZbierakaNaWode()
    {
        if (tierZbierakaNaWode == (short)ZbierakNaWode.RodzajZbierakaNaWode.NONE)
        {
            if (ZbierakNaWode.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)ZbierakNaWode.BudowaZbierakaNaWode.DREWNO)
                {
                    // Dla ludzi tworzących UI zrobić powiadomienie 
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)ZbierakNaWode.BudowaZbierakaNaWode.DREWNO;
                ZbierakNaWode.pozostaleTuryDoBudowy = (short)ZbierakNaWode.BudowaZbierakaNaWode.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (ZbierakNaWode.pozostaleTuryDoBudowy == 1)
            {
                watterCollector = Instantiate(zbierakNaWodePrefab, GetBuildPostion(), transform.rotation);
                tierZbierakaNaWode = (short)ZbierakNaWode.RodzajZbierakaNaWode.ZBIERAK_NA_WODE;
                ZbierakNaWode.pozostaleTuryDoBudowy = 0;
            }

            else
            {
                ZbierakNaWode.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Zbierak na wode jest już zbudowany");
        }

    }//BudowaZbierakaNaWode

    public void UpgradeNaStudnie()
    {
        if (tierZbierakaNaWode == (short)ZbierakNaWode.RodzajZbierakaNaWode.ZBIERAK_NA_WODE)
        {
            if (ZbierakNaWode.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)ZbierakNaWode.UpgradeStudnia.DREWNO &&
                    gM.kamien < (short)ZbierakNaWode.UpgradeStudnia.KAMIEN)
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)ZbierakNaWode.UpgradeStudnia.DREWNO;
                gM.kamien -= (short)ZbierakNaWode.UpgradeStudnia.KAMIEN;
                ZbierakNaWode.pozostaleTuryDoBudowy = (short)ZbierakNaWode.UpgradeStudnia.CZAS_BUDOWY; 
            }

            else if (ZbierakNaWode.pozostaleTuryDoBudowy == 1)
            {
                watterCollector = Instantiate(studniaPrefab, GetBuildPostion(), transform.rotation);
                tierZbierakaNaWode = (short)ZbierakNaWode.RodzajZbierakaNaWode.STUDNIA;
                ZbierakNaWode.pozostaleTuryDoBudowy = 0;
            }

            else
            {
                ZbierakNaWode.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Studnia jest już zbudowana");
        }

    }//UpgradeNaStudnie

    public void UpgradeNaZbironik()
    {
        if (tierZbierakaNaWode == (short)ZbierakNaWode.RodzajZbierakaNaWode.STUDNIA)
        {
            if (ZbierakNaWode.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)ZbierakNaWode.UpgradeZbiornik.DREWNO &&
                    gM.kamien < (short)ZbierakNaWode.UpgradeZbiornik.KAMIEN &&
                    gM.zelazo < (short)ZbierakNaWode.UpgradeZbiornik.ZELAZO)
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)ZbierakNaWode.UpgradeZbiornik.DREWNO;
                gM.kamien -= (short)ZbierakNaWode.UpgradeZbiornik.KAMIEN;
                gM.zelazo -= (short)ZbierakNaWode.UpgradeZbiornik.ZELAZO;
                ZbierakNaWode.pozostaleTuryDoBudowy = (short)ZbierakNaWode.UpgradeZbiornik.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (ZbierakNaWode.pozostaleTuryDoBudowy == 1)
            {
                watterCollector = Instantiate(zbierakNaWodePrefab, GetBuildPostion(), transform.rotation);
                tierZbierakaNaWode = (short)ZbierakNaWode.RodzajZbierakaNaWode.ZBIORNIK;
                ZbierakNaWode.pozostaleTuryDoBudowy = 0;
            }

            else
            {
                ZbierakNaWode.pozostaleTuryDoBudowy--;
            }
        }
   

        else
        {
            Debug.LogWarning("Zbiornik jest już zbudowany");
        }

    }// UpgradeNaZbironik

    #endregion

}//class
