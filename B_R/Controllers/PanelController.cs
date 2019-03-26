using B_R.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;
using B_R.Models;
using System.Net;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace B_R.Controllers
{
    [MinhaAutenticacao]
    public class PanelController : Controller
    {
        // GET: Panel
        [MinhaAutenticacao]
        public ActionResult Index(string setor, string situacao, DateTime? dateIni, DateTime? dateFim,string data)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                if(data != null)
                {
                    data = null;
                    List<Solicitacao> solicitacoesData = new List<Solicitacao>();

                    if (dateIni != null && dateFim != null)
                    {
                        solicitacoesData = session.Query<Solicitacao>().Where(x => x.abertura >= dateIni && x.abertura <= dateFim).ToList();
                    }
                    else if (dateIni == null && dateFim != null)
                    {
                        solicitacoesData = session.Query<Solicitacao>().Where(x => x.abertura <= dateFim).ToList();
                    }
                    else if (dateIni != null && dateFim == null)
                    {
                        solicitacoesData = session.Query<Solicitacao>().Where(x => x.abertura >= dateIni).ToList();
                    }
                    else
                    {
                        solicitacoesData = session.Query<Solicitacao>().ToList();
                    }
                    session.Close();
                    return View(solicitacoesData);
                }

                List<Solicitacao> solicitacoes = new List<Solicitacao>();

                if (setor != null  && situacao != null && setor != "" && situacao != "")
                {
                    solicitacoes = session.Query<Solicitacao>().Where(x => x.setor == Utils.verificarSetor(setor) && x.situacao == Utils.verificarSituacao(situacao)).ToList();
                }
                else if (setor == "" && situacao != "")
                {
                    solicitacoes = session.Query<Solicitacao>().Where(x => x.situacao == Utils.verificarSituacao(situacao)).ToList();
                }
                else if (setor != "" && situacao == "")
                {
                    solicitacoes = session.Query<Solicitacao>().Where(x => x.setor == Utils.verificarSetor(setor)).ToList();
                }
                else
                {
                    solicitacoes = session.Query<Solicitacao>().ToList();
                }
                session.Close();
                return View(solicitacoes);
            }
        }


        public ActionResult PrimeiraPagina()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                List<Solicitacao> solicitacoes = new List<Solicitacao>();
                solicitacoes = session.Query<Solicitacao>().ToList();
                //solicitacoes = session.Query<Solicitacao>().Where(x=>x.setor == HomeController.user.setor && x.situacao == situacaoSolicitacao.PENDENTE).ToList();
                
                session.Close();
                return View(solicitacoes);
            }
        }

        public ActionResult Pesquisar(string setor,string situacao)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                List<Solicitacao> solicitacoes = new List<Solicitacao>();

                if (setor != "" && situacao != "")
                {
                    solicitacoes = session.Query<Solicitacao>().Where(x=>x.setor.ToString() == setor && x.situacao.ToString() == situacao).ToList();
                }else if(setor == "" && situacao != "")
                {
                solicitacoes = session.Query<Solicitacao>().Where(x => x.situacao.ToString() == situacao).ToList();
                }
                else if(setor != "" && situacao == "")
                {
                   solicitacoes = session.Query<Solicitacao>().Where(x => x.setor.ToString() == setor).ToList();
                }
                else
                {
                    solicitacoes = session.Query<Solicitacao>().ToList();
                }
                session.Close();
                return View(solicitacoes);
            }
        }

        // GET: Panel
        [MinhaAutenticacao]
        public ActionResult MinhasSolicitacoes()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var solicitacoes = session.Query<Solicitacao>().Where(x => x.user.Id == HomeController.user.Id || x.solicitante.Id == HomeController.user.Id).ToList();
                session.Close();
                return View(solicitacoes);
            }
        }

        // GET: Panel
        [MinhaAutenticacao]
        public ActionResult TodasSolicitacoes()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var solicitacoes = session.Query<Solicitacao>().ToList();
                session.Close();
                return View(solicitacoes);
            }
        }


        // GET: Panel/Details/5
        [MinhaAutenticacao]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Panel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Panel/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Panel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Panel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Panel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Panel/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
