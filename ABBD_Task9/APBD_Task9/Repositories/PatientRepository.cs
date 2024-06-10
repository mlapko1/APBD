using APBD_Task9.DBContext;
using Microsoft.EntityFrameworkCore;
using APBD_Task9.DBContext;

namespace APBD_Task9.Repositories;

public interface IPatientRepository
{
    Task<Patient> GetPatientByIdAsync(int id);
    Task AddPatientAsync(Patient patient);
}

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Patient> GetPatientByIdAsync(int id)
    {
        return await _context.Patients
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Prescriptions)
            .ThenInclude(pr => pr.Doctor)
            .FirstOrDefaultAsync(p => p.IdPatient == id);
    }

    public async Task AddPatientAsync(Patient patient)
    {
        await _context.Patients.AddAsync(patient);
        await _context.SaveChangesAsync();
    }
}