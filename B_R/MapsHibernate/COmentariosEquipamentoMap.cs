using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B_R.Models;
using FluentNHibernate.Mapping;

namespace B_R.MapsHibernate
{
    class ComentariosEquipamentoMap : ClassMap<ComentariosEquipamento>
    {
        public ComentariosEquipamentoMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.descricao);
            References(x => x.equipamento).Not.LazyLoad();
            References(x => x.user).Not.LazyLoad();
            Map(x => x.horario);
            Table("ComentariosEquipamento");
        }
    }
}