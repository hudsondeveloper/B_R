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
                    new SetorChamado(){setor = setor.AUTOMACAO, chamado = automacao },
                    new SetorChamado(){setor = setor.BALANÇA, chamado = balanca },
                    new SetorChamado(){setor = setor.CIPA, chamado = cipa },
                    new SetorChamado(){setor = setor.CIVIL, chamado = civil },
                    new SetorChamado(){setor = setor.COMPRAS, chamado = compras },
                    new SetorChamado(){setor = setor.CONTABILIDADE, chamado = contabilidade },
                    new SetorChamado(){setor = setor.ELETRICA, chamado = eletrica },
                    new SetorChamado(){setor = setor.ESCRITÓRIO, chamado = escritorio },
                    new SetorChamado(){setor = setor.FATURAMENTO, chamado = faturamento },
                    new SetorChamado(){setor = setor.JARDINAGEM, chamado = jardinagem },
                    new SetorChamado(){setor = setor.LIMPEZA, chamado = limpeza },
                    new SetorChamado(){setor = setor.MECANICA, chamado = mecanica },
                    new SetorChamado(){setor = setor.OPERAÇÃO, chamado = operacao },
                    new SetorChamado(){setor = setor.PINTURA, chamado = pintura },
                    new SetorChamado(){setor = setor.PROJETOS, chamado = projetos },
                    new SetorChamado(){setor = setor.RH, chamado = rh },
                    new SetorChamado(){setor = setor.SEGURANÇA, chamado = seguranca },
                    new SetorChamado(){setor = setor.TI, chamado = ti },
                };

                session.Close();
                return View(setorchamado);
            }
        
          
        }

        public class SetorChamado
        {
            public virtual setor setor { get; set; }
            public virtual int chamado { get; set; }
        }

        // GET: Ranking
        public ActionResult Equipamento()
        {
            return View();
        }

        // GET: Ranking
        public ActionResult Usuario()
        {
            return View();
        }

    }
}
