using BLL.Interfaces;
using BLL.Models;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class VkladService : IVkladService
    {
        private DAL.Interfaces.IUnitOfWork db;
        
        public VkladService(DAL.Interfaces.IUnitOfWork db)
        {
            this.db = db;
        }
        Vklad vklad;
        public bool MakeRecord(VkladMod vkladDto)
        {


            if (vkladDto.Id > 0)
            {
                Vklad vklad = db.Vklads.GetItem(vkladDto.Id);
                vklad.DateOpen = DateTime.Now;
                vklad.Balance = 0;

                vklad.Client_code = vkladDto.Client_code;
                vklad.Prog_code = vkladDto.Prog_code;
                db.Vklads.Update(vklad);
            }
            else
            {
                vklad = new Vklad
                {
                    Balance = 0,
                    DateOpen = DateTime.Now,
                    Client_code = vkladDto.Client_code,
                    Prog_code = vkladDto.Prog_code
                };
                db.Vklads.Create(vklad);
            }
            bool res = db.Save() > 0;
            return res;
        }
    }
}