using Backend_Web.DAL.DAO_s;
using Backend_Web.Models;
using Backend_Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend_Web.Controllers
{
    public class HistoryController : CRUDController<History, HistoryService, HistoryDAO>
    {
    }
}