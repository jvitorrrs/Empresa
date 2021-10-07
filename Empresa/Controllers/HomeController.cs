using Clientes.Models;
using Jint.Runtime.Debugger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Clientes.Models.Participantes;

namespace Empresa.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult Salvar(int? codpar, string name, string datanascimento, string telefone, int? pacote, int? valorSelAtv1, int? valorSelAtv2, int? valorSelAtv3)
        {
            codpar = 1;
            /*pega o ultimo codpar e soma +1*/
            //var CP = Participantes.GetCodPar();
            //codpar = CP[0].CodPar;
            //codpar++;


            int preco;
            string descricao;
            int vagas;
            int precoatv = 0;
            int codatv = 0;
            string descatv = "";

            /*preco e descricao dos pacotes*/
            DateTime date = DateTime.Now;
            date.AddDays(13);
            if (DateTime.Now > date && pacote == 1)
            {
                preco = 150;
                descricao = "Sócio-Promoção";
            }
            else
            {
                preco = 300;
                descricao = "Sócio";
            }

            if (pacote == 2)
            {
                preco = 500;
                descricao = "Não-Sócio";
            }
            /*preco e descricao das atividades*/
            if (valorSelAtv1 == 1)
            {
                codatv = 1;
                descatv = "Atividade 1";
                precoatv = 300;
            }

            if (valorSelAtv2 == 2)
            {
                codatv = 2;
                descatv = "Atividade 2";
                precoatv = 500;
            }

            if (valorSelAtv3 == 3)
            {
                codatv = 3;
                descatv = "Atividade 3";
                precoatv = 600;
            }

            vagas = 20;
            //var VG = Participantes.GetCodPar();
            //vagas = VG[2].Vagas;
            //vagas--;

            var participante = new Participantes();

            participante.CodPar = (int) codpar;
            participante.Nome = name;
            participante.DataNascimento = Convert.ToDateTime(datanascimento);
            participante.Telefone = Convert.ToInt32(telefone);
            participante.Preco = preco;
            participante.Descricao = descricao;
            participante.CodAtv = codatv;
            participante.DescAtv = descatv;
            participante.Vagas = vagas;
            participante.PrecoAtv = precoatv;

            participante.Salvar();

            //retornando o json com a mensagem de sucesso
            return Json("Salvo com Sucesso", JsonRequestBehavior.AllowGet);
            
        }     
    }
}