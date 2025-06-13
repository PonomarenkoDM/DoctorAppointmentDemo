
using System.IO;

namespace MyDoctorAppointment.Data.Configuration
{
    public static class Constants
    {
        public static readonly string AppSettingsPath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "DoctorAppointmentDemo.Data",
            "Configuration",
            "appsettings.json"
        );
    }
}
