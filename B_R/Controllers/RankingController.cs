using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B_R.Authentication;
using B_R.Models;
using NHibernate;
using NHibernate.Linq;

namespace B_R.Controllers
{
    public class RankingController : Controller
    {
        // GET: Ranking
        public ActionResult Index()
        {
            return View();
        }

        // GET: Ranking
        public ActionResult Setor()
        {
            
            using (ISession session = NHibernateHelper.OpenSession())
            {
                int automacao = session.Query<Solicitacao>().Where(x=>x.setor == setor.AUTOMACAO).Count();
                int balanca = session.Query<Solicitacao>().Where(x => x.setor == setor.BALANÇA).Count();
                int cipa = session.Query<Solicitacao>().Where(x => x.setor == setor.CIPA).Count();
                int civil = session.Query<Solicitacao>().Where(x => x.setor == setor.CIVIL).Count();
                int compras = session.Query<Solicitacao>().Where(x => x.setor == setor.COMPRAS).Count();
                int contabilidade = session.Query<Solicitacao>().Where(x => x.setor == setor.CONTABILIDADE).Count();
                int eletrica = session.Query<Solicitacao>().Where(x => x.setor == setor.ELETRICA).Count();
                int escritorio = session.Query<Solicitacao>().Where(x => x.setor == setor.ESCRITÓRIO).Count();
                int faturamento = session.Query<Solicitacao>().Where(x => x.setor == setor.FATURAMENTO).Count();
                int jardinagem = session.Query<Solicitacao>().Where(x => x.setor == setor.JARDINAGEM).Count();
                int limpeza = session.Query<Solicitacao>().Where(x => x.setor == setor.LIMPEZA).Count();
                int mecanica = session.Query<Solicitacao>().Where(x => x.setor == setor.MECANICA).Count();
                int operacao = session.Query<Solicitacao>().Where(x => x.setor == setor.OPERAÇÃO).Count();
                int pintura = session.Query<Solicitacao>().Where(x => x.setor == setor.PINTURA).Count();
                int projetos = session.Query<Solicitacao>().Where(x => x.setor == setor.PROJETOS).Count();
                int rh = session.Query<Solicitacao>().Where(x => x.setor == setor.RH).Count();
                int seguranca = session.Query<Solicitacao>().Where(x => x.setor == setor.SEGURANÇA).Count();
                int ti = session.Query<Solicitacao>().Where(x => x.setor == setor.TI).Count();

                List<SetorChamado> setorchamado = new List<SetorChamado>(){
                    new SetorChamado(){setor = "AUTOMAÇÃO", chamado = automacao },
                    new SetorChamado(){setor = "BALANÇA", chamado = balanca },
                    new SetorChamado(){setor = "CIPA", chamado = cipa },
                    new SetorChamado(){setor = "CIVIL", chamado = civil },
                    new SetorChamado(){setor = "COMPRAS", chamado = compras },
                    new SetorChamado(){setor = "CONTABILIDADE", chamado = contabilidade },
                    new SetorChamado(){setor = "ELÉTRICA", chamado = eletrica },
                    new SetorChamado(){setor = "ESCRITÓRIO", chamado = escritorio },
                    new SetorChamado(){setor = "FATURAMENTO", chamado = faturamento },
                    new SetorChamado(){setor = "JARDINAGEM", chamado = jardinagem },
                    new SetorChamado(){setor = "LIMPEZA", chamado = limpeza },
                    new SetorChamado(){setor = "MECÂNICA", chamado = mecanica },
                    new SetorChamado(){setor = "OPERAÇÃO", chamado = operacao },
                    new SetorChamado(){setor = "PINTURA", chamado = pintura },
                    new SetorChamado(){setor = "PROJETOS", chamado = projetos },
                    new SetorChamado(){setor = "RH", chamado = rh },
                    new SetorChamado(){setor = "SEGURANÇA", chamado = seguranca },
                    new SetorChamado(){setor = "TI", chamado = ti },
                };

                session.Close();
                return View(setorchamado);
            }
        
          
        }


        // GET: Ranking
        public ActionResult Equipamento()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                List<EquipamentoChamado> equipamentos = new List<EquipamentoChamado>();
                var results = from p in session.Query<Solicitacao>()
                              group p.equipamento.descricao_equipamento by p.equipamento.Id into g
                              select new { PersonId = g.Key, solicitacoes = g.ToList() };

                foreach (var item in results)
                {
                    EquipamentoChamado equipamento = new EquipamentoChamado();
                    equipamento.equipamento = item.solicitacoes.FirstOrDefault();
                    equipamento.chamado = item.solicitacoes.Count();
                    equipamentos.Add(equipamento);
                };
                session.Close();
                return View(equipamentos);
            }
        }

        // GET: Ranking
        public ActionResult Usuario()
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                List<UsuarioChamado> usuarios = new List<UsuarioChamado>();
            var results = from p in session.Query<Solicitacao>()
                          group p.solicitante.Nome by p.solicitante.Id into g
                          select new { PersonId = g.Key, solicitacoes = g.ToList() };

                foreach(var item in results)
                {
                    UsuarioChamado user = new UsuarioChamado();
                     user.usuairo = item.solicitacoes.FirstOrDefault();
                    user.chamado = item.solicitacoes.Count();
                    usuarios.Add(user);
       };
                session.Close();
                return View(usuarios);
            }
        }

        //public ActionResult UsuarioSemanal()
        //{


        //    DateTime data = DateTime.Now.AddDays(-7);
        //    DateTime data1 = DateTime.Now.AddDays(7);
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        List<UsuarioChamado> usuarios = new List<UsuarioChamado>();
        //        var results = from p in session.Query<Solicitacao>().Where(x=>x.abertura < DateTime.Now.AddDays(-7) && x.abertura > DateTime.Now.AddDays(7))
        //                      group p.solicitante.Nome by p.solicitante.Id into g
        //                      select new { PersonId = g.Key, solicitacoes = g.ToList() };

        //        foreach (var item in results)
        //        {
        //            UsuarioChamado user = new UsuarioChamado();
        //            user.usuairo = item.solicitacoes.FirstOrDefault();
        //            user.chamado = item.solicitacoes.Count();
        //            usuarios.Add(user);
        //        };
        //        session.Close();
        //        return View(usuarios);
        //    }
        //}

        public class UsuarioChamado
        {
            public virtual string usuairo { get; set; }
            public virtual int chamado { get; set; }
        }


        public class EquipamentoChamado
        {
            public virtual string equipamento { get; set; }
            public virtual int chamado { get; set; }
        }


        public class SetorChamado
        {
            public virtual String setor { get; set; }
            public virtual int chamado { get; set; }
        }
    }
}
