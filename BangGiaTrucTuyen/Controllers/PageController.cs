using BangGiaTrucTuyen.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BangGiaTrucTuyen.Controllers
{
    public class PageController : Controller
    {
        // GET: Page
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(@"select MaCoPhieu, " +
                                                            "       GiaMua3,SoLuongMua3," +
                                                            "       GiaMua2,SoLuongMua2," +
                                                            "       GiaMua1,SoLuongMua1," +
                                                            "       GiaKhop,SoLuongKhop," +
                                                            "       GiaBan1,SoLuongBan1," +
                                                            "       GiaBan2,SoLuongBan2," +
                                                            "       GiaBan3,SoLuongBan3," +
                                                            "       TongSo" +
                                                            " from dbo.BangGiaTrucTuyen", connection))
                {
                    // Make sure the command object does not already have
                    // a notification object associated with it.
                    command.Notification = null;

                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    var listTbl = reader.Cast<IDataRecord>()
                            .Select(x => new
                            {
                                MaCoPhieu = (string)x["MaCoPhieu"],
                                GiaMua3 = GetFloatSafty(reader,1),
                                //(float)x["GiaMua3"],
                                SoLuongMua3 = GetIntSafty(reader, 2),
                                GiaMua2 = GetFloatSafty(reader, 3),
                                SoLuongMua2 = GetIntSafty(reader, 4),
                                GiaMua1 = GetFloatSafty(reader, 5),
                                SoLuongMua1 = GetIntSafty(reader, 6),
                                GiaKhop = GetFloatSafty(reader, 7),
                                SoLuongKhop = GetIntSafty(reader, 8),
                                GiaBan1 = GetFloatSafty(reader, 9),
                                SoLuongBan1 = GetIntSafty(reader, 10),
                                GiaBan2 = GetFloatSafty(reader, 11),
                                SoLuongBan2 = GetIntSafty(reader, 12),
                                GiaBan3 = GetFloatSafty(reader, 13),
                                SoLuongBan3 = GetIntSafty(reader, 14),
                                TongSo = GetIntSafty(reader, 15),
                                
                            }).ToList();

                    return Json(new { listTbl = listTbl }, JsonRequestBehavior.AllowGet);

                }
            }
        }

        private String GetFloatSafty(SqlDataReader reader, int col)
        {
            if(!reader.IsDBNull(col))
            {
                return reader.GetDouble(col).ToString();
            }    
            return "";
        }

        private String GetIntSafty(SqlDataReader reader, int col)
        {
            if (!reader.IsDBNull(col))
            {
                return reader.GetInt32(col).ToString();
            }
            return "";
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            PageHub.Show();
        }
    }
}