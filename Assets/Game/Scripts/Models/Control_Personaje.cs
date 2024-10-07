using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Personaje : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private float distanciaChequeoSuelo = 1.1f;

    private Rigidbody rb;
    private Control_Enemigo EnemigoActual;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ManejarMovimiento();
        Condicion_Salto();
    }
    private void ManejarMovimiento()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal") * velocidadMovimiento;
        float movimientoVertical = Input.GetAxis("Vertical") * velocidadMovimiento;

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        rb.AddForce(movimiento);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemigo") && Input.GetKeyDown(KeyCode.E)){

            EnemigoActual = other.GetComponent<Control_Enemigo>();
        }
    }

    private void Condicion_Salto()
    {
        if (Input.GetKeyDown(KeyCode.Space) && EstaEnElSuelo())
        {
            SaltarNormal();
        }
    }

    private void SaltarNormal()
    {
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
    }

    private bool EstaEnElSuelo()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanciaChequeoSuelo);
    }
}
