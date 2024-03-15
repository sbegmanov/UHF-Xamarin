package md528627aaeb6f9117298e91b1bbc0f3dd1;


public class Asset_fixed_asset_data_field
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("UHF.Asset_fixed_asset_data_field, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", Asset_fixed_asset_data_field.class, __md_methods);
	}


	public Asset_fixed_asset_data_field ()
	{
		super ();
		if (getClass () == Asset_fixed_asset_data_field.class)
			mono.android.TypeManager.Activate ("UHF.Asset_fixed_asset_data_field, UHF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
