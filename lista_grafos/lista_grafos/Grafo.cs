﻿using System;
using System.Collections.Generic;
using System.Text;

namespace lista_grafos
{
    class Grafo
    {
        public Vertice[] vetorVertices;
        public Vertice obj;

        public Grafo(int tamanhoGrafo)
        {
            this.vetorVertices = new Vertice[tamanhoGrafo];
        }
        //lembrar de tirar da classe arquivoGrafo
        public void adicionaVertice(Grafo grafo, Vertice vertice)
        {
            for (int i = 0; i < grafo.vetorVertices.Length; i++)
            {
                if (grafo.vetorVertices[i] == null)
                {
                    grafo.vetorVertices[i] = vertice;
                    return;
                }
                else if (grafo.vetorVertices[i].nomeVertice == vertice.nomeVertice)
                {
                    return;
                }
            }
        }

        public bool hasCiclo()
        {


            return true;
        }

        //Incremento todas as arestas do v1, após isso subtraio com as arestas de entrada.
        public int GetGrauSaida(Vertice v1)
        {
            int grau_saida = 0;
            int j = 0;

            if (isIsolado(v1) == true)
            {
                grau_saida = 0;
            }
            else if (isIsolado(v1) == false)
            {
                for (int i = 0; i < v1.listaAresta.Count; i++)
                {
                    while(v1.listaAresta[i] != null)
                    {
                        j++;
                    }
                }

                grau_saida = j++ - GetGrauEntrada(v1);
            }

            return grau_saida;
        }

        //O primeiro passo é verificar se o vertice como parâmentro ja foi visitado, se for
        //percorremos a sua lista de aresta e vamos contar todos os vértices incidente dele.
        public int GetGrauEntrada(Vertice v1)
        {
            int grau_entrada = 0;

            if (v1.visitado == false)
            {
                grau_entrada = 0;
            }
            else if (v1.visitado == true)
            {
                for (int i = 0; i < v1.listaAresta.Count; i++)
                {
                    while (v1.listaAresta[i].verticeIncidente != null)
                    {
                        grau_entrada++;
                    }
                }
            }

            return grau_entrada;
        }

        //procura o vertice2 na lista de arestas do vertice1, se alguma delas ligar a ele 
        //o vertice é adjacente
        public bool isAdjacente(Vertice vertice1, Vertice vertice2)
        {
            for (int i = 0; i < vertice1.listaAresta.Count; i++)
            {
                if (vertice1.listaAresta[i].verticeIncidente == vertice2)
                {
                    return true;
                }
            }
            return false;
        }

        public int getGrau(Vertice vertice)
        {
            return vertice.listaAresta.Count;
        }

        public bool isIsolado(Vertice vertice)
        {
            return vertice.listaAresta.Count == 0 ? true : false;
        }

        public bool isPendente(Vertice vertice)
        {

            return vertice.listaAresta.Count == 1 ? true : false;
        }

        //verifica o grau do vertice com o proximo, caso sejam todos iguais retorna verdadeiro
        public bool isRegular(Grafo grafo)
        {
            bool regular = false;

            for (int i = 0; i < grafo.vetorVertices.Length - 1; i++)
            {
                if (grafo.vetorVertices[i].listaAresta.Count == grafo.vetorVertices[i + 1].listaAresta.Count)
                {
                    regular = true;
                }
                else
                {
                    regular = false;
                    break;
                }
            }
            return regular;
        }

        public bool isNulo()
        {
            return this == null ? true : false;
        }


        public bool isCompleto()
        {
            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                Vertice vertice = this.vetorVertices[i];

                //grafo com um vertice é completo
                if (this.vetorVertices.Length == 1)
                {
                    return true;
                }

                for (int j = 0; j < vertice.listaAresta.Count; j++)
                {
                    //se o numero de arestas do vertice for o total de vertices do
                    //grafo menos um significa que ele se conecta a todos
                    if (vertice.listaAresta.Count == this.vetorVertices.Length - 1)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        //é feita uma busca em largura, visitando todos vertices adjacentes de um determinado vertice
        //depois verifica-se se algum dos adjacentes tem um vertice adjacente a visitar e visita-se todos adjacentes dele
        //ao fim a variavel verticesVisitados é comparada com o total de vertices do grafo
        //caso seu tamanho for igual o numero de vertices do grafo ele é conexo
        public bool isConexo()
        {
            int verticesVisitados = 0;
            Vertice vertice = new Vertice();
            vertice = this.vetorVertices[0];
            vertice.visitado = true;
            verticesVisitados++;
            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                for (int j = 0; j < vertice.listaAresta.Count; j++)
                {
                    vertice.listaAresta[j].verticeIncidente.visitado = true;
                    verticesVisitados++;
                }
                vertice = findVerticeComAdjacenteNaoVisitado(vertice);
            }

            return verticesVisitados == this.vetorVertices.Length ? true : false;
        }

        private Vertice findVerticeComAdjacenteNaoVisitado(Vertice vertice)
        {
            for (int i = 0; i < vertice.listaAresta.Count; i++)
            {
                for (int j = 0; j < vertice.listaAresta[i].verticeIncidente.listaAresta.Count; j++)
                {
                    if (vertice.listaAresta[i].verticeIncidente.listaAresta[j].verticeIncidente.visitado == false)
                    {
                        return vertice.listaAresta[i].verticeIncidente;
                    }
                }
            }
            return vertice;
        }

        //verifice se todos vertices possuem grau par, caso possuam, o grafo é euleriano
        public bool isEuleriano()
        {
            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                if (this.vetorVertices[i].listaAresta.Count % 2 == 1)
                {
                    return false;
                }
            }
            return true;
        }

        //verifica se o grafo possui apenas dois vertices de grau impar, caso sim, significa que ligando-os
        //o grafo vira euleriano, sendo assim ele é unicursal
        public bool isUnicursal()
        {
            int verticesGrauImpar = 0;

            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                if (this.vetorVertices[i].listaAresta.Count % 2 == 1)
                {
                    verticesGrauImpar++;
                }
            }

            return verticesGrauImpar == 2 ? true : false;
        }

        //percorre N -1 vertices do grafo, caso sua lista de arestas tiver tamanho menor que o total de vertices do grafo -1
        //procura-se os vertices sem ligação liga-os um ao outro e os adiciona no grafo complementar
        public Grafo getComplementar()
        {
            Grafo grafoComplementar = new Grafo(this.vetorVertices.Length);
            Aresta aresta = new Aresta();
            for (int i = 0; i < this.vetorVertices.Length - 1; i++)
            {
                if (this.vetorVertices[i].listaAresta.Count < this.vetorVertices.Length - 1)
                {
                    List<Vertice> listaVerticesSemLigacao = findVerticesSemLigacao(this.vetorVertices[i].listaAresta);
                    for (int j = 0; j < listaVerticesSemLigacao.Count; j++)
                    {
                        aresta.ligaVerticeGrafoNaoDirigido(vetorVertices[i], listaVerticesSemLigacao[j], 1);
                        adicionaVertice(grafoComplementar, vetorVertices[i]);
                        adicionaVertice(grafoComplementar, listaVerticesSemLigacao[j]);
                    }
                }
            }
            return grafoComplementar;
        }

        private List<Vertice> findVerticesSemLigacao(List<Aresta> listaArestas)
        {
            List<Vertice> listaVerticesSemLigacao = new List<Vertice>();

            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                for (int j = 0; j < this.vetorVertices.Length; j++)
                {
                    if (listaArestas[i].verticeIncidente != this.vetorVertices[j]
                        && !listaVerticesSemLigacao.Contains(this.vetorVertices[j]))
                    {
                        listaVerticesSemLigacao.Add(this.vetorVertices[j]);
                    }
                }
            }
            return listaVerticesSemLigacao;
        }

        public Grafo getAGMPrim(Vertice verticeInicial)
        {
            Grafo arvoreGeradoraMinima = new Grafo(this.vetorVertices.Length);
            Vertice[] pais = new Vertice[this.vetorVertices.Length];
            List<string> ordemAdicaoArvore = new List<string>();
            ordemAdicaoArvore.Add(verticeInicial.nomeVertice);

            bool primeiroVerticeEncontrado = true, fimLoop = false;
            int origem = 0, destino = 0, menorPeso = 0;

            for (int i = 0; i < this.vetorVertices.Length; i++)
            {   //coloca o vertice inicial como pai dele mesmo
                if (this.vetorVertices[i] == verticeInicial)
                {
                    pais[i] = verticeInicial;
                    this.vetorVertices[i].visitado = true;
                }
                else
                {
                    pais[i] = null;
                }
            }
            while (!fimLoop)
            {
                //percorre todos os vertices
                for (int i = 0; i < this.vetorVertices.Length; i++)
                {
                    if (pais[i] != null)//acha vertice que ja possui pai
                    {   //percorre todos vizinhos do vertice que ja tem pai
                        for (int j = 0; j < this.vetorVertices[i].listaAresta.Count; j++)
                        {   //procura vertices nao visitados para indicar seus pais
                            if (this.vetorVertices[i].listaAresta[j].verticeIncidente.visitado == false)
                            {   //caso esse seja o primeiro vizinho nao visitado, seta ele como destino
                                if (primeiroVerticeEncontrado)
                                {
                                    menorPeso = this.vetorVertices[i].listaAresta[j].peso;
                                    origem = i;////////////////parametro vertice indo errado para o find, deve usar o vertice original do grafo
                                    destino = findIndiceVerticeNoGrafo(this.vetorVertices[i].listaAresta[j].verticeIncidente);
                                    primeiroVerticeEncontrado = false;
                                }
                                else
                                {   //se este nao for mais o primeiro vertice encontrado verifica se o caminho para ele é menor
                                    if (menorPeso > this.vetorVertices[i].listaAresta[j].peso)
                                    {
                                        menorPeso = this.vetorVertices[i].listaAresta[j].peso;
                                        origem = i;
                                        destino = findIndiceVerticeNoGrafo(this.vetorVertices[i].listaAresta[j].verticeIncidente);
                                    }
                                }
                            }
                        }
                    }
                }

                if (primeiroVerticeEncontrado)
                {
                    fimLoop = true;
                }
                else
                {
                    Aresta aresta = new Aresta();
                    pais[destino] = this.vetorVertices[origem];//adiciona pai na posicao do filho correspondente do filho no grafo

                    Vertice verticePai = new Vertice();
                    Vertice verticeFilho = new Vertice();

                    verticePai.nomeVertice = this.vetorVertices[origem].nomeVertice;
                    verticeFilho.nomeVertice = this.vetorVertices[destino].nomeVertice;
                    this.vetorVertices[destino].visitado = true;

                    aresta.ligaVerticeGrafoNaoDirigido(verticePai, verticeFilho, menorPeso);//liga vertice pai a vertice filho

                    //adiciona vertices a arvore geradora minima
                    adicionaVertice(arvoreGeradoraMinima, verticePai);
                    adicionaVertice(arvoreGeradoraMinima, verticeFilho);

                    //adiciona nome vertice a lista ordenada por tempo de inserção na arvore
                    ordemAdicaoArvore.Add(verticeFilho.nomeVertice);
                    menorPeso = int.MaxValue;
                    primeiroVerticeEncontrado = true;
                }
            }
            imprimirOrdemIsercaoArvore(ordemAdicaoArvore);
            return arvoreGeradoraMinima;
        }

        private void imprimirOrdemIsercaoArvore(List<string> ordemAdicaoArvore)
        {
            foreach (var nomeVertice in ordemAdicaoArvore)
            {
                Console.WriteLine(nomeVertice);
            }
        }

        private int findIndiceVerticeNoGrafo(Vertice vertice)
        {
            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                if (vetorVertices[i].Equals(vertice))
                {
                    return i;
                }
            }
            return 0;
        }

    }
}
