using AutoMapper;
using EFCore.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PETiario.PETiary.Application.Appointements.Dtos.Requests;
using PETiario.PETiary.Application.Appointements.Dtos.Responses;
using PETiario.PETiary.Application.Appointements.Services.Interfaces;
using PETiario.PETiary.Domain.Appointments.Entities;

namespace PETiario.PETiary.Application.Appointements.Services
{
    public class AppointmentApplication(
        IUnitOfWork unitOfWork,
        ILogger<AppointmentApplication> logger,
        IMapper mapper) : IAppointmentApplication
    {
        public async Task CreateAsync(AppointmentRequest appointmentsRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                var repository = unitOfWork.GetRepository<Appointment>();

                Appointment appointment = mapper.Map<Appointment>(appointmentsRequest);

                await repository.InsertAsync(appointment, cancellationToken);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not create a new appointment.");
                throw;
            }
        }

        public async Task<AppointmentResponse> UpdateAsync(int id, AppointmentRequest appointmentsRequest, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Appointment can not be null.", nameof(id));

                var repository = unitOfWork.GetRepository<Appointment>();
                Appointment appointment = await repository.FindAsync(id, cancellationToken)
                    ?? throw new KeyNotFoundException("Appointment not found.");
                    
                mapper.Map(appointmentsRequest, appointment);
                repository.Update(appointment);
                await unitOfWork.SaveChangesAsync();
                return mapper.Map<AppointmentResponse>(appointment);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not update the existing appointment.");
                throw;
            }
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentNullException("Appointment can not be null.", nameof(id)); 

                var repository = unitOfWork.GetRepository<Appointment>();   
                Appointment appointment = await repository.FindAsync(id, cancellationToken) 
                    ?? throw new KeyNotFoundException("Appointment not found.");
                repository.Delete(appointment);
                await unitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Could not delete the existing appointment.");
                throw;
            }
        }

        public async Task<IEnumerable<AppointmentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var repository = unitOfWork.GetRepository<Appointment>();
            IEnumerable<Appointment> appointments = await repository.GetAll()
                .ToListAsync(cancellationToken);
            return mapper.Map<IEnumerable<AppointmentResponse>>(appointments);
        }

        public async Task<AppointmentResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            if (id <= 0)
                throw new ArgumentNullException("Appointment can not be null.", nameof(id));   

            var repository = unitOfWork.GetRepository<Appointment>();
            Appointment appointment = await repository.FindAsync(id, cancellationToken) 
                ?? throw new KeyNotFoundException("Appointment not found.");
            return mapper.Map<AppointmentResponse>(appointment);
            
        }
    }
}