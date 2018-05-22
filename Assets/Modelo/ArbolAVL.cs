using System;

using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class ArbolAVL {
    private NodoAVL raiz;
    private int estadoProceso;
    private string rec = "";

    public ArbolAVL()
    {
        
        this.raiz = null;
        this.estadoProceso = 0;
    }

    public NodoAVL retornaRaiz()
    {
        return this.raiz;
    }
    public int retornaEstadoProceso()
    {
        int x = estadoProceso;
        estadoProceso = 0;
        return x;
    }

    public void asignaRaiz(NodoAVL x)
    {
        raiz = x;
    }

    public void insertar(int dato)
    {
        this.asignaRaiz(insertar(this.retornaRaiz(), dato));
    }
    public void eliminar(int dato)
    {
        this.asignaRaiz(eliminar(this.retornaRaiz(), dato));
    }

    private NodoAVL insertar(NodoAVL nodo, int dato)
    {
        if (nodo == null)
        {
            estadoProceso = 0;
            NodoAVL x = new NodoAVL(dato);
            x.asignaAltura(1);
            return x;
        }
        if (dato == (int)nodo.retornaDato())
        {
            estadoProceso = 1;
            //print("Dato repetido");
            return nodo;
        }
        if (dato < (int)nodo.retornaDato())
        {
            nodo.asignaLi(insertar(nodo.retornaLi(), dato));
        }
        else
        {
            nodo.asignaLd(insertar(nodo.retornaLd(), dato));
        }
        nodo.asignaAltura(Math.Max(this.altura(nodo.retornaLi()), this.altura(nodo.retornaLd())) + 1);

        nodo = resolverDesbalanceIns(dato, nodo);

        return nodo;
    }

    private NodoAVL resolverDesbalanceIns(int dato, NodoAVL nodo)
    {
        int factBalance = this.retornaBalance(nodo);
        if (factBalance > 1 && dato < (int)nodo.retornaLi().retornaDato())
        {
            return rotacionALaDer(nodo);
        }
        if (factBalance < -1 && dato > (int)nodo.retornaLd().retornaDato())
        {
            return rotacionALaIzq(nodo);
        }
        if (factBalance > 1 && dato > (int)nodo.retornaLi().retornaDato())
        {
            nodo.asignaLi(rotacionALaIzq(nodo.retornaLi()));
            return rotacionALaDer(nodo);
        }
        if (factBalance < -1 && dato < (int)nodo.retornaLd().retornaDato())
        {
            nodo.asignaLd(rotacionALaDer(nodo.retornaLd()));
            return rotacionALaIzq(nodo);
        }
        return nodo;
    }
    private NodoAVL eliminar(NodoAVL nodo, int dato)
    {
        if (nodo == null)
        {
            estadoProceso = 1;
            return null;
        }
        if (dato < (int)nodo.retornaDato())
        {
            nodo.asignaLi(eliminar(nodo.retornaLi(), dato));
        }
        else if (dato > (int)nodo.retornaDato())
        {
            nodo.asignaLd(eliminar(nodo.retornaLd(), dato));
        }
        else
        {
            estadoProceso = 0;
            if (nodo.retornaLi() == null && nodo.retornaLd() == null)
            {
                //print("eliminando hoja");
                return null;
            }
            if (nodo.retornaLi() == null)
            {
                //print("removiendo hijo derecho");
                nodo = null;
                return nodo.retornaLd();
            }
            else if (nodo.retornaLd() == null)
            {
                //print("Removiendo hijo izquierdo");
                nodo = null;
                return nodo.retornaLi();
            }
            //print("Removiendo con 2 hijos");
            NodoAVL tempNodo = this.retornaPredecesor(nodo.retornaLi());
            nodo.asignaDato(tempNodo.retornaDato());
            nodo.asignaLi(eliminar(nodo.retornaLi(), (int)tempNodo.retornaDato()));
        }
        nodo.asignaAltura(Math.Max(this.altura(nodo.retornaLi()), this.altura(nodo.retornaLd())) + 1);
        return resolverDesbalanceDel(nodo);
    }
    private NodoAVL resolverDesbalanceDel(NodoAVL nodo)
    {
        int factBalance = this.retornaBalance(nodo);
        if (factBalance > 1)
        {
            if (this.retornaBalance(nodo.retornaLi()) < 0)
            {
                nodo.asignaLi(rotacionALaIzq(nodo.retornaLi()));
            }
            return rotacionALaDer(nodo);
        }
        if (factBalance < -1)
        {
            if (this.retornaBalance(nodo.retornaLd()) > 0)
            {
                nodo.asignaLd(rotacionALaDer(nodo.retornaLd()));
            }
            return rotacionALaIzq(nodo);
        }
        return nodo;
    }
    /**
     * Realiza una rotacion a la derecha desde un nodo ingresado como parametro
     * que tiene un factor de balance superior a 1 o inferior a -1
     *
     * @param nodo nodo de la clase NodoAVL que tiene un factor de balance
     * superior a 1 o inferior a -1
     * @return La nueva raiz del subarbol.
     */
    private NodoAVL retornaPredecesor(NodoAVL nodo)
    {
        NodoAVL predecesor = nodo;
        while (predecesor.retornaLd() != null)
        {
            predecesor = predecesor.retornaLd();
        }
        return predecesor;
    }
    private NodoAVL rotacionALaDer(NodoAVL nodo)
    {
        //print("Rotando a la derecha");
        NodoAVL tempNodoIzq = nodo.retornaLi();
        NodoAVL t = tempNodoIzq.retornaLd();
        tempNodoIzq.asignaLd(nodo);
        nodo.asignaLi(t);
        nodo.asignaAltura(Math.Max(altura(nodo.retornaLi()), altura(nodo.retornaLd())) + 1);
        tempNodoIzq.asignaAltura(Math.Max(altura(tempNodoIzq.retornaLi()), altura(tempNodoIzq.retornaLd())) + 1);

        return tempNodoIzq;
    }

    private NodoAVL rotacionALaIzq(NodoAVL nodo)
    {
        //print("Rotando a la izquierda");
        NodoAVL tempNodoDer = nodo.retornaLd();
        NodoAVL t = tempNodoDer.retornaLi();
        tempNodoDer.asignaLi(nodo);
        nodo.asignaLd(t);
        nodo.asignaAltura(Math.Max(altura(nodo.retornaLi()), altura(nodo.retornaLd())) + 1);
        tempNodoDer.asignaAltura(Math.Max(altura(tempNodoDer.retornaLi()), altura(tempNodoDer.retornaLd())) + 1);

        return tempNodoDer;
    }

   

    private int altura(NodoAVL nodo)
    {
        if (nodo == null)
        {
            return -1;
        }
        return nodo.retornaAltura();
    }

    private int retornaBalance(NodoAVL nodo)
    {
        if (nodo == null)
        {
            return 0;
        }
        return altura(nodo.retornaLi()) - altura(nodo.retornaLd());
    }

    
    public String inOrden(NodoAVL raiz) {
        String linea = "";
        if (raiz != null) {
            linea += inOrden(raiz.retornaLi());
            linea += " " + raiz.retornaDato() + " ";
            linea += inOrden(raiz.retornaLd());
        }
        return linea;
    }

    public String preOrden(NodoAVL raiz) {
        String linea = "";
        if (raiz != null) {
            linea += " " + raiz.retornaDato() + " ";
            linea += preOrden(raiz.retornaLi());
            linea += preOrden(raiz.retornaLd());
        }
        return linea;
    }

    public String posOrden(NodoAVL raiz) {
        String linea = "";
        if (raiz != null) {
            linea += posOrden(raiz.retornaLi());
            linea += posOrden(raiz.retornaLd());
            linea += " " + raiz.retornaDato() + " ";
        }
        return linea;
    }

   

    public int numeroRegistros() {
        return numeroRegistros;
    }

    private int retornaBalance(NodoAVL nodo) {
        if (nodo == null) {
            return 0;
        }
        return altura(nodo.retornaLi()) - altura(nodo.retornaLd());
    }

    public String recorridoBFS(NodoAVL nodo) {
        String linea = "";
        Queue<NodoAVL> cola = new LinkedList();
        cola.add(nodo);
        while (!cola.isEmpty()) {
            nodo = cola.remove();
            linea += " " + nodo.retornaDato() + " ";
            if (nodo.retornaLi() != null) {
                cola.add(nodo.retornaLi());
            }
            if (nodo.retornaLd() != null) {
                cola.add(nodo.retornaLd());
            }

        }
        return linea;

    }

    public String recorridoDFS(NodoAVL nodo) {
        String linea = "", lineaAux = "";

        if (!(nodo.retornaLi() == null)) {

            linea += " " + (nodo.retornaDato()) + " ";
            lineaAux += recorridoDFS(nodo.retornaLi());
        } else {

            linea += " " + (nodo.retornaDato()) + " ";

        }

        if (!(nodo.retornaLd() == null)) {
            lineaAux += recorridoDFS(nodo.retornaLd());
        }

        return linea + lineaAux;

    }

    public void esLleno() {
        if (numeroRegistros == (Math.pow(2, raiz.retornaAltura())) - 1) {
            Console.WriteLine("Es lleno");
        } else {
            Console.WriteLine("No es lleno");
        }
    }

    public boolean ancestros(NodoAVL nodo, Object dato, Stack pila) {

        if (nodo.retornaDato() == dato) {
            return true;
        }

        if (nodo.retornaLi() != null) {
            if (ancestros(nodo.retornaLi(), dato, pila)) {
                pila.add(nodo.retornaLi().retornaDato());
                return true;
            }
        }

        if (nodo.retornaLd() != null) {
            if (ancestros(nodo.retornaLd(), dato, pila)) {
                pila.add(nodo.retornaLd().retornaDato());
                return true;
            }
        }

        return false;

    }

    public String ancestros(NodoAVL nodo, Object dato) {
        String linea = "";
        Stack<Object> pila = new Stack<Object>();
        if (!ancestros(nodo, dato, pila)) {
            return "No existe el registro";
        }
        pila.add(nodo.retornaDato());
        while (!pila.isEmpty()) {
            linea += " " + pila.pop() + " ";
        }
        return linea;
    }

    
}
