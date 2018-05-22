using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EsferaNodoAVL : MonoBehaviour {
    public GameObject mensaje;
	// Use this for initialization
	void Start () {
        mensaje = Instantiate(mensaje,transform.position+new Vector3(0,0,-0.5f),Quaternion.identity) as GameObject;
        TextMeshPro cM = mensaje.GetComponent<TextMeshPro>();
        cM.text = this.name;
        mensaje.transform.SetParent(transform);
               
    }
    public void asignaDestinoLinea(Vector3 a, Vector3 b)
    {
        LineRenderer x = GetComponent<LineRenderer>();
        x.SetPosition(1, a);
        x.SetPosition(0, b);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
