using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma : MonoBehaviour
{
    [SerializeField] private float velocidad;
    [SerializeField] private Vector3 direccion = new Vector3(0, 0, 0);
    [SerializeField] private float CambioDireccion;
    private float timer;

    private bool cambiarDireccion;
    // Start is called before the first frame update
    void Start()
    {
        direccion = direccion.normalized;
        timer = CambioDireccion;
        cambiarDireccion = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer <= 0)
        {
            
            if (cambiarDireccion)
            {
                cambiarDireccion = false;
            }
            else
            {
                cambiarDireccion = true;
            }

            direccion = direccion * -1; 

            timer = CambioDireccion;
        }

        transform.Translate(direccion * velocidad * Time.deltaTime);
    }
}
