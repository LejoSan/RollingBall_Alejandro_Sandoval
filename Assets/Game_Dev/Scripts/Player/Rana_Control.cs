using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rana_Control : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private Barra_Salto barraSalto;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        EnMovimiento();

        barraSalto.ContrarSalto();
    }

    private void EnMovimiento()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;
        float movimientoVertical = Input.GetAxisRaw("Vertical") * velocidadMovimiento;
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        rb.AddForce(movimiento);
    }
}
