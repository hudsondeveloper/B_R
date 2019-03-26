using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace B_R.Models
{
    public class Solicitacao
    {
        public virtual int Id { get; set; }
        public virtual Area area { get; set; }
        public virtual Equipamento equipamento { get; set; }
        public virtual User user { get; set; }
        public virtual User solicitante { get; set; }
        public virtual prioridade prioridade { get; set; }
        public virtual modalidade modalidade { get; set; }
        public virtual situacaoSolicitacao situacao { get; set; }
        public virtual string descricao { get; set; }
        public virtual DateTime abertura { get; set; }
        public virtual DateTime fechamento { get; set; }
        public virtual setor setor { get; set; }

        private readonly IList<RespSolicitacao> _respSolicitacao;
        public virtual IEnumerable<RespSolicitacao> RespSolicitacao { get { return _respSolicitacao; } }

    public Solicitacao()
    {
            _respSolicitacao = new List<RespSolicitacao>();
    }

    }
    public enum prioridade
    {
        SIMPLES,     
        PROGRAMACAO,
        MODERADA,
        URGENTE,
        EMERGÊNCIA,
        MODIFICAÇÃO,
        MELHORIA
    }

    public enum modalidade
    {
    
        CORRETIVA,
        PREVENTIVA,
        PREDITIVA,
        SERVIÇOS_GERAIS,
        PLANO_DE_INSPEÇÃO
    }

    public enum situacaoSolicitacao
    {

        PENDENTE,
        FINALIZADA,
        CANCELADA,
        REABERTURA 

    }

}