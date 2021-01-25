using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sistema_de_Controle_de_Viagens
{
    class Info
    {
        public string prefixo;
        public string trem;
        public string maquinista;
        public DateTime data_viagem;
        public string mensagem;
        public int sequencia;

        public Info()
        {
            prefixo = "???";
            trem = "";
            maquinista = "";
            mensagem = "";
            sequencia = 0;
        }

        public Info(int sequencia)
        {
            prefixo = "???";
            trem = "";
            maquinista = "";
            mensagem = "";
            this.sequencia = sequencia;
        }
    }
}
