using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Rana : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private float fuerzaSalto = 5f;
    [SerializeField] private float distanciaChequeoSuelo = 1.1f;

    [SerializeField] private float tiempoMaxCarga = 2f;
    [SerializeField] private float fuerzaSaltoMaxima = 10f;

    [SerializeField] private float rangoAtaque = 10f;
    [SerializeField] private LayerMask capaEnemigos;

    private float tiempoCarga = 0f;
    private bool cargandoSalto = false;


    private bool EstaCominedo = false;
    private bool HaLlegadoAlEnemigo = false;
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

        if (Input.GetKeyDown(KeyCode.E))
        {
            DetectarEnemigos();
        }

        if (HaLlegadoAlEnemigo && Input.GetKeyDown(KeyCode.E))
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

    private void DetectarEnemigos()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, rangoAtaque, capaEnemigos))
        {
            Control_Enemigo enemigo = hit.collider.GetComponent<Control_Enemigo>();
            if (enemigo != null)
            {
                IralEnemigo(enemigo);
            }
        }
    }

    private void IralEnemigo(Control_Enemigo enemigo)
    {
        EnemigoActual = enemigo;
        EnemigoActual.SiendoComido = true;
        EstaCominedo = true;

        Vector3 direccion = (EnemigoActual.transform.position - this.transform.position).normalized;
        rb.velocity = direccion * velocidadDesplazamientoRapido;

        StartCoroutine(DetenerseAlllegar());
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemigo") && Input.GetKeyDown(KeyCode.E))
        {

            EnemigoActual = other.GetComponent<Control_Enemigo>();

            if (EnemigoActual != null)
            {
                IralEnemigo();

            }
        }
    }

    private void IralEnemigo()
    {

        EnemigoActual.SiendoComido = true;

        EstaCominedo = true;

        Vector3 direccion = (EnemigoActual.transform.position - this.transform.position).normalized;

        rb.velocity = direccion * velocidadDesplazamientoRapido;
        Vector3 distancia = (EnemigoActual.transform.position - this.transform.position);

        //if(distancia.magnitude < 0.1f)
        //{
        //     Debug.Log("LLEGUE AL ENEMIGO ");

        //}
        //StartCoroutine(DetenerseAlllegar());

        StartCoroutine(DetenerseAlllegar());
    }

    private IEnumerator DetenerseAlllegar()
    {

        //while (Vector3.Distance(transform.position, EnemigoActual.transform.position) > 0.1f)
        //{
        //    yield return null;
        //}

        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector3.zero;
        Debug.Log("ME DETUVE ");

        HaLlegadoAlEnemigo = true;
        EstaCominedo = false;

        //while (HaLlegadoAlEnemigo)
        //{
        //    yield return null;
        //}

        //yield return new WaitForSeconds(0.5f);
        //rb.velocity = Vector3.zero;
        //HaLlegadoAlEnemigo = true;
        //EstaCominedo = false;
    }

    private void ComerEnemigo()
    {

        if (EnemigoActual != null)

        {

            EnemigoActual.RecibirDahno(Danho);

            Debug.Log("TE QUITE " + EnemigoActual.Vida);

            if (!EnemigoActual.EstaVivo())
            {
                Vector3 posicionEnemigo = EnemigoActual.transform.position;
                EnemigoActual.DestruirEnemigo();
                transform.position = posicionEnemigo;
                rb.velocity = Vector3.zero;
                HaLlegadoAlEnemigo = false;
                EstaCominedo = false;

                Debug.Log("TE MATE Y ME MOVI A TU POSICION");
                //EnemigoActual.DestruirEnemigo();
                //HaLlegadoAlEnemigo = false;
                //EstaCominedo = false;
                //Debug.Log("TE MATE");

            }

        }


        //if (EnemigoActual == null || EnemigoActual.gameObject == null){

        //    SaltarNormal();
        //    EstaCominedo = false;
        //}
    }

    private void Condicion_Salto()
    {
        if (Input.GetKey(KeyCode.Space) && EstaEnElSuelo())
        {
            cargandoSalto = true;
            tiempoCarga += Time.deltaTime;
            tiempoCarga = Mathf.Clamp(tiempoCarga, 0, tiempoMaxCarga);
            // Aqu� puedes mostrar una barra de carga con un UI Slider
        }

        if (Input.GetKeyUp(KeyCode.Space) && cargandoSalto)
        {
            float fuerzaActualSalto = Mathf.Lerp(0, fuerzaSaltoMaxima, tiempoCarga / tiempoMaxCarga);
            SaltarNormal(fuerzaActualSalto);
            cargandoSalto = false;
            tiempoCarga = 0;
            // Ocultar la barra de carga
        }
    }

    private void SaltarNormal(float fuerza)
    {
        rb.AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
    }

    private bool EstaEnElSuelo()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanciaChequeoSuelo);
    }
}