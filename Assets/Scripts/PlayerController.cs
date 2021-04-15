using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // stato del player
    public enum PlayerStatus
    {
        Ground, Jump, Reload, Fire, FireAlto, Furia
    }
    public PlayerStatus xStatus;

    // stato furia
    public enum FuriaStatus { BIG, NORMAL };
    public FuriaStatus xFuriaStatus;

    //[SerializeField] private LayerMask groundLayers;
    [SerializeField] private float JumpForce = 6.5f;
    [SerializeField] private float JumpSpeed = 200f;
    [SerializeField] private float FuriaTime = 14f;   // sec per furia, attulament utilizzabile una volta solo a livello

    private CharacterController xController;    // player controller
    private Animator xAnimator;                 // player animator
    //private float xGravity = Physics.gravity.y; // gravity from project settings
    //private Vector3 xPosition;                  // player position
    private Vector3 xMovement;                  // player position end

    private bool isGround;                      // player is grounded
    //private int isFly = 0;                      // player is flying (100/?)
    //private int isFlyTot = 80;                  // tot frame per flying

    private float xTimeStart;
    private float xTimeCurrent;
    private float xTimeFuria;   //start time della furia

    private Vector3 xFuriaDimStart;
    private Vector3 xFuriaDimEnd;


    // Start is called before the first frame update
    void Start()
    {
        // inizializzazione
        xController = GetComponent<CharacterController>();
        xAnimator = GetComponent<Animator>();
        xTimeStart = -1;
        xTimeFuria = -1;
        xFuriaStatus = FuriaStatus.NORMAL;
    }

    // Update is called once per frame
    void Update()
    {
        if (xController == null)
        {
            Debug.LogError("CharacterController nullo!");
        }
        if (xAnimator == null)
        {
            Debug.LogError("Animator nullo!");
        }

        xTimeCurrent = Time.time;

        // true = contatto con ground
        isGround = xController.isGrounded;

        // dimensione attuale del player
        xFuriaDimStart = xController.transform.localScale;


        // ** GROUND ********************************************
        if ( isGround)
        {
            if ( (xTimeStart == -1) || ( (xTimeCurrent - xTimeStart) > 0.5) ) 
            //Debug.Log("isGround");
            xStatus = PlayerStatus.Ground;
            xTimeStart = -1;
        }

        // ** JUMP ********************************************
        if (xStatus == PlayerStatus.Ground && Input.GetButtonDown("Jump"))
        {
            Debug.Log("JUMP");
            xTimeStart = -1;
            xStatus = PlayerStatus.Jump;
            xAnimator.Play("Player.JUMP", 0, 1f);
        }

        // ** FIRE ********************************************
        if (xStatus == PlayerStatus.Ground && Input.GetButtonDown("Fire"))
        {
            Debug.Log("FIRE");
            xTimeStart = xTimeCurrent;
            xStatus = PlayerStatus.Fire;
            xAnimator.Play("Player.FIRE", 0, 0.1f);
        }

        // ** RELOAD ********************************************
        if (xStatus == PlayerStatus.Ground && Input.GetButtonDown("Reload"))
        {
            Debug.Log("RELOAD");
            xTimeStart = -1;
            xStatus = PlayerStatus.Reload;
            xAnimator.Play("Player.RELOAD", 0, 2f);
        }

        // ** FIRE ALTO ********************************************
        if (xStatus == PlayerStatus.Ground && Input.GetButtonDown("FireAlto"))
        {
            Debug.Log("FIREALTO");
            xTimeStart = -1;
            xStatus = PlayerStatus.FireAlto;
            xAnimator.Play("Player.FIREALTO", 0, 1f);
        }

        // ** FURIA********************************************
        if (xStatus == PlayerStatus.Ground)
        {
            if ((xTimeFuria == -1) && (xFuriaStatus == FuriaStatus.NORMAL))  // solo una volta di puo' fare la FURIA
            {
                if (Input.GetButtonDown("Furia"))
                {
                    Debug.Log("FURIA INIZIO");
                    xTimeStart = -1;
                    xTimeFuria = xTimeCurrent;
                    xStatus = PlayerStatus.Furia;
                    xAnimator.Play("Player.FURIA", 0, 1f);
                }
            }
        }
        if ( (xTimeFuria != -1) && (xFuriaStatus == FuriaStatus.NORMAL))
        {
            if ((xTimeCurrent - xTimeFuria) < FuriaTime)
            {
                if (xFuriaDimStart.y < 2.00000000001)
                {
                    xFuriaDimEnd = new Vector3(2f, 2f, 2f);
                    xController.transform.localScale += (xFuriaDimEnd * Time.deltaTime);
                }
            }
            else
            {
                Debug.Log("FURIA INDIETRO");
                xTimeFuria = xTimeCurrent;
                xFuriaStatus = FuriaStatus.BIG;
            }
        }
        if ((xTimeFuria != -1) && (xFuriaStatus == FuriaStatus.BIG))
        {
            if ((xTimeCurrent - xTimeFuria) < (FuriaTime*2))
            {
                if (xFuriaDimStart.y > 1.00000000001)
                {
                    xFuriaDimEnd = new Vector3(1f, 1f, 1f);
                    xController.transform.localScale -= (xFuriaDimEnd * Time.deltaTime);
                }
            }
            else
            {
                Debug.Log("FURIA FINE");
                xTimeFuria = -1;
                xFuriaStatus = FuriaStatus.NORMAL;
            }
        }


        // ** FIRE3 ********************************************
        //if (xStatus == PlayerStatus.Ground && Input.GetButtonDown("Fire3"))
        //{
        //    Debug.Log("FIRE3");
        //    xTimeStart = -1;
        //    xStatus = PlayerStatus.Fire3;
        //    xAnimator.Play("Player.AUTOSHOT", 0, 0.6f);
        //}

        // sposto leggermente in su per staccarlo dal ground..il primo frame
        if (xStatus != PlayerStatus.Ground)
        {
                xMovement.y = 0.01f;
                xController.Move(xMovement);
        }

    }



    // Update is called fixed rate per second
    void FixedUpdate()
    {

        // JUMP
        if (xStatus == PlayerStatus.Jump)
        {
            xMovement.y = JumpForce;
            xController.Move(xMovement * (JumpSpeed/100) * Time.deltaTime);
            return;
        }

        // FIRE
        if (xStatus == PlayerStatus.Fire)
        {
            xMovement.y = JumpForce / 3f;
            xController.Move(xMovement * (JumpSpeed / 200) * Time.deltaTime);
            return;
        }

        // RELOAD
        //if (xStatus == PlayerStatus.Reload && (xTimeCurrent - xTimeStart) < 0.2)
        if (xStatus == PlayerStatus.Reload)
        {
            xMovement.y = JumpForce;
            xController.Move(xMovement * (JumpSpeed/250) * Time.deltaTime);
            return;
        }

        // FIRE ALTO
        if (xStatus == PlayerStatus.FireAlto)
        {
            xMovement.y = JumpForce;
            xController.Move(xMovement * (JumpSpeed / 160) * Time.deltaTime);
            return;
        }

        // FURIA
        if (xStatus == PlayerStatus.Furia)
        {
            xMovement.y = JumpForce;
            xController.Move(xMovement * (JumpSpeed / 120) * Time.deltaTime);
            return;
        }


        // FIRE3
        //if (xStatus == PlayerStatus.Fire3)
        //{
        //    xMovement.y = JumpForce;
        //    xController.Move(xMovement * (JumpSpeed / 100) * Time.deltaTime);
        //    return;
        //}


    }

}
