using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B_R.Models;
using FluentNHibernate.Mapping;

namespace B_R.MapsHibernate
{
    class ComentariosAreaMap : ClassMap<ComentariosArea>
    {
        public ComentariosAreaMap()
        {
            Id(x => x.Id).GeneratedBy.Increment();
            Map(x => x.descricao);
            References(x => x.area).Not.LazyLoad(); 
            References(x => x.user).Not.LazyLoad();
            Map(x => x.horario);
            Table("ComentariosArea");
        }
    }
}