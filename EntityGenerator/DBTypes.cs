using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityGenerator
{
    internal class DBType
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    internal class DBTypes
    {
        public List<DBType> dbtypes { get; set; }
        public DBTypes()
        {
            dbtypes = new List<DBType> {
                new DBType{name="MySql",value ="0" },
                new DBType{name="SqlServer",value ="1" },
                new DBType{name="Sqlite",value ="2" },
                new DBType{name="Oracle",value ="3" },
                new DBType{name="PostgreSQL",value ="4" },
                new DBType{name="Dm",value ="5" },
                new DBType{name="Kdbndp",value ="6" },
                new DBType{name="Oscar",value ="7" }
            };
        }
    }

}
