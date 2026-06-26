using System;
using Microsoft.AspNetCore.Mvc;

namespace eAgendaWeb.WebApplication.Compartilhado.Apresentacao;

public class HomeController : Controller
{
    // GET: HomeController
    public ActionResult Index()
    {
        return View();
    }

}