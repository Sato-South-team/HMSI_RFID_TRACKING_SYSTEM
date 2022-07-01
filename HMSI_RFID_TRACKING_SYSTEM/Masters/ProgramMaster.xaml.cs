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
    /// Interaction logic for ProgramMaster.xaml
    /// </summary>
    public partial class ProgramMaster : Page
    {
        public ProgramMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
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
            if (this.cmbDiename.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT DIE NAME", CommonVariable.CustomStriing.Information.ToString());
                this.cmbDiename.Focus();
                return false;
            }
            if (this.cmbDieNo.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT DIE NO", CommonVariable.CustomStriing.Information.ToString());
                this.cmbDieNo.Focus();
                return false;
            }
            if (this.cmbMachineno.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT MACHINE NO", CommonVariable.CustomStriing.Information.ToString());
                this.cmbMachineno.Focus();
                return false;
            }
            if (this.txtProgramno.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER PROGRAM NO", CommonVariable.CustomStriing.Information.ToString());
                this.txtProgramno.Focus();
                return false;
            }
            if (!(this.txtProgramname.Text == ""))
                return true;
            CommonMethods.MessageBoxShow("PLEASE ENTER PROGRAM NAME", CommonVariable.CustomStriing.Information.ToString());
            this.txtProgramname.Focus();
            return false;
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = this.cmbModelName.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = this.cmbDiename.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo = this.cmbDieNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = this.cmbMachineno.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Programno = this.txtProgramno.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Programname = this.txtProgramname.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.OutputPartNo = this.txtOutPartNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Version = this.txtVersion.Text;
                bool? isChecked = this.chkstatus.IsChecked;
                bool flag = true;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Status = !(isChecked.GetValueOrDefault() == flag & isChecked.HasValue) ? "InActive" : "Active";
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = this.RefNo;
                CommonVariable.Result = this.obj_BL.BL_ProgramMasterTransaction();
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
                if (cmbModelName.SelectedIndex > -1)
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = cmbModelName.SelectedValue.ToString();
                else
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = "";

                this.dvgMasterDeatils.ItemsSource = this.obj_BL.BL_ProgramMasterDetails().Tables[0].DefaultView;
                this.dvgMasterDeatils.Columns[9].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = "";
            }
            if (Type == "LoadPartDetails")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = this.cmbModelName.SelectedValue.ToString();
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = this.cmbDiename.SelectedValue.ToString();
                this.dvgMasterDeatils1.ItemsSource = this.obj_BL.BL_ProgramMasterDetails().Tables[0].DefaultView;
            }
            if (Type == "GetModelName")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                CommonMethods.FillComboBox(this.cmbModelName, this.obj_BL.BL_ProgramMasterDetails().Tables[0], "ModelName", "ModelName");
            }
            if (Type == "GetDieName")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = this.cmbModelName.SelectedValue.ToString();
                CommonMethods.FillComboBox(this.cmbDiename, this.obj_BL.BL_ProgramMasterDetails().Tables[0], "DieName", "DieName");
            }
            if (Type == "GetDieNo")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = this.cmbDiename.SelectedValue.ToString();
                CommonMethods.FillComboBox(this.cmbDieNo, this.obj_BL.BL_ProgramMasterDetails().Tables[0], "DieNo", "DieNo");
            }
            if (!(Type == "GetMachineNo"))
                return;
            ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
            CommonMethods.FillComboBox(this.cmbMachineno, this.obj_BL.BL_ProgramMasterDetails().Tables[0], "MachineNo", "MachineNo");
        }

        private void Clear()
        {
            this.cmbModelName.Text = "";
            this.cmbDiename.Text = "";
            this.cmbDieNo.Text = "";
            //this.cmbMachineno.Text = "";
            this.txtProgramno.Text = "";
            this.txtProgramname.Text = "";
            this.txtVersion.Text = "";
            this.txtOutPartNo.Text = "";
            this.Transaction("LoadDetails");
            CommonMethods.UNFill_ComboBox(this.cmbDiename);
            CommonMethods.UNFill_ComboBox(this.cmbDieNo);
            this.cmbModelName.Focus();
            this.RefNo = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
                this.Transaction("GetModelName");
                this.Transaction("GetMachineNo");

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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROGRAM_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
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
                    this.cmbDiename.Text = selectedItem["DieName"].ToString();
                    this.cmbDieNo.Text = selectedItem["DieNo"].ToString();
                    this.cmbMachineno.Text = selectedItem["MachineNo"].ToString();
                    this.txtProgramno.Text = selectedItem["ProgramNo"].ToString();
                    this.txtProgramname.Text = selectedItem["ProgramName"].ToString();
                    this.txtOutPartNo.Text = selectedItem["OutputPartNo"].ToString();
                    this.txtVersion.Text = selectedItem["varsion"].ToString();
                    if (selectedItem["Status"].ToString() == "Active")
                        this.chkstatus.IsChecked = new bool?(true);
                    else
                        this.chkstatus.IsChecked = new bool?(false);
                    this.cmbDieNo.Focus();
                }
                else
                {
                    this.cmbModelName.Text = "";
                    this.cmbDiename.Text = "";
                    this.cmbDieNo.Text = "";
                    this.cmbMachineno.Text = "";
                    this.txtProgramno.Text = "";
                    this.txtProgramname.Text = "";
                    this.Transaction("LoadDetails");
                    this.cmbModelName.Focus();
                    this.RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PROGRAM_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbDiename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbDiename.SelectedValue != null)
                {
                    this.Transaction("GetDieNo");
                    this.Transaction("LoadPartDetails");
                }
                else
                    CommonMethods.UNFill_ComboBox(this.cmbDieNo);
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbModelName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (this.cmbModelName.SelectedValue != null)
                {
                    this.dvgMasterDeatils1.ItemsSource = null;
                    this.Transaction("GetDieName");
                    this.Transaction("LoadDetails");

                }
                else
                    CommonMethods.UNFill_ComboBox(this.cmbDiename);
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

      
    }
}
