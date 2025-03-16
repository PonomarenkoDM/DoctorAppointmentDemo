using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Data.Interfaces;
using MyDoctorAppointment.Domain.Entities;

namespace MyDoctorAppointment.Data.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        public override string Path { get; set; }

        public override int LastId { get; set; }

        public PatientRepository()
        {
            dynamic result = ReadFromAppSettings();

            Path = result.Database.Patient.Path;
            LastId = result.Database.Patient.LastId;
        }

        public override void ShowInfo(Patient patient)
        {
            Console.WriteLine(); // implement view of all object fields
        }

        protected override void SaveLastId()
        {
            dynamic result = ReadFromAppSettings();
            result.Database.Patient.LastId = LastId;

            File.WriteAllText(Constants.AppSettingsPath, result.ToString());
        }
    }
}
