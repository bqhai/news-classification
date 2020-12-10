﻿using DAL_NewsManagementSystem.JoinningTable;
using DAL_NewsManagementSystem.Models;
using System.Collections.Generic;
using System.Linq;
namespace DAL_NewsManagementSystem.DAL
{
    public class WatchListDAL
    {
        private NewsManagementSystemEntities _db = new NewsManagementSystemEntities();
        public WatchListDAL()
        {

        }
        public IEnumerable<JWatchList> GetAllWatchList()
        {
            var query = from wl in _db.WatchLists
                        join fbt in _db.FacebookTypes on wl.FacebookTypeID equals fbt.FacebookTypeID
                        select new JWatchList
                        {
                            FacebookID = wl.FacebookID,
                            FacebookName = wl.FacebookName,
                            FacebookUrl = wl.FacebookUrl,
                            FacebookTypeID = wl.FacebookTypeID,
                            FacebookTypeName = fbt.FacebookTypeName
                        };
            return query;
        }
        public bool CheckExistInWatchList(string facebookID)
        {
            WatchList watchList = _db.WatchLists.SingleOrDefault(b => b.FacebookID == facebookID);
            if(watchList != null)
            {
                return true;
            }
            return false;
        }
        public void AddToWatchList(WatchList watchList)
        {
            _db.WatchLists.Add(watchList);
            _db.SaveChanges();
        }
    }
}
