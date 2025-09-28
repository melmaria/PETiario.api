
using AutoMapper;
using EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PETiario.PETiary.Application.Owners.Dtos.Requests;
using PETiario.PETiary.Application.Owners.Dtos.Responses;
using PETiario.PETiary.Application.Owners.Services.Interfaces;
using PETiario.PETiary.Domain.Owners.Entities;

namespace PETiario.PETiary.Application.Owners.Services
{
    public class OwnerApplication(
        IUnitOfWork unitOfWork,
        ILogger<OwnerApplication> logger,
        IMapper mapper
    ) : IOwnerApplication
    {
        public async Task CreateAsync(OwnerRequest ownersRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Owner>();

                Owner owner = mapper.Map<Owner>(ownersRequest);

                await repository.InsertAsync(owner, cancellationToken);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not create a new Owner.");
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
           try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Owner can not be null.", nameof(id)); 

                var repository = unitOfWork.GetRepository<Owner>();   
                Owner owner = await repository.FindAsync(id, cancellationToken) 
                    ?? throw new KeyNotFoundException("Owner not found.");
                repository.Delete(owner);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not delete the existing owner.");
                throw;
            }
        }

        public async Task<IEnumerable<OwnerResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = unitOfWork.GetRepository<Owner>();
            IEnumerable<Owner> owners = await repository.GetAll()
                .ToListAsync(cancellationToken);
            return mapper.Map<IEnumerable<OwnerResponse>>(owners);
        }

        public async Task<OwnerResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
           if (id <= 0)
                throw new ArgumentNullException("Owner can not be null.", nameof(id));   

            var repository = unitOfWork.GetRepository<Owner>();
            Owner owner = await repository.FindAsync(id, cancellationToken) 
                ?? throw new KeyNotFoundException("owner not found.");
            return mapper.Map<OwnerResponse>(owner);
        }

        public async Task<OwnerResponse> UpdateAsync(int id, OwnerRequest ownersRequest, CancellationToken cancellationToken = default)
        {
             try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Owner can not be null.", nameof(id));

                var repository = unitOfWork.GetRepository<Owner>();
                Owner owner = await repository.FindAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException("Owner not found.");
                    
                mapper.Map(ownersRequest, owner);
                repository.Update(owner);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<OwnerResponse>(owner);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not update the existing owner.");
                throw;
            }
        }
    }
}