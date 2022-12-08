using Application.Abstractions;
using Application.DTOs.Farms;


namespace Application.Implementations.Commands.Farms;
public sealed record RegisterBulkFarmsCommand(List<CreateFarmRequestModel> farmRequests) : ICommand<IList<string>>;
