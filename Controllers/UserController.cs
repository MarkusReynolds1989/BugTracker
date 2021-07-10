using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Threading.Tasks;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

namespace BugTracker.Controllers
{
    public class UserController
    {
        public bool Insert(User user)
        {
            bool success;

            try
            {
                success = true;
            }
            catch (MySqlException exception)
            {
                Debug.WriteLine(exception);
                success = false;
            }

            return success;
        }

        public bool Update(User user)
        {
            bool success;

            try
            {
                success = true;
            }
            catch (MySqlException exception)
            {
                success = false;
                Debug.WriteLine(exception);
            }

            return success;
        }
    }
}