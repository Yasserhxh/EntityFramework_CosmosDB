using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Domain.Authentication;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Repository.Data;
using Repository.UnitOfWork;
using Domain.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class AuthentificationRepository : IAuthentificationRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        //private readonly ApplicationDbContext _dbConctext;
        private readonly IUnitOfWork unitOfWork;
        private readonly ApplicationDbContext _dbContext = new ApplicationDbContext();
        public AuthentificationRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,/* ApplicationDbContext dbConctext,*/ IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
           // _dbConctext = dbConctext;
            this.unitOfWork = unitOfWork;
        }

        public async Task<ApplicationUser> Login(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, lockoutOnFailure: true);
                if (result.Succeeded)
                    return user;
                else
                    return null;
            }
            //return user;
            return null;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {
            var user = new ApplicationUser { UserName = registerModel.UserName, Email = registerModel.Email };
            var result = await _userManager.CreateAsync(user, registerModel.Password);
            if (result.Succeeded)
                return true;
            else
                return false;
        }

        public async Task<bool> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<string> InsertItems(Declaration declaration)
        {
           /* var dec = new Declaration()
            {
                Dclaration_ID = Guid.NewGuid().ToString(),
                Declaration_Adresse = "Ain Sebaa",
                Declaration_Date = DateTime.Now,
                Declaration_DateValidation = DateTime.Now

            };
            var interv = new Intervention()
            {
                Intervention_ID = Guid.NewGuid().ToString(),
                Intervention_DeclarationID = "db30deeb-a8f8-45ab-9204-414379307955",
                Intervention_Date = DateTime.Now,
                Intervention_Equipe = "Equipe A",
                Intervention_Commentaire = "Commentaire",
                Intervention_Resultat = "Pass"
            };*/
           
            _dbContext.declarations.Add(declaration);
            var confirm = await _dbContext.SaveChangesAsync();
            return confirm > 0 ? declaration.Dclaration_ID : null;

        }

        public async Task<List<Declaration>> GetDeclarations(string date, string validateur)
        {
            var query = _dbContext.declarations.Where(d => d.Declaration_Statut == "En attente");
            if (string.IsNullOrEmpty(date))
                query.Where(d => Convert.ToDateTime(d.Declaration_Date).ToString("dd/MM/yyyy") == date);
            if (string.IsNullOrEmpty(validateur))
                query.Where(d => d.Declaration_Validateur == validateur);
            return await query.ToListAsync();
        }

        public async Task<string> InsertIntervention(Intervention intervention)
        {
            _dbContext.Interventions.Add(intervention);
            var confirm = await _dbContext.SaveChangesAsync();
            return confirm > 0 ? intervention.Intervention_ID : null;
        }

        public async Task<List<Intervention>> GetInterventions(string date, string declarationID, string equipe, string resultat)
        {
            var query = _dbContext.Interventions.Where(d => d.Intervention_DeclarationID == declarationID);
            if (string.IsNullOrEmpty(date))
                query.Where(d => Convert.ToDateTime(d.Intervention_Date).ToString("dd/MM/yyyy") == date);
            if (string.IsNullOrEmpty(equipe))
                query.Where(d => d.Intervention_Equipe == equipe);
            if (string.IsNullOrEmpty(resultat))
                query.Where(d => d.Intervention_Resultat == resultat);
            return await query.ToListAsync();
        }
    }
}
