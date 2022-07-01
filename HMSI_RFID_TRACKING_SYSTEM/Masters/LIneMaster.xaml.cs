using System;
using System.Collections.Generic;
using System.Data;
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
using SATOPrinterAPI;
using MOLDING_INTEGRATION_SYSTEM.CommonClasses;

namespace MOLDING_INTEGRATION_SYSTEM.Masters
{
    /// <summary>
    /// Interaction logic for LineMaster.xaml
    /// </summary>
    public partial class LineMaster : Page
    {
        public LineMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        int RefNo = 0;
        string PrnData = "", PrinterIP = "";
        #endregion

        private bool ControlValidation()
        {
            if (this.cmblineName.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT OR ENTER LINE NAME", CommonVariable.CustomStriing.Information.ToString());
                this.cmblineName.Focus();
                return false;
            }
            if (this.txtMachineName.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER MACHINE NAME", CommonVariable.CustomStriing.Information.ToString());
                this.txtMachineName.Focus();
                return false;
            }
            if (this.txtMachineNo.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER MACHINE NO", CommonVariable.CustomStriing.Information.ToString());
                this.txtMachineNo.Focus();
                return false;
            }
            if (this.cmbCategory.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT PM CATEGORY", CommonVariable.CustomStriing.Information.ToString());
                this.cmbCategory.Focus();
                return false;
            }
            if (this.cmbCategory.SelectedIndex == 0)
            {
                if (this.dtpPmdate.Text == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER PM DATE", CommonVariable.CustomStriing.Information.ToString());
                    this.dtpPmdate.Focus();
                    return false;
                }
                if (this.dtpWarningDate.Text == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER WARNNING DATE", CommonVariable.CustomStriing.Information.ToString());
                    this.dtpWarningDate.Focus();
                    return false;
                }
            }
            if (this.cmbCategory.SelectedIndex == 1)
            {
                if (this.txtPMQty.Text == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER PM QTY", CommonVariable.CustomStriing.Information.ToString());
                    this.txtPMQty.Focus();
                    return false;
                }
                if (this.txtWarningQty.Text == "")
                {
                    CommonMethods.MessageBoxShow("PLEASE ENTER WARNNING QTY", CommonVariable.CustomStriing.Information.ToString());
                    this.txtWarningQty.Focus();
                    return false;
                }
                if (this.txtPMQty.Text == "0")
                {
                    CommonMethods.MessageBoxShow("INVALID PM QTY", CommonVariable.CustomStriing.Information.ToString());
                    this.txtPMQty.Focus();
                    return false;
                }
                if (this.txtWarningQty.Text == "0")
                {
                    CommonMethods.MessageBoxShow("INVALID WARNNING QTY", CommonVariable.CustomStriing.Information.ToString());
                    this.txtWarningQty.Focus();
                    return false;
                }
                if (Convert.ToInt32(this.txtPMQty.Text) < Convert.ToInt32(this.txtWarningQty.Text))
                {
                    CommonMethods.MessageBoxShow("WARNING QTY SHOULD BE LESS THAN PM QTY", CommonVariable.CustomStriing.Information.ToString());
                    this.txtWarningQty.Focus();
                    return false;
                }
                if (Convert.ToInt32(this.txtPMQty.Text) == Convert.ToInt32(this.txtWarningQty.Text))
                {
                    CommonMethods.MessageBoxShow("WARNING QTY AND PM QTY SHOULD NOT BE SAME", CommonVariable.CustomStriing.Information.ToString());
                    this.txtWarningQty.Focus();
                    return false;
                }
            }
            return true;
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = this.cmblineName.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineName = this.txtMachineName.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = this.txtMachineNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PMCategory = this.cmbCategory.Text;
                if (this.cmbCategory.SelectedIndex == 0)
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.PMData = this.dtpPmdate.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningData = this.dtpWarningDate.Text;
                }
                if (this.cmbCategory.SelectedIndex == 1)
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.PMData = this.txtPMQty.Text;
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningData = this.txtWarningQty.Text;
                }
                bool? isChecked = this.chkstatus.IsChecked;
                bool flag = true;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Status = !(isChecked.GetValueOrDefault() == flag & isChecked.HasValue) ? "InActive" : "Active";
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = this.RefNo;
                CommonVariable.Result = this.obj_BL.BL_MachineMasterTransaction();
                if (CommonVariable.Result == "Saved")
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DataSaved, CommonVariable.CustomStriing.Successfull.ToString());
                    this.Clear();
                }
                else if (CommonVariable.Result == "Updated")
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DataUpdated, CommonVariable.CustomStriing.Successfull.ToString());
                    this.Clear();
                }
                else if (CommonVariable.Result == "Duplicate")
                    CommonMethods.MessageBoxShow(CommonVariable.Duplicate, CommonVariable.CustomStriing.Information.ToString());
                else if (CommonVariable.Result != "Deleted")
                    CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
            }
            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                this.dvgMasterDeatils.ItemsSource = this.obj_BL.BL_MachineMasterDetails().Tables[0].DefaultView;
                this.dvgMasterDeatils.Columns[7].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            }
            if (Type == "GetPRN")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                DataSet dt = this.obj_BL.BL_MachineMasterDetails();
                PrnData = dt.Tables[0].Rows[0]["Prndata"].ToString();
                PrinterIP = dt.Tables[0].Rows[0]["PrinterIP"].ToString();
            }
            if (!(Type == "GetLineName"))
                return;
            ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
            CommonMethods.FillComboBox(this.cmblineName, this.obj_BL.BL_MachineMasterDetails().Tables[0], "LineName", "LineName");
            
            
        }

        private void Clear()
        {
            this.cmblineName.Text = "";
            this.txtMachineName.Text = "";
            this.txtMachineNo.Text = "";
            this.txtPMQty.Text = "";
            this.txtWarningQty.Text = "";
            this.cmbCategory.Text = "";
            this.txtPMQty.Text = "";
            this.txtWarningQty.Text = "";
            this.dtpPmdate.Text = "";
            this.dtpWarningDate.Text = "";
            this.dtpPmdate.Visibility = Visibility.Hidden;
            this.dtpWarningDate.Visibility = Visibility.Hidden;
            this.txtWarningQty.Visibility = Visibility.Hidden;
            this.txtPMQty.Visibility = Visibility.Hidden;
            this.Transaction("LoadDetails");
            this.cmblineName.Focus();
            this.RefNo = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
                this.Transaction("GetLineName");
                this.dtpPmdate.Visibility = Visibility.Hidden;
                this.dtpWarningDate.Visibility = Visibility.Hidden;
                this.txtWarningQty.Visibility = Visibility.Hidden;
                this.txtPMQty.Visibility = Visibility.Hidden;


                ToolTipService.SetShowOnDisabled((DependencyObject)this.btnSave, true);
                ToolTipService.SetShowOnDisabled((DependencyObject)this.btnUpdate, true);
                ToolTipService.SetShowOnDisabled((DependencyObject)this.btnDelete, true);

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
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.S) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.S))
                this.btnSave_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.U) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.U))
                this.btnUpdate_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.C) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.C))
                this.btnClear_Click(sender, (RoutedEventArgs)e);
            if (Keyboard.IsKeyDown(Key.LeftAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.RightAlt) && Keyboard.IsKeyDown(Key.B) || Keyboard.IsKeyDown(Key.Escape) && Keyboard.IsKeyDown(Key.Escape))
                this.btnExit_Click(sender, (RoutedEventArgs)e);
            if ((!Keyboard.IsKeyDown(Key.LeftAlt) || !Keyboard.IsKeyDown(Key.D)) && (!Keyboard.IsKeyDown(Key.RightAlt) || !Keyboard.IsKeyDown(Key.D)))
                return;
            this.btnDelete_Click(sender, (RoutedEventArgs)e);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Clear();
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count == 0)
                {
                    if (!this.ControlValidation())
                        return;
                    this.Transaction("Save");
                }
                else
                    CommonMethods.MessageBoxShow("YOU CAN NOT SAVE THE RECORDS PLEASE GO FOR DELETION OR UPDATION", CommonVariable.CustomStriing.Information.ToString());
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    if (this.dvgMasterDeatils.SelectedItems.Count == 1)
                    {
                        if (!this.ControlValidation())
                            return;
                        this.Transaction("Update");
                    }
                    else
                        CommonMethods.MessageBoxShow("MULTIPLE SELECTION WILL NOT SUPPORT FOR UPDATE", CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonMethods.MessageBoxShow(CommonVariable.RowSelection, CommonVariable.CustomStriing.Information.ToString());
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    CommonMethods.MessageBoxShow(CommonVariable.DeleteConfirm, CommonVariable.CustomStriing.Question.ToString());
                    if (!(CommonVariable.Result == "YES"))
                        return;
                    for (int index = 0; index < this.dvgMasterDeatils.SelectedItems.Count; ++index)
                    {
                        this.RefNo = Convert.ToInt32(((DataRowView)this.dvgMasterDeatils.SelectedItems[index])["Refno"]);
                        this.Transaction("Delete");
                    }
                    if (CommonVariable.Result == "Deleted")
                    {
                        CommonMethods.MessageBoxShow(CommonVariable.DataDeleted, CommonVariable.CustomStriing.Successfull.ToString());
                        this.Clear();
                    }
                    else
                        CommonMethods.MessageBoxShow(CommonVariable.Result, CommonVariable.CustomStriing.Information.ToString());
                }
                else
                    CommonMethods.MessageBoxShow(CommonVariable.RowSelection, CommonVariable.CustomStriing.Information.ToString());
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Clear();
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void dvgMasterDeatils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count == 1)
                {
                    this.dtpPmdate.Visibility = Visibility.Hidden;
                    this.dtpWarningDate.Visibility = Visibility.Hidden;
                    this.txtWarningQty.Visibility = Visibility.Hidden;
                    this.txtPMQty.Visibility = Visibility.Hidden;
                    DataRowView selectedItem = (DataRowView)this.dvgMasterDeatils.SelectedItems[0];
                    this.RefNo = Convert.ToInt32(selectedItem["Refno"]);
                    this.cmblineName.Text = selectedItem["LineName"].ToString();
                    this.txtMachineName.Text = selectedItem["MachineName"].ToString();
                    this.txtMachineNo.Text = selectedItem["MachineNo"].ToString();
                    this.cmbCategory.Text = selectedItem["PMCategory"].ToString();
                    if (this.cmbCategory.SelectedIndex == 0)
                    {
                        this.dtpPmdate.Text = selectedItem["PMData"].ToString();
                        this.dtpWarningDate.Text = selectedItem["PMWarningData"].ToString();
                        this.dtpPmdate.Visibility = Visibility.Visible;
                        this.dtpWarningDate.Visibility = Visibility.Visible;
                    }
                    if (this.cmbCategory.SelectedIndex == 1)
                    {
                        this.txtPMQty.Text = selectedItem["PMData"].ToString();
                        this.txtWarningQty.Text = selectedItem["PMWarningData"].ToString();
                        this.txtWarningQty.Visibility = Visibility.Visible;
                        this.txtPMQty.Visibility = Visibility.Visible;
                    }
                    if (selectedItem["Status"].ToString() == "Active")
                        this.chkstatus.IsChecked = new bool?(true);
                    else
                        this.chkstatus.IsChecked = new bool?(false);
                    this.cmblineName.Focus();
                }
                else
                {
                    this.RefNo = 0;
                    this.cmblineName.Focus();
                }
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CommonVariable.PageOpenClose = "Close";
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.dtpPmdate.Visibility = Visibility.Hidden;
                this.dtpWarningDate.Visibility = Visibility.Hidden;
                this.txtWarningQty.Visibility = Visibility.Hidden;
                this.txtPMQty.Visibility = Visibility.Hidden;
                if (this.cmbCategory.SelectedIndex == 0)
                {
                    this.dtpPmdate.Visibility = Visibility.Visible;
                    this.dtpWarningDate.Visibility = Visibility.Visible;
                }
                if (this.cmbCategory.SelectedIndex != 1)
                    return;
                this.txtWarningQty.Visibility = Visibility.Visible;
                this.txtPMQty.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtPMQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonMethods.NumericValue(e);
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void TxtWarningQty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonMethods.NumericValue(e);
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void PrintLable(string var1, string var2, string Barcode)
        {
            SatoPrinter p = new SatoPrinter(SATOPrinterAPI.Printer.InterfaceType.TCPIP, PrinterIP, "9100");
            if (p.GetprinterStatus().StartsWith("OK"))
            {
                string sbpl = null;
                sbpl = PrnData;
                sbpl = sbpl.Replace("{var1}", var1);
                sbpl = sbpl.Replace("{var2}", var2);
                sbpl = sbpl.Replace("{Len}", Barcode.Length.ToString());
                sbpl = sbpl.Replace("{Barcode}", Barcode);
                p.Print_TCPIP(sbpl, PrinterIP, "9100");
            }
            else
            {
                MessageBox.Show(p.GetprinterStatus().ToString(), "Printer Status");
            }
        }
        private void BtnPrintbarcode_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("GetPRN");

                if (this.dvgMasterDeatils.SelectedItems.Count > 0)
                {
                    for (int index = 0; index < this.dvgMasterDeatils.SelectedItems.Count; ++index)
                    {
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = ((DataRowView)this.dvgMasterDeatils.SelectedItems[index])["MachineNo"].ToString();
                        PrintLable("", ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo);
                    }
                    CommonMethods.MessageBoxShow(CommonVariable.Result, "Printed Successfully.");
                }
                else
                    CommonMethods.MessageBoxShow(CommonVariable.RowSelection, CommonVariable.CustomStriing.Information.ToString());

            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MACHINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
