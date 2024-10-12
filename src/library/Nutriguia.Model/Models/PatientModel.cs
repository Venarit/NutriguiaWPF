namespace Nutriguia.Model.Models
{
    public class PatientModel : BaseModel
    {
        private string name;
        private string? secondName;
        private string lastNameP;
        private string? lastNameM;
        private string email;
        private string cellphone;
        private string birthDate;

        public required string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                SetProperty(ref this.name, value);
            }
        }
        public string? SecondName
        {
            get
            {
                return this.secondName;
            }
            set
            {
                SetProperty(ref this.secondName, value);
            }
        }
        public required string LastNameP
        {
            get
            {
                return this.lastNameP;
            }
            set
            {
                SetProperty(ref this.lastNameP, value);
            }
        }
        public string? LastNameM
        {
            get
            {
                return this.lastNameM;
            }
            set
            {
                SetProperty(ref this.lastNameM, value);
            }
        }
        public required string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                SetProperty(ref this.email, value);
            }
        }
        public required string Cellphone
        {
            get
            {
                return this.cellphone;
            }
            set
            {
                SetProperty(ref this.cellphone, value);
            }
        }
        public required string BirthDate
        {
            get
            {
                return this.birthDate;
            }
            set
            {
                SetProperty(ref this.birthDate, value);
            }
        }
        public NutritionalProfileModel NutritionalProfile
        {
            get;
            set;
        }
    }
}
