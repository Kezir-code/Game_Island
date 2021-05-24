using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject guzikDostepny;
    public GameObject guzikNieDostepny;
    private int aktuanieWybraneMiejsceWBudynku;
    public Text opisMiejsceWBudynku1;
    public Text opisMiejsceWBudynku2;
    private Text osobaWybranaText;

    List<Button> listaPrzyciski = new List<Button>();

    private float liczbaWolnych;
    private float liczbaPracujacych;



    void Start()
    {

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

    public void PokazOsobyDoPracy()
    {

        List<GameObject> listaOsob = GameManager.Instance.people;


        //czyszczenie poprzednich przycisków
        GameObject listaParent = GameObject.FindGameObjectWithTag("ListaOsóbDoWyboru");
        listaParent.Clear();

        liczbaWolnych = 0;
        liczbaPracujacych = 0;

        foreach (GameObject osoba in listaOsob)
        {
            CharacterCreator tempOsoba = osoba.GetComponent<CharacterCreator>();
            dodajPrzycisk(tempOsoba.surname, tempOsoba.czyPracuje);

        }
    }

    #region Dodawanie przycisków w odpowiednich miejscach - wybór pracownika
    void dodajPrzycisk(string nazwisko, bool czyPracuje)
    {
        if (!czyPracuje)
        {
            //dla dostępnych osób
            if (liczbaWolnych < 10)
            {
                //wszystko poniżej jest opisane; dla każdego wariantu działa dokładnie tak samo
                
                //stworzenie przycisku w odpowiednim miejscu
                GameObject tempButton = Instantiate(guzikDostepny, new Vector3(-17f, 2.5f - liczbaWolnych * 0.5f, -23f), Quaternion.identity);
                tempButton.transform.SetParent(GameObject.FindGameObjectWithTag("ListaOsóbDoWyboru").transform, false);
                
                //konwertowanie GameObject na Button
                Button osobaDostepnaButton = tempButton.GetComponent<Button>();

                //przypisanie odpowiedniego nazwiska do przycisku
                osobaDostepnaButton.GetComponentInChildren<Text>().text = nazwisko;

                //dodanie do listy przycisków
                listaPrzyciski.Add(osobaDostepnaButton);

                //bombała eventers
                osobaDostepnaButton.onClick.AddListener(delegate { GameManager.Instance.TaskOnClick(nazwisko); });

                //zmiana wartości zmiennych liczących ilość pracujących osób
                liczbaWolnych += 1;
            }
            else if (liczbaWolnych >= 10)
            {
                GameObject tempButton = Instantiate(guzikDostepny, new Vector3(-14f, 2.5f - (liczbaWolnych - 10) * 0.5f, -23f), Quaternion.identity);
                tempButton.transform.SetParent(GameObject.FindGameObjectWithTag("ListaOsóbDoWyboru").transform, false);

                Button osobaDostepnaButton = tempButton.GetComponent<Button>();

                osobaDostepnaButton.GetComponentInChildren<Text>().text = nazwisko;

                listaPrzyciski.Add(osobaDostepnaButton);

                osobaDostepnaButton.onClick.AddListener(delegate { GameManager.Instance.TaskOnClick(nazwisko); });

                liczbaWolnych += 1;
            }

        }

        else if (czyPracuje)
        {
            //dla zajętych osób
            if (liczbaPracujacych < 6)
            {
                GameObject tempButton = Instantiate(guzikNieDostepny, new Vector3(-17f, -2.5f - liczbaPracujacych * 0.5f, -23f), Quaternion.identity);
                tempButton.transform.SetParent(GameObject.FindGameObjectWithTag("ListaOsóbDoWyboru").transform, false);

                Button osobaNieDostepnaButton = tempButton.GetComponent<Button>();

                osobaNieDostepnaButton.GetComponentInChildren<Text>().text = nazwisko;

                listaPrzyciski.Add(osobaNieDostepnaButton);

                osobaNieDostepnaButton.onClick.AddListener(delegate { GameManager.Instance.TaskOnClick(nazwisko); });

                liczbaPracujacych += 1;
            }
            else if (liczbaPracujacych >= 6 && liczbaPracujacych < 12)
            {
                GameObject tempButton = Instantiate(guzikNieDostepny, new Vector3(-14f, -2.5f - (liczbaPracujacych - 6) * 0.5f, -23f), Quaternion.identity);
                tempButton.transform.SetParent(GameObject.FindGameObjectWithTag("ListaOsóbDoWyboru").transform, false);

                Button osobaNieDostepnaButton = tempButton.GetComponent<Button>();                

                osobaNieDostepnaButton.GetComponentInChildren<Text>().text = nazwisko;

                listaPrzyciski.Add(osobaNieDostepnaButton);

                osobaNieDostepnaButton.onClick.AddListener(delegate { GameManager.Instance.TaskOnClick(nazwisko); });

                liczbaPracujacych += 1;
            }
            else if (liczbaPracujacych >= 12)
            {
                GameObject tempButton = Instantiate(guzikNieDostepny, new Vector3(-11f, -2.5f - (liczbaPracujacych - 12) * 0.5f, -23f), Quaternion.identity);
                tempButton.transform.SetParent(GameObject.FindGameObjectWithTag("ListaOsóbDoWyboru").transform, false);

                Button osobaNieDostepnaButton = tempButton.GetComponent<Button>();

                osobaNieDostepnaButton.GetComponentInChildren<Text>().text = nazwisko;

                listaPrzyciski.Add(osobaNieDostepnaButton);

                osobaNieDostepnaButton.onClick.AddListener(delegate { GameManager.Instance.TaskOnClick(nazwisko); });

                liczbaPracujacych += 1;
            }


        }

    } 
    #endregion




}
