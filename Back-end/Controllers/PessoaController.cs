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
                if (Amodel.id == 0)
                {
                    var pessoa = new Pessoa();
                    pessoa.Nome = Amodel.nome;
                    pessoa.Sobrenome = Amodel.sobrenome;
                    pessoa.Cpf = Amodel.cpf.Replace(".", "").Replace("-", "");
                    pessoa.Email = Amodel.email.Replace("/","");
                    pessoa.Sexo_Id = Amodel.sexo;
                    pessoa.Nascimento = Amodel.nascimento;                    
                    _context.Pessoa.Add(pessoa);
                    _context.SaveChanges();
                    return new
                    { Status = "Success", Message = "SuccessFully Saved." };
                }
                else
                {
                    var pessoa = _context.Pessoa.FirstOrDefault(s => s.Id == Amodel.id);
                    if (pessoa.Id > 0)
                    {
                        pessoa.Nome = Amodel.nome;
                        pessoa.Sobrenome = Amodel.sobrenome;
                        pessoa.Cpf = Amodel.cpf;
                        pessoa.Email = Amodel.email;
                        pessoa.Sexo_Id = Amodel.sexo;
                        pessoa.Nascimento = Amodel.nascimento;                        
                        _context.Pessoa.Add(pessoa);
                        _context.SaveChanges();
                        return new
                        { Status = "Success", Message = "SuccessFully Update." };
                    }
                    return new
                    { Status = "Error", Message = "Invalid." };
                }
            }
            catch (Exception ex)
            {
                return new
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }

        [Route("GetPessoaData")]
        [HttpGet]
        public object GetStudentData()
        {
            var obj = from u in _context.Pessoa
                      select new UserViewModel() {
                          id = u.Id,
                          nome = u.Nome,
                          sobrenome = u.Sobrenome,
                          cpf = u.Cpf,
                          email = u.Email,
                          sexo = u.Sexo_Id,
                          nascimento = u.Nascimento
                      };
            return obj;
        }

        // && E
        // || OU
        // ! NOT

        [Route("GetPessoaById/{Id}")]
        [HttpGet]
        public object GetStudentById(int Id)
        {
            var obj = (from u in _context.Pessoa
                       select new UserViewModel()
                       {
                           id = u.Id,
                           nome = u.Nome,
                           sobrenome = u.Sobrenome,
                           cpf = u.Cpf,
                           email = u.Email,
                           sexo =  u.Sexo_Id,
                           nascimento = u.Nascimento
                       }).FirstOrDefault(x => x.id == Id);
            return obj;
        }

        [Route("UpdatePessoa")]
        [HttpPut]
        public object UpdatePessoa(UserViewModel Amodel)
        {            
            var pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == Amodel.id);

            if (pessoa != null)
            {
                pessoa.Nome = Amodel.nome;
                pessoa.Sobrenome = Amodel.sobrenome;
                pessoa.Cpf = Amodel.cpf.Replace(".", "").Replace("-", "");
                pessoa.Email = Amodel.email;
                pessoa.Sexo_Id = Amodel.sexo;
                pessoa.Nascimento = Amodel.nascimento;
                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }
            return new
            { Status = "Success", Message = "SuccessFully Updated." };
        }
        [AcceptVerbs("DELETE")]
        [Route("DeletePessoa/{Id}")]
        [HttpGet]
        public object DeleteStudent(int Id)
        {
            try
            {
                var pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == Id);
                _context.Pessoa.Remove(pessoa);
                _context.SaveChanges();
                return new
                { Status = "Success", Message = "SuccessFully Delete." };
            }
            catch (Exception ex)
            {
                return new
                { Status = "Error", Message = ex.Message.ToString() };
            }
        }




    }
}
