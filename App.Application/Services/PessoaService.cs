using App.Domain.Entities;
using App.Domain.Interfaces.Application;
using App.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Application.Services
{
    public class PessoaService : IPessoaService
    {
        private IRepositoryBase<Pessoa> _repository { get; set; }
        public PessoaService(IRepositoryBase<Pessoa> repository)
        {
            _repository = repository;
        }
        public Pessoa BuscaPorId (Guid id)
        {
            var obj = _repository.Query(x => x.Id == id).FirstOrDefault();
            return obj;
        }
        public List<Pessoa> listaPessoas(string nome, int pesoMaiorQue, int pesoMenorQue)
        {
            if(nome == null)
            {
                nome = "";
            }
            return _repository.Query(X => 
            X.Nome.ToUpper().Contains(nome.ToUpper()) &&
            (pesoMaiorQue == 0 || X.Peso >= pesoMaiorQue) &&
            (pesoMenorQue == 0 || X.Peso <= pesoMenorQue)
          
                ).Select(p => new Pessoa
                {
                    Id = p.Id,
                    Nome = p.Nome,
                    Peso = p.Peso,
                    Cidade = new Cidade
                    {
                        Nome = p.Cidade.Nome
                    }
                }).OrderByDescending(x => x.Nome).ToList();
            
        }

        public void Salvar(Pessoa obj)
        {
            if (String.IsNullOrEmpty(obj.Nome))
            { 
            throw new Exception("Informe o nome:");
            }
            _repository.Save(obj);
            _repository.SaveChanges();
        }
        public void Remover(Guid Id)
        {
            _repository.Delete(Id);
            _repository.SaveChanges();

        }
    }
}
