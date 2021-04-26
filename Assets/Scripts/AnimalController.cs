using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [HideInInspector]
    public float Speed;


    private CharacterController xController;       // player controller dell animale
    private Animator xAnimator;                    // player animator dell animale

    //private Vector3 xAnimalPosition;               // posizione inziale, mi serve per bloccare Y e Z
    private Vector3 xMovement;

    // Start is called before the first frame update
    void Start()
    {
        // inizializzazione
        xController = GetComponent<CharacterController>();
        xAnimator = GetComponent<Animator>();
        //xAnimalPosition = xController.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        xMovement = new Vector3 (-1.0f, -1.0f, 0);
        //xMovement = transform.TransformDirection(xMovement);
        xController.Move(xMovement * Speed * Time.deltaTime);
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
