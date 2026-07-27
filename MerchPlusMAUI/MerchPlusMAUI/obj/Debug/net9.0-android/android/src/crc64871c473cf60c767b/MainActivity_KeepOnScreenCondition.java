package crc64871c473cf60c767b;


public class MainActivity_KeepOnScreenCondition
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.core.splashscreen.SplashScreen.KeepOnScreenCondition
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_shouldKeepOnScreen:()Z:GetShouldKeepOnScreenHandler:AndroidX.Core.SplashScreen.SplashScreen/IKeepOnScreenConditionInvoker, Xamarin.AndroidX.Core.SplashScreen\n" +
			"";
		mono.android.Runtime.register ("MerchPlusMAUI.MainActivity+KeepOnScreenCondition, MerchPlusMAUI", MainActivity_KeepOnScreenCondition.class, __md_methods);
	}

	public MainActivity_KeepOnScreenCondition ()
	{
		super ();
		if (getClass () == MainActivity_KeepOnScreenCondition.class) {
			mono.android.TypeManager.Activate ("MerchPlusMAUI.MainActivity+KeepOnScreenCondition, MerchPlusMAUI", "", this, new java.lang.Object[] {  });
		}
	}

	public boolean shouldKeepOnScreen ()
	{
		return n_shouldKeepOnScreen ();
	}

	private native boolean n_shouldKeepOnScreen ();

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
