using STO.Data.Context;
using STO.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace STO.Presentation.Web.Controllers
{
    public class BaseController : Controller
    {
        protected static SiparisContext _dbContext = new SiparisContext();
        protected IUnitOfWork _uow = new EFUnitOfWork(_dbContext);
        
    }
}