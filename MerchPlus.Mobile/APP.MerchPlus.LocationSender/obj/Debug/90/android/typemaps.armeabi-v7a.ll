; ModuleID = 'obj\Debug\90\android\typemaps.armeabi-v7a.ll'
source_filename = "obj\Debug\90\android\typemaps.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


%struct.TypeMapEntry = type {
	i8*,; char* from
	i8*; char* to
}

%struct.TypeMap = type {
	i32,; uint32_t entry_count
	i8*,; char** assembly_name
	i8*,; uint8_t* data
	%struct.TypeMapEntry*,; TypeMapEntry* java_to_managed
	%struct.TypeMapEntry*; TypeMapEntry* managed_to_java
}
@__TypeMapEntry_from.0 = internal constant [72 x i8] c"APP.MerchPlus.LocationSender.MainActivity, APP.MerchPlus.LocationSender\00", align 1
@__TypeMapEntry_to.1 = internal constant [35 x i8] c"crc64e2e365b8ee8526bd/MainActivity\00", align 1
@__TypeMapEntry_from.2 = internal constant [35 x i8] c"Android.App.Activity, Mono.Android\00", align 1
@__TypeMapEntry_to.3 = internal constant [21 x i8] c"android/app/Activity\00", align 1
@__TypeMapEntry_from.4 = internal constant [38 x i8] c"Android.App.Application, Mono.Android\00", align 1
@__TypeMapEntry_to.5 = internal constant [24 x i8] c"android/app/Application\00", align 1
@__TypeMapEntry_from.6 = internal constant [44 x i8] c"Android.Content.ComponentName, Mono.Android\00", align 1
@__TypeMapEntry_to.7 = internal constant [30 x i8] c"android/content/ComponentName\00", align 1
@__TypeMapEntry_from.8 = internal constant [38 x i8] c"Android.Content.Context, Mono.Android\00", align 1
@__TypeMapEntry_to.9 = internal constant [24 x i8] c"android/content/Context\00", align 1
@__TypeMapEntry_from.10 = internal constant [45 x i8] c"Android.Content.ContextInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.11 = internal constant [45 x i8] c"Android.Content.ContextWrapper, Mono.Android\00", align 1
@__TypeMapEntry_to.12 = internal constant [31 x i8] c"android/content/ContextWrapper\00", align 1
@__TypeMapEntry_from.13 = internal constant [50 x i8] c"Android.Content.IComponentCallbacks, Mono.Android\00", align 1
@__TypeMapEntry_to.14 = internal constant [35 x i8] c"android/content/ComponentCallbacks\00", align 1
@__TypeMapEntry_from.15 = internal constant [51 x i8] c"Android.Content.IComponentCallbacks2, Mono.Android\00", align 1
@__TypeMapEntry_to.16 = internal constant [36 x i8] c"android/content/ComponentCallbacks2\00", align 1
@__TypeMapEntry_from.17 = internal constant [58 x i8] c"Android.Content.IComponentCallbacks2Invoker, Mono.Android\00", align 1
@__TypeMapEntry_from.18 = internal constant [57 x i8] c"Android.Content.IComponentCallbacksInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.19 = internal constant [37 x i8] c"Android.Content.Intent, Mono.Android\00", align 1
@__TypeMapEntry_to.20 = internal constant [23 x i8] c"android/content/Intent\00", align 1
@__TypeMapEntry_from.21 = internal constant [48 x i8] c"Android.Content.Res.Configuration, Mono.Android\00", align 1
@__TypeMapEntry_to.22 = internal constant [34 x i8] c"android/content/res/Configuration\00", align 1
@__TypeMapEntry_from.23 = internal constant [89 x i8] c"Android.Gms.Extensions.AwaitableTaskCompleteListener`1, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.24 = internal constant [54 x i8] c"crc6495d4f5d63cc5c882/AwaitableTaskCompleteListener_1\00", align 1
@__TypeMapEntry_from.25 = internal constant [70 x i8] c"Android.Gms.Tasks.CancellationToken, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.26 = internal constant [47 x i8] c"com/google/android/gms/tasks/CancellationToken\00", align 1
@__TypeMapEntry_from.27 = internal constant [77 x i8] c"Android.Gms.Tasks.CancellationTokenInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.28 = internal constant [66 x i8] c"Android.Gms.Tasks.IContinuation, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.29 = internal constant [42 x i8] c"com/google/android/gms/tasks/Continuation\00", align 1
@__TypeMapEntry_from.30 = internal constant [73 x i8] c"Android.Gms.Tasks.IContinuationInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.31 = internal constant [72 x i8] c"Android.Gms.Tasks.IOnCanceledListener, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.32 = internal constant [48 x i8] c"com/google/android/gms/tasks/OnCanceledListener\00", align 1
@__TypeMapEntry_from.33 = internal constant [79 x i8] c"Android.Gms.Tasks.IOnCanceledListenerInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.34 = internal constant [72 x i8] c"Android.Gms.Tasks.IOnCompleteListener, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.35 = internal constant [48 x i8] c"com/google/android/gms/tasks/OnCompleteListener\00", align 1
@__TypeMapEntry_from.36 = internal constant [79 x i8] c"Android.Gms.Tasks.IOnCompleteListenerInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.37 = internal constant [71 x i8] c"Android.Gms.Tasks.IOnFailureListener, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.38 = internal constant [47 x i8] c"com/google/android/gms/tasks/OnFailureListener\00", align 1
@__TypeMapEntry_from.39 = internal constant [78 x i8] c"Android.Gms.Tasks.IOnFailureListenerInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.40 = internal constant [71 x i8] c"Android.Gms.Tasks.IOnSuccessListener, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.41 = internal constant [47 x i8] c"com/google/android/gms/tasks/OnSuccessListener\00", align 1
@__TypeMapEntry_from.42 = internal constant [78 x i8] c"Android.Gms.Tasks.IOnSuccessListenerInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.43 = internal constant [77 x i8] c"Android.Gms.Tasks.IOnTokenCanceledListener, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.44 = internal constant [53 x i8] c"com/google/android/gms/tasks/OnTokenCanceledListener\00", align 1
@__TypeMapEntry_from.45 = internal constant [84 x i8] c"Android.Gms.Tasks.IOnTokenCanceledListenerInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.46 = internal constant [73 x i8] c"Android.Gms.Tasks.ISuccessContinuation, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.47 = internal constant [49 x i8] c"com/google/android/gms/tasks/SuccessContinuation\00", align 1
@__TypeMapEntry_from.48 = internal constant [80 x i8] c"Android.Gms.Tasks.ISuccessContinuationInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.49 = internal constant [57 x i8] c"Android.Gms.Tasks.Task, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.50 = internal constant [34 x i8] c"com/google/android/gms/tasks/Task\00", align 1
@__TypeMapEntry_from.51 = internal constant [73 x i8] c"Android.Gms.Tasks.TaskCompletionSource, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_to.52 = internal constant [50 x i8] c"com/google/android/gms/tasks/TaskCompletionSource\00", align 1
@__TypeMapEntry_from.53 = internal constant [64 x i8] c"Android.Gms.Tasks.TaskInvoker, Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapEntry_from.54 = internal constant [60 x i8] c"Android.Graphics.Drawables.Drawable+ICallback, Mono.Android\00", align 1
@__TypeMapEntry_to.55 = internal constant [44 x i8] c"android/graphics/drawable/Drawable$Callback\00", align 1
@__TypeMapEntry_from.56 = internal constant [67 x i8] c"Android.Graphics.Drawables.Drawable+ICallbackInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.57 = internal constant [50 x i8] c"Android.Graphics.Drawables.Drawable, Mono.Android\00", align 1
@__TypeMapEntry_to.58 = internal constant [35 x i8] c"android/graphics/drawable/Drawable\00", align 1
@__TypeMapEntry_from.59 = internal constant [57 x i8] c"Android.Graphics.Drawables.DrawableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.60 = internal constant [37 x i8] c"Android.Graphics.Point, Mono.Android\00", align 1
@__TypeMapEntry_to.61 = internal constant [23 x i8] c"android/graphics/Point\00", align 1
@__TypeMapEntry_from.62 = internal constant [36 x i8] c"Android.Graphics.Rect, Mono.Android\00", align 1
@__TypeMapEntry_to.63 = internal constant [22 x i8] c"android/graphics/Rect\00", align 1
@__TypeMapEntry_from.64 = internal constant [41 x i8] c"Android.Locations.Criteria, Mono.Android\00", align 1
@__TypeMapEntry_to.65 = internal constant [26 x i8] c"android/location/Criteria\00", align 1
@__TypeMapEntry_from.66 = internal constant [50 x i8] c"Android.Locations.ILocationListener, Mono.Android\00", align 1
@__TypeMapEntry_to.67 = internal constant [34 x i8] c"android/location/LocationListener\00", align 1
@__TypeMapEntry_from.68 = internal constant [57 x i8] c"Android.Locations.ILocationListenerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.69 = internal constant [41 x i8] c"Android.Locations.Location, Mono.Android\00", align 1
@__TypeMapEntry_to.70 = internal constant [26 x i8] c"android/location/Location\00", align 1
@__TypeMapEntry_from.71 = internal constant [48 x i8] c"Android.Locations.LocationManager, Mono.Android\00", align 1
@__TypeMapEntry_to.72 = internal constant [33 x i8] c"android/location/LocationManager\00", align 1
@__TypeMapEntry_from.73 = internal constant [36 x i8] c"Android.OS.BaseBundle, Mono.Android\00", align 1
@__TypeMapEntry_to.74 = internal constant [22 x i8] c"android/os/BaseBundle\00", align 1
@__TypeMapEntry_from.75 = internal constant [32 x i8] c"Android.OS.Bundle, Mono.Android\00", align 1
@__TypeMapEntry_to.76 = internal constant [18 x i8] c"android/os/Bundle\00", align 1
@__TypeMapEntry_from.77 = internal constant [33 x i8] c"Android.OS.Handler, Mono.Android\00", align 1
@__TypeMapEntry_to.78 = internal constant [19 x i8] c"android/os/Handler\00", align 1
@__TypeMapEntry_from.79 = internal constant [37 x i8] c"Android.OS.IParcelable, Mono.Android\00", align 1
@__TypeMapEntry_to.80 = internal constant [22 x i8] c"android/os/Parcelable\00", align 1
@__TypeMapEntry_from.81 = internal constant [44 x i8] c"Android.OS.IParcelableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.82 = internal constant [32 x i8] c"Android.OS.Looper, Mono.Android\00", align 1
@__TypeMapEntry_to.83 = internal constant [18 x i8] c"android/os/Looper\00", align 1
@__TypeMapEntry_from.84 = internal constant [32 x i8] c"Android.OS.Parcel, Mono.Android\00", align 1
@__TypeMapEntry_to.85 = internal constant [18 x i8] c"android/os/Parcel\00", align 1
@__TypeMapEntry_from.86 = internal constant [49 x i8] c"Android.Runtime.InputStreamAdapter, Mono.Android\00", align 1
@__TypeMapEntry_to.87 = internal constant [40 x i8] c"mono/android/runtime/InputStreamAdapter\00", align 1
@__TypeMapEntry_from.88 = internal constant [42 x i8] c"Android.Runtime.JavaArray`1, Mono.Android\00", align 1
@__TypeMapEntry_to.89 = internal constant [31 x i8] c"mono/android/runtime/JavaArray\00", align 1
@__TypeMapEntry_from.90 = internal constant [45 x i8] c"Android.Runtime.JavaCollection, Mono.Android\00", align 1
@__TypeMapEntry_to.91 = internal constant [21 x i8] c"java/util/Collection\00", align 1
@__TypeMapEntry_from.92 = internal constant [47 x i8] c"Android.Runtime.JavaCollection`1, Mono.Android\00", align 1
@__TypeMapEntry_from.93 = internal constant [45 x i8] c"Android.Runtime.JavaDictionary, Mono.Android\00", align 1
@__TypeMapEntry_to.94 = internal constant [18 x i8] c"java/util/HashMap\00", align 1
@__TypeMapEntry_from.95 = internal constant [47 x i8] c"Android.Runtime.JavaDictionary`2, Mono.Android\00", align 1
@__TypeMapEntry_from.96 = internal constant [39 x i8] c"Android.Runtime.JavaList, Mono.Android\00", align 1
@__TypeMapEntry_to.97 = internal constant [20 x i8] c"java/util/ArrayList\00", align 1
@__TypeMapEntry_from.98 = internal constant [41 x i8] c"Android.Runtime.JavaList`1, Mono.Android\00", align 1
@__TypeMapEntry_from.99 = internal constant [41 x i8] c"Android.Runtime.JavaObject, Mono.Android\00", align 1
@__TypeMapEntry_to.100 = internal constant [32 x i8] c"mono/android/runtime/JavaObject\00", align 1
@__TypeMapEntry_from.101 = internal constant [49 x i8] c"Android.Runtime.JavaProxyThrowable, Mono.Android\00", align 1
@__TypeMapEntry_to.102 = internal constant [35 x i8] c"android/runtime/JavaProxyThrowable\00", align 1
@__TypeMapEntry_from.103 = internal constant [38 x i8] c"Android.Runtime.JavaSet, Mono.Android\00", align 1
@__TypeMapEntry_to.104 = internal constant [18 x i8] c"java/util/HashSet\00", align 1
@__TypeMapEntry_from.105 = internal constant [40 x i8] c"Android.Runtime.JavaSet`1, Mono.Android\00", align 1
@__TypeMapEntry_from.106 = internal constant [50 x i8] c"Android.Runtime.OutputStreamAdapter, Mono.Android\00", align 1
@__TypeMapEntry_to.107 = internal constant [41 x i8] c"mono/android/runtime/OutputStreamAdapter\00", align 1
@__TypeMapEntry_from.108 = internal constant [42 x i8] c"Android.Util.DisplayMetrics, Mono.Android\00", align 1
@__TypeMapEntry_to.109 = internal constant [28 x i8] c"android/util/DisplayMetrics\00", align 1
@__TypeMapEntry_from.110 = internal constant [41 x i8] c"Android.Util.IAttributeSet, Mono.Android\00", align 1
@__TypeMapEntry_to.111 = internal constant [26 x i8] c"android/util/AttributeSet\00", align 1
@__TypeMapEntry_from.112 = internal constant [48 x i8] c"Android.Util.IAttributeSetInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.113 = internal constant [31 x i8] c"Android.Util.Log, Mono.Android\00", align 1
@__TypeMapEntry_to.114 = internal constant [17 x i8] c"android/util/Log\00", align 1
@__TypeMapEntry_from.115 = internal constant [61 x i8] c"Android.Views.Accessibility.AccessibilityEvent, Mono.Android\00", align 1
@__TypeMapEntry_to.116 = internal constant [46 x i8] c"android/view/accessibility/AccessibilityEvent\00", align 1
@__TypeMapEntry_from.117 = internal constant [62 x i8] c"Android.Views.Accessibility.AccessibilityRecord, Mono.Android\00", align 1
@__TypeMapEntry_to.118 = internal constant [47 x i8] c"android/view/accessibility/AccessibilityRecord\00", align 1
@__TypeMapEntry_from.119 = internal constant [68 x i8] c"Android.Views.Accessibility.IAccessibilityEventSource, Mono.Android\00", align 1
@__TypeMapEntry_to.120 = internal constant [52 x i8] c"android/view/accessibility/AccessibilityEventSource\00", align 1
@__TypeMapEntry_from.121 = internal constant [75 x i8] c"Android.Views.Accessibility.IAccessibilityEventSourceInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.122 = internal constant [49 x i8] c"Android.Views.ActionMode+ICallback, Mono.Android\00", align 1
@__TypeMapEntry_to.123 = internal constant [33 x i8] c"android/view/ActionMode$Callback\00", align 1
@__TypeMapEntry_from.124 = internal constant [56 x i8] c"Android.Views.ActionMode+ICallbackInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.125 = internal constant [39 x i8] c"Android.Views.ActionMode, Mono.Android\00", align 1
@__TypeMapEntry_to.126 = internal constant [24 x i8] c"android/view/ActionMode\00", align 1
@__TypeMapEntry_from.127 = internal constant [46 x i8] c"Android.Views.ActionModeInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.128 = internal constant [43 x i8] c"Android.Views.ActionProvider, Mono.Android\00", align 1
@__TypeMapEntry_to.129 = internal constant [28 x i8] c"android/view/ActionProvider\00", align 1
@__TypeMapEntry_from.130 = internal constant [50 x i8] c"Android.Views.ActionProviderInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.131 = internal constant [48 x i8] c"Android.Views.ContextThemeWrapper, Mono.Android\00", align 1
@__TypeMapEntry_to.132 = internal constant [33 x i8] c"android/view/ContextThemeWrapper\00", align 1
@__TypeMapEntry_from.133 = internal constant [36 x i8] c"Android.Views.Display, Mono.Android\00", align 1
@__TypeMapEntry_to.134 = internal constant [21 x i8] c"android/view/Display\00", align 1
@__TypeMapEntry_from.135 = internal constant [41 x i8] c"Android.Views.IContextMenu, Mono.Android\00", align 1
@__TypeMapEntry_to.136 = internal constant [25 x i8] c"android/view/ContextMenu\00", align 1
@__TypeMapEntry_from.137 = internal constant [56 x i8] c"Android.Views.IContextMenuContextMenuInfo, Mono.Android\00", align 1
@__TypeMapEntry_to.138 = internal constant [41 x i8] c"android/view/ContextMenu$ContextMenuInfo\00", align 1
@__TypeMapEntry_from.139 = internal constant [63 x i8] c"Android.Views.IContextMenuContextMenuInfoInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.140 = internal constant [48 x i8] c"Android.Views.IContextMenuInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.141 = internal constant [34 x i8] c"Android.Views.IMenu, Mono.Android\00", align 1
@__TypeMapEntry_to.142 = internal constant [18 x i8] c"android/view/Menu\00", align 1
@__TypeMapEntry_from.143 = internal constant [41 x i8] c"Android.Views.IMenuInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.144 = internal constant [38 x i8] c"Android.Views.IMenuItem, Mono.Android\00", align 1
@__TypeMapEntry_to.145 = internal constant [22 x i8] c"android/view/MenuItem\00", align 1
@__TypeMapEntry_from.146 = internal constant [45 x i8] c"Android.Views.IMenuItemInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.147 = internal constant [60 x i8] c"Android.Views.IMenuItemOnActionExpandListener, Mono.Android\00", align 1
@__TypeMapEntry_to.148 = internal constant [45 x i8] c"android/view/MenuItem$OnActionExpandListener\00", align 1
@__TypeMapEntry_from.149 = internal constant [67 x i8] c"Android.Views.IMenuItemOnActionExpandListenerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.150 = internal constant [61 x i8] c"Android.Views.IMenuItemOnMenuItemClickListener, Mono.Android\00", align 1
@__TypeMapEntry_to.151 = internal constant [46 x i8] c"android/view/MenuItem$OnMenuItemClickListener\00", align 1
@__TypeMapEntry_from.152 = internal constant [68 x i8] c"Android.Views.IMenuItemOnMenuItemClickListenerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.153 = internal constant [37 x i8] c"Android.Views.ISubMenu, Mono.Android\00", align 1
@__TypeMapEntry_to.154 = internal constant [21 x i8] c"android/view/SubMenu\00", align 1
@__TypeMapEntry_from.155 = internal constant [44 x i8] c"Android.Views.ISubMenuInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.156 = internal constant [41 x i8] c"Android.Views.IViewManager, Mono.Android\00", align 1
@__TypeMapEntry_to.157 = internal constant [25 x i8] c"android/view/ViewManager\00", align 1
@__TypeMapEntry_from.158 = internal constant [48 x i8] c"Android.Views.IViewManagerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.159 = internal constant [40 x i8] c"Android.Views.IViewParent, Mono.Android\00", align 1
@__TypeMapEntry_to.160 = internal constant [24 x i8] c"android/view/ViewParent\00", align 1
@__TypeMapEntry_from.161 = internal constant [47 x i8] c"Android.Views.IViewParentInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.162 = internal constant [43 x i8] c"Android.Views.IWindowManager, Mono.Android\00", align 1
@__TypeMapEntry_to.163 = internal constant [27 x i8] c"android/view/WindowManager\00", align 1
@__TypeMapEntry_from.164 = internal constant [50 x i8] c"Android.Views.IWindowManagerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.165 = internal constant [39 x i8] c"Android.Views.InputEvent, Mono.Android\00", align 1
@__TypeMapEntry_to.166 = internal constant [24 x i8] c"android/view/InputEvent\00", align 1
@__TypeMapEntry_from.167 = internal constant [46 x i8] c"Android.Views.InputEventInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.168 = internal constant [47 x i8] c"Android.Views.KeyEvent+ICallback, Mono.Android\00", align 1
@__TypeMapEntry_to.169 = internal constant [31 x i8] c"android/view/KeyEvent$Callback\00", align 1
@__TypeMapEntry_from.170 = internal constant [54 x i8] c"Android.Views.KeyEvent+ICallbackInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.171 = internal constant [37 x i8] c"Android.Views.KeyEvent, Mono.Android\00", align 1
@__TypeMapEntry_to.172 = internal constant [22 x i8] c"android/view/KeyEvent\00", align 1
@__TypeMapEntry_from.173 = internal constant [52 x i8] c"Android.Views.LayoutInflater+IFactory, Mono.Android\00", align 1
@__TypeMapEntry_to.174 = internal constant [36 x i8] c"android/view/LayoutInflater$Factory\00", align 1
@__TypeMapEntry_from.175 = internal constant [53 x i8] c"Android.Views.LayoutInflater+IFactory2, Mono.Android\00", align 1
@__TypeMapEntry_to.176 = internal constant [37 x i8] c"android/view/LayoutInflater$Factory2\00", align 1
@__TypeMapEntry_from.177 = internal constant [60 x i8] c"Android.Views.LayoutInflater+IFactory2Invoker, Mono.Android\00", align 1
@__TypeMapEntry_from.178 = internal constant [59 x i8] c"Android.Views.LayoutInflater+IFactoryInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.179 = internal constant [43 x i8] c"Android.Views.LayoutInflater, Mono.Android\00", align 1
@__TypeMapEntry_to.180 = internal constant [28 x i8] c"android/view/LayoutInflater\00", align 1
@__TypeMapEntry_from.181 = internal constant [50 x i8] c"Android.Views.LayoutInflaterInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.182 = internal constant [40 x i8] c"Android.Views.MotionEvent, Mono.Android\00", align 1
@__TypeMapEntry_to.183 = internal constant [25 x i8] c"android/view/MotionEvent\00", align 1
@__TypeMapEntry_from.184 = internal constant [40 x i8] c"Android.Views.SearchEvent, Mono.Android\00", align 1
@__TypeMapEntry_to.185 = internal constant [25 x i8] c"android/view/SearchEvent\00", align 1
@__TypeMapEntry_from.186 = internal constant [50 x i8] c"Android.Views.View+IOnClickListener, Mono.Android\00", align 1
@__TypeMapEntry_to.187 = internal constant [34 x i8] c"android/view/View$OnClickListener\00", align 1
@__TypeMapEntry_from.188 = internal constant [61 x i8] c"Android.Views.View+IOnClickListenerImplementor, Mono.Android\00", align 1
@__TypeMapEntry_to.189 = internal constant [50 x i8] c"mono/android/view/View_OnClickListenerImplementor\00", align 1
@__TypeMapEntry_from.190 = internal constant [57 x i8] c"Android.Views.View+IOnClickListenerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.191 = internal constant [62 x i8] c"Android.Views.View+IOnCreateContextMenuListener, Mono.Android\00", align 1
@__TypeMapEntry_to.192 = internal constant [46 x i8] c"android/view/View$OnCreateContextMenuListener\00", align 1
@__TypeMapEntry_from.193 = internal constant [69 x i8] c"Android.Views.View+IOnCreateContextMenuListenerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.194 = internal constant [33 x i8] c"Android.Views.View, Mono.Android\00", align 1
@__TypeMapEntry_to.195 = internal constant [18 x i8] c"android/view/View\00", align 1
@__TypeMapEntry_from.196 = internal constant [51 x i8] c"Android.Views.ViewGroup+LayoutParams, Mono.Android\00", align 1
@__TypeMapEntry_to.197 = internal constant [36 x i8] c"android/view/ViewGroup$LayoutParams\00", align 1
@__TypeMapEntry_from.198 = internal constant [38 x i8] c"Android.Views.ViewGroup, Mono.Android\00", align 1
@__TypeMapEntry_to.199 = internal constant [23 x i8] c"android/view/ViewGroup\00", align 1
@__TypeMapEntry_from.200 = internal constant [45 x i8] c"Android.Views.ViewGroupInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.201 = internal constant [64 x i8] c"Android.Views.ViewTreeObserver+IOnPreDrawListener, Mono.Android\00", align 1
@__TypeMapEntry_to.202 = internal constant [48 x i8] c"android/view/ViewTreeObserver$OnPreDrawListener\00", align 1
@__TypeMapEntry_from.203 = internal constant [71 x i8] c"Android.Views.ViewTreeObserver+IOnPreDrawListenerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.204 = internal constant [45 x i8] c"Android.Views.ViewTreeObserver, Mono.Android\00", align 1
@__TypeMapEntry_to.205 = internal constant [30 x i8] c"android/view/ViewTreeObserver\00", align 1
@__TypeMapEntry_from.206 = internal constant [45 x i8] c"Android.Views.Window+ICallback, Mono.Android\00", align 1
@__TypeMapEntry_to.207 = internal constant [29 x i8] c"android/view/Window$Callback\00", align 1
@__TypeMapEntry_from.208 = internal constant [52 x i8] c"Android.Views.Window+ICallbackInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.209 = internal constant [35 x i8] c"Android.Views.Window, Mono.Android\00", align 1
@__TypeMapEntry_to.210 = internal constant [20 x i8] c"android/view/Window\00", align 1
@__TypeMapEntry_from.211 = internal constant [42 x i8] c"Android.Views.WindowInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.212 = internal constant [54 x i8] c"Android.Views.WindowManagerLayoutParams, Mono.Android\00", align 1
@__TypeMapEntry_to.213 = internal constant [40 x i8] c"android/view/WindowManager$LayoutParams\00", align 1
@__TypeMapEntry_from.214 = internal constant [36 x i8] c"Android.Widget.Button, Mono.Android\00", align 1
@__TypeMapEntry_to.215 = internal constant [22 x i8] c"android/widget/Button\00", align 1
@__TypeMapEntry_from.216 = internal constant [38 x i8] c"Android.Widget.TextView, Mono.Android\00", align 1
@__TypeMapEntry_to.217 = internal constant [24 x i8] c"android/widget/TextView\00", align 1
@__TypeMapEntry_from.218 = internal constant [35 x i8] c"Android.Widget.Toast, Mono.Android\00", align 1
@__TypeMapEntry_to.219 = internal constant [21 x i8] c"android/widget/Toast\00", align 1
@__TypeMapEntry_from.220 = internal constant [38 x i8] c"Java.IO.FileInputStream, Mono.Android\00", align 1
@__TypeMapEntry_to.221 = internal constant [24 x i8] c"java/io/FileInputStream\00", align 1
@__TypeMapEntry_from.222 = internal constant [33 x i8] c"Java.IO.ICloseable, Mono.Android\00", align 1
@__TypeMapEntry_to.223 = internal constant [18 x i8] c"java/io/Closeable\00", align 1
@__TypeMapEntry_from.224 = internal constant [40 x i8] c"Java.IO.ICloseableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.225 = internal constant [33 x i8] c"Java.IO.IFlushable, Mono.Android\00", align 1
@__TypeMapEntry_to.226 = internal constant [18 x i8] c"java/io/Flushable\00", align 1
@__TypeMapEntry_from.227 = internal constant [40 x i8] c"Java.IO.IFlushableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.228 = internal constant [34 x i8] c"Java.IO.IOException, Mono.Android\00", align 1
@__TypeMapEntry_to.229 = internal constant [20 x i8] c"java/io/IOException\00", align 1
@__TypeMapEntry_from.230 = internal constant [36 x i8] c"Java.IO.ISerializable, Mono.Android\00", align 1
@__TypeMapEntry_to.231 = internal constant [21 x i8] c"java/io/Serializable\00", align 1
@__TypeMapEntry_from.232 = internal constant [43 x i8] c"Java.IO.ISerializableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.233 = internal constant [34 x i8] c"Java.IO.InputStream, Mono.Android\00", align 1
@__TypeMapEntry_to.234 = internal constant [20 x i8] c"java/io/InputStream\00", align 1
@__TypeMapEntry_from.235 = internal constant [41 x i8] c"Java.IO.InputStreamInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.236 = internal constant [35 x i8] c"Java.IO.OutputStream, Mono.Android\00", align 1
@__TypeMapEntry_to.237 = internal constant [21 x i8] c"java/io/OutputStream\00", align 1
@__TypeMapEntry_from.238 = internal constant [42 x i8] c"Java.IO.OutputStreamInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.239 = internal constant [34 x i8] c"Java.IO.PrintWriter, Mono.Android\00", align 1
@__TypeMapEntry_to.240 = internal constant [20 x i8] c"java/io/PrintWriter\00", align 1
@__TypeMapEntry_from.241 = internal constant [35 x i8] c"Java.IO.StringWriter, Mono.Android\00", align 1
@__TypeMapEntry_to.242 = internal constant [21 x i8] c"java/io/StringWriter\00", align 1
@__TypeMapEntry_from.243 = internal constant [29 x i8] c"Java.IO.Writer, Mono.Android\00", align 1
@__TypeMapEntry_to.244 = internal constant [15 x i8] c"java/io/Writer\00", align 1
@__TypeMapEntry_from.245 = internal constant [36 x i8] c"Java.IO.WriterInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.246 = internal constant [55 x i8] c"Java.Interop.TypeManager+JavaTypeManager, Mono.Android\00", align 1
@__TypeMapEntry_to.247 = internal constant [25 x i8] c"mono/android/TypeManager\00", align 1
@__TypeMapEntry_from.248 = internal constant [47 x i8] c"Java.Lang.Annotation.IAnnotation, Mono.Android\00", align 1
@__TypeMapEntry_to.249 = internal constant [32 x i8] c"java/lang/annotation/Annotation\00", align 1
@__TypeMapEntry_from.250 = internal constant [54 x i8] c"Java.Lang.Annotation.IAnnotationInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.251 = internal constant [32 x i8] c"Java.Lang.Boolean, Mono.Android\00", align 1
@__TypeMapEntry_to.252 = internal constant [18 x i8] c"java/lang/Boolean\00", align 1
@__TypeMapEntry_from.253 = internal constant [29 x i8] c"Java.Lang.Byte, Mono.Android\00", align 1
@__TypeMapEntry_to.254 = internal constant [15 x i8] c"java/lang/Byte\00", align 1
@__TypeMapEntry_from.255 = internal constant [34 x i8] c"Java.Lang.Character, Mono.Android\00", align 1
@__TypeMapEntry_to.256 = internal constant [20 x i8] c"java/lang/Character\00", align 1
@__TypeMapEntry_from.257 = internal constant [30 x i8] c"Java.Lang.Class, Mono.Android\00", align 1
@__TypeMapEntry_to.258 = internal constant [16 x i8] c"java/lang/Class\00", align 1
@__TypeMapEntry_from.259 = internal constant [43 x i8] c"Java.Lang.ClassCastException, Mono.Android\00", align 1
@__TypeMapEntry_to.260 = internal constant [29 x i8] c"java/lang/ClassCastException\00", align 1
@__TypeMapEntry_from.261 = internal constant [47 x i8] c"Java.Lang.ClassNotFoundException, Mono.Android\00", align 1
@__TypeMapEntry_to.262 = internal constant [33 x i8] c"java/lang/ClassNotFoundException\00", align 1
@__TypeMapEntry_from.263 = internal constant [31 x i8] c"Java.Lang.Double, Mono.Android\00", align 1
@__TypeMapEntry_to.264 = internal constant [17 x i8] c"java/lang/Double\00", align 1
@__TypeMapEntry_from.265 = internal constant [30 x i8] c"Java.Lang.Error, Mono.Android\00", align 1
@__TypeMapEntry_to.266 = internal constant [16 x i8] c"java/lang/Error\00", align 1
@__TypeMapEntry_from.267 = internal constant [34 x i8] c"Java.Lang.Exception, Mono.Android\00", align 1
@__TypeMapEntry_to.268 = internal constant [20 x i8] c"java/lang/Exception\00", align 1
@__TypeMapEntry_from.269 = internal constant [30 x i8] c"Java.Lang.Float, Mono.Android\00", align 1
@__TypeMapEntry_to.270 = internal constant [16 x i8] c"java/lang/Float\00", align 1
@__TypeMapEntry_from.271 = internal constant [36 x i8] c"Java.Lang.IAppendable, Mono.Android\00", align 1
@__TypeMapEntry_to.272 = internal constant [21 x i8] c"java/lang/Appendable\00", align 1
@__TypeMapEntry_from.273 = internal constant [43 x i8] c"Java.Lang.IAppendableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.274 = internal constant [38 x i8] c"Java.Lang.ICharSequence, Mono.Android\00", align 1
@__TypeMapEntry_to.275 = internal constant [23 x i8] c"java/lang/CharSequence\00", align 1
@__TypeMapEntry_from.276 = internal constant [45 x i8] c"Java.Lang.ICharSequenceInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.277 = internal constant [35 x i8] c"Java.Lang.ICloneable, Mono.Android\00", align 1
@__TypeMapEntry_to.278 = internal constant [20 x i8] c"java/lang/Cloneable\00", align 1
@__TypeMapEntry_from.279 = internal constant [42 x i8] c"Java.Lang.ICloneableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.280 = internal constant [36 x i8] c"Java.Lang.IComparable, Mono.Android\00", align 1
@__TypeMapEntry_to.281 = internal constant [21 x i8] c"java/lang/Comparable\00", align 1
@__TypeMapEntry_from.282 = internal constant [43 x i8] c"Java.Lang.IComparableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.283 = internal constant [34 x i8] c"Java.Lang.IRunnable, Mono.Android\00", align 1
@__TypeMapEntry_to.284 = internal constant [19 x i8] c"java/lang/Runnable\00", align 1
@__TypeMapEntry_from.285 = internal constant [41 x i8] c"Java.Lang.IRunnableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.286 = internal constant [49 x i8] c"Java.Lang.IllegalArgumentException, Mono.Android\00", align 1
@__TypeMapEntry_to.287 = internal constant [35 x i8] c"java/lang/IllegalArgumentException\00", align 1
@__TypeMapEntry_from.288 = internal constant [46 x i8] c"Java.Lang.IllegalStateException, Mono.Android\00", align 1
@__TypeMapEntry_to.289 = internal constant [32 x i8] c"java/lang/IllegalStateException\00", align 1
@__TypeMapEntry_from.290 = internal constant [50 x i8] c"Java.Lang.IndexOutOfBoundsException, Mono.Android\00", align 1
@__TypeMapEntry_to.291 = internal constant [36 x i8] c"java/lang/IndexOutOfBoundsException\00", align 1
@__TypeMapEntry_from.292 = internal constant [32 x i8] c"Java.Lang.Integer, Mono.Android\00", align 1
@__TypeMapEntry_to.293 = internal constant [18 x i8] c"java/lang/Integer\00", align 1
@__TypeMapEntry_from.294 = internal constant [37 x i8] c"Java.Lang.LinkageError, Mono.Android\00", align 1
@__TypeMapEntry_to.295 = internal constant [23 x i8] c"java/lang/LinkageError\00", align 1
@__TypeMapEntry_from.296 = internal constant [29 x i8] c"Java.Lang.Long, Mono.Android\00", align 1
@__TypeMapEntry_to.297 = internal constant [15 x i8] c"java/lang/Long\00", align 1
@__TypeMapEntry_from.298 = internal constant [45 x i8] c"Java.Lang.NoClassDefFoundError, Mono.Android\00", align 1
@__TypeMapEntry_to.299 = internal constant [31 x i8] c"java/lang/NoClassDefFoundError\00", align 1
@__TypeMapEntry_from.300 = internal constant [45 x i8] c"Java.Lang.NullPointerException, Mono.Android\00", align 1
@__TypeMapEntry_to.301 = internal constant [31 x i8] c"java/lang/NullPointerException\00", align 1
@__TypeMapEntry_from.302 = internal constant [31 x i8] c"Java.Lang.Number, Mono.Android\00", align 1
@__TypeMapEntry_to.303 = internal constant [17 x i8] c"java/lang/Number\00", align 1
@__TypeMapEntry_from.304 = internal constant [38 x i8] c"Java.Lang.NumberInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.305 = internal constant [31 x i8] c"Java.Lang.Object, Mono.Android\00", align 1
@__TypeMapEntry_to.306 = internal constant [17 x i8] c"java/lang/Object\00", align 1
@__TypeMapEntry_from.307 = internal constant [50 x i8] c"Java.Lang.Reflect.IAnnotatedElement, Mono.Android\00", align 1
@__TypeMapEntry_to.308 = internal constant [35 x i8] c"java/lang/reflect/AnnotatedElement\00", align 1
@__TypeMapEntry_from.309 = internal constant [57 x i8] c"Java.Lang.Reflect.IAnnotatedElementInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.310 = internal constant [52 x i8] c"Java.Lang.Reflect.IGenericDeclaration, Mono.Android\00", align 1
@__TypeMapEntry_to.311 = internal constant [37 x i8] c"java/lang/reflect/GenericDeclaration\00", align 1
@__TypeMapEntry_from.312 = internal constant [59 x i8] c"Java.Lang.Reflect.IGenericDeclarationInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.313 = internal constant [38 x i8] c"Java.Lang.Reflect.IType, Mono.Android\00", align 1
@__TypeMapEntry_to.314 = internal constant [23 x i8] c"java/lang/reflect/Type\00", align 1
@__TypeMapEntry_from.315 = internal constant [45 x i8] c"Java.Lang.Reflect.ITypeInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.316 = internal constant [46 x i8] c"Java.Lang.Reflect.ITypeVariable, Mono.Android\00", align 1
@__TypeMapEntry_to.317 = internal constant [31 x i8] c"java/lang/reflect/TypeVariable\00", align 1
@__TypeMapEntry_from.318 = internal constant [53 x i8] c"Java.Lang.Reflect.ITypeVariableInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.319 = internal constant [53 x i8] c"Java.Lang.ReflectiveOperationException, Mono.Android\00", align 1
@__TypeMapEntry_to.320 = internal constant [39 x i8] c"java/lang/ReflectiveOperationException\00", align 1
@__TypeMapEntry_from.321 = internal constant [41 x i8] c"Java.Lang.RuntimeException, Mono.Android\00", align 1
@__TypeMapEntry_to.322 = internal constant [27 x i8] c"java/lang/RuntimeException\00", align 1
@__TypeMapEntry_from.323 = internal constant [30 x i8] c"Java.Lang.Short, Mono.Android\00", align 1
@__TypeMapEntry_to.324 = internal constant [16 x i8] c"java/lang/Short\00", align 1
@__TypeMapEntry_from.325 = internal constant [31 x i8] c"Java.Lang.String, Mono.Android\00", align 1
@__TypeMapEntry_to.326 = internal constant [17 x i8] c"java/lang/String\00", align 1
@__TypeMapEntry_from.327 = internal constant [51 x i8] c"Java.Lang.Thread+RunnableImplementor, Mono.Android\00", align 1
@__TypeMapEntry_to.328 = internal constant [35 x i8] c"mono/java/lang/RunnableImplementor\00", align 1
@__TypeMapEntry_from.329 = internal constant [31 x i8] c"Java.Lang.Thread, Mono.Android\00", align 1
@__TypeMapEntry_to.330 = internal constant [17 x i8] c"java/lang/Thread\00", align 1
@__TypeMapEntry_from.331 = internal constant [34 x i8] c"Java.Lang.Throwable, Mono.Android\00", align 1
@__TypeMapEntry_to.332 = internal constant [20 x i8] c"java/lang/Throwable\00", align 1
@__TypeMapEntry_from.333 = internal constant [54 x i8] c"Java.Lang.UnsupportedOperationException, Mono.Android\00", align 1
@__TypeMapEntry_to.334 = internal constant [40 x i8] c"java/lang/UnsupportedOperationException\00", align 1
@__TypeMapEntry_from.335 = internal constant [41 x i8] c"Java.Net.InetSocketAddress, Mono.Android\00", align 1
@__TypeMapEntry_to.336 = internal constant [27 x i8] c"java/net/InetSocketAddress\00", align 1
@__TypeMapEntry_from.337 = internal constant [29 x i8] c"Java.Net.Proxy, Mono.Android\00", align 1
@__TypeMapEntry_to.338 = internal constant [15 x i8] c"java/net/Proxy\00", align 1
@__TypeMapEntry_from.339 = internal constant [37 x i8] c"Java.Net.ProxySelector, Mono.Android\00", align 1
@__TypeMapEntry_to.340 = internal constant [23 x i8] c"java/net/ProxySelector\00", align 1
@__TypeMapEntry_from.341 = internal constant [44 x i8] c"Java.Net.ProxySelectorInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.342 = internal constant [37 x i8] c"Java.Net.SocketAddress, Mono.Android\00", align 1
@__TypeMapEntry_to.343 = internal constant [23 x i8] c"java/net/SocketAddress\00", align 1
@__TypeMapEntry_from.344 = internal constant [44 x i8] c"Java.Net.SocketAddressInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.345 = internal constant [27 x i8] c"Java.Net.URI, Mono.Android\00", align 1
@__TypeMapEntry_to.346 = internal constant [13 x i8] c"java/net/URI\00", align 1
@__TypeMapEntry_from.347 = internal constant [30 x i8] c"Java.Nio.Buffer, Mono.Android\00", align 1
@__TypeMapEntry_to.348 = internal constant [16 x i8] c"java/nio/Buffer\00", align 1
@__TypeMapEntry_from.349 = internal constant [37 x i8] c"Java.Nio.BufferInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.350 = internal constant [34 x i8] c"Java.Nio.ByteBuffer, Mono.Android\00", align 1
@__TypeMapEntry_to.351 = internal constant [20 x i8] c"java/nio/ByteBuffer\00", align 1
@__TypeMapEntry_from.352 = internal constant [41 x i8] c"Java.Nio.ByteBufferInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.353 = internal constant [44 x i8] c"Java.Nio.Channels.FileChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.354 = internal constant [30 x i8] c"java/nio/channels/FileChannel\00", align 1
@__TypeMapEntry_from.355 = internal constant [51 x i8] c"Java.Nio.Channels.FileChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.356 = internal constant [45 x i8] c"Java.Nio.Channels.IByteChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.357 = internal constant [30 x i8] c"java/nio/channels/ByteChannel\00", align 1
@__TypeMapEntry_from.358 = internal constant [52 x i8] c"Java.Nio.Channels.IByteChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.359 = internal constant [41 x i8] c"Java.Nio.Channels.IChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.360 = internal constant [26 x i8] c"java/nio/channels/Channel\00", align 1
@__TypeMapEntry_from.361 = internal constant [48 x i8] c"Java.Nio.Channels.IChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.362 = internal constant [54 x i8] c"Java.Nio.Channels.IGatheringByteChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.363 = internal constant [39 x i8] c"java/nio/channels/GatheringByteChannel\00", align 1
@__TypeMapEntry_from.364 = internal constant [61 x i8] c"Java.Nio.Channels.IGatheringByteChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.365 = internal constant [54 x i8] c"Java.Nio.Channels.IInterruptibleChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.366 = internal constant [39 x i8] c"java/nio/channels/InterruptibleChannel\00", align 1
@__TypeMapEntry_from.367 = internal constant [61 x i8] c"Java.Nio.Channels.IInterruptibleChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.368 = internal constant [53 x i8] c"Java.Nio.Channels.IReadableByteChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.369 = internal constant [38 x i8] c"java/nio/channels/ReadableByteChannel\00", align 1
@__TypeMapEntry_from.370 = internal constant [60 x i8] c"Java.Nio.Channels.IReadableByteChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.371 = internal constant [55 x i8] c"Java.Nio.Channels.IScatteringByteChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.372 = internal constant [40 x i8] c"java/nio/channels/ScatteringByteChannel\00", align 1
@__TypeMapEntry_from.373 = internal constant [62 x i8] c"Java.Nio.Channels.IScatteringByteChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.374 = internal constant [53 x i8] c"Java.Nio.Channels.ISeekableByteChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.375 = internal constant [38 x i8] c"java/nio/channels/SeekableByteChannel\00", align 1
@__TypeMapEntry_from.376 = internal constant [60 x i8] c"Java.Nio.Channels.ISeekableByteChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.377 = internal constant [53 x i8] c"Java.Nio.Channels.IWritableByteChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.378 = internal constant [38 x i8] c"java/nio/channels/WritableByteChannel\00", align 1
@__TypeMapEntry_from.379 = internal constant [60 x i8] c"Java.Nio.Channels.IWritableByteChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.380 = internal constant [65 x i8] c"Java.Nio.Channels.Spi.AbstractInterruptibleChannel, Mono.Android\00", align 1
@__TypeMapEntry_to.381 = internal constant [51 x i8] c"java/nio/channels/spi/AbstractInterruptibleChannel\00", align 1
@__TypeMapEntry_from.382 = internal constant [72 x i8] c"Java.Nio.Channels.Spi.AbstractInterruptibleChannelInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.383 = internal constant [45 x i8] c"Java.Security.Cert.Certificate, Mono.Android\00", align 1
@__TypeMapEntry_to.384 = internal constant [31 x i8] c"java/security/cert/Certificate\00", align 1
@__TypeMapEntry_from.385 = internal constant [52 x i8] c"Java.Security.Cert.CertificateFactory, Mono.Android\00", align 1
@__TypeMapEntry_to.386 = internal constant [38 x i8] c"java/security/cert/CertificateFactory\00", align 1
@__TypeMapEntry_from.387 = internal constant [52 x i8] c"Java.Security.Cert.CertificateInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.388 = internal constant [48 x i8] c"Java.Security.Cert.IX509Extension, Mono.Android\00", align 1
@__TypeMapEntry_to.389 = internal constant [33 x i8] c"java/security/cert/X509Extension\00", align 1
@__TypeMapEntry_from.390 = internal constant [55 x i8] c"Java.Security.Cert.IX509ExtensionInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.391 = internal constant [49 x i8] c"Java.Security.Cert.X509Certificate, Mono.Android\00", align 1
@__TypeMapEntry_to.392 = internal constant [35 x i8] c"java/security/cert/X509Certificate\00", align 1
@__TypeMapEntry_from.393 = internal constant [56 x i8] c"Java.Security.Cert.X509CertificateInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.394 = internal constant [57 x i8] c"Java.Security.KeyStore+ILoadStoreParameter, Mono.Android\00", align 1
@__TypeMapEntry_to.395 = internal constant [42 x i8] c"java/security/KeyStore$LoadStoreParameter\00", align 1
@__TypeMapEntry_from.396 = internal constant [64 x i8] c"Java.Security.KeyStore+ILoadStoreParameterInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.397 = internal constant [58 x i8] c"Java.Security.KeyStore+IProtectionParameter, Mono.Android\00", align 1
@__TypeMapEntry_to.398 = internal constant [43 x i8] c"java/security/KeyStore$ProtectionParameter\00", align 1
@__TypeMapEntry_from.399 = internal constant [65 x i8] c"Java.Security.KeyStore+IProtectionParameterInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.400 = internal constant [37 x i8] c"Java.Security.KeyStore, Mono.Android\00", align 1
@__TypeMapEntry_to.401 = internal constant [23 x i8] c"java/security/KeyStore\00", align 1
@__TypeMapEntry_from.402 = internal constant [45 x i8] c"Java.Util.Concurrent.IExecutor, Mono.Android\00", align 1
@__TypeMapEntry_to.403 = internal constant [30 x i8] c"java/util/concurrent/Executor\00", align 1
@__TypeMapEntry_from.404 = internal constant [52 x i8] c"Java.Util.Concurrent.IExecutorInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.405 = internal constant [34 x i8] c"Java.Util.IIterator, Mono.Android\00", align 1
@__TypeMapEntry_to.406 = internal constant [19 x i8] c"java/util/Iterator\00", align 1
@__TypeMapEntry_from.407 = internal constant [41 x i8] c"Java.Util.IIteratorInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.408 = internal constant [42 x i8] c"Javax.Net.Ssl.ITrustManager, Mono.Android\00", align 1
@__TypeMapEntry_to.409 = internal constant [27 x i8] c"javax/net/ssl/TrustManager\00", align 1
@__TypeMapEntry_from.410 = internal constant [49 x i8] c"Javax.Net.Ssl.ITrustManagerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.411 = internal constant [46 x i8] c"Javax.Net.Ssl.IX509TrustManager, Mono.Android\00", align 1
@__TypeMapEntry_to.412 = internal constant [31 x i8] c"javax/net/ssl/X509TrustManager\00", align 1
@__TypeMapEntry_from.413 = internal constant [53 x i8] c"Javax.Net.Ssl.IX509TrustManagerInvoker, Mono.Android\00", align 1
@__TypeMapEntry_from.414 = internal constant [48 x i8] c"Javax.Net.Ssl.TrustManagerFactory, Mono.Android\00", align 1
@__TypeMapEntry_to.415 = internal constant [34 x i8] c"javax/net/ssl/TrustManagerFactory\00", align 1

; map_managed_to_java
@map_managed_to_java = internal constant [251 x %struct.TypeMapEntry] [
	; 0
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([72 x i8], [72 x i8]* @__TypeMapEntry_from.0, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.1, i32 0, i32 0); to
	}, 
	; 1
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.2, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.3, i32 0, i32 0); to
	}, 
	; 2
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.4, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.5, i32 0, i32 0); to
	}, 
	; 3
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.6, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.7, i32 0, i32 0); to
	}, 
	; 4
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.8, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.9, i32 0, i32 0); to
	}, 
	; 5
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.10, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.9, i32 0, i32 0); to
	}, 
	; 6
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.11, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.12, i32 0, i32 0); to
	}, 
	; 7
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.13, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.14, i32 0, i32 0); to
	}, 
	; 8
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_from.15, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.16, i32 0, i32 0); to
	}, 
	; 9
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([58 x i8], [58 x i8]* @__TypeMapEntry_from.17, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.16, i32 0, i32 0); to
	}, 
	; 10
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.18, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.14, i32 0, i32 0); to
	}, 
	; 11
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.19, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.20, i32 0, i32 0); to
	}, 
	; 12
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.21, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.22, i32 0, i32 0); to
	}, 
	; 13
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([89 x i8], [89 x i8]* @__TypeMapEntry_from.23, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_to.24, i32 0, i32 0); to
	}, 
	; 14
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([70 x i8], [70 x i8]* @__TypeMapEntry_from.25, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.26, i32 0, i32 0); to
	}, 
	; 15
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([77 x i8], [77 x i8]* @__TypeMapEntry_from.27, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.26, i32 0, i32 0); to
	}, 
	; 16
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([66 x i8], [66 x i8]* @__TypeMapEntry_from.28, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_to.29, i32 0, i32 0); to
	}, 
	; 17
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([73 x i8], [73 x i8]* @__TypeMapEntry_from.30, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_to.29, i32 0, i32 0); to
	}, 
	; 18
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([72 x i8], [72 x i8]* @__TypeMapEntry_from.31, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.32, i32 0, i32 0); to
	}, 
	; 19
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([79 x i8], [79 x i8]* @__TypeMapEntry_from.33, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.32, i32 0, i32 0); to
	}, 
	; 20
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([72 x i8], [72 x i8]* @__TypeMapEntry_from.34, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.35, i32 0, i32 0); to
	}, 
	; 21
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([79 x i8], [79 x i8]* @__TypeMapEntry_from.36, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.35, i32 0, i32 0); to
	}, 
	; 22
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([71 x i8], [71 x i8]* @__TypeMapEntry_from.37, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.38, i32 0, i32 0); to
	}, 
	; 23
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([78 x i8], [78 x i8]* @__TypeMapEntry_from.39, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.38, i32 0, i32 0); to
	}, 
	; 24
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([71 x i8], [71 x i8]* @__TypeMapEntry_from.40, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.41, i32 0, i32 0); to
	}, 
	; 25
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([78 x i8], [78 x i8]* @__TypeMapEntry_from.42, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.41, i32 0, i32 0); to
	}, 
	; 26
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([77 x i8], [77 x i8]* @__TypeMapEntry_from.43, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_to.44, i32 0, i32 0); to
	}, 
	; 27
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([84 x i8], [84 x i8]* @__TypeMapEntry_from.45, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_to.44, i32 0, i32 0); to
	}, 
	; 28
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([73 x i8], [73 x i8]* @__TypeMapEntry_from.46, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_to.47, i32 0, i32 0); to
	}, 
	; 29
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([80 x i8], [80 x i8]* @__TypeMapEntry_from.48, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_to.47, i32 0, i32 0); to
	}, 
	; 30
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.49, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.50, i32 0, i32 0); to
	}, 
	; 31
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([73 x i8], [73 x i8]* @__TypeMapEntry_from.51, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_to.52, i32 0, i32 0); to
	}, 
	; 32
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([64 x i8], [64 x i8]* @__TypeMapEntry_from.53, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.50, i32 0, i32 0); to
	}, 
	; 33
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([60 x i8], [60 x i8]* @__TypeMapEntry_from.54, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_to.55, i32 0, i32 0); to
	}, 
	; 34
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([67 x i8], [67 x i8]* @__TypeMapEntry_from.56, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_to.55, i32 0, i32 0); to
	}, 
	; 35
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.57, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.58, i32 0, i32 0); to
	}, 
	; 36
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.59, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.58, i32 0, i32 0); to
	}, 
	; 37
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.60, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.61, i32 0, i32 0); to
	}, 
	; 38
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.62, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.63, i32 0, i32 0); to
	}, 
	; 39
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.64, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.65, i32 0, i32 0); to
	}, 
	; 40
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.66, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.67, i32 0, i32 0); to
	}, 
	; 41
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.68, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.67, i32 0, i32 0); to
	}, 
	; 42
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.69, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.70, i32 0, i32 0); to
	}, 
	; 43
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.71, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.72, i32 0, i32 0); to
	}, 
	; 44
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.73, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.74, i32 0, i32 0); to
	}, 
	; 45
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.75, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.76, i32 0, i32 0); to
	}, 
	; 46
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_from.77, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.78, i32 0, i32 0); to
	}, 
	; 47
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.79, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.80, i32 0, i32 0); to
	}, 
	; 48
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.81, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.80, i32 0, i32 0); to
	}, 
	; 49
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.82, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.83, i32 0, i32 0); to
	}, 
	; 50
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.84, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.85, i32 0, i32 0); to
	}, 
	; 51
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.86, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.87, i32 0, i32 0); to
	}, 
	; 52
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_from.88, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.89, i32 0, i32 0); to
	}, 
	; 53
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.90, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.91, i32 0, i32 0); to
	}, 
	; 54
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_from.92, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.91, i32 0, i32 0); to
	}, 
	; 55
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.93, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.94, i32 0, i32 0); to
	}, 
	; 56
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_from.95, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.94, i32 0, i32 0); to
	}, 
	; 57
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.96, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.97, i32 0, i32 0); to
	}, 
	; 58
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.98, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.97, i32 0, i32 0); to
	}, 
	; 59
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.99, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_to.100, i32 0, i32 0); to
	}, 
	; 60
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.101, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.102, i32 0, i32 0); to
	}, 
	; 61
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.103, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.104, i32 0, i32 0); to
	}, 
	; 62
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_from.105, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.104, i32 0, i32 0); to
	}, 
	; 63
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.106, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_to.107, i32 0, i32 0); to
	}, 
	; 64
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_from.108, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.109, i32 0, i32 0); to
	}, 
	; 65
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.110, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.111, i32 0, i32 0); to
	}, 
	; 66
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.112, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.111, i32 0, i32 0); to
	}, 
	; 67
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.113, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.114, i32 0, i32 0); to
	}, 
	; 68
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([61 x i8], [61 x i8]* @__TypeMapEntry_from.115, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.116, i32 0, i32 0); to
	}, 
	; 69
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([62 x i8], [62 x i8]* @__TypeMapEntry_from.117, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.118, i32 0, i32 0); to
	}, 
	; 70
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([68 x i8], [68 x i8]* @__TypeMapEntry_from.119, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_to.120, i32 0, i32 0); to
	}, 
	; 71
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([75 x i8], [75 x i8]* @__TypeMapEntry_from.121, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_to.120, i32 0, i32 0); to
	}, 
	; 72
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.122, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.123, i32 0, i32 0); to
	}, 
	; 73
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([56 x i8], [56 x i8]* @__TypeMapEntry_from.124, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.123, i32 0, i32 0); to
	}, 
	; 74
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.125, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.126, i32 0, i32 0); to
	}, 
	; 75
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_from.127, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.126, i32 0, i32 0); to
	}, 
	; 76
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.128, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.129, i32 0, i32 0); to
	}, 
	; 77
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.130, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.129, i32 0, i32 0); to
	}, 
	; 78
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.131, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.132, i32 0, i32 0); to
	}, 
	; 79
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.133, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.134, i32 0, i32 0); to
	}, 
	; 80
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.135, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.136, i32 0, i32 0); to
	}, 
	; 81
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([56 x i8], [56 x i8]* @__TypeMapEntry_from.137, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_to.138, i32 0, i32 0); to
	}, 
	; 82
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([63 x i8], [63 x i8]* @__TypeMapEntry_from.139, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_to.138, i32 0, i32 0); to
	}, 
	; 83
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.140, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.136, i32 0, i32 0); to
	}, 
	; 84
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.141, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.142, i32 0, i32 0); to
	}, 
	; 85
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.143, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.142, i32 0, i32 0); to
	}, 
	; 86
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.144, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.145, i32 0, i32 0); to
	}, 
	; 87
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.146, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.145, i32 0, i32 0); to
	}, 
	; 88
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([60 x i8], [60 x i8]* @__TypeMapEntry_from.147, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_to.148, i32 0, i32 0); to
	}, 
	; 89
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([67 x i8], [67 x i8]* @__TypeMapEntry_from.149, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_to.148, i32 0, i32 0); to
	}, 
	; 90
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([61 x i8], [61 x i8]* @__TypeMapEntry_from.150, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.151, i32 0, i32 0); to
	}, 
	; 91
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([68 x i8], [68 x i8]* @__TypeMapEntry_from.152, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.151, i32 0, i32 0); to
	}, 
	; 92
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.153, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.154, i32 0, i32 0); to
	}, 
	; 93
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.155, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.154, i32 0, i32 0); to
	}, 
	; 94
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.156, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.157, i32 0, i32 0); to
	}, 
	; 95
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.158, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.157, i32 0, i32 0); to
	}, 
	; 96
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_from.159, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.160, i32 0, i32 0); to
	}, 
	; 97
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_from.161, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.160, i32 0, i32 0); to
	}, 
	; 98
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.162, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.163, i32 0, i32 0); to
	}, 
	; 99
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.164, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.163, i32 0, i32 0); to
	}, 
	; 100
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.165, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.166, i32 0, i32 0); to
	}, 
	; 101
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_from.167, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.166, i32 0, i32 0); to
	}, 
	; 102
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_from.168, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.169, i32 0, i32 0); to
	}, 
	; 103
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_from.170, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.169, i32 0, i32 0); to
	}, 
	; 104
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.171, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.172, i32 0, i32 0); to
	}, 
	; 105
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_from.173, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.174, i32 0, i32 0); to
	}, 
	; 106
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_from.175, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_to.176, i32 0, i32 0); to
	}, 
	; 107
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([60 x i8], [60 x i8]* @__TypeMapEntry_from.177, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_to.176, i32 0, i32 0); to
	}, 
	; 108
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([59 x i8], [59 x i8]* @__TypeMapEntry_from.178, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.174, i32 0, i32 0); to
	}, 
	; 109
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.179, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.180, i32 0, i32 0); to
	}, 
	; 110
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.181, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.180, i32 0, i32 0); to
	}, 
	; 111
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_from.182, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.183, i32 0, i32 0); to
	}, 
	; 112
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_from.184, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.185, i32 0, i32 0); to
	}, 
	; 113
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.186, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.187, i32 0, i32 0); to
	}, 
	; 114
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([61 x i8], [61 x i8]* @__TypeMapEntry_from.188, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_to.189, i32 0, i32 0); to
	}, 
	; 115
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.190, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.187, i32 0, i32 0); to
	}, 
	; 116
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([62 x i8], [62 x i8]* @__TypeMapEntry_from.191, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.192, i32 0, i32 0); to
	}, 
	; 117
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([69 x i8], [69 x i8]* @__TypeMapEntry_from.193, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.192, i32 0, i32 0); to
	}, 
	; 118
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_from.194, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.195, i32 0, i32 0); to
	}, 
	; 119
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_from.196, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.197, i32 0, i32 0); to
	}, 
	; 120
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.198, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.199, i32 0, i32 0); to
	}, 
	; 121
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.200, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.199, i32 0, i32 0); to
	}, 
	; 122
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([64 x i8], [64 x i8]* @__TypeMapEntry_from.201, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.202, i32 0, i32 0); to
	}, 
	; 123
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([71 x i8], [71 x i8]* @__TypeMapEntry_from.203, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.202, i32 0, i32 0); to
	}, 
	; 124
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.204, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.205, i32 0, i32 0); to
	}, 
	; 125
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.206, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_to.207, i32 0, i32 0); to
	}, 
	; 126
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_from.208, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_to.207, i32 0, i32 0); to
	}, 
	; 127
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.209, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.210, i32 0, i32 0); to
	}, 
	; 128
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_from.211, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.210, i32 0, i32 0); to
	}, 
	; 129
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_from.212, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.213, i32 0, i32 0); to
	}, 
	; 130
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.214, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.215, i32 0, i32 0); to
	}, 
	; 131
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.216, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.217, i32 0, i32 0); to
	}, 
	; 132
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.218, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.219, i32 0, i32 0); to
	}, 
	; 133
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.220, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.221, i32 0, i32 0); to
	}, 
	; 134
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_from.222, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.223, i32 0, i32 0); to
	}, 
	; 135
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_from.224, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.223, i32 0, i32 0); to
	}, 
	; 136
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_from.225, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.226, i32 0, i32 0); to
	}, 
	; 137
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_from.227, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.226, i32 0, i32 0); to
	}, 
	; 138
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.228, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.229, i32 0, i32 0); to
	}, 
	; 139
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.230, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.231, i32 0, i32 0); to
	}, 
	; 140
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.232, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.231, i32 0, i32 0); to
	}, 
	; 141
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.233, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.234, i32 0, i32 0); to
	}, 
	; 142
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.235, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.234, i32 0, i32 0); to
	}, 
	; 143
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.236, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.237, i32 0, i32 0); to
	}, 
	; 144
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_from.238, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.237, i32 0, i32 0); to
	}, 
	; 145
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.239, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.240, i32 0, i32 0); to
	}, 
	; 146
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.241, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.242, i32 0, i32 0); to
	}, 
	; 147
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.243, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.244, i32 0, i32 0); to
	}, 
	; 148
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.245, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.244, i32 0, i32 0); to
	}, 
	; 149
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([55 x i8], [55 x i8]* @__TypeMapEntry_from.246, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.247, i32 0, i32 0); to
	}, 
	; 150
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_from.248, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_to.249, i32 0, i32 0); to
	}, 
	; 151
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_from.250, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_to.249, i32 0, i32 0); to
	}, 
	; 152
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.251, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.252, i32 0, i32 0); to
	}, 
	; 153
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.253, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.254, i32 0, i32 0); to
	}, 
	; 154
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.255, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.256, i32 0, i32 0); to
	}, 
	; 155
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.257, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.258, i32 0, i32 0); to
	}, 
	; 156
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.259, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_to.260, i32 0, i32 0); to
	}, 
	; 157
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_from.261, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.262, i32 0, i32 0); to
	}, 
	; 158
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.263, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.264, i32 0, i32 0); to
	}, 
	; 159
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.265, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.266, i32 0, i32 0); to
	}, 
	; 160
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.267, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.268, i32 0, i32 0); to
	}, 
	; 161
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.269, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.270, i32 0, i32 0); to
	}, 
	; 162
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.271, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.272, i32 0, i32 0); to
	}, 
	; 163
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.273, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.272, i32 0, i32 0); to
	}, 
	; 164
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.274, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.275, i32 0, i32 0); to
	}, 
	; 165
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.276, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.275, i32 0, i32 0); to
	}, 
	; 166
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.277, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.278, i32 0, i32 0); to
	}, 
	; 167
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_from.279, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.278, i32 0, i32 0); to
	}, 
	; 168
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.280, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.281, i32 0, i32 0); to
	}, 
	; 169
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.282, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.281, i32 0, i32 0); to
	}, 
	; 170
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.283, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.284, i32 0, i32 0); to
	}, 
	; 171
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.285, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.284, i32 0, i32 0); to
	}, 
	; 172
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.286, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.287, i32 0, i32 0); to
	}, 
	; 173
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_from.288, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_to.289, i32 0, i32 0); to
	}, 
	; 174
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.290, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.291, i32 0, i32 0); to
	}, 
	; 175
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.292, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.293, i32 0, i32 0); to
	}, 
	; 176
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.294, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.295, i32 0, i32 0); to
	}, 
	; 177
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.296, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.297, i32 0, i32 0); to
	}, 
	; 178
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.298, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.299, i32 0, i32 0); to
	}, 
	; 179
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.300, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.301, i32 0, i32 0); to
	}, 
	; 180
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.302, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.303, i32 0, i32 0); to
	}, 
	; 181
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.304, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.303, i32 0, i32 0); to
	}, 
	; 182
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.305, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.306, i32 0, i32 0); to
	}, 
	; 183
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.307, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.308, i32 0, i32 0); to
	}, 
	; 184
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.309, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.308, i32 0, i32 0); to
	}, 
	; 185
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_from.310, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_to.311, i32 0, i32 0); to
	}, 
	; 186
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([59 x i8], [59 x i8]* @__TypeMapEntry_from.312, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_to.311, i32 0, i32 0); to
	}, 
	; 187
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.313, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.314, i32 0, i32 0); to
	}, 
	; 188
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.315, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.314, i32 0, i32 0); to
	}, 
	; 189
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_from.316, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.317, i32 0, i32 0); to
	}, 
	; 190
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_from.318, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.317, i32 0, i32 0); to
	}, 
	; 191
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_from.319, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.320, i32 0, i32 0); to
	}, 
	; 192
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.321, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.322, i32 0, i32 0); to
	}, 
	; 193
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.323, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.324, i32 0, i32 0); to
	}, 
	; 194
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.325, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.326, i32 0, i32 0); to
	}, 
	; 195
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_from.327, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.328, i32 0, i32 0); to
	}, 
	; 196
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.329, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.330, i32 0, i32 0); to
	}, 
	; 197
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.331, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.332, i32 0, i32 0); to
	}, 
	; 198
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_from.333, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.334, i32 0, i32 0); to
	}, 
	; 199
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.335, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.336, i32 0, i32 0); to
	}, 
	; 200
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.337, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.338, i32 0, i32 0); to
	}, 
	; 201
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.339, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.340, i32 0, i32 0); to
	}, 
	; 202
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.341, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.340, i32 0, i32 0); to
	}, 
	; 203
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.342, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.343, i32 0, i32 0); to
	}, 
	; 204
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.344, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.343, i32 0, i32 0); to
	}, 
	; 205
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_from.345, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([13 x i8], [13 x i8]* @__TypeMapEntry_to.346, i32 0, i32 0); to
	}, 
	; 206
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.347, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.348, i32 0, i32 0); to
	}, 
	; 207
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.349, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.348, i32 0, i32 0); to
	}, 
	; 208
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.350, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.351, i32 0, i32 0); to
	}, 
	; 209
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.352, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.351, i32 0, i32 0); to
	}, 
	; 210
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.353, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.354, i32 0, i32 0); to
	}, 
	; 211
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_from.355, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.354, i32 0, i32 0); to
	}, 
	; 212
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.356, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.357, i32 0, i32 0); to
	}, 
	; 213
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_from.358, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.357, i32 0, i32 0); to
	}, 
	; 214
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.359, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.360, i32 0, i32 0); to
	}, 
	; 215
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.361, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.360, i32 0, i32 0); to
	}, 
	; 216
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_from.362, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.363, i32 0, i32 0); to
	}, 
	; 217
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([61 x i8], [61 x i8]* @__TypeMapEntry_from.364, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.363, i32 0, i32 0); to
	}, 
	; 218
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_from.365, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.366, i32 0, i32 0); to
	}, 
	; 219
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([61 x i8], [61 x i8]* @__TypeMapEntry_from.367, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.366, i32 0, i32 0); to
	}, 
	; 220
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_from.368, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.369, i32 0, i32 0); to
	}, 
	; 221
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([60 x i8], [60 x i8]* @__TypeMapEntry_from.370, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.369, i32 0, i32 0); to
	}, 
	; 222
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([55 x i8], [55 x i8]* @__TypeMapEntry_from.371, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.372, i32 0, i32 0); to
	}, 
	; 223
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([62 x i8], [62 x i8]* @__TypeMapEntry_from.373, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.372, i32 0, i32 0); to
	}, 
	; 224
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_from.374, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.375, i32 0, i32 0); to
	}, 
	; 225
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([60 x i8], [60 x i8]* @__TypeMapEntry_from.376, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.375, i32 0, i32 0); to
	}, 
	; 226
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_from.377, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.378, i32 0, i32 0); to
	}, 
	; 227
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([60 x i8], [60 x i8]* @__TypeMapEntry_from.379, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.378, i32 0, i32 0); to
	}, 
	; 228
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([65 x i8], [65 x i8]* @__TypeMapEntry_from.380, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_to.381, i32 0, i32 0); to
	}, 
	; 229
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([72 x i8], [72 x i8]* @__TypeMapEntry_from.382, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_to.381, i32 0, i32 0); to
	}, 
	; 230
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.383, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.384, i32 0, i32 0); to
	}, 
	; 231
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_from.385, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.386, i32 0, i32 0); to
	}, 
	; 232
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_from.387, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.384, i32 0, i32 0); to
	}, 
	; 233
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.388, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.389, i32 0, i32 0); to
	}, 
	; 234
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([55 x i8], [55 x i8]* @__TypeMapEntry_from.390, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.389, i32 0, i32 0); to
	}, 
	; 235
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.391, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.392, i32 0, i32 0); to
	}, 
	; 236
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([56 x i8], [56 x i8]* @__TypeMapEntry_from.393, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.392, i32 0, i32 0); to
	}, 
	; 237
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.394, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_to.395, i32 0, i32 0); to
	}, 
	; 238
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([64 x i8], [64 x i8]* @__TypeMapEntry_from.396, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_to.395, i32 0, i32 0); to
	}, 
	; 239
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([58 x i8], [58 x i8]* @__TypeMapEntry_from.397, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_to.398, i32 0, i32 0); to
	}, 
	; 240
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([65 x i8], [65 x i8]* @__TypeMapEntry_from.399, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_to.398, i32 0, i32 0); to
	}, 
	; 241
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.400, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.401, i32 0, i32 0); to
	}, 
	; 242
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.402, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.403, i32 0, i32 0); to
	}, 
	; 243
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_from.404, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.403, i32 0, i32 0); to
	}, 
	; 244
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.405, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.406, i32 0, i32 0); to
	}, 
	; 245
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.407, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.406, i32 0, i32 0); to
	}, 
	; 246
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_from.408, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.409, i32 0, i32 0); to
	}, 
	; 247
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.410, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.409, i32 0, i32 0); to
	}, 
	; 248
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_from.411, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.412, i32 0, i32 0); to
	}, 
	; 249
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_from.413, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.412, i32 0, i32 0); to
	}, 
	; 250
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.414, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.415, i32 0, i32 0); to
	}
], align 4; end of 'map_managed_to_java' array


; map_java_to_managed
@map_java_to_managed = internal constant [251 x %struct.TypeMapEntry] [
	; 0
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.3, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.2, i32 0, i32 0); to
	}, 
	; 1
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.5, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.4, i32 0, i32 0); to
	}, 
	; 2
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.14, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 3
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.14, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 4
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.16, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 5
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.16, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 6
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.7, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.6, i32 0, i32 0); to
	}, 
	; 7
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.9, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.8, i32 0, i32 0); to
	}, 
	; 8
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.9, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.8, i32 0, i32 0); to
	}, 
	; 9
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.12, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.11, i32 0, i32 0); to
	}, 
	; 10
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.20, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.19, i32 0, i32 0); to
	}, 
	; 11
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.22, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.21, i32 0, i32 0); to
	}, 
	; 12
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.61, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.60, i32 0, i32 0); to
	}, 
	; 13
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.63, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.62, i32 0, i32 0); to
	}, 
	; 14
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.58, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.57, i32 0, i32 0); to
	}, 
	; 15
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.58, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.57, i32 0, i32 0); to
	}, 
	; 16
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_to.55, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 17
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_to.55, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 18
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.65, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.64, i32 0, i32 0); to
	}, 
	; 19
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.70, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.69, i32 0, i32 0); to
	}, 
	; 20
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.67, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 21
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.67, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 22
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.72, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.71, i32 0, i32 0); to
	}, 
	; 23
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.74, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.73, i32 0, i32 0); to
	}, 
	; 24
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.76, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.75, i32 0, i32 0); to
	}, 
	; 25
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.78, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_from.77, i32 0, i32 0); to
	}, 
	; 26
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.83, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.82, i32 0, i32 0); to
	}, 
	; 27
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.85, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.84, i32 0, i32 0); to
	}, 
	; 28
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.80, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 29
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.80, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 30
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.102, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.101, i32 0, i32 0); to
	}, 
	; 31
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.111, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 32
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.111, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 33
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.109, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_from.108, i32 0, i32 0); to
	}, 
	; 34
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.114, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.113, i32 0, i32 0); to
	}, 
	; 35
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.126, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.125, i32 0, i32 0); to
	}, 
	; 36
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.126, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.125, i32 0, i32 0); to
	}, 
	; 37
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.123, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 38
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.123, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 39
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.129, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.128, i32 0, i32 0); to
	}, 
	; 40
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.129, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.128, i32 0, i32 0); to
	}, 
	; 41
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.136, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 42
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.136, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 43
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_to.138, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 44
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_to.138, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 45
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.132, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.131, i32 0, i32 0); to
	}, 
	; 46
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.134, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.133, i32 0, i32 0); to
	}, 
	; 47
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.166, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.165, i32 0, i32 0); to
	}, 
	; 48
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.166, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.165, i32 0, i32 0); to
	}, 
	; 49
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.172, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.171, i32 0, i32 0); to
	}, 
	; 50
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.169, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 51
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.169, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 52
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.180, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.179, i32 0, i32 0); to
	}, 
	; 53
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__TypeMapEntry_to.180, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.179, i32 0, i32 0); to
	}, 
	; 54
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.174, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 55
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.174, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 56
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_to.176, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 57
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_to.176, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 58
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.142, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 59
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.142, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 60
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.145, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 61
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.145, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 62
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_to.148, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 63
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_to.148, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 64
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.151, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 65
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.151, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 66
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.183, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_from.182, i32 0, i32 0); to
	}, 
	; 67
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.185, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_from.184, i32 0, i32 0); to
	}, 
	; 68
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.154, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 69
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.154, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 70
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.195, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_from.194, i32 0, i32 0); to
	}, 
	; 71
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.187, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 72
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.187, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 73
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.192, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 74
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.192, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 75
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.199, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.198, i32 0, i32 0); to
	}, 
	; 76
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.199, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.198, i32 0, i32 0); to
	}, 
	; 77
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.197, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_from.196, i32 0, i32 0); to
	}, 
	; 78
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.157, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 79
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.157, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 80
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.160, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 81
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.160, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 82
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.205, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.204, i32 0, i32 0); to
	}, 
	; 83
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.202, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 84
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.202, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 85
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.210, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.209, i32 0, i32 0); to
	}, 
	; 86
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.210, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.209, i32 0, i32 0); to
	}, 
	; 87
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_to.207, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 88
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_to.207, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 89
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.163, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 90
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.163, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 91
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.213, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_from.212, i32 0, i32 0); to
	}, 
	; 92
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_to.116, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([61 x i8], [61 x i8]* @__TypeMapEntry_from.115, i32 0, i32 0); to
	}, 
	; 93
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_to.120, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 94
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_to.120, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 95
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.118, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([62 x i8], [62 x i8]* @__TypeMapEntry_from.117, i32 0, i32 0); to
	}, 
	; 96
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__TypeMapEntry_to.215, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_from.214, i32 0, i32 0); to
	}, 
	; 97
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.217, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.216, i32 0, i32 0); to
	}, 
	; 98
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.219, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.218, i32 0, i32 0); to
	}, 
	; 99
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.26, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([70 x i8], [70 x i8]* @__TypeMapEntry_from.25, i32 0, i32 0); to
	}, 
	; 100
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.26, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([70 x i8], [70 x i8]* @__TypeMapEntry_from.25, i32 0, i32 0); to
	}, 
	; 101
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_to.29, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 102
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_to.29, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 103
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.32, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 104
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.32, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 105
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.35, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 106
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_to.35, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 107
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.38, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 108
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.38, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 109
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.41, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 110
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_to.41, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 111
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_to.44, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 112
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_to.44, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 113
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_to.47, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 114
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_to.47, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 115
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.50, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.49, i32 0, i32 0); to
	}, 
	; 116
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.50, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([57 x i8], [57 x i8]* @__TypeMapEntry_from.49, i32 0, i32 0); to
	}, 
	; 117
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_to.52, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([73 x i8], [73 x i8]* @__TypeMapEntry_from.51, i32 0, i32 0); to
	}, 
	; 118
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_to.24, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 119
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.1, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([72 x i8], [72 x i8]* @__TypeMapEntry_from.0, i32 0, i32 0); to
	}, 
	; 120
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.223, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 121
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.223, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 122
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__TypeMapEntry_to.221, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.220, i32 0, i32 0); to
	}, 
	; 123
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.226, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 124
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.226, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 125
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.229, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.228, i32 0, i32 0); to
	}, 
	; 126
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.234, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.233, i32 0, i32 0); to
	}, 
	; 127
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.234, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.233, i32 0, i32 0); to
	}, 
	; 128
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.237, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.236, i32 0, i32 0); to
	}, 
	; 129
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.237, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.236, i32 0, i32 0); to
	}, 
	; 130
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.240, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.239, i32 0, i32 0); to
	}, 
	; 131
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.231, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 132
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.231, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 133
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.242, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_from.241, i32 0, i32 0); to
	}, 
	; 134
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.244, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.243, i32 0, i32 0); to
	}, 
	; 135
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.244, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.243, i32 0, i32 0); to
	}, 
	; 136
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.272, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 137
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.272, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 138
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.252, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.251, i32 0, i32 0); to
	}, 
	; 139
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.254, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.253, i32 0, i32 0); to
	}, 
	; 140
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.275, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 141
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.275, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 142
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.256, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.255, i32 0, i32 0); to
	}, 
	; 143
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.258, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.257, i32 0, i32 0); to
	}, 
	; 144
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_to.260, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_from.259, i32 0, i32 0); to
	}, 
	; 145
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.262, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__TypeMapEntry_from.261, i32 0, i32 0); to
	}, 
	; 146
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.278, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 147
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.278, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 148
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.281, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 149
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.281, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 150
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.264, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.263, i32 0, i32 0); to
	}, 
	; 151
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.266, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.265, i32 0, i32 0); to
	}, 
	; 152
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.268, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.267, i32 0, i32 0); to
	}, 
	; 153
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.270, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.269, i32 0, i32 0); to
	}, 
	; 154
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.287, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.286, i32 0, i32 0); to
	}, 
	; 155
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_to.289, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__TypeMapEntry_from.288, i32 0, i32 0); to
	}, 
	; 156
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__TypeMapEntry_to.291, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.290, i32 0, i32 0); to
	}, 
	; 157
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.293, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_from.292, i32 0, i32 0); to
	}, 
	; 158
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.295, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.294, i32 0, i32 0); to
	}, 
	; 159
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.297, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.296, i32 0, i32 0); to
	}, 
	; 160
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.299, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.298, i32 0, i32 0); to
	}, 
	; 161
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.301, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.300, i32 0, i32 0); to
	}, 
	; 162
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.303, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.302, i32 0, i32 0); to
	}, 
	; 163
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.303, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.302, i32 0, i32 0); to
	}, 
	; 164
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.306, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.305, i32 0, i32 0); to
	}, 
	; 165
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.320, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__TypeMapEntry_from.319, i32 0, i32 0); to
	}, 
	; 166
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.284, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 167
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.284, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 168
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.322, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.321, i32 0, i32 0); to
	}, 
	; 169
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.324, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.323, i32 0, i32 0); to
	}, 
	; 170
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.326, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.325, i32 0, i32 0); to
	}, 
	; 171
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__TypeMapEntry_to.330, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_from.329, i32 0, i32 0); to
	}, 
	; 172
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.332, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.331, i32 0, i32 0); to
	}, 
	; 173
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.334, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__TypeMapEntry_from.333, i32 0, i32 0); to
	}, 
	; 174
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_to.249, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 175
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_to.249, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 176
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.308, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 177
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.308, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 178
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_to.311, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 179
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_to.311, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 180
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.314, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 181
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.314, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 182
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.317, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 183
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.317, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 184
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.336, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.335, i32 0, i32 0); to
	}, 
	; 185
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__TypeMapEntry_to.338, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapEntry_from.337, i32 0, i32 0); to
	}, 
	; 186
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.340, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.339, i32 0, i32 0); to
	}, 
	; 187
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.340, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.339, i32 0, i32 0); to
	}, 
	; 188
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.343, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.342, i32 0, i32 0); to
	}, 
	; 189
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.343, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.342, i32 0, i32 0); to
	}, 
	; 190
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([13 x i8], [13 x i8]* @__TypeMapEntry_to.346, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_from.345, i32 0, i32 0); to
	}, 
	; 191
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.348, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.347, i32 0, i32 0); to
	}, 
	; 192
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__TypeMapEntry_to.348, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_from.347, i32 0, i32 0); to
	}, 
	; 193
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.351, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.350, i32 0, i32 0); to
	}, 
	; 194
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.351, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_from.350, i32 0, i32 0); to
	}, 
	; 195
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.357, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 196
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.357, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 197
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.360, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 198
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__TypeMapEntry_to.360, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 199
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.354, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.353, i32 0, i32 0); to
	}, 
	; 200
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.354, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__TypeMapEntry_from.353, i32 0, i32 0); to
	}, 
	; 201
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.363, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 202
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.363, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 203
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.366, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 204
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_to.366, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 205
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.369, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 206
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.369, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 207
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.372, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 208
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.372, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 209
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.375, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 210
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.375, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 211
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.378, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 212
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.378, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 213
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_to.381, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([65 x i8], [65 x i8]* @__TypeMapEntry_from.380, i32 0, i32 0); to
	}, 
	; 214
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_to.381, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([65 x i8], [65 x i8]* @__TypeMapEntry_from.380, i32 0, i32 0); to
	}, 
	; 215
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__TypeMapEntry_to.401, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__TypeMapEntry_from.400, i32 0, i32 0); to
	}, 
	; 216
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_to.395, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 217
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__TypeMapEntry_to.395, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 218
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_to.398, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 219
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__TypeMapEntry_to.398, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 220
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.384, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.383, i32 0, i32 0); to
	}, 
	; 221
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.384, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.383, i32 0, i32 0); to
	}, 
	; 222
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_to.386, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__TypeMapEntry_from.385, i32 0, i32 0); to
	}, 
	; 223
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.392, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.391, i32 0, i32 0); to
	}, 
	; 224
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.392, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.391, i32 0, i32 0); to
	}, 
	; 225
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.389, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 226
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapEntry_to.389, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 227
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.97, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.96, i32 0, i32 0); to
	}, 
	; 228
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__TypeMapEntry_to.97, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__TypeMapEntry_from.96, i32 0, i32 0); to
	}, 
	; 229
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.91, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.90, i32 0, i32 0); to
	}, 
	; 230
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__TypeMapEntry_to.91, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.90, i32 0, i32 0); to
	}, 
	; 231
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.94, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.93, i32 0, i32 0); to
	}, 
	; 232
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.94, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__TypeMapEntry_from.93, i32 0, i32 0); to
	}, 
	; 233
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.104, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.103, i32 0, i32 0); to
	}, 
	; 234
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__TypeMapEntry_to.104, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__TypeMapEntry_from.103, i32 0, i32 0); to
	}, 
	; 235
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.406, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 236
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__TypeMapEntry_to.406, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 237
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.403, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 238
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__TypeMapEntry_to.403, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 239
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.409, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 240
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__TypeMapEntry_to.409, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 241
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__TypeMapEntry_to.415, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__TypeMapEntry_from.414, i32 0, i32 0); to
	}, 
	; 242
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.412, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 243
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.412, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 244
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__TypeMapEntry_to.247, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([55 x i8], [55 x i8]* @__TypeMapEntry_from.246, i32 0, i32 0); to
	}, 
	; 245
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__TypeMapEntry_to.87, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__TypeMapEntry_from.86, i32 0, i32 0); to
	}, 
	; 246
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__TypeMapEntry_to.89, i32 0, i32 0), ; from
		i8* null; to
	}, 
	; 247
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__TypeMapEntry_to.100, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_from.99, i32 0, i32 0); to
	}, 
	; 248
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__TypeMapEntry_to.107, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_from.106, i32 0, i32 0); to
	}, 
	; 249
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__TypeMapEntry_to.189, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([61 x i8], [61 x i8]* @__TypeMapEntry_from.188, i32 0, i32 0); to
	}, 
	; 250
	%struct.TypeMapEntry {
		i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__TypeMapEntry_to.328, i32 0, i32 0), ; from
		i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__TypeMapEntry_from.327, i32 0, i32 0); to
	}
], align 4; end of 'map_java_to_managed' array


; type_map
@type_map = local_unnamed_addr constant %struct.TypeMap {
	i32 251, ; entry_count
	i8* null, ; assembly_name (unused in this mode)
	i8* null, ; data (unused in this mode)
	%struct.TypeMapEntry* getelementptr inbounds ([251 x %struct.TypeMapEntry], [251 x %struct.TypeMapEntry]* @map_java_to_managed, i32 0, i32 0), ; java_to_managed
	%struct.TypeMapEntry* getelementptr inbounds ([251 x %struct.TypeMapEntry], [251 x %struct.TypeMapEntry]* @map_managed_to_java, i32 0, i32 0); managed_to_java
}, align 4


!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}