using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuArboles : MonoBehaviour {
    public GameObject ventanaEmerg;
    public GameObject emergenteFunc;
    ArbolAVL x = new ArbolAVL();
    void Start()
    {
        
    }

    public void InsertarBtnClicked()
    {
        ventanaEmerg.SetActive(true);
        VentanaEmergente ventana = emergenteFunc.GetComponent<VentanaEmergente>();
        ventana.Inicializar(x,0);
    }
    public void EliminarBtnClicked()
    {
        ventanaEmerg.SetActive(true);
        VentanaEmergente ventana = emergenteFunc.GetComponent<VentanaEmergente>();
        ventana.Inicializar(x, 1);
    }
	
}
