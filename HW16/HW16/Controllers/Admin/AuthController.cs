using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HW16.DataAccess;
using HW16.Models;
using HW16.Services;

namespace HW16.Controllers.Admin
{
    public class AuthController : Controller
    {
        private readonly AuthService authService;

        public AuthController(AuthService authService)
        {
            this.authService = authService;
        }

        public IActionResult Auth()
        {
            return View();
        }

        // POST: Auth/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Auth([Bind("Login,Password")] User user)
        {
            if (ModelState.IsValid)
            {
                if(await authService.AuthenticateUser(user.Login, user.Password))
                {
                    return RedirectToAction("Index", "Portfolios");
                }
            }
            return View();
        }

        


    }
}
