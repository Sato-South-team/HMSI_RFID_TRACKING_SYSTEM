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
    /// Interaction logic for ProductionCheckSheetMaster.xaml
    /// </summary>
    public partial class ProductionCheckSheetMaster : Page
    {
        public ProductionCheckSheetMaster()
        {
            InitializeComponent();
        }

        private void CmbCheckSheetType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        #region Variable and Objects
        BUSINESS_LAYER.Business_Layer.Business_Layer obj_BL = new BUSINESS_LAYER.Business_Layer.Business_Layer();
        int RefNo = 0;
        DataTable dt = new DataTable();
        #endregion

        #region Methods

        private bool ControlValidation()
        {
            //if (cmbMachine.Text == "")
            //{
            //    CommonClasses.CommonMethods.MessageBoxShow("PLEASE SELECT MACHINE NO", CommonClasses.CommonVariable.CustomStriing.Information.ToString());
            //    cmbMachine.Focus();
            //    return false;
            //}
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
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "BALANCING CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo = dt.Rows[i]["ModelNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Standard = dt.Rows[i]["Standard"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo = dt.Rows[i]["PartNo"].ToString();
                            
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "CHILLER CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Int = dt.Rows[i]["Standard"].ToString();
                           
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "PARAMETER CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo = dt.Rows[i]["ModelNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = dt.Rows[i]["Category"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Standard = dt.Rows[i]["Standard"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "QUALITY CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo = dt.Rows[i]["ModelNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Standard = dt.Rows[i]["Standard"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo = dt.Rows[i]["PartNo"].ToString();                            
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "INSTRUMENT CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo = dt.Rows[i]["ModelName"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Instruments = dt.Rows[i]["InstrumentName"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Imte = dt.Rows[i]["ImteNo"].ToString();
                           // ENTITY_LAYER.Entity_Layer.Entity_Layer.calibrationdoneon = dt.Rows[i]["CalibrationDoneOn"].ToString();
                            //ENTITY_LAYER.Entity_Layer.Entity_Layer.calibrationdueon = dt.Rows[i]["CalibrationDueOn"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = dt.Rows[i]["Frequency"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                        }
                        if (dt.Rows[i]["CheckSheetType"].ToString() == "WELDING CHECK SHEET")
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = dt.Rows[i]["MachineNo"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo = dt.Rows[i]["ModelName"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = dt.Rows[i]["CheckSheetDescription"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Standard = dt.Rows[i]["Standard"].ToString();
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.Instruments = dt.Rows[i]["Instruments"].ToString(); 
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = dt.Rows[i]["CheckSheetType"].ToString();
                        }
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                        ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = RefNo;
                        CommonClasses.CommonVariable.Result = obj_BL.BL_ProductionCheckSheetMasterTransaction();
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
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo = cmbModelNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription = txtDescription.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = cmbCheckSheetType.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Category = txtcategory.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Standard = txtstandard.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Instruments = txtInstrument.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Imte = txtImtNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Frequency = txtFrequency.Text;
                if (dtpcalidoneon.IsEnabled)
                {
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.calibrationdoneon = dtpcalidoneon.SelectedDate.Value.ToString("MM-dd-yyyy");
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.calibrationdueon = dtpcalidueon.SelectedDate.Value.ToString("MM-dd-yyyy");
                }
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
                //frequency model image
                ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage = null;
                if (freqimgModel.Source != null)
                {
                    byte[] buffer;
                    var bitmap = freqimgModel.Source as BitmapSource;
                    var encoder = new PngBitmapEncoder(); // or one of the other encoders
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));

                    using (var stream = new MemoryStream())
                    {
                        encoder.Save(stream);
                        buffer = stream.ToArray();
                    }
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage = buffer;
                }
                ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo = cmbPartNo.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo = RefNo;
                CommonClasses.CommonVariable.Result = obj_BL.BL_ProductionCheckSheetMasterTransaction();
                if (CommonClasses.CommonVariable.Result == "Saved")
                {
                    CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DataSaved, CommonClasses.CommonVariable.CustomStriing.Successfull.ToString());
                    txtDescription.Text = "";
                    Transaction("LoadDetails");
                    //Clear();
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
                    //Clear();
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
                ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType = cmbCheckSheetType.Text.ToString();
                ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = cmbMachine.Text;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo = cmbModelNo.Text;
                if (cmbPartNo.SelectedIndex > -1)
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo = cmbPartNo.SelectedValue.ToString();
                else
                    ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo = "";
                if (cmbCheckSheetType.SelectedIndex != -1)
                {
                    DataTable dt = obj_BL.BL_ProductionCheckSheetMasterDetails().Tables[0];
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
                    if (cmbCheckSheetType.SelectedIndex == 4)
                    {
                        dvgMasterDeatils4.ItemsSource = dt.DefaultView;
                    }
                    if (cmbCheckSheetType.SelectedIndex == 5)
                    {
                        dvgMasterDeatils5.ItemsSource = dt.DefaultView;
                    }
                }
                if (cmbCheckSheetType.SelectedIndex == 0)
                {
                    Clear();
                    //Enable
                    //cmbPartNo.Text = "";

                    cmbModelNo.IsEnabled = true;
                    //  cmbModelNo.Background = new SolidColorBrush(Colors.White);
                    cmbPartNo.IsEnabled = true;
                    //txtPartNo.Background = new SolidColorBrush(Colors.White);
                    txtstandard.IsEnabled = true;
                    //txtstandard.Background = new SolidColorBrush(Colors.White);
                    //disabled
                    cmbMachine.IsEnabled = true;
                    //cmbMachine.Background = new SolidColorBrush(Colors.White);
                    txtcategory.IsEnabled = false;
                    txtImtNo.IsEnabled = false;
                    txtFrequency.IsEnabled = false;
                    dtpcalidueon.IsEnabled = false;
                    dtpcalidoneon.IsEnabled = false;
                    txtInstrument.IsEnabled = false;

                    //txtcategory.Background = new SolidColorBrush(Colors.Red);
                    // cmbShiftname.IsEnabled = false;
                    //cmbShiftname.Background = new SolidColorBrush(Colors.Red);
                    btnbrowse.Visibility = System.Windows.Visibility.Hidden;
                    btnimgClear.Visibility = System.Windows.Visibility.Hidden;
                    btnbrowse1.Visibility = System.Windows.Visibility.Hidden;
                    btnimgClear1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils.Visibility = System.Windows.Visibility.Visible;
                    dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils4.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils5.Visibility = System.Windows.Visibility.Hidden;
                }
                //Chiller check sheet master
                else if (cmbCheckSheetType.SelectedIndex == 1)
                {
                    Clear();
                    //Disable
                    cmbModelNo.Text = "";
                    cmbPartNo.Text = "";

                    // cmbPartNo.Text = "";
                    cmbModelNo.IsEnabled = false;
                  //  cmbModelNo.Background = new SolidColorBrush(Colors.Red);
                    txtcategory.IsEnabled = false;
                    //txtcategory.Background = new SolidColorBrush(Colors.Red);
                    txtstandard.IsEnabled = true;
                    //txtstandard.Background = new SolidColorBrush(Colors.Red);
                    cmbPartNo.IsEnabled = false;
                    txtImtNo.IsEnabled = false;
                    txtFrequency.IsEnabled = false;
                    dtpcalidueon.IsEnabled = false;
                    dtpcalidoneon.IsEnabled = false;
                    txtInstrument.IsEnabled = false;
                    cmbPartNo.IsEnabled = false;

                    //txtPartNo.Background = new SolidColorBrush(Colors.Red);
                    //enable
                    //cmbShiftname.IsEnabled = true;
                    //cmbShiftname.Background = new SolidColorBrush(Colors.White);
                    cmbMachine.IsEnabled = true;
                    //cmbMachine.Background = new SolidColorBrush(Colors.White);
                    btnbrowse.Visibility = System.Windows.Visibility.Hidden;
                    btnimgClear.Visibility = System.Windows.Visibility.Hidden;
                    btnbrowse1.Visibility = System.Windows.Visibility.Hidden;
                    btnimgClear1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils1.Visibility = System.Windows.Visibility.Visible;
                    dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils4.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils5.Visibility = System.Windows.Visibility.Hidden;
                }
                //parameter check sheet master
                else if (cmbCheckSheetType.SelectedIndex == 2)
                {
                    Clear();
                    //Disable
                    //cmbShiftname.IsEnabled = false;
                    //cmbShiftname.Background = new SolidColorBrush(Colors.Red);
                    //enable
                    cmbMachine.IsEnabled = true;
                    //cmbMachine.Background = new SolidColorBrush(Colors.White);
                    cmbModelNo.IsEnabled = true;
                    //cmbModelNo.Background = new SolidColorBrush(Colors.White);
                    txtcategory.IsEnabled = true;
                    //txtcategory.Background = new SolidColorBrush(Colors.White);
                    txtstandard.IsEnabled = true;
                    txtImtNo.IsEnabled = false;
                    txtFrequency.IsEnabled = false;
                    dtpcalidueon.IsEnabled = false;
                    dtpcalidoneon.IsEnabled = false;
                    txtInstrument.IsEnabled = false;
                    cmbPartNo.IsEnabled = false;
                    cmbPartNo.Text = "";

                    //txtstandard.Background = new SolidColorBrush(Colors.White);
                    btnbrowse.Visibility = System.Windows.Visibility.Hidden;
                    btnimgClear.Visibility = System.Windows.Visibility.Hidden;
                    btnbrowse1.Visibility = System.Windows.Visibility.Hidden;
                    btnimgClear1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils2.Visibility = System.Windows.Visibility.Visible;
                    dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils4.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils5.Visibility = System.Windows.Visibility.Hidden;
                }
                //Quality check sheet master
                else if (cmbCheckSheetType.SelectedIndex == 3)
                {
                    Clear();
                    //Disable
                    //cmbShiftname.IsEnabled = false;
                    //cmbShiftname.Background = new SolidColorBrush(Colors.Red);
                    txtcategory.IsEnabled = false;
                    txtImtNo.IsEnabled = false;
                    txtFrequency.IsEnabled = false;
                    dtpcalidueon.IsEnabled = false;
                    dtpcalidoneon.IsEnabled = false;
                    txtInstrument.IsEnabled = false;

                    //txtcategory.Background = new SolidColorBrush(Colors.Red);
                    //cmbPartNo.IsEnabled = false;
                    //cmbPartNo.Background = new SolidColorBrush(Colors.Red);
                    //enable

                    cmbMachine.IsEnabled = true;
                    //cmbMachine.Background = new SolidColorBrush(Colors.White);
                    cmbModelNo.IsEnabled = true;
                    //cmbModelNo.Background = new SolidColorBrush(Colors.White);

                    txtstandard.IsEnabled = true;
                    //txtstandard.Background = new SolidColorBrush(Colors.White);
                    cmbPartNo.IsEnabled = true;
                    //txtPartNo.Background = new SolidColorBrush(Colors.White);
                   
                    btnbrowse.Visibility = System.Windows.Visibility.Visible;
                    btnimgClear.Visibility = System.Windows.Visibility.Visible;
                    btnbrowse1.Visibility = System.Windows.Visibility.Visible;
                    btnimgClear1.Visibility = System.Windows.Visibility.Visible;
                    dvgMasterDeatils3.Visibility = System.Windows.Visibility.Visible;
                    dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils4.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils5.Visibility = System.Windows.Visibility.Hidden;
                }
                //INSTRUMENT check sheet master
                else if (cmbCheckSheetType.SelectedIndex == 4)
                {
                    Clear();
                    //Disable
                    //cmbShiftname.IsEnabled = false;
                    //cmbShiftname.Background = new SolidColorBrush(Colors.Red);
                    txtcategory.IsEnabled = false;
                    txtImtNo.IsEnabled = true;
                    txtFrequency.IsEnabled = true;
                    dtpcalidueon.IsEnabled = true;
                    dtpcalidoneon.IsEnabled = true;
                    txtInstrument.IsEnabled = true;

                    //txtcategory.Background = new SolidColorBrush(Colors.Red);
                    //cmbPartNo.IsEnabled = false;
                    //cmbPartNo.Background = new SolidColorBrush(Colors.Red);
                    //enable

                    cmbMachine.IsEnabled = true;
                    //cmbMachine.Background = new SolidColorBrush(Colors.White);
                    cmbModelNo.IsEnabled = false;
                    cmbModelNo.Text = "";
                    //cmbModelNo.Background = new SolidColorBrush(Colors.White);

                    txtstandard.IsEnabled = false;
                    //txtstandard.Background = new SolidColorBrush(Colors.White);
                    cmbPartNo.IsEnabled = false;
                    //txtPartNo.Background = new SolidColorBrush(Colors.White);
                    btnbrowse.Visibility = System.Windows.Visibility.Hidden;
                    btnimgClear.Visibility = System.Windows.Visibility.Hidden;
                    btnbrowse1.Visibility = System.Windows.Visibility.Hidden;
                    btnimgClear1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils4.Visibility = System.Windows.Visibility.Visible;
                    dvgMasterDeatils5.Visibility = System.Windows.Visibility.Hidden;
                }
                //WELDING check sheet master
                else if (cmbCheckSheetType.SelectedIndex == 5)
                {
                    Clear();
                    //Disable
                    //cmbShiftname.IsEnabled = false;
                    //cmbShiftname.Background = new SolidColorBrush(Colors.Red);
                    txtcategory.IsEnabled = false;
                    txtImtNo.IsEnabled = false;
                    txtFrequency.IsEnabled = false;
                    txtInstrument.IsEnabled = true;

                    dtpcalidueon.IsEnabled = false;
                    dtpcalidoneon.IsEnabled = false;

                    //txtcategory.Background = new SolidColorBrush(Colors.Red);
                    //cmbPartNo.IsEnabled = false;
                    //cmbPartNo.Background = new SolidColorBrush(Colors.Red);
                    //enable
                    cmbPartNo.Text = "";

                    cmbMachine.IsEnabled = true;
                    //cmbMachine.Background = new SolidColorBrush(Colors.White);
                    cmbModelNo.IsEnabled = true;
                    //cmbModelNo.Background = new SolidColorBrush(Colors.White);

                    txtstandard.IsEnabled = true;
                    //txtstandard.Background = new SolidColorBrush(Colors.White);
                    cmbPartNo.IsEnabled = false;
                    //txtPartNo.Background = new SolidColorBrush(Colors.White);
                    btnbrowse.Visibility = System.Windows.Visibility.Visible;
                    btnimgClear.Visibility = System.Windows.Visibility.Visible;
                    btnbrowse1.Visibility = System.Windows.Visibility.Visible;
                    btnimgClear1.Visibility = System.Windows.Visibility.Visible;
                    dvgMasterDeatils3.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils1.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils2.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils4.Visibility = System.Windows.Visibility.Hidden;
                    dvgMasterDeatils5.Visibility = System.Windows.Visibility.Visible;
                }


            }
            if (Type == "GetMachineNo")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                DataTable dt = obj_BL.BL_ProductionCheckSheetMasterDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbMachine, dt, "MachineNo", "MachineNo");
            }
            if (Type == "GetModelNo")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo = cmbMachine.SelectedValue.ToString();
                DataTable dt = obj_BL.BL_ProductionCheckSheetMasterDetails().Tables[0];
                CommonClasses.CommonMethods.FillComboBox(cmbModelNo, dt, "DieName", "DieName");
            }
            if (Type == "GetPartNo")
            {
                ENTITY_LAYER.Entity_Layer.Entity_Layer.Type = Type;
                ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo = cmbModelNo.SelectedValue.ToString();
                DataTable dt = obj_BL.BL_ProductionCheckSheetMasterDetails().Tables[0];
               // txtPartNo.Text = "";
               // if(dt.Rows.Count>0)
               // {
                    //txtPartNo.Text = dt.Rows[0]["PartNo"].ToString();
                    CommonClasses.CommonMethods.FillComboBox(cmbPartNo, dt, "PartNo", "PartNo");
               // }
                // CommonClasses.CommonMethods.FillComboBox(cmbPartNo, dt, "PartNo", "PartNo");
            }

        }
        private void Clear()
        {
            txtInstrument.Text = "";
            txtDescription.Text = "";
            txtcategory.Text = "";
            txtstandard.Text = "";
            txtImtNo.Text = "";
            txtFrequency.Text = "";
            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = "";
            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = "";
            txtRemarks.Text = "";
            imgModel.Source = null;
            freqimgModel.Source = null;
            //Transaction("LoadDetails");
            //cmbMachine.Focus();
            RefNo = 0;
        }
        #endregion
        #region Events
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Transaction("GetMachineNo");
                //Transaction("GetModelNo");


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
                if (!(CommonClasses.CommonVariable.Rights.Contains("PRODUCTION CHECK SHEET MASTER CHECK")))
                {
                    btnChecker.IsEnabled = false;
                    this.btnChecker.ToolTip = (object)"Access Denied";

                }
                if (!(CommonClasses.CommonVariable.Rights.Contains("PRODUCTION CHECK SHEET MASTER APPROVE")))
                {
                    btnApprover.IsEnabled = false;
                    this.btnApprover.ToolTip = (object)"Access Denied";

                }


            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    // 1
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
                    /// 2
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
                    // 3
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
                    // 4
                    if (cmbCheckSheetType.SelectedIndex == 4)
                    {
                        if (dvgMasterDeatils4.SelectedItems.Count > 0)
                        {
                            if (dvgMasterDeatils4.SelectedItems.Count == 1)
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
                    //5
                    if (cmbCheckSheetType.SelectedIndex == 5)
                    {
                        if (dvgMasterDeatils5.SelectedItems.Count > 0)
                        {
                            if (dvgMasterDeatils5.SelectedItems.Count == 1)
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                //4
                if (cmbCheckSheetType.SelectedIndex == 4)
                {
                    if (dvgMasterDeatils4.SelectedItems.Count > 0)
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DeleteConfirm, CommonClasses.CommonVariable.CustomStriing.Question.ToString());
                        if (CommonClasses.CommonVariable.Result == "YES")
                        {
                            for (int i = 0; i < dvgMasterDeatils4.SelectedItems.Count; i++)
                            {
                                DataRowView dr = (DataRowView)dvgMasterDeatils4.SelectedItems[i];
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
                //5
                if (cmbCheckSheetType.SelectedIndex == 5)
                {
                    if (dvgMasterDeatils5.SelectedItems.Count > 0)
                    {
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.DeleteConfirm, CommonClasses.CommonVariable.CustomStriing.Question.ToString());
                        if (CommonClasses.CommonVariable.Result == "YES")
                        {
                            for (int i = 0; i < dvgMasterDeatils5.SelectedItems.Count; i++)
                            {
                                DataRowView dr = (DataRowView)dvgMasterDeatils5.SelectedItems[i];
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    cmbModelNo.Text = dr["ModelNo"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txtstandard.Text = dr["Standard"].ToString();
                    cmbPartNo.Text = dr["partno"].ToString();
                    cmbMachine.Text = dr["MachineNo"].ToString();
                }
                else if (dvgMasterDeatils.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    cmbModelNo.Text = "";
                    txtDescription.Text = "";
                    txtcategory.Text = "";
                    txtstandard.Text = "";
                    txtInstrument.Text = "";
                    cmbPartNo.Text = "";
                    dtpcalidoneon.Text = "";
                    dtpcalidueon.Text = "";
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbMachine.SelectedIndex > -1)
                    Transaction("GetModelNo");
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txtstandard.Text = dr["Standard"].ToString();

                }
                else if (dvgMasterDeatils1.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    cmbModelNo.Text = "";
                    txtDescription.Text = "";
                    txtcategory.Text = "";
                    txtstandard.Text = "";
                    txtInstrument.Text = "";
                    cmbPartNo.Text = "";
                    txtImtNo.Text = "";
                    txtFrequency.Text = "";
                    dtpcalidoneon.Text = "";
                    dtpcalidueon.Text = "";
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    cmbModelNo.Text = dr["ModelNo"].ToString();
                    txtcategory.Text = dr["Category"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txtstandard.Text = dr["Standard"].ToString();

                }
                else if (dvgMasterDeatils2.SelectedItems.Count > 1)
                {
                     cmbMachine.Text = "";
                    cmbModelNo.Text = "";
                    txtDescription.Text = "";
                    txtcategory.Text = "";
                    txtstandard.Text = "";
                    txtInstrument.Text = "";
                    cmbPartNo.Text = "";
                    txtImtNo.Text = "";
                    txtFrequency.Text = "";
                    dtpcalidoneon.Text = "";
                    dtpcalidueon.Text = "";
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;

                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                    cmbModelNo.Text = dr["ModelNo"].ToString();
                    txtDescription.Text = dr["CheckSheetDescription"].ToString();
                    txtstandard.Text = dr["Standard"].ToString();
                    cmbPartNo.Text = dr["PartNo"].ToString();
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
                        freqimgModel.Source = bi;

                        //ImageSource imgSrc = bi as ImageSource;
                        //cmbModelName.Focus();
                    }
                }
                else if (dvgMasterDeatils3.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    cmbModelNo.Text = "";
                    txtDescription.Text = "";
                    txtcategory.Text = "";
                    txtstandard.Text = "";
                    txtInstrument.Text = "";
                    cmbPartNo.Text = "";
                    txtImtNo.Text = "";
                    txtFrequency.Text = "";
                    imgModel.Source = null;
                    freqimgModel.Source = null;
                    dtpcalidoneon.Text = "";
                    dtpcalidueon.Text = "";
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbModelNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cmbPartNo.Text = "";
                if (cmbModelNo.SelectedIndex > -1)
                {
                    DataRowView CMB = (DataRowView)cmbModelNo.SelectedItem;
                    cmbModelNo.Text = CMB[0].ToString();
                    Transaction("GetPartNo");
                    Transaction("LoadDetails");
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void CmbCheckSheetType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //Balance check sheet master
            try
            {
                if (cmbCheckSheetType.SelectedIndex > -1)
                {
                    ComboBoxItem CMB = (ComboBoxItem)cmbCheckSheetType.SelectedItem;
                    cmbCheckSheetType.Text = CMB.Content.ToString();
                    Transaction("LoadDetails");
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void DvgMasterDeatils4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils4.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgMasterDeatils4.SelectedItems[0];
                    RefNo = Convert.ToInt32(dr["Refno"]);
                    cmbMachine.Text = dr["MachineNo"].ToString();
                    //cmbModelNo.Text = dr["ModelName"].ToString();
                    txtInstrument.Text = dr["InstrumentName"].ToString();
                    txtImtNo.Text = dr["ImteNo"].ToString();
                    dtpcalidoneon.Text = dr["CalibrationDoneOn"].ToString();
                    dtpcalidueon.Text = dr["CalibrationDueOn"].ToString();
                    txtDescription.Text = dr["CheckDecription"].ToString();
                    txtFrequency.Text = dr["Frequency"].ToString();
                    
                }
                else if (dvgMasterDeatils4.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    cmbModelNo.Text = "";
                    txtDescription.Text = "";
                    txtcategory.Text = "";
                    txtstandard.Text = "";
                    txtInstrument.Text = "";
                    cmbPartNo.Text = "";
                    txtImtNo.Text = "";
                    txtFrequency.Text = "";
                    dtpcalidoneon.Text = "";
                    dtpcalidueon.Text = "";
                    Transaction("LoadDetails");
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
        private void DvgMasterDeatils5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (dvgMasterDeatils5.SelectedItems.Count == 1)
                {
                    DataRowView dr = (DataRowView)dvgMasterDeatils5.SelectedItems[0];
                    RefNo = Convert.ToInt32(dr["Refno"]);
                    cmbMachine.Text = dr["MachineNo"].ToString();
                    cmbModelNo.Text = dr["ModelName"].ToString();
                    txtDescription.Text = dr["CheckDecription"].ToString();
                    txtstandard.Text = dr["Standard"].ToString();
                    txtInstrument.Text = dr["Instruments"].ToString();
                    if (dr["Images"].ToString() != "")
                    {
                        byte[] Image = (byte[])dr["Images"];

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
                        freqimgModel.Source = bi;

                        //ImageSource imgSrc = bi as ImageSource;
                        //cmbModelName.Focus();
                    }
                }
                else if (dvgMasterDeatils5.SelectedItems.Count > 1)
                {
                    cmbMachine.Text = "";
                    cmbModelNo.Text = "";
                    txtDescription.Text = "";
                    txtcategory.Text = "";
                    txtstandard.Text = "";
                    txtInstrument.Text = "";
                    cmbPartNo.Text = "";
                    txtImtNo.Text = "";
                    txtFrequency.Text = "";
                    dtpcalidoneon.Text = "";
                    dtpcalidueon.Text = "";
                    Transaction("LoadDetails");
                    imgModel.Source = null;
                    freqimgModel.Source = null;
                    cmbMachine.Focus();
                    RefNo = 0;
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnimgClear_Click(object sender, RoutedEventArgs e)
        {
            {
                try
                {
                    imgModel.Source = null;
                }
                catch (Exception ex)
                {
                    obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                    CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
                }
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
                    freqimgModel.Source = new BitmapImage(new Uri(dlg.FileName));
                }
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }

        private void BtnimgClear1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                freqimgModel.Source = null;
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                //4
                if (cmbCheckSheetType.SelectedIndex == 4)
                {

                    if (dvgMasterDeatils4.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < dvgMasterDeatils4.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = txtRemarks.Text;
                            DataRowView dr = (DataRowView)dvgMasterDeatils4.SelectedItems[i];
                            RefNo = Convert.ToInt32(dr["Refno"]);
                            Transaction("Checking");
                        }

                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //5
                if (cmbCheckSheetType.SelectedIndex == 5)
                {

                    if (dvgMasterDeatils5.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < dvgMasterDeatils5.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks = txtRemarks.Text;
                            DataRowView dr = (DataRowView)dvgMasterDeatils5.SelectedItems[i];
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                //4
                if (cmbCheckSheetType.SelectedIndex == 4)
                {

                    if (dvgMasterDeatils4.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < dvgMasterDeatils4.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = txtRemarks.Text;

                            DataRowView dr = (DataRowView)dvgMasterDeatils4.SelectedItems[i];
                            RefNo = Convert.ToInt32(dr["Refno"]);
                            Transaction("Checking");
                        }



                    }
                    else
                        CommonClasses.CommonMethods.MessageBoxShow(CommonClasses.CommonVariable.RowSelection, CommonClasses.CommonVariable.CustomStriing.Information.ToString());
                }
                //5
                if (cmbCheckSheetType.SelectedIndex == 5)
                {

                    if (dvgMasterDeatils5.SelectedItems.Count > 0)
                    {
                        for (int i = 0; i < dvgMasterDeatils5.SelectedItems.Count; i++)
                        {
                            ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks = txtRemarks.Text;

                            DataRowView dr = (DataRowView)dvgMasterDeatils5.SelectedItems[i];
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
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "PRODUCTION_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
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
                        if (cmbCheckSheetType.Text == "BALANCING CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[BALANCING CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "CHILLER CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[CHILLER CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "PARAMETER CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[PARAMETER CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "QUALITY CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[QUALITY CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "INSTRUMENT CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[INSTRUMENT CHECK SHEET$]");
                        }
                        if (cmbCheckSheetType.Text == "WELDING CHECK SHEET")
                        {
                            dt = CommonClasses.CommonMethods.ReadExcelData(obj_OP.FileName, "[WELDING CHECK SHEET$]");
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

        private void BtnTemplate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var remote = AppDomain.CurrentDomain.BaseDirectory + "\\TEMPLATE PRODUCTION CHECK SHEET MASTER.xlsx";
                //var local = @"D:\TEMPLATE PRODUCTION CHECK SHEET MASTER.xlsx";
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

        private void CmbPartNo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(cmbPartNo.SelectedIndex>-1)
                Transaction("LoadDetails");
            }
            catch (Exception ex)
            {
                obj_BL.CreateLog(ex.Message.ToString(), MethodBase.GetCurrentMethod().ToString(), "MAINTENANCE_CHECK_SHEET_MASTER", CommonClasses.CommonVariable.UserID);
                CommonClasses.CommonMethods.MessageBoxShow(ex.Message.ToString(), CommonClasses.CommonVariable.CustomStriing.Error.ToString());
            }
        }
    }
}
