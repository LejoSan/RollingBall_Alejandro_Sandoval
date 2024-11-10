using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control_Animacion_Avispa : MonoBehaviour
{
    private Animation anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animation>();

        anim.Play("Idle");
    }

    // Update is called once per frame
    public void Atacar()
    {
        anim.Play("Atacar"); 
    }

    public void VolverIdle()
    {
        anim.Play("Idle"); 
    }
}
