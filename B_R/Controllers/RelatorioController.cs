using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using B_R.Authentication;
using B_R.Models;
using Microsoft.Reporting.WebForms;
using NHibernate;
using NHibernate.Linq;

namespace B_R.Controllers
{
    public class RelatorioController : Controller
    {
        // GET: Relatorio
        public ActionResult SSM(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var solicitacao = session.Query<Solicitacao>().Where(x => x.Id == id).FirstOrDefault();
                var ObservacoesBusca = session.Query<RespSolicitacao>().Where(x => x.solicitacao.Id == id).ToList();
                List<ObservacaoSolicitacao> solicitacaoDB = new List<ObservacaoSolicitacao>();
                foreach (var data in ObservacoesBusca)
                {
                    ObservacaoSolicitacao tdJunto = new ObservacaoSolicitacao();

                     tdJunto.area = data.solicitacao.area.nome;
                     tdJunto.Id = data.solicitacao.Id;
                     tdJunto.equipamento = data.solicitacao.equipamento.tag;
                     tdJunto.solicitante = data.solicitacao.solicitante.Nome;
                     tdJunto.prioridade = data.solicitacao.prioridade.ToString();
                     tdJunto.modalidade = data.solicitacao.modalidade.ToString();
                     tdJunto.situacao = data.solicitacao.situacao.ToString();
                     tdJunto.descricao = data.solicitacao.descricao; 
                     tdJunto.abertura = data.solicitacao.abertura.ToString("dd/MM/yyyy HH:mm:ss");
                     tdJunto.setor = data.solicitacao.setor.ToString();
                     tdJunto.descricao2 = data.descricao;
                     tdJunto.horario = data.horario.ToString("dd/MM/yyyy HH:mm:ss");
                     tdJunto.solicitante2 = data.solicitante.Nome;

                    solicitacaoDB.Add(tdJunto);
                }

                LocalReport relat = new LocalReport();
                //caminho do arquivo rdlc
                relat.ReportPath = Server.MapPath("~/relatorios/SSM.rdlc");

                //vinculando dataset ao objeto relat
                var ds = new ReportDataSource();
                ds.Name = "dsSolicitacao";
                ds.Value = solicitacaoDB;
                relat.DataSources.Add(ds);

                //definindo tipo que o relatório será renderizado
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                //configurações da página ex: margin, top, left ...
                string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>PDF</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0in</MarginTop>" +
                "<MarginLeft>0in</MarginLeft>" +
                "<MarginRight>0in</MarginRight>" +
                "<MarginBottom>0in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] bytes;

                //Renderizando o relatório o bytes
                bytes = relat.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //Retornondo o arquivo renderizado
                //dessa forma o arquivo será aberto na mesma aba do navegador em que foi chamado

                session.Close();
                return File(bytes, mimeType);
            }
        }

        // GET: Relatorio
        public ActionResult LinhasHidrantesBombas(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var solicitacao = session.Query<Solicitacao>().Where(x => x.Id == id).ToList();
                var solicitacaoDB = solicitacao.Select(x => new
                {
                    Id = x.Id,
                    area = x.area.nome,
                    equipamento = x.equipamento.tag,
                    solicitante = x.solicitante.Nome,
                    prioridade = x.prioridade.ToString(),
                    modalidade = x.modalidade.ToString(),
                    situacao = x.situacao.ToString(),
                    descricao = x.descricao,
                    abertura = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    fechamento = x.fechamento,
                    setor = x.setor.ToString(),

                });

                LocalReport relat = new LocalReport();
                //caminho do arquivo rdlc
                relat.ReportPath = Server.MapPath("~/relatorios/LinhasHidrantesBombas.rdlc");

                //vinculando dataset ao objeto relat
                var ds = new ReportDataSource();
                ds.Name = "dsSolicitacao";
                ds.Value = solicitacaoDB;
                relat.DataSources.Add(ds);

                //definindo tipo que o relatório será renderizado
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                //configurações da página ex: margin, top, left ...
                string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>PDF</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0in</MarginTop>" +
                "<MarginLeft>0in</MarginLeft>" +
                "<MarginRight>0in</MarginRight>" +
                "<MarginBottom>0in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] bytes;

                //Renderizando o relatório o bytes
                bytes = relat.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //Retornondo o arquivo renderizado
                //dessa forma o arquivo será aberto na mesma aba do navegador em que foi chamado

                session.Close();
                return File(bytes, mimeType);
            }
        }


        // GET: Relatorio
        public ActionResult ServicosaQuente(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var solicitacao = session.Query<Solicitacao>().Where(x => x.Id == id).ToList();
                var solicitacaoDB = solicitacao.Select(x => new
                {
                    area = x.area.nome,
                    Id = x.Id,
                    equipamento = x.equipamento.tag,
                    solicitante = x.solicitante.Nome,
                    prioridade = x.prioridade.ToString(),
                    modalidade = x.modalidade.ToString(),
                    situacao = x.situacao.ToString(),
                    descricao = x.descricao,
                    abertura = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    fechamento = x.fechamento,
                    setor = x.setor.ToString(),

                });

                LocalReport relat = new LocalReport();
                //caminho do arquivo rdlc
                relat.ReportPath = Server.MapPath("~/relatorios/ServicosaQuente.rdlc");

                //vinculando dataset ao objeto relat
                var ds = new ReportDataSource();
                ds.Name = "dsSolicitacao";
                ds.Value = solicitacaoDB;
                relat.DataSources.Add(ds);

                //definindo tipo que o relatório será renderizado
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                //configurações da página ex: margin, top, left ...
                string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>PDF</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0in</MarginTop>" +
                "<MarginLeft>0in</MarginLeft>" +
                "<MarginRight>0in</MarginRight>" +
                "<MarginBottom>0in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] bytes;

                //Renderizando o relatório o bytes
                bytes = relat.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //Retornondo o arquivo renderizado
                //dessa forma o arquivo será aberto na mesma aba do navegador em que foi chamado

                session.Close();
                return File(bytes, mimeType);
            }
        }

        // GET: Relatorio
        public ActionResult ManutencaoCivilJardinagemServicosGerais(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var solicitacao = session.Query<Solicitacao>().Where(x => x.Id == id).ToList();
                var solicitacaoDB = solicitacao.Select(x => new
                {
                    area = x.area.nome,
                    Id = x.Id,
                    equipamento = x.equipamento.tag,
                    solicitante = x.solicitante.Nome,
                    prioridade = x.prioridade.ToString(),
                    modalidade = x.modalidade.ToString(),
                    situacao = x.situacao.ToString(),
                    descricao = x.descricao,
                    abertura = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    fechamento = x.fechamento,
                    setor = x.setor.ToString(),

                });

                LocalReport relat = new LocalReport();
                //caminho do arquivo rdlc
                relat.ReportPath = Server.MapPath("~/relatorios/ManutencaoCivilJardinagemServicosGerais.rdlc");

                //vinculando dataset ao objeto relat
                var ds = new ReportDataSource();
                ds.Name = "dsSolicitacao";
                ds.Value = solicitacaoDB;
                relat.DataSources.Add(ds);

                //definindo tipo que o relatório será renderizado
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                //configurações da página ex: margin, top, left ...
                string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>PDF</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0in</MarginTop>" +
                "<MarginLeft>0in</MarginLeft>" +
                "<MarginRight>0in</MarginRight>" +
                "<MarginBottom>0in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] bytes;

                //Renderizando o relatório o bytes
                bytes = relat.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //Retornondo o arquivo renderizado
                //dessa forma o arquivo será aberto na mesma aba do navegador em que foi chamado

                session.Close();
                return File(bytes, mimeType);
            }
        }


        // GET: Relatorio
        public ActionResult EspacoConfinado(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
             
                var solicitacao = session.Query<Solicitacao>().Where(x => x.Id == id).ToList();
                var solicitacaoDB = solicitacao.Select(x => new
                {
                    area = x.area.nome,
                    Id = x.Id,
                    equipamento = x.equipamento.tag,
                    solicitante = x.solicitante.Nome,
                    prioridade = x.prioridade.ToString(),
                    modalidade = x.modalidade.ToString(),
                    situacao = x.situacao.ToString(),
                    descricao = x.descricao,
                    abertura = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    fechamento = x.fechamento,
                    setor = x.setor.ToString(),

                });

                LocalReport relat = new LocalReport();
                //caminho do arquivo rdlc
                relat.ReportPath = Server.MapPath("~/relatorios/EspacoConfinado.rdlc");

                //vinculando dataset ao objeto relat
                var ds = new ReportDataSource();
                ds.Name = "dsSolicitacao";
                ds.Value = solicitacaoDB;
                relat.DataSources.Add(ds);

                //definindo tipo que o relatório será renderizado
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                //configurações da página ex: margin, top, left ...
                string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>PDF</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
    
                "<MarginLeft>0in</MarginLeft>" +
                "<MarginRight>0in</MarginRight>" +
     
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] bytes;

                //Renderizando o relatório o bytes
                bytes = relat.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //Retornondo o arquivo renderizado
                //dessa forma o arquivo será aberto na mesma aba do navegador em que foi chamado

                session.Close();
                return File(bytes, mimeType);
            }
        }

        // GET: Relatorio
        public ActionResult ServicosEletricidade(int id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var solicitacao = session.Query<Solicitacao>().Where(x => x.Id == id).ToList();
                var solicitacaoDB = solicitacao.Select(x => new
                {
                    area = x.area.nome,
                    Id = x.Id,
                    equipamento = x.equipamento.tag,
                    solicitante = x.solicitante.Nome,
                    prioridade = x.prioridade.ToString(),
                    modalidade = x.modalidade.ToString(),
                    situacao = x.situacao.ToString(),
                    descricao = x.descricao,
                    abertura = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    fechamento = x.fechamento,
                    setor = x.setor.ToString(),

                });

                LocalReport relat = new LocalReport();
                //caminho do arquivo rdlc
                relat.ReportPath = Server.MapPath("~/relatorios/ServicosEletricidade.rdlc");

                //vinculando dataset ao objeto relat
                var ds = new ReportDataSource();
                ds.Name = "dsSolicitacao";
                ds.Value = solicitacaoDB;
                relat.DataSources.Add(ds);

                //definindo tipo que o relatório será renderizado
                string reportType = "PDF";
                string mimeType;
                string encoding;
                string fileNameExtension;

                //configurações da página ex: margin, top, left ...
                string deviceInfo =
                "<DeviceInfo>" +
                "<OutputFormat>PDF</OutputFormat>" +
                "<PageWidth>8.5in</PageWidth>" +
                "<PageHeight>11in</PageHeight>" +
                "<MarginTop>0in</MarginTop>" +
                "<MarginLeft>0in</MarginLeft>" +
                "<MarginRight>0in</MarginRight>" +
                "<MarginBottom>0in</MarginBottom>" +
                "</DeviceInfo>";

                Warning[] warnings;
                string[] streams;
                byte[] bytes;

                //Renderizando o relatório o bytes
                bytes = relat.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);

                //Retornondo o arquivo renderizado
                //dessa forma o arquivo será aberto na mesma aba do navegador em que foi chamado

                session.Close();
                return File(bytes, mimeType);
            }
        }
    }
}
