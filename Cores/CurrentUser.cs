/*
* 06.06.2022
* Copyright (c) 2022 WiiTrak, All Rights Reserved.
*/
using System.Data;
using System.Reflection;
using WiiTrakClient.DTOs;

namespace WiiTrakClient.Cores
{
    public static class CurrentUser
    {
        public static Guid UserId { get; set; }
        public static string UserRole { get; set; } = string.Empty;
        public static string UserName { get; set; } = string.Empty;
        public static bool IsFirstLogin { get; set; }
        public static DateTime PasswordLastUpdatedAt { get; set; }
        public static int UserRoleId { get; set; }
        public static string Password { get; set; } = string.Empty;
    }

    public static class Core
    {
        public static Guid SelectedDriverId { get; set; }
        public static int NotificationCount { get; set; }
        public static List<NotificationDto>? NotificationsList { get; set; }
        public static int ServiceBoardCount { get; set; }
        public static List<ServiceBoardDto>? ServiceBoardList { get; set; }

        public static int MessageCount { get; set; }
        public static List<MessagesDto>? MessagesList { get; set; }

        public static double ToDouble(string Value)
        {
            return double.Parse(Value, System.Globalization.CultureInfo.InvariantCulture);
        }
        public static string ToPascalCase(string Text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Text))
                {
                    return Text;
                }
                return string.Join(" ", Text.Split(' ').Select(w => w.Trim()).Where(w => w.Length > 0)
                        .Select(w => w.Substring(0, 1).ToUpper() + w.Substring(1).ToLower()));
            }
            catch
            {
                return Text;
            }
        }

        #region "Encrypt"

        /// <summary>
        /// Encrypts specified ciphertext using Rijndael symmetric key algorithm.
        /// </summary>
        /// <param name="plainText">
        /// Base64-formatted Text value.
        /// </param>
        public static string Base64Encrypt(string plaintext)
        {
            if (String.IsNullOrEmpty(plaintext))
            {
                return plaintext;
            }

            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(plaintext);
            return Convert.ToBase64String(textBytes);

        }
        #endregion

        #region "Decrypt"


        public static string Base64Decrypt(string base64EncodedText)
        {

            if (String.IsNullOrEmpty(base64EncodedText))
            {
                return base64EncodedText;
            }

            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        #endregion

        #region To DataTable
        public static DataTable ToDataTable<T>(List<T> items)
        {
            try
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Defining type of data column gives proper data table 
                    var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                    //Setting column names as Property names
                    dataTable.Columns.Add(prop.Name, type);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {
                        //inserting property values to datatable rows
                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }
                //put a breakpoint here and check datatable
                return dataTable;
            }
            catch
            {

                return null;
            }
        }
        #endregion
    }
    public static class Numbers
    {
        public const int TwentyFive  = 25;
        public const int Four= 4;
        public const int Five = 5;
        public const int Fifteen = 15;
        public const int Fifty = 50;
        public const int Twenty = 20;
        public const int Three = 3;
        public const int Ten = 10;
        public const int Sixty = 60;
        public const int FourHundred = 400;
        public const int FiveHundred = 500;
        public const double OneOneFive= 1.1515;
        public const double OneSixZero = 1.609344;
        public const double OneEighty = 180.0;
    }

}
