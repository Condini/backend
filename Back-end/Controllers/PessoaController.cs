﻿using System.Threading.Tasks;
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
                    return BadRequest("Não foi possível adicionar essa pessoa.\n Verifique seus dados e tente novamente.");
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
                return BadRequest("Não foi possível adicionar o usuário. Verifique seus dados e tente novamente!");
            }
        }

        [Route("GetPessoaData")]
        [HttpGet]
        public IHttpActionResult GetPessoaData()
        {
            try
            {
                var business = new Business.Businesscrud();
                return Ok(business.ListaPessoas());
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível trazer a lista de pessoas.");
            }
        }

        [Route("GetPessoaById/{Id}")]
        [HttpGet]
        public IHttpActionResult GetPessoaById(int Id)
        {
            try
            {
                var pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == Id);
                if (Id == pessoa.Id)
                {
                    var business = new Business.Businesscrud();
                    return Ok(business.LerPessoa(Id));
                }
                else
                {
                    return BadRequest("Este ID é inválido ou inexistente.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Não foi possível trazer os dados da pessoa.");
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
                    return BadRequest("Não foi possível atualizar os dados dessa pessoa.\n Verifique seus dados e tente novamente.");
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
                return BadRequest("Não foi possível atualizar os dados dessa pessoa. Verifique seus dados e tente novamente.");
            }

        }
        [AcceptVerbs("DELETE")]
        [Route("DeletePessoa/{Id}")]
        [HttpDelete]
        public IHttpActionResult DeletePessoa(int Id)
        {
            try
            {
                var business = new Business.Businesscrud();
                business.DeletarPessoa(Id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Ocorreu algum erro.");
            }
        }

        }
}
