using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UHF
{
    [Activity(Label = "Asset_check_fid")]
    public class Asset_check_fid : Activity
    {
        EditText txt_FID;
        Button btnCheckData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Asset_check_fid);

            txt_FID = (EditText)FindViewById(Resource.Id.txt_FID);
            btnCheckData = (Button)FindViewById(Resource.Id.btnCheck);

            btnCheckData.Click += delegate
            {
                btnCheck_Click();
            };
        }

        private void btnCheck_Click()
        {
            DataSet ds = new DataSet();
            string FID = txt_FID.Text;
           // var request = HttpWebRequest.Create(string.Format(@"http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/RoomAssetSearchByAssetFID?AssetFID={0}", FID));
 var request = HttpWebRequest.Create(string.Format(@"http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/SearchByFid?fid={0}", FID));

            request.ContentType = "application/json";
            request.Method = "GET";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // เข้า web service ไม่ได้
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                }
                else
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        //try
                        //{
                        //    var data = JsonConvert.DeserializeObject<List<RootItems>>(content);

                        //}
                        //catch (Exception ex)
                        //{
                        //    ex = ex;
                        //}

                        if (content.ToString().IndexOf("Asset found.") > 0)
                        {
                            string[] txt = content.ToString().Split('{');
                            txt = txt[2].ToString().Split(',');

                            string xmlData; //= txt[5].Substring(txt[5].IndexOf("<DataSet>")+ 9 ) ;
                            xmlData = txt[8].Substring(txt[8].IndexOf("<DataSet>"));
                            xmlData = xmlData.Substring(0, xmlData.Length - 1);
                            xmlData = xmlData.Replace("\\", "");

                            //  System.IO.StringReader xmlSR = new System.IO.StringReader(xmlData);
                            //   XmlTextReader xmlSR = new XmlTextReader(xmlData);
                            ds.ReadXml(new StringReader(xmlData), XmlReadMode.Auto);

                            MyGlobalVal.ds = ds;

                            var Asset_update_tid_other = new Intent(this, typeof(Asset_update_tid_other));
                            StartActivity(Asset_update_tid_other);
                        }
                        else
                        {
                            MyGlobalVal.ASSET_FID = txt_FID.Text;
                            MyGlobalVal.ds = new DataSet();
                            var Asset_new_record = new Intent(this, typeof(Asset_new_record));
                            StartActivity(Asset_new_record);
                        }
                    }
                }
            }
            //  App.Instance.GoToPage(new check_fid());

        }
    }
}