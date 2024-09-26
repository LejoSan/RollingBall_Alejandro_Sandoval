using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [SerializeField] private float Velocidad_Movimiento = 3f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Mover_Personaje();
    }

    private void Mover_Personaje()
    {
        float movimientoHorizontal = Input.GetAxis("Horizontal") * Velocidad_Movimiento;
        float movimientoVertical = Input.GetAxis("Vertical") * Velocidad_Movimiento;

        Vector3 movimiento = new Vector3(movimientoHorizontal, 0.0f, movimientoVertical);

        rb.AddForce(movimiento);
    }
}
