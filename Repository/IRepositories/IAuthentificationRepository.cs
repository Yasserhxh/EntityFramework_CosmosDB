using Domain.Authentication;
using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IAuthentificationRepository
    {
        Task<bool> Register(RegisterModel userModel);
        Task<ApplicationUser> Login(LoginModel loginModel);
        Task<bool> Logout();
        Task<string> InsertItems(Declaration declaration);
        Task<string> InsertIntervention(Intervention intervention);
        List<Declaration> GetDeclarations(string date, string validateur, string statut);
        List<Intervention> GetInterventions(string date, string declarationID, string equipe, string resultat);
        Task<bool> ValiderDeclaration(string declarationID, string statut);
    }
}
