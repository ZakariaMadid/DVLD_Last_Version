using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using Microsoft.Win32;


namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;

        //Save UserName And Password in Registry

        //Read from the registry
        public static bool GetStoredUserNameAndPasswordInRegistry(ref string UserName,ref string Password)
        {
            // Specify the registry key and path
  
            string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_Final";
           
            try
            {
                UserName = (string)Registry.GetValue(KeyPath, "UserName", null);
                Password = (string)Registry.GetValue(KeyPath, "Password", null);

                if (UserName != null && Password!=null)
                {
                    return true;
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error Occurred: {ex.Message}","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);

            }
            return false;
        }
             //writting to the Registry
       public static bool SaveUserNameAndPasswordInRegistry(string Username,string Password) 
        { 
            // Specify the registry key and path
             string KeyPath = @"HKEY_CURRENT_USER\SOFTWARE\DVLD_Final";
 
            try
            {
                Registry.SetValue(KeyPath, "Username", Username, RegistryValueKind.String);
                Registry.SetValue(KeyPath, "Password", Password, RegistryValueKind.String);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An Error Occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            return false;
        }
        // Delete stored data from Registry
        public static bool DeleteStoredDataFromRegistry()
        {
            string KeyPath = @"SOFTWARE\DVLD_Final";
            string ValueName1 = "UserName";
            string ValueName2 = "Password";
            try
            {
                //Open The registry key in read and write mode
                using (RegistryKey BaseKey = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Registry64))
                {
                    using (RegistryKey key = BaseKey.OpenSubKey(KeyPath, true))
                    {
                        if (key != null)
                        {
                            //delete Value
                            //false: if value stored delete if not No exception
                            key.DeleteValue(ValueName1,false);
                            key.DeleteValue(ValueName2,false);
                            return true;
                        }

                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Run program with Administrative Privileges.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Exception: {ex.Message}", "Exception",  MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
     //Save UserName and Password in File.
     public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            try
            {
                //this will get the current project directory folder.
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();


                // Define the path to the text file where you want to save the data
                string filePath = currentDirectory + "\\data.txt";

                //incase the username is empty, delete the file
                if (Username=="" && File.Exists(filePath)) 
                { 
                     File.Delete(filePath);
                    return true;

                }

                // concatonate username and passwrod withe seperator.
                string dataToSave = Username + "#//#"+Password ;

                // Create a StreamWriter to write to the file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Write the data to the file
                    writer.WriteLine(dataToSave);
                   
                  return true;
                }
            }
            catch (Exception ex)
            {
               MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            //this will get the stored username and password and will return true if found and false if not found.
            try
            {
                //gets the current project's directory
                string currentDirectory = System.IO.Directory.GetCurrentDirectory();

                // Path for the file that contains the credential.
                string filePath  = currentDirectory + "\\data.txt";

                // Check if the file exists before attempting to read it
                if (File.Exists(filePath))
                {
                    // Create a StreamReader to read from the file
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        // Read data line by line until the end of the file
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            Console.WriteLine(line); // Output each line of data to the console
                            string[] result = line.Split(new string[] { "#//#" }, StringSplitOptions.None);

                            Username = result[0];
                            Password = result[1];
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;   
            }

        }
    }
}
