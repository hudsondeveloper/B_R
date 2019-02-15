using B_R.Models;
using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.MapsHibernate
{
    class RespSolicitacaoMap : ClassMap<RespSolicitacao>
    {
        public RespSolicitacaoMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            References(x => x.user).Not.LazyLoad();
            References(x => x.solicitacao).Not.LazyLoad();
            Map(x => x.descricao);
            Map(x => x.situacao).CustomType<situacaoSolicitacao>(); ;
            Map(x => x.horario);
            Map(x => x.setor);
            References(x => x.solicitante).Not.LazyLoad(); ;
            Table("RespSolicitacoes");
        }
    }
}