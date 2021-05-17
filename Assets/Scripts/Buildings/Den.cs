using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Den : MonoBehaviour
{
    public enum RodzajLegowiska
    {
        NONE,
        LEGOWISKO,
        SYPIALNIA
    }

    public GameManager gM = GameManager.Instance;

    // upgrade
    private short[,] costUpgrade = new short[2, 3] { { 40, 0, 0 }, { 60, 20, 4 } };
    private short[] timeToEndBuilding = new short[2] { 4, 12 };
    public static short pozostaleTuryDoBudowy = 0;

    public GameObject den;

    public GameObject legowiskoPrefab;
    public GameObject sypialniaPrefab;

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

    public void LegowiskoBudowa()
    {
        if (gM.tierLegowskia == (short)RodzajLegowiska.NONE)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < costUpgrade[gM.tierLegowskia, 0])
                {
                    // Dla ludzi tworzących UI zrobić powiadomienie 
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= costUpgrade[gM.tierLegowskia, 0];
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[gM.tierLegowskia] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                den = Instantiate(legowiskoPrefab, GetBuildPostion(), transform.rotation);
                gM.tierLegowskia = (short)RodzajLegowiska.LEGOWISKO;
                pozostaleTuryDoBudowy = 0;
            }
            else
            {
                pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.Log("Legowisko jest już zbudowane");
        }
        
    }// legowisko budowa

    public void SypialniaUpgade()
    {
        if (gM.tierLegowskia == (short)RodzajLegowiska.LEGOWISKO)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < costUpgrade[gM.tierLegowskia, 0] &&
                    gM.kamien < costUpgrade[gM.tierLegowskia, 1] &&
                    gM.zelazo < costUpgrade[gM.tierLegowskia, 2])
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= costUpgrade[gM.tierLegowskia, 0];
                gM.kamien -= costUpgrade[gM.tierLegowskia, 1];
                gM.zelazo -= costUpgrade[gM.tierLegowskia, 2];
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[gM.tierLegowskia] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                Destroy(den);
                GameObject sypialnia = Instantiate(sypialniaPrefab, GetBuildPostion(), transform.rotation);
                den = sypialnia;
                den = Instantiate(legowiskoPrefab, GetBuildPostion(), transform.rotation);
                gM.tierLegowskia = (short)RodzajLegowiska.SYPIALNIA;
                pozostaleTuryDoBudowy = 0;
            }
            else
            {
                pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.Log("Sypialnia jest już zbudowana lub legowisko nie jest wybudowane");
        }

    }


}//class
