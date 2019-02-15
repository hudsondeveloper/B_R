using B_R.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace B_R.Controllers
{
    public class Utils
    {
        public static modalidade verificarModalidade(string modalidade)
        {
            if (modalidade == Models.modalidade.CORRETIVA.ToString())
            {
                return Models.modalidade.CORRETIVA;
            }
            else if (modalidade == Models.modalidade.PLANO_DE_INSPEÇÃO.ToString())
            {
                return Models.modalidade.PLANO_DE_INSPEÇÃO;
            }
            else if (modalidade == Models.modalidade.PREDITIVA.ToString())
            {
                return Models.modalidade.PREDITIVA;
            }
            else if (modalidade == Models.modalidade.PREVENTIVA.ToString())
            {
                return Models.modalidade.PREVENTIVA;
            }
            else if (modalidade == Models.modalidade.SERVIÇOS_GERAIS.ToString())
            {
               return Models.modalidade.SERVIÇOS_GERAIS;
            }      
           return Models.modalidade.PREVENTIVA;
        }
            public static prioridade verificarPrioridade(string prioridade)
        {
            if (prioridade == Models.prioridade.EMERGÊNCIA.ToString())
            {
                return Models.prioridade.EMERGÊNCIA;
            }else if (prioridade == Models.prioridade.MELHORIA.ToString())
            {
                return Models.prioridade.MELHORIA;
            }
            else if (prioridade == Models.prioridade.MODERADA.ToString())
            {
                return Models.prioridade.MODERADA;
            }
            else if (prioridade == Models.prioridade.MODIFICAÇÃO.ToString())
            {
                return Models.prioridade.MODIFICAÇÃO;
            }
            else if (prioridade == Models.prioridade.PROGRAMACAO.ToString())
            {
                return Models.prioridade.PROGRAMACAO;
            }
            else if (prioridade == Models.prioridade.SIMPLES.ToString())
            {
                return Models.prioridade.SIMPLES;
            }
            else if (prioridade == Models.prioridade.URGENTE.ToString())
            {
                return Models.prioridade.URGENTE;
            }
            return Models.prioridade.SIMPLES;
        
        }

        public static situacao verificarSituacaoEquipamento(string situacao)
        {
            if (situacao == Models.situacao.ATIVO.ToString())
            {
                return Models.situacao.ATIVO;
            }
            else if (situacao == Models.situacao.DESATIVADA.ToString())
            {
                return Models.situacao.DESATIVADA;
            }
            else if (situacao == Models.situacao.EM_MONTAGEM.ToString())
            {
                return Models.situacao.EM_MONTAGEM;
            }
            else if (situacao == Models.situacao.FORA_DE_OPERACAO.ToString())
            {
                return Models.situacao.FORA_DE_OPERACAO;
            }
            return Models.situacao.ATIVO;
        }

        public static situacaoSolicitacao verificarSituacao(string situacao)
        {
             if (situacao == Models.situacaoSolicitacao.CANCELADA.ToString())
            {
                return Models.situacaoSolicitacao.CANCELADA;
            }
            else if (situacao == Models.situacaoSolicitacao.FINALIZADA.ToString())
            {
                return Models.situacaoSolicitacao.FINALIZADA;
            }
            else if (situacao == Models.situacaoSolicitacao.PENDENTE.ToString())
            {
                return Models.situacaoSolicitacao.PENDENTE;
            }
            else if (situacao == Models.situacaoSolicitacao.REABERTURA.ToString())
            {
                return Models.situacaoSolicitacao.REABERTURA;
            }
            return Models.situacaoSolicitacao.PENDENTE;
        }

        public static setor verificarSetor(string setor)
        {
            if (setor == Models.setor.AUTOMACAO.ToString())
            {
                return Models.setor.AUTOMACAO;
            }
            else if (setor == Models.setor.BALANÇA.ToString())
            {
                return Models.setor.BALANÇA;
            }
            else if (setor == Models.setor.CIPA.ToString())
            {
                return Models.setor.CIPA;
            }
            else if (setor == Models.setor.CIVIL.ToString())
            {
                return Models.setor.CIVIL;
            }
            else if (setor == Models.setor.COMPRAS.ToString())
            {
                return Models.setor.COMPRAS;
            }
            else if (setor == Models.setor.CONTABILIDADE.ToString())
            {
                return Models.setor.CONTABILIDADE;
            }
            else if (setor == Models.setor.ELETRICA.ToString())
            {
                return Models.setor.ELETRICA;
            }
            else if (setor == Models.setor.ESCRITÓRIO.ToString())
            {
                return Models.setor.ESCRITÓRIO;
            }
            else if (setor == Models.setor.FATURAMENTO.ToString())
            {
                return Models.setor.FATURAMENTO;
            }
            else if (setor == Models.setor.JARDINAGEM.ToString())
            {
                return Models.setor.JARDINAGEM;
            }
            else if (setor == Models.setor.LIMPEZA.ToString())
            {
                return Models.setor.LIMPEZA;
            }
            else if (setor == Models.setor.MECANICA.ToString())
            {
                return Models.setor.MECANICA;
            }
            else if (setor == Models.setor.OPERAÇÃO.ToString())
            {
                return Models.setor.OPERAÇÃO;
            }
            else if (setor == Models.setor.PINTURA.ToString())
            {
                return Models.setor.PINTURA;
            }
            else if (setor == Models.setor.PROJETOS.ToString())
            {
                return Models.setor.PROJETOS;
            }
            else if (setor == Models.setor.RH.ToString())
            {
                return Models.setor.RH;
            }
            else if (setor == Models.setor.SEGURANÇA.ToString())
            {
                return Models.setor.SEGURANÇA;
            }
            else if (setor == Models.setor.TI.ToString())
            {
                return Models.setor.TI;
            }
            return Models.setor.TI;
        }
    }
}