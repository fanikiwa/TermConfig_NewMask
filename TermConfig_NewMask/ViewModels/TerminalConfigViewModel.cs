using KruAll.Core.Models;
using KruAll.Core.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TermConfig_NewMask.Dtos;

namespace TermConfig_NewMask.ViewModels
{
    public class TerminalConfigViewModel
    {
        #region Constructor
        public TerminalConfigViewModel() { }
        #endregion

        #region Properties
        TerminalConfigRepository _termconRepository = new TerminalConfigRepository();
        private TerminalConfig _model = new TerminalConfig();
        public static int OemId { get; set; }
        public static string OemDesc { get; set; }

        #endregion

        #region Methods
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalConfig GetTerminalConfigByTerminalID(int termId)
        {
            return _termconRepository.GetTerminalConfigbyTermID(termId);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalsConDto> GetConTerminals()
        {
            var terminals = _termconRepository.GetAllTerminalConfig().ToList();
            if (terminals.Count == 0) return null;
            return SearchResults(terminals);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalsConDto GetConTermbyID(int Id)
        {
            var terminals = _termconRepository.GetTerminalConfigbyID(Id);
            if (terminals == null) return null;
            _model = terminals;
            return SearchResult(terminals);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalsConDto GetConTermbyDescription(string d)
        {
            var terminals = _termconRepository.GetTerminalConfigbyDesc(d);
            if (terminals == null) return null;
            return SearchResult(terminals);
        }
        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalsConDto GetConTermbyType(string t)
        {
            var terminals = _termconRepository.GetTerminalConfigbyType(t);
            if (terminals == null) return null;
            return SearchResult(terminals);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public TerminalsConDto GetConTermbySerial(string t)
        {
            var terminals = _termconRepository.GetTerminalbySerialNumber(t);
            if (terminals == null) return null;
            return SearchResult(terminals);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalsConDto> GetConTerminalsbyOEM(int OEM)
        {
            var terminals = _termconRepository.GetTerminalByOEM(OEM);
            if (terminals.Count == 0) return null;
            return SearchResults(terminals);
        }


        [DataObjectMethodAttribute(DataObjectMethodType.Select, true)]
        public List<TerminalsConDto> GetConTerminalsbyType(string type)
        {
            var terminals = _termconRepository.GetTerminalByType(type);
            if (terminals.Count == 0) return null;
            return SearchResults(terminals);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Insert, true)]
        public void CreateTerminalCon(TerminalsConDto terminal)
        {
            Populatemodel(terminal);
            _termconRepository.NewTerminalConfig(_model);
        }


        [DataObjectMethodAttribute(DataObjectMethodType.Update, true)]
        public void UpdateTerminalCon(TerminalsConDto terminal)
        {
            if (terminal.ID == 0) return;
            Populatemodel(terminal);
            _termconRepository.EditTerminalConfig(_model);
        }

        [DataObjectMethodAttribute(DataObjectMethodType.Delete, true)]
        public void DelTerminalCon(TerminalsConDto terminal)
        {
            if (terminal.ID == 0) return;
            Populatemodel(terminal);
            _termconRepository.DeleteTerminalConfig(_model);
        }

        private void Populatemodel(TerminalsConDto terminalconDto)
        {
            if (terminalconDto.ID == 0)
            {


                _model.TerminalConnect = new TerminalConnect();
              //  _model.TerminalInfo = new TerminalInfo();
                _model.TerminalOEM = new TerminalOEM();

                ////_model.TerminalConnect = new TerminalConnect();
                //_model.TerminalInfo = new TerminalInfo();
                //_model.TerminalOEM = new TerminalOEM();           
                _model.TerminalConnect.Connection = terminalconDto.Connection;
                _model.TerminalConnect.IPAddress = terminalconDto.IPAddress;
                _model.TerminalConnect.IPPort = terminalconDto.IPPort;
                _model.TerminalConnect.ActiveTerminal = terminalconDto.ActiveTerminal;
                _model.TerminalConnect.PersnoPIN = terminalconDto.PersnoPIN;
                _model.TerminalConnect.FPRead = terminalconDto.FPRead;
                _model.TerminalConnect.APPosting = terminalconDto.APPosting;
                _model.TerminalConnect.TAPPosting = terminalconDto.TAPPosting;
                _model.TerminalConnect.SerialNumber = terminalconDto.SerialNumber;



                //_model.TerminalInfo.InfoText1 = "";
                //_model.TerminalInfo.InfoText2 = "";
                //_model.TerminalInfo.InfoText3 = "";
                //_model.TerminalInfo.InfoText4 = "";
                //_model.TerminalInfo.Functionkey1 = "0";
                //_model.TerminalInfo.Functionkey2 = "0";
                //_model.TerminalInfo.Functionkey3 = "0";
                //_model.TerminalInfo.Functionkey4 = "0";
                //_model.TerminalInfo.Functionkey5 = "0";
                //_model.TerminalInfo.Functionkey6 = "0";
                //_model.TerminalInfo.Functionkey7 = "0";
                //_model.TerminalInfo.Functionkey8 = "0";
                //_model.TerminalInfo.DoorAssign = terminalconDto.DoorAssign;
                //_model.TerminalInfo.Memo = terminalconDto.Memo;

                _model.TermID = terminalconDto.TermID;
                _model.TermType = terminalconDto.TermType;
                _model.Description = terminalconDto.Description;
                _model.Status = terminalconDto.Status;
                _model.TerminalConnectId = _model.TerminalConnect.ID;
               // _model.TerminalInfoId = _model.TerminalInfo.ID;
                if (_model.TerminalOEM != null)
                {
                    _model.TerminalOEM.TermOEMId = 2;
                    _model.TerminalOEM.TermOEMDesc = "Datafox";
                }

                //_model.TerminalOEM.TermOEMDesc = terminalconDto.TermOEMDesc;

            }
            else
            {
                _model = _termconRepository.GetTerminalConfigbyID(terminalconDto.ID);
                _model.TermID = terminalconDto.TermID;
                _model.TermType = terminalconDto.TermType;
                _model.Description = terminalconDto.Description;
                _model.Status = terminalconDto.Status;
                _model.TerminalConnect = new TerminalConnect();
                //_model.TerminalInfo = new TerminalInfo();
                //_model.TerminalOEM = new TerminalOEM();
                _model.TerminalConnect.Connection = terminalconDto.Connection;
                _model.TerminalConnect.IPAddress = terminalconDto.IPAddress;
                _model.TerminalConnect.IPPort = terminalconDto.IPPort;
                _model.TerminalConnect.ActiveTerminal = terminalconDto.ActiveTerminal;
                _model.TerminalConnect.PersnoPIN = terminalconDto.PersnoPIN;
                _model.TerminalConnect.FPRead = terminalconDto.FPRead;
                _model.TerminalConnect.APPosting = terminalconDto.APPosting;
                _model.TerminalConnect.TAPPosting = terminalconDto.TAPPosting;
                _model.TerminalConnect.SerialNumber = terminalconDto.SerialNumber;

                //_model.TerminalInfo.InfoText1 = "";
                //_model.TerminalInfo.InfoText2 = "";
                //_model.TerminalInfo.InfoText3 = "";
                //_model.TerminalInfo.InfoText4 = "";
                //_model.TerminalInfo.Functionkey1 = "0";
                //_model.TerminalInfo.Functionkey2 = "0";
                //_model.TerminalInfo.Functionkey3 = "0";
                //_model.TerminalInfo.Functionkey4 = "0";
                //_model.TerminalInfo.Functionkey5 = "0";
                //_model.TerminalInfo.Functionkey6 = "0";
                //_model.TerminalInfo.Functionkey7 = "0";
                //_model.TerminalInfo.Functionkey8 = "0";
                //_model.TerminalInfo.DoorAssign = terminalconDto.DoorAssign;
                //_model.TerminalInfo.Memo = terminalconDto.Memo;


                //_model.TerminalOEM.TermOEMId = terminalconDto.TermOEMId;
                //_model.TerminalOEM.TermOEMDesc = terminalconDto.TermOEMDesc;
            }

            //Populate TerminalConfig fields

            //_model.TerminalConnectId = 5;
            //_model.TerminalInfoId = 5;
            //_model.TerminalOEMId = 5;

            ////Populate TerminalOEM fields
            //terminalconDto.TermOEMId = OemId;
            //terminalconDto.TermOEMDesc = OemDesc;

        }
        private TerminalsConDto SearchResult(TerminalConfig termcon)
        {
            TerminalsConDto terminalsConDto = new TerminalsConDto();

            terminalsConDto.ID = termcon.ID;
            terminalsConDto.TermID = termcon.TermID;
            terminalsConDto.TermType = termcon.TermType;
            terminalsConDto.Description = termcon.Description;
            terminalsConDto.Status = termcon.Status;
            terminalsConDto.Connection = termcon.TerminalConnect.Connection;
            terminalsConDto.IPAddress = termcon.TerminalConnect.IPAddress;
            terminalsConDto.IPPort = termcon.TerminalConnect.IPPort;
            terminalsConDto.ActiveTerminal = termcon.TerminalConnect.ActiveTerminal;
            terminalsConDto.PersnoPIN = termcon.TerminalConnect.PersnoPIN;
            terminalsConDto.FPRead = termcon.TerminalConnect.FPRead;
            terminalsConDto.APPosting = termcon.TerminalConnect.APPosting;
            terminalsConDto.TAPPosting = termcon.TerminalConnect.TAPPosting;
            terminalsConDto.SerialNumber = termcon.TerminalConnect.SerialNumber;
           // terminalsConDto.TerminalConnectId = termcon.TerminalConnect.ID;
            //terminalsConDto.TerminalInfoId = termcon.TerminalInfo.ID;
            //terminalsConDto.TerminalOEMId = termcon.TerminalOEM.ID;
            //terminalsConDto.InfoText1 = termcon.TerminalInfo.InfoText1;
            //terminalsConDto.InfoText2 = termcon.TerminalInfo.InfoText2;
            //terminalsConDto.InfoText3 = termcon.TerminalInfo.InfoText3;
            //terminalsConDto.InfoText4 = termcon.TerminalInfo.InfoText4;
            //terminalsConDto.Functionkey1 = termcon.TerminalInfo.Functionkey1;
            //terminalsConDto.Functionkey2 = termcon.TerminalInfo.Functionkey2;
            //terminalsConDto.Functionkey3 = termcon.TerminalInfo.Functionkey3;
            //terminalsConDto.Functionkey4 = termcon.TerminalInfo.Functionkey4;
            //terminalsConDto.Functionkey5 = termcon.TerminalInfo.Functionkey5;
            //terminalsConDto.Functionkey6 = termcon.TerminalInfo.Functionkey6;
            //terminalsConDto.Functionkey7 = termcon.TerminalInfo.Functionkey7;
            //terminalsConDto.Functionkey8 = termcon.TerminalInfo.Functionkey8;
            //terminalsConDto.DoorAssign = termcon.TerminalInfo.DoorAssign;
            //terminalsConDto.Memo = termcon.TerminalInfo.Memo;
            //TermOEMId = termcon.TerminalOEM.TermOEMId,
            //TermOEMDesc = termcon.TerminalOEM.TermOEMDesc
            return terminalsConDto;
        }
        private List<TerminalsConDto> SearchResults(IList<TerminalConfig> terminalCons)
        {
            var termconListing = new List<TerminalsConDto>();
            foreach (var termCon in terminalCons)
            {
                termconListing.Add(SearchResult(termCon));
            }
            return termconListing;
        }

        public TerminalDatafoxFunction GetTerminalDatafoxFunctions(int terminalID)
        {
            TerminalConfig terminalDevice = null;
            TerminalDatafoxFunction df = new TerminalDatafoxFunction();

            terminalDevice = _termconRepository.GetTerminalConfigbyTermID(terminalID);

            if(terminalDevice != null)
            {
                df = terminalDevice.TerminalDatafoxFunctions.FirstOrDefault();

                if(df.ReaderList != null)
                {
                    df.AccessControl = (df.ReaderList.Value || df.ActionList.Value || df.TimeList.Value || df.EventList.Value ||
                                      df.LocationList.Value || df.IdentificationList.Value || df.HolidayList.Value);
                }
            }
            
            return df;
        }

        public TerminalDatafoxFunction GetTerminalDatafoxFunctions(string terminalDescription)
        {
            TerminalConfig terminalDevice = null;
            TerminalDatafoxFunction df = new TerminalDatafoxFunction();

            terminalDevice = _termconRepository.GetTerminalConfigbyDesc(terminalDescription);

            if (terminalDevice != null)
            {
                df = terminalDevice.TerminalDatafoxFunctions.FirstOrDefault();

                if (df.ReaderList != null)
                {
                    df.AccessControl = (df.ReaderList.Value || df.ActionList.Value || df.TimeList.Value || df.EventList.Value ||
                                        df.LocationList.Value || df.IdentificationList.Value || df.HolidayList.Value);
                }
            }

            return df;
        }

        public void SessionData(int id, string desc)
        {
            OemId = id;

            OemDesc = desc;

        }

        #endregion
    }
}