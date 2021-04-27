using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quarry : MonoBehaviour
{
	//kamieniołom założenia - Wymaga pracownika XD
	//Takie same jak w tartaku
	//niby pozniej w kopalnie, ale w/e


	public GameManager gameManager;
	public GameObject pracownik;
	public static short tierKamieniolomu = 0;
	private short[] getSurowce = new short[3] { 5, 20, 35 };
	//public short porownajPoreDnia = (short)GameManager.pora_dnia;
	private short turyDoKoncaPracy;
	public short minusStamina = 1;

	//debbuger (delete)
	void Start()
	{
		PracaWKamieniolomie();

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void PracaWKamieniolomie()
	{
		//zmiena_pora_dnia ++
		//stamina--;
		try
		{
			if (pracownik.scene.IsValid())
			{
				// Czy bedzię pętla?	
				// 3 - to noc, a w nocy nie pracujemy 		
				if (GameManager.pora_dnia != 3)
				{
					turyDoKoncaPracy++;
				}
				if (turyDoKoncaPracy == 4)
				{
					gameManager.Zmiana_kamien(getSurowce[tierKamieniolomu - 1]);
					//ewentualnie 
					//GameManager.kamien += getSurowce[tierKamieniolomu - 1];

					Debug.Log("Poziom kamienia wynosi:" + GameManager.kamien);
					turyDoKoncaPracy = 0;
					GameManager.grupowaStamina -= minusStamina;
				}
			}
		}
		catch (System.Exception)
		{
			Debug.Log("Nie ma pracownika w tartaku!");
			throw;
		}


	}



}
