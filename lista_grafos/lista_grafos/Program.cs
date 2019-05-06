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

            //bool resultado = grafoNaoDirigido.isConexo();
            //int totalCutVertices = grafoNaoDirigido.getCutVertices();
            grafoNaoDirigido.getAGMPrim(grafoNaoDirigido.vetorVertices[0]);

            grafoNaoDirigido.getAGMKruskal(grafoNaoDirigido.vetorVertices[0]);

            objParaLeitura = new StreamReader(@"testeComplementar.txt");
            Grafo grafoComplementar = arquivoGrafo.setarGrafo(objParaLeitura);
            grafoComplementar = grafoComplementar.getComplementar();

            objParaLeitura = new StreamReader(@"grafoDirigido.txt");
            arquivoGrafo = new ArquivoGrafo();
            Grafo grafoDirigido = arquivoGrafo.setarGrafo(objParaLeitura);
            
            Console.ReadKey();
        }
    }
}
