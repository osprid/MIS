using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS.Repository;
using System.Data;
namespace TMS.DataModel
{
    public class InstituteModel
    {
        //private UserModel _UserModel;
        private InstituteRepo  _InstituteRepo;
        private AddressRepo _AddressRepo;
        private AddressModel _AddressModel;

        public InstituteModel()
        {
            //_UserModel = new UserModel();
            _InstituteRepo = new InstituteRepo();
            _AddressRepo = new AddressRepo();
            _AddressModel = new AddressModel();

        }

        #region Fields

        private int _InstituteID;

        private string _Name;
        private String _Status;

        private int _CreatedBy;
        private DateTime _CretedDatetime;
        private int _UpdatedBy;
        private DateTime _UpdatedDatetime;

      

        #endregion

        #region Properties

        public int InstituteID
        {
            get { return _InstituteID; }
            set { _InstituteID = value; }
        }

        public AddressModel Address
        {
            get { return _AddressModel; }
            set { _AddressModel = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }               

        public DateTime CretedDatetime
        {
            get { return _CretedDatetime; }
            set { _CretedDatetime = value; }
        }

        public int CreatedBy
        {
            get { return _CreatedBy; }
            set { _CreatedBy = value; }
        }

        public DateTime UpdatedDatetime
        {
            get { return _UpdatedDatetime; }
            set { _UpdatedDatetime = value; }
        }

        public int UpdatedBy
        {
            get { return _UpdatedBy; }
            set { _UpdatedBy = value; }
        }

        public String Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        #endregion
        
        #region methods

        public string saveCompleteTrans(tbl_Institute Institute,tbl_Address Address)
        {
            return _InstituteRepo.saveCompleteTrans(Institute, Address);
        }

        public string SaveInstitute(tbl_Institute Institute)
        {
            return _InstituteRepo.saveInstitute(Institute);
        }


        public object GetInstituteList()
        {
            return _InstituteRepo.getInstituteList();
        }          

        public tbl_Institute GetInstituteByID(int InstituteID)
        {
            return _InstituteRepo.GetInstituteByID(InstituteID);             
        }

        public void getCustomerDetails(int intInstituteID)
        {
            tbl_Institute inst = this.GetInstituteByID(intInstituteID);
           
            if (inst != null)
            {
                tbl_Address add = _AddressRepo.GetAddressByid((int)inst.AddressID);
                Name = inst.Name;

                Address.Address1 = add.Address1;
                Address.Address2 = add.Address2;
                Address.Landmark = add.Landmark;
                Address.Street = add.Street;
                Address.CityID = (int)add.CityID;
                Address.StateID = (int)add.StateID;
                Address.CountryID = (int)add.CountryID;
                Address.PostalCode = add.Postalcode;
            }
            else
            {
            }
        }
        public string updateCompleteTrans(tbl_Institute objInstitute, tbl_Address objAddress)
        {
            return _InstituteRepo.updateCompleteTrans(objInstitute, objAddress);
        }
        #endregion
    }
}