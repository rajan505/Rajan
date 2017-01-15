using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JQueryDataTables.Models
{
    public class UserModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }

        public static List<UserModel> getUsers()
        {
            List<UserModel> users = new List<UserModel>()
            {
                new UserModel (){ ID=1, Name="Anubhav", SurName="Chaudhary" }, 
                new UserModel (){ ID=2, Name="Mohit", SurName="Singh" },
                new UserModel (){ ID=3, Name="Sonu", SurName="Garg" },
                new UserModel (){ ID=4, Name="Shalini", SurName="Goel" },
                new UserModel (){ ID=5, Name="James", SurName="Bond" },
            };
            return users;
        }
    }
}
