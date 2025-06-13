
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;

namespace MyDoctorAppointment.Service.Strategies
{
    public class JsonDoctorStrategy : IDoctorDataStrategy
    {
        private readonly GenericRepository<Doctor> _repo = new DoctorRepository();

        public Doctor Create(Doctor doctor) => _repo.Create(doctor);
        public Doctor? Get(int id) => _repo.GetById(id);
        public IEnumerable<Doctor> GetAll() => _repo.GetAll();
        public bool Delete(int id) => _repo.Delete(id);
    }
}
