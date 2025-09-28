
using AutoMapper;
using EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PETiario.PETiary.Application.Vaccinations.Dtos.Requests;
using PETiario.PETiary.Application.Vaccinations.Dtos.Responses;
using PETiario.PETiary.Application.Vaccinations.Services.Interfaces;
using PETiario.PETiary.Domain.Vaccinations.Entities;

namespace PETiario.PETiary.Application.Vaccinations.Services
{
    public class VaccinationApplication(
        IUnitOfWork unitOfWork,
        ILogger<VaccinationApplication> logger,
        IMapper mapper
    ) : IVaccinationApplication
    {
        public async Task CreateAsync(VaccionationRequest vaccionationsRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Vaccination>();

                Vaccination vaccination = mapper.Map<Vaccination>(vaccionationsRequest);

                await repository.InsertAsync(vaccination, cancellationToken);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not create a new vaccination.");
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Vaccionation can not be null.", nameof(id)); 

                var repository = unitOfWork.GetRepository<Vaccination>();   
                Vaccination vaccination = await repository.FindAsync(id, cancellationToken) 
                    ?? throw new KeyNotFoundException("Vaccination not found.");
                repository.Delete(vaccination);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not delete the existing vaccination.");
                throw;
            }
        }

        public async Task<IEnumerable<VaccinationResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = unitOfWork.GetRepository<Vaccination>();
            IEnumerable<Vaccination> vaccination = await repository.GetAll()
                .ToListAsync(cancellationToken);
            return mapper.Map<IEnumerable<VaccinationResponse>>(vaccination);
        }

        public async Task<VaccinationResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
           if (id <= 0)
                throw new ArgumentNullException("Vaccionation can not be null.", nameof(id));   

            var repository = unitOfWork.GetRepository<Vaccination>();
            Vaccination vaccination = await repository.FindAsync(id, cancellationToken) 
                ?? throw new KeyNotFoundException("Pet not found.");
            return mapper.Map<VaccinationResponse>(vaccination);
        }

        public async Task<VaccinationResponse> UpdateAsync(int id, VaccionationRequest vaccionationsRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Vaccionation can not be null.", nameof(id));

                var repository = unitOfWork.GetRepository<Vaccination>();
                Vaccination vaccination = await repository.FindAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException("Vaccination not found.");
                    
                mapper.Map(vaccionationsRequest, vaccination);
                repository.Update(vaccination);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<VaccinationResponse>(vaccination);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not update the existing vaccionation.");
                throw;
            }
        }
    }
}