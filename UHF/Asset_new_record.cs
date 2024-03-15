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
    [Activity(Label = "Asset_new_record")]
    public class Asset_new_record : Activity
    {
        Button btnSave;
        EditText txt_RoomID;
        EditText txt_TID;
        EditText txt_FID;
        EditText txt_Asset_lbl;
        EditText txt_Description;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Asset_new_record);

            btnSave = (Button)FindViewById(Resource.Id.btnSave);
            txt_RoomID = (EditText)FindViewById(Resource.Id.txt_RoomID);
            txt_TID = (EditText)FindViewById(Resource.Id.txt_TID);
            txt_FID = (EditText)FindViewById(Resource.Id.txt_FID);
            txt_Asset_lbl = (EditText)FindViewById(Resource.Id.txt_Asset_lbl);
            txt_Description = (EditText)FindViewById(Resource.Id.txt_Description);

            txt_Description.Text = MyGlobalVal.ASSET_DESCRIPTION;
            txt_Asset_lbl.Text = MyGlobalVal.ASSET_LABEL;
            txt_FID.Text = MyGlobalVal.ASSET_FID;
            txt_RoomID.Text = MyGlobalVal.ROOM_CODE;
            txt_TID.Text = MyGlobalVal.ASSET_TID;

            btnSave.Click += delegate
            {
                btnSave_Click();
            };
            // Create your application here
        }

        private void btnSave_Click()
        {
            string epc = "";
            string asset_type = "";
            string room_tid = "";
            // var request = HttpWebRequest.Create("http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/RoomAsset_AssetSave?room_tid=" + room_tid  + "&room_code=" + txt_RoomID.Text + "&room_description=" + room_description + "&asset_tid="+ txt_TID.Text +"&asset_fid="+ txt_FID.Text +"&asset_label="+ txt_Asset_lbl.Text +"&asset_description="+ txt_Description.Text +"&epc="+ epc);
            var request = HttpWebRequest.Create("http://" + Resources.GetString(Resource.String.SW_Server) + "/webapi.svc/Save?roomcode=" + txt_RoomID.Text + "&epc=" + epc + "&tid=" + txt_TID.Text + "&fid=" + txt_FID.Text + "&assetlabel=" + txt_Asset_lbl.Text + "&assettype=" + asset_type + "&assetdescription=" + txt_Description.Text);

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

                        if (content.ToString().IndexOf("Insertion a new asset success.") > 0)
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