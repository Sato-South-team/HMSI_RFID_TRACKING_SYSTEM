using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HMSI_RFID_TRACKING_SYSTEM.CommonClasses;
using System.Windows.Shapes;

namespace HMSI_RFID_TRACKING_SYSTEM.StartUp
{
    /// <summary>
    /// Interaction logic for ForgotPassword.xaml
    /// </summary>
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }
        #region Variables and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        #endregion

        private void Clear()
        {
            this.txtUserID.Text = "";
            this.txtPassword.Text = "YOUR PASSWORD IS ?";
            this.txtUserID.Focus();
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.txtUserID.Text == "")
                {
                    CommonMethods.MessageBoxShow("PLASE ENTER USER ID", CommonVariable.CustomStriing.Information.ToString());
                    this.txtUserID.Focus();
                }
                else
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID = this.txtUserID.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = nameof(ForgotPassword);
                    CommonVariable.Result = this.obj_BL.BL_Login();
                    if (CommonVariable.Result.StartsWith("YOUR PASSOWRD IS"))
                    {
                        this.txtPassword.Text = "YOUR PASSWORD IS " + CommonVariable.Result.Split('+')[1].ToString();
                        this.txtUserID.Focus();
                    }
                    else
                    {
                        CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
                        this.txtUserID.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "FORGOT_PASSWORD", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //while (this.NavigationService.CanGoBack)
                //{
                //    try
                //    {
                //        this.NavigationService.RemoveBackEntry();
                //    }
                //    catch (Exception ex)
                //    {
                //        break;
                //    }
                //}
                //this.NavigationService.Navigate((object)new Login());
                this.Close();
                Login obj_Page = new Login();
                obj_Page.ShowDialog();
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "FORGOT_PASSWORD", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtUserID.Focus();
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "FORGOT_PASSWORD", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void txtUserID_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return)
                return;
            this.btnShow_Click(sender, (RoutedEventArgs)e);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Clear();
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "FORGOT_PASSWORD", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
                this.btnShow_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
                this.btnClear_Click(sender, (RoutedEventArgs)e);
            if ((!Keyboard.IsKeyDown(Key.LeftAlt) || !Keyboard.IsKeyDown(Key.B)) && (!Keyboard.IsKeyDown(Key.RightAlt) || !Keyboard.IsKeyDown(Key.B)))
                return;
            this.btnExit_Click(sender, (RoutedEventArgs)e);
        }

    }
}
