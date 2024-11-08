using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Barra_Salto : MonoBehaviour
{
    [SerializeField] private Slider barraSalto;
    [SerializeField] private float fuerzaMaximaSalto = 20f;
    [SerializeField] private float distanciaChequeoSuelo = 1.1f;

    [SerializeField] private float fuerzaActual = 0f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    //public void ContrarSalto()
    //{
    //    if (EstaEnElSuelo())
    //    {
    //        if (Input.GetKey(KeyCode.Space))
    //        {
    //            CargarSalto();
    //        }

    //        if (Input.GetKeyUp(KeyCode.Space))
    //        {
    //            Debug.Log("ESTYOY PRESIONANDO EL SPACE ");
    //            Saltar();
    //        }
    //    }
    //}

    public void CargarSalto()
    {
        fuerzaActual += Time.deltaTime * 20;  
        fuerzaActual = Mathf.Clamp(fuerzaActual, 0, fuerzaMaximaSalto);
        barraSalto.value = fuerzaActual / fuerzaMaximaSalto;
    }

    public void Saltar()
    {
        rb.AddForce(Vector3.up * fuerzaActual, ForceMode.Impulse);
        fuerzaActual = 0f;
        barraSalto.value = 0f;
    }

    public bool EstaEnElSuelo()
    {
        return Physics.Raycast(transform.position, Vector3.down, distanciaChequeoSuelo);
    }
}
