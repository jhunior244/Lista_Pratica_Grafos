using System;
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
            if (vertice != null)
            {
                vertice.visitado = true;
                verticesVisitados++;
            }
            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                for (int j = 0; j < vertice.listaAresta.Count; j++)
                {
                    if (vertice != null && vertice.listaAresta[j].verticeIncidente.visitado == false)
                    {
                        vertice.listaAresta[j].verticeIncidente.visitado = true;
                        verticesVisitados++;
                    }
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
                    List<Vertice> listaVerticesSemLigacao = findVerticesSemLigacao(this.vetorVertices[i]);
                    for (int j = 0; j < listaVerticesSemLigacao.Count - 1; j++)
                    {
                        string[] dados = new string[] { listaVerticesSemLigacao[0].nomeVertice, listaVerticesSemLigacao[j + 1].nomeVertice };
                        Vertice vertice1 = listaVerticesSemLigacao[0];
                        Vertice vertice2 = listaVerticesSemLigacao[j + 1];

                        Vertice.verificaSeGrafoJaContemVertices(grafoComplementar, ref vertice1, ref vertice2, dados);
                        if (!verticeJaLigado(vertice1.listaAresta, vertice2) && !verticeJaLigado(vertice2.listaAresta, vertice1))
                        {
                            aresta.ligaVerticeGrafoNaoDirigido(vertice1, vertice2, 1);

                        }
                        adicionaVertice(grafoComplementar, vertice1);
                        adicionaVertice(grafoComplementar, vertice2);
                    }
                }
            }
            return grafoComplementar;
        }
        //verifica se vertice ja está ligado para nao adicionar duas vezes no complementar
        private bool verticeJaLigado(List<Aresta> listaArestas, Vertice vertice)
        {
            bool verticeJaLigado = false;
            listaArestas.ForEach(aresta => { if (aresta.verticeIncidente.Equals(vertice)) { verticeJaLigado = true; } });
            return verticeJaLigado;
        }
        //encontra vertices no grafo que nao se conectam com o vertice de parametro
        private List<Vertice> findVerticesSemLigacao(Vertice vertice)
        {
            List<Vertice> listaVerticesSemLigacao = new List<Vertice>();

            List<Vertice> listaVerticesLigados = new List<Vertice>();

            Vertice verticeParaComplementar = new Vertice();
            verticeParaComplementar.nomeVertice = vertice.nomeVertice;
            listaVerticesSemLigacao.Add(verticeParaComplementar);

            vertice.listaAresta.ForEach(delegate (Aresta aresta)
            {
                listaVerticesLigados.Add(aresta.verticeIncidente);
            });

            for (int j = 0; j < this.vetorVertices.Length; j++)
            {
                if (!listaVerticesLigados.Contains(this.vetorVertices[j])
                    && !this.vetorVertices[j].Equals(vertice))
                {
                    verticeParaComplementar = new Vertice();
                    verticeParaComplementar.nomeVertice = this.vetorVertices[j].nomeVertice;
                    listaVerticesSemLigacao.Add(verticeParaComplementar);
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
                                    origem = i;
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
            imprimeResultadoPrimEKrukal(ordemAdicaoArvore, pais);
            return arvoreGeradoraMinima;
        }

        private void imprimeResultadoPrimEKrukal(List<string> ordemAdicaoArvore, Vertice[] pais)
        {
            Console.WriteLine();
            string stringPais = "";
            string filhos = "";
            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                filhos += this.vetorVertices[i].nomeVertice + "-";
                stringPais += pais[i].nomeVertice + "-";
            }

            Console.WriteLine(filhos);
            Console.WriteLine(stringPais);
            Console.WriteLine();

            foreach (var nomeVertice in ordemAdicaoArvore)
            {
                Console.Write(nomeVertice + " - ");
            }

            Console.WriteLine();
        }
        //encontra o indice no grafo do respectivo vertice
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

        public Grafo getAGMKruskal(Vertice verticeIncial)
        {
            Grafo arvoreGeradoraMinima = new Grafo(this.vetorVertices.Length);
            Vertice[] pais = new Vertice[this.vetorVertices.Length];
            Vertice[] arvore = new Vertice[this.vetorVertices.Length];
            List<string> ordemAdicaoArvore = new List<string>();

            bool primeiroVerticeEncontrado = true, fimLoop = false;
            int origem = 0, destino = 0, menorPeso = 0;

            for (int i = 0; i < this.vetorVertices.Length; i++)
            {
                arvore[i] = this.vetorVertices[i];
                pais[i] = null;
                if (this.vetorVertices[i].Equals(verticeIncial))//seta apenas o vertice inicial como pai dele mesmo
                {
                    pais[i] = verticeIncial;
                }
            }

            while (!fimLoop)
            {
                //percorre todos os vertices
                for (int i = 0; i < this.vetorVertices.Length; i++)
                {
                    for (int j = 0; j < this.vetorVertices[i].listaAresta.Count; j++)
                    {   //verifica se o vertice no grafo ainda nao está conectado a árvore, se for o caso, verifica se sua aresta
                        //tem o menor peso que todas ainda não adicionadas
                        if (arvore[i] != arvore[findIndiceVerticeNoGrafo(this.vetorVertices[i].listaAresta[j].verticeIncidente)])

                        {   //caso esse seja o primeiro vizinho nao visitado, seta ele como destino
                            if (primeiroVerticeEncontrado)
                            {
                                menorPeso = this.vetorVertices[i].listaAresta[j].peso;
                                origem = i;
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

                if (primeiroVerticeEncontrado)
                {
                    fimLoop = true;
                }
                //seta pais
                if (pais[origem] == null)
                {
                    pais[origem] = this.vetorVertices[destino];
                    primeiroVerticeEncontrado = true;
                }

                else
                {
                    pais[destino] = this.vetorVertices[origem];
                    primeiroVerticeEncontrado = true;
                }

                Aresta aresta = new Aresta();

                Vertice verticePai = new Vertice();
                Vertice verticeFilho = new Vertice();

                verticePai = this.vetorVertices[origem];
                verticeFilho = this.vetorVertices[destino];

                aresta.ligaVerticeGrafoNaoDirigido(verticePai, verticeFilho, menorPeso);
                menorPeso = int.MaxValue;

                //adiciona vertices a arvore geradora minima
                adicionaVertice(arvoreGeradoraMinima, verticePai);
                adicionaVertice(arvoreGeradoraMinima, verticeFilho);

                //adiciona nome vertice a lista ordenada por tempo de inserção na arvore
                if (!ordemAdicaoArvore.Contains(verticePai.nomeVertice))
                {
                    ordemAdicaoArvore.Add(verticePai.nomeVertice);
                }
                if (!ordemAdicaoArvore.Contains(verticeFilho.nomeVertice))
                {
                    ordemAdicaoArvore.Add(verticeFilho.nomeVertice);
                }

                for (int i = 0; i < this.vetorVertices.Length; i++)
                {
                    //coloca vertice origem como chefe do vertice destino
                    if (arvore[i] == arvore[destino])
                    {
                        arvore[i] = arvore[origem];
                    }
                }
            }
            imprimeResultadoPrimEKrukal(ordemAdicaoArvore, pais);
            return arvoreGeradoraMinima;
        }

        //na teoria o metodo funcionaria com a ajuda do metodo isConexo
        //seriam retirados todos vertices do grafo, um por vez, e verificaria se o grafo ainda permanecia conexo
        //no entanto, como o grafo é orientado a objeto ele acaba apagando as referencias e como nao consegui clonar o grafo para
        //manipula-lo, o metodo nao funcionou
        public int getCutVertices()
        {
            int totalCutVertices = 0;
            for (int i = 0; i < this.vetorVertices.Length; i++)
            {   //clona grafo
                List<Vertice> listaClonada = new List<Vertice>();
                foreach (var vertice in this.vetorVertices)
                {
                    listaClonada.Add(vertice);
                }

                Vertice verticeAExcluir = listaClonada[i];
                listaClonada.Remove(listaClonada[i]);//remove o vertice retirado da referencia de todas arestas que ligavam a ele
                excluiVertice(verticeAExcluir, listaClonada);
                if (!isConexo(listaClonada))//verifica se é conexo apos a retirada do vertice
                {
                    totalCutVertices++;
                }

            }

            return totalCutVertices;
        }

        private void excluiVertice(Vertice verticeAExcluir, List<Vertice> listaClonada)
        {
            for (int i = 0; i < listaClonada.Count; i++)
            {
                for (int j = 0; j < listaClonada[i].listaAresta.Count; j++)
                {
                    if (listaClonada[i].listaAresta[j].verticeIncidente.Equals(verticeAExcluir))
                    {
                        listaClonada[i].listaAresta.Remove(listaClonada[i].listaAresta[j]);
                    }
                }
            }
        }

        //fiz uma sobrecarga do isConexo para usar com minha copia do grafo, pois com vetor nao é possivel
        //adicionar e remover vertices e ao mesmo tempo diminuir o tamanho do vetor e isso nao deixaria verificar os cut vertices
        public bool isConexo(List<Vertice> grafo)
        {
            int verticesVisitados = 0;
            Vertice vertice = new Vertice();
            vertice = grafo[0];
            if (vertice != null)
            {
                vertice.visitado = true;
                verticesVisitados++;
            }
            for (int i = 0; i < grafo.Count; i++)
            {
                for (int j = 0; j < vertice.listaAresta.Count; j++)
                {
                    if (vertice != null && vertice.listaAresta[j].verticeIncidente.visitado == false)
                    {
                        vertice.listaAresta[j].verticeIncidente.visitado = true;
                        verticesVisitados++;
                    }
                }
                vertice = findVerticeComAdjacenteNaoVisitado(vertice);
            }
            return verticesVisitados == grafo.Count ? true : false;
        }

        //Verificar todos os vértices do grafo, se todos eles tiverem pelo menos 1 grau de entrada e saída, retorna true.
        public bool hasCiclo()
        {
            Vertice obj;
            bool ciclo = false;

            for (int i = 0; i < vetorVertices.Length; i++)
            {
                obj = vetorVertices[i];

                if (GetGrauEntrada(obj) >= 1 && GetGrauSaida(obj) >= 1)
                {
                    ciclo = true;
                }
                else
                {
                    ciclo = false;
                    break;
                }
            }

            return ciclo;

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
                    while (v1.listaAresta[i] != null)
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
    }
}
