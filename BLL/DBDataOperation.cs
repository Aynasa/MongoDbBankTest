﻿using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models;

namespace BLL
{
    public class DbDataOperation : IDbCrud
    {
        IUnitOfWork db;
        public DbDataOperation(IUnitOfWork repos)
        {
            db = repos;
        }

        ObservableCollection<Client> l;

        //public ObservableCollection<PhoneModel> All {get {return l;} }

        public List<ClientMod> GetAllClients()
        {
            l = db.Clients.GetList();
            return l.Select(i => toClientMod(i)).ToList();
        }

        public List<VkladMod> GetAllVklads()
        {
            return db.Vklads.GetList().Select(i => toVkladMod(i)).ToList();
        }

        public ClientMod GetClient(int Id)
        {
            return toClientMod(db.Clients.GetItem(Id));
        }

        public VkladMod GetVklad(int Id)
        {
            return toVkladMod(db.Vklads.GetItem(Id));
        }

        public void CreateVklad(VkladMod p)
        {
            db.Vklads.Create(toVklad(p, new Vklad()));
            db.Save();
            GetAllVklads();
        }

        public void UpdateVklad(VkladMod p)
        {
            Vklad ph = db.Vklads.GetItem(p.Id);
            db.Vklads.Update(toVklad(p, ph));
            db.Save();
        }

        public void DeleteClient(int id)
        {
            Client p = db.Clients.GetItem(id);
            if (p != null)
            {
                db.Clients.Delete(id);
                db.Save();
            }
        }

        public void DeleteVklad(int id)
        {
            Vklad o = db.Vklads.GetItem(id);
            if (o != null)
                db.Vklads.Delete(id);
            db.Save();
        }

        public List<ProgMod> GetProgs()
        {
            return db.Progs.GetList().Select(i => new ProgMod { Id = i.Id, Name = i.name, Percent = i.percent }).ToList();
        }

        private VkladMod toVkladMod(Vklad i)
        {
            return new VkladMod()
            {
                Id = i.Id,
                Balance = i.Balance,
                Client_code = i.Client_code,
                DateOpen = i.DateOpen,
                Prog_code = i.Prog_code
            };
        }
        private ClientMod toClientMod(Client i)
        {
            return new ClientMod() { Id = i.Id, FIO = i.FIO, Passport = i.passport };
        }
        private Vklad toVklad(VkladMod i, Vklad o)
        {
            o.Id = i.Id;
            o.Balance = i.Balance;
            o.Client_code = i.Client_code;
            o.DateOpen = i.DateOpen;
            o.Prog_code = i.Prog_code;

            return o;
        }
        private Client toClient(ClientMod i, Client p)
        {
            p.Id = i.Id;
            p.FIO = i.FIO;
            p.passport = i.Passport;
         
            return p;
        }

    }
}
