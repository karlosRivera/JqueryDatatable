using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JQDataTable.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
//            string data = @"[{  
//             \""columns\"": [{  
//              \""title\"": \""col1_name\"",  
//              \""data\"": \""col1_name\""  
//             }, {  
//              \""title\"": \""col2_name\"",  
//              \""data\"": \""col2_name\""  
//             }],  
//             \""data\"": [{  
//              \""col1_name\"": \""col1 data\"",  
//              \""col2_name\"": \""col2 data\""  
//             }, {  
//              \""col1_name\"": \""col1 data\"",  
//              \""col2_name\"": \""col2 data\""  
//             }]  
//            }]";

            string data = @"[{  
                         \""columns\"": [{  
                          \""title\"": \""ID\"",  
                          \""data\"": \""ID\""  
                         }, {  
                          \""title\"": \""Name\"",  
                          \""data\"": \""Name\""  
                         }],  
                         \""data\"": [{  
                          \""ID\"": \""1\"",  
                          \""Name\"": \""Tridip\""  
                         }, {  
                          \""ID\"": \""2\"",  
                          \""Name\"": \""Arijit\""  
                         }]  
                        }]";

            TestData t = new TestData();
            t.jsondata = data.Replace(@"\""", "\"");
            return View(t);


        }

        public ActionResult DataTableJson()
        {
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            TestData t = new TestData();
            List<columnsinfo> _col = new List<columnsinfo>();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID", typeof(Int32));
            dt.Columns.Add("Name", typeof(string));

            DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "Ajay";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "Sanu";
            dt.Rows.Add(dr);

            for (int i = 0; i <= dt.Columns.Count - 1;i++ )
            {
                _col.Add(new columnsinfo { title = dt.Columns[i].ColumnName, data = dt.Columns[i].ColumnName });
            }

            string col =  (string)serializer.Serialize(_col);
            t.columns = col;


            var lst = dt.AsEnumerable()
            .Select(r => r.Table.Columns.Cast<DataColumn>()
                    .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])
                   ).ToDictionary(z => z.Key, z => z.Value)
            ).ToList();

            string data= serializer.Serialize(lst);
            t.data = data;

            return View(t);
        }

        [HttpGet]
        public ActionResult BindDataTable()
        {
            List<UserData> data = new List<UserData>
                {
                    new UserData
                    {
                        ID = 1,
                        Name = "Simon"
                    },
                    new UserData
                    {
                        ID = 2,
                        Name = "Alex"
                    }
                };

            return View(data);
        }

        [HttpGet]
        public ActionResult PrintModel()
        {
            List<UserData> data = new List<UserData>
                {
                    new UserData
                    {
                        ID = 1,
                        Name = "Simon"
                    },
                    new UserData
                    {
                        ID = 2,
                        Name = "Alex"
                    }
                };

            return View(data);
        }
    }

    public class TestData
    {
        public string jsondata { get; set; }
        public string columns { get; set; }
        public string data { get; set; }
    }

     public class columnsinfo
     {
         public string title { get; set; }
         public string data { get; set; }
     }
}