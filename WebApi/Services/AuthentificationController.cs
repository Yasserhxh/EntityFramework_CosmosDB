﻿using Domain.Authentication;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository.IRepositories;
using Service.IServices;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApi.Helpers;

namespace WebApi.Services
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthentificationController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IAuthentificationService authentificationService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthentificationRepository authentificationRepository;

        public AuthentificationController(IOptions<AppSettings> appSettings, IAuthentificationService authentificationService, UserManager<ApplicationUser> userManager, IAuthentificationRepository authentificationRepository)
        {
            _appSettings = appSettings.Value;
            this.authentificationService = authentificationService;
            _userManager = userManager;
            this.authentificationRepository = authentificationRepository;
        }

        // GET: api/authentification
        [HttpGet]
        [Authorize]
        public JsonResult Get()
        {
            string message = "Bonjour dans le service d'authentification";

            return new JsonResult(message);
        }

        // POST: api/authentification/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(await this.authentificationService.Register(registerModel));
        }
        // POST: api/authentification/EditProfile


        //POST: api/authentification/login
        //End Login




        //Insert Declarations
        [HttpPost]
        [Route("InsertItems")]
        public async Task<bool?> InsertItems([FromBody] DeclarationModel declarationModel)
        {
            return await authentificationService.InsertItems(declarationModel);
        }
        //Get Declarations
        [HttpPost]
        [Route("GetDeclarations")]
        public JsonResult GetDeclarations([FromBody] FilterModel filterModel)
        {
           // var date =  declarationModel.Declaration_Date != null ? Convert.ToDateTime(declarationModel.Declaration_Date).ToString() : "";
            var date = filterModel.date ?? "";
            var res = authentificationService.GetDeclarations(date, filterModel.validateur, filterModel.statut);
            return new JsonResult(res);
        }  
        [HttpPost]
        [Route("InsertIntervention")]
        public async Task<bool> InsertIntervention([FromBody] InterventionModel interventionModel)
        {
            return await authentificationService.InsertIntervention(interventionModel);
        }
        [HttpPost]
        [Route("GetInterventions")]
        public JsonResult GetInterventions([FromBody] FilterModel filterModel)
        {
            //var query = new GetAllInterventions();
            var date = filterModel.date ?? "";
            var res = authentificationService.GetInterventions(date, filterModel.declarationID, filterModel.equipe, filterModel.resultat);
            return new JsonResult(res);
        } 
        [HttpPost]
        [Route("ValiderDeclaration")]
        public async Task<bool> ValiderDeclaration([FromBody] DeclarationModel declarationModel)
        {
            var res = await authentificationService.ValiderDeclaration(declarationModel.Dclaration_ID, declarationModel.Declaration_Statut);
            return  res;
        }
        [HttpPost]
        [Route("ValiderIntervention")]
        public async Task<bool> ValiderIntervention([FromBody] FilterModel filterModel)
        {
            var res = await authentificationService.ValiderIntervention(filterModel.interventionID, filterModel.statut, filterModel.validateur);
            return  res;
        }
    }
}
