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

            grafoNaoDirigido.getAGMPrim(grafoNaoDirigido.vetorVertices[0]);

            objParaLeitura = new StreamReader(@"grafoDirigido.txt");
            arquivoGrafo = new ArquivoGrafo();
            Grafo grafoDirigido = arquivoGrafo.setarGrafo(objParaLeitura);
            //teste
            Console.ReadKey();
        }
    }
}
