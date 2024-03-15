using System;
using System.Collections.Generic;
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
    [Activity(Label = "Room_new_record_room_tid_id")]
    public class Room_new_record_room_tid_id : Activity
    {
        Button btnSave;
        EditText txt_RoomID;
        EditText txt_TID;
  

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Room_new_record_room_tid_id);

            btnSave = (Button)FindViewById(Resource.Id.btnSave);
            txt_RoomID = (EditText)FindViewById(Resource.Id.txt_RoomID);
            txt_TID = (EditText)FindViewById(Resource.Id.txt_TID);

            txt_RoomID.Text = MyGlobalVal.ROOM_CODE;
            txt_TID.Text = MyGlobalVal.ROOM_TID;

            btnSave.Click += delegate
            {
                btnSave_Click();
            };
        }

        private void btnSave_Click()
        {
            string epc = "";
            string asset_tid = "";
            string asset_fid ="";
            string asset_label = "";
            string asset_description ="";
            string room_description = "";
            // var request = HttpWebRequest.Create("http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/RoomAsset_RoomSave?room_tid=" + txt_TID.Text + "&room_code=" + txt_RoomID.Text + "&room_description=" + room_description + "&asset_tid=" + asset_tid + "&asset_fid=" + asset_fid + "&asset_label=" + asset_label + "&asset_description=" + asset_description + "&epc=" + epc);
            var request = HttpWebRequest.Create("http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/RoomSave?epc=" + epc + "&tid=" + txt_TID.Text + "&roomcode=" + txt_RoomID.Text + "&roomdescription=" + room_description);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // เข้า web service ไม่ได้
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Update");
                    alert.SetMessage("Data update failed. Check connection...");
                    alert.SetButton("OK", (c, ev) =>
                    {
                        // Ok button click task  
                    });
                    alert.Show();
                    Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                }
                else
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();

                        if (content.ToString().IndexOf("Insertion a new Room success.") > 0)
                        {
                            // var save_result = new Intent(this, typeof(save_result));
                            // StartActivity(save_result);
                            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                            AlertDialog alert = dialog.Create();
                            alert.SetTitle("Save");
                            alert.SetMessage("Data save successfully.");
                            alert.SetButton("OK", (c, ev) =>
                            {
                                // Ok button click task  
                            });
                            alert.Show();
                        }
                        else
                        {
                            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                            AlertDialog alert = dialog.Create();
                            alert.SetTitle("Save");
                            alert.SetMessage("Data update failed. Check connection...");
                            alert.SetButton("OK", (c, ev) =>
                            {
                                // Ok button click task  
                            });
                            alert.Show();
                        }
                    }
                }
            }

        }
    }
}