﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021201.DataLayers;
using _19T1021201.DomainModels;

namespace _19T1021201.BusinessLayers
{
    /// <summary>
    /// Các chức năng liên quan đến tài khoản
    /// </summary>
    public static class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL customerAccountDB;

        /// <summary>
        /// 
        /// </summary>
        static UserAccountService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            employeeAccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
            customerAccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountType"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static UserAccount Authorize(AccountTypes accountType, string userName, string password)
        {
            if(accountType == AccountTypes.Employee)
            {
                return employeeAccountDB.Authorize(userName, password);
            }    
            else
            {
                return customerAccountDB.Authorize(userName, password);
            }    
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accounttype"></param>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public static bool ChangePassword(AccountTypes accounttype, string userName, string oldPassword, string newPassword)
        {
            if (accounttype == AccountTypes.Employee)
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPassword);
            else
                return customerAccountDB.ChangePassword(userName, oldPassword, newPassword);
        }
    }
}
