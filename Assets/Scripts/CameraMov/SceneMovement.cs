using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMovement : MonoBehaviour
{
    [Header("Screen change")]
    public Transform[] screensAnchors;
    public GameObject[] screenUI;
    private GameObject activeScreen;
    private Camera camera;

    void Start()
    {
        camera = CameraMovement.Instance.GetComponent<Camera>();
        activeScreen = screenUI[1];
    }

    public void MoveCameraMapa()
    {
        camera.transform.GetComponent<CameraMovement>().target = screensAnchors[0].position;
        SetActiveScreen(screenUI[0]);
    }

    public void MoveCameraScreen1()
    {
        camera.transform.GetComponent<CameraMovement>().target = screensAnchors[1].position;
        SetActiveScreen(screenUI[1]);
    }

    private void SetActiveScreen(GameObject name)
    {
        activeScreen.SetActive(false);
        activeScreen = name;
        activeScreen.SetActive(true);

    }
}
