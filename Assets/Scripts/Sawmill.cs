using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : MonoBehaviour 
{
	//	- Wymaga pracownika
	//	- Generuje 5/20/35 (na dzień 4 tury)

	public GameManager gameManager;
	public GameObject pracownik;
	public static short tierTartaku = 0;
	private short[] getSurowce = new short[3] { 5, 20, 35};
	//public short porownajPoreDnia = (short)GameManager.pora_dnia;
	private short turyDoKoncaPracy;
	public short minusStamina = 1;

	//debbuger (delete)
    void Start()
    {
		PracaWTartaku();
    }

    void Update()
    {
        
    }

    public void PracaWTartaku()
    {
        //zmiena_pora_dnia ++
        //stamina--;
        try
        {
			if (pracownik.scene.IsValid())
			{
				// Czy bedzię pętla?	
				// 3 - to noc, a w nocy nie pracujemy 		
				if (GameManager.pora_dnia != 3)
				{
					//zrób to 
					turyDoKoncaPracy++;
				}
				if (turyDoKoncaPracy == 4)
				{
					gameManager.Zmiana_drewno(getSurowce[tierTartaku - 1]);
					//ewentualnie 
					//GameManager.drewno += getSurowce[tierTartaku - 1];

					Debug.Log("Poziom drewna wynosi:" + GameManager.drewno);
					turyDoKoncaPracy = 0;
					GameManager.grupowaStamina -= minusStamina;
				}
			}
		}
        catch (System.Exception)
        {
				Debug.Log("Nie ma pracownika w tartaku!");
			throw;
        }

        



	}

}
