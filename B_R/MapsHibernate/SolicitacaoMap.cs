using B_R.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.MapsHibernate
{
    public class SolicitacaoMap : ClassMap<Solicitacao>
    {

    
            public SolicitacaoMap()
            {
                Id(x => x.Id).GeneratedBy.Increment();
                Map(x => x.descricao);
                Map(x => x.abertura);
                Map(x => x.fechamento);
                Map(x => x.setor).CustomType<setor>();
                Map(x => x.prioridade).CustomType<prioridade>();
                Map(x => x.modalidade).CustomType<modalidade>();
                Map(x => x.situacao).CustomType<situacaoSolicitacao>();
                References(x => x.area).Not.LazyLoad();
                References(x => x.equipamento).Not.LazyLoad();
                References(x => x.user).Not.LazyLoad();
                References(x => x.solicitante).Not.LazyLoad();
                HasMany(x => x.RespSolicitacao).Access.CamelCaseField(Prefix.Underscore).Inverse().ForeignKeyCascadeOnDelete().Not.LazyLoad();
                Table("Solicitacoes");
            }
        
    }
}