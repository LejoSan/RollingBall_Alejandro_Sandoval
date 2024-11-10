using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cambio_Escena : MonoBehaviour
{

    [SerializeField] private GameObject activarObjeto;
    [SerializeField] private GameObject desactivarObjeto;
    public void CargarEscena(string Nombre_Escena)
    {
        SceneManager.LoadScene(Nombre_Escena);
    }

    //public void habilitarpanel(){

    //    gameObject.SetActive(true);
    //}
    public void HabilitarPanel()
    {
        if (activarObjeto != null)
        {
            activarObjeto.SetActive(true); // Activa el objeto deseado
        }

        if (desactivarObjeto != null)
        {
            desactivarObjeto.SetActive(false); // Desactiva el otro objeto
        }
    }
}
