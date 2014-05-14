using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Transactions;
using System.Collections;
using log4net;
namespace TMS.Repository
{
    public class InstituteRepo : BaseRepo
    {
         TMSEntities context = null;
         private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
         private AddressRepo _AddressRepo ;

         public InstituteRepo()
        {
           _AddressRepo = new AddressRepo();
        }

         #region methods

         public string saveInstitute(tbl_Institute Institute)
         {

             context = new TMSEntities();

             using (TransactionScope myTran = new TransactionScope())
             {
                 try
                 {
                     context.AddTotbl_Institute(Institute);
                     context.SaveChanges();
                     //Institute.Institute = Institute.Institute;
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

         public string saveCompleteTrans(tbl_Institute Institute, tbl_Address Address)
         {
             context = new TMSEntities();

             using (TransactionScope myTran = new TransactionScope())
             {
                 try
                 {
                     context.AddTotbl_Address(Address);
                     context.SaveChanges();                   
                     Institute.AddressID=Address.AddressID;
                     context.AddTotbl_Institute(Institute);
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
      
         public object getInstituteList()
         {
             try
             {
                 context = new TMSEntities();

                 var query = (from inst in context.tbl_Institute
                              join add in context.tbl_Address
                              on inst.AddressID equals add.AddressID
                              where inst.Status == "A"
                              orderby inst.Institute descending
                              select new
                              {
                                  inst.Institute,
                                  inst.Name,
                                  add.Address1,
                                  add.Address2
                              }
                              );

                 return query;
             }
             catch (Exception ex) { log.Error(ex.Message); return null; }
         }

     
         public List<tbl_Institute> getInstitute()
         {
             try
             {
                 context = new TMSEntities();
                 return context.tbl_Institute.ToList();
             }
             catch (Exception ex) { log.Error(ex.Message); return null; }
         }


         public tbl_Institute GetInstituteByID(int InstID)
         {
             try
             {
                 context = new TMSEntities();

                 var query = from inst in context.tbl_Institute where inst.Institute == InstID select inst;

                 return query.FirstOrDefault();
             }
             catch (Exception ex) { log.Error(ex.Message); return null; }
         }

         public string updateCompleteTrans(tbl_Institute objInstitute, tbl_Address objAddress)
         {
             context = new TMSEntities();

             using (TransactionScope myTran = new TransactionScope())
             {
                 try
                 {
                     var Inst = (from inst in context.tbl_Institute
                                 where inst.Institute == objInstitute.Institute
                                 select inst).FirstOrDefault();
                     if (Inst != null)
                     {
                         Inst.Name = objInstitute.Name;
                         
                         var Add=(from add in context.tbl_Address
                                  where add.AddressID==Inst.AddressID
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
         #endregion
    }
}