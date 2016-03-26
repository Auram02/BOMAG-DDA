using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;


namespace DDA.DataObjects
{
    class ObjectArray
    {
        private Object[] _objectArray;
        public ObjectArray(Object[] objectArray)
        {
            this._objectArray = objectArray;
        }

        public DataSet ToDataSet()
        {
            DataSet ds = new DataSet();
            XmlSerializer xmlSerializer = new XmlSerializer(_objectArray.GetType());
            StringWriter writer = new StringWriter();
            xmlSerializer.Serialize(writer, _objectArray);
            StringReader reader = new StringReader(writer.ToString());
            ds.ReadXml(reader);
            return ds;
        }
    }
}
