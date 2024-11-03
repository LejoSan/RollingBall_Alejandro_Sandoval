using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Lengua : MonoBehaviour
{
    [Header("Comportamientos y Referencias")]

    [SerializeField] private Transform puntoLengua;                 
    [SerializeField] private LineRenderer lenguaRenderer;           
    [SerializeField] private float velocidadLengua = 10f;           
    [SerializeField] private float velocidadMovimientoHaciaEnemigo = 5f; 

    // Comportamiento de la lengua 

    private bool lenguaExtendida = false;                           
    private bool moviendoHaciaEnemigo = false;                      
    private Enemigo_Control enemigoObjetivo;                       
    private Vector3 posicionObjetivo;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // --- Ataque - Busqueda Enemigo ---

    // Calcula el area para detectar al enemigo mas cercano.
    private GameObject EncontrarEnemigoMasCercano(List<GameObject> enemigosEnRango)
    {
        GameObject enemigoMasCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (GameObject enemigo in enemigosEnRango)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);

            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                enemigoMasCercano = enemigo;
            }
        }
        return enemigoMasCercano;
    }

    // Ejecuta ataque al mas cercano.
    public void AtacarEnemigoMasCercano(List<GameObject> enemigosEnRango)
    {
      
        for (int i = enemigosEnRango.Count - 1; i >= 0; i--)  // Elimina enemigos nulos de la lista
        {
            if (enemigosEnRango[i] == null)
            {
                enemigosEnRango.RemoveAt(i);
            }
        }

        if (enemigosEnRango.Count > 0)
        {
            GameObject enemigoMasCercano = EncontrarEnemigoMasCercano(enemigosEnRango); // llama el enemigo mas cercano 

            if (enemigoMasCercano != null)
            {
                Enemigo_Control enemigoControl = enemigoMasCercano.GetComponent<Enemigo_Control>();

                if (enemigoControl != null)
                {
                    AtaqueConLengua(enemigoControl);
                    Debug.Log("Atacando al enemigo más cercano: " + enemigoMasCercano.name);
                }
            }
        }
        else
        {
            Debug.Log("No hay enemigos en rango para atacar.");
        }
    }


}
