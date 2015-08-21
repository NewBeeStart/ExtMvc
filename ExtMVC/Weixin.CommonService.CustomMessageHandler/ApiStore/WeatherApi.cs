using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace Weixin.CustomMessageHandler.ApiStore
{
    public class WeatherApi
    {


        public class WeatherInfo
        {
            public int errNum { get; set; }
            public string errMsg { get; set; }
            public retData retData { get; set; }
        }

        public class retData
        {
            public string city { get; set; }
            public string cityid { get; set; }
            public today today { get; set; }
            public dayinfo[] forecast { get; set; }
            public dayinfo[] history { get; set; }
        }

        public class today
        {
            public string date { get; set; }
            public string week { get; set; }
            public string curTemp { get; set; }
            public string aqi { get; set; }
            public string fengxiang { get; set; }
            public string fengli { get; set; }
            public string hightemp { get; set; }
            public string lowtemp { get; set; }
            public string type { get; set; }
            public info[] index { get; set; }
        }

        public class info
        {
            public string name { get; set; }
            public string code { get; set; }
            public string index { get; set; }
            public string details { get; set; }
            public string otherName { get; set; }
        }

        public class dayinfo
        {
            public string date { get; set; }
            public string week { get; set; }
            public string fengxiang { get; set; }
            public string fengli { get; set; }
            public string hightemp { get; set; }
            public string lowtemp { get; set; }
            public string type { get; set; }
        }

      


        public static string Apikey = "7db90e164b5cbe036a6f950d9ad73bf6";

        public static string GetCityWeather(string cityName)
        {
            string url = "http://apis.baidu.com/apistore/weatherservice/recentweathers";
            string param = "cityname="+cityName;
            var obj = Request(url, param);
            StringBuilder result = new StringBuilder();
            if (obj.errNum == 0 && obj.errMsg == "success")
            {
                result.Append(obj.retData.city).Append("\r\n");
                result.Append(obj.retData.today.date).Append("-").Append(obj.retData.today.type);
                result.Append(obj.retData.today.week).Append("\r\n");
                result.Append("当前温度:" + obj.retData.today.curTemp).Append("\r\n");
                result.Append("高温温度:" + obj.retData.today.hightemp).Append("\r\n");
                result.Append("低温温度:" + obj.retData.today.lowtemp).Append("\r\n");
                //for (int i = 0; i < obj.retData.today.index.Length; i++)
                //{
                //    result.Append(obj.retData.today.index[i].name).Append(":").Append(obj.retData.today.index[i].details).Append("\r\n");
                //}
                for (int i = 0; i < obj.retData.forecast.Length; i++)
                {
                    result.Append(obj.retData.forecast[i].date).Append("-").Append(obj.retData.forecast[i].week).Append("-").Append(obj.retData.forecast[i].type).Append("\r\n");
                    result.Append("高温温度:" + obj.retData.forecast[i].hightemp).Append("\r\n").Append("低温温度:" + obj.retData.forecast[i].lowtemp).Append("\r\n");
                    result.Append("风向:" + obj.retData.forecast[i].fengxiang).Append("\r\n").Append("风力:" + obj.retData.forecast[i].fengli).Append("\r\n");
                }
            }
            return result.ToString();
        }

        public static WeatherInfo Request(string url, string param)
        {
            string strURL = url + '?' + param;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", "7db90e164b5cbe036a6f950d9ad73bf6");
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject <WeatherInfo>(strValue);
            //return Newtonsoft.Json.JsonConvert.SerializeObject(o);
        }
    }
}
