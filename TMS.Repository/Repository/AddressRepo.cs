using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Transactions;
using System.Data;
using log4net;
namespace TMS.Repository
{
    public class AddressRepo : BaseRepo
    {
        TMSEntities context = null;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public string saveAddress(tbl_Address Address)
        {
            context = new TMSEntities();
           
            using (TransactionScope myTran = new TransactionScope())
            {
                try
                {
                    context.AddTotbl_Address(Address);
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

        public tbl_Address GetAddressByid(int ID)
        {
            try
            {
                context = new TMSEntities();

                var query = from add in context.tbl_Address where add.AddressID == ID select add;

                return query.FirstOrDefault();
            }
            catch (Exception ex) { log.Error(ex.Message); return null; }
        }
    }
}