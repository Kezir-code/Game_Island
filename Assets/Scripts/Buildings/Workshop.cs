using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workshop : MonoBehaviour
{
    public short tierWarsztatu;

    public List<GameObject> pracownik;

    //Budowa/upgrade
    private int pozostaleTuryDoBudowy;

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
    public void PracaWWarsztacie()
    {
        if (gM.stoneAge)
        {
            foreach (var item in pracownik)
            {
                CharacterCreator worker = item.GetComponent<CharacterCreator>();
                worker.czyPracuje = true;
                gM.stamina -= (short)Warsztat.KosztDzialania.WARSZTAT;
            }
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
    public void PracaWWkuzni()
    {
        if (gM.stoneAge &&
            gM.ironAge)
        {
            foreach (var item in pracownik)
            {
                CharacterCreator worker = item.GetComponent<CharacterCreator>();
                worker.czyPracuje = true;
                gM.stamina -= (short)Warsztat.KosztDzialania.KUZNIA;
            }
            
        }
        else
        {
            Debug.LogWarning("Nie odblokowałeś jeszcze ery kamienia lub ironAge");
        }
    }

    #region Budowa, ulepszenie i sprzedaz



    public void BudowaWarsztatu()
    {
        if (tierWarsztatu == (short)Warsztat.RodzajWarsztatu.NONE)
        {
            if (Warsztat.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)Warsztat.BudowaWarsztat.DREWNO &&
                    gM.kamien < (short)Warsztat.BudowaWarsztat.KAMIEN &&
                    gM.zelazo < (short)Warsztat.BudowaWarsztat.ZELAZO)
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)Warsztat.BudowaWarsztat.DREWNO;
                gM.kamien -= (short)Warsztat.BudowaWarsztat.KAMIEN;
                gM.zelazo -= (short)Warsztat.BudowaWarsztat.ZELAZO;
                Warsztat.pozostaleTuryDoBudowy = (short)Warsztat.BudowaWarsztat.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
            }

            else if (Warsztat.pozostaleTuryDoBudowy == 1)
            {
                workshop = Instantiate(warsztatPrefab, GetBuildPostion(), transform.rotation);
                tierWarsztatu = (short)Warsztat.RodzajWarsztatu.WARSZTAT;
                Warsztat.pozostaleTuryDoBudowy = 0;
                gM.stoneAge = true;

            }
            else
            {
                Warsztat.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.Log("Ognisko jest już zbudowane");
        }
    }// budowa warsztatu


    public void UlepszenieDoKuzni()
    {
        if (tierWarsztatu == (short)Warsztat.RodzajWarsztatu.WARSZTAT)
        {
            if (Warsztat.pozostaleTuryDoBudowy == 0)
            {
                if (gM.drewno < (short)Warsztat.UpgradeKuznia.DREWNO &&
                    gM.kamien < (short)Warsztat.UpgradeKuznia.KAMIEN &&
                    gM.zelazo < (short)Warsztat.UpgradeKuznia.ZELAZO)
                {
                    Debug.Log("Nie masz wystarczająco surowców");
                    return;
                }

                gM.drewno -= (short)Warsztat.UpgradeKuznia.DREWNO;
                gM.kamien -= (short)Warsztat.UpgradeKuznia.KAMIEN;
                gM.zelazo -= (short)Warsztat.UpgradeKuznia.ZELAZO;
                Warsztat.pozostaleTuryDoBudowy = (short)(short)Warsztat.UpgradeKuznia.CZAS_BUDOWY; 
            }

            else if (Warsztat.pozostaleTuryDoBudowy == 1)
            {
                Destroy(workshop);
                GameObject kuznia = Instantiate(kuzniaPrefab, GetBuildPostion(), transform.rotation);
                workshop = kuznia;
                tierWarsztatu = (short)Warsztat.RodzajWarsztatu.KUZNIA;
                Warsztat.pozostaleTuryDoBudowy = 0;
                gM.ironAge = true;

            }

            else
            {
                Warsztat.pozostaleTuryDoBudowy--;
            }
        }

        else
        {
            Debug.LogWarning("Kuznia jest już zbudowana");
        }
    }//Ulepszenie Do kuzni

    public void SprzedajBudynek()
    {
        switch (tierWarsztatu)
        {
            case (short)Warsztat.RodzajWarsztatu.WARSZTAT:

                Destroy(workshop);
                gM.drewno -= (short)Warsztat.SprzedazWarsztat.DREWNO;
                gM.kamien -= (short)Warsztat.SprzedazWarsztat.KAMIEN;
                gM.zelazo -= (short)Warsztat.SprzedazWarsztat.ZELAZO;

                break;

            case (short)Warsztat.RodzajWarsztatu.KUZNIA:

                Destroy(workshop);
                gM.drewno -= (short)Warsztat.SprzedazKuznia.DREWNO;
                gM.kamien -= (short)Warsztat.SprzedazKuznia.KAMIEN;
                gM.zelazo -= (short)Warsztat.SprzedazKuznia.ZELAZO;

                break;

            default:
                Debug.LogWarning("Budynek już nie istnieje lub tier się nie zgadza");
                break;

        }


    }//SprzedajBudynek

    #endregion
}
