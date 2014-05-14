using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TMS.Repository;
namespace TMS.DataModel
{
    public class BranchModel
    {
        BranchRepo _BranchRepo=new BranchRepo();
        AddressRepo _AddressRepo = new AddressRepo();
        #region Fields
        private int _BranchID;
        private string _BranchName;
        private string _BranchDesc;
        private AddressModel _AddressModel;
        #endregion
        #region Properties
        public int BranchID{get { return _BranchID; }set { _BranchID = value; }}
        public string BranchName{get { return _BranchName; }set { _BranchName = value; }}
        public string BranchDesc{get { return _BranchDesc; }set { _BranchDesc = value; }}
        public AddressModel Address{get { return _AddressModel; }set { _AddressModel = value; }}
        #endregion
        public BranchModel()
        {
            _AddressModel = new AddressModel();
        }
        public string SaveBranch(tbl_InstituteBranch objInstituteBranch, tbl_Address objAddress)
        {
            return _BranchRepo.SaveBranch(objInstituteBranch,objAddress);
        }

        public object GetBranchList()
        {
            return _BranchRepo.GetBranchList();
        }

        public tbl_InstituteBranch GetBranchByID(int BranchID)
        {
            return _BranchRepo.GetBranchByID(BranchID);
        }
        public void getGovernBodyDetails(int BranchID)
        {
            tbl_InstituteBranch objInstituteBranch = GetBranchByID(BranchID);
            if (objInstituteBranch != null)
            {
                BranchName = objInstituteBranch.BranchName;
                tbl_Address add = _AddressRepo.GetAddressByid((int)objInstituteBranch.AddressID);
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

        public string updateBranch(tbl_InstituteBranch objBranch, tbl_Address objAddress)
        {
            return _BranchRepo.updateBranch(objBranch,objAddress);
        }
    }
}