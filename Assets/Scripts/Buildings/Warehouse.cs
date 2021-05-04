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

    public static short tierMagazynu = 0;

    /// <summary>
    /// Index 0 - Dodatkowe miejsce dla jedzenia 
    /// Index 1 - Dodatkowe miejsce dla wody 
    /// Index 2 - Dodatkowe miejsce dla surowców innych 
    /// </summary>
    private short[] plusIloscMiejsca = new short[3] { 50, 100, 200 };

    /// <summary>
    /// Index 0 - Do wybudowania 
    /// </summary>
    private short[,] costUpgrade = new short[2, 3] { { 10, 0, 0 }, { 35, 20, 0 }};

    public GameObject prefabWarehouseTier1;
    public GameObject prefabWarehouseTier2;

    public GameObject warehouse;



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

    #region SprawdzaniePojemnosci

    public void SprawdzPojemnoscMaxSurowcow()
    {
        //Sprawdza warunek dla wody
        if (GameManager.Instance.woda > GameManager.Instance.pojemnoscWody)
        {
            GameManager.Instance.woda = GameManager.Instance.pojemnoscWody;
        }

        //Sprawdza warunek dla jedzenia
        if (GameManager.Instance.jedzenie > GameManager.Instance.pojemnoscJedzenia)
        {
            GameManager.Instance.jedzenie = GameManager.Instance.pojemnoscJedzenia;
        }

        //Sprawdza warunek dla kamiena
        if (GameManager.Instance.kamien > GameManager.Instance.pojemnoscSurowcow)
        {                                                      
            GameManager.Instance.kamien = GameManager.Instance.pojemnoscSurowcow;
        }

        //Sprawdza warunek dla drewna
        if (GameManager.Instance.drewno > GameManager.Instance.pojemnoscSurowcow)
        {                                                      
            GameManager.Instance.drewno = GameManager.Instance.pojemnoscSurowcow;
        }

        //Sprawdza warunek dla zelaza
        if (GameManager.Instance.zelazo > GameManager.Instance.pojemnoscSurowcow)
        {
            GameManager.Instance.zelazo = GameManager.Instance.pojemnoscSurowcow;
        }
    }
    #endregion

    #region Budowa i ulepszenie Magazynu
    public void BuildWarehouse()
    {
        //if (GameManager.Instance. < costUpgrade[0, 0] &&
        //    GameManager.Instance. < costUpgrade[0, 1])
        //{
        //    // Dla ludzi tworzących UI zrobić powiadomienie 
        //    Debug.Log("Nie masz wystarczająco surowców");
        //    return;
        //}
        //
        //GameManager.Instance. -= costUpgrade[0, 0];
        //GameManager.Instance. -= costUpgrade[0, 1];

        warehouse = (GameObject)Instantiate(prefabWarehouseTier1, GetBuildPostion(), transform.rotation);

        GameManager.Instance.pojemnoscJedzenia += plusIloscMiejsca[0];
        GameManager.Instance.pojemnoscWody += plusIloscMiejsca[1];
        GameManager.Instance.pojemnoscSurowcow += plusIloscMiejsca[2];
        Debug.Log("Wodzu! Magazyn zbudowany!");
    }

    public void WarehouseUpgrade()
    {
        Destroy(warehouse);
        GameObject _warehouse = (GameObject)Instantiate(prefabWarehouseTier1, GetBuildPostion(), transform.rotation);
        warehouse = _warehouse;


        GameManager.Instance.pojemnoscJedzenia += plusIloscMiejsca[0];
        GameManager.Instance.pojemnoscWody += plusIloscMiejsca[1];
        GameManager.Instance.pojemnoscSurowcow += plusIloscMiejsca[2];
        Debug.Log("Wodzu! Magazyn ulepszony!");
    }
    #endregion


}// class
