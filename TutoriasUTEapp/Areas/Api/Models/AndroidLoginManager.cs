using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutoriasUTEapp.Models;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidLoginManager
    {

        public static List<AndroidLoginReturn> isLoged(AndroidLogin login) {


            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidLoginReturn> loginRet = new List<AndroidLoginReturn>();

            //se busca si existe
            var queryLogin = (from t in dbCtx.Teachers
                              join tr in dbCtx.TeacherRoles on t.ID equals tr.TeacherID
                              where tr.RoleID == login.role
                              where t.UserName == login.user && t.UserPassword == login.pass
                              select new
                              {
                                  ID = t.ID
                              }).SingleOrDefault();

            if (queryLogin != null)
            {
                AndroidLoginReturn objLogin = new AndroidLoginReturn();
                objLogin.MaestroID = queryLogin.ID;
                loginRet.Add(objLogin);
                return loginRet;
            }
            else
            {
                AndroidLoginReturn objLogin = new AndroidLoginReturn();
                objLogin.MaestroID = 0;
                loginRet.Add(objLogin);
                return loginRet;
            }

        }
    }
}