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
    }
}
