using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[SerializeField] private LayerMask groundLayers;

    // stato del player
    public enum ePlayerStatus
    {
        Ground, Jump, Reload, Fire, FireAlto, Furia
    }
    public ePlayerStatus PlayerStatus;

    // stato furia
    public enum eFuriaStatus { BIG, NORMAL };
    public eFuriaStatus FuriaStatus;
    public float FuriaTime = 14f;                 // secondi per furia, *2 per riaverla dopo usata
    public float FuriaTimeStatus = -1;            //start time della furia, -1 disponibile, altrimenti effetto furia = FuriaTime, ridisponibile a = FuriaTime*2

    public float JumpForce = 6.5f;
    public float JumpSpeed = 200f;

    public GameObject BulletObject;      // prefab bullet
    public GameObject FireAltoObject;    // prefab firealto

    /*
     * 
    */


    private CharacterController xController;    // player controller
    private Animator xAnimator;                 // player animator
    private SoundManager xAudioManager;         // per suoni
    private FXManager xFXManager;               // per particles effects

    //private float xGravity = Physics.gravity.y; // gravity from project settings
    private Vector3 yMovement;                  // player position end
    private Vector3 xMovement;                  // player position end

    private bool isGround;                      // player is grounded
    //private int isFly = 0;                      // player is flying (100/?)
    //private int isFlyTot = 80;                  // tot frame per flying

    private float xTimeStart;
    private float xTimeCurrent;

    private Vector3 xFuriaDimStart;
    private Vector3 xFuriaDimEnd;
    private Vector3 xFuriaPosition;

    private ParticleSystem xGunShotFX;
    private ParticleSystem xSmokeFX;
    private ParticleSystem xStaccoFX;


    private Vector3 xaggiunta;
    private Vector3 xposWeaponFX;

    private GameObject Bullet; // instanza del prefab firealto
    private GameObject FireAltoInstance; // instanza del prefab firealto


    // Start is called before the first frame update
    void Start()
    {
        // inizializzazione
        xController = GetComponent<CharacterController>();
        xAnimator = GetComponent<Animator>();
        xAudioManager = FindObjectOfType<SoundManager>();
        xFXManager = FindObjectOfType<FXManager>();
        xGunShotFX = xFXManager.FindFXParticle("FX_Gunshot");
        xSmokeFX = xFXManager.FindFXParticle("FX_Smoke");
        xStaccoFX = xFXManager.FindFXParticle("FX_Stacco");

        xTimeStart = -1;
        FuriaTimeStatus = -1;
        FuriaStatus = eFuriaStatus.NORMAL;
        xAudioManager.Play("YEAH", 0.20f);

    }

    // Update is called once per frame
    void Update()
    {
        xTimeCurrent = Time.time;

        // true = contatto con ground
        isGround = xController.isGrounded;

        // dimensione attuale del player
        xFuriaDimStart = xController.transform.localScale;
        // posizione attuale del player
        xFuriaPosition = xController.transform.position;


        // ** GROUND ********************************************
        if ( isGround)
        {
            if ( (xTimeStart == -1) || ( (xTimeCurrent - xTimeStart) > 0.5) ) 
            PlayerStatus = ePlayerStatus.Ground;
            xTimeStart = -1;
        }

        // ** JUMP ********************************************
        if (PlayerStatus == ePlayerStatus.Ground && Input.GetButtonDown("JUMP"))
        {
            xTimeStart = -1;
            PlayerStatus = ePlayerStatus.Jump;
            xAnimator.Play("Player.JUMP", 0, 1f);
            xStaccoFX.Play();
            xAudioManager.Play("JUMP2", 0);
            xAudioManager.Play("URLO", 2f);
        }

        // ** FIRE ********************************************
        if (PlayerStatus == ePlayerStatus.Ground && Input.GetButtonDown("FIRE"))
        {
            xTimeStart = xTimeCurrent;
            PlayerStatus = ePlayerStatus.Fire;
            xaggiunta = this.transform.lossyScale;
            xposWeaponFX = new Vector3(this.transform.position.x + 1, this.transform.position.y + xaggiunta.y, BulletObject.transform.position.z);
            Bullet = Instantiate(BulletObject, xposWeaponFX, BulletObject.transform.rotation);
            xAnimator.Play("Player.FIRE", 0, 0.1f);
            xAudioManager.Play("STACCO", 0);
            xAudioManager.Play("FIRE", 0.2f);
            xSmokeFX.Play();
            xGunShotFX.Play();
        }

        // ** FIRE ALTO ********************************************
        if (PlayerStatus == ePlayerStatus.Ground && Input.GetButtonDown("FIREALTO"))
        {
            //if (!ExplosionInstance)
            xTimeStart = -1;
            PlayerStatus = ePlayerStatus.FireAlto;
            xStaccoFX.Play();
            xAnimator.Play("Player.FIREALTO", 0, 1f);
            xAudioManager.Play("STACCO", 0);
            xAudioManager.Play("FIREALTO", 0.9f);
            xaggiunta = this.transform.lossyScale;
            FireAltoInstance = Instantiate(FireAltoObject, this.transform);
            xposWeaponFX = new Vector3(FireAltoInstance.transform.position.x, FireAltoInstance.transform.position.y + (xaggiunta.y * -1f), FireAltoInstance.transform.position.z);
            FireAltoInstance.transform.position = xposWeaponFX;
        }

        // ** RELOAD ********************************************
        if (PlayerStatus == ePlayerStatus.Ground && Input.GetButtonDown("RELOAD"))
        {
            xTimeStart = -1;
            PlayerStatus = ePlayerStatus.Reload;
            xAnimator.Play("Player.RELOAD", 0, 3f);
            xAudioManager.Play("STACCO", 0);
            xAudioManager.Play("RELOAD", 0.5f);
        }


        // ** FURIA********************************************
        if (PlayerStatus == ePlayerStatus.Ground)
        {
            if ((FuriaTimeStatus == -1) && (FuriaStatus == eFuriaStatus.NORMAL))
            {
                if (Input.GetButtonDown("FURIA"))
                {
                    xTimeStart = -1;
                    FuriaTimeStatus = xTimeCurrent;
                    PlayerStatus = ePlayerStatus.Furia;
                    xAnimator.Play("Player.FURIA", 0, 1f);
                    xAudioManager.Play("STACCO", 0);
                    xStaccoFX.Play();
                }
            }
        }
        // NORMAL --> BIG
        if ( (FuriaTimeStatus != -1) && (FuriaStatus == eFuriaStatus.NORMAL))
        {
            if ((xTimeCurrent - FuriaTimeStatus) < FuriaTime)
            {
                if (xFuriaDimStart.y < 2.00000000001)
                {
                    xFuriaDimEnd = new Vector3(2f, 2f, 2f);
                    xController.transform.localScale += (xFuriaDimEnd * Time.deltaTime);
                }
                if (xFuriaPosition.x < -17)
                {
                   xMovement.x = 1f;
                   xController.Move(xMovement * Time.deltaTime);
                   //xController.transform.position += (xMovement * Time.deltaTime);
                }
            }
            else
            {
                FuriaTimeStatus = xTimeCurrent;
                FuriaStatus = eFuriaStatus.BIG;
            }
        }
        // BIG --> NORMAL
        if ((FuriaTimeStatus != -1) && (FuriaStatus == eFuriaStatus.BIG))
        {
            if ((xTimeCurrent - FuriaTimeStatus) < (FuriaTime*2))
            {
                if (xFuriaDimStart.y > 1.00000000001)
                {
                    xFuriaDimEnd = new Vector3(1f, 1f, 1f);
                    xController.transform.localScale -= (xFuriaDimEnd * Time.deltaTime);
                }
                if (xFuriaPosition.x > -22.33)
                {
                    xMovement.x = -1f;
                    xController.Move( xMovement * Time.deltaTime);
                    //xController.transform.position += (xMovement * Time.deltaTime);
                }

            }
            else
            {
                FuriaTimeStatus = -1;
                FuriaStatus = eFuriaStatus.NORMAL;
            }
        }


        // ** FIRE3 ********************************************




        // sposto leggermente in su per staccarlo dal ground..il primo frame
        if (PlayerStatus != ePlayerStatus.Ground)
        {
            yMovement.y = 0.01f;
            xController.Move(yMovement);
        }


        // se indietro (per esempio urtando nuvola)
        // lo riporto al solito posto
        if (xFuriaPosition.x < -22.33f)
        {
            xMovement.x = 1f;
            xController.Move(xMovement * Time.deltaTime);
        }


    }



    // Update is called fixed rate per second
    void FixedUpdate()
    {

        // JUMP
        if (PlayerStatus == ePlayerStatus.Jump)
        {
            yMovement.y = JumpForce;
            xController.Move(yMovement * (JumpSpeed/110) * Time.deltaTime);
            return;
        }

        // FIRE
        if (PlayerStatus == ePlayerStatus.Fire)
        {
            yMovement.y = JumpForce / 3f;
            xController.Move(yMovement * (JumpSpeed / 200) * Time.deltaTime);
            return;
        }

        // FIRE ALTO
        if (PlayerStatus == ePlayerStatus.FireAlto)
        {
            yMovement.y = JumpForce;
            xController.Move(yMovement * (JumpSpeed / 150) * Time.deltaTime);
            return;
        }

        // RELOAD
        //if (PlayerStatus == PlayerStatus.Reload && (xTimeCurrent - xTimeStart) < 0.2)
        if (PlayerStatus == ePlayerStatus.Reload)
        {
            yMovement.y = JumpForce;
            xController.Move(yMovement * (JumpSpeed/250) * Time.deltaTime);
            return;
        }

        // FURIA
        if (PlayerStatus == ePlayerStatus.Furia)
        {
            yMovement.y = JumpForce;
            xController.Move(yMovement * (JumpSpeed / 180) * Time.deltaTime);
            return;
        }


        // FIRE3


    }


    void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("player-coll-enter: " + collision.collider.name);
    }
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log("player-coll-stay: " + collision.collider.name);
    }
    void OnCollisionExit(Collision collision)
    {
        //Debug.Log("player-coll-exit: " + collision.collider.name);
    }

    /*
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("player-trig-enter: " + other.name);
    }
    void OnTriggerStay(Collider other)
    {
        if (other.name != "Player")
        {
            Debug.Log("player-trig-stay: " + other.name);
        }
    }
    void OnTriggerExit(Collider other)
    {
        Debug.Log("player-trig-exit: " + other.name);
    }
    */

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
       // if (hit.collider.name != "Piano")
       // {
            //Debug.Log("player-on-controller=" + hit.collider.name);
       // }
    }


    void OnParticleCollision(GameObject other)
    {
        //Debug.Log("Player.OnParticleCollision-> " + other.name);
        if (other.name == "FX_Weed(Clone)")
        {
            xAudioManager.Play("OUCH", 0);
        }
    }


}
