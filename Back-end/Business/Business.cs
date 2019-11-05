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

namespace Back_end.Business
{
    public class Businesscrud : ApiController
    {
        private PessoaContext _context = new PessoaContext();
        public string CriarPessoa(UserViewModel Amodel)
        {
            try
            {
                if (Amodel.id == 0)
                {
                    var pessoa = new Pessoa();
                    pessoa.Nome = Amodel.nome;
                    pessoa.Sobrenome = Amodel.sobrenome;
                    pessoa.Cpf = Amodel.cpf.Replace(".", "").Replace("-", "");
                    pessoa.Email = Amodel.email.Replace("/", "");
                    pessoa.Sexo_Id = Amodel.sexo;
                    pessoa.Nascimento = Amodel.nascimento;
                    //var obj = _context.Pessoa.FirstOrDefault(c => c.Cpf == Amodel.cpf);
                    var obj = _context.Pessoa.Any(c => c.Cpf == pessoa.Cpf);
                    if (obj)
                    {
                        return "O CPF inserido já existe na base de dados!";
                    }
                    else
                    {
                        _context.Pessoa.Add(pessoa);
                        _context.SaveChanges();
                        return "Success";
                    }
                }else
                {
                    return "Id Inválido!";
                }

            }
            catch (Exception ex)
            {
                return ex.ToString(); 
            }
        }

        public IQueryable ListaPessoas()
        {
            var obj = from u in _context.Pessoa
                      select new UserViewModel()
                      {
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
        public UserViewModel LerPessoa(int Id)
        {
            var obj = (from u in _context.Pessoa
                       select new UserViewModel()
                       {
                           id = u.Id,
                           nome = u.Nome,
                           sobrenome = u.Sobrenome,
                           cpf = u.Cpf,
                           email = u.Email,
                           sexo = u.Sexo_Id,
                           nascimento = u.Nascimento
                       }).FirstOrDefault(x => x.id == Id);
            return obj;
        }

        public string AtualizarPessoa(UserViewModel Amodel)
        {
            try
            {
                var pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == Amodel.id);
                Pessoa p2 = new Pessoa();
                if (pessoa != null)
                {
                    pessoa.Nome = Amodel.nome;
                    pessoa.Sobrenome = Amodel.sobrenome;
                    if(pessoa.Cpf != Amodel.cpf.Replace(".", "").Replace("-", ""))
                    {
                        var obj = _context.Pessoa.Any(c => c.Cpf == Amodel.cpf.Replace(".", "").Replace("-", ""));
                        if (obj)
                        {
                            return "O CPF inserido já existe na base de dados!";
                        }
                        else
                        {
                            pessoa.Cpf = Amodel.cpf.Replace(".", "").Replace("-", "");
                        }                  
                    }        
                    pessoa.Email = Amodel.email;
                    pessoa.Sexo_Id = Amodel.sexo;
                    pessoa.Nascimento = Amodel.nascimento;
                    _context.SaveChanges();
                    return "Success";
                }
                else
                {
                    return "Id Inválido!";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }

        public IHttpActionResult DeletarPessoa(int Id)
        {
            try
            {
                var pessoa = _context.Pessoa.FirstOrDefault(p => p.Id == Id);
                _context.Pessoa.Remove(pessoa);
                _context.SaveChanges();
                return Ok("Usuário deletado com sucesso");
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}