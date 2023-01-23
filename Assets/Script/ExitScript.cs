using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitScript : MonoBehaviour
{
    //Public
    public GameObject gameManager;

    //Private
    GameManager scriptManager;
    // Start is called before the first frame update
    void Start()
    {
        scriptManager = gameManager.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Oui oui baguette");
        if(collision.tag == "Player")
        {

            scriptManager.exit();
        }
    }

}
