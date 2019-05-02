using System;
using System.IO;

namespace lista_grafos
{
    class Program
    {
        static void Main(string[] args)
        {
            StreamReader objParaLeitura = new StreamReader(@"grafoNaoDirigido.txt");
            ArquivoGrafo arquivoGrafo = new ArquivoGrafo();
            Grafo grafoNaoDirigido = arquivoGrafo.setarGrafo(objParaLeitura);

            grafoNaoDirigido.isAdjacente(grafoNaoDirigido.vetorVertices[0], grafoNaoDirigido.vetorVertices[1]);

            objParaLeitura = new StreamReader(@"grafoDirigido.txt");
            arquivoGrafo = new ArquivoGrafo();
            Grafo grafoDirigido = arquivoGrafo.setarGrafo(objParaLeitura);

            Console.ReadKey();
        }
    }
}
