using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject guzik;
    public int liczbaGuzików;
    private int aktuanieWybraneMiejsceWBudynku;
    public Text opisMiejsceWBudynku1;
    public Text opisMiejsceWBudynku2;
    private Text osobaWybranaText;

    
    void Start()
    {
        //dodajPrzyciski(liczbaGuzików);
    }


    void Update()
    {
        
    }

    public void ZmienTextWMiejscuPracy (Text nazwisko)
    {
        if (aktuanieWybraneMiejsceWBudynku == 1)
        {
            opisMiejsceWBudynku1.text = "miejsce 1: " + nazwisko;
        }
        else if (aktuanieWybraneMiejsceWBudynku == 2)
        {
            opisMiejsceWBudynku2.text = "miejsce 2: " + nazwisko;
        }
    }

    public void WyborMiejscaWBudynku1()
    {
        aktuanieWybraneMiejsceWBudynku = 1;
    }

    public void WyborMiejscaWBudynku2()
    {
        aktuanieWybraneMiejsceWBudynku = 2;
    }

    void dodajPrzyciski(int liczbaPrzycisków)
    {
        for (int i = 0; i > liczbaPrzycisków*-30; i -= 30) 
            {
            GameObject tempButton = Instantiate(guzik, new Vector3(0, i, 0), Quaternion.identity);
            tempButton.transform.SetParent(GameObject.FindGameObjectWithTag("ZarzadzaniePraca").transform, false);
            }

    }
}
