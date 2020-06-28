using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Abp.Domain.Entities;
using Nest;

namespace WorkFlowTaskSystem.Application.ES.Dto
{
    [ElasticsearchType( IdProperty = "toheaderid",RelationName = "finorderbookexpire")]
    public class FinOrderBookExpire
    {
        [Text(Name = "toheaderid", Index = true)]
        [MaxLength(18)]
        public string ToHeaderId { get; set; }
        /// <summary>
        /// 第三方订单号
        /// </summary>
        [MaxLength(128)]
        [Text(Name = "agentorderid", Index = true)]
        public string AgentOrderId { get; set; }

        /// <summary>
        /// 公园id
        /// </summary>
        [Text(Name = "parkid", Index = true)]
        public int? ParkId { get; set; }

        /// <summary>
        /// 公园名称
        /// </summary>
        [MaxLength(100)]
        [Text(Name = "parkname", Index = true)]
        public string ParkName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [MaxLength(100)]
        [Text(Name = "companyname", Index = true)]
        public string CompanyName { get; set; }

        /// <summary>
        /// 渠道类型
        /// </summary>
        [MaxLength(20)]
        [Text(Name = "channeltype", Index = true)]
        public string ChannelType { get; set; }

        /// <summary>
        /// 代理商id
        /// </summary>
        [Text(Name = "agencyid", Index = true)]
        public int? AgencyId { get; set; }

        /// <summary>
        /// 代理商名称
        /// </summary>
        [MaxLength(200)]
        [Text(Name = "agencyname", Index = true)]
        public string AgencyName { get; set; }

        /// <summary>
        /// 创建订单时间
        /// </summary>
        [Text(Name = "createordertime", Index = true)]
        public DateTime CreateOrderTime { get; set; }

        /// <summary>
        /// 预定入园开始时间
        /// </summary>
        [Text(Name = "preinparkstarttime", Index = true)]
        public DateTime? PreInParkStartTime { get; set; }


        /// <summary>
        /// 预定入园结束时间
        /// </summary>
        [Text(Name = "preinparkendtime", Index = true)]
        public DateTime? PreInParkEndTime { get; set; }

        /// <summary>
        /// 结算总金额
        /// </summary>
        [Text(Name = "settlementsumprice", Index = true)]
        public decimal? SettlementSumPrice { get; set; }

        /// <summary>
        /// 网售总金额
        /// </summary>
        [Text(Name = "salesumprice", Index = true)]
        public decimal? SaleSumPrice { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        [MaxLength(20)]
        [Text(Name = "idno", Index = true)]
        public string IdNo { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [MaxLength(20)]
        [Text(Name = "phonenumber", Index = true)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [Text(Name = "paymodeid", Index = true)]
        public int? PayModeId { get; set; }

        /// <summary>
        /// 支付方式名称
        /// </summary>
        [MaxLength(50)]
        [Text(Name = "paymodename", Index = true)]
        public string PayModeName { get; set; }

        /// <summary>
        /// 优惠类型
        /// </summary>
        [MaxLength(50)]
        [Text(Name = "toheaderid", Index = true)]
        public string DiscountType { get; set; }
        /// <summary>
        /// 优惠名称
        /// </summary>
        [MaxLength(50)]
        [Text(Name = "discountname", Index = true)]
        public string DiscountName { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        [Text(Name = "discountamount", Index = true)]
        public decimal? DiscountAmount { get; set; }

        /// <summary>
        /// 实际支付金额
        /// </summary>
        [Text(Name = "factpayamount", Index = true)]
        public decimal? FactPayAmount { get; set; }
        /// <summary>
        /// 核销日期
        /// </summary>
        [Text(Name = "consumedate", Index = true)]
        public DateTime? ConsumeDate { get; set; }
        /// <summary>
        /// 退款日期
        /// </summary>
        [Text(Name = "refunddate", Index = true)]
        public DateTime? RefundDate { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        [Text(Name = "expiredate", Index = true)]
        public DateTime? ExpireDate { get; set; }

        /// <summary>
        /// 退票手续费
        /// </summary>
        [Text(Name = "refundfee", Index = true)]
        public decimal? RefundFee { get; set; }
        /// <summary>
        /// 实际退款金额
        /// </summary>
        [Text(Name = "factrefundamount", Index = true)]
        public decimal? FactRefundAmount { get; set; }
        /// <summary>
        /// 结算金额
        /// </summary>
        [Text(Name = "settlementamount", Index = true)]
        public decimal? SettlementAmount { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [MaxLength(20)]
        [Text(Name = "statusname", Index = true)]
        public string StatusName { get; set; }
    }
}
