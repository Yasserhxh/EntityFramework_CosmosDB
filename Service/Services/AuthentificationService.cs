﻿using AutoMapper;
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

        public async Task<bool> InsertItems(DeclarationModel declarationModel)
        {
          //  using IDbContextTransaction transaction = unitOfWork.BeginTransaction();
            try
            {
                // Créer l'item
                Declaration declaration = mapper.Map<DeclarationModel, Declaration>(declarationModel);
                declaration.Dclaration_ID = Guid.NewGuid().ToString();
                declaration.Declaration_Date = DateTime.Now;
                declaration.Declaration_Statut = "En attente";
                var declarationID = await authentificationRepository.InsertItems(declaration);
                return declarationID == null;
            }
            catch (Exception)
            {
                //transaction.Rollback();
                return false;
            }
        }
    }
}
