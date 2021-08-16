/*string ApiClientUrl = "http://lindegise.test.ik3cloud.com/K3cloud/";//cloud网址
        string DataCenter = "20201207141732";//数据中心ID。

        //string username = "周廷志";//做单的用户名
        string username = "周廷志";//做单的用户名
        string appId = "216901_Qf6u0ZsERuB86V1o6eWNy6XsTtwb7PNu";//第三方登陆用户名
        string pwd = "fb50e067b95e46ec81b6e46fc80dd187";//第三方登陆密码*/

using System;
using Kingdee.BOS.WebApi.Client;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Web.Http;
using System.Net;
using System.IO;

public class HelloWorld
{

    string ApiClientUrl = "http://lindegise.test.ik3cloud.com/K3cloud/";//cloud网址
    string DataCenter = "20201207141732";//数据中心ID。

    //string username = "周廷志";//做单的用户名
    string username = "周廷志";//做单的用户名
    string appId = "216901_Qf6u0ZsERuB86V1o6eWNy6XsTtwb7PNu";//第三方登陆用户名
    string pwd = "fb50e067b95e46ec81b6e46fc80dd187";



    public static void Main(string[] args)
    {

        Save();

        Console.WriteLine ("Hello Mono World");
    }
     public static bool Save()
        {
            bool bRet = false;
            try
            {
                K3CloudApiClient client = new K3CloudApiClient(ApiClientUrl);
                var ret = client.LoginByAppSecret(DataCenter, username, appId, pwd, 2052);
                bool result = JObject.Parse(ret)["IsSuccessByAPI"].Value<bool>();//校验结果

                if (!result)
                {
                   Console.WriteLine ("login issue: "+ ret);
                }
                //// 登陆成功
                if (result)
                {
                    //单据头字段
                    string FBillType = "GSTFKDLX01";//单据类型 日常报销GSTFKDLX01  差旅报销GSTFKDLX02  借款GSTFKDLX03  付款GSTFKDLX04
                    string FDate = "2018-12-26";//业务日期
                    string FContactUnitType = "BD_Empinfo";//往来单位类型  员工BD_Empinfo  供应商BD_Supplier
                    string FContactUnit = "0044";//往来单位编号
                    string FRectUnitType = "BD_Empinfo";//收款单位类型  员工BD_Empinfo  供应商BD_Supplier
                    string FRectUnit = "0044";//收款单位
                    string FCurrencyId = "PRE001";//币别（付款组织币别） 人民币PRE001  港元PRE002  美元PRE007
                    string FSettleCur = "PRE001";//结算币别 同上
                    string FPayOrgId = "204";//付款组织编码
                    string FSettleOrgId = "204";//结算组织编码
                    string FDeptment = "0002";//部门  财务部0002
                    string FCJZAmount = "1000";//冲借支 1000

                    //单据体字段
                    string FSettleType = "JSFS04_SYS";//结算类型  现金JSFS01_SYS  现金支票JSFS02_SYS  转账支票JSFS03_SYS   电汇JSFS04_SYS   信汇JSFS05_SYS   商业承兑汇票JSFS06_SYS  银行承兑汇票JSFS07_SYS  信用证JSFS08_SYS  应收票据背书JSFS09_SYS  内部利息结算JSFS010_SYS  集中结算JSFS021_SYS
                    string FPurposeId = "SFKYT0_SYS";//付款用途  收付款用途基础资料，具体见图
                    string FFPLX = "A";//发票类型  增值税普通发票A 增值税专用发票B 增值税电子发票C
                    string FGSTTaxRate = "16";//税率
                    string FGSTAmount = "3000";//不含税金额
                    string FGSTTax = "480";//税额
                    string FPayTotalAmountFor = "3480";//应付金额
                    string FAccountId = "120912342210902";//我方银行账号
                    string FBankTypeRec = "";//收款银行 
                    string FOppositeBankName = "招商银行";//对方开户行
                    string FOppositeBankAccount = "123456";//对方银行账号
                    string FOppositeccountName = "李四";//对方账户名称
                    string FCostId = "FYXM11_SYS";//费用项目 交通费FYXM11_SYS
                    string FYGHS = "001";//员工核算 测试员工
                    string FHSWD4 = "001.001";//核算维度4
                    string FGSFL = "02";//归属分类  门店02
                    string FXMHS = "10000";//项目核算  大客户项目10000
                    string FBMHS = "0001";//部门核算  总经办0001
                    string FFZHS = "101";//辅助核算


                    //付款单 ，NeedReturnFields里指定需要返回的字段，例子里返回单据编号、单据内码
                    string svchJson = "{\"Creator\":\"\",\"NeedUpDateFields\":[],\"NeedReturnFields\":[\"FBillNo\",\"FId\"]";
                    svchJson = svchJson + ",\"Model\":{\"FID\":\"0\"";//单据内码，新增单据默认为0，由系统自动取
                    svchJson = svchJson + ",\"FBillTypeID\":{\"FNumber\":\"" + FBillType + "\"}";
                    svchJson = svchJson + ",\"FDATE\":\"" + FDate + "\"";
                    svchJson = svchJson + ",\"FCONTACTUNITTYPE\":\"" + FContactUnitType + "\"";
                    svchJson = svchJson + ",\"FCONTACTUNIT\":{\"FNumber\":\"" + FContactUnit + "\"}";
                    svchJson = svchJson + ",\"FRECTUNITTYPE\":\"" + FRectUnitType + "\"";
                    svchJson = svchJson + ",\"FRECTUNIT\":{\"FNumber\":\"" + FRectUnit + "\"}";
                    svchJson = svchJson + ",\"FDepartment\":{\"FNumber\":\"" + FDeptment + "\"}";
                    svchJson = svchJson + ",\"FCURRENCYID\":{\"FNumber\":\"" + FCurrencyId + "\"}";
                    svchJson = svchJson + ",\"FSETTLEORGID\":{\"FNumber\":\"" + FSettleOrgId + "\"}";
                    svchJson = svchJson + ",\"FPAYORGID\":{\"FNumber\":\"" + FPayOrgId + "\"}";
                    svchJson = svchJson + ",\"FSETTLECUR\":{\"FNumber\":\"" + FSettleCur + "\"}";
                    svchJson = svchJson + ",\"FREMARK\":\"接口测试备注\"";
                    svchJson = svchJson + ",\"FCJZAmount\":\"" + FCJZAmount + "\"";

                    //单据体Json
                    svchJson = svchJson + ",\"FPAYBILLENTRY\":[";
                    //第一行单据体
                    svchJson = svchJson + "{\"FEntryID\":\"0\"";//新增单据,单据体内码为0
                    svchJson = svchJson + ",\"FSETTLETYPEID\":{\"FNumber\":\"" + FSettleType + "\"}";
                    svchJson = svchJson + ",\"FPURPOSEID\":{\"FNumber\":\"" + FPurposeId + "\"}";
                    svchJson = svchJson + ",\"FPAYTOTALAMOUNTFOR\":\"" + FPayTotalAmountFor + "\"";
                    svchJson = svchJson + ",\"FGSTTaxRate\":\"" + FGSTTaxRate + "\"";
                    svchJson = svchJson + ",\"FGSTAmount\":\"" + FGSTAmount + "\"";
                    svchJson = svchJson + ",\"FGSTTax\":\"" + FGSTTax + "\"";
                    svchJson = svchJson + ",\"FACCOUNTID\":{\"FNumber\":\"" + FAccountId + "\"}";
                    svchJson = svchJson + ",\"FOPPOSITEBANKACCOUNT\":\"" + FOppositeBankAccount + "\"";
                    svchJson = svchJson + ",\"FOPPOSITECCOUNTNAME\":\"" + FOppositeccountName + "\"";
                    svchJson = svchJson + ",\"FOPPOSITEBANKNAME\":\"" + FOppositeBankName + "\"";
                    svchJson = svchJson + ",\"FBankTypeRec\":{\"FName\":\"" + FBankTypeRec + "\"}";//未开启银企直连，暂时不需要
                    svchJson = svchJson + ",\"FFPLX\":\"" + FFPLX + "\"";
                    svchJson = svchJson + ",\"FCOSTID\":{\"FNumber\":\"" + FCostId + "\"}";
                    svchJson = svchJson + ",\"FYGHS\":{\"FNumber\":\"" + FYGHS + "\"}";
                    svchJson = svchJson + ",\"FHSWD4\":{\"FNumber\":\"" + FHSWD4 + "\"}";
                    svchJson = svchJson + ",\"FGSFL\":{\"FNumber\":\"" + FGSFL + "\"}";
                    svchJson = svchJson + ",\"FXMHS\":{\"FNumber\":\"" + FXMHS + "\"}";
                    svchJson = svchJson + ",\"FBMHS\":{\"FNumber\":\"" + FBMHS + "\"}";
                    svchJson = svchJson + ",\"FFZHS\":{\"FNumber\":\"" + FFZHS + "\"}";
                    svchJson = svchJson + "}";
                    //第二行单据体
                    svchJson = svchJson + ",{\"FEntryID\":\"0\"";//新增单据,单据体内码为0
                    svchJson = svchJson + ",\"FSETTLETYPEID\":{\"FNumber\":\"" + FSettleType + "\"}";
                    svchJson = svchJson + ",\"FPURPOSEID\":{\"FNumber\":\"" + FPurposeId + "\"}";
                    svchJson = svchJson + ",\"FPAYTOTALAMOUNTFOR\":\"" + FPayTotalAmountFor + "\"";
                    svchJson = svchJson + ",\"FGSTTaxRate\":\"" + FGSTTaxRate + "\"";
                    svchJson = svchJson + ",\"FGSTAmount\":\"" + FGSTAmount + "\"";
                    svchJson = svchJson + ",\"FGSTTax\":\"" + FGSTTax + "\"";
                    svchJson = svchJson + ",\"FACCOUNTID\":{\"FNumber\":\"" + FAccountId + "\"}";
                    svchJson = svchJson + ",\"FOPPOSITEBANKACCOUNT\":\"" + FOppositeBankAccount + "\"";
                    svchJson = svchJson + ",\"FOPPOSITECCOUNTNAME\":\"" + FOppositeccountName + "\"";
                    svchJson = svchJson + ",\"FOPPOSITEBANKNAME\":\"" + FOppositeBankName + "\"";
                    svchJson = svchJson + ",\"FBankTypeRec\":{\"FName\":\"" + FBankTypeRec + "\"}";
                    svchJson = svchJson + ",\"FFPLX\":\"" + FFPLX + "\"";
                    svchJson = svchJson + ",\"FCOSTID\":{\"FNumber\":\"" + FCostId + "\"}";
                    svchJson = svchJson + ",\"FYGHS\":{\"FNumber\":\"" + FYGHS + "\"}";
                    svchJson = svchJson + ",\"FHSWD4\":{\"FNumber\":\"" + FHSWD4 + "\"}";
                    svchJson = svchJson + ",\"FGSFL\":{\"FNumber\":\"" + FGSFL + "\"}";
                    svchJson = svchJson + ",\"FXMHS\":{\"FNumber\":\"" + FXMHS + "\"}";
                    svchJson = svchJson + ",\"FBMHS\":{\"FNumber\":\"" + FBMHS + "\"}";
                    svchJson = svchJson + ",\"FFZHS\":{\"FNumber\":\"" + FFZHS + "\"}";
                    svchJson = svchJson + "}";

                    svchJson = svchJson + "]}}";

                    //可以用界面的文本框直接输入json测试而已，demo不需要这截
                   
                    //调用api单据保存操作
                    var vchinfo = client.Save("", svchJson);
                    Console.WriteLine ( Convert.ToString(vchinfo));
                }
            }
            catch (Exception ex)
            {
                string sError = ex.Message;
                 Console.WriteLine (sError);
            }
            return bRet;
        }
}

