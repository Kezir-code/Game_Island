using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPier : MonoBehaviour
{
	//	- Wymaga pracownika
	//	-Generuje 40 jedzenia/ 12 tur  |  90 jedzenia/12 tur
	//	Tier 1 i 2

	public GameManager gameManager;
	public GameObject pracownik;
	public static short tierPomostRybacki = 0;
	private short[] getSurowce = new short[2] { 40, 90};
	//public short porownajPoreDnia = (short)GameManager.pora_dnia;
	private short turyDoKoncaPracy;
	public short minusStamina = 1;

	//debbuger (delete)
	void Start()
	{
		PracaWPomoscieRybackim();
	}

	void Update()
	{

	}

	public void PracaWPomoscieRybackim()
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
					turyDoKoncaPracy++;
				}
				if (turyDoKoncaPracy == 12)
				{
					gameManager.Zmiana_jedzenia(getSurowce[tierPomostRybacki - 1]);
					//ewentualnie 
					//GameManager.drewno += getSurowce[tierPomostRybacki - 1];

					Debug.Log("Poziom jedzenia wynosi:" + GameManager.jedzenie);
					turyDoKoncaPracy = 0;
					GameManager.grupowaStamina -= minusStamina;
				}
			}
		}
		catch (System.Exception)
		{
			Debug.Log("Nie ma pracownika w Pomoscie Rubackim!");
			throw;
		}

	}

}
