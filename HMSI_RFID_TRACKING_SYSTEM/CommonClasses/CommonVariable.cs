// Decompiled with JetBrains decompiler
// Type: MOLDING_INTEGRATION_SYSTEM.CommonClasses.CommonVariable
// Assembly: MOLDING_INTEGRATION_SYSTEM, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: 4D144599-4C7F-484F-8D9C-1F71B8945FF9
// Assembly location: D:\Sato\mold..xbap_4ffec223c589b33d_0001.0000_92cb2251cacf4555\MOLDING_INTEGRATION_SYSTEM.exe

using System.Threading;

namespace HMSI_RFID_TRACKING_SYSTEM.CommonClasses
{
    public class CommonVariable
    {
        public static string DataSaved = "DATA SAVED SUCCESSFULLY";
        public static string DataDeleted = "DATA DELETED SUCCESSFULLY";
        public static string DataUpdated = "DATA UPDATED SUCCESSFULLY";
        public static string DataChecked = "CHECKER NOT CHECKED";
        public static string Duplicate = "DATA ALREADY EXIST";
        public static string DeleteConfirm = "DO YOU WANT TO DELETE SELECTED DATA?";
        public static string RowSelection = " PLEASE SELECT ROW FROM LIST VIEW FOR YOUR TRANSACTION!!!";
        public static string UserID = "";
        public static string UserName = "";
        public static string UserType = "";
        public static string Rights = "";
        public static int RefNo = 0;
        public static string Active = nameof(Active);
        public static string InActive = nameof(InActive);
        public static string Result = "";
        public static string PageOpenClose = "";
        public static string ChangeType = "";
  
        public static bool ThreadFlag = false;

        public static Microsoft.Office.Interop.Excel.Application xlApp1 = new Microsoft.Office.Interop.Excel.Application();


        public enum CustomStriing
        {
            YESNO,
            OKCancel,
            OK,
            Error,
            Successfull,
            Information,
            Question,
            Exclamatory,
        }
    }
}
