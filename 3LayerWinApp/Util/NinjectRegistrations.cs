using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using BLL;

namespace _3LayerWinApp.Util
{
    class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IVkladService>().To<VkladService>();
            Bind<IReportService>().To<ReportServices>();
            Bind<IDbCrud>().To<DbDataOperation>();
        }
    }
}
