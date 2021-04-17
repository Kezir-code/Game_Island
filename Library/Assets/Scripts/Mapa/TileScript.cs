using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour
{
    public Point GridPosition { get; private set; }

    public bool IsVisible { get; private set; }
    private SpriteRenderer sprite;

    // atrybuty 
    private int jedzenie;
    private bool kamien;
    private bool zelazo;
    private int losowanie;


    // Start is called before the first frame update
    void Start()
    {
        IsVisible = false;
        sprite = gameObject.transform.GetComponent<SpriteRenderer>();
        // jedzenie
        losowanie = Random.Range(0, 100);
        if(losowanie < 50)
        {
            jedzenie = Random.Range(1, 4);
        }

        // kamien
        losowanie = Random.Range(0, 100);
        if (losowanie < 30)
        {
            kamien = true;
        }

        // zelazo
        losowanie = Random.Range(0, 100);
        if (losowanie < 10)
        {
            zelazo = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Setup(Point gridPos, Vector3 worldPos, Transform parent)
    {
        this.GridPosition = gridPos;
        transform.position = worldPos;
        transform.SetParent(parent);

        LevelManager.Instance.Tiles.Add(gridPos, this);

    }

    private void Fade()
    {
        StartCoroutine(FadeInTime(2.0f));
    }

    private IEnumerator FadeInTime(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            print("WaitAndPrint " + Time.time);
        }
    }
    private void OnMouseDown()
    {
        // jezeli IsVisible == true
        IsVisible = true;
        Debug.Log(jedzenie + " " + kamien + " " + zelazo);       
    }

}
