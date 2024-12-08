package crc64298b90ab1d20ae8a;


public class MyBootReceiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("APP.MerchPlus.MyBootReceiver, MerchPlus", MyBootReceiver.class, __md_methods);
	}


	public MyBootReceiver ()
	{
		super ();
		if (getClass () == MyBootReceiver.class) {
			mono.android.TypeManager.Activate ("APP.MerchPlus.MyBootReceiver, MerchPlus", "", this, new java.lang.Object[] {  });
		}
	}

	public MyBootReceiver (long p0, java.lang.String p1, android.app.DownloadManager p2, android.app.Activity p3)
	{
		super ();
		if (getClass () == MyBootReceiver.class) {
			mono.android.TypeManager.Activate ("APP.MerchPlus.MyBootReceiver, MerchPlus", "System.Int64, System.Private.CoreLib:System.String, System.Private.CoreLib:Android.App.DownloadManager, Mono.Android:Android.App.Activity, Mono.Android", this, new java.lang.Object[] { p0, p1, p2, p3 });
		}
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

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
