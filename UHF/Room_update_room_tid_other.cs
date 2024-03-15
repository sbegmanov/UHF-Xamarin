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
    [Activity(Label = "Room_update_room_tid_other")]
    public class Room_update_room_tid_other : Activity
    {
        Button btnSave;
        EditText txt_RoomID;
        EditText txt_TID;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Room_update_room_tid_other);

            btnSave = (Button)FindViewById(Resource.Id.btnSave);
            txt_RoomID = (EditText)FindViewById(Resource.Id.txt_RoomID);
            txt_TID = (EditText)FindViewById(Resource.Id.txt_TID);

            txt_RoomID.Text = MyGlobalVal.ds.Tables[0].Rows[0]["ROOM_CODE"].ToString();
            txt_TID.Text = MyGlobalVal.ds.Tables[0].Rows[0]["ROOM_TID"].ToString();

            btnSave.Click += delegate
            {
                btnSave_Click();
            };

        }

        private void btnSave_Click()
        {
            string epc = MyGlobalVal.ds.Tables[0].Rows[0]["ROOM_EPC"].ToString();
            string asset_fid = MyGlobalVal.ds.Tables[0].Rows[0]["ASSET_FID"].ToString();
            string asset_tid = MyGlobalVal.ds.Tables[0].Rows[0]["ASSET_TID"].ToString();
            string asset_label = MyGlobalVal.ds.Tables[0].Rows[0]["ASSET_LABEL"].ToString(); 
            string asset_description = MyGlobalVal.ds.Tables[0].Rows[0]["ASSET_DESCRIPTION"].ToString(); 
            string room_description = MyGlobalVal.ds.Tables[0].Rows[0]["ROOM_DESCRIPTION"].ToString();
            //  var request = HttpWebRequest.Create("http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/RoomAsset_RoomSave?room_tid=" + txt_TID.Text + "&room_code=" + txt_RoomID.Text + "&room_description=" + room_description + "&asset_tid=" + asset_tid + "&asset_fid=" + asset_fid + "&asset_label=" + asset_label + "&asset_description=" + asset_description + "&epc=" + epc);
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

                        if (content.ToString().IndexOf("Room data updated successful.") > 0)
                        {
                            // var save_result = new Intent(this, typeof(save_result));
                            // StartActivity(save_result);
                            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                            AlertDialog alert = dialog.Create();
                            alert.SetTitle("Update");
                            alert.SetMessage("Data updated successfully.");
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
                            alert.SetTitle("Update");
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