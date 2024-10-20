using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Lengua : MonoBehaviour
{

    // Comportamiento lengua - Variables 
    [SerializeField] private float rangoLengua = 10f;
    [SerializeField] private Transform puntosalidaLengua;
    [SerializeField] private GameObject lengua;
    [SerializeField] private float VelocidadEstiramiento = 5f;
    private Vector3 posicionInicialLengua;

    // Comportamiento Logica deteccion enemigo - Variable 
    [SerializeField] private LayerMask enemigoLayer;

    // Valores especificos - Variables
    [SerializeField] private float velocidadDesplazamientoRapido = 10f;
    [SerializeField] private int danho = 1;

    // Valores de estado - Variables
    private bool estaAtacando = false;

    // Referencias - Llamados
    private Enemigo_Control Enemigo;
    private Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LanzarLengua()
    {
        if (!estaAtacando)
        {
            Collider[] enemigos = Physics.OverlapSphere(this.transform.position, rangoLengua, enemigoLayer);


            if (enemigos.Length > 0)
            {
                ObtenerEnemigoCercano(enemigos);
            }

        }

    }

    private Enemigo_Control ObtenerEnemigoCercano(Collider[] enemigos)
    {
        float distanciaMasCorta = Mathf.Infinity;
        Debug.Log("aaaa");
    }
}
