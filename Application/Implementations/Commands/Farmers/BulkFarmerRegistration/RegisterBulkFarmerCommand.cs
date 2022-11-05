using Application.Abstractions;
using Application.Wrapper;
using Application.Implementations.Commands;
using Application.DTOs.Farmers;

namespace Application.Implementations.Commands.Farmers.BulkFarmerRegistration;

public sealed record RegisterBulkFarmerCommand(List<CreateFarmerRequestModel> farmerrRequests)
: ICommand<List<FarmerResponseModel>>;
