using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dano_rata : MonoBehaviour
{
    [Header("Configuraci�n de Da�o")]
    [SerializeField] private int cantidadDa�o = 1; // Da�o que se aplicar� al enemigo

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisiona es un enemigo
        if (other.CompareTag("Jugador"))
        {
            // Intenta obtener el script de control del enemigo
            Rana_Control jugador = other.GetComponent<Rana_Control>();
          
            if (jugador != null)
            {
                // Aplica el da�o al enemigo
                jugador.RecibirDano(cantidadDa�o);
                Debug.Log("Se ha aplicado da�o al enemigo. Vida restante: " + jugador.VidaRana);
            }
        }
    }
}
