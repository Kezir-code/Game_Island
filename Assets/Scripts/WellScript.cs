using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellScript : MonoBehaviour
{
    /// <summary>
    /// 
    /// <Za³o¿enia>
    /// Na razie produkujemy pojedyncze surowce    
    /// # Je¿eli nast¹pi zmiana pozmieniaj w switch'u surowce++
    /// Standardowy czas to 50 s   
    /// # zmieniæ w pracowniku("Worker") 
    /// Level budunku zwieksza liczbe slotów pracowników i skraca czas pracy do 50%    
    /// # Na razie jest 5 poziomów. Je¿eli nast¹pi zmiana, dodaj w switch'u case'y
    /// Jeden tick pracy zu¿ywa 5 staminy
    /// Worker ma 100 staminy
    /// Na razie bez skilli które przyspieszaj¹
    /// </Za³o¿enia>
    /// 
    /// <B³edy> lub potencjalne zagro¿enia
    /// Na razie obs³uguje tylko jednego pracownika
    /// Co w sprawie zapisu i wczytywania gry?
    /// </B³edy>
    /// 
    /// <Optymalizacja>
    /// Je¿eli gracz nie wybudowa³ jeszcze jednego z trzech budynków skrypt bedzie dzia³a³? # Nie poniewa¿ skrypt nie zosta³ jeszcze przypisany
    /// </Optymalizacja> 
    /// 
    /// <Pytania_do_mnie> Nie czytaæ
    /// Czy da siê przypisaæ wyrzucany surowiec?
    /// Da sie tylko trzeba macierz 2x3 zrobiæ co raczej by³o by trudne do odczytania i operowania
    /// Wiêc daj 3 zmienne lub 3 osobne skrypty
    /// # Done! zrobi³em 3 osobne skrypty
    /// 
    /// 
    /// </Pytania_do_mnie>
    /// 
    /// </summary>


    public short currentlevel = 0; // max 5 je¿eli wiecej trzeba dopisaæ do switch'a

    //public List<GameObject> worker; // wstawiæ tutaj pracownika i jego dane takie jak stamina czy prêdkoœæ pracy # na razie problem

    public short stamina = 0;  // Je¿eli wczytujesz sam skrypt zmieñ wartoœæ i zakomentuj przypisanie
    public float timework = 0;  // Dla zespo³u zajmuj¹cego siê konceptem ustaliæ jaki powinnien byæ
    public short sloty;  // miejsce dla pracownika
    public int filteredWater;
    public short tick = 5;


    // Start is called before the first frame update
    void Start()
    {
        // Przy samym wczytaniu skryptu zakomentuj 4 linie w dó³
        GameObject pracownik = GameObject.Find("Pracownik"); // tu bedzie problem. Nie ka¿dy bedziê mia³ na imie pracownik plus nie zawsze bedzie tylko jeden
        Worker worker = pracownik.GetComponent<Worker>();
        stamina = worker.stamina; // Je¿eli wczytujesz sam skrypt zakomentuj dan¹ linijke kodu. Odkomentuj wtedy zmien¹ 
        timework = worker.workSpeed; // Je¿eli wczytujesz sam skrypt zakomentuj dan¹ linijke kodu. Odkomentuj wtedy zmien¹ 

        StartCoroutine(production());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Iloœæ Przefitrowanej wody: " + filteredWater);
    }

    #region Produkcja

    IEnumerator production()
    {
        while (stamina > 0)
        {
            switch (currentlevel)
            {
                case 1: // level 1
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds(timework); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy³ prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                case 2: // level 2 przyspiesza prace o 10% dla jednego pracownika
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds((float)(timework * 0.9)); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy³ prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                case 3: // level 3 przyspiesza prace o 20% dla jednego pracownika
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds((float)(timework * 0.8)); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy³ prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                case 4:// level 4 przyspiesza prace o 30% dla jednego pracownika
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds((float)(timework * 0.7)); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy³ prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                case 5: // level 5 przyspiesza prace o 50% dla jednego pracownika
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds((float)(timework * 0.5)); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy³ prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
