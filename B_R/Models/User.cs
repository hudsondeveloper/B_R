using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string Role { get; set; }
        public virtual string Nome { get; set; }
        public virtual string Email { get; set; }
        public virtual string Cargo { get; set; }
        public virtual string Sexo { get; set; }
        public virtual string Matricula { get; set; }
        public virtual string senha { get; set; }
        public virtual string Login { get; set; }
        public virtual setor setor { get; set; }


        private readonly IList<LogArea> _logArea;
        public virtual IEnumerable<LogArea> LogArea { get { return _logArea; } }


        private readonly IList<LogEquipamento> _logEquipamento;
        public virtual IEnumerable<LogEquipamento> LogEquipamento { get { return _logEquipamento; } }


        public User()
        {
            _logEquipamento = new List<LogEquipamento>();
            _logArea = new List<LogArea>();
        }

    }
        public enum setor
        {
           MECANICA,
           ELETRICA,
           AUTOMACAO,
           CIVIL,
           SEGURANÇA,
           COMPRAS,
           PROJETOS,
           CIPA,
           OPERAÇÃO,
           TI,
           FATURAMENTO,
           ESCRITÓRIO,
           BALANÇA,
           LIMPEZA,
           CONTABILIDADE,
            RH,
            PINTURA,
            JARDINAGEM

        }
}