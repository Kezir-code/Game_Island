using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPier : MonoBehaviour
{
	//	- Wymaga pracownika
	//	-Generuje 40 jedzenia/ 12 tur  |  90 jedzenia/12 tur
	//	Tier 1 i 2

	public static short tierPomostRybacki = 0;
	private short[] getSurowce = new short[2] { 40, 90};
	//public short porownajPoreDnia = (short)GameManager.pora_dnia;
	private short turyDoKoncaPracy;
	public short minusStamina = 1;

	void Start()
	{
		
	}

	void Update()
	{

	}

	public void PracaWPomoscieRybackim(List<CharacterController> workers)
	{
		//zmiena_pora_dnia ++
		//stamina--;

		if (GameManager.Instance.pora_dnia != 3)
		{
			turyDoKoncaPracy++;
		}
		if (turyDoKoncaPracy == 12)
		{
			foreach (CharacterController worker in workers)
			{
				//GameManager.Instance.Zmiana_jedzenia(getSurowce[tierPomostRybacki - 1]);
			}
			

			Debug.Log("Poziom jedzenia wynosi:" + GameManager.Instance.jedzenie);
			turyDoKoncaPracy = 0;
			GameManager.Instance.stamina -= minusStamina;
		}


	}

}
