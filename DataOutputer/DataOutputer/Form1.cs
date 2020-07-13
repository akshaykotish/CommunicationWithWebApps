using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataOutputer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Socket_Address();
            //byte[] bytes = imageToByteArray(pictureBox1.Image);
            //PostRequest(bytes);
            //Web_Client(bytes);
            byte[] data = Encoding.ASCII.GetBytes("Testing");
            Web_Request(data);
        }

        void Socket_Address()
        {

            //Creates an IpEndPoint.
            IPAddress ipAddress = Dns.Resolve("http://localhost:50973/").AddressList[0];
            IPEndPoint ipLocalEndPoint = new IPEndPoint(ipAddress, 11000);

            //Serializes the IPEndPoint.
            SocketAddress socketAddress = ipLocalEndPoint.Serialize();

            //Verifies that ipLocalEndPoint is now serialized by printing its contents.
            Console.WriteLine("Contents of the socketAddress are: " + socketAddress.ToString());
            //Checks the Family property.
            Console.WriteLine("The address family of the socketAddress is: " + socketAddress.Family.ToString());
            //Checks the underlying buffer size.
            Console.WriteLine("The size of the underlying buffer is: " + socketAddress.Size.ToString());

        }

        void Web_Client(byte[] data)
        {
            DateTime start = DateTime.Now;
            Console.Write("\nPlease enter the URI to post data to : ");
            string uriString = "http://localhost:50973/DataIntake.ashx?Data=234235trhgtrh43t5";
            WebClient myWebClient = new WebClient();
           
            Console.WriteLine("\nPlease enter the data to be posted to the URI {0}:", uriString);
            //string postData = "lIFE IS HELL";
            //byte[] postArray = Encoding.ASCII.GetBytes(postData); 
            myWebClient.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            byte[] responseArray = myWebClient.UploadData(uriString, data);
            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);
            MessageBox.Show(ts.TotalSeconds.ToString());
        }

        void Web_Request(byte[] data)
        {
            DateTime start = DateTime.Now;
            WebRequest request = WebRequest.Create("http://localhost:50973/DataIntake.ashx?Data=234235trhgtrh43t5");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream = request.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();


            DateTime end = DateTime.Now;
            TimeSpan ts = (end - start);
            MessageBox.Show(ts.TotalSeconds.ToString());
            /*
            WebResponse response = request.GetResponse();
            stream = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream);
            MessageBox.Show(sr.ReadToEnd());
            sr.Close();
            stream.Close();
            */
        }

        async static void HTTP_Client()
        {
            //string vlu = Convert.ToBase64String(data);

            string url1 = "http://localhost:50973/DataIntake.ashx?Data=234235trhgtrh43t5";

            //int plen = vlu.Length / 4;
            //string p1 = vlu.Substring(0 * plen, plen);
            //string p2 = vlu.Substring(1 * plen, plen);
            //string p3 = vlu.Substring(2 * plen, plen);
            //string p4 = vlu.Substring(3 * plen, plen);

                IEnumerable<KeyValuePair<string, string>> queries = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("Name", "Shivam Kumar"),
                };

            HttpContent content = new FormUrlEncodedContent(queries);

            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response1 = await client.PostAsync(url1, content))
                {
                    using (HttpContent content1 = response1.Content)
                    {
                        string mycontent = await content1.ReadAsStringAsync();
                        HttpContentHeaders headers = content1.Headers;
                        Console.WriteLine(headers);
                        MessageBox.Show(mycontent);
                    }
                }
            }
            
        }

        public byte[] imageToByteArray(System.Drawing.Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Gif);
            return ms.ToArray();
        }

        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            MemoryStream ms = new MemoryStream(byteArrayIn);
            Image returnImage = Image.FromStream(ms);
            return returnImage;
        }
    }
}
