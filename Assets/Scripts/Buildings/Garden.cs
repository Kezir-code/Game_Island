using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garden : MonoBehaviour
{
    public short tierOgrodu;

    public enum RodzajOgrodu
    {
        NONE,
        OGROD,
        PLANTRACJA,
    }

    public GameManager gM = GameManager.Instance;

    //praca
    private short[] getSurowce = new short[2] { 40, 90 };
    private short czasPracy = 12;
    private short czasPracyDoKonca;
    public bool dostalemJedzenie;


    //Budowa/upgrade
    private short[,] costUpgrade = new short[2, 3] { { 20, 0, 0 }, { 80, 30, 12 } };
    private short[] timeToEndBuilding = new short[2] { 2, 12 };
    public static short pozostaleTuryDoBudowy = 0;

    public GameObject ogrodPrefab;
    public GameObject plantacjaPrefab;

    public GameObject garden;
    private int minusStamina = 2;

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
        if (!dostalemJedzenie)
        {
            if (gM.woda >= tierOgrodu)
            {
                worker.czyPracuje = true;
                gM.woda -= tierOgrodu;
                gM.stamina -= minusStamina;
                czasPracyDoKonca = czasPracy;
                dostalemJedzenie = true;
            }
        }

        else
        {
            if (gM.pora_dnia != 3 &&
            gM.stamina >= minusStamina)
            {
                czasPracyDoKonca--;
                gM.woda -= tierOgrodu;
                gM.stamina -= minusStamina;
            }

            else if (gM.stamina < minusStamina)
            {
                worker.czyPracuje = false;
                Debug.Log("Brakuje staminy");
            }

            if (czasPracyDoKonca == 0)
            {
                gM.jedzenie += getSurowce[tierOgrodu - 1];
                dostalemJedzenie = false;
                worker.czyPracuje = false;
                Debug.Log("Robotnik " + worker.name + " wyprodukował: " + getSurowce[tierOgrodu - 1]);
                Debug.Log("Poziom drewna wynosi:" + gM.jedzenie);
            }
        }
    }
    #region Budowa i Upgrade

    public void BudowaOgrodu()
    {
        if (tierOgrodu == (short)RodzajOgrodu.NONE)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < costUpgrade[tierOgrodu, 0])
                {
                    // Dla ludzi tworzących UI zrobić powiadomienie 
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= costUpgrade[tierOgrodu, 0];
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierOgrodu] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                garden = Instantiate(ogrodPrefab, GetBuildPostion(), transform.rotation);
                tierOgrodu = (short)RodzajOgrodu.OGROD;
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

    public void UpgradeNaPlantacje()
    {
        if (tierOgrodu == (short)RodzajOgrodu.OGROD)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < costUpgrade[tierOgrodu, 0] &&
                    gM.kamien < costUpgrade[tierOgrodu, 1] &&
                    gM.zelazo < costUpgrade[tierOgrodu, 2])
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= costUpgrade[tierOgrodu, 0];
                gM.kamien -= costUpgrade[tierOgrodu, 1];
                gM.zelazo -= costUpgrade[tierOgrodu, 2];
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierOgrodu] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                garden = Instantiate(plantacjaPrefab, GetBuildPostion(), transform.rotation);
                tierOgrodu = (short)RodzajOgrodu.PLANTRACJA;
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

    }//UpgradeNaPlantacje

    #endregion

}//class
