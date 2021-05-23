using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZbieranieChrustu
{
    public static short iloscDrewnaZaMisje = 5;
    public const string tagPracy = "ZBIERAM CHRUST";
    public static short czasMisji = 2;
    public static List<CharacterCreator> pracownik_ekspedycji;
}

public class ZbieranieKamienia
{
    public static short iloscKamieniaZaMisje = 5;
    public const string tagPracy = "ZBIERAM KAMIEN";
    public static short czasMisji = 2;
    public static List<CharacterCreator> pracownik_ekspedycji;
}

public class Umarlylas
{
    public static short iloscDrewnaZaMisje = 50;
    public const string tagPracy = "ZBIERAM DREWNO Z LASU";
    public static short czasMisji = 2;
    public static List<CharacterCreator> pracownik_ekspedycji;
}

public class Osuwisko
{
    public static short iloscKamieniaZaMisje = 30;
    public const string tagPracy = "ZBIERAM KAMIEN Z OSUWISKA";
    public static short czasMisji = 2;
    public static List<CharacterCreator> pracownik_ekspedycji;
}

public class OdlamekSamolotu
{
    public enum Surowce
    {
        JEDZENIE = 20,
        WODA = 30,
        ZELAZO = 10
    }
    public const string tagPracy = "Odlamek Samolotu";
    public static short czasMisji = 2;
    public static List<CharacterCreator> pracownik_ekspedycji;
}


public class ZrodelkoWody
{
    public static short iloscWodyZaMisje = 120;
    public const string tagPracy = "Zrodelko Wody";
    public static short czasMisji = 2;
    public static List<CharacterCreator> pracownik_ekspedycji;
}

public class DrzewoKokosowe
{
    public enum Surowce
    {
        JEDZENIE = 50,
        WODA = 100,
        DREWNO = 20
    }
    public const string tagPracy = "Drzewo Kokosowe";
    public static short czasMisji = 2;
    public static List<CharacterCreator> pracownik_ekspedycji;
}

public class Grzyby
{
    public enum Surowce
    {
        JEDZENIE = 20
    }
    public const string tagPracy = "Grzyby";
    public static short czasMisji = 2;
    public static List<CharacterCreator> pracownik_ekspedycji;
}

