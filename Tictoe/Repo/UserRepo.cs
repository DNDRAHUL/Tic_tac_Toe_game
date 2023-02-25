using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Tictoe.DAL;
using Tictoe.Helper;
using Tictoe.Model;

namespace Tictoe.Repo
{
    public class UserRepo
    {
        public IConfiguration config;

        public ManualDbContext dbContext;

        public UserRepo(IConfiguration _config, ManualDbContext _dbContext)
        {
            config = _config;
            dbContext = _dbContext;
        }


        public Response CreateUser(User user)
        {
            Response res=new Response();
            res.message = "Not found";
            res.status_Code = 400;

            if (user.IsNull())
            {
                return res;
            }
            Hashtable map = new Hashtable();
            map.Add("@firstname", user.first_name);
            map.Add("@lastname", user.last_name);
            map.Add("@email", user.email);
            map.Add("@password", user.password);

            DataTable dt = new DataTable();
            dt = dbContext.GetDataTable("StoredProcedure", map);

            if (dt.IsNotNull())
            {
                if (dt.Rows.Count > 0)
                {
                    res.status_Code = Convert.ToInt32(dt.Rows[0]["statuscode"]);
                    res.message = Convert.ToString(dt.Rows[0]["message"]);
                }
            }

            return res;

        }

        public List<User> GetAllUser()
        {
            List<User> usr = new List<User>();
            DataTable dt = new DataTable();
            dt = dbContext.GetDataTable("uspGetUser");

            if (dt.IsNotNull())
            {
                if (dt.Rows.Count > 0)
                {


                    foreach(DataRow row in dt.Rows)
                    {

                        usr.Add(new User
                        {
                            first_name = Convert.ToString(row["first_name"]),
                            last_name = Convert.ToString(row["last_name"]),
                            email = Convert.ToString(row["email"]),
                            _id = Convert.ToInt32(row["_id"]),

                        });
                    }
                }
            }

            return usr;
        }
    }
}
