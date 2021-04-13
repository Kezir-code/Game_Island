using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellScript : MonoBehaviour
{
    /// <summary>
    /// 
    /// <Za�o�enia>
    /// Na razie produkujemy pojedyncze surowce    
    /// # Je�eli nast�pi zmiana pozmieniaj w switch'u surowce++
    /// Standardowy czas to 50 s   
    /// # zmieni� w pracowniku("Worker") 
    /// Level budunku zwieksza liczbe slot�w pracownik�w i skraca czas pracy do 50%    
    /// # Na razie jest 5 poziom�w. Je�eli nast�pi zmiana, dodaj w switch'u case'y
    /// Jeden tick pracy zu�ywa 5 staminy
    /// Worker ma 100 staminy
    /// Na razie bez skilli kt�re przyspieszaj�
    /// </Za�o�enia>
    /// 
    /// <B�edy> lub potencjalne zagro�enia
    /// Na razie obs�uguje tylko jednego pracownika
    /// Co w sprawie zapisu i wczytywania gry?
    /// </B�edy>
    /// 
    /// <Optymalizacja>
    /// Je�eli gracz nie wybudowa� jeszcze jednego z trzech budynk�w skrypt bedzie dzia�a�? # Nie poniewa� skrypt nie zosta� jeszcze przypisany
    /// </Optymalizacja> 
    /// 
    /// <Pytania_do_mnie> Nie czyta�
    /// Czy da si� przypisa� wyrzucany surowiec?
    /// Da sie tylko trzeba macierz 2x3 zrobi� co raczej by�o by trudne do odczytania i operowania
    /// Wi�c daj 3 zmienne lub 3 osobne skrypty
    /// # Done! zrobi�em 3 osobne skrypty
    /// 
    /// 
    /// </Pytania_do_mnie>
    /// 
    /// </summary>


    public short currentlevel = 0; // max 5 je�eli wiecej trzeba dopisa� do switch'a

    //public List<GameObject> worker; // wstawi� tutaj pracownika i jego dane takie jak stamina czy pr�dko�� pracy # na razie problem

    public short stamina = 0;  // Je�eli wczytujesz sam skrypt zmie� warto�� i zakomentuj przypisanie
    public float timework = 0;  // Dla zespo�u zajmuj�cego si� konceptem ustali� jaki powinnien by�
    public short sloty;  // miejsce dla pracownika
    public int filteredWater;
    public short tick = 5;


    // Start is called before the first frame update
    void Start()
    {
        // Przy samym wczytaniu skryptu zakomentuj 4 linie w d�
        GameObject pracownik = GameObject.Find("Pracownik"); // tu bedzie problem. Nie ka�dy bedzi� mia� na imie pracownik plus nie zawsze bedzie tylko jeden
        Worker worker = pracownik.GetComponent<Worker>();
        stamina = worker.stamina; // Je�eli wczytujesz sam skrypt zakomentuj dan� linijke kodu. Odkomentuj wtedy zmien� 
        timework = worker.workSpeed; // Je�eli wczytujesz sam skrypt zakomentuj dan� linijke kodu. Odkomentuj wtedy zmien� 

        StartCoroutine(production());
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Ilo�� Przefitrowanej wody: " + filteredWater);
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
                    Debug.Log("Pracownik zakonczy� prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                case 2: // level 2 przyspiesza prace o 10% dla jednego pracownika
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds((float)(timework * 0.9)); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy� prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                case 3: // level 3 przyspiesza prace o 20% dla jednego pracownika
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds((float)(timework * 0.8)); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy� prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                case 4:// level 4 przyspiesza prace o 30% dla jednego pracownika
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds((float)(timework * 0.7)); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy� prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                case 5: // level 5 przyspiesza prace o 50% dla jednego pracownika
                    Debug.Log("Pracownik pracuje obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    yield return new WaitForSeconds((float)(timework * 0.5)); // Poczekaj 
                    filteredWater++;
                    stamina = (short)(stamina - tick);
                    Debug.Log("Pracownik zakonczy� prace obecna stamina to: " + stamina + " A jego czas pracy wynosi" + timework);
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
