using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Testowy skrypt
/// </summary>
public class SawmillUpgrade : MonoBehaviour
{



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

	//								     {drewno, kamień, żelazo}
	private short[,] costUpgrade = new short[3, 3] { { 10, 0, 0}, {35, 20, 0}, { 70, 50, 18}};

	public GameObject sawmill;


	public Vector3 GetPozycjeBudynku()
    {
		return transform.position;
    }

	
	public void TartakTierUpgrade()
    {
        switch (Sawmill.tierTartaku)
        {
			// budowa tartaku
			case 0:
				if (GameManager.drewno < costUpgrade[0,0])
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
