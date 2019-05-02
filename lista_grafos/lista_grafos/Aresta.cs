using System;
using System.Collections.Generic;
using System.Text;

namespace lista_grafos
{
    class Aresta
    {

        public Vertice verticeIncidente = new Vertice();
        public int peso;

        public void ligaVerticeGrafoNaoDirigido(Vertice vertice1, Vertice vertice2, int peso)
        {
            //seta peso
            Aresta aresta1 = new Aresta();
            Aresta aresta2 = new Aresta();
            aresta1.peso = peso;
            aresta2.peso = peso;

            aresta1.verticeIncidente = vertice2;
            vertice1.listaAresta.Add(aresta1);

            aresta2.verticeIncidente = vertice1;
            vertice2.listaAresta.Add(aresta2);
        }

        public void ligaVerticeGrafoDirigido(Vertice antecessor, Vertice sucessor, int peso)
        {
            //instancia uma aresta, indica seu peso e o seu incidente
            this.peso = peso;
            this.verticeIncidente = sucessor;
            antecessor.listaAresta.Add(this);
        }

        
    }
}
