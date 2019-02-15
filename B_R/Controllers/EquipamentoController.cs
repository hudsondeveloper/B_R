using System;
using System.Collections.Generic;
using System.IO;
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
    public class EquipamentoController : Controller
    {
        // GET: Equipamento
        public ActionResult Index(int? id, string situacao)
        {

            List<Equipamento> equipamento = new List<Equipamento>();
            Session["idArea"] = id;
            using (ISession session = NHibernateHelper.OpenSession())
            {
            ViewBag.area = session.Query<Area>().ToList();
                if (id == null && (situacao == ""|| situacao == null))
                {
                equipamento = session.Query<Equipamento>().ToList();
                session.Close();
                return View(equipamento);
                }
                else
                {
                    if (id != null && situacao != null && situacao != "")
                    {
                        equipamento = session.Query<Equipamento>().Where(x => x.area.Id == id && x.situacao == Utils.verificarSituacaoEquipamento(situacao)).ToList();
                    }
                    else if (id == null && situacao != "")
                    {
                        equipamento = session.Query<Equipamento>().Where(x => x.situacao == Utils.verificarSituacaoEquipamento(situacao)).ToList();
                    }
                    else if (id != null && (situacao == "" || situacao == null))
                    {
                        equipamento = session.Query<Equipamento>().Where(x => x.area.Id == id).ToList();
                    }

                    session.Close();
                    return View(equipamento);
                }
              
            }
        }


        [HttpGet]
        public ActionResult Comentario(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var Equipamento = session.Get<Equipamento>(id);
                session.Close();
                return View(Equipamento);
            }
        }

        [HttpGet]
        public ActionResult CadastrarComentario(int id)
        {
            Session["idEquipamento"] = id;
            return View();
        }

        [HttpPost]
        public ActionResult CadastrarComentario(ComentariosEquipamento comentario, int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {
                        comentario.equipamento = session.Get<Equipamento>(id);
                        comentario.horario = DateTime.Now;
                        comentario.user = HomeController.user;
                        session.Save(comentario);
                        LogEquipamento log = new LogEquipamento();
                        log.acao = "Comentario";
                        log.equipamento = session.Get<Equipamento>(id);
                        log.anterior = "Comentario: " + comentario.descricao;
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
                return RedirectToAction("Comentario", new { id = id });
            }

        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                ViewBag.area = session.Query<Area>().ToList();

                session.Close();
                return View();
            }
        }

        [HttpPost]
        public ActionResult Cadastrar(Equipamento equipamento,int area)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    ViewBag.area = session.Query<Area>().ToList();
                    try
                    {
                      var equipamentos = session.Query<Equipamento>().OrderByDescending(x=>x.reg).Where(x=>x.area.Id == area).ToList();
                        var reg=0;
                        if (equipamentos.Count() == 0)
                        {
                            reg += 1;
                        }
                        else
                        {
                            reg = equipamentos.FirstOrDefault().reg;
                            reg += 1;
                        }
                        equipamento.reg = reg;
                        equipamento.area = session.Get<Area>(area);
                        session.Save(equipamento);

                        LogEquipamento log = new LogEquipamento();
                        log.equipamento = session.Query<Equipamento>().Where(x => x.reg == equipamento.reg && x.area.Id == area).FirstOrDefault();
                        log.acao = "Cadastrou";
                        var situacaoDesc = log.equipamento.situacao.ToString();
                        log.anterior = "Reg: " + log.equipamento.reg +
                            " Tag: " + log.equipamento.tag +
                            " Status do documento: " + log.equipamento.status_Doc +
                            " Situação: " + situacaoDesc +
                            " Descricao do equipamento: " + log.equipamento.descricao_equipamento +
                            " Descricao: " + log.equipamento.descricao +
                            " Área: " + log.equipamento.area.Id;
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
                        throw new Exception("Erro ao inserir Equipamento : " + ex.Message);
                    }
                }
                return View();
            }
        }

        public ActionResult Detalhe(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var logsEquipamentos = session.Query<LogEquipamento>().Where(x => x.equipamento.Id == id).ToList();
                session.Close();
                return View(logsEquipamentos);
            }
        }

        [HttpGet]
        public ActionResult UploadFile(int idequip,int idarea)
        {
            string strCaminho;
            strCaminho = this.Server.MapPath("~/EquipamentoFile/"+"area"+ idarea + "/"+ "equip" + idequip);
            if (!Directory.Exists(strCaminho)) //Se o diretório não existir...
            {
                System.IO.Directory.CreateDirectory(strCaminho);
            }
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/EquipamentoFile/" + "area" + idarea + "/"+ "equip" + idequip + "/"));
            System.IO.FileInfo[] fileNames = dir.GetFiles("*.*"); List<string> items = new List<string>();
            foreach (var file in fileNames)
            {
                items.Add(file.Name);
            }
            Session["idequip"]= idequip;
            Session["areaid"] = idarea;
            return View(items);
        }
        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file,int id, int idarea)
        {

            string strCaminho;
            strCaminho = this.Server.MapPath("~\\EquipamentoFile/" + "area" + idarea + "/"+"equip" + id +"/");
            if (!Directory.Exists(strCaminho)) //Se o diretório não existir...
            {
                System.IO.Directory.CreateDirectory(strCaminho);
            }

            try
            {
                if (file.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(file.FileName);
                    string _path = Path.Combine(Server.MapPath("~\\EquipamentoFile/" + "area" + idarea + "/" + "equip" + id + "/"), _FileName);
                    file.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                using (ISession session = NHibernateHelper.OpenSession())
                {
                    using (ITransaction transacao = session.BeginTransaction())
                    {
                        string _FileName = Path.GetFileName(file.FileName);

                        LogEquipamento log = new LogEquipamento();
                        log.acao = "Foto";
                        log.equipamento = session.Get<Equipamento>(id);
                        log.anterior = "Nome: " + _FileName;
                        log.horario = DateTime.Now;
                        log.user = HomeController.user;
                        session.Save(log);
                        transacao.Commit();
                        session.Close();
                    }
                }

                return RedirectToAction("UploadFile",new { idequip  = id, idarea = idarea });
            }
            catch(Exception ex)
            {
               var bola= ex.Message;
                ViewBag.Message = "File upload failed!!";
                return RedirectToAction("UploadFile", new { idequip = id , idarea = idarea });
            }
        }

        public FileResult Download(string ImageName,int id,int idarea)
        {
            var FileVirtualPath = "~/EquipamentoFile/" + "area" + idarea + "/" + "equip" + id + "/" + ImageName;
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        [HttpGet]
        public ActionResult Editar(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var equipamento = session.Get<Equipamento>(id);
                Session["areaid"] = equipamento.area.Id;
                session.Close();
                return View(equipamento);
            }
        }


        [HttpPost]
        public ActionResult Editar(Equipamento equipamento,int idArea)
        {
            int dado;
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transacao = session.BeginTransaction())
                {
                    try
                    {

                        LogEquipamento log = new LogEquipamento();
                        log.acao = "Editou";
                        log.equipamento = session.Get<Equipamento>(equipamento.Id);
                        var situacaoDesc = log.equipamento.situacao.ToString();
                        var situacaoNovo = equipamento.situacao.ToString();

                        log.alteracao =
                         " Status do documento: " + equipamento.status_Doc +
                         " Situação: " + situacaoNovo;


                        log.anterior =
                         " Status do documento: " + log.equipamento.status_Doc +
                         " Situação: " + situacaoDesc ;
   




                        log.horario = DateTime.Now;
                        log.user = HomeController.user;
                        session.Save(log);

                        equipamento.area = session.Get<Area>(idArea);
                        dado = equipamento.area.Id;
                        equipamento.reg = log.equipamento.reg;
                        session.Merge(equipamento);
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
                        throw new Exception("Erro ao Atualizar Equipamento : " + ex.Message);
                    }
                }
                return RedirectToAction("Index", new { id = dado });
            }
        }

        //[HttpGet]
        //public ActionResult Deletar(int id)
        //{
        //    int dado;
        //    using (ISession session = NHibernateHelper.OpenSession())
        //    {
        //        using (ITransaction transacao = session.BeginTransaction())
        //        {
        //            try
        //            {
        //                Equipamento equipamento = session.Get<Equipamento>(id);
        //                dado = equipamento.area.Id;
        //                session.Delete(equipamento);
        //                transacao.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                if (!transacao.WasCommitted)
        //                {
        //                    transacao.Rollback();
        //                }
        //                throw new Exception("Erro ao Deletar equipamento : " + ex.Message);
        //            }
        //        }
        //        return RedirectToAction("Index",new { id = dado });
        //    }
        //}
    }
}
