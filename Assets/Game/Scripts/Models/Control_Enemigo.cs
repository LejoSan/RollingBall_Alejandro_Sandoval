using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Enemigo : MonoBehaviour
{

    [SerializeField] private int vida = 5;
    [SerializeField] private float velocidadMovimiento = 3f;
    [SerializeField] private GameObject indicador;
    private Transform jugador;
    private bool jugadorEnRango = false;
    private bool siendoComido = false;
    private Rigidbody rbEnemigo;

    public int Vida { get => vida; set => vida = value; }
    public bool SiendoComido { get => siendoComido; set => siendoComido = value; }

    
    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        rbEnemigo = GetComponent<Rigidbody>();

        if (indicador != null)
        {
            indicador.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorEnRango && !siendoComido)
        {
            SeguirJugador();
        }
    }
    private void SeguirJugador()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        transform.position += direccion * velocidadMovimiento * Time.deltaTime;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            jugadorEnRango = true;
            MostrarIndicador();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            jugadorEnRango = false;
            OcultarIndicador();
        }
    }
   
    public void RecibirDahno(int cantidad)
    {
        vida = vida - cantidad;

        if (siendoComido)
        {
            rbEnemigo.constraints = RigidbodyConstraints.FreezePosition;
        }

    }


    public  void DestruirEnemigo()
    {
        if (vida <= 0)
        {
            Destroy(gameObject);
        }
        
    }

    public bool EstaVivo()
    {
        return vida > 0;
    }

    private void MostrarIndicador()
    {
        if (indicador != null)
        {
            indicador.SetActive(true);
        }
    }

    private void OcultarIndicador()
    {
        if (indicador != null)
        {
            indicador.SetActive(false);
        }
    }
}
