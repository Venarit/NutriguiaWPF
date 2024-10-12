namespace Nutriguia.Model.Models
{
    public class UserModel : BaseModel
    {
        private string user;
        private string password;
        public required string User
        {
            get
            {
                return this.user;
            }
            set
            {
                SetProperty(ref this.user, value);
            }
        }
        protected string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                SetProperty(ref this.password, value);
            }
        }
    }
}
