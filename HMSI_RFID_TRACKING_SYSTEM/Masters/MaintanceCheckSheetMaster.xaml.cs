using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MOLDING_INTEGRATION_SYSTEM.Masters
{
    /// <summary>
    /// Interaction logic for MaintanceCheckSheetMaster.xaml
    /// </summary>
    public partial class MaintanceCheckSheetMaster : Page
    {
        public MaintanceCheckSheetMaster()
        {
            InitializeComponent();
        }
        private void CmbCheckSheetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbCheckSheetType.SelectedIndex > -1)
                {
                    ComboBoxItem CMD = (ComboBoxItem)cmbCheckSheetType.SelectedItem;
                    cmbCheckSheetType.Text = CMD.Content.ToString();
                    
                    Transaction("LoadDetails");
                }
                
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Btnbrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                dlg.ShowDialog();
                if (dlg.FileName != "")
                {
                    imgModel.Source = new BitmapImage(new Uri(dlg.FileName));
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        int RefNo = 0;
        DataTable dt = new DataTable();
        public object CustomMessageBox { get; private set; }
        #endregion
        #region Methods
        private bool ControlValidation()
        {
            if (cmbMachine.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MACHINE NO", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbMachine.Focus();
                return false;
            }
            if (txtDescription.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER DESCRIPTION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtDescription.Focus();
                return false;
            }
            if (cmbCheckSheetType.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT CHECK SHEET TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbCheckSheetType.Focus();
                return false;
            }
            //if (txtCategory.Text == "")
            //{
            //    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER CATEGORY", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
            //    txtCategory.Focus();
            //    return false;
            //}
            
            //if (txtDescription.Text == "")
            //{
            //    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER CHECK SHEET DESCRIPTION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
            //    txtDescription.Focus();
            //    return false;
            //}
            return true;
        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete" || Type == "Checking")
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["CheckSheetType"].ToString()== "DAILY CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Int = dt.Rows[i]["int"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment = dt.Rows[i]["Judgement"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = dt.Rows[i]["Catergory"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Method = dt.Rows[i]["Method"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Condition = dt.Rows[i]["Condition"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal = dt.Rows[i]["Abnormal"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage = null;
                            if (imgModel.Source != null)
                            {
                                byte[] buffer;
                                var bitmap = imgModel.Source as BitmapSource;
                                var encoder = new PngBitmapEncoder(); // or one of the other encoders
                                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                                using (var stream = new MemoryStream())
                                {
                                    encoder.Save(stream);
                                    buffer = stream.ToArray();
                                }
                                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage = buffer;
                            }
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage = null;
                            if (imgRefimg.Source != null)
                            {
                                byte[] buffer;
                                var bitmap = imgRefimg.Source as BitmapSource;
                                var encoder = new PngBitmapEncoder(); // or one of the other encoders
                                encoder.Frames.Add(BitmapFrame.Create(bitmap));

                                using (var stream = new MemoryStream())
                                {
                                    encoder.Save(stream);
                                    buffer = stream.ToArray();
                                }
                                ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage = buffer;
                            }
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType = dt.Rows[i]["CheckSheetSide"].ToString();
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "ANNUAL CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Int = dt.Rows[i]["int"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment = dt.Rows[i]["Judgement"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "NEEDLE CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "PM CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = dt.Rows[i]["Catergory"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Int = dt.Rows[i]["int"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment = dt.Rows[i]["Judgement"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                        }                        
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = RefNo;
                        CommonClasses.CommonVariable.Result = obj_BL.BL_MaintenanceCheckSheetMasterTransaction();
                        if (CommonClasses.CommonVariable.Result == "Saved")
                        {
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                            txtDescription.Text = "";
                            Transaction("LoadDetails");
                        }
                    }
                    dt = null;
                }
                else
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = cmbMachine.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = txtDescription.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = cmbCheckSheetType.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Int = txtint.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment = txtjudgement.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = txtCategory.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Method = txtMethod.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Condition = txtCondition.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal = txtAbnormal.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage = null;
                    if (imgModel.Source != null)
                    {
                        byte[] buffer;
                        var bitmap = imgModel.Source as BitmapSource;
                        var encoder = new PngBitmapEncoder(); // or one of the other encoders
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));

                        using (var stream = new MemoryStream())
                        {
                            encoder.Save(stream);
                            buffer = stream.ToArray();
                        }
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage = buffer;
                    }
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage = null;
                    if (imgRefimg.Source != null)
                    {
                        byte[] buffer;
                        var bitmap = imgRefimg.Source as BitmapSource;
                        var encoder = new PngBitmapEncoder(); // or one of the other encoders
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));

                        using (var stream = new MemoryStream())
                        {
                            encoder.Save(stream);
                            buffer = stream.ToArray();
                        }
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage = buffer;
                    }
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType = cmbCheckSheetSide.Text;
                    //ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = "Test";
                    //ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = "";
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = RefNo;
                    CommonClasses.CommonVariable.Result = obj_BL.BL_MaintenanceCheckSheetMasterTransaction();
                    if (CommonClasses.CommonVariable.Result == "Saved")
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                        txtDescription.Text = "";
                        Transaction("LoadDetails");
                    }
                    else if (CommonClasses.CommonVariable.Result == "Updated")
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataUpdated, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                        Clear();
                        Transaction("LoadDetails");
                    }
                    else if (CommonClasses.CommonVariable.Result == "Not checked")
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataChecked, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        // Clear();
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = "";
                    }
                    else if (CommonClasses.CommonVariable.Result == "Duplicate")
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Duplicate, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    else if (CommonClasses.CommonVariable.Result != "Deleted")
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
            }
            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = cmbCheckSheetType.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = cmbMachine.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType = cmbCheckSheetSide.Text;

                if (cmbCheckSheetType.SelectedIndex != -1)
                {
                    DataTable dt = obj_BL.BL_MaintenanceCheckSheetMasterDetails().Tables[0];
                    if (cmbCheckSheetType.SelectedIndex == 0)
                    {
                        dvgMasterDeatils.ItemsSource = dt.DefaultView;
                    }
                    if (cmbCheckSheetType.SelectedIndex == 1)
                    {
                        dvgMasterDeatils1.ItemsSource = dt.DefaultView;
                    }
                    if (cmbCheckSheetType.SelectedIndex == 2)
                    {
                        dvgMasterDeatils2.ItemsSource = dt.DefaultView;
                    }
                    if (cmbCheckSheetType.SelectedIndex == 3)
                    {
                        dvgMasterDeatils3.ItemsSource = dt.DefaultView;
                    }

                    if (cmbCheckSheetType.SelectedIndex == 0)
                    {
                        Clear();
                        txtint.IsEnabled = true;
                      //  txtint.Background = new SolidColorBrush(Colors.White);
                        txtjudgement.IsEnabled = true;
                        //txtjudgement.Background = new SolidColorBrush(Colors.White);
                        txtCategory.IsEnabled = true;
                        //txtCategory.Background = new SolidColorBrush(Colors.White);
                        txtMethod.IsEnabled = true;
                        //txtMethod.Background = new SolidColorBrush(Colors.White);
                        txtCondition.IsEnabled = true;
                        //txtCondition.Background = new SolidColorBrush(Colors.White);
                        txtAbnormal.IsEnabled = true;
                        //txtAbnormal.Background = new SolidColorBrush(Colors.White);
                        cmbCheckSheetSide.IsEnabled = true;
                        btnbrowse.Visibility = System.Windows.Visibility.Visible;

                        dvgMasterDeatils.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else if (cmbCheckSheetType.SelectedIndex == 1)
                    {
                        Clear();
                        cmbCheckSheetSide.Text = "";
                        txtCategory.IsEnabled = false;
                      //  txtCategory.Background = new SolidColorBrush(Colors.Red);
                        txtMethod.IsEnabled = false;
                        //txtMethod.Background = new SolidColorBrush(Colors.Red);
                        txtCondition.IsEnabled = false;
                        //txtCondition.Background = new SolidColorBrush(Colors.Red);
                        txtAbnormal.IsEnabled = false;
                        //txtAbnormal.Background = new SolidColorBrush(Colors.Red);
                        cmbCheckSheetSide.IsEnabled = false;
                        //
                        txtint.IsEnabled = true;
                        txtint.Background = new SolidColorBrush(Colors.White);
                        txtjudgement.IsEnabled = true;
                        txtjudgement.Background = new SolidColorBrush(Colors.White);
                        btnbrowse.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils1.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else if (cmbCheckSheetType.SelectedIndex == 2)
                    {
                        Clear();
                        cmbCheckSheetSide.Text = "";
                        txtint.IsEnabled = false;
                      //  txtint.Background = new SolidColorBrush(Colors.Red);
                        txtjudgement.IsEnabled = false;
                        //txtjudgement.Background = new SolidColorBrush(Colors.Red);
                        txtCategory.IsEnabled = false;
                        //txtCategory.Background = new SolidColorBrush(Colors.Red);
                        txtMethod.IsEnabled = false;
                        //txtMethod.Background = new SolidColorBrush(Colors.Red);
                        txtCondition.IsEnabled = false;
                        //txtCondition.Background = new SolidColorBrush(Colors.Red);
                        txtAbnormal.IsEnabled = false;
                        //txtAbnormal.Background = new SolidColorBrush(Colors.Red);
                        cmbCheckSheetSide.IsEnabled = false;
                        dvgMasterDeatils2.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else if (cmbCheckSheetType.SelectedIndex == 3)
                    {
                        Clear();
                        cmbCheckSheetSide.Text = "";
                        txtMethod.IsEnabled = false;
                      //  txtMethod.Background = new SolidColorBrush(Colors.Red);
                        txtCondition.IsEnabled = false;
                        //txtCondition.Background = new SolidColorBrush(Colors.Red);
                        txtAbnormal.IsEnabled = false;
                        //txtAbnormal.Background = new SolidColorBrush(Colors.Red);
                        //

                        txtCategory.IsEnabled = true;
                        //txtCategory.Background = new SolidColorBrush(Colors.White);
                        txtint.IsEnabled = true;
                        //txtint.Background = new SolidColorBrush(Colors.White);
                        txtjudgement.IsEnabled = true;
                       
                        //txtjudgement.Background = new SolidColorBrush(Colors.White);
                        cmbCheckSheetSide.IsEnabled = false;
                        dvgMasterDeatils3.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                    }
                }
                // dvgMasterDeatils.Columns[7].Width = new DataGridLength(1, DataGridLengthUnitType.Star);

            }
            if (Type == "GetMachineNo")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                DataTable dt = obj_BL.BL_MaintenanceCheckSheetMasterDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbMachine, dt, "MachineNo", "MachineNo");
            }          
        }
        private void Clear()
        {
            //cmbMachine.Text = "";
            txtDescription.Text = "";
            txtint.Text = "";
            txtjudgement.Text = "";
            txtCategory.Text = "";
            txtMethod.Text = "";
            txtCondition.Text = "";
            txtAbnormal.Text = "";
            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = "";
            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = "";
            txtRemarks.Text = "";
            imgModel.Source = null;
            imgRefimg.Source = null;
            RefNo = 0;
        }
        #endregion
        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Transaction("GetMachineNo");
                // Transaction("LoadDetails");
                

                ToolTipService.SetShowOnDisabled((DependencyObject)this.btnSave, true);
                ToolTipService.SetShowOnDisabled((DependencyObject)this.btnUpdate, true);
                ToolTipService.SetShowOnDisabled((DependencyObject)this.btnDelete, true);
                ToolTipService.SetShowOnDisabled((DependencyObject)this.btnChecker, true);
                ToolTipService.SetShowOnDisabled((DependencyObject)this.btnApprover, true);

                if (!(CommonClasses.CommonVariable.Rights.Contains("SAVE")))
                {
                    btnSave.IsEnabled = false;
                    this.btnSave.ToolTip = (object)"Access Denied";

                }
                if (!(CommonClasses.CommonVariable.Rights.Contains("UPDATE")))
                {
                    btnUpdate.IsEnabled = false;
                    this.btnUpdate.ToolTip = (object)"Access Denied";

                }
                if (!(CommonClasses.CommonVariable.Rights.Contains("DELETE")))
                {
                    btnDelete.IsEnabled = false;
                    this.btnDelete.ToolTip = (object)"Access Denied";

                }
                if (!(CommonClasses.CommonVariable.Rights.Contains("MAINTENANCE CHECK SHEET MASTER CHECK")))
                {
                    btnChecker.IsEnabled = false;
                    this.btnChecker.ToolTip = (object)"Access Denied";

                }
                if (!(CommonClasses.CommonVariable.Rights.Contains("MAINTENANCE CHECK SHEET MASTER APPROVE")))
                {
                    btnApprover.IsEnabled = false;
                    this.btnApprover.ToolTip = (object)"Access Denied";

                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count == 0 && dvgMasterDeatils1.SelectedItems.Count == 0 && dvgMasterDeatils2.SelectedItems.Count == 0 && dvgMasterDeatils3.SelectedItems.Count == 0)
                {
                    if (ControlValidation())
                    {
                        Transaction("Save");
                    }
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("YOU CAN NOT SAVE THE RECORDS PLEASE GO FOR DELETION OR UPDATION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //0
                if (cmbCheckSheetType.SelectedIndex > -1)
                {
                    if (cmbCheckSheetType.SelectedIndex == 0)
                    {
                        if (dvgMasterDeatils.SelectedItems.Count > 0)
                        {
                            if (dvgMasterDeatils.SelectedItems.Count == 1)
                            {
                                if (ControlValidation())
                                    Transaction("Update");
                            }
                            else
                                CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR UPDATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        }
                        else
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    }
                    //1
                    if (cmbCheckSheetType.SelectedIndex == 1)
                    {
                        if (dvgMasterDeatils1.SelectedItems.Count > 0)
                        {
                            if (dvgMasterDeatils1.SelectedItems.Count == 1)
                            {
                                if (ControlValidation())
                                    Transaction("Update");
                            }
                            else
                                CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR UPDATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        }
                        else
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    }
                    //2
                    if (cmbCheckSheetType.SelectedIndex == 2)
                    {
                        if (dvgMasterDeatils2.SelectedItems.Count > 0)
                        {
                            if (dvgMasterDeatils2.SelectedItems.Count == 1)
                            {
                                if (ControlValidation())
                                    Transaction("Update");
                            }
                            else
                                CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR UPDATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        }
                        else
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    }
                    //3
                    if (cmbCheckSheetType.SelectedIndex == 3)
                    {
                        if (dvgMasterDeatils3.SelectedItems.Count > 0)
                        {
                            if (dvgMasterDeatils3.SelectedItems.Count == 1)
                            {
                                if (ControlValidation())
                                    Transaction("Update");
                            }
                            else
                                CommonClasses.CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR UPDATE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        }
                        else
                            CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    }
                }
                else
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT CHECK SHEET TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //0
                if (cmbCheckSheetType.SelectedIndex == 0)
                {
                    if (dvgMasterDeatils.SelectedItems.Count > 0)
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DeleteConfirm, CommonClasses.CommonVariable.CustomStriing.Question.ToString());
                        if (CommonClasses.CommonVariable.Result == "YES")
                        {
                            for (int i = 0; i < dvgMasterDeatils.SelectedItems.Count; i++)
                            {
                                DataRowView dr = (DataRowView)dvgMasterDeatils.SelectedItems[i];
                                RefNo = Convert.ToInt32(dr["Refno"]);
                                Transaction("Delete");
                            }

                            if (CommonClasses.CommonVariable.Result == "Deleted")
                            {
                                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                                Clear();
                                Transaction("LoadDetails");

                            }
                            else
                            {
                                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                            }
                        }
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //1
                if (cmbCheckSheetType.SelectedIndex == 1)
                {
                    if (dvgMasterDeatils1.SelectedItems.Count > 0)
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DeleteConfirm, CommonClasses.CommonVariable.CustomStriing.Question.ToString());
                        if (CommonClasses.CommonVariable.Result == "YES")
                        {
                            for (int i = 0; i < dvgMasterDeatils1.SelectedItems.Count; i++)
                            {
                                DataRowView dr = (DataRowView)dvgMasterDeatils1.SelectedItems[i];
                                RefNo = Convert.ToInt32(dr["Refno"]);
                                Transaction("Delete");
                            }

                            if (CommonClasses.CommonVariable.Result == "Deleted")
                            {
                                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                                Clear();
                                Transaction("LoadDetails");

                            }
                            else
                            {
                                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                            }
                        }
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
            
                //2
                if (cmbCheckSheetType.SelectedIndex == 2)
                {
                    if (dvgMasterDeatils2.SelectedItems.Count > 0)
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DeleteConfirm, CommonClasses.CommonVariable.CustomStriing.Question.ToString());
                        if (CommonClasses.CommonVariable.Result == "YES")
                        {
                            for (int i = 0; i < dvgMasterDeatils2.SelectedItems.Count; i++)
                            {
                                DataRowView dr = (DataRowView)dvgMasterDeatils2.SelectedItems[i];
                                RefNo = Convert.ToInt32(dr["Refno"]);
                                Transaction("Delete");
                            }

                            if (CommonClasses.CommonVariable.Result == "Deleted")
                            {
                                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                                Clear();
                            }
                            else
                            {
                                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                            }
                        }
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //3
                if (cmbCheckSheetType.SelectedIndex == 3)
                {
                    if (dvgMasterDeatils3.SelectedItems.Count > 0)
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DeleteConfirm, CommonClasses.CommonVariable.CustomStriing.Question.ToString());
                        if (CommonClasses.CommonVariable.Result == "YES")
                        {
                            for (int i = 0; i < dvgMasterDeatils3.SelectedItems.Count; i++)
                            {
                                DataRowView dr = (DataRowView)dvgMasterDeatils3.SelectedItems[i];
                                RefNo = Convert.ToInt32(dr["Refno"]);
                                Transaction("Delete");
                            }

                            if (CommonClasses.CommonVariable.Result == "Deleted")
                            {
                                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                                Clear();
                                Transaction("LoadDetails");

                            }
                            else
                            {
                                CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                            }
                        }
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonClasses.CommonVariable.PageOpenClose = "Close";
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void dvgMasterDeatils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgMasterDeatils.SelectedItems[0];
                    RefNo = Convert.ToInt32(dr["Refno"]);
                    cmbMachine.Text = dr["MachineNo"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txtCategory.Text = dr["Catergory"].ToString();
                    txtCondition.Text = dr["Condition"].ToString();
                    txtint.Text = dr["Int"].ToString();
                    txtMethod.Text = dr["Method"].ToString();
                    txtjudgement.Text = dr["Judgement"].ToString();
                    txtAbnormal.Text = dr["Abnormal"].ToString();
                    cmbCheckSheetSide.Text = dr["CheckSheetSide"].ToString();
                    if (dr["PartImage"].ToString() != "")
                    {
                        byte[] Image = (byte[])dr["PartImage"];

                        //name = dataRow[0].ToString();
                        //bitmapimage = dataRow[1] as byte[];

                        MemoryStream strm = new MemoryStream();
                        strm.Write(Image, 0, Image.Length);
                        strm.Position = 0;
                        System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ms.Seek(0, SeekOrigin.Begin);
                        bi.StreamSource = ms;
                        bi.EndInit();
                        imgModel.Source = bi;

                        //ImageSource imgSrc = bi as ImageSource;
                        //cmbModelName.Focus();
                    }
                    if (dr["RefPartImage"].ToString() != "")
                    {
                        byte[] Image = (byte[])dr["RefPartImage"];

                        //name = dataRow[0].ToString();
                        //bitmapimage = dataRow[1] as byte[];

                        MemoryStream strm = new MemoryStream();
                        strm.Write(Image, 0, Image.Length);
                        strm.Position = 0;
                        System.Drawing.Image img = System.Drawing.Image.FromStream(strm);
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        ms.Seek(0, SeekOrigin.Begin);
                        bi.StreamSource = ms;
                        bi.EndInit();
                        imgRefimg.Source = bi;

                        //ImageSource imgSrc = bi as ImageSource;
                        //cmbModelName.Focus();
                    }
                }
                else if (dvgMasterDeatils.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    txtDescription.Text = "";
                    txtint.Text = "";
                    txtjudgement.Text = "";
                    txtCategory.Text = "";
                    txtMethod.Text = "";
                    txtCondition.Text = "";
                    txtAbnormal.Text = "";
                    //Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                    txtDescription.Text = "";
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
            {
                btnSave_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.U) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.U))
            {
                btnUpdate_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
            {
                btnClear_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
            {
                btnExit_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.D) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.D))
            {
                btnDelete_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.K) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.K))
            {
                BtnChecker_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.A) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.A))
            {
                BtnApprover_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.T) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.T))
            {
                BtnTemplate_Click(sender, e);
            }
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.I) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.I))
            {
                BtnImport_Click(sender, e);
            }
        }
        #endregion
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void CmbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (cmbMachine.SelectedIndex > -1)
            //        Transaction("GetDieName");
            //}
            //catch (Exception ex)
            //{
            //    obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
            //    CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            //}
        }
        private void DvgMasterDeatils1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils1.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgMasterDeatils1.SelectedItems[0];
                    RefNo = Convert.ToInt32(dr["Refno"]);
                    cmbMachine.Text = dr["MachineNo"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txtint.Text = dr["Int"].ToString();
                    txtjudgement.Text = dr["Judgement"].ToString();
                }
                else if (dvgMasterDeatils1.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    txtDescription.Text = "";
                    txtint.Text = "";
                    txtjudgement.Text = "";
                    txtCategory.Text = "";
                    txtMethod.Text = "";
                    txtCondition.Text = "";
                    txtAbnormal.Text = "";
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void DvgMasterDeatils2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils2.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgMasterDeatils2.SelectedItems[0];
                    RefNo = Convert.ToInt32(dr["Refno"]);
                    cmbMachine.Text = dr["MachineNo"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    
                }
                else if (dvgMasterDeatils2.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    txtDescription.Text = "";
                    txtint.Text = "";
                    txtjudgement.Text = "";
                    txtCategory.Text = "";
                    txtMethod.Text = "";
                    txtCondition.Text = "";
                    txtAbnormal.Text = "";
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;

                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void DvgMasterDeatils3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils3.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgMasterDeatils3.SelectedItems[0];
                    RefNo = Convert.ToInt32(dr["Refno"]);
                    cmbMachine.Text = dr["MachineNo"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txtint.Text = dr["Int"].ToString();
                    txtjudgement.Text = dr["Judgement"].ToString();
                    txtCategory.Text = dr["Catergory"].ToString();

                }
                else if (dvgMasterDeatils3.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    txtDescription.Text = "";
                    txtint.Text = "";
                    txtjudgement.Text = "";
                    txtCategory.Text = "";
                    txtMethod.Text = "";
                    txtCondition.Text = "";
                    txtAbnormal.Text = "";
                    imgModel.Source = null;
                    imgRefimg.Source = null;
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void CmbCheckSheetSide_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbCheckSheetSide.SelectedIndex > -1)
                {
                    ComboBoxItem CMD = (ComboBoxItem)cmbCheckSheetSide.SelectedItem;
                    cmbCheckSheetSide.Text = CMD.Content.ToString();
                    Transaction("LoadDetails");
                }

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void BtnimgClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imgModel.Source = null;
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Btnbrowse1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                dlg.ShowDialog();
                if (dlg.FileName != "")
                {
                    imgRefimg.Source = new BitmapImage(new Uri(dlg.FileName));
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void BtnimgClear1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imgRefimg.Source = null;
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void BtnChecker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtRemarks.Text == "")
                {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER CHECKER REMARKS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtDescription.Focus();
                return ;
                }
                
                //0
                if (cmbCheckSheetType.SelectedIndex == 0)
                {
                    if (dvgMasterDeatils.SelectedItems.Count > 0)
                    {
                        
                            for (int i = 0; i < dvgMasterDeatils.SelectedItems.Count; i++)
                            {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = txtRemarks.Text;
                            DataRowView dr = (DataRowView)dvgMasterDeatils.SelectedItems[i];
                                RefNo = Convert.ToInt32(dr["Refno"]);
                                Transaction("Checking");
                            }
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //1
                if (cmbCheckSheetType.SelectedIndex == 1)
                {
                    if (dvgMasterDeatils1.SelectedItems.Count > 0)
                    {
                      
                            for (int i = 0; i < dvgMasterDeatils1.SelectedItems.Count; i++)
                            {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = txtRemarks.Text;
                            DataRowView dr = (DataRowView)dvgMasterDeatils1.SelectedItems[i];
                                RefNo = Convert.ToInt32(dr["Refno"]);
                                Transaction("Checking");
                            }

                            
                        
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //2
                if (cmbCheckSheetType.SelectedIndex == 2)
                {
                    if (dvgMasterDeatils2.SelectedItems.Count > 0)
                    {
                        
                            for (int i = 0; i < dvgMasterDeatils2.SelectedItems.Count; i++)
                            {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = txtRemarks.Text;
                            DataRowView dr = (DataRowView)dvgMasterDeatils2.SelectedItems[i];
                                RefNo = Convert.ToInt32(dr["Refno"]);
                                Transaction("Checking");
                            }  
                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //3
                if (cmbCheckSheetType.SelectedIndex == 3)
                {

                    if (dvgMasterDeatils3.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < dvgMasterDeatils3.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = txtRemarks.Text;
                            DataRowView dr = (DataRowView)dvgMasterDeatils3.SelectedItems[i];
                                RefNo = Convert.ToInt32(dr["Refno"]);
                                Transaction("Checking");
                        }

                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                Transaction("LoadDetails");

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void BtnApprover_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (txtRemarks.Text == "")
                {
                    CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER APPROVER REMARKS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    txtDescription.Focus();
                    return;
                }
                
               // ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = txtRemarks.Text;
                //0
                if (cmbCheckSheetType.SelectedIndex == 0)
                {
                    if (dvgMasterDeatils.SelectedItems.Count > 0)
                    {

                        for (int i = 0; i < dvgMasterDeatils.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = txtRemarks.Text;

                            DataRowView dr = (DataRowView)dvgMasterDeatils.SelectedItems[i];
                            RefNo = Convert.ToInt32(dr["Refno"]);
                            Transaction("Checking");
                        }

                        //if (CommonClasses.CommonVariable.Result == "Saved")
                        //{
                        //    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                        //    Clear();
                        //    Transaction("LoadDetails");

                        //}
                        //else
                        //{
                        //    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        //}

                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //1
                if (cmbCheckSheetType.SelectedIndex == 1)
                {
                    if (dvgMasterDeatils1.SelectedItems.Count > 0)
                    {

                        for (int i = 0; i < dvgMasterDeatils1.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = txtRemarks.Text;

                            DataRowView dr = (DataRowView)dvgMasterDeatils1.SelectedItems[i];
                            RefNo = Convert.ToInt32(dr["Refno"]);
                            Transaction("Checking");
                        }

                        //if (CommonClasses.CommonVariable.Result == "Saved")
                        //{
                        //    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                        //    Clear();
                        //    Transaction("LoadDetails");

                        //}
                        //else
                        //{
                        //    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        //}

                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //2
                if (cmbCheckSheetType.SelectedIndex == 2)
                {
                    if (dvgMasterDeatils2.SelectedItems.Count > 0)
                    {

                        for (int i = 0; i < dvgMasterDeatils2.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = txtRemarks.Text;

                            DataRowView dr = (DataRowView)dvgMasterDeatils2.SelectedItems[i];
                            RefNo = Convert.ToInt32(dr["Refno"]);
                            Transaction("Checking");
                        }

                        //if (CommonClasses.CommonVariable.Result == "Saved")
                        //{
                        //    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataDeleted, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                        //    Clear();
                        //}
                        //else
                        //{
                        //    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                        //}

                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //3
                if (cmbCheckSheetType.SelectedIndex == 3)
                {

                    if (dvgMasterDeatils3.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < dvgMasterDeatils3.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = txtRemarks.Text;

                            DataRowView dr = (DataRowView)dvgMasterDeatils3.SelectedItems[i];
                            RefNo = Convert.ToInt32(dr["Refno"]);
                            Transaction("Checking");
                        }

                        

                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                Transaction("LoadDetails");
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void BtnTemplate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var remote = AppDomain.CurrentDomain.BaseDirectory + "\\TEMPLATE MAINTENANCE CHECKSHEET MASTER.xlsx";
               // var local = @"D:\TEMPLATE MAINTENANCE CHECKSHEET MASTER.xlsx";
                //File.Copy(remote, local, true);
                Microsoft.Office.Interop.Excel.Application xlApp;
                Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlApp.Visible = true;

                xlApp.DisplayAlerts = false;
                xlWorkBook = xlApp.Workbooks.Open(remote, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog obj_OP = new OpenFileDialog();
                obj_OP.Filter = "Excel files (*.xlsx)|*.xlsx|Excel files(*.xls)|*.xls";
                obj_OP.ShowDialog();
                if (obj_OP.FileName != "")
                {
                    if (cmbCheckSheetType.SelectedIndex > -1)
                    {
                        ComboBoxItem CMD = (ComboBoxItem)cmbCheckSheetType.SelectedItem;
                        cmbCheckSheetType.Text = CMD.Content.ToString();
                        if (cmbCheckSheetType.Text== "DAILY CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[DAILY CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "ANNUAL CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[ANNUAL CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "NEEDLE CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[NEEDLE CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "PM CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[PM CHECK SHEET$]");
                        }

                        // int Count = dt.Columns.Count;
                        //dt.Columns.Add("REFNO");
                        if (dt.Rows.Count > 0)
                            Transaction("Save");
                        else
                            CommonClasses.CommonMethods.MessageBoxShow("DATA NOT FOUND TO IMPORT", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    }
                    else
                    {
                        CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT CHECK SHEET TYPE BEFORE IMPORTING", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
