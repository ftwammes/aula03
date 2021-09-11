using App.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Interfaces.Application
{
    public interface IPessoaService
    {
        Pessoa BuscaPorId(Guid Id);
        List<Pessoa> listaPessoas(string nome, int pesoMaiorQue, int pesoMenorQue);
        void Salvar(Pessoa obj);
        void Remover(Guid Id);
    }
}
