﻿using Microsoft.AspNetCore.Mvc;
using MvcNetCoreUtilidades.Repositories;
using MvcNetCoreUtilidades.Models;

namespace MvcNetCoreUtilidades.ViewComponents
{
    public class MenuCochesViewComponent: ViewComponent
    {
        private RepositoryCoches repo;

        public MenuCochesViewComponent(RepositoryCoches repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Coche> coches = this.repo.GetCoches();
            return View(coches);
        }
    }
}
