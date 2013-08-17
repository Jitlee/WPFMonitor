using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.DAL.ZTControls
{
    public class ScreenShortcutDA : DALBase
    {
        public ObservableCollection<t_ScreenShortcut> Select()
        {
            DataTable dt = base.db.ExecuteQuery("SELECT ID, SCREENID, SCREENNAME, SCREENDESCRIPTION, SCREENIMAGE FROM T_SCREEN_SHORTCUT");
            ObservableCollection<t_ScreenShortcut> result = new ObservableCollection<t_ScreenShortcut>();

            foreach(DataRow row in dt.Rows)
            {
                result.Add(new t_ScreenShortcut(row));
            }
            return result;
        }

        public void Insert(t_ScreenShortcut shortcut)
        {
            string sql = "INSERT INTO T_SCREEN_SHORTCUT(SCREENID, SCREENNAME, SCREENDESCRIPTION, SCREENIMAGE) VALUES(@SCREENID, @SCREENNAME, @SCREENDESCRIPTION, @SCREENIMAGE)";
            base.db.ExecuteNoQuery(
                sql,
                new SqlParameter[]{
                    new SqlParameter("@SCREENID", shortcut.ScreenId),
                    new SqlParameter("@SCREENNAME", shortcut.ScreenName ?? string.Empty),
                    new SqlParameter("@SCREENDESCRIPTION", shortcut.ScreenDescription ?? string.Empty),
                    new SqlParameter("@SCREENIMAGE", shortcut.ImageBuffer ?? new byte[0])
                });
        }

        public void Update(t_ScreenShortcut shortcut)
        {
            string sql = "UPDATE T_SCREEN_SHORTCUT SET SCREENID = @SCREENID, SCREENNAME = @SCREENNAME, SCREENDESCRIPTION = @SCREENDESCRIPTION, SCREENIMAGE = @SCREENIMAGE WHERE ID = @ID";
            base.db.ExecuteNoQuery(
                sql,
                new SqlParameter[]{
                    new SqlParameter("@ID", shortcut.Id),
                    new SqlParameter("@SCREENID", shortcut.ScreenId),
                    new SqlParameter("@SCREENNAME", shortcut.ScreenName),
                    new SqlParameter("@SCREENDESCRIPTION", shortcut.ScreenDescription),
                    new SqlParameter("@SCREENIMAGE", shortcut.ImageBuffer)
                });
        }

        public void Delete(int id)
        {
            string sql = "DELETE T_SCREEN_SHORTCUT WHERE ID = @ID";
            base.db.ExecuteNoQuery(sql, new SqlParameter("@ID", id));
        }
    }
}
