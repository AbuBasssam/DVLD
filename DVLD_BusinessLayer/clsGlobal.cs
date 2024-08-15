using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using DVlD_BusinessLayer;
using Microsoft.Win32;



namespace DVlD.BusinessLayer
{
    public static class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {


            try
            {
                /* this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();*/


                // Specify the Registry key and path
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD USER LOGIN";


                if (Username == "" )
                {
                    using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                    {
                        using (RegistryKey key = baseKey.OpenSubKey(keyPath, true))
                        {
                            if (key != null)
                            {
                                // Delete the specified value
                                key.DeleteValue("UserName");
                                key.DeleteValue("Password");



                            }

                        }

                    }

                    // concatonate username and passwrod withe seperator.


                    // Write the value to the Registry
                    
                   
                }
                Registry.SetValue(keyPath, "UserName", Username, RegistryValueKind.String);
                    Registry.SetValue(keyPath, "Password", Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                

                // Path for the file that contains the credential.
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD USER LOGIN";

                // Check if the file exists before attempting to read it


                Username = Registry.GetValue(keyPath, "UserName", null) as string;

                Password = Registry.GetValue(keyPath, "Password", null) as string;

                    if (Username != ""&& Password != "")
                    {

                        return true;
                    }
                    
                    
                        
                    
                    // Create a StreamReader to read from the file
                       //    using (StreamReader reader = new StreamReader(filePath))
                       //    {
                       //        // Read data line by line until the end of the file
                       //        string line;
                       //        while ((line = reader.ReadLine()) != null)
                       //        {
                       //            Console.WriteLine(line); // Output each line of data to the console
                       //            string[] result = line.Split(new string[] { "/***/" }, StringSplitOptions.None);
                      
                       //            Username = result[0];
                       //            Password = result[1];
                       //        }
                       //        return true;
                       //    }
                       //}
                       //else
                       //{
                       //    return false;
                
                return false;

            }
            catch (Exception ex)
            {
                //MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

     }
}
