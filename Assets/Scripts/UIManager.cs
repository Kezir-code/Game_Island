using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject guzik;
    public int liczbaGuzików;

    void Start()
    {
        dodajPrzyciski(liczbaGuzików);
    }


    void Update()
    {
        
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
