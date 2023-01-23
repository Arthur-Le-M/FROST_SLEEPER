using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerScript : MonoBehaviour
{
    //Public
    public GameObject gameManager;
    public int step;
    public int vieMax = 3;
    public int vie;
    public AudioClip sonPas;
    public AudioClip sonDegat;

    //Private
    GameManager scriptManager;
    private string movementPrecedent;
    private Animator anim;
    private AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        scriptManager = gameManager.GetComponent<GameManager>();
        vie = vieMax;
        anim = GetComponentInChildren<Animator>();
        anim.SetBool("isMoving", false);
        AS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        scriptManager.afficherVie(vie);
        //Mouvement
        playerMovement();

        
    }

    //Déclaration variable global pour la fonction playerMouvement
    Vector3 newPos;
    Quaternion newRot;
    public bool isMovable = true;
    public GameObject mesh;
    private int dir = 0;

    //Fonction de mouvement du player
    void playerMovement(){
        
        if (Input.GetKey(KeyCode.RightArrow) && isMovable){
            anim.SetBool("isMoving", true);
            movementPrecedent = "droite";
            newPos = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
            if (dir != 0)
            {
                newRot = new Quaternion(0, 0, 0, transform.rotation.w);
                dir = 0;
            }
            AS.clip = sonPas;
            AS.Play();
            isMovable = false;

        }
        else if(Input.GetKey(KeyCode.LeftArrow)&& isMovable){
            anim.SetBool("isMoving", true);
            movementPrecedent = "gauche";
            newPos = new Vector3(transform.position.x - step, transform.position.y, transform.position.z);
            AS.clip = sonPas;
            AS.Play();
            if (dir != 1) {
                newRot = new Quaternion(0, 180, 0, transform.rotation.w);
                dir = 1;
            }
            
            isMovable = false;
        }
        else if(Input.GetKey(KeyCode.UpArrow)&& isMovable){
            anim.SetBool("isMoving", true);
            movementPrecedent = "haut";
            AS.clip = sonPas;
            AS.Play();
            newPos = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
            isMovable = false;
        }
        else if(Input.GetKey(KeyCode.DownArrow)&& isMovable){
            anim.SetBool("isMoving", true);
            movementPrecedent = "bas";
            AS.clip = sonPas;
            AS.Play();
            newPos = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
            isMovable = false;
        }
        //Vérification de la sortie de map
        if (newPos.x < scriptManager.mapWidth && newPos.y < scriptManager.mapHeigt && newPos.x > -scriptManager.mapWidth && newPos.y > -scriptManager.mapHeigt)
        {
            transform.position = Vector3.Lerp(transform.position, newPos, 10f * Time.deltaTime);
            if(dir == 0) {
                mesh.GetComponent<Transform>().transform.rotation = Quaternion.Lerp(mesh.GetComponent<Transform>().transform.rotation, newRot, 10f * Time.deltaTime);
            }
            else if(dir == 1)
            {
                mesh.GetComponent<Transform>().transform.rotation = Quaternion.Lerp(mesh.GetComponent<Transform>().transform.rotation, newRot, 0.5f * Time.deltaTime);
            }
            
            //mesh.GetComponent<Transform>().transform.rotation = Quaternion.Lerp(mesh.GetComponent<Transform>().transform.rotation, newRot, 10f * Time.deltaTime);
        }
        else
        {
            //Bordure
            newPos = transform.position;
        }

        //On arrête l'objet à la fin de l'interpolation
        if (Vector3.Distance(transform.position, newPos) < 0.05f)
        {
            transform.position = newPos;
        }

        //Vérification de l'état du mouvement
        if (transform.position == newPos)
        {
            isMovable = true;
            anim.SetBool("isMoving", false);
        }
    }

    public void perdreVie(int nb)
    {
        AS.clip = sonDegat;
        AS.Play();
        vie -= nb;
        if (vie <= 0)
        {
            scriptManager.GameOver();
        }        
    }

    public void revenirPrecedent()
    {
        if(movementPrecedent == "droite")
        {
            newPos = new Vector3(transform.position.x - step, transform.position.y, transform.position.z);
            isMovable = false;
        }
        else if (movementPrecedent == "gauche")
        {
            newPos = new Vector3(transform.position.x + step, transform.position.y, transform.position.z);
            isMovable = false;
        }
        else if (movementPrecedent == "bas")
        {
            newPos = new Vector3(transform.position.x, transform.position.y + step, transform.position.z);
            isMovable = false;
        }
        else if (movementPrecedent == "haut")
        {
            newPos = new Vector3(transform.position.x, transform.position.y - step, transform.position.z);
            isMovable = false;
        }
    }
}
