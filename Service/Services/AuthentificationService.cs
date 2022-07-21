using AutoMapper;
using Domain.Authentication;
using Domain.Entities;
using Domain.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Repository.IRepositories;
using Repository.UnitOfWork;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class AuthentificationService : IAuthentificationService
    {
        private readonly IAuthentificationRepository authentificationRepository;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public AuthentificationService(IAuthentificationRepository authentificationRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.authentificationRepository = authentificationRepository;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        public async Task<bool> Register(RegisterModel registerModel)
        {

            return await authentificationRepository.Register(registerModel);
        }

        public async Task<bool> Login(LoginModel loginModel)
        {
            var user = await authentificationRepository.Login(loginModel);

            if (user == null)
                return false;

            return true;
        }

        public async Task<bool> Logout()
        {
            return await authentificationRepository.Logout();
        }

        public async Task<bool?> InsertItems(DeclarationModel declarationModel)
        {
          //  using IDbContextTransaction transaction = unitOfWork.BeginTransaction();
            try
            {
                // Créer l'item
                Declaration declaration = mapper.Map<DeclarationModel, Declaration>(declarationModel);
                declaration.Dclaration_ID = Guid.NewGuid().ToString();
                declaration.Declaration_Date = DateTime.UtcNow;
                declaration.Declaration_Statut = "En attente";
            //    declaration.Declaration_Photo = "PhotoAPI";
                var declarationID = await authentificationRepository.InsertItems(declaration);
                return declarationID != null ? true : false;
            }
            catch (Exception)
            {
                //transaction.Rollback();
                return null;
            }
        }

        public async Task<bool> InsertIntervention(InterventionModel interventionModel)
        {
            try
            {
                // Créer l'item
                Intervention intervention = mapper.Map<InterventionModel, Intervention>(interventionModel);
                intervention.Intervention_ID = Guid.NewGuid().ToString();
                intervention.Intervention_Date = DateTime.UtcNow;
                intervention.Intervention_Resultat = "En attente";
                var interventionID = await authentificationRepository.InsertIntervention(intervention);
                return interventionID != null;
            }
            catch (Exception)
            {
                //transaction.Rollback();
                return false;
            }
        }

        public List<Declaration> GetDeclarations(string date, string validateur, string statut)
        {
            var res =  authentificationRepository.GetDeclarations(date, validateur, statut);
            //foreach(var item in res)
                //item.Declaration_Date = Convert.ToDateTime(item.Declaration_Date.Value.ToString("dd/MM/yyyy hh:mm:ss").Replace('T',' '));
            return res;

        }

        public List<InterventionModel> GetInterventions(string date, string declarationID, string equipe, string resultat)
        {
            return mapper.Map<List<Intervention>, List<InterventionModel>>( authentificationRepository.GetInterventions(date, declarationID, equipe, resultat));
        }

        public async Task<bool> ValiderDeclaration(string declarationID, string statut)
        {
            return await this.authentificationRepository.ValiderDeclaration(declarationID, statut);
        } 
        public async Task<bool> ValiderIntervention(string interventionID, string statut, string validateur)
        {
            return await this.authentificationRepository.ValiderIntervention(interventionID, statut, validateur);
        }
    }
}
