using System;
using OnlineTalent.BusinessModel;


namespace OnlineTalent.Authentication
{
    public class TokenServices : ITokenServices
    {
        public object ConfigurationManager { get; private set; }

        public double ExpiredOn { get; set; }

   
        public TokenModel GenerateToken(int userId)
        {
            string token = Guid.NewGuid().ToString();
            DateTime issuedOn = DateTime.Now;
            DateTime expiredOn = DateTime.Now.AddSeconds(ExpiredOn);
            //Convert.ToDouble(ConfigurationManager.AppSettings["AuthTokenExpiry"]));

            var tokendomain = new TokenModel
            {
                UserId = userId,
                AuthToken = token,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn
            };
            // burada db ye insert işlemi yap veya json a 

            var tokenModel = new TokenModel()
            {
                UserId = userId,
                IssuedOn = issuedOn,
                ExpiresOn = expiredOn,
                AuthToken = token
            };

            return tokenModel;
        }

        public bool Kill(string tokenId)
        {
            return true;
        }

        public bool ValidateToken(string tokenId)
        {
            return true;
        }

        public bool DeleteByUserId(int userId)
        {
            return true;
        }
    }
}
