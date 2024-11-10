using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pasanivel : MonoBehaviour
{
    [Header("Configuración de Escena")]
    [SerializeField] private string nombreEscena; 

    public void CargarEscena()
    {
        SceneManager.LoadScene(nombreEscena);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Jugador"))
        {
            Rana_Control jugador = other.GetComponent<Rana_Control>();

            if (jugador != null)
            {
                CargarEscena();
            }
        }
    }
}
