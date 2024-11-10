using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{

    [Header("Configuraci�n de Power-Up")]
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
        
        if (other.gameObject.layer == LayerMask.NameToLayer("Jugador"))
        {
            
            Rana_Control jugador = other.GetComponent<Rana_Control>();
            if (jugador != null)
            {
                
                jugador.AumentarVida(cantidadVida);

                
                Debug.Log("Vida del jugador aumentada en: " + cantidadVida);

                
                Destroy(gameObject);
            }
        }
    }
}