using System;
using System.Collections.Generic;
using System.Text;

namespace Lion.Client.SDK.Models
{
    public class User
    {
        public int ID { get; set; }

        public Guid UserGuid { get; set; }

        public Guid EnvironmentGuid { get; set; }

        public string Key { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime ModifyDate { get; set; }


    }

}
