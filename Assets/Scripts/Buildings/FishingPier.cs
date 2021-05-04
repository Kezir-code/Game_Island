using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingPier : MonoBehaviour
{
	//	- Wymaga pracownika
	//	-Generuje 40 jedzenia/ 12 tur  |  90 jedzenia/12 tur
	//	Tier 1 i 2

	//praca
	public static short tierPomostRybacki = 0;
	private short[] getSurowce = new short[2] { 40, 90};
	public short minusStamina = 1;
	private short czasPracy = 5; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)


	//Tier 1 (2 tur)
	//-20 drewna
	//-10 jedzenia
	//+itemy
	//Tier 2 (8 tur) 
	//-40 drewna
	//-20 jedzenia

	//upgrade
	private short[] timeToEndBuilding = new short[2] { 2, 8 };
	private short[,] costUpgrade = new short[2, 2] { { 20, 10 }, { 40, 20} };

	public GameObject prefabFishingPierTier1;
	public GameObject prefabFishingPierTier2;

	private GameObject fishingPier;

	void Start()
	{
		
	}

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

	#region Praca

	public void PracaWPomoscieRybackim(List<CharacterCreator> characterCreators)
	{
		foreach (CharacterCreator worker in characterCreators)
		{
			//int i = characterCreators.Count; // 0,1,2 czy 1,2,3?
			if (worker.turyDoKoncaPracy == 0)
			{
				worker.turyDoKoncaPracy = czasPracy; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)
			}
			// Czy bedzię pętla?	
			// 3 - to noc, a w nocy nie pracujemy 		
			if (GameManager.Instance.pora_dnia != 3)
			{
				worker.turyDoKoncaPracy--;
			}

			if (worker.turyDoKoncaPracy == 1)
			{
				GameManager.Instance.Zmiana_drewno(getSurowce[tierPomostRybacki - 1]);
				GameManager.Instance.stamina -= minusStamina;
				worker.turyDoKoncaPracy = 0;

				Debug.Log("Robotnik " + worker.name + " wyprodukował: " + getSurowce[tierPomostRybacki - 1]);
				Debug.Log("Poziom jedzenia wynosi:" + GameManager.Instance.drewno);
			}

		}

	}

	#endregion

	#region Upgrade

	// Zapytaj scenarzystów czy do ulepszenia potrzeba pracownika
	public void FishingPierTierUpgrade()
	{
		switch (tierPomostRybacki)
		{
			// budowa tartaku
			//	Tier 1: (czas budowy 4 tur)
			//		-Drewno 10
			case 0:
				if (GameManager.Instance.drewno < costUpgrade[0, 0] &&
					GameManager.Instance.jedzenie < costUpgrade[0, 1])
				{
					// Dla ludzi tworzących UI zrobić powiadomienie 
					Debug.Log("Nie masz wystarczająco surowców");
					return;
				}

				GameManager.Instance.drewno -= costUpgrade[0, 0];
				GameManager.Instance.jedzenie -= costUpgrade[0, 1];

				fishingPier = (GameObject)Instantiate(prefabFishingPierTier1, GetBuildPostion(), transform.rotation);
				tierPomostRybacki++;

				break;



			case 1:
				if (GameManager.Instance.drewno < costUpgrade[1, 0] &&
					GameManager.Instance.jedzenie < costUpgrade[1, 1])
				{
					Debug.Log("Nie masz wystarczająco surowców");
					return;
				}
				GameManager.Instance.drewno -= costUpgrade[1, 0];
				GameManager.Instance.jedzenie -= costUpgrade[1, 1];

				Destroy(fishingPier);
				GameObject QuarryTier2 = (GameObject)Instantiate(prefabFishingPierTier2, GetBuildPostion(), transform.rotation);
				fishingPier = QuarryTier2;
				tierPomostRybacki++;
				break;
		
		}	
			

	}
	#endregion
}


