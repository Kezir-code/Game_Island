using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarry : MonoBehaviour
{
	//	- Wymaga pracownika
	//	- Generuje 5/20/35 (na dzień 4 tury)
	public short tierQuarry = 0;
	private short[] getSurowce = new short[3] { 5, 20, 35 };
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
	private short[] timeToEndBuilding = new short[3] { 4, 8, 12 };
	public short minusStamina = 1;
	private short czasPracy = 5; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)


	public GameObject prefabQuarryTier1;
	public GameObject prefabQuarryTier2;
	public GameObject prefabQuarryTier3;

	private GameObject quarry;


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

	void Start()
	{

	}

	void Update()
	{

	}

	#region Praca

	public void PracaWKamieniolomie(List<CharacterCreator> characterCreators)
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
				GameManager.Instance.Zmiana_kamien(getSurowce[tierQuarry - 1]);
				GameManager.Instance.stamina -= minusStamina;
				worker.turyDoKoncaPracy = 0;

				Debug.Log("Robotnik " + worker.name + " wyprodukował: " + getSurowce[tierQuarry - 1]);
				Debug.Log("Poziom kamienia wynosi:" + GameManager.Instance.kamien);
			}

		}

	}

	#endregion


	#region Upgrade

	// Zapytaj scenarzystów czy do ulepszenia potrzeba pracownika
	public void QuarryTierUpgrade()
	{
		switch (tierQuarry)
		{
			// budowa tartaku
			//	Tier 1: (czas budowy 4 tur)
			//		-Drewno 10
			case 0:
				if (GameManager.Instance.drewno < costUpgrade[0, 0])
				{
					// Dla ludzi tworzących UI zrobić powiadomienie 
					Debug.Log("Nie masz wystarczająco surowców");
					return;
				}

				GameManager.Instance.drewno -= costUpgrade[0, 0];

				quarry = (GameObject)Instantiate(prefabQuarryTier1, GetBuildPostion(), transform.rotation);
				tierQuarry++;

				break;


			//	Tier 2: (czas budowy 8 tur)
			//		-Kamień 20
			//		-Drewno 35
			case 1:
				if (GameManager.Instance.drewno < costUpgrade[1, 0] &&
					GameManager.Instance.kamien < costUpgrade[1, 1])
				{
					Debug.Log("Nie masz wystarczająco surowców");
					return;
				}
				GameManager.Instance.drewno -= costUpgrade[1, 0];
				GameManager.Instance.kamien -= costUpgrade[1, 1];

				Destroy(quarry);
				GameObject QuarryTier2 = (GameObject)Instantiate(prefabQuarryTier2, GetBuildPostion(), transform.rotation);
				quarry = QuarryTier2;
				tierQuarry++;
				break;

			//	Tier 3: (czas bydowy 12 tur)
			//		-Kamień 50
			//		-Drewno 70
			//		-Żelazo 18
			case 2:
				if (GameManager.Instance.drewno < costUpgrade[2, 0] &&
					GameManager.Instance.kamien < costUpgrade[2, 1] &&
					GameManager.Instance.zelazo < costUpgrade[2, 2])
				{
					Debug.Log("Nie masz wystarczająco surowców");
					return;
				}
				GameManager.Instance.drewno -= costUpgrade[2, 0];
				GameManager.Instance.kamien -= costUpgrade[2, 1];
				GameManager.Instance.kamien -= costUpgrade[2, 2];

				GameObject QuarryTier3 = (GameObject)Instantiate(prefabQuarryTier2, GetBuildPostion(), transform.rotation);
				quarry = QuarryTier3;
				tierQuarry++;
				break;
			default:
				break;
		}

	}
	#endregion
}
