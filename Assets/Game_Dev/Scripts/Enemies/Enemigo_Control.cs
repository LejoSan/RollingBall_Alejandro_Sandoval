using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo_Control : MonoBehaviour
{
    [Header("Configuración del Enemigo")]
    [SerializeField] private int vida = 5;
    [SerializeField] private int Pinchazo = 1;
    [SerializeField] private float velocidadMovimiento = 3f;
    [SerializeField] private GameObject indicador;


    [Header("Referencias Internas")]
    private Transform jugador;
    private Rigidbody rbEnemigo;
    private Rana_Control rana;
    private Rigidbody rbjugador;

    [Header("Estado del Enemigo")]
    private bool jugadorEnRango = false;
    private bool enContactoParaDano = false;
    private bool enContactoConRana = false;

    public int Vida { get => vida; set => vida = value; }
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
    }

    // Perseguir al jugador 
    private void SeguirJugador()
    {
        Vector3 direccion = (jugador.position - transform.position).normalized;
        transform.position += direccion * velocidadMovimiento * Time.deltaTime;
    }

    // -- Cuando la Rana Entra y salga del Rango del Enemigo
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            EnContactoParaDano = false;
            jugadorEnRango = true;
            MostrarIndicador();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            //EnContactoParaDano = true;
            jugadorEnRango = false;           
            OcultarIndicador();
        }
    }



    // -- Cuando el Enemigo Choca con la Rana
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            EnContactoParaDano = true;
            jugadorEnRango = true;             // Detenemos el movimiento hacia el jugador

            enContactoConRana = true;

            Debug.Log("Detengo el movimiento hacia el enemigo");

            StartCoroutine(PreparoAtaque());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Jugador"))
        {
            // Sale de contacto con la rana
            enContactoConRana = false;
            EnContactoParaDano = false;
        }
    }

    private IEnumerator PreparoAtaque()
    {
        while (enContactoConRana)
        {
            // Reduce la vida de la rana en 1
            rana.VidaRana -= Pinchazo;
            Debug.Log("La vida de la rana es " + rana.VidaRana);

            // Espera 1 segundo antes de repetir
            yield return new WaitForSeconds(3f);

            if (rana.VidaRana >= 0)
            {
                rana.MuerteRana();
            }
        }

    }


    public void RecibirDahno(int cantidad)
    {
        if (EnContactoParaDano)                 // Aplica daño solo si está en contacto y siendo comido
        {
            vida -= cantidad;
            rbjugador.constraints = RigidbodyConstraints.FreezeAll;
            rbEnemigo.constraints = RigidbodyConstraints.FreezeAll;

            Debug.Log("Enemigo recibió " + cantidad + " de daño. Vida actual: " + vida);

            if (vida <= 0)
            {
                DestruirEnemigo();
            }
            //else
            //{
            //    // Descongela el Rigidbody después de un tiempo
            //    StartCoroutine(DescongelarDespuesDeDano());
            //}
        }
    }

    //private IEnumerator DescongelarDespuesDeDano()
    //{
    //    yield return new WaitForSeconds(1f); // Espera 1 segundo antes de descongelar
    //    rbEnemigo.constraints = RigidbodyConstraints.None;
    //    rbEnemigo.constraints = RigidbodyConstraints.FreezeRotation; // Congela solo la rotación para que el enemigo pueda moverse
    //}
    public void DestruirEnemigo()
    {
        if (vida <= 0)
        {
            GameObject jugador = GameObject.FindGameObjectWithTag("Jugador");
            Destroy(gameObject);
            rana.LiberarMovimiento();
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
