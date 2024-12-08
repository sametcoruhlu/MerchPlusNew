package crc64298b90ab1d20ae8a;


public class DisplayVitrineCameraActivity
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("APP.MerchPlus.DisplayVitrineCameraActivity, MerchPlus", DisplayVitrineCameraActivity.class, __md_methods);
	}


	public DisplayVitrineCameraActivity ()
	{
		super ();
		if (getClass () == DisplayVitrineCameraActivity.class) {
			mono.android.TypeManager.Activate ("APP.MerchPlus.DisplayVitrineCameraActivity, MerchPlus", "", this, new java.lang.Object[] {  });
		}
	}

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
