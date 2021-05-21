using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warehouse : MonoBehaviour
{
    /*
    public enum TierMagazynu
    {
        NONE,
        TIER_1,
        TIER_2  
    }
    */

    

    /// <summary>
    /// Index 0 - Dodatkowe miejsce dla jedzenia 
    /// Index 1 - Dodatkowe miejsce dla wody 
    /// Index 2 - Dodatkowe miejsce dla surowców innych 
    /// </summary>
    //private short[] plusIloscMiejsca = new short[3] { 50, 100, 200 };

    /// <summary>
    /// Index 0 - Do wybudowania 
    /// </summary>
    //private short[,] costUpgrade = new short[2, 3] { { 10, 0, 0 }, { 35, 20, 0 }};
    //public static short pozostaleTuryDoBudowy = 0;
    //public static short pozostaleTuryDoBudowy = 0;
    //private short[] timeToEndBuilding = new short[3] { 4, 8, 12 };

    public static short tierMagazynu = 0;

    public GameObject prefabWarehouseTier1;

    public GameObject warehouse;

    private GameManager gM = GameManager.Instance;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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



    #region Budowa i sprzedaz Magazynu
    public void BudowaWarehouse()
    {
        if (tierMagazynu == (short)Magazyn.RodzajMagazynu.MAGAZYN)
        {
            if (Magazyn.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)Magazyn.BudowaMagazyn.DREWNO &&
                    gM.kamien < (short)Magazyn.BudowaMagazyn.KAMIEN &&
                    gM.zelazo < (short)Magazyn.BudowaMagazyn.ZELAZO)
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)Magazyn.BudowaMagazyn.DREWNO;
                gM.kamien -= (short)Magazyn.BudowaMagazyn.KAMIEN;
                gM.zelazo -= (short)Magazyn.BudowaMagazyn.ZELAZO;
                Magazyn.pozostaleTuryDoBudowy = (short)(short)Magazyn.BudowaMagazyn.CZAS_BUDOWY;
            }

            else if (Magazyn.pozostaleTuryDoBudowy == 1)
            {
                warehouse = (GameObject)Instantiate(prefabWarehouseTier1, GetBuildPostion(), transform.rotation);

                gM.pojemnoscJedzenia += (short)Magazyn.DodatkoweMiejsce.POJEMNOSC_JEDZENIA;
                gM.pojemnoscWody += (short)Magazyn.DodatkoweMiejsce.POJEMNOSC_WODY;
                gM.pojemnoscSurowcow += (short)Magazyn.DodatkoweMiejsce.POJEMNOSC_SUROWCOW;
                Debug.Log("Wodzu! Magazyn zbudowany!");
            }

            else
            {
                Magazyn.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Magazyn jest już zbudowana");
        }

    }

    public void SprzedajBudynek()
    {
        switch (tierMagazynu)
        {
            case (short)Magazyn.RodzajMagazynu.MAGAZYN:

                Destroy(warehouse);
                gM.drewno -= (short)Magazyn.SprzedazMagazyn.DREWNO;
                gM.kamien -= (short)Magazyn.SprzedazMagazyn.KAMIEN;
                gM.zelazo -= (short)Magazyn.SprzedazMagazyn.ZELAZO;

                break;

            default:
                Debug.LogWarning("Budynek już nie istnieje lub tier się nie zgadza");
                break;

        }


        
    }//SprzedajBudynek

    #endregion

}// class
