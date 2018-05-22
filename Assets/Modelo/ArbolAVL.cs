using System;
using System.Collections;
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

    public String inOrden()
    {

        inorden(raiz);
        return this.rec;
    }

    public void inorden(NodoAVL aux)
    {
        if (aux != null)
        {
            inorden(aux.retornaLi());
            rec = rec + Convert.ToString(aux.retornaDato());
            inorden(aux.retornaLd());
        }
    }

    public void preOrden(NodoAVL raiz)
    {
        if (raiz != null)
        {
            // print(" " + raiz.retornaDato() + " ");
            preOrden(raiz.retornaLi());
            preOrden(raiz.retornaLd());
        }
    }
    public void posOrden(NodoAVL raiz)
    {
        if (raiz != null)
        {
            posOrden(raiz.retornaLi());
            posOrden(raiz.retornaLd());
            //print(" " + raiz.retornaDato() + " ");
        }
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
}
