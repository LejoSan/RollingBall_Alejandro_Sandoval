using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dano_rata : MonoBehaviour
{
    [Header("Configuración de Daño")]
    [SerializeField] private int cantidadDaño = 1; // Daño que se aplicará al enemigo

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisiona es un enemigo
        if (other.CompareTag("Jugador"))
        {
            // Intenta obtener el script de control del enemigo
            Rana_Control jugador = other.GetComponent<Rana_Control>();
          
            if (jugador != null)
            {
                // Aplica el daño al enemigo
                jugador.RecibirDano(cantidadDaño);
                Debug.Log("Se ha aplicado daño al enemigo. Vida restante: " + jugador.VidaRana);
            }
        }
    }
}
