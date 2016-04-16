/******************************************************************
 * Description   : 参数类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-26 12：00
 * LastChanger   : 焦杰
 * ******************************************************************/
using System;
using System.Data;

namespace JDD.DAL.Utility
{
    public class Parameters
    {
        private string _name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private SqlDbType _sqlType;
        /// <summary>
        /// sql类型
        /// </summary>
        public SqlDbType SqlType
        {
            get { return _sqlType; }
            set { _sqlType = value; }
        }

        private int _size;
        /// <summary>
        /// 大小
        /// </summary>
        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        readonly ParameterDirection _direction;
        /// <summary>
        /// 参数方向
        /// </summary>
        public ParameterDirection Direction
        {
            get
            {
                return _direction;
            }
        }

        private readonly string _paramCode;
        /// <summary>
        /// 参数Code
        /// </summary>
        public string ParamCode
        {
            get
            {
                return _paramCode;
            }
        }

        private object _paramValue;
        public object ParamValue
        {
            get { return _paramValue; }
            set { _paramValue = value; }
        }

        /// <summary>
        /// 参数函数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <param name="paramType">参数类型</param>
        /// <param name="paramSize">大小</param>
        /// <param name="direction">方向</param>
        /// <param name="paramCode">参数Code</param>
        public Parameters(string paramName, string paramType, int paramSize, int direction, string paramCode)
        {
            _name = paramName;
            _sqlType = SqlTypeString2SqlType(paramType);
            _size = paramSize;
            _direction = (ParameterDirection)direction;
            _paramCode = paramCode;

            if (_direction == ParameterDirection.Output)
            {
                switch (_sqlType)
                {
                    case SqlDbType.Int:
                        _paramValue = 0;
                        break;
                    case SqlDbType.VarChar:
                        _paramValue = string.Empty;
                        break;
                }
            }
        }

        #region c# 和sqlDBType转换
        /// <summary>
        /// 字符串转换为SqlDbType类型
        /// </summary>
        /// <param name="sqlTypeString">sql类型</param>
        /// <returns></returns>
        private SqlDbType SqlTypeString2SqlType(string sqlTypeString)
        {
            SqlDbType dbType = SqlDbType.Variant;//默认为Object
            sqlTypeString = sqlTypeString.Trim().ToLower();
            switch (sqlTypeString)
            {
                case "int":
                    dbType = SqlDbType.Int;
                    break;
                case "varchar":
                    dbType = SqlDbType.VarChar;
                    break;
                case "bit":
                    dbType = SqlDbType.Bit;
                    break;
                case "datetime":
                    dbType = SqlDbType.DateTime;
                    break;
                case "decimal":
                    dbType = SqlDbType.Decimal;
                    break;
                case "float":
                    dbType = SqlDbType.Float;
                    break;
                case "image":
                    dbType = SqlDbType.Image;
                    break;
                case "money":
                    dbType = SqlDbType.Money;
                    break;
                case "ntext":
                    dbType = SqlDbType.NText;
                    break;
                case "nvarchar":
                    dbType = SqlDbType.NVarChar;
                    break;
                case "smalldatetime":
                    dbType = SqlDbType.SmallDateTime;
                    break;
                case "smallint":
                    dbType = SqlDbType.SmallInt;
                    break;
                case "text":
                    dbType = SqlDbType.Text;
                    break;
                case "bigint":
                    dbType = SqlDbType.BigInt;
                    break;
                case "binary":
                    dbType = SqlDbType.Binary;
                    break;
                case "char":
                    dbType = SqlDbType.Char;
                    break;
                case "nchar":
                    dbType = SqlDbType.NChar;
                    break;
                case "numeric":
                    dbType = SqlDbType.Decimal;
                    break;
                case "real":
                    dbType = SqlDbType.Real;
                    break;
                case "smallmoney":
                    dbType = SqlDbType.SmallMoney;
                    break;
                case "sql_variant":
                    dbType = SqlDbType.Variant;
                    break;
                case "timestamp":
                    dbType = SqlDbType.Timestamp;
                    break;
                case "tinyint":
                    dbType = SqlDbType.TinyInt;
                    break;
                case "uniqueidentifier":
                    dbType = SqlDbType.UniqueIdentifier;
                    break;
                case "varbinary":
                    dbType = SqlDbType.VarBinary;
                    break;
                case "xml":
                    dbType = SqlDbType.Xml;
                    break;
            }
            return dbType;
        }
        
        /// <summary>
        ///  SqlDbType转换为C#数据类型
        /// </summary>
        /// <returns></returns>
        private Type SqlType2CsharpType()
        {
            switch (_sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(Int64);
                case SqlDbType.Binary:
                    return typeof(Object);
                case SqlDbType.Bit:
                    return typeof(Boolean);
                case SqlDbType.Char:
                    return typeof(String);
                case SqlDbType.DateTime:
                    return typeof(DateTime);
                case SqlDbType.Decimal:
                    return typeof(Decimal);
                case SqlDbType.Float:
                    return typeof(Double);
                case SqlDbType.Image:
                    return typeof(Object);
                case SqlDbType.Int:
                    return typeof(Int32);
                case SqlDbType.Money:
                    return typeof(Decimal);
                case SqlDbType.NChar:
                    return typeof(String);
                case SqlDbType.NText:
                    return typeof(String);
                case SqlDbType.NVarChar:
                    return typeof(String);
                case SqlDbType.Real:
                    return typeof(Single);
                case SqlDbType.SmallDateTime:
                    return typeof(DateTime);
                case SqlDbType.SmallInt:
                    return typeof(Int16);
                case SqlDbType.SmallMoney:
                    return typeof(Decimal);
                case SqlDbType.Text:
                    return typeof(String);
                case SqlDbType.Timestamp:
                    return typeof(Object);
                case SqlDbType.TinyInt:
                    return typeof(Byte);
                case SqlDbType.Udt://自定义的数据类型
                    return typeof(Object);
                case SqlDbType.UniqueIdentifier:
                    return typeof(Object);
                case SqlDbType.VarBinary:
                    return typeof(Object);
                case SqlDbType.VarChar:
                    return typeof(String);
                case SqlDbType.Variant:
                    return typeof(Object);
                case SqlDbType.Xml:
                    return typeof(Object);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 检查参数类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckParamType(object value)
        {
            try
            {
                Convert.ChangeType(value, SqlType2CsharpType());
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion
    }
}
