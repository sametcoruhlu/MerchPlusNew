package crc646c316a5726d9f3c1;


public class JavaObjectWrapper_1
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("APP.MerchPlus.Adapters.JavaObjectWrapper`1, MerchPlus", JavaObjectWrapper_1.class, __md_methods);
	}


	public JavaObjectWrapper_1 ()
	{
		super ();
		if (getClass () == JavaObjectWrapper_1.class) {
			mono.android.TypeManager.Activate ("APP.MerchPlus.Adapters.JavaObjectWrapper`1, MerchPlus", "", this, new java.lang.Object[] {  });
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
