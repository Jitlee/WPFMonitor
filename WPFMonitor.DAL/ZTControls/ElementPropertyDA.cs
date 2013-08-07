using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DBUtility;
using WPFMonitor.Model.ZTControls;

namespace WPFMonitor.DAL.ZTControls
{
    /// <summary>
    /// 
    /// </summary>
    public class ElementPropertyDA : DALBase
    {
        private static readonly ElementPropertyDA _instance = new ElementPropertyDA();

        private static Dictionary<int, List<t_ElementProperty>> _caches = new Dictionary<int, List<t_ElementProperty>>();

        #region 查询

        public List<t_ElementProperty> SelectBy(int elementID)
        {
            return db.ExecuteQuery("select * from t_ElementProperty where ElementID = @ElementID",
                new[]
                {
                    new SqlParameter("@ElementID", elementID),
                })
                .Rows
                .OfType<DataRow>()
                .Select(r => new t_ElementProperty(r))
                .ToList();
        }

        public static List<t_ElementProperty> SelectByElementID(int elementID)
        {
            if (!_caches.ContainsKey(elementID))
            {
                List<t_ElementProperty> list = _instance.db.ExecuteQuery("SELECT * FROM t_ElementProperty WHERE ElementID = @ElementID",
                new SqlParameter("@ElementID", elementID))
                .Rows
                .OfType<DataRow>()
                .Select(r => new t_ElementProperty(r))
                .ToList();

                _caches.Add(elementID, list);

                return list;
            }
            return _caches[elementID];
        }

		//public DataTable selectAllDateByWhere(int pageCrrent, int pageSize, out int pageCount, string where)
		//{
		//    string sql = "select * from t_ElementProperty";
		//    if (!string.IsNullOrEmpty(where))
		//    {
		//        sql = string.Format(" {0} where {1}", sql, where);
		//    }
		//    DataTable dt = null;
		//    int returnC = 0; try
		//    {
		//        dt = db.ExecuteQuery(sql, pageCrrent, pageSize, out returnC);
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//    pageCount = returnC;
		//    return dt;
		//}

		//public t_ElementProperty selectARowDate(string m_id)
		//{
		//    string sql = string.Format("select * from t_ElementProperty where  Elementid='{0}'", m_id);
		//    DataTable dt = null;
		//    try
		//    {
		//        dt = db.ExecuteQueryDataSet(sql).Tables[0];
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//    if (dt == null)
		//        return null;
		//    if (dt.Rows.Count == 0)
		//        return null;
		//    DataRow dr = dt.Rows[0];
		//    t_ElementProperty m_Elem = new t_ElementProperty(dr);
		//    return m_Elem;

		//}

        public List<t_ElementProperty> selectByScreenID(int screenID)
        {
            string sql = "select * from t_ElementProperty where exists(select 0 from t_element where t_element.ScreenID=" + screenID.ToString() + ")";

            DataTable dt = null;
            try
            {
                dt = db.ExecuteQuery(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            List<t_ElementProperty> _List = new List<t_ElementProperty>();
            foreach (DataRow dr in dt.Rows)
            {
                t_ElementProperty obj = new t_ElementProperty(dr);
                _List.Add(obj);
            }
            return _List;
        }

		//public List<t_ElementProperty> selectAllDate()
		//{
		//    string sql = "select * from t_ElementProperty";

		//    DataTable dt = null;
		//    try
		//    {
		//        dt = db.ExecuteQuery(sql);
		//    }
		//    catch (Exception ex)
		//    {
		//        throw ex;
		//    }
		//    List<t_ElementProperty> _List = new List<t_ElementProperty>();
		//    foreach (DataRow dr in dt.Rows)
		//    {
		//        t_ElementProperty obj = new t_ElementProperty(dr);
		//        _List.Add(obj);
		//    }
		//    return _List;
		//}


        #endregion

		//#region 插入
		///// <summary>
		///// 插入t_ElementProperty
		///// </summary>
		//public virtual bool Insert(t_ElementProperty elementProperty)
		//{
		//    string sql = "insert into t_ElementProperty (ElementID, PropertyNo, PropertyValue, Caption, PropertyName) values (@ElementID, @PropertyNo, @PropertyValue, @Caption, @PropertyName)";
		//    SqlParameter[] parameters = new SqlParameter[]
		//    {
		//        new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, elementProperty.ElementID),
		//        new SqlParameter("@PropertyNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PropertyNo", DataRowVersion.Default, elementProperty.PropertyNo),
		//        new SqlParameter("@PropertyValue", SqlDbType.VarChar, 4096, ParameterDirection.Input, false, 0, 0, "PropertyValue", DataRowVersion.Default, elementProperty.PropertyValue),
		//        new SqlParameter("@Caption", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Caption", DataRowVersion.Default, elementProperty.Caption),
		//        new SqlParameter("@PropertyName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PropertyName", DataRowVersion.Default, elementProperty.PropertyName)
		//    };
		//    return db.ExecuteNoQuery(sql, parameters) > -1;
		//}
		//#endregion

		//#region 修改
		///// <summary>
		///// 更新t_ElementProperty
		///// </summary>
		//public virtual bool Update(t_ElementProperty elementProperty)
		//{
		//    string sql = "update t_ElementProperty set  PropertyNo = @PropertyNo,  PropertyValue = @PropertyValue,  Caption = @Caption,  PropertyName = @PropertyName where  ElementID = @ElementID";
		//    SqlParameter[] parameters = new SqlParameter[]
		//    {
		//        new SqlParameter("@ElementID", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "ElementID", DataRowVersion.Default, elementProperty.ElementID),
		//        new SqlParameter("@PropertyNo", SqlDbType.Int, 4, ParameterDirection.Input, false, 0, 0, "PropertyNo", DataRowVersion.Default, elementProperty.PropertyNo),
		//        new SqlParameter("@PropertyValue", SqlDbType.VarChar, 4096, ParameterDirection.Input, false, 0, 0, "PropertyValue", DataRowVersion.Default, elementProperty.PropertyValue),
		//        new SqlParameter("@Caption", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "Caption", DataRowVersion.Default, elementProperty.Caption),
		//        new SqlParameter("@PropertyName", SqlDbType.VarChar, 50, ParameterDirection.Input, false, 0, 0, "PropertyName", DataRowVersion.Default, elementProperty.PropertyName)
		//    };
		//    return db.ExecuteNoQuery(sql, parameters) > -1;
		//}
		//#endregion

		//#region DELETE
		///// <summary>
		///// 删除t_ElementProperty
		///// </summary>
		//public virtual bool Delete(IList mlist)
		//{
		//    if (mlist == null)
		//        return false;
		//    List<CommandList> listcmd = new List<CommandList>();
		//    foreach (t_ElementProperty obj in mlist)
		//    {
		//        string sql = string.Format(" delete from t_ElementProperty where  Elementid = '{0}'", obj.ElementID);
		//        listcmd.Add(new CommandList() { strCommandText = sql, Type = CommandType.Text });
		//    }
		//    return db.ExecuteNoQueryTranPro(listcmd);
		//}

		//public virtual bool Delete(int elementID)
		//{
		//    string sql = string.Format(" delete from t_ElementProperty where  Elementid = '{0}'", elementID);
		//    return db.ExecuteNoQuery(sql) > 0;
		//}
		//#endregion
    }
}

