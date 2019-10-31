using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Sql;
using System.Data.Entity;
using Back_end.Models;
using System.Web.Http.Cors;
using System.Data.SqlClient;
using System.Text;

namespace Back_end.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]


    [RoutePrefix("api")]
    public class PessoaController : ApiController
    {

        private PessoaContext _context = new PessoaContext();
        
        [Route("PessoaInsert")]
        [HttpPost]
        public IHttpActionResult PessoaInsert(UserViewModel Amodel) // Amodel = Model do Angular
        {
            var mensagem = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var business = new Business.Businesscrud();
                mensagem = business.CriarPessoa(Amodel);
                if (mensagem == "Success")
                {
                    return Ok();
                }else
                {
                    return BadRequest(mensagem);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("GetPessoaData")]
        [HttpGet]
        public IHttpActionResult GetStudentData()
        {
            try
            {
                var business = new Business.Businesscrud();
                return Ok(business.ListaPessoas());
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // && E
        // || OU
        // ! NOT

        [Route("GetPessoaById/{Id}")]
        [HttpGet]
        public IHttpActionResult GetStudentById(int Id)
        {
            try
            {
                var business = new Business.Businesscrud();
                return Ok(business.LerPessoa(Id));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

        }

        [Route("UpdatePessoa")]
        [HttpPut]
        public IHttpActionResult UpdatePessoa(UserViewModel Amodel)
        {
            var mensagem = "";
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var business = new Business.Businesscrud();
                mensagem = business.AtualizarPessoa(Amodel);
                if (mensagem == "Success")
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(mensagem);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [AcceptVerbs("DELETE")]
        [Route("DeletePessoa/{Id}")]
        [HttpDelete]
        public object DeleteStudent(int Id)
        {
            try
            {
                var business = new Business.Businesscrud();
                business.DeletarPessoa(Id);
            }
            catch (Exception ex)
            {
                return new
                { Status = "Error", Message = ex.Message.ToString() };
            }
            return Ok();
        }

        /// ------------------------------------------------------------- testando opçoes
        }
}
