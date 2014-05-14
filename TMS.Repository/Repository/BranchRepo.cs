using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Data;
using log4net;
namespace TMS.Repository
{
    public class BranchRepo : BaseRepo
    {
        TMSEntities context;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string SaveBranch(tbl_InstituteBranch objInstituteBranch, tbl_Address objAddress)
        {
            context = new TMSEntities();

            using (TransactionScope myTran = new TransactionScope())
            {
                try
                {
                    context.AddTotbl_Address(objAddress);
                    context.SaveChanges();
                    objInstituteBranch.AddressID = objAddress.AddressID;
                    context.AddTotbl_InstituteBranch(objInstituteBranch);
                    context.SaveChanges();
                    saved = true;
                }
                catch (OptimisticConcurrencyException e)
                {
                    strMessage = "Data_save_error";
                    log.Error(e.Message);
                }
                catch (Exception ex)
                {
                    strMessage = "Data_save_error";
                    log.Error(ex.Message);
                }

                finally
                {
                    if (saved)
                    {
                        myTran.Complete();
                        context.AcceptAllChanges();
                        strMessage = "Data_save_success";
                        context.Dispose();
                    }
                }
            }
            return strMessage;
        }
        public object GetBranchList()
        {
            try
            {
                context = new TMSEntities();
                var query = (from branch in context.tbl_InstituteBranch
                             join add in context.tbl_Address
                             on branch.AddressID equals add.AddressID
                             where branch.status == "A"
                             select new
                             {
                                 branch.BranchName,
                                 branch.BranchID,
                                 add.Address1,
                                 add.Address2
                             });
                return query;
            }
            catch (Exception ex) { log.Error(ex.Message); return null; }
        }
        public tbl_InstituteBranch GetBranchByID(int BranchID)
        {
            try
            {
                context = new TMSEntities();

                var query = (from branch in context.tbl_InstituteBranch where branch.BranchID == BranchID select branch).FirstOrDefault();

                return query;
            }
            catch (Exception ex) { log.Error(ex.Message); return null; }
        }
        public string updateBranch(tbl_InstituteBranch objBranch, tbl_Address objAddress)
        {
            context = new TMSEntities();

            using (TransactionScope myTran = new TransactionScope())
            {
                try
                {
                    var query = (from branch in context.tbl_InstituteBranch
                                where branch.BranchID == objBranch.BranchID
                                select branch).FirstOrDefault();
                    

                    if (query != null)
                    {
                        query.BranchName = objBranch.BranchName;
                        query.description = objBranch.description;
                        var Add = (from add in context.tbl_Address
                                   where add.AddressID == query.AddressID
                                   select add).FirstOrDefault();
                        Add.Address1 = objAddress.Address1;
                        Add.Address2 = objAddress.Address2;
                        Add.CityID = objAddress.CityID;
                        Add.StateID = objAddress.StateID;
                        Add.ContactID = objAddress.CountryID;
                        Add.Landmark = objAddress.Landmark;
                        Add.Postalcode = objAddress.Postalcode;
                        Add.Street = objAddress.Street;
                        context.SaveChanges();
                    }
                    saved = true;
                }
                catch (OptimisticConcurrencyException e)
                {
                    strMessage = "Data_update_error";
                    log.Error(e.Message);
                }
                catch (Exception ex)
                {
                    strMessage = "Data_update_error";
                    log.Error(ex.Message);
                }

                finally
                {
                    if (saved)
                    {
                        myTran.Complete();
                        context.AcceptAllChanges();
                        strMessage = "Data_update_success";
                        context.Dispose();
                    }
                }
            }
            return strMessage;
        }
    }
}