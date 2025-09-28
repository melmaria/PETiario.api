
using AutoMapper;
using EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PETiario.PETiary.Application.Walks.Dtos.Requests;
using PETiario.PETiary.Application.Walks.Dtos.Responses;
using PETiario.PETiary.Application.Walks.Services.Interfaces;
using PETiario.PETiary.Domain.Walks.Entities;

namespace PETiario.PETiary.Application.Walks.Services
{
    public class WalkApplication(
        IUnitOfWork unitOfWork,
        ILogger<WalkApplication> logger,
        IMapper mapper 
    ) : IWalkApplication 
    {
        public async Task CreateAsync(WalkRequest walksRequest, CancellationToken cancellationToken = default)
        {
             try
            {
                var repository = unitOfWork.GetRepository<Walk>();

                Walk walk = mapper.Map<Walk>(walksRequest);

                await repository.InsertAsync(walk, cancellationToken);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not create a new walk.");
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
             try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Walk can not be null.", nameof(id)); 

                var repository = unitOfWork.GetRepository<Walk>();   
                Walk walk = await repository.FindAsync(id, cancellationToken) 
                    ?? throw new KeyNotFoundException("Walk not found.");
                repository.Delete(walk);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not delete the existing walk.");
                throw;
            }
        }

        public async Task<IEnumerable<WalkResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = unitOfWork.GetRepository<Walk>();
            IEnumerable<Walk> walk = await repository.GetAll()
                .ToListAsync(cancellationToken);
            return mapper.Map<IEnumerable<WalkResponse>>(walk);
        }

        public async Task<WalkResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentNullException("Walk can not be null.", nameof(id));   

            var repository = unitOfWork.GetRepository<Walk>();
            Walk walk = await repository.FindAsync(id, cancellationToken) 
                ?? throw new KeyNotFoundException("Walk not found.");
            return mapper.Map<WalkResponse>(walk);
        }

        public async Task<WalkResponse> UpdateAsync(int id, WalkRequest walksRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Walk can not be null.", nameof(id));

                var repository = unitOfWork.GetRepository<Walk>();
                Walk walk = await repository.FindAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException("Walk not found.");
                    
                mapper.Map(walksRequest, walk);
                repository.Update(walk);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<WalkResponse>(walk);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not update the existing walk.");
                throw;
            }
        }
    }
}