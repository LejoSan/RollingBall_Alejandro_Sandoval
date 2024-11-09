using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Lengua : MonoBehaviour
{
    [Header("Comportamientos y Referencias")]

    [SerializeField] private Transform puntoLengua;                 
    [SerializeField] private LineRenderer lenguaRenderer;           
    [SerializeField] private float velocidadLengua = 10f;           
    [SerializeField] private float velocidadMovimientoHaciaEnemigo = 5f;
    [SerializeField] private int Danho = 1;

    // Comportamiento de la lengua 

    private bool lenguaExtendida = false;                           
    private bool moviendoHaciaEnemigo = false;                      
    private Enemigo_Control enemigoObjetivo;                       
    private Vector3 posicionObjetivo;

    private Animator animator;
    void Start()
    {
        if (lenguaRenderer != null)
        {
            lenguaRenderer.enabled = false;
        }

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lenguaExtendida && enemigoObjetivo != null)
            EstiraLenguaHaciaEnemigo();  // Si la lengua está extendida y hay un objetivo, se estira la lengua.

        if (moviendoHaciaEnemigo)
            MoverRanaHaciaEnemigo();    // Si la rana debe moverse al enemigo, la mueve.

        if (enemigoObjetivo != null && enemigoObjetivo.EnContactoParaDano && Input.GetKeyDown(KeyCode.E))
            HacerDanoAlEnemigo();   // Si la rana está en contacto y se pulsa "E", se hace daño al enemigo.
    }

   
   
    
    // --- Ataque - Busqueda Enemigo ---
    // Selección del Enemigo - El método recibe una lista de enemigos cerca
    public void AtacarEnemigoMasCercano(List<GameObject> enemigosEnRango)
    {
      
        for (int i = enemigosEnRango.Count - 1; i >= 0; i--)  // Elimina enemigos nulos de la lista
        {
            if (enemigosEnRango[i] == null)
            {
                enemigosEnRango.RemoveAt(i);
            }
        }

        if (enemigosEnRango.Count > 0)
        {
            GameObject enemigoMasCercano = EncontrarEnemigoMasCercano(enemigosEnRango);         // llama el enemigo mas cercano 

            if (enemigoMasCercano != null)
            {
                Enemigo_Control enemigoControl = enemigoMasCercano.GetComponent<Enemigo_Control>();

                if (enemigoControl != null)
                {
                    
                    AtaqueConLengua(enemigoControl);                                            // Atacar al enemigo
                    animator.SetTrigger("Comer");
                    animator.SetBool("EnAtaque", true);
                    Debug.Log("Atacando al enemigo más cercano: " + enemigoMasCercano.name);
                }
            }
        }
        else
        {
            Debug.Log("No hay enemigos en rango para atacar.");
        }
    }

    // Calcula el area para detectar al enemigo mas cercano.
    private GameObject EncontrarEnemigoMasCercano(List<GameObject> enemigosEnRango)
    {
        GameObject enemigoMasCercano = null;
        float distanciaMinima = Mathf.Infinity;

        foreach (GameObject enemigo in enemigosEnRango)
        {
            float distancia = Vector3.Distance(transform.position, enemigo.transform.position);

            if (distancia < distanciaMinima)
            {
                distanciaMinima = distancia;
                enemigoMasCercano = enemigo;
            }
        }
        return enemigoMasCercano;
    }


    // Configuración del Ataque
    private void PrepararAtaque(GameObject enemigo)
    {
        Enemigo_Control enemigoControl = enemigo.GetComponent<Enemigo_Control>();
        if (enemigoControl != null)
        {
            AtaqueConLengua(enemigoControl);
            Debug.Log("Atacando al enemigo más cercano: " + enemigo.name);
        }
    }

    // Iniciar el Ataque con la Lengua
    public void AtaqueConLengua(Enemigo_Control enemigo)
    {
        if (enemigo != null)
        {
            enemigoObjetivo = enemigo; 
            posicionObjetivo = enemigo.transform.position; 
            lenguaExtendida = true; 
            moviendoHaciaEnemigo = false; 


            lenguaRenderer.enabled = true;
            lenguaRenderer.positionCount = 2;
            lenguaRenderer.SetPosition(0, puntoLengua.position);
            lenguaRenderer.SetPosition(1, puntoLengua.position);
        }
    }

    // Metodo visual de la lengua hacia el enemigo.
    private void EstiraLenguaHaciaEnemigo()
    {
        lenguaRenderer.SetPosition(0, puntoLengua.position);
        lenguaRenderer.SetPosition(1, Vector3.MoveTowards(lenguaRenderer.GetPosition(1), posicionObjetivo, velocidadLengua * Time.deltaTime));

        if (Vector3.Distance(lenguaRenderer.GetPosition(1), posicionObjetivo) < 0.1f)
        {
            lenguaExtendida = false;
            moviendoHaciaEnemigo = true;
        }
    }

    // Metodo para moverse hacia el enemigo
    public void MoverRanaHaciaEnemigo()
    {
        if (enemigoObjetivo == null)
        {
            Debug.LogWarning("enemigoObjetivo es null. No se puede mover hacia un enemigo no asignado.");
            moviendoHaciaEnemigo = false; // Detiene el movimiento si no hay enemigo
            return;
        }


        Vector3 direccionHaciaEnemigo = (enemigoObjetivo.transform.position - transform.position).normalized;
        Quaternion rotacionObjetivo = Quaternion.LookRotation(direccionHaciaEnemigo);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * 5f);  // Ajusta la velocidad según sea necesari


        this.transform.position = Vector3.MoveTowards(this.transform.position, enemigoObjetivo.transform.position, Time.deltaTime * velocidadMovimientoHaciaEnemigo);      

        lenguaRenderer.SetPosition(0, puntoLengua.position);
        lenguaRenderer.SetPosition(1, enemigoObjetivo.transform.position);

        if (Vector3.Distance(this.transform.position, enemigoObjetivo.transform.position) < 0.1f)
        {
            moviendoHaciaEnemigo = false;
            enemigoObjetivo.EnContactoParaDano = true;
            Debug.Log("Contacto con el enemigo. Listo para atacar.");
        }
    }

    // Danho al enemigo
    private void HacerDanoAlEnemigo()
    {
        enemigoObjetivo.RecibirDahno(Danho);
        Debug.Log("Enemigo atacado, vida restante: " + enemigoObjetivo.Vida);

        if (enemigoObjetivo.Vida <= 0)
            DevoraEnemigo();
    }

    // Devorar al Enemigo
    private void DevoraEnemigo()
    {
        if (enemigoObjetivo != null) // Verifica que el enemigo exista antes de destruirlo
        {
            Destroy(enemigoObjetivo.gameObject);
            enemigoObjetivo = null;

            if (lenguaRenderer != null)
            {
                lenguaRenderer.enabled = false;
            }
            // Restablecer el estado de ataque en el Animator
            StartCoroutine(Reseteo_Estado(0.2f));

            Debug.Log("Enemigo devorado. Movimiento desbloqueado.");
        }
        //Destroy(enemigoObjetivo.gameObject); 
        //enemigoObjetivo = null;
        //lenguaRenderer.enabled = false;


        //ResetAttackState();
        //Debug.Log("Enemigo devorado. Movimiento desbloqueado.");
    }

    private IEnumerator Reseteo_Estado(float delay)
    {
        yield return new WaitForSeconds(delay);
        lenguaRenderer.enabled = false;
        animator.SetBool("EnAtaque", false);

        // Verifica si el Animator realmente ha salido del estado de ataque
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            // Forzar la transición a Idle si aún no ha cambiado
            animator.Play("Idle");
        }
    }

}
