using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net;
using System.Transactions;
using System.Data;
namespace TMS.Repository
{
    public class governingBodyRepo : BaseRepo
    {
        TMSEntities context = null;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string saveGoverning(tbl_GoverningBody objGoverningBody, tbl_Address objAddress)
        {
           
                context = new TMSEntities();
                using (TransactionScope myTran = new TransactionScope())
                {
                    try
                    {
                        context.AddTotbl_Address(objAddress);
                        context.SaveChanges();
                        objGoverningBody.AddressID = objAddress.AddressID;
                        context.AddTotbl_GoverningBody(objGoverningBody);
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

        public object GetGoverningList()
        {
           
            try
            {
                context = new TMSEntities();
                var query = (from gov in context.tbl_GoverningBody
                             join add in context.tbl_Address
                             on gov.AddressID equals add.AddressID
                             where gov.status == "A"
                             select new
                             {
                                 gov.Name,
                                 gov.GoverningBodyID,
                                 add.Address1,
                                 add.Address2
                             });
                return query;
            }
            catch (Exception ex) { log.Error(ex.Message); return null; }
        }

        public tbl_GoverningBody GetGovernByID(int govid)
        {
            try
            {
                context = new TMSEntities();
                var query = (from gov in context.tbl_GoverningBody where gov.GoverningBodyID == govid select gov).FirstOrDefault();
                return query;
            }
            catch (Exception ex) { log.Error(ex.Message); return null; }
        }

        public string updateGovernBodies(tbl_GoverningBody objGovern, tbl_Address objAddress)
        {
            context = new TMSEntities();

            using (TransactionScope myTran = new TransactionScope())
            {
                try
                {
                    var Gov = (from gov in context.tbl_GoverningBody
                              where gov.GoverningBodyID == objGovern.GoverningBodyID
                              select gov).FirstOrDefault();
                    
                    if (Gov != null)
                    {
                        Gov.Name = objGovern.Name;

                        var Add = (from add in context.tbl_Address
                                   where add.AddressID == Gov.AddressID
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