using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollector : MonoBehaviour
{
    public short tierZbierakaNaWode;

    public enum RodzajZbierakaNaWode
    {
        NONE,
        ZBIERAKNAWODE,
        STUDNIA,
        ZBIORNIK
    }

    public GameManager gM = GameManager.Instance;

    //praca
    private short[] getSurowce = new short[3] { 15, 80, 160 };
    private short czasPracy = 4;
    private short czasPracyDoKonca;
    public bool dostalemWode;

    //Budowa/upgrade
    private short[,] costUpgrade = new short[3, 3] { { 10, 0, 0 }, { 20, 40, 0 }, { 80, 80, 24 } };
    private short[] timeToEndBuilding = new short[3] { 4, 8, 12 };
    public static short pozostaleTuryDoBudowy = 0;

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
        if (tierZbierakaNaWode == (short)RodzajZbierakaNaWode.ZBIORNIK ||
            tierZbierakaNaWode == (short)RodzajZbierakaNaWode.ZBIERAKNAWODE ||
            tierZbierakaNaWode == (short)RodzajZbierakaNaWode.STUDNIA)
        {
            if (dostalemWode == false)
            {
                czasPracyDoKonca = czasPracy;
            }

            else if (czasPracyDoKonca == 0)
            {
                gM.woda += getSurowce[tierZbierakaNaWode - 1];
            }

            else
            {
                czasPracyDoKonca--;
            }
        }
    }//praca W Zbieraku Na wode

    #region Budowa i Upgrade

    public void BudowaZbierakaNaWode()
    {
        if (tierZbierakaNaWode == (short)RodzajZbierakaNaWode.NONE)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < costUpgrade[tierZbierakaNaWode, 0])
                {
                    // Dla ludzi tworzących UI zrobić powiadomienie 
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= costUpgrade[tierZbierakaNaWode, 0];
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierZbierakaNaWode] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                watterCollector = Instantiate(zbierakNaWodePrefab, GetBuildPostion(), transform.rotation);
                tierZbierakaNaWode = (short)RodzajZbierakaNaWode.ZBIERAKNAWODE;
                pozostaleTuryDoBudowy = 0;
            }

            else
            {
                pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Zbierak na wode jest już zbudowany");
        }

    }//BudowaZbierakaNaWode

    public void UpgradeNaStudnie()
    {
        if (tierZbierakaNaWode == (short)RodzajZbierakaNaWode.ZBIERAKNAWODE)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < costUpgrade[tierZbierakaNaWode, 0] &&
                    gM.kamien < costUpgrade[tierZbierakaNaWode, 1])
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= costUpgrade[tierZbierakaNaWode, 0];
                gM.kamien -= costUpgrade[tierZbierakaNaWode, 1];
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierZbierakaNaWode] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                watterCollector = Instantiate(studniaPrefab, GetBuildPostion(), transform.rotation);
                tierZbierakaNaWode = (short)RodzajZbierakaNaWode.STUDNIA;
                pozostaleTuryDoBudowy = 0;
            }

            else
            {
                pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Studnia jest już zbudowana");
        }

    }//UpgradeNaStudnie

    public void UpgradeNaZbironik()
    {
        if (tierZbierakaNaWode == (short)RodzajZbierakaNaWode.STUDNIA)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < costUpgrade[tierZbierakaNaWode, 0] &&
                    gM.kamien < costUpgrade[tierZbierakaNaWode, 1] &&
                    gM.zelazo < costUpgrade[tierZbierakaNaWode, 2])
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= costUpgrade[tierZbierakaNaWode, 0];
                gM.kamien -= costUpgrade[tierZbierakaNaWode, 1];
                gM.zelazo -= costUpgrade[tierZbierakaNaWode, 2];
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierZbierakaNaWode] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                watterCollector = Instantiate(zbierakNaWodePrefab, GetBuildPostion(), transform.rotation);
                tierZbierakaNaWode = (short)RodzajZbierakaNaWode.ZBIORNIK;
                pozostaleTuryDoBudowy = 0;
            }

            else
            {
                pozostaleTuryDoBudowy--;
            }
        }
   

        else
        {
            Debug.LogWarning("Zbiornik jest już zbudowany");
        }

    }// UpgradeNaZbironik

    #endregion

}//class
