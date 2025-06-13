
using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Service.Interfaces
{
    public interface IDoctorDataStrategy
    {
        Doctor Create(Doctor doctor);
        Doctor? Get(int id);
        IEnumerable<Doctor> GetAll();
        bool Delete(int id);
    }
}
