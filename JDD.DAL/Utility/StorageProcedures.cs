/******************************************************************
 * Description   : 存储过程封装操作类
 * CreateDate    : 2012-7-3
 * Creater       : 李书喜
 * LastChangeDate: 2015-10-26 13：50
 * LastChanger   : 焦杰
 * ******************************************************************/

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JDD.DAL.Utility
{
    public class StorageProcedures
    {
        /// <summary>
        /// 存储过程名称
        /// </summary>
        private readonly string _name;
        public string Name
        {
            get { return _name; }
        }
        /// <summary>
        /// 存储过程编码
        /// </summary>
        private readonly string _code;
        public string Code
        {
            get { return _code; }
        }
        /// <summary>
        /// 参数列表
        /// </summary>
        private List<Parameters> _params;
        public List<Parameters> Params
        {
            get { return _params; }
            set { _params = value; }
        }

        /// <summary>
        /// 参数赋值
        /// </summary>
        /// <param name="proceCode">存储Code</param>
        /// <param name="proceName">存储名称</param>
        public StorageProcedures(string proceCode, string proceName)
        {
            _code = proceCode;
            _name = proceName;
            _params = new List<Parameters>();
        }
        
        /// <summary>
        /// 增加参数
        /// </summary>
        /// <param name="param">参数</param>
        public void AddParameter(Parameters param)
        {
            _params.Add(param);
        }

        /// <summary>
        /// 获取参数Code
        /// </summary>
        /// <returns></returns>
        public string[] GetParamCodes()
        {
            string[] obj = new string[this._params.FindAll(ii => ii.Direction == ParameterDirection.Input).Count];

            for (int ii = 0; ii < obj.Length; ii++)
            {
                obj[ii] = this._params.FindAll(param => param.Direction == ParameterDirection.Input)[ii].ParamCode;
            }

            return obj;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <returns></returns>
        public SqlParameter[] GetParameters()
        {
            SqlParameter[] cmdParams = SQLHelper.GetCachedParameters(this._code);
            if (cmdParams == null)
            {
                cmdParams = new SqlParameter[this._params.Count];
                for (int ii = 0; ii < this._params.Count; ii++)
                {
                    Parameters param = this._params[ii];
                    SqlParameter cmdparam = new SqlParameter(param.Name, param.SqlType, param.Size);
                    cmdparam.Direction = param.Direction;
                    cmdParams[ii] = cmdparam;
                }
                SQLHelper.CacheParameters(this._code, cmdParams);
            }
            return cmdParams;
        }    
    }
}
