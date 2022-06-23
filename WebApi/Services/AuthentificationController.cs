using Domain.Authentication;
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
        public async Task<bool> InsertItems([FromBody] DeclarationModel declarationModel)
        {
            return await authentificationService.InsertItems(declarationModel);
        }
        //Get Declarations
        [HttpGet]
        [Route("GetDeclarations")]
        public async Task<JsonResult> GetDeclarations([FromBody] DeclarationModel declarationModel)
        {
            var date = Convert.ToDateTime(declarationModel.Declaration_Date).ToString("dd/MM/yyyy");
            var res = await authentificationService.GetDeclarations(date, declarationModel.Declaration_Validateur, declarationModel.Declaration_Statut);
            return new JsonResult(res);
        }

    }
}
