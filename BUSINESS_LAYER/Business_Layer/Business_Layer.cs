// Decompiled with JetBrains decompiler
// Type: BUSINESS_LAYER.Business_Layer.Business_Layer
// Assembly: BUSINESS_LAYER, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 327BC3B6-698A-4D56-863C-2DDBC39BF096
// Assembly location: D:\Sato\mold..xbap_4ffec223c589b33d_0001.0000_92cb2251cacf4555\BUSINESS_LAYER.dll

using DATA_LAYER.DatabaseConnectivity;
using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace BUSINESS_LAYER.Business_Layer
{
    public class Business_Layer
    {
        private DatabaseConnections obj_DB = new DatabaseConnections();

        public void CreateLog(
          string ErrorDescription,
          string MethodName,
          string ModulName,
          string UserId)
        {
            try
            {
                this.obj_DB.ExecuteProcedureParam("Proc_LogDetails", (object)ErrorDescription, (object)MethodName, (object)ModulName, (object)UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_Login()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_Login", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Password, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ConfirmPassword, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                //StreamWriter streamWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Log\\"  + DateTime.Now.ToString("dd-MM-yyyy") + ".txt", true);
                //streamWriter.WriteLine(ex.Message.ToString()+", " + DateTime.Now.ToString());
                //streamWriter.Dispose();
                //streamWriter.Close();
                throw ex;
            }
        }

        public string BL_GroupMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_GroupMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.GroupID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Rights, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_GroupMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_GroupMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.GroupID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Rights, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_UserMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_UserMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Password, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.GroupID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_UserMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_UserMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Password, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.GroupID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_CheckListMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_CheckListMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckListName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_CheckListMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_CheckListMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckListName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_DieMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_DieMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningData, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_DieMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_DieMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningData, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_IPMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_IPMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.HardwareType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.IP, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Port, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ProcessType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcAddress, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcValue, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcMethods, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_IPMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_IPMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.HardwareType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.IP, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Port, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ProcessType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcAddress, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcValue, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PlcMethods, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_ProductMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_ProductMaster", ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.PartName, ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, ENTITY_LAYER.Entity_Layer.Entity_Layer.DieType, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.Noofcavity, ENTITY_LAYER.Entity_Layer.Entity_Layer.Noofpuls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_ProductMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_ProductMaster", ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.PartName, ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, ENTITY_LAYER.Entity_Layer.Entity_Layer.DieType, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.Noofcavity, ENTITY_LAYER.Entity_Layer.Entity_Layer.Noofpuls);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string BL_ProgramMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_ProgramMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Programno, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Programname, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.OutputPartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Version);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_ProgramMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_ProgramMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Programno, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Programname, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.OutputPartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Version);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_ShiftMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_ShiftMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftStartTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftEndTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_ShiftMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_ShiftMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftStartTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftEndTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_MachineMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_MachineMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMCategory, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMData, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningData, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_MachineMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_MachineMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMCategory, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMData, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningData, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_JigMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_JigMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.JigNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_JIgMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_JigMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.JigNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.WarningQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_ProductionPlanTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_ProductionPlan", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Month, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DailyQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_ProductionPlanDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_ProductionPlan", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Month, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PMQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DailyQty, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_ShortCountMonitoringTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_ShortCountMonitoring", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Programno, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.NoOfNG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_ShortCountMonitoringDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_ShortCountMonitoring", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Programno, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.NoOfNG);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string BL_PokeYokeMasterTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_PokeYokeMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.InsertNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Hoppertype, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.HopperBarcode, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_PokeYokeMasterDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_PokeYokeMaster", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Linename, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.InsertNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Hoppertype, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.HopperBarcode, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region ToolCheckSheetMaster
        public string BL_ToolCheckSheetMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_ToolCheckSheetMaster", ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, ENTITY_LAYER.Entity_Layer.Entity_Layer.Category, ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription, ENTITY_LAYER.Entity_Layer.Entity_Layer.Online, ENTITY_LAYER.Entity_Layer.Entity_Layer.Offline, ENTITY_LAYER.Entity_Layer.Entity_Layer.Int, ENTITY_LAYER.Entity_Layer.Entity_Layer.Method, ENTITY_LAYER.Entity_Layer.Entity_Layer.Who, ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment, ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal, ENTITY_LAYER.Entity_Layer.Entity_Layer.Toll, ENTITY_LAYER.Entity_Layer.Entity_Layer.Timetopm, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_ToolCheckSheetMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_ToolCheckSheetMaster", ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, ENTITY_LAYER.Entity_Layer.Entity_Layer.Category, ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription, ENTITY_LAYER.Entity_Layer.Entity_Layer.Online, ENTITY_LAYER.Entity_Layer.Entity_Layer.Offline, ENTITY_LAYER.Entity_Layer.Entity_Layer.Int, ENTITY_LAYER.Entity_Layer.Entity_Layer.Method, ENTITY_LAYER.Entity_Layer.Entity_Layer.Who, ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment, ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal, ENTITY_LAYER.Entity_Layer.Entity_Layer.Toll, ENTITY_LAYER.Entity_Layer.Entity_Layer.Timetopm, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ProductionCheckSheetMaster
        public string BL_ProductionCheckSheetMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_ProductionCheckSheetMaster", ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, ENTITY_LAYER.Entity_Layer.Entity_Layer.Category, ENTITY_LAYER.Entity_Layer.Entity_Layer.Standard, ENTITY_LAYER.Entity_Layer.Entity_Layer.Instruments, ENTITY_LAYER.Entity_Layer.Entity_Layer.Imte, ENTITY_LAYER.Entity_Layer.Entity_Layer.Frequency, ENTITY_LAYER.Entity_Layer.Entity_Layer.calibrationdoneon, ENTITY_LAYER.Entity_Layer.Entity_Layer.calibrationdueon, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_ProductionCheckSheetMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_ProductionCheckSheetMaster", ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, ENTITY_LAYER.Entity_Layer.Entity_Layer.Category, ENTITY_LAYER.Entity_Layer.Entity_Layer.Standard, ENTITY_LAYER.Entity_Layer.Entity_Layer.Instruments, ENTITY_LAYER.Entity_Layer.Entity_Layer.Imte, ENTITY_LAYER.Entity_Layer.Entity_Layer.Frequency, ENTITY_LAYER.Entity_Layer.Entity_Layer.calibrationdoneon, ENTITY_LAYER.Entity_Layer.Entity_Layer.calibrationdueon, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region MaintenanceCheckSheetMaster
        public string BL_MaintenanceCheckSheetMasterTransaction()
        {
            try
            {
                return obj_DB.ExecuteProcedureParam("Proc_MaintenanceCheckSheetMaster", ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, ENTITY_LAYER.Entity_Layer.Entity_Layer.Int, ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment, ENTITY_LAYER.Entity_Layer.Entity_Layer.Category, ENTITY_LAYER.Entity_Layer.Entity_Layer.Method, ENTITY_LAYER.Entity_Layer.Entity_Layer.Condition, ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataSet BL_MaintenanceCheckSheetMasterDetails()
        {
            try
            {
                return obj_DB.ExecuteDataSetParam("Proc_MaintenanceCheckSheetMaster", ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, ENTITY_LAYER.Entity_Layer.Entity_Layer.Int, ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment, ENTITY_LAYER.Entity_Layer.Entity_Layer.Category, ENTITY_LAYER.Entity_Layer.Entity_Layer.Method, ENTITY_LAYER.Entity_Layer.Entity_Layer.Condition, ENTITY_LAYER.Entity_Layer.Entity_Layer.Abnormal, ENTITY_LAYER.Entity_Layer.Entity_Layer.ModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.FreqModuleImage, ENTITY_LAYER.Entity_Layer.Entity_Layer.EntryType, ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckerRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.ApproverRemarks, ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        public string BL_CheckSheetEntryTransaction()
        {
            try
            {
                return this.obj_DB.ExecuteProcedureParam("Proc_Check_Sheet_Entry", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Change, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Clean, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RepairDate, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RequestDetails, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.SpecificInfo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MaintenanceInfo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Actual, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.A, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.B, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.C, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.D, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.E, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.F, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.G, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.H, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Controler, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.FP, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.LP, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Perpose, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.StrokeLen, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.StrokeLenAfter, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Remarks, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Category, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.StartTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.EndTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.TotalTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Countermeasure, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ApprovedRemarks, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.SearchDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_CheckSheetEntryDetails()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_Check_Sheet_Entry", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RefNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSheetDescription, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Status, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.UserID, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Change, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Clean, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RepairDate, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.RequestDetails, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.SpecificInfo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MaintenanceInfo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Actual, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Judgment, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.A, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.B, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.C, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.D, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.E, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.F, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.G, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.H, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Controler, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.FP, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.LP, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Perpose, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.StrokeLen, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.StrokeLenAfter, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Remarks, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Category, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.StartTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.EndTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.TotalTime, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Countermeasure, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ApprovedRemarks, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.SearchDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_Report()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_Report", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.FromDate, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ToDate, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.DieName, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.PartNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ModelType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ShiftName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet BL_MasterReport()
        {
            try
            {
                return this.obj_DB.ExecuteDataSetParam("Proc_MasterReport", (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.FromDate, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ToDate, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.ReportType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.CheckSeetType, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.MachineNo, (object)ENTITY_LAYER.Entity_Layer.Entity_Layer.Type);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
