﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using B_R.Models;
using FluentNHibernate.Mapping;

namespace B_R.MapsHibernate
{
     class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.Identity(); 
            Map(x => x.Nome);
            Map(x => x.Email);
            Map(x => x.Cargo);
            Map(x => x.Matricula);
            Map(x => x.Sexo);
            Map(x => x.Role);
            Map(x => x.senha);
            Map(x => x.Login);
            Map(x => x.setor).CustomType<setor>();
            HasMany(x => x.LogArea).Access.CamelCaseField(Prefix.Underscore).Inverse().ForeignKeyCascadeOnDelete().Not.LazyLoad();
            HasMany(x => x.LogEquipamento).Access.CamelCaseField(Prefix.Underscore).Inverse().ForeignKeyCascadeOnDelete().Not.LazyLoad();
            Table("Users");
        }
    }
}