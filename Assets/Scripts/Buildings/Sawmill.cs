using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : MonoBehaviour 
{
	//	- Wymaga pracownika
	//	- Generuje 5/20/35 (na dzień 4 tury)
	public static short tierTartaku = 0;
	private short[] getSurowce = new short[3] { 5, 20, 35};
	//	-Wymagania:
	//	Tier 1: (czas budowy 4 tur)
	//		-Drewno 10
	//	Tier 2: (czas budowy 8 tur)
	//		-Kamień 20
	//		-Drewno 35
	//	
	//	Tier 3: (czas bydowy 12 tur)
	//		-Kamień 50
	//		-Drewno 70
	//		-Żelazo 18
	private short[,] costUpgrade = new short[3, 3] { { 10, 0, 0 }, { 35, 20, 0 }, { 70, 50, 18 } };
	//public short porownajPoreDnia = (short)GameManager.pora_dnia;
	private short turyDoKoncaPracy;
	public short minusStamina = 1;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void PracaWTartaku(List<CharacterController> workers)
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
			foreach (CharacterController worker in workers)
			{
				//GameManager.Instance.Zmiana_drewno(getSurowce[tierTartaku - 1]);
			}

			Debug.Log("Poziom drewna wynosi:" + GameManager.drewno);
			turyDoKoncaPracy = 0;
			GameManager.grupowaStamina -= minusStamina;
		}



	}
	public void TartakTierUpgrade()
	{
		switch (Sawmill.tierTartaku)
		{
			// budowa tartaku
			case 0:
				if (GameManager.drewno < costUpgrade[0, 0])
				{
					// Dla ludzi tworzących UI zrobić powiadomienie 
					Debug.Log("Nie masz wystarczająco surowców");
					return;
				}

				GameManager.drewno -= costUpgrade[0, 0];



				//GameObject _sawmill = (GameObject)Instantiate();

				break;
			case 1:
				if (GameManager.drewno < 35 && GameManager.kamien < 20)
				{
					Debug.Log("Nie masz wystarczająco surowców");
					return;
				}
				GameManager.drewno -= costUpgrade[1, 0];
				break;
			default:
				break;
		}

	}

}
