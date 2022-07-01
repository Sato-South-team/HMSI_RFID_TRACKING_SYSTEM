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
using MOLDING_INTEGRATION_SYSTEM.CommonClasses;

namespace MOLDING_INTEGRATION_SYSTEM.Masters
{
    /// <summary>
    /// Interaction logic for DieMaster.xaml
    /// </summary>
    public partial class DieMaster : Page
    {
        public DieMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        string PrnData = "", PrinterIP = "";
        int RefNo = 0;
        #endregion

        private bool ControlValidation()
        {
            if (this.cmbModelName.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT MODEL NAME", CommonVariable.CustomStriing.Information.ToString());
                this.cmbModelName.Focus();
                return false;
            }
            if (this.cmbDieName.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT DIE NAME", CommonVariable.CustomStriing.Information.ToString());
                this.cmbDieName.Focus();
                return false;
            }
            if (this.txtdieno.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER DIE NO", CommonVariable.CustomStriing.Information.ToString());
                this.txtdieno.Focus();
                return false;
            }
            if (this.txtpmqty.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER PM QTY", CommonVariable.CustomStriing.Information.ToString());
                this.txtpmqty.Focus();
                return false;
            }
            if (Convert.ToInt32(this.txtpmqty.Text) <= 0)
            {
                CommonMethods.MessageBoxShow("INVALID PM QTY", CommonVariable.CustomStriing.Information.ToString());
                this.txtpmqty.Focus();
                return false;
            }
            if (this.txtwarningqty.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER WARNING QTY", CommonVariable.CustomStriing.Information.ToString());
                this.txtwarningqty.Focus();
                return false;
            }
            if (Convert.ToInt32(this.txtwarningqty.Text) <= 0)
            {
                CommonMethods.MessageBoxShow("INVALID WARNING QTY", CommonVariable.CustomStriing.Information.ToString());
                this.txtwarningqty.Focus();
                return false;
            }
            if (Convert.ToInt32(this.txtpmqty.Text) < Convert.ToInt32(this.txtwarningqty.Text))
            {
                CommonMethods.MessageBoxShow("WARNING QTY SHOULD BE LESS THAN PM QTY", CommonVariable.CustomStriing.Information.ToString());
                this.txtwarningqty.Focus();
                return false;
            }
            if (Convert.ToInt32(this.txtpmqty.Text) == Convert.ToInt32(this.txtwarningqty.Text))
            {
                CommonMethods.MessageBoxShow("WARNING QTY AND PM QTY SHOULD NOT BE SAME", CommonVariable.CustomStriing.Information.ToString());
                this.txtwarningqty.Focus();
                return false;
            }
            if (!(this.txtwarningdays.Text == ""))
                return true;
            CommonMethods.MessageBoxShow("WARNING DAYS SHOULD NOT BE EMPTY", CommonVariable.CustomStriing.Information.ToString());
            this.txtwarningqty.Focus();
            return false;
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = this.cmbModelName.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = this.cmbDieName.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo = this.txtdieno.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PMQty = this.txtpmqty.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningQty = this.txtwarningqty.Text;
                bool? isChecked = this.chkstatus.IsChecked;
                bool flag = true;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Status = !(isChecked.GetValueOrDefault() == flag & isChecked.HasValue) ? "InActive" : "Active";
                ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningData = this.txtwarningdays.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = this.RefNo;
                CommonVariable.Result = this.obj_BL.BL_DieMasterTransaction();
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
                if (cmbModelName.SelectedIndex != -1)
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = this.cmbModelName.SelectedValue.ToString();
                }
                this.dvgMasterDeatils.ItemsSource = this.obj_BL.BL_DieMasterDetails().Tables[0].DefaultView;
                this.dvgMasterDeatils.Columns[7].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = "";
            }
            if (Type == "GetModelName")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                CommonMethods.FillComboBox(this.cmbModelName, this.obj_BL.BL_DieMasterDetails().Tables[0], "ModelName", "ModelName");
            }
            if (Type == "GetDieName")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = this.cmbModelName.SelectedValue.ToString();
                CommonMethods.FillComboBox(this.cmbDieName, this.obj_BL.BL_DieMasterDetails().Tables[0], "DieName", "DieName");
            }
            if (Type == "GetPRN")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                DataSet dt = this.obj_BL.BL_MachineMasterDetails();
                PrnData = dt.Tables[0].Rows[0]["Prndata"].ToString();
                PrinterIP = dt.Tables[0].Rows[0]["PrinterIP"].ToString();
            }
            if (!(Type == "LoadPartDetails"))
                return;
            ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
            ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = this.cmbModelName.SelectedValue.ToString();
            ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = this.cmbDieName.SelectedValue.ToString();
            this.dvgMasterDeatils1.ItemsSource = this.obj_BL.BL_DieMasterDetails().Tables[0].DefaultView;
        }

        private void Clear()
        {
            this.cmbModelName.Text = "";
            this.cmbDieName.Text = "";
            this.txtdieno.Text = "";
            this.txtpmqty.Text = "";
            this.txtwarningqty.Text = "";
            this.txtwarningdays.Text = "";
            this.Transaction("LoadDetails");
            this.cmbModelName.Focus();
            this.RefNo = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
                this.Transaction("GetModelName");

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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void dvgMasterDeatils_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.dvgMasterDeatils.SelectedItems.Count == 1)
                {
                    DataRowView selectedItem = (DataRowView)this.dvgMasterDeatils.SelectedItems[0];
                    this.RefNo = Convert.ToInt32(selectedItem["Refno"]);
                    this.cmbModelName.Text = selectedItem["ModelName"].ToString();
                    this.cmbDieName.Text = selectedItem["DieName"].ToString();
                    this.txtdieno.Text = selectedItem["DieNo"].ToString();
                    this.txtpmqty.Text = selectedItem["PMQty"].ToString();
                    this.txtwarningqty.Text = selectedItem["WarningQty"].ToString();
                    this.txtwarningdays.Text = selectedItem["WarningDays"].ToString();
                    if (selectedItem["Status"].ToString() == "Active")
                        this.chkstatus.IsChecked = new bool?(true);
                    else
                        this.chkstatus.IsChecked = new bool?(false);
                    this.txtdieno.Focus();
                }
                else
                {
                    this.RefNo = 0;
                    this.cmbModelName.Text = "";
                    this.txtdieno.Text = "";
                    this.cmbDieName.Text = "";
                    this.txtpmqty.Text = "";
                    this.txtwarningqty.Text = "";
                    this.txtwarningdays.Text = "";
                    this.txtdieno.Focus();
                }
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbModelName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbModelName.SelectedValue == null)
                    return;
                this.Transaction("LoadDetails");
                this.dvgMasterDeatils1.ItemsSource = null;
                this.Transaction("GetDieName");
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbDieName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbDieName.SelectedValue == null)
                    return;
                this.Transaction("LoadPartDetails");
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Txtpmqty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonMethods.NumericValue(e);
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void Txtwarningqty_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                CommonMethods.NumericValue(e);
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "DIE_MASTER", CommonVariable.UserID);
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
                MessageBox.Show(p.GetprinterStatus().ToString(), "Printer Status"); ;
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
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = ((DataRowView)this.dvgMasterDeatils.SelectedItems[index])["DieName"].ToString();
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo = ((DataRowView)this.dvgMasterDeatils.SelectedItems[index])["DieNo"].ToString(); 
                        PrintLable(ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo);
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
