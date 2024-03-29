﻿using BiznisLogika.Exceptions;
using BiznisLogika.Interfejsi;
using Data.UnitOfWork;
using Domen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiznisLogika.Klase
{
    public class RezervacijaServis : IRezervacijaServis
    {
        public RezervacijaServis(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        private IUnitOfWork uow { get; set; }
        public void Add(Rezervacija t)
        {
            var let = uow.RepositoryLet.Find(l => l.LetId == t.LetId);
            if (let.BrojMesta - t.BrojMesta < 0)
                throw new ReservationException("Nema dovoljno mesta !");
           
            uow.RepositoryRezervacija.Add(t);
            let.BrojMesta -= t.BrojMesta;
          
            uow.Commit();
        }

        public void CheckDate(int id)
        {
            var let = uow.RepositoryLet.Find(l => l.LetId == id);

            if (DateTime.Now.AddDays(3) > let.Datum)
                throw new DateException("Let je za manje od 3 dana");
        }

        public List<Rezervacija> GetAll()
        {
            return uow.RepositoryRezervacija.FindAll();
        }

        public List<Rezervacija> GetByCondition(Predicate<Rezervacija> condition)
        {
            return uow.RepositoryRezervacija.GetByCondition(condition);
        }

        public void ChangeStatus(int letId, int korisnikId)
        {
            var rezervacija = uow.RepositoryRezervacija.Find(r => r.LetId == letId && r.KorisnikId == korisnikId);
            rezervacija.StatusLeta = StatusLeta.Odobren;
            uow.Commit();
        }

        public Rezervacija Find(Predicate<Rezervacija> condition)
        {
            return uow.RepositoryRezervacija.Find(condition);
        }

        public void Delete(Rezervacija t)
        {
            throw new NotImplementedException();
        }
    }
}
