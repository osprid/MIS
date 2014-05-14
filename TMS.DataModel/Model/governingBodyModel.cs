using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS.Repository;
namespace TMS.DataModel
{
    public class governingBodyModel
    {
        public governingBodyModel()
        {
            _AddressModel = new AddressModel();
        }
        #region Fields
        private int _GovBodyID;
        private string _GovName;
        private AddressModel _AddressModel;
        private AddressRepo _AddressRepo=new AddressRepo();
        #endregion

        #region Properties
        public int GovBodyID
        {
            get { return _GovBodyID; }
            set { _GovBodyID = value; }
        }
        public string GovName
        {
            get { return _GovName; }
            set { _GovName = value; }
        }
        public AddressModel Address
        {
            get { return _AddressModel; }
            set { _AddressModel = value; }
        }
        #endregion
        
        
        governingBodyRepo _governingBodyRepo = new governingBodyRepo();
        public string saveGoverning(tbl_GoverningBody objGoverningBody, tbl_Address objAddress)
        {
            return _governingBodyRepo.saveGoverning(objGoverningBody,objAddress);
        }
        public object GetGoverningList() 
        {
            return _governingBodyRepo.GetGoverningList();
        }

        public void getGovernBodyDetails(int GOVID)
        {
            try
            {
                tbl_GoverningBody gov = GetGovernByID(GOVID);
                if (gov != null)
                {
                    GovName = gov.Name;
                    tbl_Address add = _AddressRepo.GetAddressByid((int)gov.AddressID);
                    Address.Address1 = add.Address1;
                    Address.Address2 = add.Address2;
                    Address.Landmark = add.Landmark;
                    Address.Street = add.Street;
                    Address.CityID = (int)add.CityID;
                    Address.StateID = (int)add.StateID;
                    Address.CountryID = (int)add.CountryID;
                    Address.PostalCode = add.Postalcode;
                }
            }
            catch (Exception ex) {  }
        }
        public tbl_GoverningBody GetGovernByID(int govid)
        {
            return _governingBodyRepo.GetGovernByID(govid);
        }
        public string updateGovernBodies(tbl_GoverningBody objGovern, tbl_Address objAddress)
        {
            return _governingBodyRepo.updateGovernBodies(objGovern,objAddress);
        }
    }
}