using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Models
{
    public class ComentariosArea
    {
        public virtual int Id { get; set; }
        public virtual User user { get; set; }
        public virtual Area area { get; set; }
        public virtual string descricao { get; set; }
        public virtual DateTime horario { get; set; }

        public ComentariosArea()
        {
        area = new Area();
        }
    }
}