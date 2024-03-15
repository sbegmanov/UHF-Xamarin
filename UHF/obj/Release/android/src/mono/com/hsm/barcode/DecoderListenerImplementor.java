package mono.com.hsm.barcode;


public class DecoderListenerImplementor
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		com.hsm.barcode.DecoderListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onKeepGoingCallback:()Z:GetOnKeepGoingCallbackHandler:Com.Hsm.Barcode.IDecoderListenerInvoker, DeviceAPI\n" +
			"n_onMultiReadCallback:()Z:GetOnMultiReadCallbackHandler:Com.Hsm.Barcode.IDecoderListenerInvoker, DeviceAPI\n" +
			"";
		mono.android.Runtime.register ("Com.Hsm.Barcode.IDecoderListenerImplementor, DeviceAPI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", DecoderListenerImplementor.class, __md_methods);
	}


	public DecoderListenerImplementor ()
	{
		super ();
		if (getClass () == DecoderListenerImplementor.class)
			mono.android.TypeManager.Activate ("Com.Hsm.Barcode.IDecoderListenerImplementor, DeviceAPI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public boolean onKeepGoingCallback ()
	{
		return n_onKeepGoingCallback ();
	}

	private native boolean n_onKeepGoingCallback ();


	public boolean onMultiReadCallback ()
	{
		return n_onMultiReadCallback ();
	}

	private native boolean n_onMultiReadCallback ();

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
