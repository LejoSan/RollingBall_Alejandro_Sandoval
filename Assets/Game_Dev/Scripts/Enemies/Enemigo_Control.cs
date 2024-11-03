using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Control : MonoBehaviour
{

    [SerializeField] private int vida = 5;
    [SerializeField] private float velocidadMovimiento = 3f;
    [SerializeField] private GameObject indicador;
    private Transform jugador;
    private bool jugadorEnRango = false;
    private bool siendoComido = false;
    private Rigidbody rbEnemigo;
    private bool enContactoParaDano = false;

    private Rana_Control rana;
    private Rigidbody rbjugador;

    public int Vida { get => vida; set => vida = value; }
    public bool SiendoComido { get => siendoComido; set => siendoComido = value; }
    public bool EnContactoParaDano { get => enContactoParaDano; set => enContactoParaDano = value; }


    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
        rbEnemigo = GetComponent<Rigidbody>();


        rana = jugador.GetComponent<Rana_Control>();
        rbjugador = jugador.GetComponent<Rigidbody>();

        if (indicador != null)
        {
            indicador.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorEnRango && !EnContactoParaDano)
        {
            SeguirJugador();
        }
        //if (jugadorEnRango && !siendoComido)
        //{
        //    SeguirJugador();
        //}
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            EnContactoParaDano = true;
            jugadorEnRango = false; // Detenemos el movimiento hacia el jugador
        }
    }

    public void RecibirDahno(int cantidad)
    {
        if (EnContactoParaDano) // Aplica daño solo si está en contacto y siendo comido
        {
            vida -= cantidad;

            rbjugador.constraints = RigidbodyConstraints.FreezeAll;

            rbEnemigo.constraints = RigidbodyConstraints.FreezeAll; // Congela el movimiento al ser atacado
            Debug.Log("Enemigo recibió " + cantidad + " de daño. Vida actual: " + vida);

            if (vida <= 0)
            {
                DestruirEnemigo();
            }
        }
        //vida = vida - cantidad;

        //if (siendoComido)
        //{
        //    rbEnemigo.constraints = RigidbodyConstraints.FreezePosition;
        //    Debug.Log("Enemigo recibió " + cantidad + " de daño. Vida actual: " + vida);
        //}

    }
    public void CongelarEnemigo(bool congelar)
    {
        rbEnemigo.constraints = congelar ? RigidbodyConstraints.FreezeAll : RigidbodyConstraints.None;
    }

    public void DestruirEnemigo()
    {
        if (vida <= 0)
        {
            // Llamar a LiberarMovimiento en el jugador
            GameObject jugador = GameObject.FindGameObjectWithTag("Jugador");
            if (jugador != null)
            {
                //jugador.GetComponent<Rana_Control>().LiberarMovimiento();
            }

            Destroy(gameObject);
            Debug.Log("Enemigo destruido y movimiento del jugador liberado.");
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
