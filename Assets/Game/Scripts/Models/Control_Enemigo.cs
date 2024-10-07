using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Enemigo : MonoBehaviour
{

    [SerializeField] private int vida = 5;

    public int Vida { get => vida; set => vida = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RecibirDahno(int cantidad)
    {
        vida = vida - cantidad;
        if (vida <= 0)
        {
            DestruirEnemigo();
        }
    }

    private void DestruirEnemigo()
    {
        Destroy(gameObject);
    }

}
