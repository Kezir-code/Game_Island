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
	//private short[] getSurowce = new short[2] { 40, 90};
	//public short minusStamina = 1;
	//private short czasPracy = 5; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)


	//Tier 1 (2 tur)
	//-20 drewna
	//-10 jedzenia
	//+itemy
	//Tier 2 (8 tur) 
	//-40 drewna
	//-20 jedzenia

	//upgrade
	//private short timeToEndBuilding =  12;
	//private short[,] costUpgrade = new short[2, 3] { { 30, 0, 0 }, { 40, 20, 6} };
	//public static short pozostaleTuryDoBudowy = 0;

	//public float mnoznikZKuchni = 1.2f; // wstawić to do gamemanger'a?


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
				worker.turyDoKoncaPracy = PomostRybacki.czasPracy; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)
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
                switch (tierPomostRybacki)
                {
					case (short)PomostRybacki.TierPomostRybacki.POMOST_RYBACKI:
						if (Ognisko.kuchniaBonus == true && worker.trait == Trait.ZAWODOWY_KUCHARZ)
						{
							gM.jedzenie = (int)((short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI *
								PomostRybacki.mnoznikZKuchni * Trait.MODYFIKATOR_KUCHARZ_TRAIT);

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " + 
								(short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI * PomostRybacki.mnoznikZKuchni * Trait.MODYFIKATOR_KUCHARZ_TRAIT);
							Debug.Log("Poziom jedzenia wynosi:" + gM.drewno);
						}

                        else if (Ognisko.kuchniaBonus == true)
                        {
							gM.jedzenie = (int)((short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI * PomostRybacki.mnoznikZKuchni);

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " + 
								(short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI * PomostRybacki.mnoznikZKuchni);
							Debug.Log("Poziom jedzenia wynosi:" + gM.drewno);
						}

						else
						{
							gM.jedzenie = (short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI;

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " + (short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI);
							Debug.Log("Poziom jedzenia wynosi:" + gM.drewno);
						}

						break;

					case (short)PomostRybacki.TierPomostRybacki.CHATA_RYBACKA:
						if (Ognisko.kuchniaBonus == true && worker.trait == Trait.ZAWODOWY_KUCHARZ)
						{
							gM.jedzenie = (int)((short)PomostRybacki.DostanSurowce.JEDZENIE_CHATA_RYBACKA * 
								PomostRybacki.mnoznikZKuchni * Trait.MODYFIKATOR_KUCHARZ_TRAIT);

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " + (short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI);
							Debug.Log("Poziom jedzenia wynosi:" + gM.drewno);
						}

						else if (Ognisko.kuchniaBonus == true)
						{
							gM.jedzenie = (int)((short)PomostRybacki.DostanSurowce.JEDZENIE_CHATA_RYBACKA * PomostRybacki.mnoznikZKuchni);

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " + (short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI);
							Debug.Log("Poziom jedzenia wynosi:" + gM.drewno);
						}

						else
						{
							gM.jedzenie = (short)PomostRybacki.DostanSurowce.JEDZENIE_CHATA_RYBACKA;

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " + (short)PomostRybacki.DostanSurowce.JEDZENIE_POMOST_RYBACKI);
							Debug.Log("Poziom jedzenia wynosi:" + gM.drewno);
						}

						break;
                    default:
                        break;
                }
				//gM.stamina -= minusStamina;
				worker.turyDoKoncaPracy = 0;
				worker.czyPracuje = false;
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
                if (PomostRybacki.pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < (short)PomostRybacki.BudowaPomostRybacki.DREWNO)
					{
						// Dla ludzi tworzących UI zrobić powiadomienie 
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}

					gM.drewno -= (short)PomostRybacki.BudowaPomostRybacki.DREWNO;
					PomostRybacki.pozostaleTuryDoBudowy = (short)PomostRybacki.BudowaPomostRybacki.CZAS_BUDOWY;
				}

                else if (PomostRybacki.pozostaleTuryDoBudowy == 1)
                {
					fishingPier = (GameObject)Instantiate(prefabFishingPierTier1, GetBuildPostion(), transform.rotation);
					tierPomostRybacki++;
					PomostRybacki.pozostaleTuryDoBudowy = 0;
				}

				else
				{
					Debug.Log($"Pozostały czas do wybudowania pomostu rybackiego to: {PomostRybacki.pozostaleTuryDoBudowy}"); // #2
					PomostRybacki.pozostaleTuryDoBudowy--; // # 2
				}

				break; //case 0

			case 1:
                if (PomostRybacki.pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < (short)PomostRybacki.UpgradeNaChataRybacka.DREWNO &&
						gM.kamien < (short)PomostRybacki.UpgradeNaChataRybacka.KAMIEN &&
						gM.zelazo < (short)PomostRybacki.UpgradeNaChataRybacka.ZELAZO)
					{
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}

					gM.drewno -= (short)PomostRybacki.UpgradeNaChataRybacka.DREWNO;
					gM.kamien -= (short)PomostRybacki.UpgradeNaChataRybacka.KAMIEN;
					gM.zelazo -= (short)PomostRybacki.UpgradeNaChataRybacka.ZELAZO;
					PomostRybacki.pozostaleTuryDoBudowy = (short)PomostRybacki.UpgradeNaChataRybacka.CZAS_BUDOWY;	 // # 12
				}

				else if (PomostRybacki.pozostaleTuryDoBudowy == 1)
				{
					Destroy(fishingPier);
					GameObject FishingPierTier2 = (GameObject)Instantiate(prefabFishingPierTier2, GetBuildPostion(), transform.rotation);
					fishingPier = FishingPierTier2;
					tierPomostRybacki++;
				}

				else
				{
					Debug.Log($"Pozostały czas do ulepszenia pomostu rybackiego to: {PomostRybacki.pozostaleTuryDoBudowy}"); // #12
					PomostRybacki.pozostaleTuryDoBudowy--; // # 12
				}

				break; // case 1
		
		}	
			
	}
	#endregion
}


