
using AutoMapper;
using EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PETiario.PETiary.Application.GroomingBaths.Dtos.Requests;
using PETiario.PETiary.Application.GroomingBaths.Dtos.Responses;
using PETiario.PETiary.Application.GroomingBaths.Services.Interfaces;
using PETiario.PETiary.Domain.GroomingBaths.Entities;

namespace PETiario.PETiary.Application.GroomingBaths.Services
{
    public class GroomingBathApplication(
        IUnitOfWork unitOfWork,
        ILogger<GroomingBathApplication> logger,
        IMapper mapper) : IGroomingBathApplication
    {
        public async Task CreateAsync(GroomingBathRequest groomingBathsRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var repository = unitOfWork.GetRepository<GroomingBath>();

                GroomingBath groomingBath = mapper.Map<GroomingBath>(groomingBathsRequest);

                await repository.InsertAsync(groomingBath, cancellationToken);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not create a new Grooming Bath.");
                throw;
            }
        }

        public async Task<GroomingBathResponse> UpdateAsync(int id, GroomingBathRequest groomingBathsRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Grooming Bath can not be null.", nameof(id));

                var repository = unitOfWork.GetRepository<GroomingBath>();
                GroomingBath groomingBath = await repository.FindAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException("Grooming Bath not found.");
                    
                mapper.Map(groomingBathsRequest, groomingBath);
                repository.Update(groomingBath);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<GroomingBathResponse>(groomingBath);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not update the existing Grooming Bath.");
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Grooming Bath can not be null.", nameof(id)); 

                var repository = unitOfWork.GetRepository<GroomingBath>();   
                GroomingBath groomingBath = await repository.FindAsync(id, cancellationToken) 
                    ?? throw new KeyNotFoundException("Grooming Bath not found.");
                repository.Delete(groomingBath);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not delete the existing Grooming Bath.");
                throw;
            }
        }

        public async Task<IEnumerable<GroomingBathResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = unitOfWork.GetRepository<GroomingBath>();
            IEnumerable<GroomingBath> groomingBaths = await repository.GetAll()
                .ToListAsync(cancellationToken);
            return mapper.Map<IEnumerable<GroomingBathResponse>>(groomingBaths);
        }

        public async Task<GroomingBathResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentNullException("Grooming Bath can not be null.", nameof(id));   

            var repository = unitOfWork.GetRepository<GroomingBath>();
            GroomingBath groomingBath = await repository.FindAsync(id, cancellationToken) 
                ?? throw new KeyNotFoundException("Grooming Bath not found.");
            return mapper.Map<GroomingBathResponse>(groomingBath);
            
        }
    }
}