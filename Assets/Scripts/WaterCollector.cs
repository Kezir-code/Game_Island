using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCollector : MonoBehaviour
{


	public GameManager gameManager;
	public GameObject pracownik; // null
	public static short tierZbierakWody = 0;
	private short[] getSurowce = new short[3] { 5, 20, 35 };
	//public short porownajPoreDnia = (short)GameManager.pora_dnia;
	private short turyDoKoncaPracy;
	public short minusStamina = 1;

	//debbuger (delete)
	void Start()
	{
		PracaWZbierakuWody();
	}

	void Update()
	{

	}

	public void PracaWZbierakuWody()
	{
		//zmiena_pora_dnia ++
		//stamina--;
		try
		{
			if (pracownik.scene.IsValid()) // pracownik = null != 0
			{
				// Czy bedzię pętla?	
				// 3 - to noc, a w nocy nie pracujemy 		
				if (GameManager.pora_dnia != 3)
				{
					turyDoKoncaPracy++;
				}
				if (turyDoKoncaPracy == 4)
				{
					gameManager.Zmiana_wody(getSurowce[tierZbierakWody - 1]);
                    //ewentualnie 
                    //GameManager.drewno += getSurowce[tierZbierakWody - 1];

                    // to do UI
					Debug.Log("Poziom wody wynosi:" + GameManager.woda);
					turyDoKoncaPracy = 0;
					GameManager.grupowaStamina -= minusStamina;
				}
			}
		}
		catch (System.Exception)
		{
			Debug.Log("Nie ma pracownika w zbieraku wody!");
			throw;
		}

	}

}
