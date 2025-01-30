using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary1
{
    /// <summary>
    /// 
    /// </summary>
    public enum InterestingFields
    {
        ObjectName,
        NameWinter,
        DistrictAndHasDressingRoom,
        District,
        HasDressingRoom,
        Lightning,
        Seats
    }
    /// <summary>
    /// Класс MyType для хранения данных об объектах.
    /// </summary>
    public class HockeyData
    {
        [JsonPropertyName("global_id")]
        public string GlobalId { get; private set; }

        [JsonPropertyName("ObjectName")]
        public string ObjectName { get; private set; }

        [JsonPropertyName("NameWinter")]
        public string NameWinter { get; private set; }

        [JsonPropertyName("PhotoWinter")]
        public string PhotoWinter { get; private set; }

        [JsonPropertyName("AdmArea")]
        public string AdmArea { get; private set; }

        [JsonPropertyName("District")]
        public string District { get; private set; }

        [JsonPropertyName("Address")]
        public string Address { get; private set; }

        [JsonPropertyName("Email")]
        public string Email { get; private set; }

        [JsonPropertyName("WebSite")]
        public string WebSite { get; private set; }

        [JsonPropertyName("HelpPhone")]
        public string HelpPhone { get; private set; }

        [JsonPropertyName("HelpPhoneExtension")]
        public string HelpPhoneExtension { get; private set; }

        [JsonPropertyName("WorkingHoursWinter")]
        public string WorkingHoursWinter { get; private set; }

        [JsonPropertyName("ClarificationOfWorkingHoursWinter")]
        public string ClarificationOfWorkingHoursWinter { get; private set; }

        [JsonPropertyName("PropertyType")]
        public string PropertyType { get; private set; }

        [JsonPropertyName("DepartamentalAffiliationType")]
        public string DepartamentalAffiliationType { get; private set; }

        [JsonPropertyName("HasEquipmentRental")]
        public string HasEquipmentRental { get; private set; }

        [JsonPropertyName("EquipmentRentalComments")]
        public string EquipmentRentalComments { get; private set; }

        [JsonPropertyName("HasTechService")]
        public string HasTechService { get; private set; }

        [JsonPropertyName("TechServiceComments")]
        public string TechServiceComments { get; private set; }

        [JsonPropertyName("HasDressingRoom")]
        public string HasDressingRoom { get; private set; }

        [JsonPropertyName("HasEatery")]
        public string HasEatery { get; private set; }

        [JsonPropertyName("HasToilet")]
        public string HasToilet { get; private set; }

        [JsonPropertyName("HasWifi")]
        public string HasWifi { get; private set; }

        [JsonPropertyName("HasCashMachine")]
        public string HasCashMachine { get; private set; }

        [JsonPropertyName("HasFirstAidPost")]
        public string HasFirstAidPost { get; private set; }

        [JsonPropertyName("HasMusic")]
        public string HasMusic { get; private set; }

        [JsonPropertyName("UsagePeriodWinter")]
        public string UsagePeriodWinter { get; private set; }

        [JsonPropertyName("Status")]
        public string Status { get; private set; }

        [JsonPropertyName("DimensionsWinter")]
        public string DimensionsWinter { get; private set; }

        [JsonPropertyName("Lighting")]
        public string Lighting { get; private set; }

        [JsonPropertyName("SurfaceTypeWinter")]
        public string SurfaceTypeWinter { get; private set; }

        [JsonPropertyName("Seats")]
        public int Seats { get; private set; }

        [JsonPropertyName("Paid")]
        public string Paid { get; private set; }

        [JsonPropertyName("PaidComments")]
        public string PaidComments { get; private set; }

        [JsonPropertyName("DisabilityFriendly")]
        public string DisabilityFriendly { get; private set; }

        [JsonPropertyName("ServicesWinter")]
        public string ServicesWinter { get; private set; }

        [JsonPropertyName("geoData")]
        public string GeoData { get; private set; }

        [JsonPropertyName("geodata_center")]
        public string GeoDataCenter { get; private set; }

        [JsonPropertyName("geoarea")]
        public string GeoArea { get; private set; }

        public HockeyData(string[] arr)
        {
            try
            {
                GlobalId = arr[0].Trim('\"');
                ObjectName = arr[1].Trim('\"');
                NameWinter = arr[2].Trim('\"');
                PhotoWinter = arr[3].Trim('\"');
                AdmArea = arr[4].Trim('\"');
                District = arr[5].Trim('\"');
                Address = arr[6].Trim('\"');
                Email = arr[7].Trim('\"');
                WebSite = arr[8].Trim('\"');
                HelpPhone = arr[9].Trim('\"');
                HelpPhoneExtension = arr[10].Trim('\"');
                WorkingHoursWinter = arr[11].Trim('\"');
                ClarificationOfWorkingHoursWinter = arr[12].Trim('\"');
                PropertyType = arr[13].Trim('\"');
                DepartamentalAffiliationType = arr[14].Trim('\"');
                HasEquipmentRental = arr[15].Trim('\"');
                EquipmentRentalComments = arr[16].Trim('\"');
                HasTechService = arr[17].Trim('\"');
                TechServiceComments = arr[18].Trim('\"');
                HasDressingRoom = arr[19].Trim('\"');
                HasEatery = arr[20].Trim('\"');
                HasToilet = arr[21].Trim('\"');
                HasWifi = arr[22].Trim('\"');
                HasCashMachine = arr[23].Trim('\"');
                HasFirstAidPost = arr[24].Trim('\"');
                HasMusic = arr[25].Trim('\"');
                UsagePeriodWinter = arr[26].Trim('\"');
                Status = arr[27].Trim('\"');
                DimensionsWinter = arr[28].Trim('\"');
                Lighting = arr[29].Trim('\"');
                SurfaceTypeWinter = arr[30].Trim('\"');
                Seats = int.Parse(arr[31].Trim('\"'));
                Paid = arr[32].Trim('\"');
                PaidComments = arr[33].Trim('\"');
                DisabilityFriendly = arr[34].Trim('\"');
                ServicesWinter = arr[35].Trim('\"');
                GeoData = arr[36].Trim('\"');
                GeoDataCenter = arr[37].Trim('\"');
                GeoArea = arr[38].Trim('\"');
            }
            catch(Exception ex)
            {
                throw new ArgumentException("С форматом что-то не так! Ошибка" + ex.Message);
            }
        }
        public static int CompareBySeats(HockeyData x, HockeyData y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return x.Seats.CompareTo(y.Seats);
            }
        }
        public static int CompareByLighting(HockeyData x, HockeyData y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    return 0;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                return x.Lighting.CompareTo(y.Lighting);
            }
        }
        public HockeyData(JsonElement jsonEl)
        {
            try
            {
                GlobalId = jsonEl.GetProperty("global_id").GetString().Trim('\"');
                ObjectName = jsonEl.GetProperty("ObjectName").GetString().Trim('\"');
                NameWinter = jsonEl.GetProperty("NameWinter").GetString().Trim('\"');
                PhotoWinter = jsonEl.GetProperty("PhotoWinter").GetString().Trim('\"');
                AdmArea = jsonEl.GetProperty("AdmArea").GetString().Trim('\"');
                District = jsonEl.GetProperty("District").GetString().Trim('\"');
                Address = jsonEl.GetProperty("Address").GetString().Trim('\"');
                Email = jsonEl.GetProperty("Email").GetString().Trim('\"');
                WebSite = jsonEl.GetProperty("WebSite").GetString().Trim('\"');
                HelpPhone = jsonEl.GetProperty("HelpPhone").GetString().Trim('\"');
                HelpPhoneExtension = jsonEl.GetProperty("HelpPhoneExtension").GetString().Trim('\"');
                WorkingHoursWinter = jsonEl.GetProperty("WorkingHoursWinter").GetString().Trim('\"');
                ClarificationOfWorkingHoursWinter = jsonEl.GetProperty("ClarificationOfWorkingHoursWinter").GetString().Trim('\"');
                PropertyType = jsonEl.GetProperty("PropertyType").GetString().Trim('\"');
                DepartamentalAffiliationType = jsonEl.GetProperty("DepartamentalAffiliationType").GetString().Trim('\"');
                HasEquipmentRental = jsonEl.GetProperty("HasEquipmentRental").GetString().Trim('\"');
                EquipmentRentalComments = jsonEl.GetProperty("EquipmentRentalComments").GetString().Trim('\"');
                HasTechService = jsonEl.GetProperty("HasTechService").GetString().Trim('\"');
                TechServiceComments = jsonEl.GetProperty("TechServiceComments").GetString().Trim('\"');
                HasDressingRoom = jsonEl.GetProperty("HasDressingRoom").GetString().Trim('\"');
                HasEatery = jsonEl.GetProperty("HasEatery").GetString().Trim('\"');
                HasToilet = jsonEl.GetProperty("HasToilet").GetString().Trim('\"');
                HasWifi = jsonEl.GetProperty("HasWifi").GetString().Trim('\"');
                HasCashMachine = jsonEl.GetProperty("HasCashMachine").GetString().Trim('\"');
                HasFirstAidPost = jsonEl.GetProperty("HasFirstAidPost").GetString().Trim('\"');
                HasMusic = jsonEl.GetProperty("HasMusic").GetString().Trim('\"');
                UsagePeriodWinter = jsonEl.GetProperty("UsagePeriodWinter").GetString().Trim('\"');
                Status = jsonEl.GetProperty("Status").GetString().Trim('\"');
                DimensionsWinter = jsonEl.GetProperty("DimensionsWinter").GetString().Trim('\"');
                Lighting = jsonEl.GetProperty("Lighting").GetString().Trim('\"');
                SurfaceTypeWinter = jsonEl.GetProperty("SurfaceTypeWinter").GetString().Trim('\"');
                Seats = jsonEl.GetProperty("Seats").GetInt32();
                Paid = jsonEl.GetProperty("Paid").GetString().Trim('\"');
                PaidComments = jsonEl.GetProperty("PaidComments").GetString().Trim('\"');
                DisabilityFriendly = jsonEl.GetProperty("DisabilityFriendly").GetString().Trim('\"');
                ServicesWinter = jsonEl.GetProperty("ServicesWinter").GetString().Trim('\"');
                GeoData = jsonEl.GetProperty("geoData").GetString().Trim('\"');
                GeoDataCenter = jsonEl.GetProperty("geodata_center").GetString().Trim('\"');
                GeoArea = jsonEl.GetProperty("geoarea").GetString().Trim('\"');
            }
            catch (Exception ex)
            {
                throw new ArgumentException("С форматом что-то не так! Ошибка" + ex.Message);
            }
        }

    }
}
