using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{

    [Header("Configuración de Power-Up")]
    [SerializeField] private int cantidadVida = 1;
    //[SerializeField] private Vector3 velocidadRotacion = new Vector3(0, 0, 0);

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(velocidadRotacion * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto con el que colisiona es el jugador
        if (other.gameObject.layer == LayerMask.NameToLayer("Jugador"))
        {
            // Intenta obtener el script de la rana para aumentar su vida
            Rana_Control jugador = other.GetComponent<Rana_Control>();
            if (jugador != null)
            {
                // Llama a un método en el jugador para aumentar su vida
                jugador.AumentarVida(cantidadVida);

                // Muestra un mensaje en la consola
                Debug.Log("Vida del jugador aumentada en: " + cantidadVida);

                // Destruye el objeto Power-Up para que no se vuelva a recoger
                Destroy(gameObject);
            }
        }
    }
}