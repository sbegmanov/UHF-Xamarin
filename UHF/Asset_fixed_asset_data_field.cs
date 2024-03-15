using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace UHF
{
    [Activity(Label = "Asset_fixed_asset_data_field")]
    public class Asset_fixed_asset_data_field : Activity
    {

        TextView txt_RoomID;
        TextView txt_TID;
        TextView txt_FID;
        TextView txt_Asset_lbl;
        TextView txt_Description;
        Button btnUpdate;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Asset_fixed_asset_data_field);


                txt_RoomID = (TextView)FindViewById(Resource.Id.txt_RoomID);
                txt_TID = (TextView)FindViewById(Resource.Id.txt_TID);
                txt_FID = (TextView)FindViewById(Resource.Id.txt_FID);
                txt_Asset_lbl = (TextView)FindViewById(Resource.Id.txt_Asset_lbl);
                txt_Description = (TextView)FindViewById(Resource.Id.txt_Description);
            btnUpdate = (Button)FindViewById(Resource.Id.btnUpdate);
            btnUpdate.Click += delegate
            {
                btnUpdate_Click();
            };

            if (MyGlobalVal.ds.Tables[0].Rows.Count > 0)
            {
                txt_Description.Text = MyGlobalVal.ds.Tables[0].Rows[0]["ASSET_DESCRIPTION"].ToString();
                txt_Asset_lbl.Text = MyGlobalVal.ds.Tables[0].Rows[0]["ASSET_LABEL"].ToString();
                txt_FID.Text = MyGlobalVal.ds.Tables[0].Rows[0]["ASSET_FID"].ToString();
                txt_RoomID.Text = MyGlobalVal.ds.Tables[0].Rows[0]["ROOM_CODE"].ToString();
                txt_TID.Text = MyGlobalVal.ds.Tables[0].Rows[0]["ASSET_TID"].ToString();
            }
        }

        private void btnUpdate_Click()
        {
            var Asset_update_tid_other = new Intent(this, typeof(Asset_update_tid_other));
            StartActivity(Asset_update_tid_other);
        }
        }
}