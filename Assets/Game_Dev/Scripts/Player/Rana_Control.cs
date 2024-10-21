using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rana_Control : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 5f;
    [SerializeField] private Barra_Salto barraSalto;
    [SerializeField] private Ataque_Lengua ataqueLengua;

    private Rigidbody rb;

    public Ataque_Lengua AtaqueLengua { get => ataqueLengua; set => ataqueLengua = value; }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        // Solo permitir movimiento si no está atacando
        if (!ataqueLengua.estaAtacando)  
        {
            EnMovimiento(); 
            barraSalto.ContrarSalto(); 
        }

        // Llamar al ataque de la lengua
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Presionaste la E");
            ataqueLengua.LanzarLengua();
        }
    }

    private void EnMovimiento()
    {
        float movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadMovimiento;
        float movimientoVertical = Input.GetAxisRaw("Vertical") * velocidadMovimiento;
        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);
        rb.AddForce(movimiento);
    }
}
