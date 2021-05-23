using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//ni pacz pls
public class Gathering : MonoBehaviour
{
    public List<CharacterCreator> pracownik_ekspedycji;
    public void ZbieranieChrusty()
    {
        foreach (var worker in pracownik_ekspedycji)
        {
            if (worker.czyPracuje == false ||
                worker.tagPracy == ZbieranieChrustu.tagPracy)
            {
                worker.tagPracy = ZbieranieChrustu.tagPracy;
                worker.czyPracuje = true;
            }
            else
            {
                
            }
        }
    }
}
