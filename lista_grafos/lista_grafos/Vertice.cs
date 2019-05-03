using System;
using System.Collections.Generic;
using System.Text;

namespace lista_grafos
{
    class Vertice
    {
        public List<Aresta> listaAresta = new List<Aresta>();
        public string nomeVertice;
        public bool visitado;

        public void setaVerticeGrafoNaoDirigido(Grafo grafo, string[] dados)
        {
            Vertice vertice1 = new Vertice();
            Vertice vertice2 = new Vertice();
            Aresta aresta = new Aresta();

            vertice1.nomeVertice = dados[0];
            vertice2.nomeVertice = dados[1];

            verificaSeGrafoJaContemVertices(grafo, ref vertice1, ref vertice2, dados);

            aresta.ligaVerticeGrafoNaoDirigido(vertice1, vertice2, int.Parse(dados[2]));

            grafo.adicionaVerticeGrafoNaoDirigido(grafo, vertice1);
            grafo.adicionaVerticeGrafoNaoDirigido(grafo, vertice2);
        }

        public void setaVerticeGrafoDirigido(Grafo grafo, string[] dados)
        {
            Vertice vertice1 = new Vertice();
            Vertice vertice2 = new Vertice();
            Aresta aresta = new Aresta();

            vertice1.nomeVertice = dados[0];
            vertice2.nomeVertice = dados[1];

            int direcaoAresta = int.Parse(dados[3]);

            verificaSeGrafoJaContemVertices(grafo, ref vertice1, ref vertice2, dados);

            if (direcaoAresta == 1)
            {
                aresta.ligaVerticeGrafoDirigido(vertice1, vertice2, int.Parse(dados[2]));
                grafo.adicionaVerticeGrafoNaoDirigido(grafo, vertice1);
                return;
            }
            else if (direcaoAresta == -1)
            {
                aresta.ligaVerticeGrafoDirigido(vertice2, vertice1, int.Parse(dados[2]));
                grafo.adicionaVerticeGrafoNaoDirigido(grafo, vertice2);
                return;
            }

        }

        private void verificaSeGrafoJaContemVertices(Grafo grafo, ref Vertice vertice1, ref Vertice vertice2, string[] dados)
        {
            for (int i = 0; i < grafo.vetorVertices.Length; i++)
            {
                if (grafo.vetorVertices[i] != null && grafo.vetorVertices[i].nomeVertice == dados[0])
                {
                    vertice1 = null;
                    vertice1 = grafo.vetorVertices[i];
                }
                else if (grafo.vetorVertices[i] != null && grafo.vetorVertices[i].nomeVertice == dados[1])
                {
                    vertice2 = null;
                    vertice2 = grafo.vetorVertices[i];
                }
            }
        }
    }
}
