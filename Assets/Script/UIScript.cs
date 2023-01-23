using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class UIScript : MonoBehaviour
{
    public TMP_Text textTimer;
    public TMP_Text quete1;
    public TMP_Text quete2;
    public TMP_Text quete3;
    public TMP_Text textVie;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTextTimer(string text)
    {
        textTimer.SetText(text);
    }
    public void completed(TMP_Text quete)
    {
        quete.color = Color.cyan;
    }

    public void setVie(int nbVie) {
        textVie.SetText("Vie : " + nbVie.ToString());
    }


}
