using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DataManager
    {
        public static List<Users> GetUsers()
        {
            using (DALContext cnx = new DALContext())
            {
                return cnx.Users.ToList();
            }
        }

        public static List<user_franchaser> GetFranchizers()
        {
            using (DALContext cnx = new DALContext())
            {
                return cnx.user_franchaser.ToList();
            }
        }

        public static List<application> GetApps()
        {
            using (DALContext cnx = new DALContext())
            {
                return cnx.application.ToList();
            }
        }

        public static void CreateFranchize(user_franchaser uf)
        {
            using (DALContext cnx = new DALContext())
            {
                cnx.user_franchaser.AddObject(uf);
                cnx.SaveChanges();
            }
        }

        public static void CreateOwner(user_owner no)
        {
            using (DALContext cnx = new DALContext())
            {
                cnx.user_owner.AddObject(no);
                cnx.SaveChanges();
            }
        }

        public static bool IsFrinchizerExist(int id)
        {
            using (DALContext cnx = new DALContext())
            {
                return cnx.user_franchaser.Where(c => c.id == id).Count() > 0;
            }
        }

        public static int GetOwnerId(string userId)
        {
            using (DALContext cnx = new DALContext())
            {
                user_owner o = cnx.user_owner.Where(c => c.user_id.Equals(userId)).FirstOrDefault();
                if (o != null) return o.id;
                else return 0;
            }
        }

        public static int CreateApp(application app)
        {
            int modificationCount = 0;

            using (DALContext cnx = new DALContext())
            {
                cnx.application.AddObject(app);
                modificationCount += cnx.SaveChanges();

                cnx.skins.AddObject(new skins
                {
                    app_id = app.id
                });
                cnx.themes.AddObject(new themes
                {
                    app_id = app.id
                });
                modificationCount += cnx.SaveChanges();
            }

            return modificationCount;
        }

        public static application GetAppById(int id)
        {
            using (DALContext cnx = new DALContext())
            {
                return cnx.application.Where(x => x.id == id).FirstOrDefault();
            }
        }

        public static int DeleteApp(int appId,int ownerId)
        {
            using (DALContext cnx = new DALContext())
            {
                application app = cnx.application.Where(x => x.id == appId && x.owner_id == ownerId).FirstOrDefault();
                cnx.application.DeleteObject(app);
                return cnx.SaveChanges();
            }
        }

        public static int UpdateApp(application app)
        {
            using (DALContext cnx = new DALContext())
            {
                application realApp = cnx.application.Where(x => x.id == app.id && x.owner_id == app.owner_id).FirstOrDefault();
                if (realApp != null)
                {
                    realApp.is_active = app.is_active;
                    realApp.app_name = app.app_name;
                    return cnx.SaveChanges();
                }
                else return 0;
            }
        }

        public static int AddSkin(skins skn)
        {
            using (DALContext cnx = new DALContext())
            {
                cnx.skins.AddObject(skn);
                return cnx.SaveChanges();
            }
        }
    }
}
