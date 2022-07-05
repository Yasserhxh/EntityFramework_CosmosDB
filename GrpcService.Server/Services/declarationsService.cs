using Grpc.Core;
using GrpcService.Server.Protos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcService.Server.Services
{
    public class DeclarationsServices : DeclarationService.DeclarationServiceBase
    {
        public override Task<Declarations> GetDeclarations(Empty request, ServerCallContext context)
        {
            return base.GetDeclarations(request, context);
        }
    }
}
