using UnityEngine;

public class NebbiaController : MonoBehaviour
{

    public float Durata;
    public float Velocita;
    public float Pausa;

    private float TimeStart;
    private float PausaStart;
    private Vector3 xCurrentPosition;
    private Vector3 NebbiaMovement;
    private enum eMovimento
    {
        sinistra,
        pausa,
        destra
    }
    private eMovimento xMovimento;

    // Start is called before the first frame update
    void Start()
    {
        TimeStart = Time.time;
        PausaStart = 0;
        xMovimento = eMovimento.sinistra;
    }

    // Update is called once per frame
    void Update()
    {

        // posizione attuale
        xCurrentPosition = this.transform.position;

        // morte
        if (Time.time >= TimeStart + Durata)
        {
            Debug.Log("Nebbia DEATH");
            Destroy(gameObject);
            return;
        }

        if (xMovimento == eMovimento.sinistra && xCurrentPosition.x < -21)
        {
            xMovimento = eMovimento.pausa;
            PausaStart = Time.time;
        }
        if (xMovimento == eMovimento.pausa && Time.time >= PausaStart + Pausa)
        {
            xMovimento = eMovimento.destra;
        }

        switch (xMovimento)
        {
            case eMovimento.sinistra:
                NebbiaMovement = new Vector3(-1f, 0f, 0f);
                break;
            case eMovimento.pausa:
                NebbiaMovement = new Vector3(0f, 0f, 0f);
                break;
            case eMovimento.destra:
                NebbiaMovement = new Vector3(1f, 0f, 0f);
                break;
        }


        this.transform.position += NebbiaMovement * Velocita * Time.deltaTime;

    }
}
