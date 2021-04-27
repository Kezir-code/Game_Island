using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Transform[] screens;
    public GameObject[] screenUI;
    private GameObject activeScreen;
    private Camera camera;
    public List<GameObject> people;
    public int drewno;
    public int pora_dnia;
    public int stamina;
    public int kamien;
    public int jedzenie;
    void Start()
    {
        camera = CameraMovement.Instance.GetComponent<Camera>();
        activeScreen = screenUI[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MoveCameraMapa()
    {
        camera.transform.GetComponent<CameraMovement>().target = screens[0].position;
        SetActiveScreen(screenUI[0]);
    }

    public void MoveCameraScreen1()
    {
        camera.transform.GetComponent<CameraMovement>().target = screens[1].position;
        SetActiveScreen(screenUI[1]);
    }

    private void SetActiveScreen(GameObject name)
    {
        activeScreen.SetActive(false);
        activeScreen = name;
        activeScreen.SetActive(true);

    }

}
