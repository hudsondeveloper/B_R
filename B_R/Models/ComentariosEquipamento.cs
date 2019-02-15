﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class ComentariosEquipamento
    {
        public virtual int Id { get; set; }
        public virtual User user { get; set; }
        public virtual Equipamento equipamento { get; set; }
        public virtual string descricao { get; set; }
        public virtual DateTime horario { get; set; }

        public ComentariosEquipamento()
        {
            equipamento = new Equipamento();
        }
    }
}