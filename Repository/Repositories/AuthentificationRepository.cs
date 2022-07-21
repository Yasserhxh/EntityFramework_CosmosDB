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
using System.Globalization;

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

        public  List<Declaration> GetDeclarations(string date, string validateur, string statut)
        {
            CultureInfo provider = new CultureInfo("en-US");
            string[] validformats = new[] { "MM/dd/yyyy", "yyyy/MM/dd", "MM/dd/yyyy HH:mm:ss",
                                        "MM/dd/yyyy hh:mm tt", "yyyy-MM-dd HH:mm:ss, fff" };

            var query =  _dbContext.declarations.OrderByDescending(p=>p.Declaration_Date).AsEnumerable();//
            if (!string.IsNullOrEmpty(statut))
                query = query.Where(d => d.Declaration_Statut == statut);
            else
                query = query.Where(d => d.Declaration_Statut == "En attente");

            if (!string.IsNullOrEmpty(date))
            {
               // DateTime dateTime = DateTime.ParseExact(date, validformats, provider);
               //var datetime = DateTime.ParseExact(@date, "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                query = query.Where(d => d.Declaration_Date.Value.ToString("M/dd/yyyy") == date); 
                //query = query.Where(d => d.Declaration_Date.Value.Date == DateTime.UtcNow.Date);

            }
            else
                //query = query.Where(d => d.Declaration_Date.Value.Date == DateTime.UtcNow.Date);

            if (!string.IsNullOrEmpty(validateur))
                query = query.Where(d => d.Declaration_Validateur == validateur);
            return query == null ? new List<Declaration>() : query.ToList();
           
            /*var res = ;
            return res;*/
        }
        
        public async Task<string> InsertIntervention(Intervention intervention)
        {
            _dbContext.Interventions.Add(intervention);
            var confirm = await _dbContext.SaveChangesAsync();
            return confirm > 0 ? intervention.Intervention_ID : null;
        }

        public List<Intervention> GetInterventions(string date, string declarationID, string equipe, string resultat)
        {
            var query = _dbContext.Interventions.AsQueryable();//.Where(d => d.Intervention_DeclarationID == declarationID);////
            if (!string.IsNullOrEmpty(date))
                query = query.Where(d => Convert.ToDateTime(d.Intervention_Date).ToString("dd/MM/yyyy") == date);
            //else
                //query.Where(d => Convert.ToDateTime(d.Intervention_Date).ToString("dd/MM/yyyy") == DateTime.UtcNow.ToString("dd/MM/yyyy"));
            if (!string.IsNullOrEmpty(equipe))
                query = query.Where(d => d.Intervention_Equipe == equipe);
            if (!string.IsNullOrEmpty(resultat))
                query = query.Where(d => d.Intervention_Resultat == resultat);
            return  query.ToList();
        }
        public async Task<bool> ValiderDeclaration(string declarationID, string statut)
        {
            var dec = _dbContext.declarations.Where(p => p.Dclaration_ID == declarationID).FirstOrDefault();
            if (dec != null)
            {
                dec.Declaration_Statut = statut;
                _dbContext.Entry(dec).State = EntityState.Modified;

            }
            var confirm = await _dbContext.SaveChangesAsync();
            return confirm > 0;
        }
        public async Task<bool> ValiderIntervention(string interventionID, string statut, string validateur)
        {
            var intervention = _dbContext.Interventions.Where(p => p.Intervention_ID == interventionID).FirstOrDefault();
            if (intervention != null)
            {
                intervention.Intervention_Resultat = statut;
                _dbContext.Entry(intervention).State = EntityState.Modified;
                var dec = _dbContext.declarations.Where(p => p.Dclaration_ID == intervention.Intervention_DeclarationID).FirstOrDefault();
                if (dec != null)
                {
                    dec.Declaration_Validateur = validateur;
                    dec.Declaration_Statut = statut;
                    _dbContext.Entry(dec).State = EntityState.Modified;

                }
            }
            
            var confirm = await _dbContext.SaveChangesAsync();
            return confirm > 0;
        }
    }
}
