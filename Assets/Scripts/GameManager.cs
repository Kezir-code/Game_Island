using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class GameManager : Singleton<GameManager>
{
    private GameObject activeScreen;
    private Camera camera;
    public List<GameObject> people;
    //public List<GameObject> people = Resources.LoadAll<GameObject>("Characters").ToList();
    public int drewno;
    public int woda;
    public int dzien;
    public int pora_dnia;
    public int stamina;
    public int kamien;
    public int jedzenie;
    void Start()
    {
        camera = CameraMovement.Instance.GetComponent<Camera>();
        GetAttributes();
    }
   // Update is called once per frame
    void Update()
    {
        people = Resources.LoadAll<GameObject>("Characters").ToList();
    }

    public void Zmiana_Pory_Dnia()
    {
        pora_dnia = (pora_dnia + 1) % 3;
    }

    public void Zmiana_kamien(int kamien_update)
    {
        int result = kamien + kamien_update;
        if (result >= 0)
        {
            kamien = result;
        }
    }

    public void Zmiana_wody(int woda_update)
    {
        int result = woda + woda_update;
        if (result >= 0)
        {
            woda = result;
        }
    }
    public void Zmiana_drewno(int drewno_update)
    {
        int result = drewno + drewno_update;
        if (result >= 0)
        {
            drewno = result;
        }
    }
    public void Zmiana_dnia()
    {
        dzien++;
    }
    public void GetAttributes()
    {
        for (int i = 0; i < people.Count; i++)
        {
            people[i].GetComponent<CharacterCreator>();
            Debug.Log(name);
        }
        
    }
    public void PlaceCharacter(GameObject character, Vector3 pocision)
    {
        
    }
    

}
