using HMSI_RFID_TRACKING_SYSTEM.CommonClasses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;

namespace HMSI_RFID_TRACKING_SYSTEM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();

        private void StartUP(object sender, StartupEventArgs e)
        {
            try
            {
                if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Log"))
                {
                    Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Log");
                }
                string data = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                if (data != "")
                {
                    string[] DataSplit = data.Split(',');
                    if (DataSplit.Length > 0)
                    {
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.SqldbServer = DataSplit[0].ToString();
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.SqlDBUserID = DataSplit[1].ToString();
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.SqlDBPassword = DataSplit[2].ToString();
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.SqlDBName = DataSplit[3].ToString();
                        StartUp.Login obj_Login = new StartUp.Login();
                        obj_Login.ShowDialog();
                        // App.Current.MainWindow.Content = new StartUp.Login();
                    }
                    else
                    {
                        CommonClasses.CommonMethods.MessageBoxShow("INCORRECT DATABASE SETTING!!", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

                    }
                }
                else
                {
                    CommonClasses.CommonMethods.MessageBoxShow("INCORRECT DATABASE SETTING!!", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }



    }
}

