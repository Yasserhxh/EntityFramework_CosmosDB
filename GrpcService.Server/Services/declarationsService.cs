using Grpc.Core;
using GrpcService.Server.Protos;
using Microsoft.Extensions.Logging;
using Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrpcService.Server.Services
{
    public class DeclarationsServices : DeclarationService.DeclarationServiceBase
    {
        private readonly IAuthentificationService _authentificationService;

        public DeclarationsServices(IAuthentificationService authentificationService)
        {
            _authentificationService = authentificationService;
        }
        public override Task<Declarations> GetDeclarations(Empty request, ServerCallContext context)
        {
            var res = _authentificationService.GetDeclarations(null, null, null);
            Declarations response = new();
            foreach(var item in res)
            {
                Declaration dec = new()
                {
                    DeclarationID = item.Dclaration_ID,
                    DeclarationAdresse =item.Declaration_Adresse,
                    DeclarationDate = item.Declaration_Date.ToString(),
                    DeclarationLocalisation = item.Declaration_Localisation,
                    DeclarationStatut = item.Declaration_Statut,
                    DeclarationValidateur = item.Declaration_Validateur,
                    DeclarationVille = item.Declaration_Ville
                };
                response.Items.Add(dec);
            }
            //var resp = response.Items;
            //var resp = JsonSerializer.Deserialize<string>(response.Items.ToString());
            return Task.FromResult(response); 
        }
    }
}
