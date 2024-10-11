using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Enemigo : MonoBehaviour
{

    [SerializeField] private int vida = 5;
    [SerializeField] private float velocidadMovimiento = 3f;

    public int Vida { get => vida; set => vida = value; }

    private Transform jugador;
    private bool jugadorEnRango = false;
    private bool siendoComido = false;

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.FindGameObjectWithTag("Jugador").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorEnRango)
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
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Jugador"))
        {
            jugadorEnRango = false;
        }
    }
   
    public void RecibirDahno(int cantidad)
    {
        vida = vida - cantidad;


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

}
