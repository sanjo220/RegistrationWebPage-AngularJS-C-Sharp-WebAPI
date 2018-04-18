namespace RegistrationWebAPI.Controllers
{
    using System;
    using System.Configuration;
    using System.IO;
    using System.Web.Http;
    using Models;
    using Newtonsoft.Json;

    public class RegistrationController : ApiController
    {
        private const string REG_DATA_PATH_CONFIG_NAME = "RegistrationDataPath";
        private const string REG_DATA_FILENAME_CONFIG_NAME = "RegistrationDataFileName";

        // POST api/Registration
        public string Post([FromBody]RegistrationDetail registrationDetail)
        {
            // Mock saving to database.
            // Just save the object to a json file.
            var reader = new AppSettingsReader();
            var registrationDetailJson = JsonConvert.SerializeObject(registrationDetail);
            var path = Convert.ToString(reader.GetValue(REG_DATA_PATH_CONFIG_NAME, typeof(String)));
            var filename = Convert.ToString(reader.GetValue(REG_DATA_FILENAME_CONFIG_NAME, typeof(String)));
            var fullyQualifiedFilePath = $"{path}\\{filename}";

            Directory.CreateDirectory(path);

            if (File.Exists(fullyQualifiedFilePath))
            {
                using (var registrationDataFile = new StreamWriter(fullyQualifiedFilePath, true))
                {
                    registrationDataFile.WriteLine(registrationDetailJson);
                }
            }
            else
            {
                File.WriteAllLines(fullyQualifiedFilePath, new string[] { registrationDetailJson });
            }

            return fullyQualifiedFilePath;
        }
    }
}
