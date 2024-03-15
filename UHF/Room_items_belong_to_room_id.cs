using System;
using System.Collections;
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
    [Activity(Label = "Room_items_belong_to_room_id")]
    public class Room_items_belong_to_room_id : Activity
    {
        private GridView gv;
        ArrayList al;
        ArrayAdapter aa;
        TextView txt_RoomTID;
        TextView txt_Count;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Room_items_belong_to_room_id);

            gv = FindViewById<GridView>(Resource.Id.gv);
            txt_RoomTID = FindViewById<TextView>(Resource.Id.txt_RoomTID);
            txt_Count = FindViewById<TextView>(Resource.Id.txt_Count);
            getData();

            aa = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1, al);
            gv.Adapter = aa;
            // Create your application here
        }

        private void getData()
        {
            txt_RoomTID.Text = MyGlobalVal.ds.Tables[0].Rows[0]["ROOM_TID"].ToString();
            txt_Count.Text = "Count : " + MyGlobalVal.ds.Tables[0].Rows.Count.ToString();
            al = new ArrayList();
            al.Add("FID");
            al.Add("TID");
            al.Add("Asset Label");
            for (int i = 0; i <= MyGlobalVal.ds.Tables[0].Rows.Count - 1; i++)
            {
                al.Add(MyGlobalVal.ds.Tables[0].Rows[i]["ASSET_FID"]);
                al.Add(MyGlobalVal.ds.Tables[0].Rows[i]["ASSET_TID"]);
                al.Add(MyGlobalVal.ds.Tables[0].Rows[i]["ASSET_LABEL"]);
            }
        }
    }
}
