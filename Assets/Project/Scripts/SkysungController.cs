using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.U2D;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class SkysungController : MonoBehaviour
{
    public Transform playerObj;

    public float speed = 0f;
    public float rotationSpeed = 0f;

    public Animator skysungAnimator;
    public AnimatorStateInfo skysungStateInfo;
    private Rigidbody rb;

    public bool isGrounded = false;
    public float forceJump = 400.0f;
    public bool dobleJump;
    public int dobleJumpCount;

    public bool dash = true;

    public CameraSyle currentCamera;

    private bool isAIM;

    //ParticlesSystem-------------------------------
    public ParticlesSkysung ps;

    public bool isTuto;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        dobleJumpCount = 0;
        isAIM=false;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    public IEnumerator DashEnable()
    {
        dash = false;
        yield return new WaitForSeconds(1f);
        dash = true;
    }

    public void Dead(float life)
    {
        skysungAnimator.SetTrigger("Death");
        Debug.Log(life);
    }

    public enum CameraSyle
    {
        BASIC,
        COMBAT
    }

    public void Movement()
    {

        //Movimiento horizontal y vertical del jugador
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        
        dobleJump = false;
        if (currentCamera == CameraSyle.BASIC)
        {
            if (isAIM)
            {
                speed = 5f;
                isAIM = false;
            }

            //Se crea un vector que tiene como valores la vista de frente e izquierda dada por la cámara, junto con los valores de entrada vertical y horizontal
            Vector3 inputDir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up) * verticalInput
                               + Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up) * horizontalInput;

            //Cuando se encuentre en movieminto el personaje, se activará la animación Walk y se rotará el movimiento y modelo según el frente de la cámara 
            if (inputDir != Vector3.zero)
            {
                skysungAnimator.SetBool("Walk", true);
                transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up));
                playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);

                if (Input.GetKeyDown(KeyCode.C) && dash && !isTuto)
                {
                    rb.AddForce(inputDir * 500, ForceMode.Impulse);
                    skysungAnimator.SetTrigger("Dash");
                    StartCoroutine("DashEnable");
                    ps.activate();
                }

            }
            else
            {
                skysungAnimator.SetBool("Walk", false);
                speed = 5f;
                skysungAnimator.SetInteger("Speed", 5);
            }

            Vector3 ground = transform.TransformDirection(Vector3.down);



            if (Physics.Raycast(transform.position, ground, 1.3f))
            {
                isGrounded = true;
                skysungAnimator.SetBool("IsGrounded", true);
                //Debug.Log("piso");
                  
            }
            else
            {
                isGrounded = false;
                skysungAnimator.SetBool("IsGrounded", false);
                if (dobleJumpCount < 2)
                    dobleJump = true;

            }
            if (Physics.Raycast(transform.position, ground, 0.985f))
                dobleJumpCount = 0;

            if (isTuto)
                dobleJump = false;


            if ((Input.GetKeyDown(KeyCode.Space) && (isGrounded || dobleJump)))
            {
                skysungAnimator.SetTrigger("Jump");
                if (dobleJumpCount > 0)
                    ps.activate();
                dobleJumpCount++;
                rb.AddForce(new Vector3(0, forceJump, 0), ForceMode.Impulse);
                

            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded)
            {
                //skysungAnimator.SetTrigger("Run");
                skysungAnimator.SetInteger("Speed", 10);
                speed = 10f;
            }
        }
        else if (currentCamera == CameraSyle.COMBAT)
        {
            skysungAnimator.SetInteger("Speed", 5);
            isAIM = true;
            speed = 2f;

            Vector3 combatDir = Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up) * verticalInput
                              + Vector3.ProjectOnPlane(Camera.main.transform.right, Vector3.up) * horizontalInput;


            transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up));
            playerObj.forward = Vector3.Slerp(playerObj.forward, Vector3.ProjectOnPlane(Camera.main.transform.forward, Vector3.up).normalized, Time.deltaTime * rotationSpeed);

            if (combatDir != Vector3.zero)
            {
                skysungAnimator.SetBool("Walk", true);

            }
            else
            {
                skysungAnimator.SetBool("Walk", false);
            }


            Vector3 ground = transform.TransformDirection(Vector3.down);

            if (Physics.Raycast(transform.position, ground, 1.1f))
            {
                isGrounded = true;
                skysungAnimator.SetBool("IsGrounded", true);
                //Debug.Log("piso");

            }
            else
            {
                isGrounded = false;
                skysungAnimator.SetBool("IsGrounded", false);

            }
        }
        //crea un movimiento con rigidbody, usando las entradas vertical y horizontal
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        rb.MovePosition(rb.position + transform.TransformDirection(movement));


    }
}
