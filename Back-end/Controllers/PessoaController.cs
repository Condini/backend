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
        public object PessoaInsert(UserViewModel Amodel) // Amodel = Model do Angular
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var business = new Business.Businesscrud();
                business.CriarPessoa(Amodel);
            }
            catch (Exception ex)
            {
                return new
                { Status = "Error", Message = ex.Message.ToString() };
            }
            return Ok();
        }

        [Route("GetPessoaData")]
        [HttpGet]
        public object GetStudentData()
        {
            try
            {
                var business = new Business.Businesscrud();
                return business.ListaPessoas();
            }
            catch (Exception ex)
            {
                return new
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }

        // && E
        // || OU
        // ! NOT

        [Route("GetPessoaById/{Id}")]
        [HttpGet]
        public object GetStudentById(int Id)
        {
            try
            {
                var business = new Business.Businesscrud();
                return business.LerPessoa(Id);
            }
            catch (Exception ex)
            {
                return new
                { Status = "Error", Message = ex.Message.ToString() };
            }

        }

        [Route("UpdatePessoa")]
        [HttpPut]
        public object UpdatePessoa(UserViewModel Amodel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var business = new Business.Businesscrud();
                business.AtualizarPessoa(Amodel);
            }
            catch (Exception ex)
            {
                return new
                { Status = "Error", Message = ex.Message.ToString() };
            }
            return Ok();
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
