; ModuleID = 'typemaps.x86.ll'
source_filename = "typemaps.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.TypeMapJava = type {
	i32, ; uint32_t module_index
	i32, ; uint32_t type_token_id
	i32 ; uint32_t java_name_index
}

%struct.TypeMapModule = type {
	[16 x i8], ; uint8_t module_uuid[16]
	i32, ; uint32_t entry_count
	i32, ; uint32_t duplicate_count
	ptr, ; TypeMapModuleEntry map
	ptr, ; TypeMapModuleEntry duplicate_map
	ptr, ; char* assembly_name
	ptr, ; MonoImage image
	i32, ; uint32_t java_name_width
	ptr ; uint8_t java_map
}

%struct.TypeMapModuleEntry = type {
	i32, ; uint32_t type_token_id
	i32 ; uint32_t java_map_index
}

@map_module_count = dso_local local_unnamed_addr constant i32 3, align 4

@java_type_count = dso_local local_unnamed_addr constant i32 232, align 4

; Managed modules map
@map_modules = dso_local local_unnamed_addr global [3 x %struct.TypeMapModule] [
	%struct.TypeMapModule {
		[16 x i8] c"R\1FV\22\B3\F9\A8O\ABA\C1R_\0B?\0F", ; module_uuid: 22561f52-f9b3-4fa8-ab41-c1525f0b3f0f
		i32 24, ; uint32_t entry_count (0x18)
		i32 0, ; uint32_t duplicate_count (0x0)
		ptr @module0_managed_to_java, ; TypeMapModuleEntry* map
		ptr null, ; TypeMapModuleEntry* duplicate_map
		ptr @.TypeMapModule.0_assembly_name, ; assembly_name: MerchPlus
		ptr null, ; MonoImage* image
		i32 0, ; uint32_t java_name_width (0x0)
		ptr null; uint8_t* java_map (0x0)
	}, ; 0
	%struct.TypeMapModule {
		[16 x i8] c"R\B0'b\17\E8\F1N\96\92f\05Ap\97\F0", ; module_uuid: 6227b052-e817-4ef1-9692-6605417097f0
		i32 8, ; uint32_t entry_count (0x8)
		i32 3, ; uint32_t duplicate_count (0x3)
		ptr @module1_managed_to_java, ; TypeMapModuleEntry* map
		ptr @module1_managed_to_java_duplicates, ; TypeMapModuleEntry* duplicate_map
		ptr @.TypeMapModule.1_assembly_name, ; assembly_name: Xamarin.AndroidX.Core
		ptr null, ; MonoImage* image
		i32 0, ; uint32_t java_name_width (0x0)
		ptr null; uint8_t* java_map (0x0)
	}, ; 1
	%struct.TypeMapModule {
		[16 x i8] c"\D3\B0\ECM\1F6\17F\96x\B7\84\E4o\E3\F5", ; module_uuid: 4decb0d3-361f-4617-9678-b784e46fe3f5
		i32 200, ; uint32_t entry_count (0xc8)
		i32 77, ; uint32_t duplicate_count (0x4d)
		ptr @module2_managed_to_java, ; TypeMapModuleEntry* map
		ptr @module2_managed_to_java_duplicates, ; TypeMapModuleEntry* duplicate_map
		ptr @.TypeMapModule.2_assembly_name, ; assembly_name: Mono.Android
		ptr null, ; MonoImage* image
		i32 0, ; uint32_t java_name_width (0x0)
		ptr null; uint8_t* java_map (0x0)
	} ; 2
], align 4

; Java types name hashes
@map_java_hashes = dso_local local_unnamed_addr constant [232 x i32] [
	i32 9160146, ; 0: 0x8bc5d2 => android/provider/Settings$Secure
	i32 12341354, ; 1: 0xbc506a => java/lang/Object
	i32 29653966, ; 2: 0x1c47bce => android/widget/ListAdapter
	i32 32078366, ; 3: 0x1e97a1e => java/security/cert/Certificate
	i32 74282880, ; 4: 0x46d7780 => android/view/ViewGroup
	i32 138171443, ; 5: 0x83c5433 => javax/net/ssl/SSLSessionContext
	i32 148505617, ; 6: 0x8da0411 => android/text/GetChars
	i32 176697843, ; 7: 0xa8831f3 => java/lang/IllegalArgumentException
	i32 187487404, ; 8: 0xb2cd4ac => crc64298b90ab1d20ae8a/ShopEntryVitrineCameraActivity
	i32 203922170, ; 9: 0xc279afa => crc64298b90ab1d20ae8a/SalesOrderActivity
	i32 249670625, ; 10: 0xee1abe1 => crc64298b90ab1d20ae8a/DatabaseSyncService
	i32 262602588, ; 11: 0xfa6ff5c => android/provider/MediaStore
	i32 269199815, ; 12: 0x100ba9c7 => javax/security/cert/X509Certificate
	i32 271552298, ; 13: 0x102f8f2a => crc646c316a5726d9f3c1/JavaObjectWrapper_1
	i32 279693177, ; 14: 0x10abc779 => android/content/SharedPreferences$Editor
	i32 366534601, ; 15: 0x15d8dfc9 => android/view/ViewGroup$LayoutParams
	i32 393371378, ; 16: 0x17725ef2 => mono/java/lang/RunnableImplementor
	i32 412771173, ; 17: 0x189a6365 => java/lang/Long
	i32 419359493, ; 18: 0x18feeb05 => java/util/Iterator
	i32 420482824, ; 19: 0x19100f08 => java/net/ConnectException
	i32 467220734, ; 20: 0x1bd938fe => android/widget/AbsSpinner
	i32 480538695, ; 21: 0x1ca47047 => androidx/core/content/LocusIdCompat
	i32 501733478, ; 22: 0x1de7d866 => android/view/ViewGroup$MarginLayoutParams
	i32 517668398, ; 23: 0x1edafe2e => android/os/Parcel
	i32 531198748, ; 24: 0x1fa9731c => mono/android/runtime/OutputStreamAdapter
	i32 581097368, ; 25: 0x22a2d798 => java/nio/channels/FileChannel
	i32 584231583, ; 26: 0x22d2aa9f => java/lang/IllegalStateException
	i32 591810476, ; 27: 0x23464fac => android/os/Bundle
	i32 614381788, ; 28: 0x249eb8dc => android/app/TabActivity
	i32 619060219, ; 29: 0x24e61bfb => java/net/URL
	i32 657696663, ; 30: 0x2733a797 => androidx/core/app/SharedElementCallback$OnSharedElementsReadyListener
	i32 673806054, ; 31: 0x282976e6 => mono/android/widget/AdapterView_OnItemSelectedListenerImplementor
	i32 692638611, ; 32: 0x2948d393 => android/app/Service
	i32 692920175, ; 33: 0x294d1f6f => java/util/ArrayList
	i32 715935713, ; 34: 0x2aac4fe1 => android/widget/TabHost$TabSpec
	i32 780408360, ; 35: 0x2e841628 => java/lang/CharSequence
	i32 780987551, ; 36: 0x2e8cec9f => java/io/PrintWriter
	i32 793918146, ; 37: 0x2f523ac2 => java/lang/Integer
	i32 805498755, ; 38: 0x3002ef83 => android/os/IBinder$DeathRecipient
	i32 806800039, ; 39: 0x3016caa7 => java/lang/Thread
	i32 827461610, ; 40: 0x31520fea => android/provider/MediaStore$Images
	i32 838682992, ; 41: 0x31fd4970 => java/lang/NullPointerException
	i32 857541409, ; 42: 0x331d0b21 => crc64298b90ab1d20ae8a/CustomerProductList
	i32 876646173, ; 43: 0x34408f1d => javax/net/ssl/TrustManager
	i32 883855573, ; 44: 0x34ae90d5 => android/provider/MediaStore$Images$Media
	i32 885223365, ; 45: 0x34c36fc5 => android/content/ContentResolver
	i32 893363610, ; 46: 0x353fa59a => java/lang/Short
	i32 899478241, ; 47: 0x359cf2e1 => androidx/core/content/FileProvider
	i32 906034180, ; 48: 0x3600fc04 => android/database/Cursor
	i32 924972967, ; 49: 0x3721f7a7 => androidx/core/app/ActivityCompat$PermissionCompatDelegate
	i32 928674904, ; 50: 0x375a7458 => android/graphics/BitmapFactory
	i32 986059584, ; 51: 0x3ac61340 => androidx/core/content/ContextCompat
	i32 996699600, ; 52: 0x3b686dd0 => java/io/FileDescriptor
	i32 1005032213, ; 53: 0x3be79315 => crc64298b90ab1d20ae8a/ShopCameraActivity_PromotionTypeSpinnerAdapter
	i32 1007618603, ; 54: 0x3c0f0a2b => android/location/Location
	i32 1018791985, ; 55: 0x3cb98831 => android/widget/EditText
	i32 1026507328, ; 56: 0x3d2f4240 => java/net/SocketAddress
	i32 1030707578, ; 57: 0x3d6f597a => android/database/DataSetObserver
	i32 1035992969, ; 58: 0x3dbfff89 => android/content/res/Resources
	i32 1070459310, ; 59: 0x3fcde9ae => android/database/ContentObserver
	i32 1077629184, ; 60: 0x403b5100 => java/util/function/Consumer
	i32 1090939588, ; 61: 0x41066ac4 => javax/net/ssl/KeyManagerFactory
	i32 1102556300, ; 62: 0x41b7ac8c => android/provider/Settings$NameValueTable
	i32 1139090428, ; 63: 0x43e523fc => mono/android/content/DialogInterface_OnMultiChoiceClickListenerImplementor
	i32 1142011573, ; 64: 0x4411b6b5 => java/util/Enumeration
	i32 1196063310, ; 65: 0x474a7a4e => java/lang/Appendable
	i32 1227075600, ; 66: 0x4923b010 => javax/security/cert/Certificate
	i32 1270561450, ; 67: 0x4bbb3aaa => java/net/SocketTimeoutException
	i32 1298454265, ; 68: 0x4d64d6f9 => java/lang/Throwable
	i32 1323697755, ; 69: 0x4ee6065b => javax/net/ssl/SSLContext
	i32 1335098580, ; 70: 0x4f93fcd4 => java/util/Collection
	i32 1368421702, ; 71: 0x51907546 => java/lang/ClassCastException
	i32 1373631042, ; 72: 0x51dff242 => javax/net/ssl/KeyManager
	i32 1386757446, ; 73: 0x52a83d46 => android/content/ComponentName
	i32 1388503527, ; 74: 0x52c2e1e7 => crc646c316a5726d9f3c1/SalesOrderPreviewAdapter
	i32 1424104402, ; 75: 0x54e21bd2 => crc64298b90ab1d20ae8a/DisplayVitrineCameraActivity
	i32 1425790689, ; 76: 0x54fbd6e1 => java/lang/SecurityException
	i32 1428048664, ; 77: 0x551e4b18 => java/net/HttpURLConnection
	i32 1429796945, ; 78: 0x5538f851 => android/graphics/RectF
	i32 1447309214, ; 79: 0x56442f9e => android/widget/LinearLayout$LayoutParams
	i32 1472468295, ; 80: 0x57c41547 => androidx/core/app/ActivityCompat
	i32 1475682991, ; 81: 0x57f522af => java/util/HashMap
	i32 1476293262, ; 82: 0x57fe728e => javax/security/auth/Subject
	i32 1489594546, ; 83: 0x58c968b2 => java/nio/channels/spi/AbstractInterruptibleChannel
	i32 1492815417, ; 84: 0x58fa8e39 => java/util/concurrent/Executor
	i32 1506774891, ; 85: 0x59cf8f6b => android/widget/Button
	i32 1515270195, ; 86: 0x5a513033 => android/app/ActivityGroup
	i32 1544613420, ; 87: 0x5c10ee2c => java/io/File
	i32 1550531333, ; 88: 0x5c6b3b05 => android/content/ContentProvider
	i32 1573833883, ; 89: 0x5dcecc9b => android/app/AlertDialog
	i32 1584672329, ; 90: 0x5e742e49 => android/view/Display
	i32 1586851388, ; 91: 0x5e956e3c => android/os/Handler
	i32 1637959351, ; 92: 0x61a146b7 => java/security/Principal
	i32 1644876130, ; 93: 0x620ad162 => android/graphics/Matrix
	i32 1646348278, ; 94: 0x622147f6 => android/view/View
	i32 1649695927, ; 95: 0x62545cb7 => java/lang/RuntimeException
	i32 1657134862, ; 96: 0x62c5df0e => java/lang/IndexOutOfBoundsException
	i32 1680835779, ; 97: 0x642f84c3 => java/lang/Byte
	i32 1718265030, ; 98: 0x666aa4c6 => java/lang/Character
	i32 1740814247, ; 99: 0x67c2b7a7 => android/widget/FrameLayout
	i32 1740929322, ; 100: 0x67c4792a => android/os/IInterface
	i32 1755285137, ; 101: 0x689f8691 => java/util/Random
	i32 1758490869, ; 102: 0x68d070f5 => android/os/BaseBundle
	i32 1775355160, ; 103: 0x69d1c518 => android/content/res/ColorStateList
	i32 1790236887, ; 104: 0x6ab4d8d7 => android/text/Spanned
	i32 1807220671, ; 105: 0x6bb7ffbf => android/view/View$OnClickListener
	i32 1851730788, ; 106: 0x6e5f2b64 => java/lang/Runnable
	i32 1855628473, ; 107: 0x6e9aa4b9 => mono/android/text/TextWatcherImplementor
	i32 1859010077, ; 108: 0x6ece3e1d => android/widget/LinearLayout
	i32 1869790738, ; 109: 0x6f72be12 => android/location/Criteria
	i32 1884841200, ; 110: 0x705864f0 => android/os/PowerManager
	i32 1884960853, ; 111: 0x705a3855 => android/content/DialogInterface$OnMultiChoiceClickListener
	i32 1943778051, ; 112: 0x73dbb303 => android/widget/AdapterView$OnItemSelectedListener
	i32 1944129628, ; 113: 0x73e1105c => java/io/OutputStream
	i32 1962126435, ; 114: 0x74f3ac63 => android/content/BroadcastReceiver
	i32 1971567327, ; 115: 0x7583badf => crc64298b90ab1d20ae8a/LocationAgentService
	i32 1985929388, ; 116: 0x765ee0ac => android/app/Activity
	i32 1987841337, ; 117: 0x767c0d39 => java/lang/Boolean
	i32 1990610968, ; 118: 0x76a65018 => android/widget/AdapterView$OnItemClickListener
	i32 2008064836, ; 119: 0x77b0a344 => android/content/Intent
	i32 2027782872, ; 120: 0x78dd82d8 => android/view/ContextThemeWrapper
	i32 2031450615, ; 121: 0x791579f7 => android/widget/AdapterView
	i32 2036556174, ; 122: 0x7963618e => android/content/DialogInterface
	i32 2061721420, ; 123: 0x7ae35f4c => android/database/CharArrayBuffer
	i32 2064723667, ; 124: 0x7b112ed3 => android/widget/SpinnerAdapter
	i32 2073337312, ; 125: 0x7b949de0 => android/app/AlertDialog$Builder
	i32 2079753938, ; 126: 0x7bf686d2 => android/content/IntentSender
	i32 2080685156, ; 127: 0x7c04bc64 => java/security/SecureRandom
	i32 2090823071, ; 128: 0x7c9f6d9f => android/os/Environment
	i32 2123880745, ; 129: 0x7e97d929 => android/content/ContentValues
	i32 2267531675, ; 130: 0x8727c99b => crc646c316a5726d9f3c1/PhotoListAdapter
	i32 2269094561, ; 131: 0x873fa2a1 => java/net/UnknownServiceException
	i32 2270923754, ; 132: 0x875b8bea => java/net/Proxy$Type
	i32 2284656609, ; 133: 0x882d17e1 => android/app/Application
	i32 2296411383, ; 134: 0x88e074f7 => android/content/IntentFilter
	i32 2363729366, ; 135: 0x8ce3a5d6 => java/lang/Enum
	i32 2411404453, ; 136: 0x8fbb1ca5 => java/lang/UnsupportedOperationException
	i32 2443438835, ; 137: 0x91a3eaf3 => java/net/SocketException
	i32 2458007569, ; 138: 0x92823811 => android/os/Process
	i32 2509871678, ; 139: 0x95999a3e => crc64298b90ab1d20ae8a/CustomerProductSpinnerAdapter
	i32 2532846927, ; 140: 0x96f82d4f => android/content/SharedPreferences$OnSharedPreferenceChangeListener
	i32 2558143838, ; 141: 0x987a2d5e => java/io/FileInputStream
	i32 2594241228, ; 142: 0x9aa0facc => android/widget/BaseAdapter
	i32 2642928435, ; 143: 0x9d87e333 => crc64298b90ab1d20ae8a/AboutMeActivity
	i32 2654672461, ; 144: 0x9e3b164d => java/io/InterruptedIOException
	i32 2661939407, ; 145: 0x9ea9f8cf => android/widget/ImageButton
	i32 2664928003, ; 146: 0x9ed79303 => javax/net/ssl/HostnameVerifier
	i32 2681209703, ; 147: 0x9fd00367 => android/widget/Adapter
	i32 2687778660, ; 148: 0xa0343f64 => android/widget/TextView
	i32 2692535938, ; 149: 0xa07cd682 => android/util/Log
	i32 2708965253, ; 150: 0xa1778785 => crc646c316a5726d9f3c1/TodaysRouteAdapter
	i32 2721599187, ; 151: 0xa2384ed3 => android/graphics/drawable/Drawable
	i32 2755748727, ; 152: 0xa4416377 => android/text/Spannable
	i32 2762684487, ; 153: 0xa4ab3847 => java/lang/Float
	i32 2767414743, ; 154: 0xa4f365d7 => android/content/LocusId
	i32 2815615939, ; 155: 0xa7d2e3c3 => android/os/Build
	i32 2837435745, ; 156: 0xa91fd561 => android/view/DragEvent
	i32 2874673969, ; 157: 0xab580b31 => java/lang/StackTraceElement
	i32 2890481465, ; 158: 0xac493f39 => crc64298b90ab1d20ae8a/ExitShopActivity
	i32 2918613155, ; 159: 0xadf680a3 => android/content/DialogInterface$OnClickListener
	i32 2933762856, ; 160: 0xaeddab28 => android/util/AttributeSet
	i32 2942792700, ; 161: 0xaf6773fc => java/lang/Exception
	i32 2944806563, ; 162: 0xaf862ea3 => android/widget/ListView
	i32 2983720033, ; 163: 0xb1d7f461 => mono/android/TypeManager
	i32 2983793634, ; 164: 0xb1d913e2 => android/widget/Spinner
	i32 3005220318, ; 165: 0xb32005de => crc646c316a5726d9f3c1/ProductExpirationPreviewAdapter
	i32 3032808825, ; 166: 0xb4c4fd79 => java/io/StringWriter
	i32 3085665645, ; 167: 0xb7eb856d => crc64298b90ab1d20ae8a/ShopEntryCameraActivity
	i32 3173395525, ; 168: 0xbd262c45 => android/os/IBinder
	i32 3173427379, ; 169: 0xbd26a8b3 => android/app/DownloadManager
	i32 3178304415, ; 170: 0xbd71139f => android/view/inputmethod/InputMethodManager
	i32 3199337905, ; 171: 0xbeb205b1 => crc646c316a5726d9f3c1/RetailShopProductListAdapter
	i32 3268587150, ; 172: 0xc2d2ae8e => android/location/LocationListener
	i32 3300906352, ; 173: 0xc4bfd570 => javax/net/ssl/SSLSession
	i32 3319735188, ; 174: 0xc5df2394 => java/net/Proxy
	i32 3379688415, ; 175: 0xc971f3df => android/text/Editable
	i32 3397817114, ; 176: 0xca86931a => android/widget/ArrayAdapter
	i32 3409419575, ; 177: 0xcb379d37 => javax/net/ssl/HttpsURLConnection
	i32 3423467887, ; 178: 0xcc0df96f => java/lang/Number
	i32 3430868172, ; 179: 0xcc7ee4cc => android/content/SharedPreferences
	i32 3519931621, ; 180: 0xd1cde4e5 => java/net/URLConnection
	i32 3549151004, ; 181: 0xd38bbf1c => android/provider/Settings
	i32 3557300893, ; 182: 0xd4081a9d => crc64298b90ab1d20ae8a/ShopCameraActivity
	i32 3576242387, ; 183: 0xd52920d3 => android/runtime/JavaProxyThrowable
	i32 3597654493, ; 184: 0xd66fd9dd => android/widget/AbsListView
	i32 3608980748, ; 185: 0xd71cad0c => android/os/PowerManager$WakeLock
	i32 3651817504, ; 186: 0xd9aa5020 => android/widget/TabHost
	i32 3665774669, ; 187: 0xda7f484d => android/view/LayoutInflater
	i32 3666243682, ; 188: 0xda867062 => java/lang/String
	i32 3669061717, ; 189: 0xdab17055 => java/net/InetSocketAddress
	i32 3681695975, ; 190: 0xdb7238e7 => android/telephony/TelephonyManager
	i32 3683323802, ; 191: 0xdb8b0f9a => mono/android/runtime/JavaObject
	i32 3702230909, ; 192: 0xdcab8f7d => java/lang/Double
	i32 3715861037, ; 193: 0xdd7b8a2d => android/os/Build$VERSION
	i32 3721088312, ; 194: 0xddcb4d38 => android/text/NoCopySpan
	i32 3722843854, ; 195: 0xdde616ce => javax/net/SocketFactory
	i32 3726680736, ; 196: 0xde20a2a0 => java/net/ProtocolException
	i32 3759929762, ; 197: 0xe01bf9a2 => android/graphics/Bitmap
	i32 3823421666, ; 198: 0xe3e4c8e2 => android/net/Uri
	i32 3823436350, ; 199: 0xe3e5023e => crc64298b90ab1d20ae8a/MainActivity
	i32 3828108282, ; 200: 0xe42c4bfa => android/widget/TextView$BufferType
	i32 3882570516, ; 201: 0xe76b5314 => java/lang/Class
	i32 3895425567, ; 202: 0xe82f7a1f => androidx/core/app/SharedElementCallback
	i32 3896288302, ; 203: 0xe83ca42e => android/widget/ImageView
	i32 3900581163, ; 204: 0xe87e252b => java/io/InputStream
	i32 3901837667, ; 205: 0xe8915163 => android/text/InputFilter
	i32 3912451735, ; 206: 0xe9334697 => java/security/KeyStore
	i32 3931120197, ; 207: 0xea502245 => mono/android/content/DialogInterface_OnClickListenerImplementor
	i32 3969984744, ; 208: 0xeca128e8 => mono/android/runtime/InputStreamAdapter
	i32 3975001277, ; 209: 0xecedb4bd => javax/net/ssl/SSLSocketFactory
	i32 3993327007, ; 210: 0xee05559f => android/content/ContextWrapper
	i32 4001554463, ; 211: 0xee82e01f => crc64298b90ab1d20ae8a/TodaysRoute
	i32 4018969757, ; 212: 0xef8c9c9d => crc64298b90ab1d20ae8a/MyBootReceiver
	i32 4020308495, ; 213: 0xefa10a0f => java/lang/Error
	i32 4026153166, ; 214: 0xeffa38ce => androidx/core/view/DragAndDropPermissionsCompat
	i32 4030673356, ; 215: 0xf03f31cc => android/app/Dialog
	i32 4051772911, ; 216: 0xf18125ef => android/content/Context
	i32 4098107575, ; 217: 0xf44428b7 => mono/android/view/View_OnClickListenerImplementor
	i32 4101363546, ; 218: 0xf475d75a => java/io/Writer
	i32 4118878202, ; 219: 0xf58117fa => android/os/Looper
	i32 4126711388, ; 220: 0xf5f89e5c => crc64298b90ab1d20ae8a/ExpirationDateActivity
	i32 4127266501, ; 221: 0xf60116c5 => mono/android/widget/AdapterView_OnItemClickListenerImplementor
	i32 4127569708, ; 222: 0xf605b72c => crc64298b90ab1d20ae8a/MainMenu
	i32 4148593869, ; 223: 0xf74684cd => javax/net/ssl/TrustManagerFactory
	i32 4157808693, ; 224: 0xf7d32035 => java/io/IOException
	i32 4166165970, ; 225: 0xf852a5d2 => android/text/TextWatcher
	i32 4203502565, ; 226: 0xfa8c5be5 => android/graphics/Bitmap$CompressFormat
	i32 4232707919, ; 227: 0xfc49ff4f => java/util/HashSet
	i32 4236724582, ; 228: 0xfc874966 => android/os/Parcelable
	i32 4268805981, ; 229: 0xfe70cf5d => android/location/LocationManager
	i32 4277523103, ; 230: 0xfef5d29f => java/io/Closeable
	i32 4284878527 ; 231: 0xff660ebf => crc64298b90ab1d20ae8a/MerchDataEntryActivity
], align 4

@module0_managed_to_java = internal dso_local constant [24 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554434, ; uint32_t type_token_id (0x2000002)
		i32 143; uint32_t java_map_index (0x8f)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554435, ; uint32_t type_token_id (0x2000003)
		i32 115; uint32_t java_map_index (0x73)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554436, ; uint32_t type_token_id (0x2000004)
		i32 10; uint32_t java_map_index (0xa)
	}, ; 2
	%struct.TypeMapModuleEntry {
		i32 33554438, ; uint32_t type_token_id (0x2000006)
		i32 42; uint32_t java_map_index (0x2a)
	}, ; 3
	%struct.TypeMapModuleEntry {
		i32 33554439, ; uint32_t type_token_id (0x2000007)
		i32 75; uint32_t java_map_index (0x4b)
	}, ; 4
	%struct.TypeMapModuleEntry {
		i32 33554440, ; uint32_t type_token_id (0x2000008)
		i32 158; uint32_t java_map_index (0x9e)
	}, ; 5
	%struct.TypeMapModuleEntry {
		i32 33554441, ; uint32_t type_token_id (0x2000009)
		i32 220; uint32_t java_map_index (0xdc)
	}, ; 6
	%struct.TypeMapModuleEntry {
		i32 33554446, ; uint32_t type_token_id (0x200000e)
		i32 199; uint32_t java_map_index (0xc7)
	}, ; 7
	%struct.TypeMapModuleEntry {
		i32 33554447, ; uint32_t type_token_id (0x200000f)
		i32 212; uint32_t java_map_index (0xd4)
	}, ; 8
	%struct.TypeMapModuleEntry {
		i32 33554448, ; uint32_t type_token_id (0x2000010)
		i32 222; uint32_t java_map_index (0xde)
	}, ; 9
	%struct.TypeMapModuleEntry {
		i32 33554449, ; uint32_t type_token_id (0x2000011)
		i32 231; uint32_t java_map_index (0xe7)
	}, ; 10
	%struct.TypeMapModuleEntry {
		i32 33554452, ; uint32_t type_token_id (0x2000014)
		i32 9; uint32_t java_map_index (0x9)
	}, ; 11
	%struct.TypeMapModuleEntry {
		i32 33554453, ; uint32_t type_token_id (0x2000015)
		i32 139; uint32_t java_map_index (0x8b)
	}, ; 12
	%struct.TypeMapModuleEntry {
		i32 33554454, ; uint32_t type_token_id (0x2000016)
		i32 182; uint32_t java_map_index (0xb6)
	}, ; 13
	%struct.TypeMapModuleEntry {
		i32 33554455, ; uint32_t type_token_id (0x2000017)
		i32 167; uint32_t java_map_index (0xa7)
	}, ; 14
	%struct.TypeMapModuleEntry {
		i32 33554456, ; uint32_t type_token_id (0x2000018)
		i32 8; uint32_t java_map_index (0x8)
	}, ; 15
	%struct.TypeMapModuleEntry {
		i32 33554457, ; uint32_t type_token_id (0x2000019)
		i32 211; uint32_t java_map_index (0xd3)
	}, ; 16
	%struct.TypeMapModuleEntry {
		i32 33554465, ; uint32_t type_token_id (0x2000021)
		i32 13; uint32_t java_map_index (0xd)
	}, ; 17
	%struct.TypeMapModuleEntry {
		i32 33554466, ; uint32_t type_token_id (0x2000022)
		i32 130; uint32_t java_map_index (0x82)
	}, ; 18
	%struct.TypeMapModuleEntry {
		i32 33554467, ; uint32_t type_token_id (0x2000023)
		i32 165; uint32_t java_map_index (0xa5)
	}, ; 19
	%struct.TypeMapModuleEntry {
		i32 33554468, ; uint32_t type_token_id (0x2000024)
		i32 171; uint32_t java_map_index (0xab)
	}, ; 20
	%struct.TypeMapModuleEntry {
		i32 33554469, ; uint32_t type_token_id (0x2000025)
		i32 74; uint32_t java_map_index (0x4a)
	}, ; 21
	%struct.TypeMapModuleEntry {
		i32 33554470, ; uint32_t type_token_id (0x2000026)
		i32 150; uint32_t java_map_index (0x96)
	}, ; 22
	%struct.TypeMapModuleEntry {
		i32 33554486, ; uint32_t type_token_id (0x2000036)
		i32 53; uint32_t java_map_index (0x35)
	} ; 23
], align 4

@module1_managed_to_java = internal dso_local constant [8 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554447, ; uint32_t type_token_id (0x200000f)
		i32 51; uint32_t java_map_index (0x33)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554448, ; uint32_t type_token_id (0x2000010)
		i32 47; uint32_t java_map_index (0x2f)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554449, ; uint32_t type_token_id (0x2000011)
		i32 21; uint32_t java_map_index (0x15)
	}, ; 2
	%struct.TypeMapModuleEntry {
		i32 33554450, ; uint32_t type_token_id (0x2000012)
		i32 80; uint32_t java_map_index (0x50)
	}, ; 3
	%struct.TypeMapModuleEntry {
		i32 33554451, ; uint32_t type_token_id (0x2000013)
		i32 49; uint32_t java_map_index (0x31)
	}, ; 4
	%struct.TypeMapModuleEntry {
		i32 33554453, ; uint32_t type_token_id (0x2000015)
		i32 202; uint32_t java_map_index (0xca)
	}, ; 5
	%struct.TypeMapModuleEntry {
		i32 33554454, ; uint32_t type_token_id (0x2000016)
		i32 30; uint32_t java_map_index (0x1e)
	}, ; 6
	%struct.TypeMapModuleEntry {
		i32 33554457, ; uint32_t type_token_id (0x2000019)
		i32 214; uint32_t java_map_index (0xd6)
	} ; 7
], align 4

@module1_managed_to_java_duplicates = internal dso_local constant [3 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554452, ; uint32_t type_token_id (0x2000014)
		i32 49; uint32_t java_map_index (0x31)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554455, ; uint32_t type_token_id (0x2000017)
		i32 30; uint32_t java_map_index (0x1e)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554456, ; uint32_t type_token_id (0x2000018)
		i32 202; uint32_t java_map_index (0xca)
	} ; 2
], align 4

@module2_managed_to_java = internal dso_local constant [200 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554552, ; uint32_t type_token_id (0x2000078)
		i32 66; uint32_t java_map_index (0x42)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554554, ; uint32_t type_token_id (0x200007a)
		i32 12; uint32_t java_map_index (0xc)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554556, ; uint32_t type_token_id (0x200007c)
		i32 82; uint32_t java_map_index (0x52)
	}, ; 2
	%struct.TypeMapModuleEntry {
		i32 33554557, ; uint32_t type_token_id (0x200007d)
		i32 195; uint32_t java_map_index (0xc3)
	}, ; 3
	%struct.TypeMapModuleEntry {
		i32 33554559, ; uint32_t type_token_id (0x200007f)
		i32 177; uint32_t java_map_index (0xb1)
	}, ; 4
	%struct.TypeMapModuleEntry {
		i32 33554561, ; uint32_t type_token_id (0x2000081)
		i32 146; uint32_t java_map_index (0x92)
	}, ; 5
	%struct.TypeMapModuleEntry {
		i32 33554563, ; uint32_t type_token_id (0x2000083)
		i32 72; uint32_t java_map_index (0x48)
	}, ; 6
	%struct.TypeMapModuleEntry {
		i32 33554565, ; uint32_t type_token_id (0x2000085)
		i32 173; uint32_t java_map_index (0xad)
	}, ; 7
	%struct.TypeMapModuleEntry {
		i32 33554567, ; uint32_t type_token_id (0x2000087)
		i32 5; uint32_t java_map_index (0x5)
	}, ; 8
	%struct.TypeMapModuleEntry {
		i32 33554569, ; uint32_t type_token_id (0x2000089)
		i32 43; uint32_t java_map_index (0x2b)
	}, ; 9
	%struct.TypeMapModuleEntry {
		i32 33554571, ; uint32_t type_token_id (0x200008b)
		i32 61; uint32_t java_map_index (0x3d)
	}, ; 10
	%struct.TypeMapModuleEntry {
		i32 33554572, ; uint32_t type_token_id (0x200008c)
		i32 69; uint32_t java_map_index (0x45)
	}, ; 11
	%struct.TypeMapModuleEntry {
		i32 33554573, ; uint32_t type_token_id (0x200008d)
		i32 209; uint32_t java_map_index (0xd1)
	}, ; 12
	%struct.TypeMapModuleEntry {
		i32 33554575, ; uint32_t type_token_id (0x200008f)
		i32 223; uint32_t java_map_index (0xdf)
	}, ; 13
	%struct.TypeMapModuleEntry {
		i32 33554576, ; uint32_t type_token_id (0x2000090)
		i32 184; uint32_t java_map_index (0xb8)
	}, ; 14
	%struct.TypeMapModuleEntry {
		i32 33554579, ; uint32_t type_token_id (0x2000093)
		i32 20; uint32_t java_map_index (0x14)
	}, ; 15
	%struct.TypeMapModuleEntry {
		i32 33554581, ; uint32_t type_token_id (0x2000095)
		i32 121; uint32_t java_map_index (0x79)
	}, ; 16
	%struct.TypeMapModuleEntry {
		i32 33554582, ; uint32_t type_token_id (0x2000096)
		i32 118; uint32_t java_map_index (0x76)
	}, ; 17
	%struct.TypeMapModuleEntry {
		i32 33554585, ; uint32_t type_token_id (0x2000099)
		i32 221; uint32_t java_map_index (0xdd)
	}, ; 18
	%struct.TypeMapModuleEntry {
		i32 33554586, ; uint32_t type_token_id (0x200009a)
		i32 112; uint32_t java_map_index (0x70)
	}, ; 19
	%struct.TypeMapModuleEntry {
		i32 33554590, ; uint32_t type_token_id (0x200009e)
		i32 31; uint32_t java_map_index (0x1f)
	}, ; 20
	%struct.TypeMapModuleEntry {
		i32 33554597, ; uint32_t type_token_id (0x20000a5)
		i32 176; uint32_t java_map_index (0xb0)
	}, ; 21
	%struct.TypeMapModuleEntry {
		i32 33554598, ; uint32_t type_token_id (0x20000a6)
		i32 142; uint32_t java_map_index (0x8e)
	}, ; 22
	%struct.TypeMapModuleEntry {
		i32 33554600, ; uint32_t type_token_id (0x20000a8)
		i32 85; uint32_t java_map_index (0x55)
	}, ; 23
	%struct.TypeMapModuleEntry {
		i32 33554601, ; uint32_t type_token_id (0x20000a9)
		i32 55; uint32_t java_map_index (0x37)
	}, ; 24
	%struct.TypeMapModuleEntry {
		i32 33554602, ; uint32_t type_token_id (0x20000aa)
		i32 99; uint32_t java_map_index (0x63)
	}, ; 25
	%struct.TypeMapModuleEntry {
		i32 33554603, ; uint32_t type_token_id (0x20000ab)
		i32 147; uint32_t java_map_index (0x93)
	}, ; 26
	%struct.TypeMapModuleEntry {
		i32 33554605, ; uint32_t type_token_id (0x20000ad)
		i32 2; uint32_t java_map_index (0x2)
	}, ; 27
	%struct.TypeMapModuleEntry {
		i32 33554607, ; uint32_t type_token_id (0x20000af)
		i32 145; uint32_t java_map_index (0x91)
	}, ; 28
	%struct.TypeMapModuleEntry {
		i32 33554608, ; uint32_t type_token_id (0x20000b0)
		i32 203; uint32_t java_map_index (0xcb)
	}, ; 29
	%struct.TypeMapModuleEntry {
		i32 33554609, ; uint32_t type_token_id (0x20000b1)
		i32 124; uint32_t java_map_index (0x7c)
	}, ; 30
	%struct.TypeMapModuleEntry {
		i32 33554611, ; uint32_t type_token_id (0x20000b3)
		i32 108; uint32_t java_map_index (0x6c)
	}, ; 31
	%struct.TypeMapModuleEntry {
		i32 33554612, ; uint32_t type_token_id (0x20000b4)
		i32 79; uint32_t java_map_index (0x4f)
	}, ; 32
	%struct.TypeMapModuleEntry {
		i32 33554613, ; uint32_t type_token_id (0x20000b5)
		i32 162; uint32_t java_map_index (0xa2)
	}, ; 33
	%struct.TypeMapModuleEntry {
		i32 33554614, ; uint32_t type_token_id (0x20000b6)
		i32 164; uint32_t java_map_index (0xa4)
	}, ; 34
	%struct.TypeMapModuleEntry {
		i32 33554615, ; uint32_t type_token_id (0x20000b7)
		i32 186; uint32_t java_map_index (0xba)
	}, ; 35
	%struct.TypeMapModuleEntry {
		i32 33554616, ; uint32_t type_token_id (0x20000b8)
		i32 34; uint32_t java_map_index (0x22)
	}, ; 36
	%struct.TypeMapModuleEntry {
		i32 33554617, ; uint32_t type_token_id (0x20000b9)
		i32 148; uint32_t java_map_index (0x94)
	}, ; 37
	%struct.TypeMapModuleEntry {
		i32 33554618, ; uint32_t type_token_id (0x20000ba)
		i32 200; uint32_t java_map_index (0xc8)
	}, ; 38
	%struct.TypeMapModuleEntry {
		i32 33554621, ; uint32_t type_token_id (0x20000bd)
		i32 160; uint32_t java_map_index (0xa0)
	}, ; 39
	%struct.TypeMapModuleEntry {
		i32 33554623, ; uint32_t type_token_id (0x20000bf)
		i32 149; uint32_t java_map_index (0x95)
	}, ; 40
	%struct.TypeMapModuleEntry {
		i32 33554624, ; uint32_t type_token_id (0x20000c0)
		i32 175; uint32_t java_map_index (0xaf)
	}, ; 41
	%struct.TypeMapModuleEntry {
		i32 33554627, ; uint32_t type_token_id (0x20000c3)
		i32 6; uint32_t java_map_index (0x6)
	}, ; 42
	%struct.TypeMapModuleEntry {
		i32 33554630, ; uint32_t type_token_id (0x20000c6)
		i32 205; uint32_t java_map_index (0xcd)
	}, ; 43
	%struct.TypeMapModuleEntry {
		i32 33554632, ; uint32_t type_token_id (0x20000c8)
		i32 194; uint32_t java_map_index (0xc2)
	}, ; 44
	%struct.TypeMapModuleEntry {
		i32 33554634, ; uint32_t type_token_id (0x20000ca)
		i32 152; uint32_t java_map_index (0x98)
	}, ; 45
	%struct.TypeMapModuleEntry {
		i32 33554637, ; uint32_t type_token_id (0x20000cd)
		i32 104; uint32_t java_map_index (0x68)
	}, ; 46
	%struct.TypeMapModuleEntry {
		i32 33554640, ; uint32_t type_token_id (0x20000d0)
		i32 225; uint32_t java_map_index (0xe1)
	}, ; 47
	%struct.TypeMapModuleEntry {
		i32 33554645, ; uint32_t type_token_id (0x20000d5)
		i32 107; uint32_t java_map_index (0x6b)
	}, ; 48
	%struct.TypeMapModuleEntry {
		i32 33554646, ; uint32_t type_token_id (0x20000d6)
		i32 190; uint32_t java_map_index (0xbe)
	}, ; 49
	%struct.TypeMapModuleEntry {
		i32 33554647, ; uint32_t type_token_id (0x20000d7)
		i32 11; uint32_t java_map_index (0xb)
	}, ; 50
	%struct.TypeMapModuleEntry {
		i32 33554648, ; uint32_t type_token_id (0x20000d8)
		i32 40; uint32_t java_map_index (0x28)
	}, ; 51
	%struct.TypeMapModuleEntry {
		i32 33554649, ; uint32_t type_token_id (0x20000d9)
		i32 44; uint32_t java_map_index (0x2c)
	}, ; 52
	%struct.TypeMapModuleEntry {
		i32 33554650, ; uint32_t type_token_id (0x20000da)
		i32 181; uint32_t java_map_index (0xb5)
	}, ; 53
	%struct.TypeMapModuleEntry {
		i32 33554651, ; uint32_t type_token_id (0x20000db)
		i32 62; uint32_t java_map_index (0x3e)
	}, ; 54
	%struct.TypeMapModuleEntry {
		i32 33554652, ; uint32_t type_token_id (0x20000dc)
		i32 0; uint32_t java_map_index (0x0)
	}, ; 55
	%struct.TypeMapModuleEntry {
		i32 33554653, ; uint32_t type_token_id (0x20000dd)
		i32 102; uint32_t java_map_index (0x66)
	}, ; 56
	%struct.TypeMapModuleEntry {
		i32 33554654, ; uint32_t type_token_id (0x20000de)
		i32 155; uint32_t java_map_index (0x9b)
	}, ; 57
	%struct.TypeMapModuleEntry {
		i32 33554655, ; uint32_t type_token_id (0x20000df)
		i32 193; uint32_t java_map_index (0xc1)
	}, ; 58
	%struct.TypeMapModuleEntry {
		i32 33554656, ; uint32_t type_token_id (0x20000e0)
		i32 27; uint32_t java_map_index (0x1b)
	}, ; 59
	%struct.TypeMapModuleEntry {
		i32 33554657, ; uint32_t type_token_id (0x20000e1)
		i32 128; uint32_t java_map_index (0x80)
	}, ; 60
	%struct.TypeMapModuleEntry {
		i32 33554658, ; uint32_t type_token_id (0x20000e2)
		i32 91; uint32_t java_map_index (0x5b)
	}, ; 61
	%struct.TypeMapModuleEntry {
		i32 33554659, ; uint32_t type_token_id (0x20000e3)
		i32 38; uint32_t java_map_index (0x26)
	}, ; 62
	%struct.TypeMapModuleEntry {
		i32 33554661, ; uint32_t type_token_id (0x20000e5)
		i32 168; uint32_t java_map_index (0xa8)
	}, ; 63
	%struct.TypeMapModuleEntry {
		i32 33554663, ; uint32_t type_token_id (0x20000e7)
		i32 100; uint32_t java_map_index (0x64)
	}, ; 64
	%struct.TypeMapModuleEntry {
		i32 33554665, ; uint32_t type_token_id (0x20000e9)
		i32 228; uint32_t java_map_index (0xe4)
	}, ; 65
	%struct.TypeMapModuleEntry {
		i32 33554667, ; uint32_t type_token_id (0x20000eb)
		i32 219; uint32_t java_map_index (0xdb)
	}, ; 66
	%struct.TypeMapModuleEntry {
		i32 33554668, ; uint32_t type_token_id (0x20000ec)
		i32 23; uint32_t java_map_index (0x17)
	}, ; 67
	%struct.TypeMapModuleEntry {
		i32 33554669, ; uint32_t type_token_id (0x20000ed)
		i32 110; uint32_t java_map_index (0x6e)
	}, ; 68
	%struct.TypeMapModuleEntry {
		i32 33554670, ; uint32_t type_token_id (0x20000ee)
		i32 185; uint32_t java_map_index (0xb9)
	}, ; 69
	%struct.TypeMapModuleEntry {
		i32 33554671, ; uint32_t type_token_id (0x20000ef)
		i32 138; uint32_t java_map_index (0x8a)
	}, ; 70
	%struct.TypeMapModuleEntry {
		i32 33554676, ; uint32_t type_token_id (0x20000f4)
		i32 198; uint32_t java_map_index (0xc6)
	}, ; 71
	%struct.TypeMapModuleEntry {
		i32 33554678, ; uint32_t type_token_id (0x20000f6)
		i32 109; uint32_t java_map_index (0x6d)
	}, ; 72
	%struct.TypeMapModuleEntry {
		i32 33554679, ; uint32_t type_token_id (0x20000f7)
		i32 172; uint32_t java_map_index (0xac)
	}, ; 73
	%struct.TypeMapModuleEntry {
		i32 33554681, ; uint32_t type_token_id (0x20000f9)
		i32 54; uint32_t java_map_index (0x36)
	}, ; 74
	%struct.TypeMapModuleEntry {
		i32 33554682, ; uint32_t type_token_id (0x20000fa)
		i32 229; uint32_t java_map_index (0xe5)
	}, ; 75
	%struct.TypeMapModuleEntry {
		i32 33554685, ; uint32_t type_token_id (0x20000fd)
		i32 123; uint32_t java_map_index (0x7b)
	}, ; 76
	%struct.TypeMapModuleEntry {
		i32 33554686, ; uint32_t type_token_id (0x20000fe)
		i32 59; uint32_t java_map_index (0x3b)
	}, ; 77
	%struct.TypeMapModuleEntry {
		i32 33554688, ; uint32_t type_token_id (0x2000100)
		i32 57; uint32_t java_map_index (0x39)
	}, ; 78
	%struct.TypeMapModuleEntry {
		i32 33554690, ; uint32_t type_token_id (0x2000102)
		i32 48; uint32_t java_map_index (0x30)
	}, ; 79
	%struct.TypeMapModuleEntry {
		i32 33554695, ; uint32_t type_token_id (0x2000107)
		i32 116; uint32_t java_map_index (0x74)
	}, ; 80
	%struct.TypeMapModuleEntry {
		i32 33554696, ; uint32_t type_token_id (0x2000108)
		i32 86; uint32_t java_map_index (0x56)
	}, ; 81
	%struct.TypeMapModuleEntry {
		i32 33554697, ; uint32_t type_token_id (0x2000109)
		i32 89; uint32_t java_map_index (0x59)
	}, ; 82
	%struct.TypeMapModuleEntry {
		i32 33554698, ; uint32_t type_token_id (0x200010a)
		i32 125; uint32_t java_map_index (0x7d)
	}, ; 83
	%struct.TypeMapModuleEntry {
		i32 33554699, ; uint32_t type_token_id (0x200010b)
		i32 133; uint32_t java_map_index (0x85)
	}, ; 84
	%struct.TypeMapModuleEntry {
		i32 33554700, ; uint32_t type_token_id (0x200010c)
		i32 215; uint32_t java_map_index (0xd7)
	}, ; 85
	%struct.TypeMapModuleEntry {
		i32 33554701, ; uint32_t type_token_id (0x200010d)
		i32 169; uint32_t java_map_index (0xa9)
	}, ; 86
	%struct.TypeMapModuleEntry {
		i32 33554702, ; uint32_t type_token_id (0x200010e)
		i32 32; uint32_t java_map_index (0x20)
	}, ; 87
	%struct.TypeMapModuleEntry {
		i32 33554704, ; uint32_t type_token_id (0x2000110)
		i32 28; uint32_t java_map_index (0x1c)
	}, ; 88
	%struct.TypeMapModuleEntry {
		i32 33554712, ; uint32_t type_token_id (0x2000118)
		i32 120; uint32_t java_map_index (0x78)
	}, ; 89
	%struct.TypeMapModuleEntry {
		i32 33554713, ; uint32_t type_token_id (0x2000119)
		i32 90; uint32_t java_map_index (0x5a)
	}, ; 90
	%struct.TypeMapModuleEntry {
		i32 33554714, ; uint32_t type_token_id (0x200011a)
		i32 156; uint32_t java_map_index (0x9c)
	}, ; 91
	%struct.TypeMapModuleEntry {
		i32 33554715, ; uint32_t type_token_id (0x200011b)
		i32 187; uint32_t java_map_index (0xbb)
	}, ; 92
	%struct.TypeMapModuleEntry {
		i32 33554717, ; uint32_t type_token_id (0x200011d)
		i32 94; uint32_t java_map_index (0x5e)
	}, ; 93
	%struct.TypeMapModuleEntry {
		i32 33554718, ; uint32_t type_token_id (0x200011e)
		i32 105; uint32_t java_map_index (0x69)
	}, ; 94
	%struct.TypeMapModuleEntry {
		i32 33554720, ; uint32_t type_token_id (0x2000120)
		i32 217; uint32_t java_map_index (0xd9)
	}, ; 95
	%struct.TypeMapModuleEntry {
		i32 33554724, ; uint32_t type_token_id (0x2000124)
		i32 4; uint32_t java_map_index (0x4)
	}, ; 96
	%struct.TypeMapModuleEntry {
		i32 33554725, ; uint32_t type_token_id (0x2000125)
		i32 15; uint32_t java_map_index (0xf)
	}, ; 97
	%struct.TypeMapModuleEntry {
		i32 33554726, ; uint32_t type_token_id (0x2000126)
		i32 22; uint32_t java_map_index (0x16)
	}, ; 98
	%struct.TypeMapModuleEntry {
		i32 33554731, ; uint32_t type_token_id (0x200012b)
		i32 170; uint32_t java_map_index (0xaa)
	}, ; 99
	%struct.TypeMapModuleEntry {
		i32 33554754, ; uint32_t type_token_id (0x2000142)
		i32 208; uint32_t java_map_index (0xd0)
	}, ; 100
	%struct.TypeMapModuleEntry {
		i32 33554756, ; uint32_t type_token_id (0x2000144)
		i32 70; uint32_t java_map_index (0x46)
	}, ; 101
	%struct.TypeMapModuleEntry {
		i32 33554758, ; uint32_t type_token_id (0x2000146)
		i32 81; uint32_t java_map_index (0x51)
	}, ; 102
	%struct.TypeMapModuleEntry {
		i32 33554767, ; uint32_t type_token_id (0x200014f)
		i32 33; uint32_t java_map_index (0x21)
	}, ; 103
	%struct.TypeMapModuleEntry {
		i32 33554769, ; uint32_t type_token_id (0x2000151)
		i32 191; uint32_t java_map_index (0xbf)
	}, ; 104
	%struct.TypeMapModuleEntry {
		i32 33554770, ; uint32_t type_token_id (0x2000152)
		i32 183; uint32_t java_map_index (0xb7)
	}, ; 105
	%struct.TypeMapModuleEntry {
		i32 33554771, ; uint32_t type_token_id (0x2000153)
		i32 227; uint32_t java_map_index (0xe3)
	}, ; 106
	%struct.TypeMapModuleEntry {
		i32 33554783, ; uint32_t type_token_id (0x200015f)
		i32 24; uint32_t java_map_index (0x18)
	}, ; 107
	%struct.TypeMapModuleEntry {
		i32 33554793, ; uint32_t type_token_id (0x2000169)
		i32 197; uint32_t java_map_index (0xc5)
	}, ; 108
	%struct.TypeMapModuleEntry {
		i32 33554794, ; uint32_t type_token_id (0x200016a)
		i32 226; uint32_t java_map_index (0xe2)
	}, ; 109
	%struct.TypeMapModuleEntry {
		i32 33554795, ; uint32_t type_token_id (0x200016b)
		i32 50; uint32_t java_map_index (0x32)
	}, ; 110
	%struct.TypeMapModuleEntry {
		i32 33554796, ; uint32_t type_token_id (0x200016c)
		i32 93; uint32_t java_map_index (0x5d)
	}, ; 111
	%struct.TypeMapModuleEntry {
		i32 33554797, ; uint32_t type_token_id (0x200016d)
		i32 78; uint32_t java_map_index (0x4e)
	}, ; 112
	%struct.TypeMapModuleEntry {
		i32 33554800, ; uint32_t type_token_id (0x2000170)
		i32 151; uint32_t java_map_index (0x97)
	}, ; 113
	%struct.TypeMapModuleEntry {
		i32 33554803, ; uint32_t type_token_id (0x2000173)
		i32 114; uint32_t java_map_index (0x72)
	}, ; 114
	%struct.TypeMapModuleEntry {
		i32 33554805, ; uint32_t type_token_id (0x2000175)
		i32 73; uint32_t java_map_index (0x49)
	}, ; 115
	%struct.TypeMapModuleEntry {
		i32 33554806, ; uint32_t type_token_id (0x2000176)
		i32 88; uint32_t java_map_index (0x58)
	}, ; 116
	%struct.TypeMapModuleEntry {
		i32 33554808, ; uint32_t type_token_id (0x2000178)
		i32 45; uint32_t java_map_index (0x2d)
	}, ; 117
	%struct.TypeMapModuleEntry {
		i32 33554810, ; uint32_t type_token_id (0x200017a)
		i32 129; uint32_t java_map_index (0x81)
	}, ; 118
	%struct.TypeMapModuleEntry {
		i32 33554811, ; uint32_t type_token_id (0x200017b)
		i32 216; uint32_t java_map_index (0xd8)
	}, ; 119
	%struct.TypeMapModuleEntry {
		i32 33554813, ; uint32_t type_token_id (0x200017d)
		i32 210; uint32_t java_map_index (0xd2)
	}, ; 120
	%struct.TypeMapModuleEntry {
		i32 33554814, ; uint32_t type_token_id (0x200017e)
		i32 159; uint32_t java_map_index (0x9f)
	}, ; 121
	%struct.TypeMapModuleEntry {
		i32 33554817, ; uint32_t type_token_id (0x2000181)
		i32 207; uint32_t java_map_index (0xcf)
	}, ; 122
	%struct.TypeMapModuleEntry {
		i32 33554818, ; uint32_t type_token_id (0x2000182)
		i32 111; uint32_t java_map_index (0x6f)
	}, ; 123
	%struct.TypeMapModuleEntry {
		i32 33554821, ; uint32_t type_token_id (0x2000185)
		i32 63; uint32_t java_map_index (0x3f)
	}, ; 124
	%struct.TypeMapModuleEntry {
		i32 33554822, ; uint32_t type_token_id (0x2000186)
		i32 122; uint32_t java_map_index (0x7a)
	}, ; 125
	%struct.TypeMapModuleEntry {
		i32 33554824, ; uint32_t type_token_id (0x2000188)
		i32 119; uint32_t java_map_index (0x77)
	}, ; 126
	%struct.TypeMapModuleEntry {
		i32 33554825, ; uint32_t type_token_id (0x2000189)
		i32 134; uint32_t java_map_index (0x86)
	}, ; 127
	%struct.TypeMapModuleEntry {
		i32 33554826, ; uint32_t type_token_id (0x200018a)
		i32 126; uint32_t java_map_index (0x7e)
	}, ; 128
	%struct.TypeMapModuleEntry {
		i32 33554827, ; uint32_t type_token_id (0x200018b)
		i32 14; uint32_t java_map_index (0xe)
	}, ; 129
	%struct.TypeMapModuleEntry {
		i32 33554829, ; uint32_t type_token_id (0x200018d)
		i32 140; uint32_t java_map_index (0x8c)
	}, ; 130
	%struct.TypeMapModuleEntry {
		i32 33554831, ; uint32_t type_token_id (0x200018f)
		i32 179; uint32_t java_map_index (0xb3)
	}, ; 131
	%struct.TypeMapModuleEntry {
		i32 33554833, ; uint32_t type_token_id (0x2000191)
		i32 154; uint32_t java_map_index (0x9a)
	}, ; 132
	%struct.TypeMapModuleEntry {
		i32 33554836, ; uint32_t type_token_id (0x2000194)
		i32 103; uint32_t java_map_index (0x67)
	}, ; 133
	%struct.TypeMapModuleEntry {
		i32 33554837, ; uint32_t type_token_id (0x2000195)
		i32 58; uint32_t java_map_index (0x3a)
	}, ; 134
	%struct.TypeMapModuleEntry {
		i32 33554840, ; uint32_t type_token_id (0x2000198)
		i32 64; uint32_t java_map_index (0x40)
	}, ; 135
	%struct.TypeMapModuleEntry {
		i32 33554842, ; uint32_t type_token_id (0x200019a)
		i32 18; uint32_t java_map_index (0x12)
	}, ; 136
	%struct.TypeMapModuleEntry {
		i32 33554844, ; uint32_t type_token_id (0x200019c)
		i32 101; uint32_t java_map_index (0x65)
	}, ; 137
	%struct.TypeMapModuleEntry {
		i32 33554845, ; uint32_t type_token_id (0x200019d)
		i32 60; uint32_t java_map_index (0x3c)
	}, ; 138
	%struct.TypeMapModuleEntry {
		i32 33554847, ; uint32_t type_token_id (0x200019f)
		i32 84; uint32_t java_map_index (0x54)
	}, ; 139
	%struct.TypeMapModuleEntry {
		i32 33554849, ; uint32_t type_token_id (0x20001a1)
		i32 92; uint32_t java_map_index (0x5c)
	}, ; 140
	%struct.TypeMapModuleEntry {
		i32 33554851, ; uint32_t type_token_id (0x20001a3)
		i32 206; uint32_t java_map_index (0xce)
	}, ; 141
	%struct.TypeMapModuleEntry {
		i32 33554852, ; uint32_t type_token_id (0x20001a4)
		i32 127; uint32_t java_map_index (0x7f)
	}, ; 142
	%struct.TypeMapModuleEntry {
		i32 33554853, ; uint32_t type_token_id (0x20001a5)
		i32 3; uint32_t java_map_index (0x3)
	}, ; 143
	%struct.TypeMapModuleEntry {
		i32 33554855, ; uint32_t type_token_id (0x20001a7)
		i32 25; uint32_t java_map_index (0x19)
	}, ; 144
	%struct.TypeMapModuleEntry {
		i32 33554857, ; uint32_t type_token_id (0x20001a9)
		i32 83; uint32_t java_map_index (0x53)
	}, ; 145
	%struct.TypeMapModuleEntry {
		i32 33554859, ; uint32_t type_token_id (0x20001ab)
		i32 19; uint32_t java_map_index (0x13)
	}, ; 146
	%struct.TypeMapModuleEntry {
		i32 33554860, ; uint32_t type_token_id (0x20001ac)
		i32 77; uint32_t java_map_index (0x4d)
	}, ; 147
	%struct.TypeMapModuleEntry {
		i32 33554862, ; uint32_t type_token_id (0x20001ae)
		i32 189; uint32_t java_map_index (0xbd)
	}, ; 148
	%struct.TypeMapModuleEntry {
		i32 33554863, ; uint32_t type_token_id (0x20001af)
		i32 196; uint32_t java_map_index (0xc4)
	}, ; 149
	%struct.TypeMapModuleEntry {
		i32 33554864, ; uint32_t type_token_id (0x20001b0)
		i32 174; uint32_t java_map_index (0xae)
	}, ; 150
	%struct.TypeMapModuleEntry {
		i32 33554865, ; uint32_t type_token_id (0x20001b1)
		i32 132; uint32_t java_map_index (0x84)
	}, ; 151
	%struct.TypeMapModuleEntry {
		i32 33554866, ; uint32_t type_token_id (0x20001b2)
		i32 56; uint32_t java_map_index (0x38)
	}, ; 152
	%struct.TypeMapModuleEntry {
		i32 33554868, ; uint32_t type_token_id (0x20001b4)
		i32 137; uint32_t java_map_index (0x89)
	}, ; 153
	%struct.TypeMapModuleEntry {
		i32 33554869, ; uint32_t type_token_id (0x20001b5)
		i32 67; uint32_t java_map_index (0x43)
	}, ; 154
	%struct.TypeMapModuleEntry {
		i32 33554870, ; uint32_t type_token_id (0x20001b6)
		i32 131; uint32_t java_map_index (0x83)
	}, ; 155
	%struct.TypeMapModuleEntry {
		i32 33554871, ; uint32_t type_token_id (0x20001b7)
		i32 29; uint32_t java_map_index (0x1d)
	}, ; 156
	%struct.TypeMapModuleEntry {
		i32 33554872, ; uint32_t type_token_id (0x20001b8)
		i32 180; uint32_t java_map_index (0xb4)
	}, ; 157
	%struct.TypeMapModuleEntry {
		i32 33554875, ; uint32_t type_token_id (0x20001bb)
		i32 87; uint32_t java_map_index (0x57)
	}, ; 158
	%struct.TypeMapModuleEntry {
		i32 33554876, ; uint32_t type_token_id (0x20001bc)
		i32 52; uint32_t java_map_index (0x34)
	}, ; 159
	%struct.TypeMapModuleEntry {
		i32 33554877, ; uint32_t type_token_id (0x20001bd)
		i32 141; uint32_t java_map_index (0x8d)
	}, ; 160
	%struct.TypeMapModuleEntry {
		i32 33554878, ; uint32_t type_token_id (0x20001be)
		i32 230; uint32_t java_map_index (0xe6)
	}, ; 161
	%struct.TypeMapModuleEntry {
		i32 33554880, ; uint32_t type_token_id (0x20001c0)
		i32 204; uint32_t java_map_index (0xcc)
	}, ; 162
	%struct.TypeMapModuleEntry {
		i32 33554882, ; uint32_t type_token_id (0x20001c2)
		i32 144; uint32_t java_map_index (0x90)
	}, ; 163
	%struct.TypeMapModuleEntry {
		i32 33554883, ; uint32_t type_token_id (0x20001c3)
		i32 224; uint32_t java_map_index (0xe0)
	}, ; 164
	%struct.TypeMapModuleEntry {
		i32 33554884, ; uint32_t type_token_id (0x20001c4)
		i32 113; uint32_t java_map_index (0x71)
	}, ; 165
	%struct.TypeMapModuleEntry {
		i32 33554886, ; uint32_t type_token_id (0x20001c6)
		i32 36; uint32_t java_map_index (0x24)
	}, ; 166
	%struct.TypeMapModuleEntry {
		i32 33554887, ; uint32_t type_token_id (0x20001c7)
		i32 166; uint32_t java_map_index (0xa6)
	}, ; 167
	%struct.TypeMapModuleEntry {
		i32 33554888, ; uint32_t type_token_id (0x20001c8)
		i32 218; uint32_t java_map_index (0xda)
	}, ; 168
	%struct.TypeMapModuleEntry {
		i32 33554890, ; uint32_t type_token_id (0x20001ca)
		i32 117; uint32_t java_map_index (0x75)
	}, ; 169
	%struct.TypeMapModuleEntry {
		i32 33554891, ; uint32_t type_token_id (0x20001cb)
		i32 97; uint32_t java_map_index (0x61)
	}, ; 170
	%struct.TypeMapModuleEntry {
		i32 33554892, ; uint32_t type_token_id (0x20001cc)
		i32 98; uint32_t java_map_index (0x62)
	}, ; 171
	%struct.TypeMapModuleEntry {
		i32 33554893, ; uint32_t type_token_id (0x20001cd)
		i32 201; uint32_t java_map_index (0xc9)
	}, ; 172
	%struct.TypeMapModuleEntry {
		i32 33554894, ; uint32_t type_token_id (0x20001ce)
		i32 71; uint32_t java_map_index (0x47)
	}, ; 173
	%struct.TypeMapModuleEntry {
		i32 33554895, ; uint32_t type_token_id (0x20001cf)
		i32 192; uint32_t java_map_index (0xc0)
	}, ; 174
	%struct.TypeMapModuleEntry {
		i32 33554896, ; uint32_t type_token_id (0x20001d0)
		i32 135; uint32_t java_map_index (0x87)
	}, ; 175
	%struct.TypeMapModuleEntry {
		i32 33554898, ; uint32_t type_token_id (0x20001d2)
		i32 213; uint32_t java_map_index (0xd5)
	}, ; 176
	%struct.TypeMapModuleEntry {
		i32 33554899, ; uint32_t type_token_id (0x20001d3)
		i32 161; uint32_t java_map_index (0xa1)
	}, ; 177
	%struct.TypeMapModuleEntry {
		i32 33554900, ; uint32_t type_token_id (0x20001d4)
		i32 153; uint32_t java_map_index (0x99)
	}, ; 178
	%struct.TypeMapModuleEntry {
		i32 33554901, ; uint32_t type_token_id (0x20001d5)
		i32 65; uint32_t java_map_index (0x41)
	}, ; 179
	%struct.TypeMapModuleEntry {
		i32 33554903, ; uint32_t type_token_id (0x20001d7)
		i32 35; uint32_t java_map_index (0x23)
	}, ; 180
	%struct.TypeMapModuleEntry {
		i32 33554906, ; uint32_t type_token_id (0x20001da)
		i32 7; uint32_t java_map_index (0x7)
	}, ; 181
	%struct.TypeMapModuleEntry {
		i32 33554907, ; uint32_t type_token_id (0x20001db)
		i32 26; uint32_t java_map_index (0x1a)
	}, ; 182
	%struct.TypeMapModuleEntry {
		i32 33554908, ; uint32_t type_token_id (0x20001dc)
		i32 96; uint32_t java_map_index (0x60)
	}, ; 183
	%struct.TypeMapModuleEntry {
		i32 33554909, ; uint32_t type_token_id (0x20001dd)
		i32 37; uint32_t java_map_index (0x25)
	}, ; 184
	%struct.TypeMapModuleEntry {
		i32 33554910, ; uint32_t type_token_id (0x20001de)
		i32 106; uint32_t java_map_index (0x6a)
	}, ; 185
	%struct.TypeMapModuleEntry {
		i32 33554912, ; uint32_t type_token_id (0x20001e0)
		i32 17; uint32_t java_map_index (0x11)
	}, ; 186
	%struct.TypeMapModuleEntry {
		i32 33554913, ; uint32_t type_token_id (0x20001e1)
		i32 41; uint32_t java_map_index (0x29)
	}, ; 187
	%struct.TypeMapModuleEntry {
		i32 33554914, ; uint32_t type_token_id (0x20001e2)
		i32 178; uint32_t java_map_index (0xb2)
	}, ; 188
	%struct.TypeMapModuleEntry {
		i32 33554916, ; uint32_t type_token_id (0x20001e4)
		i32 1; uint32_t java_map_index (0x1)
	}, ; 189
	%struct.TypeMapModuleEntry {
		i32 33554917, ; uint32_t type_token_id (0x20001e5)
		i32 95; uint32_t java_map_index (0x5f)
	}, ; 190
	%struct.TypeMapModuleEntry {
		i32 33554918, ; uint32_t type_token_id (0x20001e6)
		i32 76; uint32_t java_map_index (0x4c)
	}, ; 191
	%struct.TypeMapModuleEntry {
		i32 33554919, ; uint32_t type_token_id (0x20001e7)
		i32 46; uint32_t java_map_index (0x2e)
	}, ; 192
	%struct.TypeMapModuleEntry {
		i32 33554920, ; uint32_t type_token_id (0x20001e8)
		i32 157; uint32_t java_map_index (0x9d)
	}, ; 193
	%struct.TypeMapModuleEntry {
		i32 33554921, ; uint32_t type_token_id (0x20001e9)
		i32 188; uint32_t java_map_index (0xbc)
	}, ; 194
	%struct.TypeMapModuleEntry {
		i32 33554923, ; uint32_t type_token_id (0x20001eb)
		i32 39; uint32_t java_map_index (0x27)
	}, ; 195
	%struct.TypeMapModuleEntry {
		i32 33554924, ; uint32_t type_token_id (0x20001ec)
		i32 16; uint32_t java_map_index (0x10)
	}, ; 196
	%struct.TypeMapModuleEntry {
		i32 33554925, ; uint32_t type_token_id (0x20001ed)
		i32 68; uint32_t java_map_index (0x44)
	}, ; 197
	%struct.TypeMapModuleEntry {
		i32 33554926, ; uint32_t type_token_id (0x20001ee)
		i32 136; uint32_t java_map_index (0x88)
	}, ; 198
	%struct.TypeMapModuleEntry {
		i32 33554941, ; uint32_t type_token_id (0x20001fd)
		i32 163; uint32_t java_map_index (0xa3)
	} ; 199
], align 4

@module2_managed_to_java_duplicates = internal dso_local constant [77 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554553, ; uint32_t type_token_id (0x2000079)
		i32 66; uint32_t java_map_index (0x42)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554555, ; uint32_t type_token_id (0x200007b)
		i32 12; uint32_t java_map_index (0xc)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554558, ; uint32_t type_token_id (0x200007e)
		i32 195; uint32_t java_map_index (0xc3)
	}, ; 2
	%struct.TypeMapModuleEntry {
		i32 33554560, ; uint32_t type_token_id (0x2000080)
		i32 177; uint32_t java_map_index (0xb1)
	}, ; 3
	%struct.TypeMapModuleEntry {
		i32 33554562, ; uint32_t type_token_id (0x2000082)
		i32 146; uint32_t java_map_index (0x92)
	}, ; 4
	%struct.TypeMapModuleEntry {
		i32 33554564, ; uint32_t type_token_id (0x2000084)
		i32 72; uint32_t java_map_index (0x48)
	}, ; 5
	%struct.TypeMapModuleEntry {
		i32 33554566, ; uint32_t type_token_id (0x2000086)
		i32 173; uint32_t java_map_index (0xad)
	}, ; 6
	%struct.TypeMapModuleEntry {
		i32 33554568, ; uint32_t type_token_id (0x2000088)
		i32 5; uint32_t java_map_index (0x5)
	}, ; 7
	%struct.TypeMapModuleEntry {
		i32 33554570, ; uint32_t type_token_id (0x200008a)
		i32 43; uint32_t java_map_index (0x2b)
	}, ; 8
	%struct.TypeMapModuleEntry {
		i32 33554574, ; uint32_t type_token_id (0x200008e)
		i32 209; uint32_t java_map_index (0xd1)
	}, ; 9
	%struct.TypeMapModuleEntry {
		i32 33554578, ; uint32_t type_token_id (0x2000092)
		i32 184; uint32_t java_map_index (0xb8)
	}, ; 10
	%struct.TypeMapModuleEntry {
		i32 33554580, ; uint32_t type_token_id (0x2000094)
		i32 20; uint32_t java_map_index (0x14)
	}, ; 11
	%struct.TypeMapModuleEntry {
		i32 33554583, ; uint32_t type_token_id (0x2000097)
		i32 118; uint32_t java_map_index (0x76)
	}, ; 12
	%struct.TypeMapModuleEntry {
		i32 33554587, ; uint32_t type_token_id (0x200009b)
		i32 112; uint32_t java_map_index (0x70)
	}, ; 13
	%struct.TypeMapModuleEntry {
		i32 33554596, ; uint32_t type_token_id (0x20000a4)
		i32 121; uint32_t java_map_index (0x79)
	}, ; 14
	%struct.TypeMapModuleEntry {
		i32 33554599, ; uint32_t type_token_id (0x20000a7)
		i32 142; uint32_t java_map_index (0x8e)
	}, ; 15
	%struct.TypeMapModuleEntry {
		i32 33554604, ; uint32_t type_token_id (0x20000ac)
		i32 147; uint32_t java_map_index (0x93)
	}, ; 16
	%struct.TypeMapModuleEntry {
		i32 33554606, ; uint32_t type_token_id (0x20000ae)
		i32 2; uint32_t java_map_index (0x2)
	}, ; 17
	%struct.TypeMapModuleEntry {
		i32 33554610, ; uint32_t type_token_id (0x20000b2)
		i32 124; uint32_t java_map_index (0x7c)
	}, ; 18
	%struct.TypeMapModuleEntry {
		i32 33554619, ; uint32_t type_token_id (0x20000bb)
		i32 121; uint32_t java_map_index (0x79)
	}, ; 19
	%struct.TypeMapModuleEntry {
		i32 33554620, ; uint32_t type_token_id (0x20000bc)
		i32 142; uint32_t java_map_index (0x8e)
	}, ; 20
	%struct.TypeMapModuleEntry {
		i32 33554622, ; uint32_t type_token_id (0x20000be)
		i32 160; uint32_t java_map_index (0xa0)
	}, ; 21
	%struct.TypeMapModuleEntry {
		i32 33554625, ; uint32_t type_token_id (0x20000c1)
		i32 175; uint32_t java_map_index (0xaf)
	}, ; 22
	%struct.TypeMapModuleEntry {
		i32 33554628, ; uint32_t type_token_id (0x20000c4)
		i32 6; uint32_t java_map_index (0x6)
	}, ; 23
	%struct.TypeMapModuleEntry {
		i32 33554631, ; uint32_t type_token_id (0x20000c7)
		i32 205; uint32_t java_map_index (0xcd)
	}, ; 24
	%struct.TypeMapModuleEntry {
		i32 33554633, ; uint32_t type_token_id (0x20000c9)
		i32 194; uint32_t java_map_index (0xc2)
	}, ; 25
	%struct.TypeMapModuleEntry {
		i32 33554635, ; uint32_t type_token_id (0x20000cb)
		i32 152; uint32_t java_map_index (0x98)
	}, ; 26
	%struct.TypeMapModuleEntry {
		i32 33554638, ; uint32_t type_token_id (0x20000ce)
		i32 104; uint32_t java_map_index (0x68)
	}, ; 27
	%struct.TypeMapModuleEntry {
		i32 33554641, ; uint32_t type_token_id (0x20000d1)
		i32 225; uint32_t java_map_index (0xe1)
	}, ; 28
	%struct.TypeMapModuleEntry {
		i32 33554660, ; uint32_t type_token_id (0x20000e4)
		i32 38; uint32_t java_map_index (0x26)
	}, ; 29
	%struct.TypeMapModuleEntry {
		i32 33554662, ; uint32_t type_token_id (0x20000e6)
		i32 168; uint32_t java_map_index (0xa8)
	}, ; 30
	%struct.TypeMapModuleEntry {
		i32 33554664, ; uint32_t type_token_id (0x20000e8)
		i32 100; uint32_t java_map_index (0x64)
	}, ; 31
	%struct.TypeMapModuleEntry {
		i32 33554666, ; uint32_t type_token_id (0x20000ea)
		i32 228; uint32_t java_map_index (0xe4)
	}, ; 32
	%struct.TypeMapModuleEntry {
		i32 33554677, ; uint32_t type_token_id (0x20000f5)
		i32 198; uint32_t java_map_index (0xc6)
	}, ; 33
	%struct.TypeMapModuleEntry {
		i32 33554680, ; uint32_t type_token_id (0x20000f8)
		i32 172; uint32_t java_map_index (0xac)
	}, ; 34
	%struct.TypeMapModuleEntry {
		i32 33554687, ; uint32_t type_token_id (0x20000ff)
		i32 59; uint32_t java_map_index (0x3b)
	}, ; 35
	%struct.TypeMapModuleEntry {
		i32 33554689, ; uint32_t type_token_id (0x2000101)
		i32 57; uint32_t java_map_index (0x39)
	}, ; 36
	%struct.TypeMapModuleEntry {
		i32 33554691, ; uint32_t type_token_id (0x2000103)
		i32 48; uint32_t java_map_index (0x30)
	}, ; 37
	%struct.TypeMapModuleEntry {
		i32 33554703, ; uint32_t type_token_id (0x200010f)
		i32 32; uint32_t java_map_index (0x20)
	}, ; 38
	%struct.TypeMapModuleEntry {
		i32 33554716, ; uint32_t type_token_id (0x200011c)
		i32 187; uint32_t java_map_index (0xbb)
	}, ; 39
	%struct.TypeMapModuleEntry {
		i32 33554719, ; uint32_t type_token_id (0x200011f)
		i32 105; uint32_t java_map_index (0x69)
	}, ; 40
	%struct.TypeMapModuleEntry {
		i32 33554727, ; uint32_t type_token_id (0x2000127)
		i32 4; uint32_t java_map_index (0x4)
	}, ; 41
	%struct.TypeMapModuleEntry {
		i32 33554757, ; uint32_t type_token_id (0x2000145)
		i32 70; uint32_t java_map_index (0x46)
	}, ; 42
	%struct.TypeMapModuleEntry {
		i32 33554763, ; uint32_t type_token_id (0x200014b)
		i32 81; uint32_t java_map_index (0x51)
	}, ; 43
	%struct.TypeMapModuleEntry {
		i32 33554768, ; uint32_t type_token_id (0x2000150)
		i32 33; uint32_t java_map_index (0x21)
	}, ; 44
	%struct.TypeMapModuleEntry {
		i32 33554772, ; uint32_t type_token_id (0x2000154)
		i32 227; uint32_t java_map_index (0xe3)
	}, ; 45
	%struct.TypeMapModuleEntry {
		i32 33554801, ; uint32_t type_token_id (0x2000171)
		i32 151; uint32_t java_map_index (0x97)
	}, ; 46
	%struct.TypeMapModuleEntry {
		i32 33554804, ; uint32_t type_token_id (0x2000174)
		i32 114; uint32_t java_map_index (0x72)
	}, ; 47
	%struct.TypeMapModuleEntry {
		i32 33554807, ; uint32_t type_token_id (0x2000177)
		i32 88; uint32_t java_map_index (0x58)
	}, ; 48
	%struct.TypeMapModuleEntry {
		i32 33554809, ; uint32_t type_token_id (0x2000179)
		i32 45; uint32_t java_map_index (0x2d)
	}, ; 49
	%struct.TypeMapModuleEntry {
		i32 33554812, ; uint32_t type_token_id (0x200017c)
		i32 216; uint32_t java_map_index (0xd8)
	}, ; 50
	%struct.TypeMapModuleEntry {
		i32 33554815, ; uint32_t type_token_id (0x200017f)
		i32 159; uint32_t java_map_index (0x9f)
	}, ; 51
	%struct.TypeMapModuleEntry {
		i32 33554819, ; uint32_t type_token_id (0x2000183)
		i32 111; uint32_t java_map_index (0x6f)
	}, ; 52
	%struct.TypeMapModuleEntry {
		i32 33554823, ; uint32_t type_token_id (0x2000187)
		i32 122; uint32_t java_map_index (0x7a)
	}, ; 53
	%struct.TypeMapModuleEntry {
		i32 33554828, ; uint32_t type_token_id (0x200018c)
		i32 14; uint32_t java_map_index (0xe)
	}, ; 54
	%struct.TypeMapModuleEntry {
		i32 33554830, ; uint32_t type_token_id (0x200018e)
		i32 140; uint32_t java_map_index (0x8c)
	}, ; 55
	%struct.TypeMapModuleEntry {
		i32 33554832, ; uint32_t type_token_id (0x2000190)
		i32 179; uint32_t java_map_index (0xb3)
	}, ; 56
	%struct.TypeMapModuleEntry {
		i32 33554841, ; uint32_t type_token_id (0x2000199)
		i32 64; uint32_t java_map_index (0x40)
	}, ; 57
	%struct.TypeMapModuleEntry {
		i32 33554843, ; uint32_t type_token_id (0x200019b)
		i32 18; uint32_t java_map_index (0x12)
	}, ; 58
	%struct.TypeMapModuleEntry {
		i32 33554846, ; uint32_t type_token_id (0x200019e)
		i32 60; uint32_t java_map_index (0x3c)
	}, ; 59
	%struct.TypeMapModuleEntry {
		i32 33554848, ; uint32_t type_token_id (0x20001a0)
		i32 84; uint32_t java_map_index (0x54)
	}, ; 60
	%struct.TypeMapModuleEntry {
		i32 33554850, ; uint32_t type_token_id (0x20001a2)
		i32 92; uint32_t java_map_index (0x5c)
	}, ; 61
	%struct.TypeMapModuleEntry {
		i32 33554854, ; uint32_t type_token_id (0x20001a6)
		i32 3; uint32_t java_map_index (0x3)
	}, ; 62
	%struct.TypeMapModuleEntry {
		i32 33554856, ; uint32_t type_token_id (0x20001a8)
		i32 25; uint32_t java_map_index (0x19)
	}, ; 63
	%struct.TypeMapModuleEntry {
		i32 33554858, ; uint32_t type_token_id (0x20001aa)
		i32 83; uint32_t java_map_index (0x53)
	}, ; 64
	%struct.TypeMapModuleEntry {
		i32 33554861, ; uint32_t type_token_id (0x20001ad)
		i32 77; uint32_t java_map_index (0x4d)
	}, ; 65
	%struct.TypeMapModuleEntry {
		i32 33554867, ; uint32_t type_token_id (0x20001b3)
		i32 56; uint32_t java_map_index (0x38)
	}, ; 66
	%struct.TypeMapModuleEntry {
		i32 33554873, ; uint32_t type_token_id (0x20001b9)
		i32 180; uint32_t java_map_index (0xb4)
	}, ; 67
	%struct.TypeMapModuleEntry {
		i32 33554879, ; uint32_t type_token_id (0x20001bf)
		i32 230; uint32_t java_map_index (0xe6)
	}, ; 68
	%struct.TypeMapModuleEntry {
		i32 33554881, ; uint32_t type_token_id (0x20001c1)
		i32 204; uint32_t java_map_index (0xcc)
	}, ; 69
	%struct.TypeMapModuleEntry {
		i32 33554885, ; uint32_t type_token_id (0x20001c5)
		i32 113; uint32_t java_map_index (0x71)
	}, ; 70
	%struct.TypeMapModuleEntry {
		i32 33554889, ; uint32_t type_token_id (0x20001c9)
		i32 218; uint32_t java_map_index (0xda)
	}, ; 71
	%struct.TypeMapModuleEntry {
		i32 33554897, ; uint32_t type_token_id (0x20001d1)
		i32 135; uint32_t java_map_index (0x87)
	}, ; 72
	%struct.TypeMapModuleEntry {
		i32 33554902, ; uint32_t type_token_id (0x20001d6)
		i32 65; uint32_t java_map_index (0x41)
	}, ; 73
	%struct.TypeMapModuleEntry {
		i32 33554904, ; uint32_t type_token_id (0x20001d8)
		i32 35; uint32_t java_map_index (0x23)
	}, ; 74
	%struct.TypeMapModuleEntry {
		i32 33554911, ; uint32_t type_token_id (0x20001df)
		i32 106; uint32_t java_map_index (0x6a)
	}, ; 75
	%struct.TypeMapModuleEntry {
		i32 33554915, ; uint32_t type_token_id (0x20001e3)
		i32 178; uint32_t java_map_index (0xb2)
	} ; 76
], align 4

; Java to managed map
@map_java = dso_local local_unnamed_addr constant [232 x %struct.TypeMapJava] [
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554652, ; uint32_t type_token_id (0x20000dc)
		i32 87; uint32_t java_name_index (0x57)
	}, ; 0
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554916, ; uint32_t type_token_id (0x20001e4)
		i32 221; uint32_t java_name_index (0xdd)
	}, ; 1
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 59; uint32_t java_name_index (0x3b)
	}, ; 2
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554853, ; uint32_t type_token_id (0x20001a5)
		i32 175; uint32_t java_name_index (0xaf)
	}, ; 3
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554724, ; uint32_t type_token_id (0x2000124)
		i32 128; uint32_t java_name_index (0x80)
	}, ; 4
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 40; uint32_t java_name_index (0x28)
	}, ; 5
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 74; uint32_t java_name_index (0x4a)
	}, ; 6
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554906, ; uint32_t type_token_id (0x20001da)
		i32 213; uint32_t java_name_index (0xd5)
	}, ; 7
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554456, ; uint32_t type_token_id (0x2000018)
		i32 16; uint32_t java_name_index (0x10)
	}, ; 8
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554452, ; uint32_t type_token_id (0x2000014)
		i32 11; uint32_t java_name_index (0xb)
	}, ; 9
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554436, ; uint32_t type_token_id (0x2000004)
		i32 2; uint32_t java_name_index (0x2)
	}, ; 10
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554647, ; uint32_t type_token_id (0x20000d7)
		i32 82; uint32_t java_name_index (0x52)
	}, ; 11
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554554, ; uint32_t type_token_id (0x200007a)
		i32 33; uint32_t java_name_index (0x21)
	}, ; 12
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 18; uint32_t java_name_index (0x12)
	}, ; 13
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 161; uint32_t java_name_index (0xa1)
	}, ; 14
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554725, ; uint32_t type_token_id (0x2000125)
		i32 129; uint32_t java_name_index (0x81)
	}, ; 15
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554924, ; uint32_t type_token_id (0x20001ec)
		i32 228; uint32_t java_name_index (0xe4)
	}, ; 16
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554912, ; uint32_t type_token_id (0x20001e0)
		i32 218; uint32_t java_name_index (0xda)
	}, ; 17
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 168; uint32_t java_name_index (0xa8)
	}, ; 18
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554859, ; uint32_t type_token_id (0x20001ab)
		i32 178; uint32_t java_name_index (0xb2)
	}, ; 19
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554579, ; uint32_t type_token_id (0x2000093)
		i32 47; uint32_t java_name_index (0x2f)
	}, ; 20
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554449, ; uint32_t type_token_id (0x2000011)
		i32 26; uint32_t java_name_index (0x1a)
	}, ; 21
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554726, ; uint32_t type_token_id (0x2000126)
		i32 130; uint32_t java_name_index (0x82)
	}, ; 22
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554668, ; uint32_t type_token_id (0x20000ec)
		i32 99; uint32_t java_name_index (0x63)
	}, ; 23
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554783, ; uint32_t type_token_id (0x200015f)
		i32 139; uint32_t java_name_index (0x8b)
	}, ; 24
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554855, ; uint32_t type_token_id (0x20001a7)
		i32 176; uint32_t java_name_index (0xb0)
	}, ; 25
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554907, ; uint32_t type_token_id (0x20001db)
		i32 214; uint32_t java_name_index (0xd6)
	}, ; 26
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554656, ; uint32_t type_token_id (0x20000e0)
		i32 91; uint32_t java_name_index (0x5b)
	}, ; 27
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554704, ; uint32_t type_token_id (0x2000110)
		i32 120; uint32_t java_name_index (0x78)
	}, ; 28
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554871, ; uint32_t type_token_id (0x20001b7)
		i32 188; uint32_t java_name_index (0xbc)
	}, ; 29
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 30; uint32_t java_name_index (0x1e)
	}, ; 30
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554590, ; uint32_t type_token_id (0x200009e)
		i32 52; uint32_t java_name_index (0x34)
	}, ; 31
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554702, ; uint32_t type_token_id (0x200010e)
		i32 119; uint32_t java_name_index (0x77)
	}, ; 32
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554767, ; uint32_t type_token_id (0x200014f)
		i32 135; uint32_t java_name_index (0x87)
	}, ; 33
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554616, ; uint32_t type_token_id (0x20000b8)
		i32 68; uint32_t java_name_index (0x44)
	}, ; 34
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 212; uint32_t java_name_index (0xd4)
	}, ; 35
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554886, ; uint32_t type_token_id (0x20001c6)
		i32 198; uint32_t java_name_index (0xc6)
	}, ; 36
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554909, ; uint32_t type_token_id (0x20001dd)
		i32 216; uint32_t java_name_index (0xd8)
	}, ; 37
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 94; uint32_t java_name_index (0x5e)
	}, ; 38
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554923, ; uint32_t type_token_id (0x20001eb)
		i32 227; uint32_t java_name_index (0xe3)
	}, ; 39
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554648, ; uint32_t type_token_id (0x20000d8)
		i32 83; uint32_t java_name_index (0x53)
	}, ; 40
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554913, ; uint32_t type_token_id (0x20001e1)
		i32 219; uint32_t java_name_index (0xdb)
	}, ; 41
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554438, ; uint32_t type_token_id (0x2000006)
		i32 3; uint32_t java_name_index (0x3)
	}, ; 42
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 41; uint32_t java_name_index (0x29)
	}, ; 43
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554649, ; uint32_t type_token_id (0x20000d9)
		i32 84; uint32_t java_name_index (0x54)
	}, ; 44
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554808, ; uint32_t type_token_id (0x2000178)
		i32 149; uint32_t java_name_index (0x95)
	}, ; 45
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554919, ; uint32_t type_token_id (0x20001e7)
		i32 224; uint32_t java_name_index (0xe0)
	}, ; 46
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554448, ; uint32_t type_token_id (0x2000010)
		i32 25; uint32_t java_name_index (0x19)
	}, ; 47
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 111; uint32_t java_name_index (0x6f)
	}, ; 48
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 28; uint32_t java_name_index (0x1c)
	}, ; 49
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554795, ; uint32_t type_token_id (0x200016b)
		i32 142; uint32_t java_name_index (0x8e)
	}, ; 50
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554447, ; uint32_t type_token_id (0x200000f)
		i32 24; uint32_t java_name_index (0x18)
	}, ; 51
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554876, ; uint32_t type_token_id (0x20001bc)
		i32 191; uint32_t java_name_index (0xbf)
	}, ; 52
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554486, ; uint32_t type_token_id (0x2000036)
		i32 14; uint32_t java_name_index (0xe)
	}, ; 53
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554681, ; uint32_t type_token_id (0x20000f9)
		i32 106; uint32_t java_name_index (0x6a)
	}, ; 54
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554601, ; uint32_t type_token_id (0x20000a9)
		i32 56; uint32_t java_name_index (0x38)
	}, ; 55
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554866, ; uint32_t type_token_id (0x20001b2)
		i32 184; uint32_t java_name_index (0xb8)
	}, ; 56
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554688, ; uint32_t type_token_id (0x2000100)
		i32 110; uint32_t java_name_index (0x6e)
	}, ; 57
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554837, ; uint32_t type_token_id (0x2000195)
		i32 166; uint32_t java_name_index (0xa6)
	}, ; 58
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554686, ; uint32_t type_token_id (0x20000fe)
		i32 109; uint32_t java_name_index (0x6d)
	}, ; 59
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 170; uint32_t java_name_index (0xaa)
	}, ; 60
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554571, ; uint32_t type_token_id (0x200008b)
		i32 42; uint32_t java_name_index (0x2a)
	}, ; 61
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554651, ; uint32_t type_token_id (0x20000db)
		i32 86; uint32_t java_name_index (0x56)
	}, ; 62
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554821, ; uint32_t type_token_id (0x2000185)
		i32 156; uint32_t java_name_index (0x9c)
	}, ; 63
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 167; uint32_t java_name_index (0xa7)
	}, ; 64
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 211; uint32_t java_name_index (0xd3)
	}, ; 65
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554552, ; uint32_t type_token_id (0x2000078)
		i32 32; uint32_t java_name_index (0x20)
	}, ; 66
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554869, ; uint32_t type_token_id (0x20001b5)
		i32 186; uint32_t java_name_index (0xba)
	}, ; 67
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554925, ; uint32_t type_token_id (0x20001ed)
		i32 229; uint32_t java_name_index (0xe5)
	}, ; 68
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554572, ; uint32_t type_token_id (0x200008c)
		i32 43; uint32_t java_name_index (0x2b)
	}, ; 69
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554756, ; uint32_t type_token_id (0x2000144)
		i32 133; uint32_t java_name_index (0x85)
	}, ; 70
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554894, ; uint32_t type_token_id (0x20001ce)
		i32 205; uint32_t java_name_index (0xcd)
	}, ; 71
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 38; uint32_t java_name_index (0x26)
	}, ; 72
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554805, ; uint32_t type_token_id (0x2000175)
		i32 147; uint32_t java_name_index (0x93)
	}, ; 73
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554469, ; uint32_t type_token_id (0x2000025)
		i32 22; uint32_t java_name_index (0x16)
	}, ; 74
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554439, ; uint32_t type_token_id (0x2000007)
		i32 4; uint32_t java_name_index (0x4)
	}, ; 75
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554918, ; uint32_t type_token_id (0x20001e6)
		i32 223; uint32_t java_name_index (0xdf)
	}, ; 76
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554860, ; uint32_t type_token_id (0x20001ac)
		i32 179; uint32_t java_name_index (0xb3)
	}, ; 77
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554797, ; uint32_t type_token_id (0x200016d)
		i32 144; uint32_t java_name_index (0x90)
	}, ; 78
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554612, ; uint32_t type_token_id (0x20000b4)
		i32 64; uint32_t java_name_index (0x40)
	}, ; 79
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554450, ; uint32_t type_token_id (0x2000012)
		i32 27; uint32_t java_name_index (0x1b)
	}, ; 80
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554758, ; uint32_t type_token_id (0x2000146)
		i32 134; uint32_t java_name_index (0x86)
	}, ; 81
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554556, ; uint32_t type_token_id (0x200007c)
		i32 34; uint32_t java_name_index (0x22)
	}, ; 82
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554857, ; uint32_t type_token_id (0x20001a9)
		i32 177; uint32_t java_name_index (0xb1)
	}, ; 83
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 171; uint32_t java_name_index (0xab)
	}, ; 84
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554600, ; uint32_t type_token_id (0x20000a8)
		i32 55; uint32_t java_name_index (0x37)
	}, ; 85
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554696, ; uint32_t type_token_id (0x2000108)
		i32 113; uint32_t java_name_index (0x71)
	}, ; 86
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554875, ; uint32_t type_token_id (0x20001bb)
		i32 190; uint32_t java_name_index (0xbe)
	}, ; 87
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554806, ; uint32_t type_token_id (0x2000176)
		i32 148; uint32_t java_name_index (0x94)
	}, ; 88
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554697, ; uint32_t type_token_id (0x2000109)
		i32 114; uint32_t java_name_index (0x72)
	}, ; 89
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554713, ; uint32_t type_token_id (0x2000119)
		i32 122; uint32_t java_name_index (0x7a)
	}, ; 90
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554658, ; uint32_t type_token_id (0x20000e2)
		i32 93; uint32_t java_name_index (0x5d)
	}, ; 91
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 172; uint32_t java_name_index (0xac)
	}, ; 92
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554796, ; uint32_t type_token_id (0x200016c)
		i32 143; uint32_t java_name_index (0x8f)
	}, ; 93
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554717, ; uint32_t type_token_id (0x200011d)
		i32 125; uint32_t java_name_index (0x7d)
	}, ; 94
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554917, ; uint32_t type_token_id (0x20001e5)
		i32 222; uint32_t java_name_index (0xde)
	}, ; 95
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554908, ; uint32_t type_token_id (0x20001dc)
		i32 215; uint32_t java_name_index (0xd7)
	}, ; 96
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554891, ; uint32_t type_token_id (0x20001cb)
		i32 202; uint32_t java_name_index (0xca)
	}, ; 97
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554892, ; uint32_t type_token_id (0x20001cc)
		i32 203; uint32_t java_name_index (0xcb)
	}, ; 98
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554602, ; uint32_t type_token_id (0x20000aa)
		i32 57; uint32_t java_name_index (0x39)
	}, ; 99
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 96; uint32_t java_name_index (0x60)
	}, ; 100
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554844, ; uint32_t type_token_id (0x200019c)
		i32 169; uint32_t java_name_index (0xa9)
	}, ; 101
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554653, ; uint32_t type_token_id (0x20000dd)
		i32 88; uint32_t java_name_index (0x58)
	}, ; 102
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554836, ; uint32_t type_token_id (0x2000194)
		i32 165; uint32_t java_name_index (0xa5)
	}, ; 103
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 78; uint32_t java_name_index (0x4e)
	}, ; 104
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 126; uint32_t java_name_index (0x7e)
	}, ; 105
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 217; uint32_t java_name_index (0xd9)
	}, ; 106
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554645, ; uint32_t type_token_id (0x20000d5)
		i32 80; uint32_t java_name_index (0x50)
	}, ; 107
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554611, ; uint32_t type_token_id (0x20000b3)
		i32 63; uint32_t java_name_index (0x3f)
	}, ; 108
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554678, ; uint32_t type_token_id (0x20000f6)
		i32 104; uint32_t java_name_index (0x68)
	}, ; 109
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554669, ; uint32_t type_token_id (0x20000ed)
		i32 100; uint32_t java_name_index (0x64)
	}, ; 110
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 155; uint32_t java_name_index (0x9b)
	}, ; 111
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 51; uint32_t java_name_index (0x33)
	}, ; 112
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554884, ; uint32_t type_token_id (0x20001c4)
		i32 197; uint32_t java_name_index (0xc5)
	}, ; 113
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554803, ; uint32_t type_token_id (0x2000173)
		i32 146; uint32_t java_name_index (0x92)
	}, ; 114
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554435, ; uint32_t type_token_id (0x2000003)
		i32 1; uint32_t java_name_index (0x1)
	}, ; 115
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554695, ; uint32_t type_token_id (0x2000107)
		i32 112; uint32_t java_name_index (0x70)
	}, ; 116
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554890, ; uint32_t type_token_id (0x20001ca)
		i32 201; uint32_t java_name_index (0xc9)
	}, ; 117
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 49; uint32_t java_name_index (0x31)
	}, ; 118
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554824, ; uint32_t type_token_id (0x2000188)
		i32 158; uint32_t java_name_index (0x9e)
	}, ; 119
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554712, ; uint32_t type_token_id (0x2000118)
		i32 121; uint32_t java_name_index (0x79)
	}, ; 120
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554581, ; uint32_t type_token_id (0x2000095)
		i32 48; uint32_t java_name_index (0x30)
	}, ; 121
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 157; uint32_t java_name_index (0x9d)
	}, ; 122
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554685, ; uint32_t type_token_id (0x20000fd)
		i32 108; uint32_t java_name_index (0x6c)
	}, ; 123
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 62; uint32_t java_name_index (0x3e)
	}, ; 124
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554698, ; uint32_t type_token_id (0x200010a)
		i32 115; uint32_t java_name_index (0x73)
	}, ; 125
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554826, ; uint32_t type_token_id (0x200018a)
		i32 160; uint32_t java_name_index (0xa0)
	}, ; 126
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554852, ; uint32_t type_token_id (0x20001a4)
		i32 174; uint32_t java_name_index (0xae)
	}, ; 127
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554657, ; uint32_t type_token_id (0x20000e1)
		i32 92; uint32_t java_name_index (0x5c)
	}, ; 128
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554810, ; uint32_t type_token_id (0x200017a)
		i32 150; uint32_t java_name_index (0x96)
	}, ; 129
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554466, ; uint32_t type_token_id (0x2000022)
		i32 19; uint32_t java_name_index (0x13)
	}, ; 130
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554870, ; uint32_t type_token_id (0x20001b6)
		i32 187; uint32_t java_name_index (0xbb)
	}, ; 131
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554865, ; uint32_t type_token_id (0x20001b1)
		i32 183; uint32_t java_name_index (0xb7)
	}, ; 132
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554699, ; uint32_t type_token_id (0x200010b)
		i32 116; uint32_t java_name_index (0x74)
	}, ; 133
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554825, ; uint32_t type_token_id (0x2000189)
		i32 159; uint32_t java_name_index (0x9f)
	}, ; 134
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554896, ; uint32_t type_token_id (0x20001d0)
		i32 207; uint32_t java_name_index (0xcf)
	}, ; 135
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554926, ; uint32_t type_token_id (0x20001ee)
		i32 230; uint32_t java_name_index (0xe6)
	}, ; 136
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554868, ; uint32_t type_token_id (0x20001b4)
		i32 185; uint32_t java_name_index (0xb9)
	}, ; 137
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554671, ; uint32_t type_token_id (0x20000ef)
		i32 102; uint32_t java_name_index (0x66)
	}, ; 138
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554453, ; uint32_t type_token_id (0x2000015)
		i32 12; uint32_t java_name_index (0xc)
	}, ; 139
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 162; uint32_t java_name_index (0xa2)
	}, ; 140
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554877, ; uint32_t type_token_id (0x20001bd)
		i32 192; uint32_t java_name_index (0xc0)
	}, ; 141
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554598, ; uint32_t type_token_id (0x20000a6)
		i32 54; uint32_t java_name_index (0x36)
	}, ; 142
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554434, ; uint32_t type_token_id (0x2000002)
		i32 0; uint32_t java_name_index (0x0)
	}, ; 143
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554882, ; uint32_t type_token_id (0x20001c2)
		i32 195; uint32_t java_name_index (0xc3)
	}, ; 144
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554607, ; uint32_t type_token_id (0x20000af)
		i32 60; uint32_t java_name_index (0x3c)
	}, ; 145
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 37; uint32_t java_name_index (0x25)
	}, ; 146
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 58; uint32_t java_name_index (0x3a)
	}, ; 147
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554617, ; uint32_t type_token_id (0x20000b9)
		i32 69; uint32_t java_name_index (0x45)
	}, ; 148
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554623, ; uint32_t type_token_id (0x20000bf)
		i32 72; uint32_t java_name_index (0x48)
	}, ; 149
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554470, ; uint32_t type_token_id (0x2000026)
		i32 23; uint32_t java_name_index (0x17)
	}, ; 150
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554800, ; uint32_t type_token_id (0x2000170)
		i32 145; uint32_t java_name_index (0x91)
	}, ; 151
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 77; uint32_t java_name_index (0x4d)
	}, ; 152
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554900, ; uint32_t type_token_id (0x20001d4)
		i32 210; uint32_t java_name_index (0xd2)
	}, ; 153
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554833, ; uint32_t type_token_id (0x2000191)
		i32 164; uint32_t java_name_index (0xa4)
	}, ; 154
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554654, ; uint32_t type_token_id (0x20000de)
		i32 89; uint32_t java_name_index (0x59)
	}, ; 155
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554714, ; uint32_t type_token_id (0x200011a)
		i32 123; uint32_t java_name_index (0x7b)
	}, ; 156
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554920, ; uint32_t type_token_id (0x20001e8)
		i32 225; uint32_t java_name_index (0xe1)
	}, ; 157
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554440, ; uint32_t type_token_id (0x2000008)
		i32 5; uint32_t java_name_index (0x5)
	}, ; 158
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 153; uint32_t java_name_index (0x99)
	}, ; 159
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 71; uint32_t java_name_index (0x47)
	}, ; 160
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554899, ; uint32_t type_token_id (0x20001d3)
		i32 209; uint32_t java_name_index (0xd1)
	}, ; 161
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554613, ; uint32_t type_token_id (0x20000b5)
		i32 65; uint32_t java_name_index (0x41)
	}, ; 162
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554941, ; uint32_t type_token_id (0x20001fd)
		i32 231; uint32_t java_name_index (0xe7)
	}, ; 163
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554614, ; uint32_t type_token_id (0x20000b6)
		i32 66; uint32_t java_name_index (0x42)
	}, ; 164
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554467, ; uint32_t type_token_id (0x2000023)
		i32 20; uint32_t java_name_index (0x14)
	}, ; 165
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554887, ; uint32_t type_token_id (0x20001c7)
		i32 199; uint32_t java_name_index (0xc7)
	}, ; 166
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554455, ; uint32_t type_token_id (0x2000017)
		i32 15; uint32_t java_name_index (0xf)
	}, ; 167
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 95; uint32_t java_name_index (0x5f)
	}, ; 168
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554701, ; uint32_t type_token_id (0x200010d)
		i32 118; uint32_t java_name_index (0x76)
	}, ; 169
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554731, ; uint32_t type_token_id (0x200012b)
		i32 131; uint32_t java_name_index (0x83)
	}, ; 170
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554468, ; uint32_t type_token_id (0x2000024)
		i32 21; uint32_t java_name_index (0x15)
	}, ; 171
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 105; uint32_t java_name_index (0x69)
	}, ; 172
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 39; uint32_t java_name_index (0x27)
	}, ; 173
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554864, ; uint32_t type_token_id (0x20001b0)
		i32 182; uint32_t java_name_index (0xb6)
	}, ; 174
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 73; uint32_t java_name_index (0x49)
	}, ; 175
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554597, ; uint32_t type_token_id (0x20000a5)
		i32 53; uint32_t java_name_index (0x35)
	}, ; 176
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554559, ; uint32_t type_token_id (0x200007f)
		i32 36; uint32_t java_name_index (0x24)
	}, ; 177
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554914, ; uint32_t type_token_id (0x20001e2)
		i32 220; uint32_t java_name_index (0xdc)
	}, ; 178
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 163; uint32_t java_name_index (0xa3)
	}, ; 179
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554872, ; uint32_t type_token_id (0x20001b8)
		i32 189; uint32_t java_name_index (0xbd)
	}, ; 180
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554650, ; uint32_t type_token_id (0x20000da)
		i32 85; uint32_t java_name_index (0x55)
	}, ; 181
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554454, ; uint32_t type_token_id (0x2000016)
		i32 13; uint32_t java_name_index (0xd)
	}, ; 182
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554770, ; uint32_t type_token_id (0x2000152)
		i32 137; uint32_t java_name_index (0x89)
	}, ; 183
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554576, ; uint32_t type_token_id (0x2000090)
		i32 46; uint32_t java_name_index (0x2e)
	}, ; 184
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554670, ; uint32_t type_token_id (0x20000ee)
		i32 101; uint32_t java_name_index (0x65)
	}, ; 185
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554615, ; uint32_t type_token_id (0x20000b7)
		i32 67; uint32_t java_name_index (0x43)
	}, ; 186
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554715, ; uint32_t type_token_id (0x200011b)
		i32 124; uint32_t java_name_index (0x7c)
	}, ; 187
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554921, ; uint32_t type_token_id (0x20001e9)
		i32 226; uint32_t java_name_index (0xe2)
	}, ; 188
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554862, ; uint32_t type_token_id (0x20001ae)
		i32 180; uint32_t java_name_index (0xb4)
	}, ; 189
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554646, ; uint32_t type_token_id (0x20000d6)
		i32 81; uint32_t java_name_index (0x51)
	}, ; 190
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554769, ; uint32_t type_token_id (0x2000151)
		i32 136; uint32_t java_name_index (0x88)
	}, ; 191
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554895, ; uint32_t type_token_id (0x20001cf)
		i32 206; uint32_t java_name_index (0xce)
	}, ; 192
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554655, ; uint32_t type_token_id (0x20000df)
		i32 90; uint32_t java_name_index (0x5a)
	}, ; 193
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 76; uint32_t java_name_index (0x4c)
	}, ; 194
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554557, ; uint32_t type_token_id (0x200007d)
		i32 35; uint32_t java_name_index (0x23)
	}, ; 195
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554863, ; uint32_t type_token_id (0x20001af)
		i32 181; uint32_t java_name_index (0xb5)
	}, ; 196
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554793, ; uint32_t type_token_id (0x2000169)
		i32 140; uint32_t java_name_index (0x8c)
	}, ; 197
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554676, ; uint32_t type_token_id (0x20000f4)
		i32 103; uint32_t java_name_index (0x67)
	}, ; 198
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554446, ; uint32_t type_token_id (0x200000e)
		i32 7; uint32_t java_name_index (0x7)
	}, ; 199
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554618, ; uint32_t type_token_id (0x20000ba)
		i32 70; uint32_t java_name_index (0x46)
	}, ; 200
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554893, ; uint32_t type_token_id (0x20001cd)
		i32 204; uint32_t java_name_index (0xcc)
	}, ; 201
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554453, ; uint32_t type_token_id (0x2000015)
		i32 29; uint32_t java_name_index (0x1d)
	}, ; 202
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554608, ; uint32_t type_token_id (0x20000b0)
		i32 61; uint32_t java_name_index (0x3d)
	}, ; 203
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554880, ; uint32_t type_token_id (0x20001c0)
		i32 194; uint32_t java_name_index (0xc2)
	}, ; 204
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 75; uint32_t java_name_index (0x4b)
	}, ; 205
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554851, ; uint32_t type_token_id (0x20001a3)
		i32 173; uint32_t java_name_index (0xad)
	}, ; 206
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554817, ; uint32_t type_token_id (0x2000181)
		i32 154; uint32_t java_name_index (0x9a)
	}, ; 207
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554754, ; uint32_t type_token_id (0x2000142)
		i32 132; uint32_t java_name_index (0x84)
	}, ; 208
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554573, ; uint32_t type_token_id (0x200008d)
		i32 44; uint32_t java_name_index (0x2c)
	}, ; 209
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554813, ; uint32_t type_token_id (0x200017d)
		i32 152; uint32_t java_name_index (0x98)
	}, ; 210
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554457, ; uint32_t type_token_id (0x2000019)
		i32 17; uint32_t java_name_index (0x11)
	}, ; 211
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554447, ; uint32_t type_token_id (0x200000f)
		i32 8; uint32_t java_name_index (0x8)
	}, ; 212
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554898, ; uint32_t type_token_id (0x20001d2)
		i32 208; uint32_t java_name_index (0xd0)
	}, ; 213
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554457, ; uint32_t type_token_id (0x2000019)
		i32 31; uint32_t java_name_index (0x1f)
	}, ; 214
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554700, ; uint32_t type_token_id (0x200010c)
		i32 117; uint32_t java_name_index (0x75)
	}, ; 215
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554811, ; uint32_t type_token_id (0x200017b)
		i32 151; uint32_t java_name_index (0x97)
	}, ; 216
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554720, ; uint32_t type_token_id (0x2000120)
		i32 127; uint32_t java_name_index (0x7f)
	}, ; 217
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554888, ; uint32_t type_token_id (0x20001c8)
		i32 200; uint32_t java_name_index (0xc8)
	}, ; 218
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554667, ; uint32_t type_token_id (0x20000eb)
		i32 98; uint32_t java_name_index (0x62)
	}, ; 219
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554441, ; uint32_t type_token_id (0x2000009)
		i32 6; uint32_t java_name_index (0x6)
	}, ; 220
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554585, ; uint32_t type_token_id (0x2000099)
		i32 50; uint32_t java_name_index (0x32)
	}, ; 221
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554448, ; uint32_t type_token_id (0x2000010)
		i32 9; uint32_t java_name_index (0x9)
	}, ; 222
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554575, ; uint32_t type_token_id (0x200008f)
		i32 45; uint32_t java_name_index (0x2d)
	}, ; 223
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554883, ; uint32_t type_token_id (0x20001c3)
		i32 196; uint32_t java_name_index (0xc4)
	}, ; 224
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 79; uint32_t java_name_index (0x4f)
	}, ; 225
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554794, ; uint32_t type_token_id (0x200016a)
		i32 141; uint32_t java_name_index (0x8d)
	}, ; 226
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554771, ; uint32_t type_token_id (0x2000153)
		i32 138; uint32_t java_name_index (0x8a)
	}, ; 227
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 97; uint32_t java_name_index (0x61)
	}, ; 228
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554682, ; uint32_t type_token_id (0x20000fa)
		i32 107; uint32_t java_name_index (0x6b)
	}, ; 229
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 193; uint32_t java_name_index (0xc1)
	}, ; 230
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554449, ; uint32_t type_token_id (0x2000011)
		i32 10; uint32_t java_name_index (0xa)
	} ; 231
], align 4

; Java type names
@java_type_names = dso_local local_unnamed_addr constant [232 x ptr] [
	ptr @.str.0, ; 0
	ptr @.str.1, ; 1
	ptr @.str.2, ; 2
	ptr @.str.3, ; 3
	ptr @.str.4, ; 4
	ptr @.str.5, ; 5
	ptr @.str.6, ; 6
	ptr @.str.7, ; 7
	ptr @.str.8, ; 8
	ptr @.str.9, ; 9
	ptr @.str.10, ; 10
	ptr @.str.11, ; 11
	ptr @.str.12, ; 12
	ptr @.str.13, ; 13
	ptr @.str.14, ; 14
	ptr @.str.15, ; 15
	ptr @.str.16, ; 16
	ptr @.str.17, ; 17
	ptr @.str.18, ; 18
	ptr @.str.19, ; 19
	ptr @.str.20, ; 20
	ptr @.str.21, ; 21
	ptr @.str.22, ; 22
	ptr @.str.23, ; 23
	ptr @.str.24, ; 24
	ptr @.str.25, ; 25
	ptr @.str.26, ; 26
	ptr @.str.27, ; 27
	ptr @.str.28, ; 28
	ptr @.str.29, ; 29
	ptr @.str.30, ; 30
	ptr @.str.31, ; 31
	ptr @.str.32, ; 32
	ptr @.str.33, ; 33
	ptr @.str.34, ; 34
	ptr @.str.35, ; 35
	ptr @.str.36, ; 36
	ptr @.str.37, ; 37
	ptr @.str.38, ; 38
	ptr @.str.39, ; 39
	ptr @.str.40, ; 40
	ptr @.str.41, ; 41
	ptr @.str.42, ; 42
	ptr @.str.43, ; 43
	ptr @.str.44, ; 44
	ptr @.str.45, ; 45
	ptr @.str.46, ; 46
	ptr @.str.47, ; 47
	ptr @.str.48, ; 48
	ptr @.str.49, ; 49
	ptr @.str.50, ; 50
	ptr @.str.51, ; 51
	ptr @.str.52, ; 52
	ptr @.str.53, ; 53
	ptr @.str.54, ; 54
	ptr @.str.55, ; 55
	ptr @.str.56, ; 56
	ptr @.str.57, ; 57
	ptr @.str.58, ; 58
	ptr @.str.59, ; 59
	ptr @.str.60, ; 60
	ptr @.str.61, ; 61
	ptr @.str.62, ; 62
	ptr @.str.63, ; 63
	ptr @.str.64, ; 64
	ptr @.str.65, ; 65
	ptr @.str.66, ; 66
	ptr @.str.67, ; 67
	ptr @.str.68, ; 68
	ptr @.str.69, ; 69
	ptr @.str.70, ; 70
	ptr @.str.71, ; 71
	ptr @.str.72, ; 72
	ptr @.str.73, ; 73
	ptr @.str.74, ; 74
	ptr @.str.75, ; 75
	ptr @.str.76, ; 76
	ptr @.str.77, ; 77
	ptr @.str.78, ; 78
	ptr @.str.79, ; 79
	ptr @.str.80, ; 80
	ptr @.str.81, ; 81
	ptr @.str.82, ; 82
	ptr @.str.83, ; 83
	ptr @.str.84, ; 84
	ptr @.str.85, ; 85
	ptr @.str.86, ; 86
	ptr @.str.87, ; 87
	ptr @.str.88, ; 88
	ptr @.str.89, ; 89
	ptr @.str.90, ; 90
	ptr @.str.91, ; 91
	ptr @.str.92, ; 92
	ptr @.str.93, ; 93
	ptr @.str.94, ; 94
	ptr @.str.95, ; 95
	ptr @.str.96, ; 96
	ptr @.str.97, ; 97
	ptr @.str.98, ; 98
	ptr @.str.99, ; 99
	ptr @.str.100, ; 100
	ptr @.str.101, ; 101
	ptr @.str.102, ; 102
	ptr @.str.103, ; 103
	ptr @.str.104, ; 104
	ptr @.str.105, ; 105
	ptr @.str.106, ; 106
	ptr @.str.107, ; 107
	ptr @.str.108, ; 108
	ptr @.str.109, ; 109
	ptr @.str.110, ; 110
	ptr @.str.111, ; 111
	ptr @.str.112, ; 112
	ptr @.str.113, ; 113
	ptr @.str.114, ; 114
	ptr @.str.115, ; 115
	ptr @.str.116, ; 116
	ptr @.str.117, ; 117
	ptr @.str.118, ; 118
	ptr @.str.119, ; 119
	ptr @.str.120, ; 120
	ptr @.str.121, ; 121
	ptr @.str.122, ; 122
	ptr @.str.123, ; 123
	ptr @.str.124, ; 124
	ptr @.str.125, ; 125
	ptr @.str.126, ; 126
	ptr @.str.127, ; 127
	ptr @.str.128, ; 128
	ptr @.str.129, ; 129
	ptr @.str.130, ; 130
	ptr @.str.131, ; 131
	ptr @.str.132, ; 132
	ptr @.str.133, ; 133
	ptr @.str.134, ; 134
	ptr @.str.135, ; 135
	ptr @.str.136, ; 136
	ptr @.str.137, ; 137
	ptr @.str.138, ; 138
	ptr @.str.139, ; 139
	ptr @.str.140, ; 140
	ptr @.str.141, ; 141
	ptr @.str.142, ; 142
	ptr @.str.143, ; 143
	ptr @.str.144, ; 144
	ptr @.str.145, ; 145
	ptr @.str.146, ; 146
	ptr @.str.147, ; 147
	ptr @.str.148, ; 148
	ptr @.str.149, ; 149
	ptr @.str.150, ; 150
	ptr @.str.151, ; 151
	ptr @.str.152, ; 152
	ptr @.str.153, ; 153
	ptr @.str.154, ; 154
	ptr @.str.155, ; 155
	ptr @.str.156, ; 156
	ptr @.str.157, ; 157
	ptr @.str.158, ; 158
	ptr @.str.159, ; 159
	ptr @.str.160, ; 160
	ptr @.str.161, ; 161
	ptr @.str.162, ; 162
	ptr @.str.163, ; 163
	ptr @.str.164, ; 164
	ptr @.str.165, ; 165
	ptr @.str.166, ; 166
	ptr @.str.167, ; 167
	ptr @.str.168, ; 168
	ptr @.str.169, ; 169
	ptr @.str.170, ; 170
	ptr @.str.171, ; 171
	ptr @.str.172, ; 172
	ptr @.str.173, ; 173
	ptr @.str.174, ; 174
	ptr @.str.175, ; 175
	ptr @.str.176, ; 176
	ptr @.str.177, ; 177
	ptr @.str.178, ; 178
	ptr @.str.179, ; 179
	ptr @.str.180, ; 180
	ptr @.str.181, ; 181
	ptr @.str.182, ; 182
	ptr @.str.183, ; 183
	ptr @.str.184, ; 184
	ptr @.str.185, ; 185
	ptr @.str.186, ; 186
	ptr @.str.187, ; 187
	ptr @.str.188, ; 188
	ptr @.str.189, ; 189
	ptr @.str.190, ; 190
	ptr @.str.191, ; 191
	ptr @.str.192, ; 192
	ptr @.str.193, ; 193
	ptr @.str.194, ; 194
	ptr @.str.195, ; 195
	ptr @.str.196, ; 196
	ptr @.str.197, ; 197
	ptr @.str.198, ; 198
	ptr @.str.199, ; 199
	ptr @.str.200, ; 200
	ptr @.str.201, ; 201
	ptr @.str.202, ; 202
	ptr @.str.203, ; 203
	ptr @.str.204, ; 204
	ptr @.str.205, ; 205
	ptr @.str.206, ; 206
	ptr @.str.207, ; 207
	ptr @.str.208, ; 208
	ptr @.str.209, ; 209
	ptr @.str.210, ; 210
	ptr @.str.211, ; 211
	ptr @.str.212, ; 212
	ptr @.str.213, ; 213
	ptr @.str.214, ; 214
	ptr @.str.215, ; 215
	ptr @.str.216, ; 216
	ptr @.str.217, ; 217
	ptr @.str.218, ; 218
	ptr @.str.219, ; 219
	ptr @.str.220, ; 220
	ptr @.str.221, ; 221
	ptr @.str.222, ; 222
	ptr @.str.223, ; 223
	ptr @.str.224, ; 224
	ptr @.str.225, ; 225
	ptr @.str.226, ; 226
	ptr @.str.227, ; 227
	ptr @.str.228, ; 228
	ptr @.str.229, ; 229
	ptr @.str.230, ; 230
	ptr @.str.231 ; 231
], align 4

; Strings
@.str.0 = private unnamed_addr constant [38 x i8] c"crc64298b90ab1d20ae8a/AboutMeActivity\00", align 1
@.str.1 = private unnamed_addr constant [43 x i8] c"crc64298b90ab1d20ae8a/LocationAgentService\00", align 1
@.str.2 = private unnamed_addr constant [42 x i8] c"crc64298b90ab1d20ae8a/DatabaseSyncService\00", align 1
@.str.3 = private unnamed_addr constant [42 x i8] c"crc64298b90ab1d20ae8a/CustomerProductList\00", align 1
@.str.4 = private unnamed_addr constant [51 x i8] c"crc64298b90ab1d20ae8a/DisplayVitrineCameraActivity\00", align 1
@.str.5 = private unnamed_addr constant [39 x i8] c"crc64298b90ab1d20ae8a/ExitShopActivity\00", align 1
@.str.6 = private unnamed_addr constant [45 x i8] c"crc64298b90ab1d20ae8a/ExpirationDateActivity\00", align 1
@.str.7 = private unnamed_addr constant [35 x i8] c"crc64298b90ab1d20ae8a/MainActivity\00", align 1
@.str.8 = private unnamed_addr constant [37 x i8] c"crc64298b90ab1d20ae8a/MyBootReceiver\00", align 1
@.str.9 = private unnamed_addr constant [31 x i8] c"crc64298b90ab1d20ae8a/MainMenu\00", align 1
@.str.10 = private unnamed_addr constant [45 x i8] c"crc64298b90ab1d20ae8a/MerchDataEntryActivity\00", align 1
@.str.11 = private unnamed_addr constant [41 x i8] c"crc64298b90ab1d20ae8a/SalesOrderActivity\00", align 1
@.str.12 = private unnamed_addr constant [52 x i8] c"crc64298b90ab1d20ae8a/CustomerProductSpinnerAdapter\00", align 1
@.str.13 = private unnamed_addr constant [41 x i8] c"crc64298b90ab1d20ae8a/ShopCameraActivity\00", align 1
@.str.14 = private unnamed_addr constant [69 x i8] c"crc64298b90ab1d20ae8a/ShopCameraActivity_PromotionTypeSpinnerAdapter\00", align 1
@.str.15 = private unnamed_addr constant [46 x i8] c"crc64298b90ab1d20ae8a/ShopEntryCameraActivity\00", align 1
@.str.16 = private unnamed_addr constant [53 x i8] c"crc64298b90ab1d20ae8a/ShopEntryVitrineCameraActivity\00", align 1
@.str.17 = private unnamed_addr constant [34 x i8] c"crc64298b90ab1d20ae8a/TodaysRoute\00", align 1
@.str.18 = private unnamed_addr constant [42 x i8] c"crc646c316a5726d9f3c1/JavaObjectWrapper_1\00", align 1
@.str.19 = private unnamed_addr constant [39 x i8] c"crc646c316a5726d9f3c1/PhotoListAdapter\00", align 1
@.str.20 = private unnamed_addr constant [54 x i8] c"crc646c316a5726d9f3c1/ProductExpirationPreviewAdapter\00", align 1
@.str.21 = private unnamed_addr constant [51 x i8] c"crc646c316a5726d9f3c1/RetailShopProductListAdapter\00", align 1
@.str.22 = private unnamed_addr constant [47 x i8] c"crc646c316a5726d9f3c1/SalesOrderPreviewAdapter\00", align 1
@.str.23 = private unnamed_addr constant [41 x i8] c"crc646c316a5726d9f3c1/TodaysRouteAdapter\00", align 1
@.str.24 = private unnamed_addr constant [36 x i8] c"androidx/core/content/ContextCompat\00", align 1
@.str.25 = private unnamed_addr constant [35 x i8] c"androidx/core/content/FileProvider\00", align 1
@.str.26 = private unnamed_addr constant [36 x i8] c"androidx/core/content/LocusIdCompat\00", align 1
@.str.27 = private unnamed_addr constant [33 x i8] c"androidx/core/app/ActivityCompat\00", align 1
@.str.28 = private unnamed_addr constant [58 x i8] c"androidx/core/app/ActivityCompat$PermissionCompatDelegate\00", align 1
@.str.29 = private unnamed_addr constant [40 x i8] c"androidx/core/app/SharedElementCallback\00", align 1
@.str.30 = private unnamed_addr constant [70 x i8] c"androidx/core/app/SharedElementCallback$OnSharedElementsReadyListener\00", align 1
@.str.31 = private unnamed_addr constant [48 x i8] c"androidx/core/view/DragAndDropPermissionsCompat\00", align 1
@.str.32 = private unnamed_addr constant [32 x i8] c"javax/security/cert/Certificate\00", align 1
@.str.33 = private unnamed_addr constant [36 x i8] c"javax/security/cert/X509Certificate\00", align 1
@.str.34 = private unnamed_addr constant [28 x i8] c"javax/security/auth/Subject\00", align 1
@.str.35 = private unnamed_addr constant [24 x i8] c"javax/net/SocketFactory\00", align 1
@.str.36 = private unnamed_addr constant [33 x i8] c"javax/net/ssl/HttpsURLConnection\00", align 1
@.str.37 = private unnamed_addr constant [31 x i8] c"javax/net/ssl/HostnameVerifier\00", align 1
@.str.38 = private unnamed_addr constant [25 x i8] c"javax/net/ssl/KeyManager\00", align 1
@.str.39 = private unnamed_addr constant [25 x i8] c"javax/net/ssl/SSLSession\00", align 1
@.str.40 = private unnamed_addr constant [32 x i8] c"javax/net/ssl/SSLSessionContext\00", align 1
@.str.41 = private unnamed_addr constant [27 x i8] c"javax/net/ssl/TrustManager\00", align 1
@.str.42 = private unnamed_addr constant [32 x i8] c"javax/net/ssl/KeyManagerFactory\00", align 1
@.str.43 = private unnamed_addr constant [25 x i8] c"javax/net/ssl/SSLContext\00", align 1
@.str.44 = private unnamed_addr constant [31 x i8] c"javax/net/ssl/SSLSocketFactory\00", align 1
@.str.45 = private unnamed_addr constant [34 x i8] c"javax/net/ssl/TrustManagerFactory\00", align 1
@.str.46 = private unnamed_addr constant [27 x i8] c"android/widget/AbsListView\00", align 1
@.str.47 = private unnamed_addr constant [26 x i8] c"android/widget/AbsSpinner\00", align 1
@.str.48 = private unnamed_addr constant [27 x i8] c"android/widget/AdapterView\00", align 1
@.str.49 = private unnamed_addr constant [47 x i8] c"android/widget/AdapterView$OnItemClickListener\00", align 1
@.str.50 = private unnamed_addr constant [63 x i8] c"mono/android/widget/AdapterView_OnItemClickListenerImplementor\00", align 1
@.str.51 = private unnamed_addr constant [50 x i8] c"android/widget/AdapterView$OnItemSelectedListener\00", align 1
@.str.52 = private unnamed_addr constant [66 x i8] c"mono/android/widget/AdapterView_OnItemSelectedListenerImplementor\00", align 1
@.str.53 = private unnamed_addr constant [28 x i8] c"android/widget/ArrayAdapter\00", align 1
@.str.54 = private unnamed_addr constant [27 x i8] c"android/widget/BaseAdapter\00", align 1
@.str.55 = private unnamed_addr constant [22 x i8] c"android/widget/Button\00", align 1
@.str.56 = private unnamed_addr constant [24 x i8] c"android/widget/EditText\00", align 1
@.str.57 = private unnamed_addr constant [27 x i8] c"android/widget/FrameLayout\00", align 1
@.str.58 = private unnamed_addr constant [23 x i8] c"android/widget/Adapter\00", align 1
@.str.59 = private unnamed_addr constant [27 x i8] c"android/widget/ListAdapter\00", align 1
@.str.60 = private unnamed_addr constant [27 x i8] c"android/widget/ImageButton\00", align 1
@.str.61 = private unnamed_addr constant [25 x i8] c"android/widget/ImageView\00", align 1
@.str.62 = private unnamed_addr constant [30 x i8] c"android/widget/SpinnerAdapter\00", align 1
@.str.63 = private unnamed_addr constant [28 x i8] c"android/widget/LinearLayout\00", align 1
@.str.64 = private unnamed_addr constant [41 x i8] c"android/widget/LinearLayout$LayoutParams\00", align 1
@.str.65 = private unnamed_addr constant [24 x i8] c"android/widget/ListView\00", align 1
@.str.66 = private unnamed_addr constant [23 x i8] c"android/widget/Spinner\00", align 1
@.str.67 = private unnamed_addr constant [23 x i8] c"android/widget/TabHost\00", align 1
@.str.68 = private unnamed_addr constant [31 x i8] c"android/widget/TabHost$TabSpec\00", align 1
@.str.69 = private unnamed_addr constant [24 x i8] c"android/widget/TextView\00", align 1
@.str.70 = private unnamed_addr constant [35 x i8] c"android/widget/TextView$BufferType\00", align 1
@.str.71 = private unnamed_addr constant [26 x i8] c"android/util/AttributeSet\00", align 1
@.str.72 = private unnamed_addr constant [17 x i8] c"android/util/Log\00", align 1
@.str.73 = private unnamed_addr constant [22 x i8] c"android/text/Editable\00", align 1
@.str.74 = private unnamed_addr constant [22 x i8] c"android/text/GetChars\00", align 1
@.str.75 = private unnamed_addr constant [25 x i8] c"android/text/InputFilter\00", align 1
@.str.76 = private unnamed_addr constant [24 x i8] c"android/text/NoCopySpan\00", align 1
@.str.77 = private unnamed_addr constant [23 x i8] c"android/text/Spannable\00", align 1
@.str.78 = private unnamed_addr constant [21 x i8] c"android/text/Spanned\00", align 1
@.str.79 = private unnamed_addr constant [25 x i8] c"android/text/TextWatcher\00", align 1
@.str.80 = private unnamed_addr constant [41 x i8] c"mono/android/text/TextWatcherImplementor\00", align 1
@.str.81 = private unnamed_addr constant [35 x i8] c"android/telephony/TelephonyManager\00", align 1
@.str.82 = private unnamed_addr constant [28 x i8] c"android/provider/MediaStore\00", align 1
@.str.83 = private unnamed_addr constant [35 x i8] c"android/provider/MediaStore$Images\00", align 1
@.str.84 = private unnamed_addr constant [41 x i8] c"android/provider/MediaStore$Images$Media\00", align 1
@.str.85 = private unnamed_addr constant [26 x i8] c"android/provider/Settings\00", align 1
@.str.86 = private unnamed_addr constant [41 x i8] c"android/provider/Settings$NameValueTable\00", align 1
@.str.87 = private unnamed_addr constant [33 x i8] c"android/provider/Settings$Secure\00", align 1
@.str.88 = private unnamed_addr constant [22 x i8] c"android/os/BaseBundle\00", align 1
@.str.89 = private unnamed_addr constant [17 x i8] c"android/os/Build\00", align 1
@.str.90 = private unnamed_addr constant [25 x i8] c"android/os/Build$VERSION\00", align 1
@.str.91 = private unnamed_addr constant [18 x i8] c"android/os/Bundle\00", align 1
@.str.92 = private unnamed_addr constant [23 x i8] c"android/os/Environment\00", align 1
@.str.93 = private unnamed_addr constant [19 x i8] c"android/os/Handler\00", align 1
@.str.94 = private unnamed_addr constant [34 x i8] c"android/os/IBinder$DeathRecipient\00", align 1
@.str.95 = private unnamed_addr constant [19 x i8] c"android/os/IBinder\00", align 1
@.str.96 = private unnamed_addr constant [22 x i8] c"android/os/IInterface\00", align 1
@.str.97 = private unnamed_addr constant [22 x i8] c"android/os/Parcelable\00", align 1
@.str.98 = private unnamed_addr constant [18 x i8] c"android/os/Looper\00", align 1
@.str.99 = private unnamed_addr constant [18 x i8] c"android/os/Parcel\00", align 1
@.str.100 = private unnamed_addr constant [24 x i8] c"android/os/PowerManager\00", align 1
@.str.101 = private unnamed_addr constant [33 x i8] c"android/os/PowerManager$WakeLock\00", align 1
@.str.102 = private unnamed_addr constant [19 x i8] c"android/os/Process\00", align 1
@.str.103 = private unnamed_addr constant [16 x i8] c"android/net/Uri\00", align 1
@.str.104 = private unnamed_addr constant [26 x i8] c"android/location/Criteria\00", align 1
@.str.105 = private unnamed_addr constant [34 x i8] c"android/location/LocationListener\00", align 1
@.str.106 = private unnamed_addr constant [26 x i8] c"android/location/Location\00", align 1
@.str.107 = private unnamed_addr constant [33 x i8] c"android/location/LocationManager\00", align 1
@.str.108 = private unnamed_addr constant [33 x i8] c"android/database/CharArrayBuffer\00", align 1
@.str.109 = private unnamed_addr constant [33 x i8] c"android/database/ContentObserver\00", align 1
@.str.110 = private unnamed_addr constant [33 x i8] c"android/database/DataSetObserver\00", align 1
@.str.111 = private unnamed_addr constant [24 x i8] c"android/database/Cursor\00", align 1
@.str.112 = private unnamed_addr constant [21 x i8] c"android/app/Activity\00", align 1
@.str.113 = private unnamed_addr constant [26 x i8] c"android/app/ActivityGroup\00", align 1
@.str.114 = private unnamed_addr constant [24 x i8] c"android/app/AlertDialog\00", align 1
@.str.115 = private unnamed_addr constant [32 x i8] c"android/app/AlertDialog$Builder\00", align 1
@.str.116 = private unnamed_addr constant [24 x i8] c"android/app/Application\00", align 1
@.str.117 = private unnamed_addr constant [19 x i8] c"android/app/Dialog\00", align 1
@.str.118 = private unnamed_addr constant [28 x i8] c"android/app/DownloadManager\00", align 1
@.str.119 = private unnamed_addr constant [20 x i8] c"android/app/Service\00", align 1
@.str.120 = private unnamed_addr constant [24 x i8] c"android/app/TabActivity\00", align 1
@.str.121 = private unnamed_addr constant [33 x i8] c"android/view/ContextThemeWrapper\00", align 1
@.str.122 = private unnamed_addr constant [21 x i8] c"android/view/Display\00", align 1
@.str.123 = private unnamed_addr constant [23 x i8] c"android/view/DragEvent\00", align 1
@.str.124 = private unnamed_addr constant [28 x i8] c"android/view/LayoutInflater\00", align 1
@.str.125 = private unnamed_addr constant [18 x i8] c"android/view/View\00", align 1
@.str.126 = private unnamed_addr constant [34 x i8] c"android/view/View$OnClickListener\00", align 1
@.str.127 = private unnamed_addr constant [50 x i8] c"mono/android/view/View_OnClickListenerImplementor\00", align 1
@.str.128 = private unnamed_addr constant [23 x i8] c"android/view/ViewGroup\00", align 1
@.str.129 = private unnamed_addr constant [36 x i8] c"android/view/ViewGroup$LayoutParams\00", align 1
@.str.130 = private unnamed_addr constant [42 x i8] c"android/view/ViewGroup$MarginLayoutParams\00", align 1
@.str.131 = private unnamed_addr constant [44 x i8] c"android/view/inputmethod/InputMethodManager\00", align 1
@.str.132 = private unnamed_addr constant [40 x i8] c"mono/android/runtime/InputStreamAdapter\00", align 1
@.str.133 = private unnamed_addr constant [21 x i8] c"java/util/Collection\00", align 1
@.str.134 = private unnamed_addr constant [18 x i8] c"java/util/HashMap\00", align 1
@.str.135 = private unnamed_addr constant [20 x i8] c"java/util/ArrayList\00", align 1
@.str.136 = private unnamed_addr constant [32 x i8] c"mono/android/runtime/JavaObject\00", align 1
@.str.137 = private unnamed_addr constant [35 x i8] c"android/runtime/JavaProxyThrowable\00", align 1
@.str.138 = private unnamed_addr constant [18 x i8] c"java/util/HashSet\00", align 1
@.str.139 = private unnamed_addr constant [41 x i8] c"mono/android/runtime/OutputStreamAdapter\00", align 1
@.str.140 = private unnamed_addr constant [24 x i8] c"android/graphics/Bitmap\00", align 1
@.str.141 = private unnamed_addr constant [39 x i8] c"android/graphics/Bitmap$CompressFormat\00", align 1
@.str.142 = private unnamed_addr constant [31 x i8] c"android/graphics/BitmapFactory\00", align 1
@.str.143 = private unnamed_addr constant [24 x i8] c"android/graphics/Matrix\00", align 1
@.str.144 = private unnamed_addr constant [23 x i8] c"android/graphics/RectF\00", align 1
@.str.145 = private unnamed_addr constant [35 x i8] c"android/graphics/drawable/Drawable\00", align 1
@.str.146 = private unnamed_addr constant [34 x i8] c"android/content/BroadcastReceiver\00", align 1
@.str.147 = private unnamed_addr constant [30 x i8] c"android/content/ComponentName\00", align 1
@.str.148 = private unnamed_addr constant [32 x i8] c"android/content/ContentProvider\00", align 1
@.str.149 = private unnamed_addr constant [32 x i8] c"android/content/ContentResolver\00", align 1
@.str.150 = private unnamed_addr constant [30 x i8] c"android/content/ContentValues\00", align 1
@.str.151 = private unnamed_addr constant [24 x i8] c"android/content/Context\00", align 1
@.str.152 = private unnamed_addr constant [31 x i8] c"android/content/ContextWrapper\00", align 1
@.str.153 = private unnamed_addr constant [48 x i8] c"android/content/DialogInterface$OnClickListener\00", align 1
@.str.154 = private unnamed_addr constant [64 x i8] c"mono/android/content/DialogInterface_OnClickListenerImplementor\00", align 1
@.str.155 = private unnamed_addr constant [59 x i8] c"android/content/DialogInterface$OnMultiChoiceClickListener\00", align 1
@.str.156 = private unnamed_addr constant [75 x i8] c"mono/android/content/DialogInterface_OnMultiChoiceClickListenerImplementor\00", align 1
@.str.157 = private unnamed_addr constant [32 x i8] c"android/content/DialogInterface\00", align 1
@.str.158 = private unnamed_addr constant [23 x i8] c"android/content/Intent\00", align 1
@.str.159 = private unnamed_addr constant [29 x i8] c"android/content/IntentFilter\00", align 1
@.str.160 = private unnamed_addr constant [29 x i8] c"android/content/IntentSender\00", align 1
@.str.161 = private unnamed_addr constant [41 x i8] c"android/content/SharedPreferences$Editor\00", align 1
@.str.162 = private unnamed_addr constant [67 x i8] c"android/content/SharedPreferences$OnSharedPreferenceChangeListener\00", align 1
@.str.163 = private unnamed_addr constant [34 x i8] c"android/content/SharedPreferences\00", align 1
@.str.164 = private unnamed_addr constant [24 x i8] c"android/content/LocusId\00", align 1
@.str.165 = private unnamed_addr constant [35 x i8] c"android/content/res/ColorStateList\00", align 1
@.str.166 = private unnamed_addr constant [30 x i8] c"android/content/res/Resources\00", align 1
@.str.167 = private unnamed_addr constant [22 x i8] c"java/util/Enumeration\00", align 1
@.str.168 = private unnamed_addr constant [19 x i8] c"java/util/Iterator\00", align 1
@.str.169 = private unnamed_addr constant [17 x i8] c"java/util/Random\00", align 1
@.str.170 = private unnamed_addr constant [28 x i8] c"java/util/function/Consumer\00", align 1
@.str.171 = private unnamed_addr constant [30 x i8] c"java/util/concurrent/Executor\00", align 1
@.str.172 = private unnamed_addr constant [24 x i8] c"java/security/Principal\00", align 1
@.str.173 = private unnamed_addr constant [23 x i8] c"java/security/KeyStore\00", align 1
@.str.174 = private unnamed_addr constant [27 x i8] c"java/security/SecureRandom\00", align 1
@.str.175 = private unnamed_addr constant [31 x i8] c"java/security/cert/Certificate\00", align 1
@.str.176 = private unnamed_addr constant [30 x i8] c"java/nio/channels/FileChannel\00", align 1
@.str.177 = private unnamed_addr constant [51 x i8] c"java/nio/channels/spi/AbstractInterruptibleChannel\00", align 1
@.str.178 = private unnamed_addr constant [26 x i8] c"java/net/ConnectException\00", align 1
@.str.179 = private unnamed_addr constant [27 x i8] c"java/net/HttpURLConnection\00", align 1
@.str.180 = private unnamed_addr constant [27 x i8] c"java/net/InetSocketAddress\00", align 1
@.str.181 = private unnamed_addr constant [27 x i8] c"java/net/ProtocolException\00", align 1
@.str.182 = private unnamed_addr constant [15 x i8] c"java/net/Proxy\00", align 1
@.str.183 = private unnamed_addr constant [20 x i8] c"java/net/Proxy$Type\00", align 1
@.str.184 = private unnamed_addr constant [23 x i8] c"java/net/SocketAddress\00", align 1
@.str.185 = private unnamed_addr constant [25 x i8] c"java/net/SocketException\00", align 1
@.str.186 = private unnamed_addr constant [32 x i8] c"java/net/SocketTimeoutException\00", align 1
@.str.187 = private unnamed_addr constant [33 x i8] c"java/net/UnknownServiceException\00", align 1
@.str.188 = private unnamed_addr constant [13 x i8] c"java/net/URL\00", align 1
@.str.189 = private unnamed_addr constant [23 x i8] c"java/net/URLConnection\00", align 1
@.str.190 = private unnamed_addr constant [13 x i8] c"java/io/File\00", align 1
@.str.191 = private unnamed_addr constant [23 x i8] c"java/io/FileDescriptor\00", align 1
@.str.192 = private unnamed_addr constant [24 x i8] c"java/io/FileInputStream\00", align 1
@.str.193 = private unnamed_addr constant [18 x i8] c"java/io/Closeable\00", align 1
@.str.194 = private unnamed_addr constant [20 x i8] c"java/io/InputStream\00", align 1
@.str.195 = private unnamed_addr constant [31 x i8] c"java/io/InterruptedIOException\00", align 1
@.str.196 = private unnamed_addr constant [20 x i8] c"java/io/IOException\00", align 1
@.str.197 = private unnamed_addr constant [21 x i8] c"java/io/OutputStream\00", align 1
@.str.198 = private unnamed_addr constant [20 x i8] c"java/io/PrintWriter\00", align 1
@.str.199 = private unnamed_addr constant [21 x i8] c"java/io/StringWriter\00", align 1
@.str.200 = private unnamed_addr constant [15 x i8] c"java/io/Writer\00", align 1
@.str.201 = private unnamed_addr constant [18 x i8] c"java/lang/Boolean\00", align 1
@.str.202 = private unnamed_addr constant [15 x i8] c"java/lang/Byte\00", align 1
@.str.203 = private unnamed_addr constant [20 x i8] c"java/lang/Character\00", align 1
@.str.204 = private unnamed_addr constant [16 x i8] c"java/lang/Class\00", align 1
@.str.205 = private unnamed_addr constant [29 x i8] c"java/lang/ClassCastException\00", align 1
@.str.206 = private unnamed_addr constant [17 x i8] c"java/lang/Double\00", align 1
@.str.207 = private unnamed_addr constant [15 x i8] c"java/lang/Enum\00", align 1
@.str.208 = private unnamed_addr constant [16 x i8] c"java/lang/Error\00", align 1
@.str.209 = private unnamed_addr constant [20 x i8] c"java/lang/Exception\00", align 1
@.str.210 = private unnamed_addr constant [16 x i8] c"java/lang/Float\00", align 1
@.str.211 = private unnamed_addr constant [21 x i8] c"java/lang/Appendable\00", align 1
@.str.212 = private unnamed_addr constant [23 x i8] c"java/lang/CharSequence\00", align 1
@.str.213 = private unnamed_addr constant [35 x i8] c"java/lang/IllegalArgumentException\00", align 1
@.str.214 = private unnamed_addr constant [32 x i8] c"java/lang/IllegalStateException\00", align 1
@.str.215 = private unnamed_addr constant [36 x i8] c"java/lang/IndexOutOfBoundsException\00", align 1
@.str.216 = private unnamed_addr constant [18 x i8] c"java/lang/Integer\00", align 1
@.str.217 = private unnamed_addr constant [19 x i8] c"java/lang/Runnable\00", align 1
@.str.218 = private unnamed_addr constant [15 x i8] c"java/lang/Long\00", align 1
@.str.219 = private unnamed_addr constant [31 x i8] c"java/lang/NullPointerException\00", align 1
@.str.220 = private unnamed_addr constant [17 x i8] c"java/lang/Number\00", align 1
@.str.221 = private unnamed_addr constant [17 x i8] c"java/lang/Object\00", align 1
@.str.222 = private unnamed_addr constant [27 x i8] c"java/lang/RuntimeException\00", align 1
@.str.223 = private unnamed_addr constant [28 x i8] c"java/lang/SecurityException\00", align 1
@.str.224 = private unnamed_addr constant [16 x i8] c"java/lang/Short\00", align 1
@.str.225 = private unnamed_addr constant [28 x i8] c"java/lang/StackTraceElement\00", align 1
@.str.226 = private unnamed_addr constant [17 x i8] c"java/lang/String\00", align 1
@.str.227 = private unnamed_addr constant [17 x i8] c"java/lang/Thread\00", align 1
@.str.228 = private unnamed_addr constant [35 x i8] c"mono/java/lang/RunnableImplementor\00", align 1
@.str.229 = private unnamed_addr constant [20 x i8] c"java/lang/Throwable\00", align 1
@.str.230 = private unnamed_addr constant [40 x i8] c"java/lang/UnsupportedOperationException\00", align 1
@.str.231 = private unnamed_addr constant [25 x i8] c"mono/android/TypeManager\00", align 1

;TypeMapModule
@.TypeMapModule.0_assembly_name = private unnamed_addr constant [10 x i8] c"MerchPlus\00", align 1
@.TypeMapModule.1_assembly_name = private unnamed_addr constant [22 x i8] c"Xamarin.AndroidX.Core\00", align 1
@.TypeMapModule.2_assembly_name = private unnamed_addr constant [13 x i8] c"Mono.Android\00", align 1

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 0d97e20b84d8e87c3502469ee395805907905fe3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
