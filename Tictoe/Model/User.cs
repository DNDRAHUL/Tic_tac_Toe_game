using System;

namespace Tictoe.Model
{
    public class User
    {
        public int _id { get; set; }
        public DateTime Created_at { get; set; }
        public DateTime updated_at { get; set; }
        public int Status_Code { get; set; }
        public int Code { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }

    }
}
