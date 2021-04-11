using System;
using System.Collections.Generic;
using System.Text;

namespace Cliente.API.Data.Base
{
    public static class Extensions
    {
        #region Data

        public static T As<T>(this object source)
        {
            return source == null || source == DBNull.Value
                ? default(T)
                : (T)source;
        }

        // Used to convert values going to the db
        public static object AsDbValue(this object source)
        {
            return source ?? DBNull.Value;
        }

        #endregion
    }
}
