using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackScrolling : MonoBehaviour
{
    //[SerializeField] private LayerMask groundLayers;
    [SerializeField] private float VelocityX;
    [SerializeField] private float VelocityY;

    public bool isTEST;

    private Material xMaterial;
    private Vector2 xOffset;

    void Awake()
    {
        xMaterial = GetComponent<Renderer>().material;
    }

    void Start()
    {
        if (!isTEST)
        {
            xOffset = new Vector2((VelocityX / 100), (VelocityY / 100));
        }
    }

    void Update()
    {
        if (!isTEST)
        {
            xMaterial.mainTextureOffset += xOffset * Time.deltaTime;
        }
    }
}
