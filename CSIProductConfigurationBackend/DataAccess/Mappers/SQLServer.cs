using CSIProductConfigurationBackend.DataAccess.Interfaces;
using Niu.OneWorkspace.DataAccess.Mappers.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CSIProductConfigurationBackend.DataAccess
{
    public class SqlServerDataMapper<T> : ICSIProductConfigurationSqlServerDataMapper<T>, ISqlServerDataMapper<T> where T : class
    {
        List<PropertyInfo> props = typeof(T).GetProperties().ToList();

        /// <summary>
        /// Maps the database entity to common entity
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public T Map(DataRow dataRow)
        {
            T dbEntity = Activator.CreateInstance<T>();

            //implement one to one mapping
            props = typeof(T).GetProperties().ToList();

            foreach (PropertyInfo info in props)
            {
                object propertyValue = null;
                try
                {
                    //for each property get value
                    propertyValue = dataRow[info.Name];
                    info.SetValue(dbEntity, propertyValue, null);
                }
                catch
                {
                    if (propertyValue != null && propertyValue.GetType() == typeof(Guid))
                    {
                        try
                        {
                            info.SetValue(dbEntity, ((Guid)propertyValue).ToString(), null);
                        }
                        catch { }
                    }
                }
            }

            return dbEntity;
        }

        public List<T> Map(DataTable dataTable)
        {
            List<T> entities = new List<T>();

            using (DataTableReader reader = dataTable.CreateDataReader())
            {
                Object[] values = new object[reader.FieldCount];
                while (reader.Read())
                {
                    reader.GetValues(values);

                    T entity = Activator.CreateInstance<T>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        MapPrimative(values[i], reader.GetName(i), entity);
                    }
                    entities.Add(entity);
                }
            }

            return entities;
        }

        private void MapPrimative(Object value, String name, T entity)
        {
            PropertyInfo info = typeof(T).GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (info != null)
            {
                if (info.PropertyType == typeof(String))
                {
                    if (DBNull.Value == value)
                    {
                        info.SetValue(entity, null);
                    }
                    else
                    {
                        info.SetValue(entity, value.ToString());
                    }
                }
                if (info.PropertyType == typeof(Guid))
                {
                    if (DBNull.Value == value)
                    {
                        info.SetValue(entity, null);
                    }
                    else
                    {
                        info.SetValue(entity, (Guid)value);
                    }
                }
                if (info.PropertyType == typeof(int))
                {
                    if (DBNull.Value == value)
                    {
                        info.SetValue(entity, 0);
                    }
                    else
                    {
                        info.SetValue(entity, (int)value);
                    }
                }
                if (info.PropertyType == typeof(int?))
                {
                    if (DBNull.Value == value)
                    {
                        info.SetValue(entity, null);
                    }
                    else
                    {
                        info.SetValue(entity, (int)value);
                    }
                }
                if (info.PropertyType == typeof(DateTime))
                {
                    if (DBNull.Value == value)
                    {
                        info.SetValue(entity, null);
                    }
                    else
                    {
                        info.SetValue(entity, (DateTime)value);
                    }
                }
                if (info.PropertyType == typeof(DateTime?))
                {
                    if (DBNull.Value == value)
                    {
                        info.SetValue(entity, null);
                    }
                    else
                    {
                        info.SetValue(entity, (DateTime?)value);
                    }
                }
                if (info.PropertyType == typeof(Boolean))
                {
                    if (DBNull.Value == value)
                    {
                        info.SetValue(entity, false);
                    }
                    else
                    {
                        info.SetValue(entity, (Boolean)value);
                    }
                }
            }
        }
    }
}