
using MyDoctorAppointment.Data.Configuration;
using MyDoctorAppointment.Domain.Entities;
using System.Xml.Serialization;

namespace MyDoctorAppointment.Data.Repositories
{
    public class DoctorRepository : GenericRepository<Doctor>
    {
        public override string Path { get; set; }
        public override int LastId { get; set; }

        private string XmlPath => Path.Replace("json", "xml");

        public DoctorRepository()
        {
            Path = Constants.DoctorJsonPath;
            LastId = ReadLastId();
        }

        public Doctor CreateXML(Doctor doctor)
        {
            var list = GetAllXML().ToList();
            doctor.Id = list.Any() ? list.Max(x => x.Id) + 1 : 1;
            doctor.CreatedAt = DateTime.Now;
            list.Add(doctor);
            SaveToXml(list);
            return doctor;
        }

        public IEnumerable<Doctor> GetAllXML()
        {
            if (!File.Exists(XmlPath)) return new List<Doctor>();
            using var reader = new StreamReader(XmlPath);
            return (List<Doctor>)new XmlSerializer(typeof(List<Doctor>)).Deserialize(reader);
        }

        public Doctor? GetByIdXML(int id)
        {
            return GetAllXML().FirstOrDefault(x => x.Id == id);
        }

        public bool DeleteXML(int id)
        {
            var list = GetAllXML().ToList();
            var toRemove = list.FirstOrDefault(x => x.Id == id);
            if (toRemove == null) return false;
            list.Remove(toRemove);
            SaveToXml(list);
            return true;
        }

        private void SaveToXml(List<Doctor> doctors)
        {
            using var writer = new StreamWriter(XmlPath);
            new XmlSerializer(typeof(List<Doctor>)).Serialize(writer, doctors);
        }

        private int ReadLastId()
        {
            if (!File.Exists(Path)) return 0;
            var list = GetAll();
            return list.Any() ? list.Max(x => x.Id) : 0;
        }

        protected override void SaveLastId()
        {
            // Збереження LastId можна реалізувати при потребі
        }

        public override void ShowInfo(Doctor source)
        {
            Console.WriteLine($"{source.Id}: {source.Name} {source.Surname}");
        }
    }
}
