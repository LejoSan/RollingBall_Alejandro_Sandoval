using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Personaje : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private float distanciaChequeoSuelo = 1.1f;
    
    private bool EstaCominedo = false;
    [SerializeField] private float velocidadDesplazamientoRapido = 10f;
    [SerializeField] private int Danho = 1;

   

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
        if (!EstaCominedo)
        {
            EnMovimiento();
            Condicion_Salto();
        }

        if (EnemigoActual != null && Input.GetKey(KeyCode.E))
        {
            ComerEnemigo();
        }
        
       
    }
    private void EnMovimiento()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;
        float movimientoVertical = Input.GetAxisRaw("Vertical") * velocidadMovimiento;

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        rb.AddForce(movimiento);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemigo") && Input.GetKeyDown(KeyCode.E)){

            EnemigoActual = other.GetComponent<Control_Enemigo>();

            if (EnemigoActual != null)
            {
                IralEnemigo();
            }
        }
    }

    private void IralEnemigo()
    {
        EstaCominedo = true;

        Vector3 direccion = (EnemigoActual.transform.position - transform.position).normalized;
        rb.velocity = direccion * velocidadDesplazamientoRapido;
    }

    private void ComerEnemigo()
    {
        EnemigoActual.RecibirDahno(Danho);

        if (EnemigoActual == null || EnemigoActual.gameObject == null){

            SaltarNormal();
            EstaCominedo = false;
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
