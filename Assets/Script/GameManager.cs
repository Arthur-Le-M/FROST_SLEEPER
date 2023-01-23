using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public float mapWidth = 100;
    public float mapHeigt = 100;
    public bool isUnlocked;
    public bool isDiscovered;
    public GameObject fogPrefab;
    public GameObject objectKey;
    public GameObject UICanvas;
    public float dureePartie;
    public string sceneSuivante;
    
    
    //Private
    private GameObject fogObject;
    private bool isFogged;
    private bool isDissiped;
    private UIScript ui;
    private float actualPartie;
    // Start is called before the first frame update
    void Start()
    {
        //Assignation de base
        isUnlocked = false;
        isDiscovered = false;
        isFogged = false;
        isDissiped = false;
        objectKey.SetActive(false);
        ui = UICanvas.GetComponent<UIScript>();
        actualPartie = dureePartie;
    }

    // Update is called once per frame
    void Update()
    {
        //Timer
        TimerPartie();
        //Fog
        if (isFogged)
        {
            fog();
            if (isDissiped)
            {
                dissipFog(fogObject);
            }
        }
    }
    
    public void fogSpawn(){
        isFogged = true;
        fogObject = Instantiate<GameObject>(fogPrefab);
        fogObject.GetComponent<SpriteRenderer>().color = new Color(fogObject.GetComponent<SpriteRenderer>().color.r, fogObject.GetComponent<SpriteRenderer>().color.g, fogObject.GetComponent<SpriteRenderer>().color.b, 0f);
        fogObject.transform.position = new Vector3(0, 0, -5);
        //Arrivé du brouillard
        StartCoroutine(FogFadeIn());
    }
    IEnumerator FogFadeIn()
    {
        float targetAlpha = 1f;
        float speed = 2f;
        float currentAlpha = fogObject.GetComponent<SpriteRenderer>().color.a;
        while (currentAlpha < targetAlpha)
        {
            if(targetAlpha - currentAlpha < 0.001f)
            {
                currentAlpha = targetAlpha;
            }
            currentAlpha = Mathf.Lerp(currentAlpha, targetAlpha, speed * Time.deltaTime);
            fogObject.GetComponent<SpriteRenderer>().color = new Color(fogObject.GetComponent<SpriteRenderer>().color.r, fogObject.GetComponent<SpriteRenderer>().color.g, fogObject.GetComponent<SpriteRenderer>().color.b, currentAlpha);
            yield return new WaitForEndOfFrame();
        }
    }


    public void exit()
    {
        if (isDiscovered)
        {
            if (isUnlocked)
            {
                //Exit
                Debug.Log("Sortie");
                ui.completed(ui.quete3);
                SceneManager.LoadScene(sceneSuivante);
            }
            else
            {
                Debug.Log("Pas encore unlock");
            }
        }
        else
        {
            isDiscovered = true;
            ui.completed(ui.quete1);
            objectKey.SetActive(true);
            exit();
        }
    }


    public void setCollected()
    {
        isUnlocked = true;
        ui.completed(ui.quete2);
    }

    //Fog
    private float actualFog = 0f;
    private float tempsAleatoireFog = 4f;

    private void fog()
    {
        //Timer
        //Incrémention
        actualFog += 1f * Time.deltaTime;
        //Condition d'arrêt
        if(actualFog > tempsAleatoireFog)
        {
            actualFog = 0;
            tempsAleatoireFog = Random.Range(2f, 6f);
            isDissiped = true;
        }
    }

    private float actualDissip = 0f;
    private float dureeDissipation = 0.5f;
    private void dissipFog(GameObject fogObj)
    {
        if(actualDissip < dureeDissipation / 2)
        {
            fogObj.GetComponent<SpriteRenderer>().color = new Color(fogObj.GetComponent<SpriteRenderer>().color.r, fogObj.GetComponent<SpriteRenderer>().color.g, fogObj.GetComponent<SpriteRenderer>().color.b, fogObj.GetComponent<SpriteRenderer>().color.a - 0.5f * Time.deltaTime);
        }
        else
        {
            fogObj.GetComponent<SpriteRenderer>().color = new Color(fogObj.GetComponent<SpriteRenderer>().color.r, fogObj.GetComponent<SpriteRenderer>().color.g, fogObj.GetComponent<SpriteRenderer>().color.b, fogObj.GetComponent<SpriteRenderer>().color.a + 0.5f * Time.deltaTime);
        }
        actualDissip += 1f * Time.deltaTime;
        if(actualDissip > dureeDissipation)
        {
            actualDissip = 0f;
            dureeDissipation = Random.Range(1f, 2f);
            isDissiped = false;
            fogObj.GetComponent<SpriteRenderer>().color = new Color(fogObj.GetComponent<SpriteRenderer>().color.r, fogObj.GetComponent<SpriteRenderer>().color.g, fogObj.GetComponent<SpriteRenderer>().color.b, 1);
        }
    }

    
    public void TimerPartie()
    {
        actualPartie -= 1f * Time.deltaTime;
        ui.setTextTimer(((int)actualPartie).ToString());
        if(actualPartie <= 0f)
        {
            //Game over
            GameOver();
        }
    }

    public void afficherVie(int nbVie)
    {
        ui.setVie(nbVie);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
