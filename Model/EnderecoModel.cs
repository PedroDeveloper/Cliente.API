using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.API.Model
{
    public class EnderecoModel
    {
        public int ID_end { get; set; }
        public int ID_cliente { get; set; }
        public string Nome_end { get; set; }
        public string  Tel_end { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

    }
}
