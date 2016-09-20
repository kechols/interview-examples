namespace Kevins.Examples.Database.Mapping
{
    public class UserMap : BaseEntityMap<User>
    {
        public UserMap()
        {
            Table("Users");

            Id(o => o.Id, "User_No").Not.Nullable();
            Map(o => o.UserCode, "UserCode").Nullable();
            Map(o => o.UserPassword, "UserPass").Nullable();
            Map(o => o.Name, "Name").Nullable();
            Map(o => o.PasswordLastChanged, "PasswordLastChg").Nullable();
            Map(o => o.Status, "Status").Nullable();
            Map(o => o.Salt, "Salt").Nullable();
            Map(o => o.LastLogin, "LastLogin").Nullable();
            Map(o => o.TypeOfUser, "TypeOfUser").Nullable();
            Map(o => o.LicenseNumber, "License_No").Nullable();
            Map(o => o.BillingGroup, "BillingGroup").Nullable();
            Map(o => o.Phone, "Phone").Nullable();
            Map(o => o.Street1, "Street1").Nullable();
            Map(o => o.Street2, "Street2").Nullable();
            Map(o => o.City, "City").Nullable();
            Map(o => o.State, "State").Nullable();
            Map(o => o.Zip, "Zip").Nullable();
            Map(o => o.Country, "Country");
            Map(o => o.PreferredLanguage, "PreferredLanguage").Nullable();
            Map(o => o.IpAddress, "IpAddress").Nullable();
            Map(o => o.LastWebActivity, "LastWebActivity").Nullable();
            Map(o => o.PasswordViolation, "PasswordViolation").Nullable();
            Map(o => o.PaLayoutNumber, "PaLayout_no").Nullable();
            Map(o => o.LastUrl, "LastURL").Nullable();
            Map(o => o.EmailAddress, "EmailAddress");
            Map(o => o.FaxNumber, "FaxNumber").Nullable();
            Map(o => o.PasswordReminder, "PasswordReminder").Nullable();
            Map(o => o.CurrentMachineLoggedInto, "currentMachineLoggedInto").Nullable();
            Map(o => o.Title, "Title").Nullable();
            Map(o => o.Suffix, "Suffix").Nullable();
            Map(o => o.LicenseNumber2, "License_No2").Nullable();
            Map(o => o.SpecialityNumber, "SpecialityNo").Nullable();
            Map(o => o.AlertWhenRadSendsNoteToPhysician, "AlertWhenRadSendsNoteToPhys").Nullable();
            Map(o => o.TypeOfRadiologist, "TypeOfRadiologist").Nullable();
            Map(o => o.Npi, "NPI").Nullable();
            Map(o => o.PermissionTemplateId, "PermissionTemplate_ID").Nullable();
            Map(o => o.ForceUserToChangePassword, "forceUserToChangePwd");
            Map(o => o.DefaultCity, "defaultCity").Nullable();
            Map(o => o.DefaultState, "defaultState").Nullable();
            Map(o => o.DefaultCountry, "defaultCountry").Nullable();
            Map(o => o.ClearUserCode, "clearUserCode").Nullable();
            Map(o => o.ResidentsLevelOfExperience, "residentsLevelOfExperience").Nullable();

            References(o => o.Department, "Department").Nullable().NotFound.Ignore().Cascade.None().LazyLoad();
            References(o => o.Facility, "FacilityNo").Nullable().NotFound.Ignore().Cascade.None().LazyLoad();

            HasMany(o => o.Preferences)
                .Table("UserPrefs")
                .KeyColumn("User_No")
                .AsSet().Cascade.None().LazyLoad();
        }
    }
}
