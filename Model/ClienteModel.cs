using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cliente.API.Model
{
    public class ClienteModel
    {

        public int  ID { get; set; }
        public string Cnpj { get; set; }
        public string Cpf{ get; set; }
        public string Nome { get; set; }
        public string DtNas { get; set; }
        public string RazaoSoci { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }

    }
}
