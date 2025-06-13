
using MyDoctorAppointment.Data.Repositories;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Service.Interfaces;

namespace MyDoctorAppointment.Service.Strategies
{
    public class XmlDoctorStrategy : IDoctorDataStrategy
    {
        private readonly DoctorRepository _repo = new();

        public Doctor Create(Doctor doctor) => _repo.CreateXML(doctor);
        public Doctor? Get(int id) => _repo.GetByIdXML(id);
        public IEnumerable<Doctor> GetAll() => _repo.GetAllXML();
        public bool Delete(int id) => _repo.DeleteXML(id);
    }
}
