using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class RespSolicitacao
    {
        public virtual int Id { get; set; }
        public virtual User user { get; set; }
        public virtual Solicitacao solicitacao { get; set; }
        public virtual string descricao { get; set; }
        public virtual situacaoSolicitacao situacao { get; set; }
        public virtual DateTime horario { get; set; }
        public virtual User solicitante { get; set; }
        public virtual setor setor { get; set; }

        public RespSolicitacao()
        {
            solicitacao = new Solicitacao();
        }

    }
}