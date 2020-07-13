using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Data_Change
{
    /// <summary>
    /// Summary description for DataIntake
    /// </summary>
    public class DataIntake : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            Stream stream;
            string data;

            stream = context.Request.InputStream;

            int stream_length = (int)stream.Length;

            byte[] arr = new byte[stream_length];
            stream.Read(arr, 0, stream_length);


            data = "";
            for(int i=0; i<stream_length; i++)
            {
                data = data + (char)arr[i];
            }
            context.Response.Write(data);
            /*
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World</br>");
            context.Response.Write(context.Request.QueryString["data"]);
            string str = context.Request.ToString();
            context.Response.Write(context.Request.Form["Name"]);
            context.Response.Write(context.Request.Form["Father Name"]);
            */     
    }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}