using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS.Repository;

namespace TMS.DataModel
{
    
    public class AddressModel
    {
        TMSEntities context = null;
        private AddressRepo _AddressRepo;
        
        public AddressModel()
        {
            _AddressRepo = new AddressRepo();
        }

        #region Fields

        private int _AddressID;

        private string _Address1;
        private string _Address2;
        private string _Landmark;
        private string _Street;
        private int _CityID;
        private int _StateID;
        private int _CountryID;
        private string _PostalCode;

        private int _CreatedBy;
        private DateTime _CretedDatetime;
        private int _UpdatedBy;
        private DateTime _UpdatedDatetime;

        #endregion

        #region Properties

        public int AddressID
        {
            get { return _AddressID; }
            set { _AddressID = value; }
        }

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }

        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }

        public string Landmark
        {
            get { return _Landmark; }
            set { _Landmark = value; }
        }

        public string Street
        {
            get { return _Street; }
            set { _Street = value; }
        }            
        
        public int CityID
        {
            get { return _CityID; }
            set { _CityID = value; }
        }

        public int StateID
        {
            get { return _StateID; }
            set { _StateID = value; }
        }

        public int CountryID
        {
            get { return _CountryID; }
            set { _CountryID = value; }
        }

        public String PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }

        #endregion



        public string SaveAddress(tbl_Address Address)
        {
            return _AddressRepo.saveAddress(Address);
        }
               
        public tbl_Address GetAddressByID(int ID)
        {
            return _AddressRepo.GetAddressByid(ID);
        }

    }
}