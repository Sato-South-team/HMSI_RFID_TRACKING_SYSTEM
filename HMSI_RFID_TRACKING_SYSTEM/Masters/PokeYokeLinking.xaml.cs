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
using System.Windows.Shapes;
using MOLDING_INTEGRATION_SYSTEM.CommonClasses;

namespace MOLDING_INTEGRATION_SYSTEM.Masters
{
    /// <summary>
    /// Interaction logic for PokeYokeLinking.xaml
    /// </summary>
    public partial class PokeYokeLinking : Page
    {
        public PokeYokeLinking()
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
            if (this.cmbMachinename.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT MACHINE NAME", CommonVariable.CustomStriing.Information.ToString());
                this.cmbMachinename.Focus();
                return false;
            }
            if (this.cmbDiename.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT DIE NAME", CommonVariable.CustomStriing.Information.ToString());
                this.cmbDiename.Focus();
                return false;
            }
            if (this.txtrawmaterial.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER RAW MATERIAL", CommonVariable.CustomStriing.Information.ToString());
                this.txtrawmaterial.Focus();
                return false;
            }
            if (this.txtinsertNo.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER INSERT NUMBER", CommonVariable.CustomStriing.Information.ToString());
                this.txtinsertNo.Focus();
                return false;
            }
            if (this.cmbHoppertype.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT HOPPER TYPE", CommonVariable.CustomStriing.Information.ToString());
                this.cmbHoppertype.Focus();
                return false;
            }
            if (!(this.cmbhopperno.SelectedIndex == -1))
                return true;
            CommonMethods.MessageBoxShow("PLEASE SELECT TANK BARCODE", CommonVariable.CustomStriing.Information.ToString());
            this.cmbhopperno.Focus();
            return false;
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = this.cmbMachinename.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = this.cmbDiename.SelectedValue.ToString();
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo = this.txtrawmaterial.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.InsertNo = this.txtinsertNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Hoppertype = this.cmbHoppertype.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.HopperBarcode = this.cmbhopperno.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = this.RefNo;
                CommonVariable.Result = this.obj_BL.BL_PokeYokeMasterTransaction();
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
                if (cmbMachinename.SelectedIndex != -1)
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = cmbMachinename.SelectedValue.ToString();
                }
                this.dvgMasterDeatils.ItemsSource = this.obj_BL.BL_PokeYokeMasterDetails().Tables[0].DefaultView;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = "";
            }
            if (Type == "GetDieNo")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                CommonMethods.FillComboBox(this.cmbDiename, this.obj_BL.BL_PokeYokeMasterDetails().Tables[0], "DieName", "DieNo");
            }
            if (Type == "GetPRN")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                DataSet dt = this.obj_BL.BL_MachineMasterDetails();
                PrnData = dt.Tables[0].Rows[0]["Prndata"].ToString();
                PrinterIP = dt.Tables[0].Rows[0]["PrinterIP"].ToString();
            }
            if (!(Type == "LoadMachineNo"))
                return;
            ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
            CommonMethods.FillComboBox(this.cmbMachinename, this.obj_BL.BL_PokeYokeMasterDetails().Tables[0], "MachineNo", "MachineNo");
        }

        private void Clear()
        {
            this.cmbMachinename.Text = "";
            this.cmbDiename.Text = "";
            this.txtrawmaterial.Text = "";
            this.txtinsertNo.Text = "";
            this.Transaction("LoadDetails");
            this.cmbhopperno.Text = "";
            this.RefNo = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
                this.Transaction("LoadMachineNo");
                this.Transaction("GetDieNo");

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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
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
                    this.cmbMachinename.Text = selectedItem["AssetBarcode"].ToString();
                    this.cmbDiename.SelectedValue = (object)selectedItem["DieNo"].ToString();
                    this.txtrawmaterial.Text = selectedItem["PartNo"].ToString();
                    this.txtinsertNo.Text = selectedItem["InsertNo"].ToString();
                    this.cmbHoppertype.Text = selectedItem["HopperName"].ToString();
                    this.cmbhopperno.SelectedItem = selectedItem["HopperBarcode"].ToString();
                    this.cmbMachinename.Focus();
                }
                else
                {
                    this.RefNo = 0;
                    this.cmbMachinename.Text = "";
                    this.cmbDiename.Text = "";
                    this.txtrawmaterial.Text = "";
                    this.txtinsertNo.Text = "";
                    this.cmbHoppertype.Text = "";
                    this.cmbhopperno.Text = "";
                }
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbMachinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbMachinename.SelectedIndex > -1)
                {
                    DataTable DT = new DataTable(); 
                    DT.Columns.Add("TankNo");
                    DT.Rows.Add(cmbMachinename.SelectedValue.ToString()+"|TANK-001");
                    DT.Rows.Add(cmbMachinename.SelectedValue.ToString()+"|TANK-002");
                    CommonMethods.FillComboBox(this.cmbhopperno, DT, "TankNo", "TankNo");
                }
                else
                    CommonMethods.UNFill_ComboBox(this.cmbhopperno);
                this.Transaction("LoadDetails");
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "POKEYOKE_MASTER", CommonVariable.UserID);
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
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = ((DataRowView)this.dvgMasterDeatils.SelectedItems[index])["AssetBarcode"].ToString();
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.HopperBarcode = ((DataRowView)this.dvgMasterDeatils.SelectedItems[index])["HopperBarcode"].ToString();
                        PrintLable(ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.HopperBarcode, ENTITY_LAYER.Entity_Layer.Entity_Layer.HopperBarcode);
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
