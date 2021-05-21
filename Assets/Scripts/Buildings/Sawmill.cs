using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to do
// Zmodyfikować kod
// 


	public class Sawmill : MonoBehaviour 
{
	//	- Wymaga pracownika
	//	- Generuje 5/20/35 (na dzień 4 tury)
	//	-Wymagania:
	//	Tier 1: (czas budowy 4 tur)
	//		-Drewno 5
	//	Tier 2: (czas budowy 8 tur)
	//		-Kamień 20
	//		-Drewno 35
	//	
	//	Tier 3: (czas bydowy 12 tur)
	//		-Kamień 50
	//		-Drewno 70
	//		-Żelazo 18

	//praca

	//private short[] getSurowce = new short[3] { 5, 20, 35};
	//private short czasPracy = 3; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)


	//upgrade

	//private short[,] costUpgrade = new short[3, 3] { { 5, 0, 0 }, { 35, 20, 0 }, { 70, 50, 18 } };
	//private short[] timeToEndBuilding = new short[3] { 4, 8, 12 };
	//public static short pozostaleTuryDoBudowy = 0;

	private short tierTartaku = 0;
	
	public GameObject prefabSawmillTier1;
	public GameObject prefabSawmillTier2;
	public GameObject prefabSawmillTier3;

	private GameObject sawmill;

	private readonly GameManager gM = GameManager.Instance;


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

    public void PracaWTartaku(List<CharacterCreator> characterCreators)
	{
        foreach (CharacterCreator worker in characterCreators)
        {
			if (worker.czyPracuje == false || 
				worker.tagPracy != Tartak.tagPracy)
			{
				worker.turyDoKoncaPracy = Tartak.czasPracy; 
				worker.czyPracuje = true;
				worker.tagPracy = Tartak.tagPracy;
			}
			
			// 3 - to noc, a w nocy nie pracujemy 		
			else if (gM.pora_dnia != 3 &&			 
				gM.stamina >= Tartak.kosztStaminy)
			{
				worker.turyDoKoncaPracy--;					
				gM.stamina -= Tartak.kosztStaminy;			
			}

            else if (gM.stamina < Tartak.kosztStaminy) 
            {
				worker.czyPracuje = false;
				Debug.Log("Brakuje staminy");
			}

			if (worker.turyDoKoncaPracy == 1 )
			{
                switch (tierTartaku)
                {
					case (short)Tartak.TierTartaku.TIER_1:
						worker.turyDoKoncaPracy = 0;
						worker.czyPracuje = false;
                        if (worker.trait == Trait.ZAWODOWY_DRWAL)
                        {
                            gM.drewno += (int)((short)Tartak.DostanSurowce.DREWNO_TIER_1* Trait.MODYFIKATOR_DRWAL_TRAIT);

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " + 
								(int)((short)Tartak.DostanSurowce.DREWNO_TIER_1 * Trait.MODYFIKATOR_DRWAL_TRAIT));

							Debug.Log("Poziom drewna wynosi:" + gM.drewno);
						}

                        else
                        {
							gM.drewno += (short)Tartak.DostanSurowce.DREWNO_TIER_1;

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " +
								(short)Tartak.DostanSurowce.DREWNO_TIER_1);

							Debug.Log("Poziom drewna wynosi:" + gM.drewno);
						}
						break;

					case (short)Tartak.TierTartaku.TIER_2:
						gM.drewno += (short)Tartak.DostanSurowce.DREWNO_TIER_2;
						worker.turyDoKoncaPracy = 0;
						worker.czyPracuje = false;

						if (worker.trait == Trait.ZAWODOWY_DRWAL)
						{
							gM.drewno += (int)((short)Tartak.DostanSurowce.DREWNO_TIER_2 * Trait.MODYFIKATOR_DRWAL_TRAIT);

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " +
								(int)((short)Tartak.DostanSurowce.DREWNO_TIER_2 * Trait.MODYFIKATOR_DRWAL_TRAIT));

							Debug.Log("Poziom drewna wynosi:" + gM.drewno);
						}

						else
						{
							gM.drewno += (short)Tartak.DostanSurowce.DREWNO_TIER_2;

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " +
								(short)Tartak.DostanSurowce.DREWNO_TIER_2);

							Debug.Log("Poziom drewna wynosi:" + gM.drewno);
						}

						Debug.Log("Robotnik " + worker.name + " wyprodukował: " + (short)Tartak.DostanSurowce.DREWNO_TIER_2);
						Debug.Log("Poziom drewna wynosi:" + gM.drewno);
						break;

					case (short)Tartak.TierTartaku.TIER_3:
						gM.drewno += (short)Tartak.DostanSurowce.DREWNO_TIER_3;
						worker.turyDoKoncaPracy = 0;
						worker.czyPracuje = false;

						if (worker.trait == Trait.ZAWODOWY_DRWAL)
						{
							gM.drewno += (int)((short)Tartak.DostanSurowce.DREWNO_TIER_3 * Trait.MODYFIKATOR_DRWAL_TRAIT);

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " +
								(int)((short)Tartak.DostanSurowce.DREWNO_TIER_3 * Trait.MODYFIKATOR_DRWAL_TRAIT));

							Debug.Log("Poziom drewna wynosi:" + gM.drewno);
						}

						else
						{
							gM.drewno += (short)Tartak.DostanSurowce.DREWNO_TIER_3;

							Debug.Log("Robotnik " + worker.name + " wyprodukował: " +
								(short)Tartak.DostanSurowce.DREWNO_TIER_3);

							Debug.Log("Poziom drewna wynosi:" + gM.drewno);
						}

						Debug.Log("Robotnik " + worker.name + " wyprodukował: " + (short)Tartak.DostanSurowce.DREWNO_TIER_3);
						Debug.Log("Poziom drewna wynosi:" + gM.drewno);
						break;
                }
                
			}

		}

	}//PracaWTartaku

	#endregion


	#region Upgrade

	// Zapytaj scenarzystów czy do ulepszenia potrzeba pracownika
	public void TartakTierUpgrade()
	{
		switch (tierTartaku)
		{
			// budowa tartaku
			//	Tier 1: (czas budowy 4 tur)
			//		-Drewno 10
			case (short)Tartak.TierTartaku.NONE:
                if (Tartak.pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < (short)Tartak.BudowaTartaku.DREWNO &&
						gM.kamien < (short)Tartak.BudowaTartaku.KAMIEN &&
						gM.zelazo < (short)Tartak.BudowaTartaku.ZELAZO)
					{
						// Dla ludzi tworzących UI zrobić powiadomienie 
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}

					gM.drewno -= (short)Tartak.BudowaTartaku.DREWNO;
					gM.drewno -= (short)Tartak.BudowaTartaku.KAMIEN;
					gM.drewno -= (short)Tartak.BudowaTartaku.ZELAZO;
					Tartak.pozostaleTuryDoBudowy = (short)(Tartak.BudowaTartaku.CZAS_BUDOWY); // #4
				}

				else if (Tartak.pozostaleTuryDoBudowy == 1)
				{
					sawmill = (GameObject)Instantiate(prefabSawmillTier1, GetBuildPostion(), transform.rotation);
					tierTartaku = (short)Tartak.TierTartaku.TIER_1;
					Tartak.pozostaleTuryDoBudowy = 0;
				}

				else
				{
					Tartak.pozostaleTuryDoBudowy--; // # 3
					Debug.Log($"Pozostały czas do wybudowania tartaku to: {Tartak.pozostaleTuryDoBudowy}"); // # 3
				}

				break; // case 0


			//	Tier 2: (czas budowy 8 tur)
			//		-Kamień 20
			//		-Drewno 35
			case (short)Tartak.TierTartaku.TIER_1:
                if (Tartak.pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < (short)Tartak.UpgradeTartakTier2.DREWNO &&
						gM.kamien < (short)Tartak.UpgradeTartakTier2.KAMIEN &&
						gM.zelazo < (short)Tartak.UpgradeTartakTier2.ZELAZO)
					{
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}
					gM.drewno -= (short)Tartak.UpgradeTartakTier2.DREWNO;
					gM.kamien -= (short)Tartak.UpgradeTartakTier2.KAMIEN;
					gM.zelazo -= (short)Tartak.UpgradeTartakTier2.ZELAZO;
					Tartak.pozostaleTuryDoBudowy = (short)Tartak.UpgradeTartakTier2.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 9
				}			

                else if (Tartak.pozostaleTuryDoBudowy == 1)
                {
					Destroy(sawmill);
					GameObject sawmillTier2 = (GameObject)Instantiate(prefabSawmillTier2, GetBuildPostion(), transform.rotation);
					sawmill = sawmillTier2;
					tierTartaku++;
					Tartak.pozostaleTuryDoBudowy = 0;
				}

                else
                {
					Tartak.pozostaleTuryDoBudowy--; // # 7
					Debug.Log($"Pozostały czas do ulepszenia tartaku to: {Tartak.pozostaleTuryDoBudowy}"); // #7
					
                }

				break; // case 1 

			//	Tier 3: (czas budowy 12 tur)
			//		-Kamień 50
			//		-Drewno 70
			//		-Żelazo 18
			case (short)Tartak.TierTartaku.TIER_2:
                if (Tartak.pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < (short)Tartak.UpgradeTartakTier3.DREWNO &&
						gM.kamien < (short)Tartak.UpgradeTartakTier3.KAMIEN &&
						gM.zelazo < (short)Tartak.UpgradeTartakTier3.ZELAZO)
					{
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}

					gM.drewno -= (short)Tartak.UpgradeTartakTier3.DREWNO;
					gM.kamien -= (short)Tartak.UpgradeTartakTier3.KAMIEN;
					gM.zelazo -= (short)Tartak.UpgradeTartakTier3.ZELAZO;
					Tartak.pozostaleTuryDoBudowy = (short)Tartak.UpgradeTartakTier3.CZAS_BUDOWY; // to samo jak z pracą trzeba dodać +1 żeby to działało # 13

				}

				else if (Tartak.pozostaleTuryDoBudowy == 1)
				{
					Destroy(sawmill);
					GameObject sawmillTier3 = (GameObject)Instantiate(prefabSawmillTier2, GetBuildPostion(), transform.rotation);
					sawmill = sawmillTier3;
					tierTartaku++;
					Tartak.pozostaleTuryDoBudowy = 0;
				}

				else
				{
					Debug.Log($"Pozostały czas do ulepszenia tartaku to: {Tartak.pozostaleTuryDoBudowy - 1}"); // #12
					Tartak.pozostaleTuryDoBudowy--; // # 12
				}

				break;
			default:
				Debug.LogWarning("Nie możesz już ulepszyć tartaku!!");
				break; // case 2
		}

	}//Tartak Tier Upgrade

	public void SprzedajBudynek()
	{
		switch (tierTartaku)
		{
			case (short)Tartak.TierTartaku.TIER_1:

				Destroy(sawmill);
				gM.drewno -= (short)Tartak.SprzedazBudynkuTier1.DREWNO;
				gM.kamien -= (short)Tartak.SprzedazBudynkuTier1.KAMIEN;
				gM.zelazo -= (short)Tartak.SprzedazBudynkuTier1.ZELAZO;

				break;

			case (short)Tartak.TierTartaku.TIER_2:

				Destroy(sawmill);
				gM.drewno -= (short)Tartak.SprzedazBudynkuTier2.DREWNO;
				gM.kamien -= (short)Tartak.SprzedazBudynkuTier2.KAMIEN;
				gM.zelazo -= (short)Tartak.SprzedazBudynkuTier2.ZELAZO;

				break;

			case (short)Tartak.TierTartaku.TIER_3:

				Destroy(sawmill);
				gM.drewno -= (short)Tartak.SprzedazBudynkuTier3.DREWNO;
				gM.kamien -= (short)Tartak.SprzedazBudynkuTier3.KAMIEN;
				gM.zelazo -= (short)Tartak.SprzedazBudynkuTier3.ZELAZO;

				break;
			default:
				Debug.LogWarning("Budynek już nie istnieje lub tier się nie zgadza");
				break;

		}
	}//SprzedajBudynek
		#endregion
}//class
