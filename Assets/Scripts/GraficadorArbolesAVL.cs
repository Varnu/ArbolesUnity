using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraficadorArbolesAVL : MonoBehaviour {

    // Use this for initialization
    public GameObject nodo;
    public GameObject linea;
    private ArbolAVL arbol;
    private Stack padres = null;
    private Hashtable tamañoSubArboles = null;
    private bool dirty = true;
    private float distPadreHijo = 1.5f, distHijoHijo = 1.5f;
    private Vector3 empty = new Vector3(0, 0,0);


    private void Start()
    {
        
    }

    /**
     * Constructor de la clase ArbolExpresionGrafico.
     * El constructor permite inicializar los atributos de la clase ArbolExpresionGrafico
     * y llama al método repaint(), que es el encargado de pintar el Arbol.
     * @param miExpresion: dato de tipo ArbolExpresion que contiene el Arbol a
     * dibujar.
     */
    public void Inicializar(ArbolAVL arbol)
    {
        this.arbol = arbol;
        padres = new Stack();
        tamañoSubArboles = new Hashtable();
        dirty = true;
        dibujar();

    }


    /**
     * Calcula las posiciones de los respectivos subárboles y de cada nodo que 
     * forma parte de ese subárbol, para conocer en que posición van a ir dibujados
     * los rectángulos representativos del árbol de la expresión.
     */
    private void calcularPosiciones()
    {
        tamañoSubArboles.Clear();
        NodoAVL raiz = this.arbol.retornaRaiz();
        if (raiz != null)
        {
            calcularTamañoSubarbol(raiz);
            calcularPosicion(raiz, Int32.MaxValue, Int32.MaxValue, 0);
        }
    }

    /**
     * Calcula el tamaño de cada subárbol y lo agrega al objeto tamañoSubArboles de la clase
     * de tipo HashMap que va a contener la coleccion de todos los 
     * subárboles que contiene un arbol.
     * @param n:Objeto de la clase NodoB <T> que se utiliza como
     * referencia calcular el tamaño de cada subárbol.
     * @return Vector3 con el tamaño de cada subárbol.
     */
    private Vector3 calcularTamañoSubarbol(NodoAVL n)
    {
        if (n == null)
            return new Vector3(0, 0,0);

        Vector3 ld = calcularTamañoSubarbol(n.retornaLi());
        Vector3 rd = calcularTamañoSubarbol(n.retornaLd());

        float h = distPadreHijo + Math.Max(ld.y, rd.y);
        float w = ld.x + distHijoHijo + rd.x;

        Vector3 d = new Vector3(w, h);
        tamañoSubArboles.Add(n, d);

        return d;
    }


    /**
     * Calcula la ubicación de cada nodo de cada subárbol y agrega cada nodo con 
     * un objeto de tipo Rectangule que tiene la ubicación y la información específica de dónde 
     * va a ser dibujado.
     * @param n: Objeto de tipo NodoB <T> que se utiliza como
     * referencia para calcular la ubicación de cada nodo.
     * @param left: int con alineación y orientación a la izquierda.
     * @param right: int con alineación y orientación a la derecha.
     * @param top: int con el tope.
     */
    private void calcularPosicion(NodoAVL n, float left, float right, float top)
    {
        Vector3 ld = empty;
        Vector3 rd = empty;
        if (n == null)
        {
            return;
        }

        if(n.retornaLi() != null)
        {
            ld = (Vector3)tamañoSubArboles[n.retornaLi()];
        }
        else
        {
            ld = new Vector3(-10,-10,-10);
        }
        if (ld.Equals(new Vector3(-10,-10,-10)))
        {
            ld = empty;
        }


        if (n.retornaLd()!=null)
        {
            rd = (Vector3)tamañoSubArboles[n.retornaLd()];

        }
        else
        {
            rd = new Vector3(-10, -10, -10);
        }
        if (rd.Equals(new Vector3(-10, -10, -10)))
        {
            rd = empty;
        }


        GameObject padre;

        float center = 0;

        if (right != Int32.MaxValue)
        {
            center = right - rd.x - distHijoHijo / 2;
        } 
        else if (left != Int32.MaxValue)
        {
            center = left + ld.x + distHijoHijo / 2;
        }
        
        

        GameObject p = Instantiate(nodo, new Vector3(center - 3, -top, 0), Quaternion.identity);
        p.SetActive(true);
        if (padres.Count > 0)
        {
            padre = (GameObject)padres.Pop();
            //GameObject linea = Instantiate(this.linea);
            //Linea cLinea = linea.GetComponent<Linea>();
           // cLinea.asignaPosicionesLinea(padre.transform.position, new Vector3(center - 3, -top, 0));
            //linea.transform.SetParent(padre.transform);
        }
        else
        {
            padre = p;
        }
        p.transform.SetParent(padre.transform);
        EsferaNodoAVL cp = p.GetComponent<EsferaNodoAVL>();
        cp.asignaDestinoLinea(padre.transform.position, p.transform.position);
        p.name = Convert.ToString(n.retornaDato());
        padres.Push(p);


        calcularPosicion(n.retornaLi(), Int32.MaxValue, center - distHijoHijo / 2, top + distPadreHijo);
        padres.Push(p);
        calcularPosicion(n.retornaLd(), center + distHijoHijo / 2, Int32.MaxValue, top + distPadreHijo);
    }

    /**
     * Dibuja el árbol teniendo en cuenta las ubicaciones de los nodos y los 
     * subárboles calculadas anteriormente.
     * @param g: Objeto de la clase Graphics2D que permite realizar el dibujo de las líneas, rectangulos y del String de la información que contiene el Nodo.
     * @param n: Objeto de la clase NodoB <T> que se utiliza como referencia para dibujar el árbol.
     * @param puntox: int con la posición en x desde donde se va a dibujar la línea hasta el siguiente hijo.
     * @param puntoy: int con la posición en y desde donde se va a dibujar la línea hasta el siguiente hijo.
     * @param yoffs: int con la altura del FontMetrics.
     */
    
    public void dibujar()
    {
        if (dirty)
        {
            calcularPosiciones();
            dirty = false;
        }
        

    }


    /**
      * Sobreescribe el metodo paint y se encarga de pintar todo el árbol.
      * @param g: Objeto de la clase Graphics.
      */
    
}
