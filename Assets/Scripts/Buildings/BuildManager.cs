using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trait
{
    // pamiętaj że wartosci które modyfikujemy to int'y
    // Wartość możecie modyfikować tylko bez zmian nazwy zmiennej 

    //kucharz
    public const string ZAWODOWY_KUCHARZ = "Kucharz";
    public static float MODYFIKATOR_KUCHARZ_TRAIT = 1.2f;

    //leniuch
    public const string ZAWODOWY_LENIUCH = "Kalski";
    public static float MODYFIKATOR_LENIUCH_TRAIT = 2f;

    //drwal
    public const string ZAWODOWY_DRWAL = "Drwal";
    public static float MODYFIKATOR_DRWAL_TRAIT = 1.2f;  
    
    //ogrodnik
    public const string ZAWODOWY_OGRODNIK = "Ogrodnik";
    public static float MODYFIKATOR_OGRODNIK_TRAIT = 1.2f; 

    // tutaj dajcie reszte, a ja to jakoś do budynków wprowadze
}


public class Tartak
{
    public enum TierTartaku
    {
        NONE,
        TIER_1,
        TIER_2,
        TIER_3
    }

    public enum BudowaTartaku : short
    {
        DREWNO = 5,
        KAMIEN = 0,
        ZELAZO = 0,
        CZAS_BUDOWY = 4
    }

    public enum UpgradeTartakTier2 : short
    {
        DREWNO = 35,
        KAMIEN = 20,
        ZELAZO = 0,
        CZAS_BUDOWY = 8
    }

    public enum UpgradeTartakTier3 : short
    {
        DREWNO = 70,
        KAMIEN = 50,
        ZELAZO = 18,
        CZAS_BUDOWY = 12
    }

    public enum DostanSurowce : short
    {
        DREWNO_TIER_1 = 5,
        DREWNO_TIER_2 = 20,
        DREWNO_TIER_3 = 35
    }

    public static short kosztStaminy = 4;
    public static short pozostaleTuryDoBudowy = 0;
    public static short czasPracy = 2;

}// Tartak

public class PomostRybacki
{
    public enum TierPomostRybacki
    {
        NONE,
        POMOST_RYBACKI,
        CHATA_RYBACKA

    }

    public enum BudowaPomostRybacki : short
    {
        DREWNO = 30,
        KAMIEN = 0,
        ZELAZO = 0,
        CZAS_BUDOWY = 12
    }

    public enum UpgradeNaChataRybacka : short
    {
        DREWNO = 40,
        KAMIEN = 20,
        ZELAZO = 6,
        CZAS_BUDOWY = 12
    }

    public enum DostanSurowce : short
    {
        JEDZENIE_POMOST_RYBACKI = 40,
        JEDZENIE_CHATA_RYBACKA = 100
    }


    //public static short kosztStaminy = 4;
    public static short pozostaleTuryDoBudowy = 0;
    public static short czasPracy = 8;
    public static float mnoznikZKuchni = 1.2f;

}// PomostRybacki

public class Ognisko
{
    public enum RodzajOgniska
    {
        NONE,       //0
        OGNISKO,    //1
        KUCHNIA,    //2
        PIEC        //3
    }

    public enum KosztDzialnia : short
    {
        OGNISKO = 1,    
        KUCHNIA = 1,    
        PIEC = 5
    }

    public enum CzasBudowy
    {
        OGNISKO = 1,    
        KUCHNIA = 8,    
        PIEC = 8
    }

    //public static short kosztStaminy = 4;
    public static short pozostaleTuryDoBudowy = 0;

    public static short iloscZelaza = 8;
    public static short czasPracyKuznia = 4;
    public static short iloscKamieniaDoPieca = 40;



    //bonusy z ogniska itp.
    public static bool ogniskoBonus;
    public static bool kuchniaBonus;

}// Ognisko

public class Legowisko
{
    public enum RodzajLegowiska
    {
        NONE,
        LEGOWISKO,
        SYPIALNIA
    }

    public enum BudowaLegowiska
    {
        DREWNO = 40,
        KAMIEN = 0,
        ZELAZO = 0,
        CZAS_BUDOWY = 4
    }

    public enum BudowaSypialni
    {
        DREWNO = 60,
        KAMIEN = 20,
        ZELAZO = 4,
        CZAS_BUDOWY = 12
    }

    //public static short kosztStaminy = 4;
    public static short pozostaleTuryDoBudowy = 0;

}// Legowisko

public class Ogrod
{
    public enum RodzajOgrodu
    {
        NONE,
        OGROD,
        PLANTACJA,
    }

    public enum BudowaLegowiska : short
    {
        DREWNO = 20,
        KAMIEN = 0,
        ZELAZO = 0,
        CZAS_BUDOWY = 4
    }

    public enum BudowaSypialni : short
    {
        DREWNO = 80,
        KAMIEN = 30,
        ZELAZO = 12,
        CZAS_BUDOWY = 12
    }

    public enum Praca : short
    {
        CZAS_PRACY_OGROD = 12,
        PRODUKCJA_OGROD = 40,

        CZAS_PRACY_PLANTACJA = 12,
        PRODUKCJA_PLANTACJA = 90

    }

    public enum KosztDzialania
    {
        WODA_OGROD = 1,
        STAMINA_OGROD = 2,

        WODA_PLANTACJA = 2,
        STAMIN_PLANTACJA = 2
    }

    //public static short kosztStaminy = 4;
    public static short pozostaleTuryDoBudowy = 0;

}//Ogrod


public class ZbierakNaWode
{
    public enum RodzajZbierakaNaWode
    {
        NONE,
        ZBIERAK_NA_WODE,
        STUDNIA,
        ZBIORNIK
    }

    public enum BudowaZbierakaNaWode : short
    {
        DREWNO = 10,
        KAMIEN = 0,
        ZELAZO = 0,
        CZAS_BUDOWY = 1
    }

    public enum UpgradeStudnia : short
    {
        DREWNO = 20,
        KAMIEN = 40,
        ZELAZO = 0,
        CZAS_BUDOWY = 8
    }

    public enum UpgradeZbiornik : short
    {
        DREWNO = 80,
        KAMIEN = 80,
        ZELAZO = 24,
        CZAS_BUDOWY = 16
    }

    public enum DostanSurowce : short
    {
        WODA_Z_ZBIERAKA_NA_WODE = 15,
        WODA_Z_STUDNI = 80,
        WODA_Z_ZBIORNIKA = 160 
    }

    public enum CzasPracy
    {
        ZBIERAK_NA_WODE = 4,
        STUDNIA = 4,
        ZBIORNIK = 4
    }

    public static short pozostaleTuryDoBudowy = 0;

}// Zbierak na wode

public class Warsztat
{
    public enum RodzajWarsztatu
    {
        NONE,
        WARSZTAT,
        KUZNIA
    }

    public enum CzasBudowy
    {
        WARSZTAT = 12,
        KUZNIA = 20
    }


}// Warsztat








