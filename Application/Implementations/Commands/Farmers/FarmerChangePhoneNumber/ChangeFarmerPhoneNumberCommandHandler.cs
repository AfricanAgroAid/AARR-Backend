using Application.Abstractions.Messaging;
using Application.Interfaces.Repositories;
using Domain.Entities;
using Application.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapster;

namespace Application.Implementations.Commands.Farmer.FarmerChangePhoneNumber
{
    internal class ChangeFarmerPhoneNumberCommandHandler : ICommandHandler<ChangeFarmerPhoneNumberCommand, FarmerResponseModel>
    {
        private readonly IFarmerRepository _farmerRepository;


        public ChangeFarmerPhoneNumberCommandHandler(IFarmerRepository farmerRepository)
        {
            _farmerRepository = farmerRepository;

        }
        public async Task<Result<FarmerResponseModel>> Handle(ChangeFarmerPhoneNumberCommand request, CancellationToken cancellationToken)
        {

            var farmerExists = await _farmerRepository.GetAsync(farmer => farmer.PhoneNumber == request.PhoneNumber);


            if (farmerExists is null) return await Result<FarmerResponseModel>.FailAsync($"Our records shows no farmer with phone number:{request.PhoneNumber}");
            farmerExists = farmerExists.ChangePhoneNumber(request.PhoneNumber);

            var farmerReturned = _farmerRepository.Update(farmerExists);
            var farmerResponse = farmerReturned.Adapt<FarmerResponseModel>();
            return await Result<FarmerResponseModel>.SuccessAsync(farmerResponse, "Farmer PhoneNumber is Updated Successfully");
        }


    }
}