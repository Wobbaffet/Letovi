﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LetWebApp.Controllers
{
    public class RezervacijaController : Controller
    {
        public IActionResult Reservation()
        {
            return View();
        }
    }
}
