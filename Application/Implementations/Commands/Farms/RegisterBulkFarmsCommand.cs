using Application.Abstractions;
using Application.DTOs.Farms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Commands.Farms;
public sealed record RegisterBulkFarmsCommand(List<CreateFarmRequestModel> farmRequests) : ICommand<IList<string>>;
