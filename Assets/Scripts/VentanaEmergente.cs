using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VentanaEmergente : MonoBehaviour {
    public GameObject entrada;
    public GameObject emergente;
    public GameObject graficador;
    int codigo;
    ArbolAVL arbol;
	// Use this for initialization
	void Start () {
		
	}
    public void Inicializar(ArbolAVL arbol, int codigo)
    {
        this.arbol = arbol;
        this.codigo = codigo;
    }
    public void AceptarBtnClicked()
    {
        InputField valor = entrada.GetComponent<InputField>();
        if (arbol.retornaRaiz() != null)
        {
            String raiz = arbol.retornaRaiz().retornaDato().ToString();
            Destroy(GameObject.Find(raiz));
        }
        if (codigo == 0)
        {
            GraficadorArbolesAVL cGraficador = graficador.GetComponent<GraficadorArbolesAVL>();
            arbol.insertar(Convert.ToInt32(valor.textComponent.text));
            cGraficador.Inicializar(arbol);
            emergente.SetActive(false);
            valor.text = "";
        }
        else
        {
            GraficadorArbolesAVL cGraficador = graficador.GetComponent<GraficadorArbolesAVL>();
            arbol.eliminar(Convert.ToInt32(valor.textComponent.text));
            cGraficador.Inicializar(arbol);
            emergente.SetActive(false);
            valor.text = "";
        }

    }

}
