using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkysungController : MonoBehaviour
{
    public Transform orientation;
    public Transform player;
    public Transform playerObj;


    public float speed = 0f;
    public float rotationSpeed = 0f;

    private Vector3 moveDirection;
    private Vector3 sideMovement;
    private Vector3 tmpGravity;

    public Animator skysungAnimator;
    private Rigidbody rb;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //skysungAnimator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;   
    }

    // Update is called once per frame
    void Update()
    {
        //Rotación de orientación
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y,transform.position.z);
        orientation.forward = viewDir.normalized;

        //Ritación del modelo
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (inputDir != Vector3.zero)
        {
            skysungAnimator.SetBool("Walk", true);
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
        }
        else
        {
            skysungAnimator.SetBool("Walk", false);
        }
        rb.AddForce(inputDir.normalized * speed * 10f, ForceMode.Force);


        /*Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

        transform.position = transform.position + movementDirection * speed * Time.deltaTime;
        if (movementDirection != Vector3.zero)
        {
            targetAngle = Mathf.Atan2(movementDirection.x, movementDirection.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

            skysungAnimator.SetBool("Walk",true);
        }
        else
        {
            skysungAnimator.SetBool("Walk", false);
        }*/

        /*float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(movement));

        if (movement != Vector3.zero)
        {
            skysungAnimator.SetBool("Walk", true);
            Quaternion targetRotation = Quaternion.LookRotation(movement.normalized, Vector3.up);

            Quaternion newRotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            rb.MoveRotation(newRotation);
        }
        else
        {
            skysungAnimator.SetBool("Walk", false);
        }*/



    }
}
