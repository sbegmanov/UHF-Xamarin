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
    [Activity(Label = "Room_check_room_id")]
    public class Room_check_room_id : Activity
    {
        EditText txt_RoomID;
        Button btnCheckData;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Room_check_room_id);

            txt_RoomID = (EditText)FindViewById(Resource.Id.txt_RoomID);
            btnCheckData = (Button)FindViewById(Resource.Id.btnCheck);

            btnCheckData.Click += delegate
            {
                btnCheck_Click();
            };
        }

        private void btnCheck_Click()
        {
            string RoomID = txt_RoomID.Text;
           // var request = HttpWebRequest.Create(string.Format(@"http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/RoomAssetSearchByRoomCode?ROOM_CODE={0}", RoomID));
 var request = HttpWebRequest.Create(string.Format(@"http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/SearchByRoomCode?RoomCode={0}", RoomID));

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
                        DataSet ds = new DataSet();

                        if (content.ToString().IndexOf("Room found.") > 0)
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

                            var Room_update_room_tid_other = new Intent(this, typeof(Room_update_room_tid_other));
                            StartActivity(Room_update_room_tid_other);
                        }
                        else
                        {
                            ds = new DataSet();
                            MyGlobalVal.ROOM_CODE = txt_RoomID.Text;
                           var Room_new_record_room_tid_id = new Intent(this, typeof(Room_new_record_room_tid_id));
                            StartActivity(Room_new_record_room_tid_id);
                        }
                    }
                }
            }

        }
    }
}