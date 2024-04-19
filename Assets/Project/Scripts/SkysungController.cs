using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SkysungController : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    //Eliminar después ^^^

    public Transform playerObj;

    public float speed = 0f;
    public float rotationSpeed = 0f;

    public Animator skysungAnimator;
    private Rigidbody rb;
    

    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Lock;
        //Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   
    }

    // Update is called once per frame
    void Update()
    {
        //Movimiento horizontal y vertical del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //Se crea un vector que tiene como valores la vista de frente e izquierda dada por la cámara, junto con los valores de entrada vertical y horizontal
        Vector3 inputDir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up) * verticalInput 
                           + Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up) * horizontalInput;

        //Cuando se encuentre en movieminto el personaje, se activará la animación Walk y se rotará el movimiento y modelo según el frente de la cámara 
        if (inputDir != Vector3.zero)
        {
            skysungAnimator.SetBool("Walk", true);
            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up));
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
            

        }
        else
        {
            skysungAnimator.SetBool("Walk", false);
        }

        //cre un movimiento con rigidbody, usando las entradas vertical y horizontal
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(movement));

    }
}
