using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TutoriasUTEapp.Models;

namespace TutoriasUTEapp.Areas.Api.Models
{
    public class AndroidRecordatoriosManager
    {

        public static List<AndroidRecordatorios> Recordatorios(int role)
        {
            TutoriasUTEDbContext dbCtx = new TutoriasUTEDbContext();

            List<AndroidRecordatorios> recordatoriosRet = new List<AndroidRecordatorios>();

            var query = (from r in dbCtx.Reminders
                         where r.RoleID == role
                         select new
                         {
                             Date = r.Date,
                             Subject = r.Subject,
                             Reminder = r.Description
                         }).ToList();

            foreach(var reminder in query)
            {
                AndroidRecordatorios recordatorio = new AndroidRecordatorios();
                recordatorio.Fecha = reminder.Date;
                recordatorio.Asunto = reminder.Subject;
                recordatorio.Recordatorio = reminder.Reminder;

                //se agrega a la lista
                recordatoriosRet.Add(recordatorio);
            }

            return recordatoriosRet;
        }
    }
}