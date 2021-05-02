using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [HideInInspector]
    public int Speed;

    [HideInInspector]
    public int PausaDelay;

    [HideInInspector]
    public int PausaTime;

    //[HideInInspector]
    //public float Speed;

    //[HideInInspector]
    //public float Speed;

    private CharacterController xController;       // player controller dell animale
    private Animator xAnimator;                    // player animator dell animale

    private float TimeCurrent;

    private Vector3 xMovement;
    private float PausaStart;
    //private bool Pausa;

    // Start is called before the first frame update
    void Start()
    {
        //TimeStart = Time.time;
        // inizializzazione
        xController = GetComponent<CharacterController>();
        xAnimator = GetComponent<Animator>();
        //xAnimalPosition = xController.transform.position;
        xAnimator.SetBool("isWalking", true);
        //Pausa = false;
        PausaStart = Time.time; 
    }

    void FixedUpdate()
    {
        TimeCurrent = Time.time;

        // gestione PAUSA
        if (this.PausaDelay > 0 && this.PausaTime > 0)
        {
            if ((TimeCurrent - PausaStart) >= this.PausaDelay)
            {
                if ((TimeCurrent - PausaStart) <= this.PausaDelay + this.PausaTime)
                {
                    if (xAnimator.GetBool("isWalking"))
                    {
                        xAnimator.SetBool("isWalking", false);
                    }
                }
                else
                {
                    if (!xAnimator.GetBool("isWalking"))
                    {
                        xAnimator.SetBool("isWalking", true);
                    }
                    PausaStart = Time.time;
                }
            }
        }

        //if ( (xAnimator.GetBool("isWalking") && TimeCurrent >= PausaStart + 1f )
        //    ||
        //     (!xAnimator.GetBool("isWalking") && TimeCurrent <= PausaStart + 0.5f)
        //   )
        if ( xAnimator.GetBool("isWalking") )
        {
            xMovement = new Vector3(-1.0f, -1.0f, 0);
            xController.Move(xMovement * (Speed / 5) * Time.deltaTime);
        }

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.tag == "DEATH" || hit.collider.tag == "WEAPON")
        {
            //Debug.Log(collision.gameObject.tag);
            Destroy(this.gameObject);
        }

        if (hit.collider.tag == "ANIMAL" )
        {
            if (this.Speed > hit.collider.GetComponent<AnimalController>().Speed)
            {
                //Debug.Log("impostata velocita a " + hit.collider.GetComponent<AnimalController>().Speed);
                this.Speed = hit.collider.GetComponent<AnimalController>().Speed;
            }
        }
    }

}
