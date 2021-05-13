using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// to do
// Sprawdz czy tier tartaku jest taki jaki potrzeba
// Zrobić budowanie


	public class Sawmill : MonoBehaviour 
{
	//	- Wymaga pracownika
	//	- Generuje 5/20/35 (na dzień 4 tury)

	//praca
	private short tierTartaku = 0;
	private short[] getSurowce = new short[3] { 5, 20, 35};
	private short czasPracy = 3; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)
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

	//upgrade
	private short[,] costUpgrade = new short[3, 3] { { 5, 0, 0 }, { 35, 20, 0 }, { 70, 50, 18 } };
	private short[] timeToEndBuilding = new short[3] { 4, 8, 12 };
	public static short pozostaleTuryDoBudowy = 0;
	public short minusStamina = 4;

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
			//int i = characterCreators.Count; // 0,1,2 czy 1,2,3?
			if (worker.turyDoKoncaPracy == 0)
			{
				worker.turyDoKoncaPracy = czasPracy; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)
				worker.czyPracuje = true;
			}
			// Czy bedzię pętla?	
			// 3 - to noc, a w nocy nie pracujemy 		
			if (gM.pora_dnia != 3 &&			 
				gM.stamina >= minusStamina)
			{
				worker.turyDoKoncaPracy--;					
				gM.stamina -= minusStamina;			
			}
            else if (gM.stamina < minusStamina) 
            {
				worker.czyPracuje = false;
				Debug.Log("Brakuje staminy");
			}

			if (worker.turyDoKoncaPracy == 1 )
			{
				gM.Zmiana_drewno(getSurowce[tierTartaku - 1]);
				worker.turyDoKoncaPracy = 0;
				worker.czyPracuje = false;
				Debug.Log("Robotnik " + worker.name + " wyprodukował: " + getSurowce[tierTartaku - 1]);
				Debug.Log("Poziom drewna wynosi:" + gM.drewno);
			}

		}
		
	}

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
			case 0:
                if (pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < costUpgrade[tierTartaku, 0])
					{
						// Dla ludzi tworzących UI zrobić powiadomienie 
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}

					gM.drewno -= costUpgrade[tierTartaku, 0];
					pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierTartaku] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 5
				}

				else if (pozostaleTuryDoBudowy == 1)
				{
					sawmill = (GameObject)Instantiate(prefabSawmillTier1, GetBuildPostion(), transform.rotation);
					tierTartaku++;
					pozostaleTuryDoBudowy = 0;
				}

				else
				{
					Debug.Log($"Pozostały czas do wybudowania tartaku to: {pozostaleTuryDoBudowy - 1}"); // # 4
					pozostaleTuryDoBudowy--; // # 4
				}

				break; // case 0


			//	Tier 2: (czas budowy 8 tur)
			//		-Kamień 20
			//		-Drewno 35
			case 1:
                if (pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < costUpgrade[1, 0] &&
						gM.kamien < costUpgrade[1, 1])
					{
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}
					gM.drewno -= costUpgrade[1, 0];
					gM.kamien -= costUpgrade[1, 1];
					pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierTartaku] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 9
				}			

                else if (pozostaleTuryDoBudowy == 1)
                {
					Destroy(sawmill);
					GameObject sawmillTier2 = (GameObject)Instantiate(prefabSawmillTier2, GetBuildPostion(), transform.rotation);
					sawmill = sawmillTier2;
					tierTartaku++;
					pozostaleTuryDoBudowy = 0;
				}

                else
                {
					Debug.Log($"Pozostały czas do ulepszenia tartaku to: {pozostaleTuryDoBudowy - 1}"); // #8
					pozostaleTuryDoBudowy--; // # 8
                }

				break; // case 1 

			//	Tier 3: (czas bydowy 12 tur)
			//		-Kamień 50
			//		-Drewno 70
			//		-Żelazo 18
			case 2:
                if (pozostaleTuryDoBudowy == 0)
                {
					if (gM.drewno < costUpgrade[2, 0] &&
						gM.kamien < costUpgrade[2, 1] &&
						gM.zelazo < costUpgrade[2, 2])
					{
						Debug.Log("Nie masz wystarczająco surowców");
						return;
					}

					gM.drewno -= costUpgrade[2, 0];
					gM.kamien -= costUpgrade[2, 1];
					gM.zelazo -= costUpgrade[2, 2];
					pozostaleTuryDoBudowy = (short)(timeToEndBuilding[tierTartaku] + 1); // to samo jak z pracą trzeba dodać +1 żeby to działało # 13

				}

				else if (pozostaleTuryDoBudowy == 1)
				{
					Destroy(sawmill);
					GameObject sawmillTier3 = (GameObject)Instantiate(prefabSawmillTier2, GetBuildPostion(), transform.rotation);
					sawmill = sawmillTier3;
					tierTartaku++;
					pozostaleTuryDoBudowy = 0;
				}

				else
				{
					Debug.Log($"Pozostały czas do ulepszenia tartaku to: {pozostaleTuryDoBudowy - 1}"); // #12
					pozostaleTuryDoBudowy--; // # 12
				}



				break;
			default:
				break;
		}

	}
	#endregion
}
