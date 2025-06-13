
using MyDoctorAppointment.Service.Interfaces;
using MyDoctorAppointment.Domain.Entities;
using MyDoctorAppointment.Domain.Enums;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Service.Strategies;

namespace MyDoctorAppointment.Service.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorDataStrategy _strategy;

        public DoctorService(BaseTypes baseType)
        {
            _strategy = baseType switch
            {
                BaseTypes.Json => new JsonDoctorStrategy(),
                BaseTypes.XML => new XmlDoctorStrategy(),
                _ => throw new NotSupportedException("Unknown storage type")
            };
        }

        public Doctor Create(Doctor doctor) => _strategy.Create(doctor);
        public bool Delete(int id) => _strategy.Delete(id);
        public Doctor? Get(int id) => _strategy.Get(id);
        public IEnumerable<Doctor> GetAll() => _strategy.GetAll();

        public Doctor Update(int id, Doctor doctor)
        {
            var existing = _strategy.Get(id);
            if (existing == null)
                return new Doctor();

            doctor.Id = id;
            Delete(id);
            return Create(doctor);
        }
    }
}
