using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class ObservacaoSolicitacao
    {
        public virtual int Id { get; set; }
        public virtual string area { get; set; }
        public virtual string equipamento { get; set; }
        public virtual string solicitante { get; set; }
        public virtual string prioridade { get; set; }
        public virtual string modalidade { get; set; }
        public virtual string situacao { get; set; }
        public virtual string descricao { get; set; }
        public virtual string abertura { get; set; }
        public virtual DateTime fechamento { get; set; }
        public virtual string setor { get; set; }

        public virtual string descricao2 { get; set; }
        public virtual string horario { get; set; }
        public virtual string solicitante2 { get; set; }


    }
}