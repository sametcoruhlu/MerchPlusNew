package crc64f11ff80d67ca3ed7;


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
		mono.android.Runtime.register ("APP.MerchPlus.Adapters.JavaObjectWrapper`1, APP.MerchPlus.AndroidApp", JavaObjectWrapper_1.class, __md_methods);
	}


	public JavaObjectWrapper_1 ()
	{
		super ();
		if (getClass () == JavaObjectWrapper_1.class) {
			mono.android.TypeManager.Activate ("APP.MerchPlus.Adapters.JavaObjectWrapper`1, APP.MerchPlus.AndroidApp", "", this, new java.lang.Object[] {  });
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
