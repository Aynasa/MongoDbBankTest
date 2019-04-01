using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using _3LayerWinApp;
using _3LayerWinApp.Util;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL;
using Ninject;

namespace _4lab
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // внедрение зависимостей
            var kernel = new StandardKernel(new NinjectRegistrations(), new ServiceModule("dbConnection"));

            IDbCrud crudServ = kernel.Get<IDbCrud>();
            IVkladService vkladServ = kernel.Get<IVkladService>();
            IReportService reportServ = kernel.Get<IReportService>();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(crudServ, vkladServ, reportServ));
        }
    }
}
