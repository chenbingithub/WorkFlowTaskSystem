using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KingRoad.EPM.InvestmeReport
{
    public class ParameterEntity
    {
        #region 公共属性

        protected string _reportCode;//反射 方法名
        protected string _CoverCode;//方法编号
        protected int _CoverHtdId;//标段id
        protected int _CoverSessionId;//计量清单id
        protected bool _CoverIsSign;//是否要电子签名
        protected bool _CoverMustReCreate;//计量期批复时水印
        protected int _projectId;//项目id
        /// <summary>
        /// 反射 方法名
        /// </summary>
        public string reportCode
        {
            get { return _reportCode; }
            set { this._reportCode = value; }
        }
        /// <summary>
        /// 方法编号
        /// </summary>
        public string Code
        {
            get { return _CoverCode; }
            set { this._CoverCode = value; }
        }
        /// <summary>
        /// 标段id
        /// </summary>
        public int htdId
        {
            get { return _CoverHtdId; }
            set { this._CoverHtdId = value; }
        }  /// <summary>
        /// 计量清单id
        /// </summary>
        public int sessionId
        {
            get { return _CoverSessionId; }
            set { this._CoverSessionId = value; }
        }
        /// <summary>
        /// 是否要电子签名
        /// </summary>
        public bool isSign
        {
            get { return _CoverIsSign; }
            set { this._CoverIsSign = value; }
        }
        /// <summary>
        /// 计量期批复时水印
        /// </summary>
        public bool mustReCreate
        {
            get { return _CoverMustReCreate; }
            set { this._CoverMustReCreate = value; }
        }
        /// <summary>
        /// 项目id
        /// </summary>
        public int projectId
        {
            get { return _projectId; }
            set { this._projectId = value; }
        }
        #endregion
    }
}
