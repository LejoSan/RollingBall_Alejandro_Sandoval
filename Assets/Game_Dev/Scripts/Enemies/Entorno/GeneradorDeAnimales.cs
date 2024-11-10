using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorDeAnimales : MonoBehaviour
{
    [Header("Configuración del Generador")]
    [SerializeField] private GameObject prefabAnimal;
    [SerializeField] private Transform puntoInicio;
    [SerializeField] private Transform puntoFinal;
    [SerializeField] private float intervaloGeneracion = 1f; // Generar un animal cada segundo

    public Transform PuntoInicio { get => puntoInicio; set => puntoInicio = value; }
    public Transform PuntoFinal { get => puntoFinal; set => puntoFinal = value; }

    void Start()
    {
        StartCoroutine(GenerarAnimales());
    }

    private IEnumerator GenerarAnimales()
    {
        while (true)
        {
            // Instancia un nuevo animal en el punto de inicio
            GameObject nuevoAnimal = Instantiate(prefabAnimal, PuntoInicio.position, Quaternion.identity);

            // Asigna el punto final al animal
            MovimientoAnimal movimiento = nuevoAnimal.GetComponent<MovimientoAnimal>();
            movimiento.PuntoFinal = PuntoFinal;

            yield return new WaitForSeconds(intervaloGeneracion); // Espera antes de generar otro animal
        }
    }
}
