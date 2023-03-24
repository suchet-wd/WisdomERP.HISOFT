using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace HI.Auditor
{
    internal class ChangeLoggerGlobal
    {
        private Object _MainObject { get; set; }
        public Object MainObject { get { return _MainObject; } }
        public IList<ChangeLog> Changes { get; set; }
        public ChangeLoggerGlobal(Object mainObject)
        {
            this._MainObject = mainObject;
            this.Changes = new List<ChangeLog>();
        }
    }
    /// <summary>
    /// ChangeLogger Class
    /// </summary>
    public class ChangeLogger
    {
        private DataTable dtX { get; set; }
        private DataTable dtY { get; set; }

        private Guid Id { get; set; }
        private String refKey { get; set; }
        private Object X { get; set; }
        private Object Y { get; set; }
        /// <summary>
        /// Indicates if any section of the audit failed
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// If not successful the exception details will be passed through here
        /// </summary>
        public Exception Exception { get; set; }
        private ChangeLoggerGlobal GlobalSettings { get; set; }
        /// <summary>
        /// Lists the differences between the two objects following auditing
        /// </summary>
        public IList<ChangeLog> Changes { get { return GlobalSettings.Changes; } }
        /// <summary>
        /// Create a new instance of ChangeLogger. The Objects must be of the same type
        /// </summary>
        /// <param name="id">The main object id that you want changes logged against</param>
        /// <param name="original">The original object to compare against</param>
        /// <param name="changed">The changed object to check for changes</param>
        public ChangeLogger(string id, object original, object changed)
        {
            this.Success = true;
            if (original.GetType() != changed.GetType())
            {
                this.Success = false;
                this.Exception = new UnmatchedTypeException();
                return;
            }
            
            /* Check that already GUID value */
            Guid newId;
            try
            {
                newId = new Guid(id.ToString());
                this.refKey = String.Empty;
            }
            catch
            {
                newId = Guid.Empty;
                this.refKey = id.ToString();
            }

            this.Id = newId;
            this.X = changed;
            this.Y = original;
            GlobalSettings = new ChangeLoggerGlobal(original);

            if (changed.GetType() == typeof(DataTable))
            {
                this.dtX = (DataTable)changed;
            }

            if (original.GetType() == typeof(DataTable))
            {
                this.dtY = (DataTable)original;
            }
        }

        private ChangeLogger(string id, ChangeLoggerGlobal globalSettings, object original, object changed)
        {
            /* Check that already GUID value */
            Guid newId;
            try
            {
                newId = new Guid(id.ToString());
                this.refKey = String.Empty;
            }
            catch
            {
                newId = Guid.Empty;
                this.refKey = id.ToString();
            }

            this.Id = newId;
            this.X = changed;
            this.Y = original;
            this.GlobalSettings = globalSettings;

            if (changed.GetType() == typeof(DataTable))
            {
                this.dtX = (DataTable)changed;
            }

            if (original.GetType() == typeof(DataTable))
            {
                this.dtY = (DataTable)original;
            }
        }

        /// <summary>
        /// Takes the two objects from the constructor and checks for differences between the object types decorated with the Audited attribute.
        /// Differences between the two objects are logged in the Changes List.
        /// </summary>
        //public void Audit()
        //{
        //    try
        //    {
        //        PropertyInfo[] propertyList = X.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        //        foreach (PropertyInfo PI in propertyList)
        //        {
        //            if (IsAudited(PI))
        //            {
        //                if (isOfSimpleType(PI.GetValue(X, null)))
        //                {
        //                    Compare(new TypePropertyInfoPair(X.GetType(), PI));
        //                }
        //                else
        //                {
        //                    ChangeLogger logger = new ChangeLogger(this.Id, GlobalSettings, PI.GetValue(Y, null), PI.GetValue(X, null));
        //                    logger.Audit();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        this.Success = false;
        //        this.Exception = ex;
        //    }
        //}

        public void Audit()
        {
            try

            {
                int chageType  = 0;
                int chageNew = 0;
                int chageDelete = 0;
                for (int i = 0; i < dtY.Rows.Count; i++)
                {
                    chageType = 0;
                    chageNew = 0;
                    chageDelete = 0;

                    for (int c = 0; c < dtY.Columns.Count; c++)
                    {
                        if (dtY.Rows[i][c].ToString() != "")
                        {
                            chageNew = 1;
                        }
                    }

                    if (chageNew == 0) { chageType = 1; };

                    if (chageType == 0)
                    {
                        for (int c = 0; c < dtX.Columns.Count; c++)
                        {
                            if (dtX.Rows[i][c].ToString() != "")
                            {
                                chageDelete = 1;
                            }
                        }

                        if (chageDelete == 0) { chageType = 2; };
                    };

                    for (int c = 0; c < dtY.Columns.Count; c++)
                    {
                        try
                        {
                            //DateTime time1 = Convert.ToDateTime(dtY.Rows[i][c]);
                            //DateTime time2 = Convert.ToDateTime(dtX.Rows[i][c]);
                            switch (dtY.Columns[c].ColumnName.ToString())
                            {
                                case "FDInsDate":
                                    break;
                                case "FTInsTime":
                                    break;
                                case "FTUpdUser":
                                    break;
                                case "FDUpdDate":
                                    break;
                                case "FTUpdTime":
                                    break;
                                default :
                                    //if (isOfSimpleType(time1))
                                    //    Compare(dtY.Columns[c].ColumnName.ToString(), dtY.Rows[i][c].ToString(), dtX.Rows[i][c].ToString());                                    

                                    //if (isOfSimpleType(dtY.Rows[i][c]))
                                    if (isOfSimpleType(dtY.Columns[c]))
                                    {
                                        if (!Equals(dtY.Rows[i][c].ToString() , dtX.Rows[i][c].ToString() ))
                                            Compare(dtY.Columns[c].ColumnName.ToString(), dtY.Rows[i][c].ToString(), dtX.Rows[i][c].ToString(), chageType);
                                    }
                                    break;                                
                            }
                        }
                        catch
                        {
                           // if (isOfSimpleType(dtY.Rows[i][c]))
                            if (isOfSimpleType(dtY.Columns[c]))
                            {
                               // if (!Equals(dtY.Rows[i][c], dtX.Rows[i][c]))
                                if (!Equals(dtY.Rows[i][c].ToString(), dtX.Rows[i][c].ToString()))
                                    Compare(dtY.Columns[c].ColumnName.ToString(), dtY.Rows[i][c].ToString(), dtX.Rows[i][c].ToString(), chageType);
                            }
                        }
                    }
                }
            }
            catch 
            {
                this.Success = true;
                //this.Exception = ex;
            }
        }

        private bool isOfSimpleType(object o)
        {
            if (o == null) return true;
            var type = ((DataColumn)o).DataType;
            return type.IsPrimitive
                            || type == typeof(String)
                            || type == typeof(Decimal)
                            || type == typeof(int)
                            || type == typeof(Int16)
                            || type == typeof(Int32)
                            || type == typeof(Int64)
                          ;
        }

        /// <summary>
        /// Check if a particular property is decorated with Audited attribute
        /// </summary>
        /// <param name="property">The reflected property to be checked</param>
        /// <returns>Boolean indicating if it has the Audited attribute</returns>
        private bool IsAudited(PropertyInfo property)
        {
            bool result = false;
            foreach (Attribute att in property.GetCustomAttributes(typeof(Audited), true))
            {
                if (!result && att is Audited) result = true;
            }
            return result;
        }
        /// <summary>
        /// Check if a particular field is decorated with Audited attribute
        /// </summary>
        /// <param name="field">The reflected field to be checked</param>
        /// <returns>Boolean indicating if it has the Audited attribute</returns>
        private bool IsAudited(FieldInfo field)
        {
            bool result = false;
            foreach (Attribute att in field.GetCustomAttributes(typeof(Audited), true))
            {
                if (!result && att is Audited) result = true;
            }
            return result;
        }

        //private void Compare(TypePropertyInfoPair typePI)
        //{
        //    IComparable valx = typePI.PropertyInfo.GetValue(X, null) as IComparable;
        //    var valy = typePI.PropertyInfo.GetValue(Y, null);
        //    ChangeLog log;

        //    if (valx == null && valy == null) return;
        //    if (valx == null && valy != null)
        //    {
        //        log = new ChangeLog(GlobalSettings.MainObject.GetType().Name, GlobalSettings.MainObject.ToString(), Id, ((GlobalSettings.MainObject.GetType() == X.GetType()) ? string.Empty : typePI.Type.Name + ":") + typePI.PropertyInfo.Name, string.Empty, valy.ToString(), Id);
        //        GlobalSettings.Changes.Add(log);
        //    }
        //    else if (valx != null && valy == null)
        //    {
        //        log = new ChangeLog(GlobalSettings.MainObject.GetType().Name, GlobalSettings.MainObject.ToString(), Id, ((GlobalSettings.MainObject.GetType() == X.GetType()) ? string.Empty : typePI.Type.Name + ":") + typePI.PropertyInfo.Name, valx.ToString(), string.Empty, Id);
        //        GlobalSettings.Changes.Add(log);
        //    }
        //    else
        //    {
        //        if (valx.CompareTo(valy) != 0)
        //        {
        //            log = new ChangeLog(GlobalSettings.MainObject.GetType().Name, GlobalSettings.MainObject.ToString(), Id, ((GlobalSettings.MainObject.GetType() == X.GetType()) ? string.Empty : typePI.Type.Name + ":") + typePI.PropertyInfo.Name, valx.ToString(), valy.ToString(), Id);
        //            GlobalSettings.Changes.Add(log);
        //        }
        //    }
        //}

        private void Compare(string colName, string colOldValue, string colNewValue,int ChangeType)
        {
            var valx = colNewValue;
            var valy = colOldValue;
            ChangeLog log;

            if (valx == null && valy == null) return;
            if (valx == null && valy != null)
            {
                log = new ChangeLog(GlobalSettings.MainObject.GetType().Name, Id, colName, valy.ToString(), string.Empty, Id, refKey, ChangeType);
                GlobalSettings.Changes.Add(log);
            }
            else if (valx != null && valy == null)
            {
                log = new ChangeLog(GlobalSettings.MainObject.GetType().Name, Id, colName, string.Empty, valx.ToString(), Id, refKey, ChangeType);
                GlobalSettings.Changes.Add(log);
            }
            else
            {
                if (valx.CompareTo(valy) != 0)
                {
                    log = new ChangeLog(GlobalSettings.MainObject.GetType().Name, Id, colName, valy.ToString(), valx.ToString(), Id, refKey, ChangeType);
                    GlobalSettings.Changes.Add(log);
                }
            }
        }

    }

    public class TypePropertyInfoPair
    {
        internal Type Type { get; set; }
        internal PropertyInfo PropertyInfo { get; set; }
        private FieldInfo FieldInfo { get; set; }

        public TypePropertyInfoPair(Type type, FieldInfo fieldInfo)
        {
            this.Type = type;
            this.FieldInfo = fieldInfo;
        }

        public TypePropertyInfoPair(Type type, PropertyInfo propertyInfo)
        {
            this.Type = type;
            this.PropertyInfo = propertyInfo;
        }
    }
}
