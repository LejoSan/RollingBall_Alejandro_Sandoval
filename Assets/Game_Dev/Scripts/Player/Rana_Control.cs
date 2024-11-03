using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rana_Control : MonoBehaviour
{
    [Header("Comportamientos")]

    // Referencia a los parametros de la rana
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float rangoDeteccion = 10f;
    [SerializeField] private Barra_Salto barraSalto;
    [SerializeField] private Ataque_Lengua ataqueLengua;

    // Lista de enemigos en rango
    [SerializeField] private List<GameObject> enemigosEnRango = new List<GameObject>();

    // Rigidbody de la rana
    private Rigidbody rb; 

  
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update - Lo que se Actualiza 
    void Update()
    {
        EnMovimiento();
        barraSalto.ContrarSalto();

        if (Input.GetKeyDown(KeyCode.E))
        {
            ataqueLengua.AtacarEnemigoMasCercano(enemigosEnRango); // Llama al ataque en Ataque_Lengua
        }
    }

    // Control del movimiento de la rana 
    private void EnMovimiento()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;
        float movimientoVertical = Input.GetAxisRaw("Vertical") * velocidadMovimiento;
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        rb.AddForce(movimiento);
    }

    // --- Detectar Enemigos ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo") && !enemigosEnRango.Contains(other.gameObject)) // Si es un emeigo y no esta en la lista
        {
            enemigosEnRango.Add(other.gameObject);
            Debug.Log("Enemigo detectado en rango: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            enemigosEnRango.Remove(other.gameObject);
            Debug.Log("Enemigo fuera de rango: " + other.gameObject.name);
        }
    }

    // --- Libera el movimiento de la rana al destruir al enemigo ---
    public void LiberarMovimiento()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("Movimiento del jugador liberado.");
    }


    // Grafico de la capsula
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }
}
