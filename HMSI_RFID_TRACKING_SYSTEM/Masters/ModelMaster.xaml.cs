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
    /// Interaction logic for ModelMaster.xaml
    /// </summary>
    public partial class ModelMaster : Page
    {
        public ModelMaster()
        {
            InitializeComponent();
        }
        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        int RefNo = 0;
        #endregion

        #region Methods

        private bool ControlValidation()
        {
            if (cmbModelName.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT / ENTER MODEL NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbModelName.Focus();
                return false;
            }
             if (cmbDieType.SelectedIndex == -1)
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT DIE TYPE", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                cmbDieType.Focus();
                return false;
            }
            if (txtDiename.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER DIE NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtDiename.Focus();
                return false;
            }
            if (txtPartNo.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PART NO", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPartNo.Focus();
                return false;
            }
            if (txtPartName.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER PART NAME", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtPartName.Focus();
                return false;
            }
            if (txtnoofcavity.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF CAVITY", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtnoofcavity.Focus();
                return false;
            }
            if (txtnoofPuls.Text == "")
            {
                CommonClasses.CommonMethods.MessageBoxShow("PLEASE ENTER NO OF PULS", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                txtnoofPuls.Focus();
                return false;
            }
            return true;
        }
        private void Transaction(string Type)
        {
            if (Type == "Save" || Type == "Update" || Type == "Delete")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = cmbModelName.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName = txtDiename.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo = txtPartNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PartName = txtPartName.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.DieType = cmbDieType.Text;
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
                if (chkstatus.IsChecked == true)
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Status = "Active";
                }
                else
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.Status = "InActive";
                }
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Noofcavity = txtnoofcavity.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Noofpuls = txtnoofPuls.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = RefNo;
                CommonClasses.CommonVariable.Result = obj_BL.BL_ProductMasterTransaction();
                if (CommonClasses.CommonVariable.Result == "Saved")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    txtPartNo.Focus();
                    txtPartNo.Text = "";
                    txtPartName.Text = "";
                    txtnoofcavity.Text = "";
                    txtnoofPuls.Text = "";
                    imgModel.Source = null;
                    Transaction("LoadDetails");
                }
                else if (CommonClasses.CommonVariable.Result == "Updated")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataUpdated, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    Clear();
                }
                else if (CommonClasses.CommonVariable.Result == "Duplicate")
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Duplicate, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                else if (CommonClasses.CommonVariable.Result != "Deleted")
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.Result, CommonClasses.CommonVariable.CustomStriing.Information.ToString());

            }
            if (Type == "LoadDetails")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                if (cmbModelName.SelectedIndex != -1)
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = cmbModelName.SelectedValue.ToString();
                }
                DataTable dt = obj_BL.BL_ProductMasterDetails().Tables[0];
                dvgMasterDeatils.ItemsSource = dt.DefaultView;
                dvgMasterDeatils.Columns[9].Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName = "";

            }
            if (Type == "GetModelName")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                DataTable dt = obj_BL.BL_ProductMasterDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbModelName, dt, "ModelName", "ModelName");
            }

        }
        private void Clear()
        {
            cmbModelName.Text = "";
            txtDiename.Text = "";
            txtPartNo.Text = "";
            txtPartName.Text = "";
            txtnoofcavity.Text = "";
            txtnoofPuls.Text = "";
            cmbDieType.Text = "";
            imgModel.Source = null;
            Transaction("LoadDetails");
            Transaction("GetModelName");
            cmbModelName.Focus();
            RefNo = 0;
        }
        #endregion

        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                Transaction("LoadDetails");
                Transaction("GetModelName");

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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils.SelectedItems.Count == 0)
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
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
                    cmbModelName.Text = dr["ModelName"].ToString();
                    txtDiename.Text = dr["DieName"].ToString();
                    txtPartNo.Text = dr["PartNo"].ToString();
                    txtPartName.Text = dr["PartName"].ToString();
                    txtnoofcavity.Text = dr["Noofcavity"].ToString();
                    txtnoofPuls.Text = dr["Noofpuls"].ToString();
                    cmbDieType.Text = dr["DieType"].ToString();
                    string Status1 = dr["Status"].ToString();

                    if (Status1 == "Active")
                        chkstatus.IsChecked = true;
                    else
                        chkstatus.IsChecked = false;

                    imgModel.Source = null;
                    if (dr["ModuleImage"].ToString() != "")
                    {
                        byte[] Image = (byte[])dr["ModuleImage"];

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
                }
                else
                {
                    RefNo = 0;
                    cmbModelName.Text = "";
                    txtDiename.Text = "";
                    txtPartNo.Text = "";
                    txtPartName.Text = "";
                    txtnoofcavity.Text = "";
                    txtnoofPuls.Text = "";
                    cmbDieType.Text = "";
                    cmbModelName.Focus();
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCT_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void CmbModelName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Transaction("LoadDetails");
            }
            catch(Exception ex)
            {

            }
        }
    }
}
