using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Lengua : MonoBehaviour
{

    // Variables de la lengua

    [SerializeField] private Transform puntoLengua; 
    [SerializeField] private float velocidadEstiramiento = 10f; 
    [SerializeField] private float distanciaMaxima = 5f;
    private float longitudLengua = 0f;


    //[SerializeField] private GameObject lengua;
    //private float longitudLengua = 0f;

    // Detectar enemigos

    private List<Transform> enemigosCercanos = new List<Transform>();
    private List<Transform> enemigosObjetivos = new List<Transform>();

 
    //[SerializeField] private int danho = 1;
    //private Transform enemigoCercano;
    //[SerializeField] private LayerMask enemigoLayer;



    // Variables de estado

    public bool estaAtacando = false;


    //private Vector3 escalaInicialLengua;
    //private Enemigo_Control enemigoObjetivo;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Atacar()
    {
        if (!estaAtacando && enemigosCercanos.Count > 0)
        {
            enemigosObjetivos.Clear();

            enemigosObjetivos.AddRange(enemigosCercanos);

            estaAtacando = true;
            
            
        }

        private IEnumerator 
    }

   
}
