using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public float allMunicion; //Toda la munición del arma en suelo
    public float currentMunicion; //La munición actual con la que se quedó el arma


    private BoxCollider col;
    private Component rb;
    public string weaponType;
    public LayerMask ignoreLayer;
    public GameObject particlesPrefabFront;
    public GameObject particlesPrefabBack;

    private Vector3 groundLeft;
    private Vector3 groundRigt;
    private Vector3 groundBack;
    private Vector3 groundForward;

    public void Start()
    {
        col = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
    }

    public void Update()
    {

        groundLeft = transform.TransformDirection(Vector3.left);
        groundRigt = transform.TransformDirection(Vector3.right);
        groundBack = transform.TransformDirection(Vector3.back);
        groundForward = transform.TransformDirection(Vector3.forward);

        if (weaponType == "DroneCanon")
        {
            if ((Physics.Raycast(transform.position, groundLeft, .07f, ~ignoreLayer) || (Physics.Raycast(transform.position, groundRigt, .07f, ~ignoreLayer))))
            {
                //Debug.Log("DESTRUCCION DE RIGI del Dronen");
                Destroy(rb);
                col.isTrigger = true;
                col.size = new Vector3(1f, 1f, 1f);
                activeParticles();
            }
        }
        else if (weaponType == "RifleA34")
        {
            if ((Physics.Raycast(transform.position, groundBack, .08f, ~ignoreLayer) || (Physics.Raycast(transform.position, groundForward, .08f, ~ignoreLayer))))
            {
                //Debug.Log("DESTRUCCION DE RIGI del RA34");
                Destroy(rb);
                col.isTrigger = true;
                col.size = new Vector3(1f, 1f, 1f);
                activeParticles();
            }
        }
        else
        {
            //Debug.Log("cayendo");
            col.isTrigger = false;
        }
    }

    public void activeParticles()
    {
        if ((Physics.Raycast(transform.position, groundLeft, .07f, ~ignoreLayer) || (Physics.Raycast(transform.position, groundForward, .08f, ~ignoreLayer))))
            particlesPrefabFront.SetActive(true);
        else
            particlesPrefabBack.SetActive(true);
    }
}
