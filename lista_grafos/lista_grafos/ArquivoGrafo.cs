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
            Vertice vertice = new Vertice();
            Grafo grafo = new Grafo(tamanhoGrafo);
            while (!objetoLeitura.EndOfStream)
            {
                string linha = objetoLeitura.ReadLine();
                string[] dados = linha.Split(';');
                if (dados.Length == 3)
                {
                    vertice.setaVerticeGrafoNaoDirigido(grafo, dados);
                }
                else if(dados.Length == 4)
                {
                    vertice.setaVerticeGrafoDirigido(grafo, dados);
                }
            }
            return grafo;
        }
    }
}
