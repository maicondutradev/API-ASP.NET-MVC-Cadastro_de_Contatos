﻿using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repository
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly BancoContext _context;
        public ContatoRepository(BancoContext bancoContext) 
        { 
            _context = bancoContext;
        }
        public ContatoModel ListarPorId(int id)
        {
            return _context.Contatos.FirstOrDefault(x => x.Id == id);
        }
        public List<ContatoModel> BuscarTodos()
        {
            return _context.Contatos.ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            //Gravar no banco de dados
            _context.Contatos.Add(contato);
            _context.SaveChanges();
            return contato;
        }
        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if(contatoDB == null)
            {
                throw new System.Exception("Houve um erro na atualização do contato.");
            }

            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _context.Contatos.Update(contatoDB);
            _context.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel contatoDB = ListarPorId(id);

            if (contatoDB == null)
            {
                throw new System.Exception("Houve um erro ao apagar o contato.");
            }

            _context.Contatos.Remove(contatoDB);
            _context.SaveChanges();

            return true;
        }
    }
}
