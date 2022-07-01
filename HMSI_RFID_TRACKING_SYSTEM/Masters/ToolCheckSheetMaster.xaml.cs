using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for CheckSheetMaster.xaml
    /// </summary>
    public partial class ToolCheckSheetMaster : Page
    {
        public ToolCheckSheetMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        int RefNo = 0;
        DataTable dt = new DataTable();
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
            if (cmbDieName.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT DIE NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbDieName.Focus();
                return false;
            }
            if (cmbCheckSheetType.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT CHECK SHEET TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbCheckSheetType.Focus();
                return false;
            }
            if (txtDescription.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER CHECK SHEET DESCRIPTION", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtDescription.Focus();
                return false;
            }
            return true;
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete" || Type == "Checking")
            {
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "TOOL ROOM DAILY CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString(); 
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = dt.Rows[i]["DieName"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = dt.Rows[i]["Catergory"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType = dt.Rows[i]["CheckSheetSide"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Online = dt.Rows[i]["online"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Int = dt.Rows[i]["int"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Method = dt.Rows[i]["method"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Who = dt.Rows[i]["Who"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment = dt.Rows[i][" judgement"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal = dt.Rows[i]["Abnormal"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Offline = dt.Rows[i]["offline"].ToString();
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "TOOL ROOM DIE CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = dt.Rows[i]["DieName"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = dt.Rows[i]["Catergory"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType = dt.Rows[i]["CheckSheetSide"].ToString();                                                      
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Method = dt.Rows[i]["method"].ToString();                           
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment = dt.Rows[i][" Judgement"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal = dt.Rows[i]["Abnormal"].ToString();
                            
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "TOOL ROOM PM CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = dt.Rows[i]["DieName"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Timetopm = dt.Rows[i]["TimeToPM"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Int = dt.Rows[i]["int"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Method = dt.Rows[i]["method"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Toll = dt.Rows[i]["toll"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment = dt.Rows[i][" Judgement"].ToString();
                            
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "TOOL ROOM QUALITY DAILY CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = dt.Rows[i]["DieName"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString(); ;
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString(); ;                            
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Toll = dt.Rows[i]["toll"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Standard = dt.Rows[i][" Standard"].ToString();
                        }
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = RefNo;
                        CommonClasses.CommonVariable.Result = obj_BL.BL_ToolCheckSheetMasterTransaction();
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
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = cmbDieName.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = cmbCheckSheetType.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = txtDescription.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = cmbCategory.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType = cmbCheckSheetSide.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Online = txtonline.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Int = txtint.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Method = txtmethod.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Who = txtwho.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment = txtjudgement.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal = txtAbnormal.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Toll = txttoll.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Timetopm = txttimetopm.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Offline = txtOffline.Text;
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
                    if (refimgModel.Source != null)
                    {
                        byte[] buffer;
                        var bitmap = refimgModel.Source as BitmapSource;
                        var encoder = new PngBitmapEncoder(); // or one of the other encoders
                        encoder.Frames.Add(BitmapFrame.Create(bitmap));

                        using (var stream = new MemoryStream())
                        {
                            encoder.Save(stream);
                            buffer = stream.ToArray();
                        }
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage = buffer;
                    }
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = RefNo;
                    CommonClasses.CommonVariable.Result = obj_BL.BL_ToolCheckSheetMasterTransaction();
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
                    Clear();
                }
                }
                if (Type == "LoadDetails")
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = cmbCheckSheetType.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = cmbMachine.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = cmbDieName.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = cmbCategory.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType = cmbCheckSheetSide.Text;


                    if (cmbCheckSheetType.SelectedIndex != -1)
                    {
                        DataTable dt = obj_BL.BL_ToolCheckSheetMasterDetails().Tables[0];
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
                    }
                    if (cmbCheckSheetType.SelectedIndex == 0)
                    {
                        Clear();
                        cmbCategory.IsEnabled = true;
                        cmbCheckSheetSide.IsEnabled = true;
                        txtonline.IsEnabled = true;
                        txtOffline.IsEnabled = true;
                        // txtonline.Background = new SolidColorBrush(Colors.White);
                        txtint.IsEnabled = true;
                        //txtint.Background = new SolidColorBrush(Colors.White);
                        txtmethod.IsEnabled = true;
                        //txtmethod.Background = new SolidColorBrush(Colors.White);
                        txtjudgement.IsEnabled = true;
                        //txtjudgement.Background = new SolidColorBrush(Colors.White);
                        txtAbnormal.IsEnabled = true;
                        //txtAbnormal.Background = new SolidColorBrush(Colors.White);
                        txtwho.IsEnabled = true;
                        //txtwho.Background = new SolidColorBrush(Colors.White);
                        //txtCondition.IsEnabled = true;
                        //txtCondition.Background = new SolidColorBrush(Colors.White);
                        btnbrowse.Visibility = System.Windows.Visibility.Visible;
                        txttoll.IsEnabled = false;
                        // txttoll.Background = new SolidColorBrush(Colors.Red);
                        txttimetopm.IsEnabled = false;
                        //txttimetopm.Background = new SolidColorBrush(Colors.Red);



                        dvgMasterDeatils.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else if (cmbCheckSheetType.SelectedIndex == 1)
                    {
                        Clear();
                        cmbCategory.IsEnabled = true;
                        // cmbCategory.Background = new SolidColorBrush(Colors.White);
                        cmbCheckSheetSide.IsEnabled = true;
                        //cmbCheckSheetSide.Background = new SolidColorBrush(Colors.White);
                        txtmethod.IsEnabled = true;
                        //txtmethod.Background = new SolidColorBrush(Colors.White);
                        txtjudgement.IsEnabled = true;
                        //txtjudgement.Background = new SolidColorBrush(Colors.White);
                        txtAbnormal.IsEnabled = true;
                        //txtAbnormal.Background = new SolidColorBrush(Colors.White);
                        txtonline.IsEnabled = false;
                        txtOffline.IsEnabled = false;

                        //txtonline.Background = new SolidColorBrush(Colors.Red);
                        txtint.IsEnabled = false;
                        //txtint.Background = new SolidColorBrush(Colors.Red);
                        txtwho.IsEnabled = false;
                        //txtwho.Background = new SolidColorBrush(Colors.Red);
                        txttoll.IsEnabled = false;
                        //txttoll.Background = new SolidColorBrush(Colors.Red);
                        txttimetopm.IsEnabled = false;
                        //txttimetopm.Background = new SolidColorBrush(Colors.Red);
                        dvgMasterDeatils1.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                        btnbrowse.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else if (cmbCheckSheetType.SelectedIndex == 2)
                    {
                        Clear();
                        // [TimeToPM],[Int],[Method],[Toll],[Judgement]
                        txttimetopm.IsEnabled = true;
                        // txttimetopm.Background = new SolidColorBrush(Colors.White);
                        txtint.IsEnabled = true;
                        // txtint.Background = new SolidColorBrush(Colors.White);
                        txtmethod.IsEnabled = true;
                        // txtmethod.Background = new SolidColorBrush(Colors.White);
                        txttoll.IsEnabled = true;
                        //txttoll.Background = new SolidColorBrush(Colors.White);
                        txtjudgement.IsEnabled = true;
                        // txtjudgement.Background = new SolidColorBrush(Colors.White);
                        btnbrowse.Visibility = System.Windows.Visibility.Visible;
                        // [online],[who],[abnormal],checksheetside

                        txtonline.IsEnabled = false;
                        txtOffline.IsEnabled = false;

                        //txtonline.Background = new SolidColorBrush(Colors.Red);
                        txtwho.IsEnabled = false;
                        //txtwho.Background = new SolidColorBrush(Colors.Red);
                        txtAbnormal.IsEnabled = false;
                        //txtAbnormal.Background = new SolidColorBrush(Colors.Red);
                        cmbCheckSheetSide.IsEnabled = false;
                        //cmbCheckSheetSide.Background = new SolidColorBrush(Colors.Red);
                        cmbCategory.IsEnabled = false;
                        //cmbCategory.Background = new SolidColorBrush(Colors.Red);

                        //txtCondition.IsEnabled = false;
                        //txtCondition.Background = new SolidColorBrush(Colors.Red);
                        dvgMasterDeatils2.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    }
                    else if (cmbCheckSheetType.SelectedIndex == 3)
                    {
                        Clear();
                        txttoll.IsEnabled = true;
                        //txttoll.Background = new SolidColorBrush(Colors.White);
                        txtmethod.IsEnabled = true;
                        //txtmethod.Background = new SolidColorBrush(Colors.White);
                        //
                        txtonline.IsEnabled = false;
                        txtOffline.IsEnabled = false;

                        //txtonline.Background = new SolidColorBrush(Colors.Red);
                        cmbCheckSheetSide.IsEnabled = false;
                        //cmbCheckSheetSide.Background = new SolidColorBrush(Colors.Red);
                        cmbCategory.IsEnabled = false;
                        //cmbCategory.Background = new SolidColorBrush(Colors.Red);
                        txtint.IsEnabled = false;
                        //txtint.Background = new SolidColorBrush(Colors.Red);

                        txtwho.IsEnabled = false;
                        //txtwho.Background = new SolidColorBrush(Colors.Red);
                        txtjudgement.IsEnabled = false;
                        //txtjudgement.Background = new SolidColorBrush(Colors.Red);
                        txtAbnormal.IsEnabled = false;
                        //txtAbnormal.Background = new SolidColorBrush(Colors.Red);
                        txttimetopm.IsEnabled = false;
                        //txttimetopm.Background = new SolidColorBrush(Colors.Red);
                        btnbrowse.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils3.Visibility = System.Windows.Visibility.Visible;
                        dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                        dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                    }

                }
                if (Type == "GetMachineNo")
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                    DataTable dt = obj_BL.BL_ToolCheckSheetMasterDetails().Tables[0];
                    CommonClasses.CommonMethods.FillComboBox(cmbMachine, dt, "MachineNo", "MachineNo");
                }
                if (Type == "GetDieName")
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = cmbMachine.SelectedValue.ToString();
                    DataTable dt = obj_BL.BL_ToolCheckSheetMasterDetails().Tables[0];
                    CommonClasses.CommonMethods.FillComboBox(cmbDieName, dt, "DieName", "DieName");
                }
            
        }
        private void Clear()
        {
           // cmbMachine.Text = "";
            //cmbDieName.Text = "";
            cmbCategory.Text = "";
            cmbCheckSheetSide.Text = "";
            txtDescription.Text = "";
            txtonline.Text = "";
            txtint.Text = "";
            txtmethod.Text = "";
            txtwho.Text = "";
            txtjudgement.Text = "";
            txtAbnormal.Text = "";
            txttoll.Text = "";
            txttimetopm.Text = "";
            txtOffline.Text = "";
            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = "";
            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = "";
            txtRemarks.Text = "";
            imgModel.Source = null;
            refimgModel.Source = null;
            RefNo = 0;
        }
        #endregion
        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Transaction("GetMachineNo");



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
                if (!(CommonClasses.CommonVariable.Rights.Contains("TOOL ROOM CHECK SHEET MASTER CHECK")))
                {
                    btnChecker.IsEnabled = false;
                    this.btnChecker.ToolTip = (object)"Access Denied";

                }
                if (!(CommonClasses.CommonVariable.Rights.Contains("TOOL ROOM CHECK SHEET MASTER APPROVE")))
                {
                    btnApprover.IsEnabled = false;
                    this.btnApprover.ToolTip = (object)"Access Denied";

                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                                Transaction("LoadDetails");

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
                                Transaction("LoadDetails");

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
                                Transaction("LoadDetails");

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
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    cmbDieName.Text = dr["DieName"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    cmbCategory.Text = dr["Catergory"].ToString();
                    cmbCheckSheetSide.Text = dr["CheckSheetSide"].ToString();
                    txtonline.Text = dr["Online"].ToString();
                    txtOffline.Text = dr["offline"].ToString();
                    txtint.Text = dr["int"].ToString();
                    txtmethod.Text = dr["method"].ToString();
                    txtwho.Text = dr["who"].ToString();
                    txtjudgement.Text = dr["judgement"].ToString();
                    txtAbnormal.Text = dr["Abnormal"].ToString();
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
                        refimgModel.Source = bi;

                        //ImageSource imgSrc = bi as ImageSource;
                        //cmbModelName.Focus();
                    }
                }
                else if (dvgMasterDeatils.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    cmbDieName.Text = "";
                    cmbCategory.Text = "";
                    cmbCheckSheetSide.Text = "";
                    txtDescription.Text = "";
                    txtonline.Text = "";
                    txtint.Text = "";
                    txtmethod.Text = "";
                    txtwho.Text = "";
                    txtjudgement.Text = "";
                    txtAbnormal.Text = "";
                    txttoll.Text = "";
                    txttimetopm.Text = "";
                    txtOffline.Text = "";

                    imgModel.Source = null;
                    refimgModel.Source = null;
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
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
                    cmbDieName.Text = dr["DieName"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    cmbCategory.Text = dr["Catergory"].ToString();
                    cmbCheckSheetSide.Text = dr["CheckSheetSide"].ToString();
                    txtmethod.Text = dr["Method"].ToString();
                    txtjudgement.Text = dr["Judgement"].ToString();
                    txtAbnormal.Text = dr["Abnormal"].ToString();
                }
                else if (dvgMasterDeatils1.SelectedItems.Count > 1)
                {
                    
                    cmbMachine.Text = "";
                    cmbDieName.Text = "";
                    cmbCategory.Text = "";
                    cmbCheckSheetSide.Text = "";
                    txtDescription.Text = "";
                    txtonline.Text = "";
                    txtint.Text = "";
                    txtmethod.Text = "";
                    txtwho.Text = "";
                    txtjudgement.Text = "";
                    txtAbnormal.Text = "";
                    txttoll.Text = "";
                    txttimetopm.Text = "";
                    txtOffline.Text = "";

                    imgModel.Source = null;
                    refimgModel.Source = null;
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    cmbDieName.Text = dr["DieName"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txttimetopm.Text = dr["TimeToPM"].ToString();
                    txtint.Text = dr["Int"].ToString();
                    txtmethod.Text = dr["Method"].ToString();
                    txttoll.Text = dr["Toll"].ToString();
                    txtjudgement.Text = dr["Judgement"].ToString();
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
                        refimgModel.Source = bi;

                        //ImageSource imgSrc = bi as ImageSource;
                        //cmbModelName.Focus();
                    }
                }
                else if (dvgMasterDeatils2.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    cmbDieName.Text = "";
                    cmbCategory.Text = "";
                    cmbCheckSheetSide.Text = "";
                    txtDescription.Text = "";
                    txtonline.Text = "";
                    txtint.Text = "";
                    txtmethod.Text = "";
                    txtwho.Text = "";
                    txtjudgement.Text = "";
                    txtAbnormal.Text = "";
                    txttoll.Text = "";
                    txttimetopm.Text = "";
                    imgModel.Source = null;
                    refimgModel.Source = null;
                    txtOffline.Text = "";

                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;

                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    cmbDieName.Text = dr["DieName"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txttoll.Text = dr["Toll"].ToString();
                    txtmethod.Text = dr["Standard"].ToString();
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
                        refimgModel.Source = bi;

                        //ImageSource imgSrc = bi as ImageSource;
                        //cmbModelName.Focus();
                    }
                }
                else if (dvgMasterDeatils3.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    cmbDieName.Text = "";
                    cmbCategory.Text = "";
                    cmbCheckSheetSide.Text = "";
                    txtDescription.Text = "";
                    txtonline.Text = "";
                    txtint.Text = "";
                    txtmethod.Text = "";
                    txtwho.Text = "";
                    txtjudgement.Text = "";
                    txtAbnormal.Text = "";
                    txttoll.Text = "";
                    txttimetopm.Text = "";
                    imgModel.Source = null;
                    refimgModel.Source = null;
                    txtOffline.Text = "";

                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void CmbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbMachine.SelectedIndex > -1)
                {
                    Transaction("GetDieName");
                }

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void CmbDieName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbDieName.SelectedIndex > -1)
                {
                    DataRowView CMD = (DataRowView)cmbDieName.SelectedItem;
                    cmbDieName.Text = CMD[0].ToString();
                    Transaction("LoadDetails");
                }

            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void CmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (cmbCategory.SelectedIndex > -1)
            //    {
            //        ComboBoxItem CMD = (ComboBoxItem)cmbCategory.SelectedItem;
            //        cmbCategory.Text = CMD.Content.ToString();
            //        Transaction("LoadDetails");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
            //    CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            //}
        }
        private void CmbCheckSheetSide_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    if (cmbCheckSheetSide.SelectedIndex > -1)
            //    {
            //        ComboBoxItem CMD = (ComboBoxItem)cmbCheckSheetSide.SelectedItem;
            //        cmbCheckSheetSide.Text = CMD.Content.ToString();
            //        Transaction("LoadDetails");
            //    }

            //}
            //catch (Exception ex)
            //{
            //    obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
            //    CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            //}
        }
        private void BtnimgClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                imgModel.Source = null;
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    refimgModel.Source = new BitmapImage(new Uri(dlg.FileName));
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void BtnrefimgClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                refimgModel.Source = null;
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_ROOM_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    return;
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "TOOL_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void BtnTemplate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var remote = AppDomain.CurrentDomain.BaseDirectory + "\\TEMPLATE TOOLROOM CHECK SHEET MASTER.xlsx";
                //var local = @"D:\TEMPLATE TOOLROOM CHECK SHEET MASTER.xlsx";
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
                        if (cmbCheckSheetType.Text == "TOOL ROOM DAILY CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[TOOL ROOM DAILY CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "TOOL ROOM DIE CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[TOOL ROOM DIE CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "TOOL ROOM PM CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[TOOL ROOM PM CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "TOOL ROOM QUALITY DAILY CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[TOOL ROOM QUALITY DAILY CHECK$]");
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
