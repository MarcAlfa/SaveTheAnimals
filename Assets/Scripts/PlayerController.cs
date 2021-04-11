using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private LayerMask groundLayers;
    [SerializeField] private float JumpForce = 2000F;

    private CharacterController xController;    // player controller
    private Animator xAnimator;                 // player animator
    //private float xGravity = Physics.gravity.y; // gravity from project settings
    //private Vector3 xPosition;                  // player position
    private Vector3 xMovement;                  // player position end
    
    private bool isGround;                      // player is grounded
    private int isFly = 0;                      // player is flying (100/?)
    private int isFlyTot = 80;                  // tot frame per flying


    // Start is called before the first frame update
    void Start()
    {
        // inizializzazione
        xController = GetComponent<CharacterController>();
        xAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // true = contatto con ground
        isGround = xController.isGrounded;
        //isGrounded = Physics.CheckSphere(transform.position, 0.5f, groundLayers, QueryTriggerInteraction.Ignore);

        // current position of the player
        //xPosition = this.transform.position;

        // se ground non puo' essere fly
        if (isGround)
        {
            isFly = 0;
        }


        // ** JUMP ********************************************
        if (isGround && Input.GetButtonDown("Jump"))
        {
            isFly = isFlyTot;
            // MOVE - lo sposto in alto di pochissimo per staccarlo subito dal ground
            if (xController != null)
            {
                xMovement.y = 0.1f;
                // provo a spostarlo in alto di pochissimo per staccarlo dal ground
                xController.Move(xMovement);
                //this.transform.position = Vector3.Lerp(xCurrVelocity, xVelocity, 0.5f); //* Time.deltaTime);
            }

            // ANIM
            if (xAnimator != null)
            {
                xAnimator.Play("Gioco.RUOTA", 0, 1f);
            }
        }


        // ** CROUCH ********************************************
        if (isGround && Input.GetButtonDown("Crouch"))
        {
            // anim
            if (xAnimator != null)
            {
                xAnimator.PlayInFixedTime("Gioco.CROUCH", 0, 0.85f);
            }
        }


        // ** SPARA ********************************************
        if (isGround && Input.GetButtonDown("Sparo"))
        {
            // anim
            if (xAnimator != null)
            {
                xAnimator.PlayInFixedTime("Gioco.SPARA", 0, 0);
            }
        }


        // ** RELOAD ********************************************
        // dovra' essere riempito tutto il caricatore, quindi in base a quanto e' vuoto / 1 animazione per ogni proiettile da caricare
        if (isGround && Input.GetButtonDown("Reload"))
        {
            // anim
            if (xAnimator != null)
            {
                xAnimator.PlayInFixedTime("Gioco.RELOAD", 0, 0);
            }
        }

        // ** FIRE1 ********************************************
        // sparo potente, solo 1 .. devono passare poi x sec per riaverlo
        // il caricatore nella GUI si illumina, dopo sparo si spegna e torna con numero proiettili "normali" che aveva prima
        if (isGround && Input.GetButtonDown("Fire1"))
        {
            // anim
            if (xAnimator != null)
            {
                xAnimator.PlayInFixedTime("Gioco.FIRE1", 0, 1f);
            }
        }


        // ** FIRE2 ********************************************
        // animazione e sound: dice semplicemente "brutta merda"
        // si puo fare che se subito dopo si ammazza un nemico punteggio vale di piu. pero' non si deve poter fare sempre
        // se lo si fa prima di uccidere un animale si perde di piu' del normale anche
        if (isGround && Input.GetButtonDown("Fire2"))
        {
            // anim
            if (xAnimator != null)
            {
                xAnimator.PlayInFixedTime("Gioco.FIRE2", 0, 1f);
            }

            // azioni
            if (xController != null)
            {
                //
            }
        }


    }



    // Update is called fixed rate per second
    void FixedUpdate()
    {
        // JUMP
        // MOVE  -- praticamente completo l'animazione in 100 fixedupdate, ma cmq si ferma quando tocca terra (isground)
        if (xController != null && isFly > 0)
        {
            xMovement.y = JumpForce / isFlyTot; 
            xController.Move(xMovement * Time.deltaTime);
            isFly -= 1;
        }
    }

}
