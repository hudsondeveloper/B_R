using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B_R.Authentication;
using B_R.Models;
using NHibernate;
using NHibernate.Linq;
using ISession = NHibernate.ISession;

namespace B_R.Controllers
{
    [MinhaAutenticacao]
    public class SolicitacaoController : Controller
    {
        // GET: Solicitacao
        public ActionResult Index()
        {
          return  RedirectToAction("Index", "Panel");
        }
    
        public ActionResult verificarArea(int ?area)
        {
                if (area == null)
                {
                    return RedirectToAction("Cadastrar", "Solicitacao");
                }
      
            using (ISession session = NHibernateHelper.OpenSession())
            {

                List<Equipamento> equipamentos = new List<Equipamento>();
          
                ViewBag.area = new SelectList(session.Query<Area>().Where(x=>x.Id == area).ToList(), "Id", "Nome", area);

            //    var Equipamento = session.Query<Equipamento>().Where(x => x.area.Id == area).ToList().
            //  Select(e => new
            //  {
            //      e.Id,
            //        e.descricao_equipamento 

            //  }).ToList();
            //    return Equipamento;
            //}

            foreach (Equipamento equipamento in session.Query<Equipamento>().Where(x => x.area.Id == area).ToList())
                {
                    Equipamento Equipamento = new Equipamento
                {
                    Id = equipamento.Id,
                    descricao_equipamento = equipamento.tag +" - "+equipamento.descricao_equipamento

                };
                    equipamentos.Add(Equipamento);
                }

                ViewBag.equipamento = new SelectList(equipamentos.ToList(), "Id", "descricao_equipamento");

                ViewBag.user = new SelectList(session.Query<User>().ToList(), "Id", "Nome");
                ViewBag.id = area;
                session.Close();
                return View();
            }
        }

        //public ActionResult verificarArea()
        //{

        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        ViewBag.area = Session["area"];
        //        ViewBag.equipamento = Session["equipamento"];
        //        ViewBag.user = Session["user"];
        //        ViewBag.id = Session["id"];
        //        session.Close();
        //        return View();
        //    }
        //}

        [HttpGet]
        public ActionResult CadastrarSolicitacao(int? id) { 
            if( id!=null)
            {
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    var resultado = session.Query<Equipamento>().Where(x=>x.area.Id==id).ToList();

                    List<Object> equipamentos = new List<object>();
                 foreach(var equip in resultado)
                    {
                        equipamentos.Add(new { id = equip.Id, nome = equip.tag +" - "+ equip.descricao_equipamento});

                    }         
                    session.Close();
                  
                return Json(equipamentos, JsonRequestBehavior.AllowGet);
                }

            }
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ViewBag.area = session.Query<Area>().ToList();
                session.Close();
                return View();
            }
        }

        [HttpPost]
        public ActionResult CadastrarSolicitacao(string setor,string prioridade,string modalidade,int? area,int? equipamento,string descricao)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        Solicitacao solicitacao = new Solicitacao();

                        solicitacao.equipamento = session.Query<Equipamento>().Where(x => x.area.Id == area && x.Id == equipamento).FirstOrDefault();
                        solicitacao.area = session.Query<Area>().Where(x => x.Id == area).FirstOrDefault();
                        solicitacao.setor =  Utils.verificarSetor(setor);
                        solicitacao.solicitante = HomeController.user;
                        solicitacao.abertura = DateTime.Now;
                        solicitacao.situacao = situacaoSolicitacao.PENDENTE;
                        solicitacao.modalidade = Utils.verificarModalidade(modalidade);
                        solicitacao.prioridade = Utils.verificarPrioridade(prioridade);
                        solicitacao.descricao = descricao;
                        session.Save(solicitacao);

                        LogEquipamento log = new LogEquipamento();
                        log.acao = "Abertura de Solicitação";
                        log.equipamento = session.Query<Equipamento>().Where(x => x.area.Id == area && x.Id == equipamento).FirstOrDefault();
                        log.anterior = "Estado: " + solicitacao.situacao + " descrição: " + solicitacao.descricao;
                        log.horario = DateTime.Now;
                        log.user = HomeController.user;
                        session.Save(log);

                        RespSolicitacao respSolicitacao = new RespSolicitacao();
                        respSolicitacao.descricao = descricao;
                        respSolicitacao.situacao = solicitacao.situacao;
                        respSolicitacao.horario = DateTime.Now;
                        respSolicitacao.solicitacao = session.Query<Solicitacao>().Where(x => x.abertura == solicitacao.abertura).FirstOrDefault();
                        respSolicitacao.solicitante = HomeController.user;
                        respSolicitacao.setor = solicitacao.setor;
                        session.Save(respSolicitacao);

                        ViewBag.area = session.Query<Area>().ToList();
                        transacao.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                            session.Close();
                        }
                        throw new Exception("Erro ao inserir Solicitação : " + ex.Message);
                    }
                }
                //  return RedirectToAction("Index", "Home");
                return View();
            }

        }

        

        [HttpGet]
        public ActionResult Cadastrar(int? verificarArea, int? id)
        {

            using (ISession session = NHibernateHelper.OpenSession())
            {
                ViewBag.area = new SelectList(session.Query<Area>().ToList(), "Id", "Nome");
                session.Close();
                return View();
            }
        }


        //[HttpPost]
        //public ActionResult Cadastrar(Solicitacao solicitiacao,int area, int equipamento)
        //{
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        using (ITransaction transacao = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                solicitiacao.equipamento = session.Query<Equipamento>().Where(x => x.area.Id == area && x.Id == equipamento).FirstOrDefault();
        //                solicitiacao.area = session.Query<Area>().Where(x => x.Id == area ).FirstOrDefault();
        //                solicitiacao.setor = solicitiacao.setor;
        //                solicitiacao.solicitante = HomeController.user;
        //                solicitiacao.abertura = DateTime.Now;
        //                session.Save(solicitiacao);

        //                LogEquipamento log = new LogEquipamento();
        //                log.acao = "Abertura de Solicitação";
        //                log.equipamento = session.Query<Equipamento>().Where(x => x.area.Id == area && x.Id == equipamento).FirstOrDefault();
        //                log.anterior = "Estado: " + solicitiacao.situacao + " descrição: " + solicitiacao.descricao ;
        //                log.horario = DateTime.Now;
        //                log.user = HomeController.user;
        //                session.Save(log);

        //                RespSolicitacao respSolicitacao = new RespSolicitacao();
        //                respSolicitacao.descricao = "Abertura de Solicitação";
        //                respSolicitacao.situacao = solicitiacao.situacao;
        //                respSolicitacao.horario = DateTime.Now;
        //                respSolicitacao.solicitacao = session.Query<Solicitacao>().Where(x=>x.abertura == solicitiacao.abertura).FirstOrDefault();
        //                respSolicitacao.solicitante = HomeController.user;
        //                respSolicitacao.setor = solicitiacao.setor;
        //                session.Save(respSolicitacao);

        //                transacao.Commit();
        //                session.Close();
        //            }
        //            catch (Exception ex)
        //            {
        //                if (!transacao.WasCommitted)
        //                {
        //                    transacao.Rollback();
        //                    session.Close();
        //                }
        //                throw new Exception("Erro ao inserir Solicitação : " + ex.Message);
        //            }
        //        }
        //        //  return RedirectToAction("Index", "Home");
        //        return RedirectToAction("Index", "Panel");
        //    }

        //}

        public ActionResult Detalhe(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var respSolicitacao = session.Query<RespSolicitacao>().Where(x => x.solicitacao.Id == id).ToList();
                session.Close();
                return View(respSolicitacao);
            }
        }

        public string contarChamados()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                List<Solicitacao> solicitacoes = new List<Solicitacao>();

                solicitacoes = session.Query<Solicitacao>().Where(x => x.setor == HomeController.user.setor && x.situacao == situacaoSolicitacao.PENDENTE || x.situacao == situacaoSolicitacao.REABERTURA).ToList();

                session.Close();
                return solicitacoes.Count().ToString();
            }
        }

        public string DataHoraAtual()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }


        [HttpGet]
        [MinhaAutenticacao]
        public ActionResult RespSolicitacao(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var solicitacao = session.Get<Solicitacao>(id);
                
                session.Close();
                return View(solicitacao);
            }
        }

        [HttpPost]
        public ActionResult RespSolicitacao(Solicitacao solicitacao,int idSolicitacao)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        RespSolicitacao respSolicitacao = new RespSolicitacao();
                        respSolicitacao.descricao = solicitacao.descricao;
                        respSolicitacao.situacao = solicitacao.situacao;
                        respSolicitacao.horario = DateTime.Now;
                        respSolicitacao.solicitacao = session.Get<Solicitacao>(idSolicitacao);
                        respSolicitacao.solicitante = HomeController.user;
                        respSolicitacao.setor = respSolicitacao.solicitacao.setor; 
                        session.Save(respSolicitacao);

                        Solicitacao soli = new Solicitacao();
                        soli.Id = respSolicitacao.solicitacao.Id;
                        soli.equipamento = respSolicitacao.solicitacao.equipamento;
                        soli.abertura = respSolicitacao.solicitacao.abertura;
                        soli.area = respSolicitacao.solicitacao.area;
                        soli.descricao = respSolicitacao.solicitacao.descricao;
                        soli.fechamento = respSolicitacao.solicitacao.fechamento;
                        soli.solicitante = respSolicitacao.solicitacao.solicitante;

                        soli.situacao = solicitacao.situacao;
                        soli.setor = solicitacao.setor;
                        if (solicitacao.situacao == situacaoSolicitacao.FINALIZADA)
                        {
                            soli.fechamento = DateTime.Now;
                        }
                        else
                        {
                            soli.fechamento = DateTime.Parse("01/01/0001 00:00:00");
                        }

                        session.Merge(soli);
                        transacao.Commit();
                        session.Close();
                    }
                    catch (Exception ex)
                    {
                        if (!transacao.WasCommitted)
                        {
                            transacao.Rollback();
                            session.Close();
                        }
                        throw new Exception("Erro ao Atualizar solicitação ou guardar resposta da solicitação : " + ex.Message);
                    }
                }
                return RedirectToAction("Index");
            }
        }
    }
}
