
using AutoMapper;
using EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PETiario.PETiary.Application.Pets.Dtos.Requests;
using PETiario.PETiary.Application.Pets.Dtos.Responses;
using PETiario.PETiary.Application.Pets.Services.Interfaces;
using PETiario.PETiary.Domain.Pets.Entities;

namespace PETiario.PETiary.Application.Pets.Services
{
    public class PetApplication(
        IUnitOfWork unitOfWork,
        ILogger<PetApplication> logger,
        IMapper mapper
    ) : IPetApplication
    {
        public async Task CreateAsync(PetRequest petsRequest, CancellationToken cancellationToken = default)
        {
             try
            {
                var repository = unitOfWork.GetRepository<Pet>();

                Pet pet = mapper.Map<Pet>(petsRequest);

                await repository.InsertAsync(pet, cancellationToken);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not create a new pet.");
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Pet can not be null.", nameof(id)); 

                var repository = unitOfWork.GetRepository<Pet>();   
                Pet pet = await repository.FindAsync(id, cancellationToken) 
                    ?? throw new KeyNotFoundException("Pet not found.");
                repository.Delete(pet);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not delete the existing pet.");
                throw;
            }
        }

        public async Task<IEnumerable<PetResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = unitOfWork.GetRepository<Pet>();
            IEnumerable<Pet> pet = await repository.GetAll()
                .ToListAsync(cancellationToken);
            return mapper.Map<IEnumerable<PetResponse>>(pet);
        }

        public async Task<PetResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentNullException("Pet can not be null.", nameof(id));   

            var repository = unitOfWork.GetRepository<Pet>();
            Pet pet = await repository.FindAsync(id, cancellationToken) 
                ?? throw new KeyNotFoundException("Pet not found.");
            return mapper.Map<PetResponse>(pet);
            
        }

        public async Task<PetResponse> UpdateAsync(int id, PetRequest petsRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Pet can not be null.", nameof(id));

                var repository = unitOfWork.GetRepository<Pet>();
                Pet pet = await repository.FindAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException("Pet not found.");
                    
                mapper.Map(petsRequest, pet);
                repository.Update(pet);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<PetResponse>(pet);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not update the existing pet.");
                throw;
            }
        }
    }
}