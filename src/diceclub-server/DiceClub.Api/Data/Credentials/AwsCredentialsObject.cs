using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceClub.Api.Data.Credentials
{
    public class AwsCredentialsObject
    {
        public string AwsAccessKey { get; set; }

        public string AwsSecretKey { get; set; }

        public string Region { get; set; }
        
    }
}
