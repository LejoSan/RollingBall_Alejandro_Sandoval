using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Final : MonoBehaviour
{
    [SerializeField] GameObject victoria;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
                victoria.SetActive(true);
                Debug.Log("GANASTE");
            }
        }
    }
}
