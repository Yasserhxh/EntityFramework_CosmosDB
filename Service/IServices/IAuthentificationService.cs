﻿using Domain.Entities;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.IServices
{
    public interface IAuthentificationService
    {
        Task<bool> Register(RegisterModel userModel);
        Task<bool> Login(LoginModel loginModel);
        Task<bool> Logout();
        Task<bool> InsertItems(DeclarationModel declarationModel);
        Task<bool> InsertIntervention(InterventionModel interventionModel);
        Task<List<Declaration>> GetDeclarations(string date, string validateur, string statut);
        Task<List<InterventionModel>> GetInterventions(string date, string declarationID, string equipe, string resultat);
    }
}