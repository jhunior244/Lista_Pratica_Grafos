using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lista_grafos
{
    class ArquivoGrafo
    {
        public Grafo setarGrafo(StreamReader objetoLeitura)
        {
            int tamanhoGrafo = int.Parse(objetoLeitura.ReadLine());

            Grafo grafo = new Grafo(tamanhoGrafo);
            while (!objetoLeitura.EndOfStream)
            {
                string linha = objetoLeitura.ReadLine();
                string[] dados = linha.Split(';');
                if (dados.Length == 3)
                {
                    setaVerticeGrafoNaoDirigido(grafo, dados);
                }
                else if(dados.Length == 4)
                {
                    setaVerticeGrafoDirigido(grafo, dados);
                }
            }
            return grafo;
        }

        private void setaVerticeGrafoNaoDirigido(Grafo grafo, string[] dados)
        {
            Vertice vertice1 = new Vertice();
            Vertice vertice2 = new Vertice();
            Aresta aresta = new Aresta();

            vertice1.nomeVertice = dados[0];
            vertice2.nomeVertice = dados[1];

            for(int i =0; i < grafo.vetorVertices.Length; i++)
            {
                if(grafo.vetorVertices[i] != null && grafo.vetorVertices[i].nomeVertice == dados[0])
                {
                    vertice1 = null;
                    vertice1 = grafo.vetorVertices[i];
                }
                else if(grafo.vetorVertices[i] != null && grafo.vetorVertices[i].nomeVertice == dados[1])
                {
                    vertice2 = null;
                    vertice2 = grafo.vetorVertices[i];
                }
            }

            aresta.ligaVerticeGrafoNaoDirigido(vertice1, vertice2, int.Parse(dados[2]));

            grafo.adicionaVerticeGrafoNaoDirigido(grafo, vertice1);
            grafo.adicionaVerticeGrafoNaoDirigido(grafo, vertice2);
        }

        private void setaVerticeGrafoDirigido(Grafo grafo, string[] dados)
        {
            Vertice vertice1 = new Vertice();
            Vertice vertice2 = new Vertice();
            Aresta aresta = new Aresta();

            vertice1.nomeVertice = dados[0];
            vertice2.nomeVertice = dados[1];

            int direcaoAresta = int.Parse(dados[3]);

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

            if (direcaoAresta == 1)
            {
                aresta.ligaVerticeGrafoDirigido(vertice1, vertice2, int.Parse(dados[2]));
                grafo.adicionaVerticeGrafoNaoDirigido(grafo, vertice1);
                return;
            }
            else if( direcaoAresta == -1)
            {
                aresta.ligaVerticeGrafoDirigido(vertice2, vertice1, int.Parse(dados[2]));
                grafo.adicionaVerticeGrafoNaoDirigido(grafo, vertice2);
                return;
            }
            
        }

       
    }
}
