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
    public class AreaController : Controller
    {
        // GET: Area
        public ActionResult Index()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var areas = session.Query<Area>().ToList();
                session.Close();
                return View(areas);
            } 
        }


        public ActionResult Detalhe(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var logsArea = session.Query<LogArea>().Where(x => x.area.Id == id).ToList();
                session.Close();
                return View(logsArea);
            }
        }

        //[HttpGet]
        //public ActionResult Deletar(int id)
        //{
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        using (ITransaction transacao = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                Area area = session.Get<Area>(id);

        //                LogArea log = new LogArea();
        //                log.acao = "Deletar";
        //                log.area = session.Get<Area>(id);
        //                log.descricao = "Deletando area de codigo: " + area.codigo + " e nome: " + area.nome;
        //                log.horario = DateTime.Now;
        //                log.user = HomeController.user;
        //                session.Save(log);

        //                session.Delete(area);
        //                transacao.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                if (!transacao.WasCommitted)
        //                {
        //                    transacao.Rollback();
        //                }
        //                throw new Exception("Erro ao Deletar Cliente : " + ex.Message);
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //}

        [HttpGet]
        public ActionResult Comentario(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var area = session.Get<Area>(id);
                session.Close();
                return View(area);
            }
        }

        [HttpGet]
        public ActionResult CadastrarComentario(int id)
        {
            Session["idArea"] = id;
            return View();
        }


        [HttpPost]
        public ActionResult CadastrarComentario(ComentariosArea comentario,int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        comentario.area = session.Get<Area>(id);
                        comentario.horario = DateTime.Now;
                        comentario.user = HomeController.user;
                        session.Save(comentario);
                        LogArea log = new LogArea();
                        log.acao = "Comentario";
                        log.area = session.Get<Area>(id);
                        log.anterior = "Comentario: " + comentario.descricao ;
                        log.horario = DateTime.Now;
                        log.user = HomeController.user;
                        session.Save(log);
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
                        throw new Exception("Erro ao inserir Comentario : " + ex.Message);
                    }
                }
                //  return RedirectToAction("Index", "Home");
                return RedirectToAction("Comentario",new {id=id });
            }

        }

        [HttpGet]
        public ActionResult Editar(int id) {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var area = session.Get<Area>(id);
                session.Close();
                return View(area);
            }
        }

        [HttpPost]
        public ActionResult Editar(Area area)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        LogArea log = new LogArea();
                        log.acao = "Editou";
                        log.area = session.Get<Area>(area.Id);
                        log.anterior = "Codigo: " + log.area.codigo +
                            " Nome: " + log.area.nome;
                        log.alteracao  = "Codigo: " + area.codigo + " Nome: " + area.nome;
                        log.horario = DateTime.Now;
                        log.user = HomeController.user;
                        session.Save(log);
                        
                        session.Merge(area);
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
                        throw new Exception("Erro ao Atualizar Área : " + ex.Message);
                    }
                }
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Cadastrar(Area area)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        session.Save(area);
                        LogArea log = new LogArea();
                        log.acao = "Cadastrou";
                        log.area = session.Query<Area>().Where(x => x.codigo == area.codigo).FirstOrDefault();
                        log.anterior = "Codigo: "+area.codigo+ " Nome: "+area.nome;
                        log.horario = DateTime.Now;
                        log.user = HomeController.user;
                        session.Save(log);
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
                        throw new Exception("Erro ao inserir Cliente : " + ex.Message);
                    }
                }
                //  return RedirectToAction("Index", "Home");
                return View();
            }
       
        }

    }
}
