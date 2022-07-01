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
    /// Interaction logic for ShiftMaster.xaml
    /// </summary>
    public partial class ShiftMaster : Page
    {
        public ShiftMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        int RefNo = 0;
        #endregion

        private bool ControlValidation()
        {
            if (this.cmbshiftname.SelectedIndex == -1)
            {
                CommonMethods.MessageBoxShow("PLEASE SELECT SHIFT NAME", CommonVariable.CustomStriing.Information.ToString());
                this.cmbshiftname.Focus();
                return false;
            }
            if (this.txtstart.Text == "")
            {
                CommonMethods.MessageBoxShow("PLEASE ENTER START TIME", CommonVariable.CustomStriing.Information.ToString());
                this.txtstart.Focus();
                return false;
            }
            if (!(this.txtend.Text == ""))
                return true;
            CommonMethods.MessageBoxShow("PLEASE ENTER END TIME", CommonVariable.CustomStriing.Information.ToString());
            this.txtend.Focus();
            return false;
        }

        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftName = this.cmbshiftname.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftStartTime = this.txtstart.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftEndTime = this.txtend.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = this.RefNo;
                CommonVariable.Result = this.obj_BL.BL_ShiftMasterTransaction();
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
                this.dvgMasterDeatils.ItemsSource = this.obj_BL.BL_ShiftMasterDetails().Tables[0].DefaultView;
                this.dvgMasterDeatils.Columns[3].Width = new DataGridLength(1.0, DataGridLengthUnitType.Star);
            }
            if (Type == "GetGroupID")
                ;
        }

        private void Clear()
        {
            this.cmbshiftname.Text = "";
            this.txtstart.Text = "";
            this.txtend.Text = "";
            this.Transaction("LoadDetails");
            this.cmbshiftname.Focus();
            this.RefNo = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Transaction("LoadDetails");
                this.Transaction("GetGroupID");

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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
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
                    this.cmbshiftname.Text = selectedItem["ShiftName"].ToString();
                    this.txtstart.Text = selectedItem["ShiftStarttime"].ToString();
                    this.txtend.Text = selectedItem["ShiftEndTime"].ToString();
                    this.cmbshiftname.Focus();
                }
                else
                {
                    this.RefNo = 0;
                    this.cmbshiftname.SelectedIndex = -1;
                    this.txtstart.Text = "";
                    this.txtend.Text = "";
                    this.cmbshiftname.Focus();
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
                this.obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "LINE_MASTER", CommonVariable.UserID);
                CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
