using UnityEngine;
using UnityEditor;

public class NodoAVL
{
    int dato;
    NodoAVL li;
    NodoAVL ld;
    int altura;
    public NodoAVL(int dato)
    {
        this.dato = dato;
        li = null;
        ld = null;
        altura = 0;
    }
    public void asignaDato(int dato)
    {
        this.dato = dato;
    }
    public void asignaLi(NodoAVL li)
    {
        this.li = li;
    }
    public void asignaLd(NodoAVL ld)
    {
        this.ld = ld;
    }
    public void asignaAltura(int fb)
    {
        this.altura = fb;
    }
    public int retornaDato()
    {
        return dato;
    }
    public NodoAVL retornaLi()
    {
        return li;
    }
    public NodoAVL retornaLd()
    {
        return ld;
    }
    public int retornaAltura()
    {
        return altura;
    }
}