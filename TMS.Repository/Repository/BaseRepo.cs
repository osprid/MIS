using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Transactions;

namespace TMS.Repository
{
    public class BaseRepo
    {
        public string strMessage;
        public Boolean saved;

        public BaseRepo()
        {

        }
    }
}