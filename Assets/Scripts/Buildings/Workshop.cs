using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    public short tierWarsztatu;

    public enum RodzajWarsztatu
    {
        NONE,
        WARSZTAT,
        KUZNIA
    }

    //Budowa/upgrade
    private int pozostaleTuryDoBudowy;
    private short[] timeToEndBuilding = new short[2] { 12, 20 };

    public GameObject warsztatPrefab;
    public GameObject kuzniaPrefab;

    public GameObject workshop;

    public GameManager gM = GameManager.Instance;

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


    /// <summary>
    /// Skrypt do pracy w warsztacie.
    /// Sprawdza czy jest stoneAge == true. 
    /// Ważne!!! Do kolesia który będzie używał ten skrypt ustaw wartosc 
    /// pracownika czyParcuje na false w CharacterCreator po zakonczeniu pracy
    /// </summary>
    /// <param name="pracownik"></param>
    public void PracaWWarsztacie(GameObject pracownik)
    {
        if (gM.stoneAge)
        {
            CharacterCreator worker = pracownik.GetComponent<CharacterCreator>();
            worker.czyPracuje = true;
        }
        else
        {
            Debug.LogWarning("Nie odblokowałeś jeszcze ery kamienia");
        }
    }

    /// <summary>
    /// Skrypt do pracy w kuzni.
    /// Sprawdza czy jest stoneAge == true && ironAge == true. 
    /// Ważne!!! Do kolesia który będzie używał ten skrypt ustaw wartosc 
    /// pracownika czyParcuje na false w CharacterCreator po zakonczeniu pracy
    /// </summary>
    /// <param name="pracownik"></param>
    public void PracaWWkuzni(GameObject pracownik)
    {
        if (gM.stoneAge &&
            gM.ironAge)
        {
            CharacterCreator worker = pracownik.GetComponent<CharacterCreator>();
            worker.czyPracuje = true;
        }
        else
        {
            Debug.LogWarning("Nie odblokowałeś jeszcze ery kamienia lub ironAge");
        }
    }


    public void BudowaWarsztatu()
    {
        if (tierWarsztatu == (short)RodzajWarsztatu.NONE)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierWarsztatu] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                workshop = Instantiate(warsztatPrefab, GetBuildPostion(), transform.rotation);
                tierWarsztatu = (short)RodzajWarsztatu.WARSZTAT;
                pozostaleTuryDoBudowy = 0;
                gM.stoneAge = true;

            }
            else
            {
                pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.Log("Ognisko jest już zbudowane");
        }
    }// budowa ogniska

    public void UlepszenieDoKuzni()
    {
        if (tierWarsztatu == (short)RodzajWarsztatu.KUZNIA)
        {
            if (pozostaleTuryDoBudowy == 0)
            {
                pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierWarsztatu] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (pozostaleTuryDoBudowy == 1)
            {
                Destroy(workshop);
                GameObject kuznia = Instantiate(kuzniaPrefab, GetBuildPostion(), transform.rotation);
                workshop = kuznia;
                tierWarsztatu = (short)RodzajWarsztatu.KUZNIA;
                pozostaleTuryDoBudowy = 0;
                gM.ironAge = true;

            }

            else
            {
                pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Kuznia jest już zbudowana");
        }
    }//Ulepszenie Do Kuchni

}
