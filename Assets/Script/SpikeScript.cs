using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool enter = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "Player"))
        {
            collision.gameObject.GetComponent<PlayerScript>().perdreVie(1);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.gameObject.GetComponent<PlayerScript>().isMovable && enter == false)
        {
            collision.gameObject.GetComponent<PlayerScript>().revenirPrecedent();
            enter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        enter = false;
    }
}
