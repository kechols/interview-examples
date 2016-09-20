using System;
using System.Collections.Generic;
using Kevins.Examples.Common.Extensions;

namespace Kevins.Examples.Database
{
    public class User : BaseEntity<User>
    {
        public virtual string UserCode { get; set; }
        public virtual string UserPassword { get; set; }
        public virtual string Name { get; set; }
        public virtual string PasswordLastChanged { get; set; }
        public virtual string Status { get; set; }
        public virtual string Salt { get; set; }
        public virtual DateTime? LastLogin { get; set; }
        public virtual string TypeOfUser { get; set; }
        public virtual string LicenseNumber { get; set; }
        public virtual string BillingGroup { get; set; }
        public virtual Department Department { get; set; }
        public virtual string Phone { get; set; }
        public virtual string Street1 { get; set; }
        public virtual string Street2 { get; set; }
        public virtual string City { get; set; }
        public virtual string State { get; set; }
        public virtual string Zip { get; set; }
        public virtual string Country { get; set; }
        public virtual string PreferredLanguage { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string LastWebActivity { get; set; }
        public virtual int PasswordViolation { get; set; }
        public virtual int PaLayoutNumber { get; set; }
        public virtual string LastUrl { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string FaxNumber { get; set; }
        public virtual string PasswordReminder { get; set; }
        public virtual string CurrentMachineLoggedInto { get; set; }
        public virtual string Title { get; set; }
        public virtual string Suffix { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual string LicenseNumber2 { get; set; }
        public virtual int SpecialityNumber { get; set; }
        public virtual string AlertWhenRadSendsNoteToPhysician { get; set; }
        public virtual string TypeOfRadiologist { get; set; }
        public virtual string Npi { get; set; }
        public virtual int PermissionTemplateId { get; set; }
        public virtual bool ForceUserToChangePassword { get; set; }
        public virtual string DefaultCity { get; set; }
        public virtual string DefaultState { get; set; }
        public virtual string DefaultCountry { get; set; }
        public virtual bool ClearUserCode { get; set; }
        public virtual string ResidentsLevelOfExperience { get; set; }

        public virtual ISet<UserPreference> Preferences { get; set; }

        public virtual string FirstName
        {
            get { return Name.FirstName(); }
        }

        public virtual string LastName
        {
            get { return Name.LastName(); }
        }

        public virtual string VerboseUserName
        {
            get
            {
                var result = string.Empty;
                if (!string.IsNullOrEmpty(Title))
                {
                    result += Title + " ";
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    result += Name.AsFirstAndLastName() + " ";
                }
                if (!string.IsNullOrEmpty(Suffix))
                {
                    result += Suffix;
                }
                return result.Trim();
            }
        }


        public virtual bool HasCountry
        {
            get { return !string.IsNullOrEmpty(Country); }
        }


        public virtual bool Equals(User other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(User)) return false;
            return Equals((User)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397);
            }
        }
    }
}
