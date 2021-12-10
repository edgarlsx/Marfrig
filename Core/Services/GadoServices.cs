using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Core.DTO;

namespace Core.Services
{
    public class GadoServices
    {

        #region Create
        public static SendAllDto CreateAll(SendAllDto send)
        {
            if (send.IdPecuarista >=1 && send.IdCompraGado >=1)
                send = AddPecuarista(send);

            if (send.IdAnimal >= 1 && send.IdCompraGadoItem >= 1)
                send = AddAnimal(send);

            if (send.IdCompraGado >= 1 && send.IdCompraGadoItem >= 1)
                send = AddCompraGado(send);

            if (send.IdCompraGadoItem >= 1)
                send = AddCompraGadoItem(send);

            return send;
        }

        private static SendAllDto AddPecuarista(SendAllDto send)
        {
            using (var db = new Context())
            {
                var nPecuarista = db.Pecuarista.Find(send.IdPecuarista);
                if (nPecuarista.Nome.Length <= 0 && send.Nome.Length >= 3)
                {
                    Pecuarista pe = new Pecuarista();
                    pe.Nome = send.Nome;
                    db.Pecuarista.Add(pe);
                    db.SaveChanges();
                }
                else if (nPecuarista.Nome.Length <= 0 && send.Nome.Length >= 3)
                {
                    Pecuarista pe = new Pecuarista();
                    pe.Id = send.IdPecuarista;
                    pe.Nome = send.Nome;
                    db.Pecuarista.Update(pe);
                    db.SaveChanges();
                }
                else
                {
                    send.Message = "Não foi possível salvar Pecuarista; ";
                }
            }

            return send;
        }

        private static SendAllDto AddAnimal(SendAllDto send)
        {
            using (var db = new Context())
            {
                var nAnimal = db.Animal.Find(send.IdAnimal);
                if (nAnimal.Descricao.Length <= 0 && send.Descricao.Length >= 3 && send.Preco >= 0)
                {
                    Animal an = new Animal();
                    an.Descricao = send.Descricao;
                    an.Preco = send.Preco;
                    db.Animal.Add(an);
                    db.SaveChanges();
                }
                else if (nAnimal.Descricao.Length >= 0 && send.Descricao.Length >= 3 && send.Preco >= 0)
                {
                    Animal an = new Animal();
                    an.Id = send.IdAnimal;
                    an.Descricao = send.Descricao;
                    an.Preco = send.Preco;
                    db.Animal.Update(an);
                    db.SaveChanges();
                }
                else
                {
                    send.Message = "Não foi possível salvar Animal; ";
                }
            }

            return send;
        }

        private static SendAllDto AddCompraGado(SendAllDto send)
        {
            using (var db = new Context())
            {
                var cg = db.CompraGado.Find(send.IdCompraGado);
                if (cg.DataEntrega.HasValue == false && send.IdPecuarista >= 0 && send.DataEntrega.HasValue == true )
                {
                    CompraGado c = new CompraGado();
                    c.IdPecuarista = send.IdPecuarista;
                    c.DataEntrega = send.DataEntrega;
                    db.CompraGado.Add(c);
                    db.SaveChanges();
                }
                else if (cg.DataEntrega.HasValue == true && send.IdPecuarista >= 0 && send.DataEntrega.HasValue == true)
                {
                    CompraGado c = new CompraGado();
                    c.Id = send.IdCompraGado;
                    c.IdPecuarista = send.IdPecuarista;
                    c.DataEntrega = send.DataEntrega;
                    db.CompraGado.Update(c);
                    db.SaveChanges();
                }
                else
                {
                    send.Message = "Não foi possível salvar CompraGado; ";
                }
            }
            return send;
        }

        private static SendAllDto AddCompraGadoItem(SendAllDto send)
        {
            using (var db = new Context())
            {
                var ci = db.CompraGadoItem.Find(send.IdCompraGadoItem);
                var cg = db.CompraGado.Find(send.IdCompraGado);
                var an = db.Animal.Find(send.IdAnimal);

                if (ci.IdCompraGado <= 0 && ci.IdAnimal <= 0 && cg.Id >= 0 && an.Id >=0 && send.Quantidade > 0)
                {
                    CompraGadoItem c = new CompraGadoItem();
                    c.IdCompraGado = cg.Id;
                    c.IdAnimal = an.Id;
                    c.Quantidade = send.Quantidade;
                    db.CompraGadoItem.Add(c);
                    db.SaveChanges();
                }
                else if (ci.IdCompraGado >= 0 && ci.IdAnimal >= 0 && cg.Id >= 0 && an.Id >= 0 && send.Quantidade > 0)
                {
                    CompraGadoItem c = new CompraGadoItem();
                    c.IdCompraGado = cg.Id;
                    c.IdAnimal = an.Id;
                    c.Quantidade = send.Quantidade;
                    db.CompraGadoItem.Update(c);
                    db.SaveChanges();
                }
                else
                {
                    send.Message = "Não foi possível salvar CompraGadoItem; ";
                }
            }
            return send;
        }
        #endregion


        #region Get
        public static ICollection<Pecuarista> GetAllPecuarista()
        {
            List<Pecuarista> Pec = new List<Pecuarista>();
            using (var db = new Context())
            {
                Pec = db.Pecuarista.ToList();
            }

            return Pec;
        }

        public static ICollection<GetAllDto> GetAll()
        {
            GetAllDto g = new GetAllDto();
            g.IdCompraGado = 0;
            return Get(g);
        }

        public static ICollection<GetAllDto> GetAllFilters(GetAllDto ga)
        {
            return Get(ga);
        }

        private static ICollection<GetAllDto> Get(GetAllDto ga)
        {
            List<GetAllDto> GetAll = new List<GetAllDto>();

            var db = new Context();
            if (ga.IdCompraGado == 0)
            {
                var list = (from p in db.Pecuarista
                            join c in db.CompraGado on p.Id equals c.IdPecuarista
                            join ci in db.CompraGadoItem on c.Id equals ci.IdCompraGado
                            join a in db.Animal on ci.IdAnimal equals a.Id
                            select new
                            {
                                IdPecuarista = p.Id,
                                Nome = p.Nome,
                                IdCompraGado = c.Id,
                                DataEntrega = c.DataEntrega,
                                IdCompraGadoItem = ci.Id,
                                Quantidade = ci.Quantidade,
                                IdAnimal = a.Id,
                                Descricao = a.Descricao,
                                Preco = a.Preco
                            });
                foreach (var l in list)
                {
                    GetAllDto g = new GetAllDto();
                    g.IdPecuarista = l.IdPecuarista;
                    g.Nome = l.Nome;
                    g.IdCompraGado = l.IdCompraGado;
                    g.DataEntrega = l.DataEntrega;
                    g.IdCompraGadoItem = l.IdCompraGadoItem;
                    g.Quantidade = (int)l.Quantidade;
                    g.IdAnimal = l.IdAnimal;
                    g.Descricao = l.Descricao;
                    g.Preco = l.Preco;

                    GetAll.Add(g);
                }
            }
            else
            {
                var list = (from p in db.Pecuarista
                            join c in db.CompraGado on p.Id equals c.IdPecuarista
                            join ci in db.CompraGadoItem on c.Id equals ci.IdCompraGado
                            join a in db.Animal on ci.IdAnimal equals a.Id
                            where c.Id == ga.IdCompraGado 
                                && p.Id == ga.IdPecuarista 
                                && c.DataEntrega >= ga.DataEntrega 
                                && c.DataEntrega <= ga.DataEntrega
                            select new
                            {
                                IdPecuarista = p.Id,
                                Nome = p.Nome,
                                IdCompraGado = c.Id,
                                DataEntrega = c.DataEntrega,
                                IdCompraGadoItem = ci.Id,
                                Quantidade = ci.Quantidade,
                                IdAnimal = a.Id,
                                Descricao = a.Descricao,
                                Preco = a.Preco
                            }); ;

                foreach (var l in list)
                {
                    GetAllDto g = new GetAllDto();
                    g.IdPecuarista = l.IdPecuarista;
                    g.Nome = l.Nome;
                    g.IdCompraGado = l.IdCompraGado;
                    g.DataEntrega = l.DataEntrega;
                    g.IdCompraGadoItem = l.IdCompraGadoItem;
                    g.Quantidade = (int)l.Quantidade;
                    g.IdAnimal = l.IdAnimal;
                    g.Descricao = l.Descricao;
                    g.Preco = l.Preco;

                    GetAll.Add(g);
                }
            }

            return GetAll;
        }
        #endregion

        #region Delete
        public static DelAllDto DeleteAll(DelAllDto del)
        {
            bool saved = true;
            del.Message = "";

            del = DelPecuarista(del, ref saved);
            if (saved == false) 
                return del;

            del = DelAnimal(del, ref saved);
            if (saved == false)
                return del;

            del = DelCompraGado(del, ref saved);
            if (saved == false)
                return del;

            del = DelCompraGadoItem(del, ref saved);

            return del;
        }

        private static DelAllDto DelPecuarista(DelAllDto del, ref bool saved)
        {
            using (var db = new Context())
            {
                var IdPec = db.Pecuarista.Find(del.IdPecuarista);
                var idCg = (CompraGado)db.CompraGado.Where(c => c.IdPecuarista == del.IdPecuarista);

                if (IdPec.Id >= 0 && idCg.IdPecuarista <= 0)
                {
                    db.Pecuarista.Remove(IdPec);
                    db.SaveChanges();
                    saved = true;
                }
                else
                {
                    del.Message = "Não foi possível Excluir Pecuarista. ";
                    saved = false;
                }
            }

            return del;
        }

        private static DelAllDto DelAnimal(DelAllDto del, ref bool saved)
        {
            using (var db = new Context())
            {
                var idA = db.Animal.Find(del.IdAnimal);
                var idCi = (CompraGadoItem)db.CompraGadoItem.Where(c => c.IdAnimal == del.IdAnimal);

                if (idA.Id >= 0 && idCi.IdAnimal <= 0)
                {
                    db.Animal.Remove(idA);
                    db.SaveChanges();
                }
                else
                {
                    del.Message = "Não foi possível Excluir Animal; ";
                    saved = false;
                }
            }

            return del;
        }

        private static DelAllDto DelCompraGado(DelAllDto del, ref bool saved)
        {
            using (var db = new Context())
            {
                var idC = db.Animal.Find(del.IdCompraGado);
                var idCi = (CompraGadoItem)db.CompraGadoItem.Where(c => c.IdCompraGado == del.IdCompraGado);

                if (idC.Id >= 0 && idCi.IdCompraGado <= 0)
                {
                    db.Animal.Remove(idC);
                    db.SaveChanges();
                    saved = true;
                }
                else
                {
                    del.Message = "Não foi possível Excluir CompraGrado; ";
                    saved = false;
                }
            }

            return del;
        }
                                 
        private static DelAllDto DelCompraGadoItem(DelAllDto del, ref bool saved)
        {
            using (var db = new Context())
            {
                var idCi = (CompraGadoItem)db.CompraGadoItem.Where(c => c.Id == del.IdCompraGadoItem);

                if (idCi.Id >= 0)
                {
                    db.CompraGadoItem.Remove(idCi);
                    db.SaveChanges();
                }
                else
                {
                    del.Message = "Não foi possível Excluir CompraGradoItem; ";
                    saved = false;
                }
            }

            return del;
        }


        #endregion

    }
}
