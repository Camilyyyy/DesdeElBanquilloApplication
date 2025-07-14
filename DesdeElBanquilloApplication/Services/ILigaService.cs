﻿using DesdeElBanquilloApplication.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesdeElBanquilloApplication.Services
{
    public interface ILigaService
    {
        Task<IEnumerable<LeagueViewModel>> GetAllAsync();
        Task<LeagueViewModel?> GetByIdAsync(int id);
        Task CreateAsync(LeagueViewModel vm);
        Task UpdateAsync(LeagueViewModel vm);
        Task DeleteAsync(int id);
        Task<IEnumerable<SelectListItem>> GetCountriesAsync();
    }
}