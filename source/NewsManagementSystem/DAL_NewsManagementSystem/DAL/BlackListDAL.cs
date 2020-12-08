﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_NewsManagementSystem.Models;
using DAL_NewsManagementSystem.JoinningTable;
namespace DAL_NewsManagementSystem.DAL
{
    public class BlackListDAL
    {
        private NewsManagementSystemEntities _db = new NewsManagementSystemEntities();
        public BlackListDAL()
        {

        }
        public IEnumerable<JBlackList> GetAllBlackList()
        {

            var query = from bll in _db.BlackLists
                        join fbt in _db.FacebookTypes on bll.FacebookTypeID equals fbt.FacebookTypeID
                        select new JBlackList
                        {
                            FacebookID = bll.FacebookID,
                            FacebookName = bll.FacebookName,
                            FacebookUrl = bll.FacebookUrl,
                            FacebookTypeID = bll.FacebookTypeID,
                            FacebookTypeName = fbt.FacebookTypeName
                        };
            return query;
        }
        public bool CheckExistID(string facebookID)
        {
            BlackList blackList = _db.BlackLists.SingleOrDefault(b => b.FacebookID == facebookID);
            if(blackList != null)
            {
                return true;
            }
            return false;
        }
    }
}
