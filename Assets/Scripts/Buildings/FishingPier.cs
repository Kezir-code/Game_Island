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
	private short timeToEndBuilding =  12;
	private short[,] costUpgrade = new short[2, 3] { { 30, 0, 0 }, { 40, 20, 6} };
	public static short pozostaleTuryDoBudowy = 0;


	public GameObject prefabFishingPierTier1;
	public GameObject prefabFishingPierTier2;

	private GameObject fishingPier;

	private readonly GameManager gM = GameManager.Instance;

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
				worker.czyPracuje = true;
			}
			// Czy bedzię pętla?	
			// 3 - to noc, a w nocy nie pracujemy 		
			if (gM.pora_dnia != 3)
			{
				worker.turyDoKoncaPracy--;
			}

			if (worker.turyDoKoncaPracy == 1)
			{
				gM.Zmiana_drewno(getSurowce[tierPomostRybacki - 1]);
				gM.stamina -= minusStamina;
				worker.turyDoKoncaPracy = 0;
				worker.czyPracuje = false;

				Debug.Log("Robotnik " + worker.name + " wyprodukował: " + getSurowce[tierPomostRybacki - 1]);
				Debug.Log("Poziom jedzenia wynosi:" + gM.drewno);
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
                if (pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < costUpgrade[tierPomostRybacki, 0])
					{
						// Dla ludzi tworzących UI zrobić powiadomienie 
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}

					gM.drewno -= costUpgrade[tierPomostRybacki, 0];
					pozostaleTuryDoBudowy = (short)(timeToEndBuilding + 1);
				}

                else if (pozostaleTuryDoBudowy == 1)
                {
					fishingPier = (GameObject)Instantiate(prefabFishingPierTier1, GetBuildPostion(), transform.rotation);
					tierPomostRybacki++;
					pozostaleTuryDoBudowy = 0;
				}

				else
				{
					Debug.Log($"Pozostały czas do wybudowania pomostu rybackiego to: {pozostaleTuryDoBudowy - 1}"); // #2
					pozostaleTuryDoBudowy--; // # 2
				}

				break; //case 0



			case 1:
                if (pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < costUpgrade[tierPomostRybacki, 0] &&
						gM.kamien < costUpgrade[tierPomostRybacki, 1] &&
						gM.zelazo < costUpgrade[tierPomostRybacki, 2])
					{
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}

					gM.drewno -= costUpgrade[tierPomostRybacki, 0];
					gM.kamien -= costUpgrade[tierPomostRybacki, 1];
					gM.zelazo -= costUpgrade[tierPomostRybacki, 2];
					pozostaleTuryDoBudowy = (short)(timeToEndBuilding + 1);	 // # 13
				}

				else if (pozostaleTuryDoBudowy == 1)
				{
					Destroy(fishingPier);
					GameObject FishingPierTier2 = (GameObject)Instantiate(prefabFishingPierTier2, GetBuildPostion(), transform.rotation);
					fishingPier = FishingPierTier2;
					tierPomostRybacki++;
				}

				else
				{
					Debug.Log($"Pozostały czas do ulepszenia pomostu rybackiego to: {pozostaleTuryDoBudowy - 1}"); // #12
					pozostaleTuryDoBudowy--; // # 12
				}


				break; // case 1
		
		}	
			

	}
	#endregion
}


