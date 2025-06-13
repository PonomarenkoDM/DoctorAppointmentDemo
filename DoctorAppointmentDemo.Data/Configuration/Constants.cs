
using System;
using System.IO;

namespace MyDoctorAppointment.Data.Configuration
{
    public static class Constants
    {
        private static readonly string BaseDir = AppDomain.CurrentDomain.BaseDirectory;

        public static readonly string AppSettingsPath = Path.Combine(
            BaseDir,
            "appsettings.json"
        );

        public static readonly string DoctorJsonPath = Path.Combine(
            BaseDir,
            "doctors.json"
        );
    }
}
