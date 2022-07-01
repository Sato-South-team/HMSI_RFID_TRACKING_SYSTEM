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
    /// Interaction logic for IPConfiguration.xaml
    /// </summary>
    public partial class IPConfiguration : Page
    {
        public IPConfiguration()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        int RefNo = 0;
        #endregion

        private bool ControlValidation()
        {
            if (this.cmbHARDTYPE.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT HARDWARE TYPE", CommonVariable.CustomStriing.Information.ToString());
                this.cmbHARDTYPE.Focus();
                return false;
            }
            if (this.cmbLinename.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT MACHINE NO", CommonVariable.CustomStriing.Information.ToString());
                this.cmbLinename.Focus();
                return false;
            }
            if (this.txtIP.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER IP ADDRESS", CommonVariable.CustomStriing.Information.ToString());
                this.txtIP.Focus();
                return false;
            }
            if (this.txtportNo.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER PORT NO", CommonVariable.CustomStriing.Information.ToString());
                this.txtportNo.Focus();
                return false;
            }
            if (this.cmbProcessType.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT PROCESS TYPE", CommonVariable.CustomStriing.Information.ToString());
                this.cmbProcessType.Focus();
                return false;
            }
            if (this.txtplcaddress.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER PLC ADDRESS", CommonVariable.CustomStriing.Information.ToString());
                this.txtplcaddress.Focus();
                return false;
            }
            if (this.txtPlcvalue.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER PLC VALUE", CommonVariable.CustomStriing.Information.ToString());
                this.txtPlcvalue.Focus();
                return false;
            }
            if (!(this.cmbplcmethod.Text == ""))
                return true;
            CommonMethods.MessageBoxShow("PLEASE SELECT PLC METHODS", CommonVariable.CustomStriing.Information.ToString());
            this.cmbplcmethod.Focus();
            return false;
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.HardwareType = this.cmbHARDTYPE.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = this.cmbLinename.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.IP = this.txtIP.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Port = this.txtportNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ProcessType = this.cmbProcessType.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcAddress = this.txtplcaddress.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcValue = this.txtPlcvalue.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcMethods = this.cmbplcmethod.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = this.RefNo;
                CommonVariable.Result = this.obj_BL.BL_IPMasterTransaction();
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
                if (cmbLinename.SelectedIndex !=-1)
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = cmbLinename.SelectedValue.ToString();
                }
                this.dvgMasterDeatils.ItemsSource = this.obj_BL.BL_IPMasterDetails().Tables[0].DefaultView;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename = "";
            }
            if (!(Type == "LoadMachineNo"))
                return;
            ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
            CommonMethods.FillComboBox(this.cmbLinename, this.obj_BL.BL_IPMasterDetails().Tables[0], "MachineNo", "MachineNo");
        }

        private void Clear()
        {
            this.cmbHARDTYPE.Text = "";
            this.cmbLinename.Text = "";
            this.txtIP.Text = "";
            this.txtportNo.Text = "";
            this.cmbProcessType.Text = "";
            this.txtplcaddress.Text = "";
            this.txtPlcvalue.Text = "";
            this.cmbplcmethod.Text = "";
            this.Transaction("LoadDetails");
            this.cmbHARDTYPE.Focus();
            this.RefNo = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
                this.Transaction("LoadMachineNo");

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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
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
                    this.cmbHARDTYPE.Text = selectedItem["HardwareType"].ToString();
                    this.cmbLinename.Text = selectedItem["LineName"].ToString();
                    this.txtIP.Text = selectedItem["IPaddress"].ToString();
                    this.txtportNo.Text = selectedItem["Port"].ToString();
                    this.cmbProcessType.Text = selectedItem["ProcessType"].ToString();
                    this.txtplcaddress.Text = selectedItem["PLCAddress"].ToString();
                    this.txtPlcvalue.Text = selectedItem["PLCValue"].ToString();
                    this.cmbplcmethod.Text = selectedItem["PLCMethode"].ToString();
                    this.cmbHARDTYPE.Focus();
                }
                else
                {
                    this.RefNo = 0;
                    this.cmbHARDTYPE.Text = "";
                    this.txtIP.Text = "";
                    this.txtportNo.Text = "";
                    this.cmbHARDTYPE.Focus();
                }
            }
            catch (Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbLinename_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
            }
            catch(Exception ex)
            {
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "IP_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
