package crc64871c473cf60c767b;


public class MainActivity_SplashScreenExitListener
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		androidx.core.splashscreen.SplashScreen.OnExitAnimationListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onSplashScreenExit:(Landroidx/core/splashscreen/SplashScreenViewProvider;)V:GetOnSplashScreenExit_Landroidx_core_splashscreen_SplashScreenViewProvider_Handler:AndroidX.Core.SplashScreen.SplashScreen/IOnExitAnimationListenerInvoker, Xamarin.AndroidX.Core.SplashScreen\n" +
			"";
		mono.android.Runtime.register ("MerchPlusMAUI.MainActivity+SplashScreenExitListener, MerchPlusMAUI", MainActivity_SplashScreenExitListener.class, __md_methods);
	}

	public MainActivity_SplashScreenExitListener ()
	{
		super ();
		if (getClass () == MainActivity_SplashScreenExitListener.class) {
			mono.android.TypeManager.Activate ("MerchPlusMAUI.MainActivity+SplashScreenExitListener, MerchPlusMAUI", "", this, new java.lang.Object[] {  });
		}
	}

	public void onSplashScreenExit (androidx.core.splashscreen.SplashScreenViewProvider p0)
	{
		n_onSplashScreenExit (p0);
	}

	private native void n_onSplashScreenExit (androidx.core.splashscreen.SplashScreenViewProvider p0);

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
