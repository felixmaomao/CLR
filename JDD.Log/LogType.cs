/******************************************************************
 * Description   : 日志类型基础类
 * CreateDate    : 
 * Creater       : 
 * LastChangeDate: 2015-10-27 14:45
 * LastChanger   : 焦杰
 * ******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JDD.Log
{
    public class LogType
    {
        /// <summary>
        /// 系统
        /// </summary>
        public static readonly string Sys = "Sys";

        /// <summary>
        /// 用户：所有用户模块的问题
        /// </summary>
        public static readonly string User = "User";

        /// <summary>
        /// 关于
        /// </summary>
        public static readonly string AboutUs = "AboutUs";

        /// <summary>
        /// 提款
        /// </summary>
        public static readonly string Distill = "Distill";

        /// <summary>
        /// 过滤
        /// </summary>
        public static readonly string Filter = "Filter";

        /// <summary>
        /// 跟单
        /// </summary>
        public static readonly string Follow = "Follow";

        /// <summary>
        /// 密码找回
        /// </summary>
        public static readonly string GetPwd = "GetPwd";

        /// <summary>
        /// 活动
        /// </summary>
        public static readonly string HD = "HD";

        /// <summary>
        /// 合买
        /// </summary>
        public static readonly string JoinBuy = "JoinBuy";

        /// <summary>
        /// 登陆
        /// </summary>
        public static readonly string Login = "Login";

        /// <summary>
        /// 投注
        /// </summary>
        public static readonly string Lott = "Lott";

        /// <summary>
        /// 购彩大厅
        /// </summary>
        public static readonly string LottHall = "LottHall";

        /// <summary>
        /// 彩票盒子
        /// </summary>
        public static readonly string LottBox = "LottBox";

        /// <summary>
        /// 主站
        /// </summary>
        public static readonly string Main = "Main";

        /// <summary>
        /// 开奖详情
        /// </summary>
        public static readonly string OpenAward = "OpenAward";

        /// <summary>
        /// 奖金优化
        /// </summary>
        public static readonly string Optimize = "Optimize";

        /// <summary>
        /// 包月套餐
        /// </summary>
        public static readonly string Package = "Package";

        /// <summary>
        /// 过关
        /// </summary>
        public static readonly string Pass = "Pass";

        /// <summary>
        /// 支付
        /// </summary>
        public static readonly string Pay = "Pay";

        /// <summary>
        /// 交易
        /// </summary>
        public static readonly string Trade = "Trade";

        /// <summary>
        /// 
        /// </summary>
        public static readonly string PreScheme = "PreScheme";

        /// <summary>
        /// 公共部分
        /// </summary>
        public static readonly string Public = "Public";

        /// <summary>
        /// 即买即付
        /// </summary>
        public static readonly string QuickBuy = "QuickBuy";

        /// <summary>
        /// 注册
        /// </summary>
        public static readonly string Register = "Register";

        /// <summary>
        /// 资源
        /// </summary>
        public static readonly string Resource = "Resource";

        /// <summary>
        /// 方案详情
        /// </summary>
        public static readonly string SchemeDetail = "SchemeDetail";

        /// <summary>
        /// 战绩
        /// </summary>
        public static readonly string Score = "Score";

        /// <summary>
        /// 主题活动
        /// </summary>
        public static readonly string Theme = "Theme";

        /// <summary>
        /// 推广联盟
        /// </summary>
        public static readonly string Union = "Union";

        /// <summary>
        /// 资讯
        /// </summary>
        public static readonly string ZX = "ZX";

        /// <summary>
        /// 百家赔率
        /// </summary>
        public static readonly string Odds = "Odds";

        /// <summary>
        /// 直播
        /// </summary>
        public static readonly string Live = "Live";

        /// <summary>
        /// 帮助中心
        /// </summary>
        public static readonly string Help = "Help";

        /// <summary>
        /// 资料库
        /// </summary>
        public static readonly string Info = "Info";

        /// <summary>
        /// Infoc
        /// </summary>
        public static readonly string Infoc = "Infoc";

        /// <summary>
        /// 走势图
        /// </summary>
        public static readonly string Chart = "Chart";

        /// <summary>
        /// 论坛
        /// </summary>
        public static readonly string BBS = "BBS";

        /// <summary>
        /// 日志
        /// </summary>
        public static readonly string Log = "Log";

        /// <summary>
        /// 缓存
        /// </summary>
        public static readonly string Cache = "Cache";

        /// <summary>
        /// 基础数据
        /// </summary>
        public static readonly string Data = "Data";

        /// <summary>
        /// 数据库
        /// </summary>
        public static readonly string DataBase = "DataBase";

        /// <summary>
        /// Request请求
        /// </summary>
        public static readonly string Request = "Request";

        /// <summary>
        /// 系统错误
        /// </summary>
        public static readonly string Error = "Error";

        /// <summary>
        /// 公共组件库
        /// </summary>
        public static readonly string Common = "Common";
        /// <summary>
        /// 其他
        /// </summary>
        public static readonly string Other = "Other";

        /// <summary>
        /// Redis
        /// </summary>
        public static readonly string Redis = "Redis";

        /// <summary>
        /// Memcached
        /// </summary>
        public static readonly string Memcache = "Memcache";

        /// <summary>
        /// 手机接口client.jiangduoduo.com
        /// </summary>
        public static readonly string Client = "Client";

        /// <summary>
        /// 红包
        /// </summary>
        public static readonly string HongBao = "HongBao";

        /// <summary>
        /// 过关服务
        /// </summary>
        public static readonly string TaskPass = "TaskPass";

        /// <summary>
        /// 提款服务
        /// </summary>
        public static readonly string TaskDistill = "TaskDistill";

        /// <summary>
        /// 出票
        /// </summary>
        public static readonly string Print = "Print";


        /// <summary>
        /// RedisJob
        /// </summary>
        public static readonly string RedisJob = "RedisJob";

        /// <summary>
        /// RedisSite
        /// </summary>
        public static readonly string RedisSite = "RedisSite";

        /// <summary>
        /// RedisOrder
        /// </summary>
        public static readonly string RedisOrder = "RedisOrder";

        /// <summary>
        /// RedisTrade
        /// </summary>
        public static readonly string RedisTrade = "RedisTrade";

        /// <summary>
        /// RedisUser
        /// </summary>
        public static readonly string RedisUser = "RedisUser";

        /// <summary>
        /// RedisHongbao
        /// </summary>
        public static readonly string RedisHongbao = "RedisHongbao";

        /// <summary>
        /// RedisClient
        /// </summary>
        public static readonly string RedisClient = "RedisClient";

        /// <summary>
        /// SchemeWCF
        /// </summary>
        public static readonly string SchemeWCF = "SchemeWCF";

        /// <summary>
        /// ChaseWCF
        /// </summary>
        public static readonly string ChaseWCF = "ChaseWCF";

        /// <summary>
        /// PackageWCF
        /// </summary>
        public static readonly string PackageWCF = "PackageWCF";

        /// <summary>
        /// ASPX
        /// </summary>
        public static readonly string ASPX = "ASPX";

        /// <summary>
        /// 擂台(推荐擂台)
        /// </summary>
        public static readonly string Arena = "Arena";

        /// <summary>
        /// 一元夺宝
        /// </summary>
        public static readonly string DuoBao = "DuoBao";


        /// <summary>
        /// 一元购认购
        /// </summary>
        public static readonly string YYGOrder = "YYGOrder";

        /// <summary>
        /// Site
        /// </summary>
        public static readonly string Site = "Site";
        /// <summary>
        /// Product
        /// </summary>
        public static readonly string Product = "Product";
        /// <summary>
        /// 图片
        /// </summary>
        public static readonly string File = "File";
        /// <summary>
        /// 一元猎宝
        /// </summary>
        public static readonly string YYLB = "YYLB";
    }
}
