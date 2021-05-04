using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sawmill : MonoBehaviour 
{
	//	- Wymaga pracownika
	//	- Generuje 5/20/35 (na dzień 4 tury)
	public short tierTartaku = 0;
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
	private short[] timeToEndBuilding = new short[3] { 4, 8, 12 };
	public short minusStamina = 1;
	private short czasPracy = 5; // dodaj +1 do tur(Jezeli ktoś pracuje 4 tury to dajesz 5)


	public GameObject prefabSawmillTier1;
	public GameObject prefabSawmillTier2;
	public GameObject prefabSawmillTier3;

	private GameObject sawmill;


	# region Metody pomocnicze

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
			}
			// Czy bedzię pętla?	
			// 3 - to noc, a w nocy nie pracujemy 		
			if (GameManager.Instance.pora_dnia != 3)
			{
				worker.turyDoKoncaPracy--;
			}

			if (worker.turyDoKoncaPracy == 1)
			{
				GameManager.Instance.Zmiana_drewno(getSurowce[tierTartaku - 1]);
				GameManager.Instance.stamina -= minusStamina;
				worker.turyDoKoncaPracy = 0;

				Debug.Log("Robotnik " + worker.name + " wyprodukował: " + getSurowce[tierTartaku - 1]);
				Debug.Log("Poziom drewna wynosi:" + GameManager.Instance.drewno);
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
				if (GameManager.Instance.drewno < costUpgrade[0, 0])
				{
					// Dla ludzi tworzących UI zrobić powiadomienie 
					Debug.Log("Nie masz wystarczająco surowców");
					return;
				}

				GameManager.Instance.drewno -= costUpgrade[0, 0];

				sawmill = (GameObject)Instantiate(prefabSawmillTier1, GetBuildPostion(), transform.rotation);
				tierTartaku++;

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

				Destroy(sawmill);
				GameObject sawmillTier2 = (GameObject)Instantiate(prefabSawmillTier2, GetBuildPostion(), transform.rotation);
				sawmill = sawmillTier2;
				tierTartaku++;
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

				GameObject sawmillTier3 = (GameObject)Instantiate(prefabSawmillTier2, GetBuildPostion(), transform.rotation);
				sawmill = sawmillTier3;
				tierTartaku++;
				break;
			default:
				break;
		}

	}
	#endregion
}
