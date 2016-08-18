using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace TermConfig_NewMask.ViewModels
{

    public class ZBBIViewModel
    {
        TerminalReaderRepository terminalReaderRepository = new TerminalReaderRepository();
        public DataTable BindTerminalReaderRawData(List<TerminalReader> readersList)
        {
            var readers= readersList.OrderBy(x => x.ReaderID).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("ReaderID", typeof(int));
            dt.Columns.Add("ReaderType", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Direction", typeof(int));
            dt.Columns.Add("Status", typeof(int));
            dt.Columns.Add("RelayTime", typeof(string));
            dt.Columns.Add("Memo", typeof(string));
            dt.Columns.Add("ReaderImage", typeof(string));
            dt.Columns.Add("TerminalReaderID", typeof(string));
            dt.Columns.Add("DirectionDescription", typeof(string));
            dt.Columns.Add("StatusDescription", typeof(string));

            if (readers.Count == 0)
            {
                for (int i = 0; i < 2; i++)
                {
                    dt.Rows.Add(i + 1);
                }
            }
            else
            {

                foreach (TerminalReader ReaderRawData in readers)
                {

                    DataRow row = dt.NewRow();
                    row["ID"] = ReaderRawData.ID;
                    row["ReaderID"] = ReaderRawData.ReaderID;
                    row["ReaderType"] = ReaderRawData.ReaderType;
                    row["Name"] = ReaderRawData.Name;
                    row["Direction"] = ReaderRawData.Direction;
                    row["Status"] = ReaderRawData.Status;
                    row["RelayTime"] = ReaderRawData.RelayTime;
                    row["Memo"] = ReaderRawData.Memo;
                    row["ReaderImage"] = ReaderRawData.ReaderImage;
                    row["TerminalReaderID"] = ReaderRawData.TerminalReaderID;
                    row["DirectionDescription"] = ReaderRawData.Direction ==0? Resources.LocalizedText.doorEntry: Resources.LocalizedText.doorExit;
                    row["StatusDescription"] = ReaderRawData.Status ==1? Resources.LocalizedText.statusAktiv : Resources.LocalizedText.statusInaktiv;

                    dt.Rows.Add(row);

                }

                if (readers.Count < 2)
                {
                    for (int i = 0; i < (2 - readers.Count); i++)
                    {
                        dt.Rows.Add(i + 1);
                    }
                }

            }

            return dt;

        }
        public DataTable BindTerminalReaderRawDataFour(List<TerminalReader> readersList)
        {
            var readers = readersList.OrderBy(x => x.ReaderID).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("ReaderID", typeof(int));
            dt.Columns.Add("ReaderType", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Direction", typeof(int));
            dt.Columns.Add("Status", typeof(int));
            dt.Columns.Add("RelayTime", typeof(string));
            dt.Columns.Add("Memo", typeof(string));
            dt.Columns.Add("ReaderImage", typeof(string));
            dt.Columns.Add("TerminalReaderID", typeof(string));
            dt.Columns.Add("DirectionDescription", typeof(string));
            dt.Columns.Add("StatusDescription", typeof(string));

            if (readers.Count == 0)
            {
                for (int i = 0; i < 4; i++)
                {
                    dt.Rows.Add(i + 1);
                }
            }
            else
            {

                foreach (TerminalReader ReaderRawData in readers)
                {

                    DataRow row = dt.NewRow();
                    row["ID"] = ReaderRawData.ID;
                    row["ReaderID"] = ReaderRawData.ReaderID;
                    row["ReaderType"] = ReaderRawData.ReaderType;
                    row["Name"] = ReaderRawData.Name;
                    row["Direction"] = ReaderRawData.Direction;
                    row["Status"] = ReaderRawData.Status;
                    row["RelayTime"] = ReaderRawData.RelayTime;
                    row["Memo"] = ReaderRawData.Memo;
                    row["ReaderImage"] = ReaderRawData.ReaderImage;
                    row["TerminalReaderID"] = ReaderRawData.TerminalReaderID;
                    row["DirectionDescription"] = ReaderRawData.Direction == 0 ? Resources.LocalizedText.doorEntry : Resources.LocalizedText.doorExit;
                    row["StatusDescription"] = ReaderRawData.Status == 1 ? Resources.LocalizedText.statusAktiv : Resources.LocalizedText.statusInaktiv;

                    dt.Rows.Add(row);

                }

                if (readers.Count < 4)
                {
                    for (int i = 0; i < (4 - readers.Count); i++)
                    {
                        dt.Rows.Add(i + 1);
                    }
                }

            }

            return dt;

        }
        public DataTable BindTerminalReaderRawDataEight(List<TerminalReader> readersList)
        {
          
            var readers = readersList.OrderBy(x => x.ReaderID).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(long));
            dt.Columns.Add("ReaderID", typeof(int));
            dt.Columns.Add("ReaderType", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Direction", typeof(int));
            dt.Columns.Add("Status", typeof(int));
            dt.Columns.Add("RelayTime", typeof(string));
            dt.Columns.Add("Memo", typeof(string));
            dt.Columns.Add("ReaderImage", typeof(string));
            dt.Columns.Add("TerminalReaderID", typeof(string));
            dt.Columns.Add("DirectionDescription", typeof(string));
            dt.Columns.Add("StatusDescription", typeof(string));
            dt.Columns.Add("LockDescription", typeof(string));
            dt.Columns.Add("Delay", typeof(string));

            if (readers.Count == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    dt.Rows.Add(i + 1);
                }
            }
            else
            {

                foreach (TerminalReader ReaderRawData in readers)
                {
                    string _statusDescription = string.Empty;
                    if (ReaderRawData.Status == 0)
                    {
                        _statusDescription = Resources.LocalizedText.statusInaktiv;//"InAktiv";
                    }
                    else if (ReaderRawData.Status == 1)
                    {
                        _statusDescription = Resources.LocalizedText.statusAktiv;//"Aktiv";
                    }
                    else if (ReaderRawData.Status == 2)
                    {
                        _statusDescription = Resources.LocalizedText.TimeAttendanceCome;
                    }
                    else if (ReaderRawData.Status == 3)
                    {
                        _statusDescription = Resources.LocalizedText.TimeAttendanceGo;
                    }

                    DataRow row = dt.NewRow();
                    row["ID"] = ReaderRawData.ID;
                    row["ReaderID"] = ReaderRawData.ReaderID;
                    row["ReaderType"] = ReaderRawData.ReaderType;
                    row["Name"] = ReaderRawData.Name;
                    row["Direction"] = ReaderRawData.Direction;
                    row["Status"] = ReaderRawData.Status;
                    row["RelayTime"] = ReaderRawData.RelayTime;
                    row["Memo"] = ReaderRawData.Memo;
                    row["ReaderImage"] = ReaderRawData.ReaderImage;
                    row["TerminalReaderID"] = ReaderRawData.TerminalReaderID;
                    row["DirectionDescription"] = ReaderRawData.Direction == 0 ? Resources.LocalizedText.doorEntry : Resources.LocalizedText.doorExit;
                    row["StatusDescription"] = _statusDescription;
                    row["LockDescription"] = ReaderRawData.Lock == 0 ?Resources.LocalizedText.none : ReaderRawData.Lock.ToString();
                    row["Delay"] = ReaderRawData.Delay == -1 ? null : ReaderRawData.Delay;
                    dt.Rows.Add(row);

                }

                if (readers.Count < 4)
                {
                    for (int i = 0; i < (8 - readers.Count); i++)
                    {
                        dt.Rows.Add(i + 1);
                    }
                }

            }

            return dt;

        }
    }
}