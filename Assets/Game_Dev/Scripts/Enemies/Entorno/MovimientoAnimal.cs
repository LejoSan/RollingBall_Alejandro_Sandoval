using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoAnimal : MonoBehaviour
{
    [Header("Puntos de Movimiento")]
    [SerializeField] private Transform puntoFinal;
    [SerializeField] private float velocidad = 3f;

    public Transform PuntoFinal { get => puntoFinal; set => puntoFinal = value; }

    void Update()
    {
        // Mueve el animal hacia el punto final
        transform.position = Vector3.MoveTowards(transform.position, PuntoFinal.position, velocidad * Time.deltaTime);

        // Si el animal llega al punto final, se destruye
        if (Vector3.Distance(transform.position, PuntoFinal.position) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}