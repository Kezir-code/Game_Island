using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ni pacz pls
public class Gathering : MonoBehaviour
{
    public GameManager gameManager = GameManager.Instance;
    public void MisjaZbieranieChrustu()
    {
        foreach (CharacterCreator worker in ZbieranieChrustu.pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == ZbieranieChrustu.tagPracy)
            {
                worker.tagPracy = ZbieranieChrustu.tagPracy;
                worker.czyPracuje = true;
                worker.turyDoKoncaPracy = ZbieranieChrustu.czasMisji;
            }
            else if (worker.turyDoKoncaPracy == 0)
            {
                gameManager.drewno += ZbieranieChrustu.iloscDrewnaZaMisje;
            }
            else
            {
                worker.turyDoKoncaPracy++;
            }

        }
    }//MisjaZbieranieChrustu

    public void MisjaZbieranieKamienia()
    {
        foreach (CharacterCreator worker in ZbieranieKamienia.pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == ZbieranieKamienia.tagPracy)
            {
                worker.tagPracy = ZbieranieKamienia.tagPracy;
                worker.czyPracuje = true;
                worker.turyDoKoncaPracy = ZbieranieKamienia.czasMisji;
            }
            else if (worker.turyDoKoncaPracy == 0)
            {
                gameManager.kamien += ZbieranieKamienia.iloscKamieniaZaMisje;
            }
            else
            {
                worker.turyDoKoncaPracy++;
            }

        }
    }//MisjaZbieranieKamienia

    public void MisjaUmarlylas()
    {
        foreach (CharacterCreator worker in Umarlylas.pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == Umarlylas.tagPracy)
            {
                worker.tagPracy = Umarlylas.tagPracy;
                worker.czyPracuje = true;
                worker.turyDoKoncaPracy = Umarlylas.czasMisji;
            }
            else if (worker.turyDoKoncaPracy == 0)
            {
                gameManager.drewno += Umarlylas.iloscDrewnaZaMisje;
            }
            else
            {
                worker.turyDoKoncaPracy++;
            }

        }
    }//Misja Umarly Las

    public void MisjaOsuwisko()
    {
        foreach (CharacterCreator worker in Osuwisko.pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == Osuwisko.tagPracy)
            {
                worker.tagPracy = Osuwisko.tagPracy;
                worker.czyPracuje = true;
                worker.turyDoKoncaPracy = Osuwisko.czasMisji;
            }
            else if (worker.turyDoKoncaPracy == 0)
            {
                gameManager.drewno += Osuwisko.iloscKamieniaZaMisje;
            }
            else
            {
                worker.turyDoKoncaPracy++;
            }

        }
    }//Misja Osuwisko

    public void MisjaOdlamekSamolotu()
    {
        foreach (CharacterCreator worker in OdlamekSamolotu.pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == OdlamekSamolotu.tagPracy)
            {
                worker.tagPracy = OdlamekSamolotu.tagPracy;
                worker.czyPracuje = true;
                worker.turyDoKoncaPracy = OdlamekSamolotu.czasMisji;
            }
            else if (worker.turyDoKoncaPracy == 0)
            {
                gameManager.jedzenie += (short)OdlamekSamolotu.Surowce.JEDZENIE;
                gameManager.woda += (short)OdlamekSamolotu.Surowce.WODA;
                gameManager.zelazo += (short)OdlamekSamolotu.Surowce.ZELAZO;
            }
            else
            {
                worker.turyDoKoncaPracy++;
            }

        }
    }//Misja Odlamek Samolotu

    public void MisjaZrodelkoWody()
    {
        foreach (CharacterCreator worker in ZrodelkoWody.pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == ZrodelkoWody.tagPracy)
            {
                worker.tagPracy = ZrodelkoWody.tagPracy;
                worker.czyPracuje = true;
                worker.turyDoKoncaPracy = ZrodelkoWody.czasMisji;
            }
            else if (worker.turyDoKoncaPracy == 0)
            {
                gameManager.woda += ZrodelkoWody.iloscWodyZaMisje;
            }
            else
            {
                worker.turyDoKoncaPracy++;
            }

        }
    }//Misja Zrodelko Wody

    public void MisjaDrzewoKokosowe()
    {
        foreach (CharacterCreator worker in DrzewoKokosowe.pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == DrzewoKokosowe.tagPracy)
            {
                worker.tagPracy = DrzewoKokosowe.tagPracy;
                worker.czyPracuje = true;
                worker.turyDoKoncaPracy = DrzewoKokosowe.czasMisji;
            }
            else if (worker.turyDoKoncaPracy == 0)
            {
                gameManager.jedzenie += (short)DrzewoKokosowe.Surowce.JEDZENIE;
                gameManager.woda += (short)DrzewoKokosowe.Surowce.WODA;
                gameManager.drewno += (short)DrzewoKokosowe.Surowce.DREWNO;
            }
            else
            {
                worker.turyDoKoncaPracy++;
            }

        }
    }//Misja Drzewo Kokosowe

    
    public void MisjaGrzyby()
    {
        foreach (CharacterCreator worker in Grzyby.pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == Grzyby.tagPracy)
            {
                worker.tagPracy = Grzyby.tagPracy;
                worker.czyPracuje = true;
                worker.turyDoKoncaPracy = Grzyby.czasMisji;
            }
            else if (worker.turyDoKoncaPracy == 0)
            {
                gameManager.jedzenie += (short)Grzyby.Surowce.JEDZENIE;
            }
            else
            {
                worker.turyDoKoncaPracy++;
            }

        }
    }//Misja Grzyby

}//class
