using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rana_Control : MonoBehaviour
{
    [Header("Comportamientos")]

    // Referencia a los parametros de la rana
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private int Vida_Rana = 5;
    [SerializeField] private float rangoDeteccion = 10f;
    [SerializeField] private Barra_Salto barraSalto;
    [SerializeField] private Ataque_Lengua ataqueLengua;

    private float rotacionSuave = 0.1f;
    float velocidadRotacionSuave;

    [Header("Animaciones")]
    private Animator animator;

    // Lista de enemigos en rango
    [SerializeField] private List<GameObject> enemigosEnRango = new List<GameObject>();

    // Rigidbody de la rana
    private Rigidbody rb;

    public int VidaRana { get => Vida_Rana; set => Vida_Rana = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

    }
    void FixedUpdate()
    {
        EnMovimiento();
    }
    // Update - Lo que se Actualiza 
    void Update()
    {
        //EnMovimiento();
        //barraSalto.ContrarSalto();

        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ataqueLengua.AtacarEnemigoMasCercano(enemigosEnRango); // Llama al ataque en Ataque_Lengua
        //}
        // Gestiona la carga y liberación del salto

        if (Input.GetKey(KeyCode.Space))
        {
            barraSalto.CargarSalto();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            barraSalto.Saltar();
            animator.SetTrigger("Saltar");
        }

    
        if (Input.GetKeyDown(KeyCode.E))
        {
            ataqueLengua.AtacarEnemigoMasCercano(enemigosEnRango);
            

        }
    }

    // Control del movimiento de la rana 
    private void EnMovimiento()
    {
        //float movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;
        //float movimientoVertical = Input.GetAxisRaw("Vertical") * velocidadMovimiento;
        //Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        //rb.AddForce(movimiento);

        float movimientoHorizontal = Input.GetAxisRaw("Horizontal");
        float movimientoVertical = Input.GetAxisRaw("Vertical");

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical).normalized;

        if (movimiento.magnitude>= 0.1f)
        {
            float anguloARotar = Mathf.Atan2(movimiento.x, movimiento.z) * Mathf.Rad2Deg;
            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, anguloARotar, ref velocidadRotacionSuave, rotacionSuave);

            transform.rotation = Quaternion.Euler(0f, angulo, 0f);

            Vector3 direccionDelMovimiento = Quaternion.Euler(0f, anguloARotar, 0f) * Vector3.forward;
            rb.MovePosition(rb.position + direccionDelMovimiento * velocidadMovimiento * Time.fixedDeltaTime);

        }


        // Animacion
        bool SeEstaMoviendo = movimientoHorizontal != 0 || movimientoVertical != 0;
        animator.SetBool("Caminar", SeEstaMoviendo);
    }

    // --- Detectar Enemigos ---
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemigo") && !enemigosEnRango.Contains(other.gameObject)) // Si es un emeigo y no esta en la lista
        {
            enemigosEnRango.Add(other.gameObject);
            Debug.Log("Enemigo detectado en rango: " + other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemigo"))
        {
            enemigosEnRango.Remove(other.gameObject);
            Debug.Log("Enemigo fuera de rango: " + other.gameObject.name);
        }
    }

    // --- Libera el movimiento de la rana al destruir al enemigo ---
    public void LiberarMovimiento()
    {
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        Debug.Log("Movimiento del jugador liberado.");
    }


    // Grafico de la capsula
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, rangoDeteccion);
    }

    public void MuerteRana()
    {
        if (VidaRana <= 0)
        {
            Debug.Log("LA RANA MURIO-----------------------------------------");
        }
    }
}
