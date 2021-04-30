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
    private bool Pausa;

    // Start is called before the first frame update
    void Start()
    {
        //TimeStart = Time.time;
        // inizializzazione
        xController = GetComponent<CharacterController>();
        xAnimator = GetComponent<Animator>();
        //xAnimalPosition = xController.transform.position;
        Pausa = false;
    }

    // Update is called once per frame
    void Update()
    {
        TimeCurrent = Time.time;

        // gestione PAUSA
        if (this.PausaDelay > 0 && this.PausaTime > 0){
            if (TimeCurrent >= this.PausaDelay){
                if (TimeCurrent <= this.PausaDelay + this.PausaTime){
                    Pausa = true;
                }
                else{
                    Pausa = false;
                }
            }
        }

        if (Pausa){
                //Debug.Log("io sono in pausa...");
        }
        else{
            xMovement = new Vector3(-1.0f, -1.0f, 0);
            xController.Move(xMovement * (Speed / 10) * Time.deltaTime);
        }
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        //Debug.Log("animal-on-controller=" + hit.collider.name);
        if (hit.collider.tag == "DEATH" || hit.collider.tag == "WEAPON")
        {
            //Debug.Log(collision.gameObject.tag);
            Destroy(this.gameObject);
        }
    }

}
