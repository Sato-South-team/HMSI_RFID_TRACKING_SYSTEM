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
using System.Windows.Shapes;
using System.Diagnostics;
using System.Data;
using System.Threading;
using HMSI_RFID_TRACKING_SYSTEM.CommonClasses;
using HMSI_RFID_TRACKING_SYSTEM.Masters;

using System.Windows.Forms;
using Button = System.Windows.Controls.Button;

namespace HMSI_RFID_TRACKING_SYSTEM.StartUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region Varialble and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        #endregion

        private void ShowDateTime()
        {
            this.dispatcherTimer.Tick += new EventHandler(this.dispatcherTimer_Tick);
            this.dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            this.dispatcherTimer.Start();
        }

        private void ImgSmily3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                //    while (this.NavigationService.CanGoBack)
                //    {
                //        try
                //        {
                //            this.NavigationService.RemoveBackEntry();
                //        }
                //        catch (Exception ex)
                //        {
                //            break;
                //        }
                //    }
                this.dispatcherTimer.Stop();
                this.Close();
                Login obj_Page = new Login();
                obj_Page.ShowDialog();
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dispatcherTimer.Stop();
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                this.txtDatetime.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss");
                if (!(CommonVariable.PageOpenClose == "Close"))
                {
                    return;
                }
                if (CommonVariable.PageOpenClose == "Close")
                {
                   
                    this.frmPage.Navigate((Uri)null);
                    this.Background = (Brush)Brushes.White;
                    grdFrame.Visibility = Visibility.Hidden;
                    GridSubMenu.Visibility = Visibility.Visible;
                }
              
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.txtuserID.Text = "Application is using by " + CommonVariable.UserName;
                this.ShowDateTime();
                this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
                WindowState = WindowState.Maximized;

               // MnuDieMaintenance_Click(null, null);
                //this.frmPage.Navigate(new Uri("/Transaction/DieMaitenanceControl.xaml", UriKind.RelativeOrAbsolute));

            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAIN_WINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        

        private void BtnMasters_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.GridSubMenu.Children.RemoveRange(0, 20);
                if (CommonVariable.Rights == "" || CommonVariable.Rights == null)
                {
                    CommonMethods.MessageBoxShow("NO RIGHTS TO ACCESS THE MASTERS", CommonVariable.CustomStriing.Information.ToString());
                }
                else
                {
                    grdFrame.Visibility = Visibility.Hidden;
                    GridSubMenu.Visibility = Visibility.Visible;
                    for (int index1 = 0; index1 < this.GridSubMenu.RowDefinitions.Count; ++index1)
                    {
                        int num = 0;
                        if (num != 5)
                        {
                            for (int index2 = 0; index2 < this.GridSubMenu.ColumnDefinitions.Count; ++index2)
                            {
                                if (index2 == 0 && index1 == 0)
                                {
                                    Button element = new Button();
                                    element.Content = (object)"USER MASTER";
                                    element.Style = (Style)null;
                                    element.Style = (Style)this.FindResource((object)"SubMenuButton");
                                    Grid.SetColumn((UIElement)element, index2);
                                    Grid.SetRow((UIElement)element, index1);
                                    this.GridSubMenu.Children.Add((UIElement)element);
                                    ++num;
                                    if (CommonVariable.Rights.Contains("USER MASTER"))
                                    {
                                        element.Click += new RoutedEventHandler(this.MnuUserMaster_Click);
                                    }
                                    else
                                    {
                                        element.Click -= new RoutedEventHandler(this.MnuUserMaster_Click);
                                        element.ToolTip = (object)"Access Denied";
                                    }
                                }
                                if (index2 == 1 && index1 == 0)
                                {
                                    Grid grid = new Grid();
                                    Button element = new Button();
                                    element.Content = (object)"GROUP MASTER";
                                    element.Style = (Style)this.FindResource((object)"SubMenuButton");
                                    Grid.SetColumn((UIElement)element, index2);
                                    Grid.SetRow((UIElement)element, index1);
                                    this.GridSubMenu.Children.Add((UIElement)element);
                                    ++num;
                                    if (CommonVariable.Rights.Contains("GROUP MASTER"))
                                    {
                                        element.Click += new RoutedEventHandler(this.MnuGroupMaster_Click);
                                    }
                                    else
                                    {
                                        element.Click -= new RoutedEventHandler(this.MnuGroupMaster_Click);
                                        element.ToolTip = (object)"Access Denied";
                                    }
                                }
                              
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

      
        private void BtnTransport_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.GridSubMenu.Children.RemoveRange(0, 20);
                if (CommonVariable.Rights == "" || CommonVariable.Rights == null)
                {
                    CommonMethods.MessageBoxShow("NO RIGHTS TO ACCESS THE MASTERS", CommonVariable.CustomStriing.Information.ToString());
                }
                else
                {
                    grdFrame.Visibility = Visibility.Hidden;
                    GridSubMenu.Visibility = Visibility.Visible;
                    for (int index1 = 0; index1 < this.GridSubMenu.RowDefinitions.Count; ++index1)
                    {
                        int num = 0;
                        if (num != 5)
                        {
                            for (int index2 = 0; index2 < this.GridSubMenu.ColumnDefinitions.Count; ++index2)
                            {
                                //if (index2 == 0 && index1 == 0)
                                //{
                                //    Button element = new Button();
                                //    element.Content = (object)"PRODUCTION PLAN";
                                //    element.Style = (Style)null;
                                //    element.Style = (Style)this.FindResource((object)"SubMenuButton");
                                //    Grid.SetColumn((UIElement)element, index2);
                                //    Grid.SetRow((UIElement)element, index1);
                                //    this.GridSubMenu.Children.Add((UIElement)element);
                                //    ++num;
                                //    if (CommonVariable.Rights.Contains("PRODUCTION PLAN"))
                                //    {
                                //        element.Click += new RoutedEventHandler(this.MnuProductionPlan_Click);
                                //    }
                                //    else
                                //    {
                                //        element.Click -= new RoutedEventHandler(this.MnuProductionPlan_Click);
                                //        element.ToolTip = (object)"Access Denied";
                                //    }
                                //}
                               
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }

        }


        private void BtnReport_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                this.GridSubMenu.Children.RemoveRange(0, 20);
                if (CommonVariable.Rights == "" || CommonVariable.Rights == null)
                {
                    CommonMethods.MessageBoxShow("NO RIGHTS TO ACCESS THE MASTERS", CommonVariable.CustomStriing.Information.ToString());
                }
                else
                {
                    grdFrame.Visibility = Visibility.Hidden;
                    GridSubMenu.Visibility = Visibility.Visible;
                    for (int index1 = 0; index1 < this.GridSubMenu.RowDefinitions.Count; ++index1)
                    {
                        int num = 0;
                        if (num != 5)
                        {
                            for (int index2 = 0; index2 < this.GridSubMenu.ColumnDefinitions.Count; ++index2)
                            {
                                //if (index2 == 0 && index1 == 0)
                                //{
                                //    Button element = new Button();
                                //    element.Content = (object)"SHORT COUNT REPORT";
                                //    element.Style = (Style)null;
                                //    element.Style = (Style)this.FindResource((object)"SubMenuButton");
                                //    Grid.SetColumn((UIElement)element, index2);
                                //    Grid.SetRow((UIElement)element, index1);
                                //    this.GridSubMenu.Children.Add((UIElement)element);
                                //    ++num;
                                //    if (CommonVariable.Rights.Contains("SHORT COUNT REPORT"))
                                //    {
                                //        element.Click += new RoutedEventHandler(this.MnuShortCountReport_Click);
                                //    }
                                //    else
                                //    {
                                //        element.Click -= new RoutedEventHandler(this.MnuShortCountReport_Click);
                                //        element.ToolTip = (object)"Access Denied";
                                //    }
                                //}
                                
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }

        }

        //private void MnuRevHistoryReport_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        grdFrame.Visibility = Visibility.Visible;
        //        GridSubMenu.Visibility = Visibility.Hidden;
        //        CommonVariable.PageOpenClose = "Open";
        //        this.Background = (Brush)Brushes.White;
        //        this.frmPage.NavigationService.Navigate((object)new RevisionHistoryReport());
        //    }
        //    catch (Exception ex)
        //    {
        //        this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
        //        CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
        //    }
        //}

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Process.GetProcessesByName("MOLDING_INTEGRATION_SYSTEM")[0].Kill();
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuUserMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                grdFrame.Visibility = Visibility.Visible;
                GridSubMenu.Visibility = Visibility.Hidden;
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/UserMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void MnuGroupMaster_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                grdFrame.Visibility = Visibility.Visible;
                GridSubMenu.Visibility = Visibility.Hidden;
                CommonVariable.PageOpenClose = "Open";
                this.Background = (Brush)Brushes.White;
                this.frmPage.Navigate(new Uri("/Masters/GroupMaster.xaml", UriKind.RelativeOrAbsolute));
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINWINDOW", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

      

    }
}
