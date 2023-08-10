using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TDBAccess
{
    [Serializable]
    public class TableNames : IEnumerable
    {
        IEnumerator IEnumerable.GetEnumerator()
        {
            yield return rec;
        }

        public int rec { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return "Rec#: " + rec + "   Name: " + name;
        }

        public static List<TableNames> GetTables(int DBIndex = 0)
        {
            //Number of tables
            int TableCount = TDB.TDBDatabaseGetTableCount(DBIndex);

            //Table Properties
            TdbTableProperties TableProps = new TdbTableProperties();

            //Table Name
            StringBuilder TableName = new StringBuilder("    ", 5);

            List<TableNames> tablenames = new List<TableNames>();

            for (int i = 0; i < TableCount; i++)
            {
                // Init the tdbtableproperties name
                TableProps.Name = TableName.ToString();

                // Get the tableproperties for the given table number
                if (TDB.TDBTableGetProperties(DBIndex, i, ref TableProps))
                {
                    TableProps.Name = TDB.StrReverse(TableProps.Name);
                    tablenames.Add(new TableNames { rec = i, name = TableProps.Name });
                }
            }
            return tablenames;
        }

        internal static object Find(Func<object, bool> p)
        {
            throw new NotImplementedException();
        }
    }
}
