; ModuleID = 'typemaps.x86_64.ll'
source_filename = "typemaps.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

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
], align 16

; Java types name hashes
@map_java_hashes = dso_local local_unnamed_addr constant [232 x i64] [
	i64 128182020419974451, ; 0: 0x1c764de51b97533 => java/lang/String
	i64 146042335049966109, ; 1: 0x206d8baded2621d => java/util/concurrent/Executor
	i64 191318127108177865, ; 2: 0x2a7b2d2ad603fc9 => android/provider/Settings$NameValueTable
	i64 301911693875001261, ; 3: 0x4309b1602f4b7ad => crc646c316a5726d9f3c1/PhotoListAdapter
	i64 318564728890166633, ; 4: 0x46bc4eedf778d69 => android/widget/Button
	i64 361870449891484378, ; 5: 0x5059f41c47e22da => android/os/Bundle
	i64 363417747702605178, ; 6: 0x50b1e841ce2e57a => android/widget/TextView
	i64 698692053645229055, ; 7: 0x9b240b890e97bff => javax/net/ssl/HttpsURLConnection
	i64 698738878519169148, ; 8: 0x9b26b4ed4e3d07c => mono/android/content/DialogInterface_OnClickListenerImplementor
	i64 870874870088288028, ; 9: 0xc15f8148b6d471c => java/lang/Exception
	i64 1079586186822872943, ; 10: 0xefb75eac1feef6f => java/util/function/Consumer
	i64 1093289771861447773, ; 11: 0xf2c2541485e945d => android/text/Spanned
	i64 1217044833273073796, ; 12: 0x10e3cfd3e2f75084 => java/util/HashSet
	i64 1264118133047759527, ; 13: 0x118b0cbe927112a7 => android/provider/Settings
	i64 1283121375857603354, ; 14: 0x11ce9017d3b3f31a => java/net/ConnectException
	i64 1317579852464953526, ; 15: 0x1248fbe51d6298b6 => java/io/FileInputStream
	i64 1320822650197077237, ; 16: 0x12548133cc496cf5 => android/runtime/JavaProxyThrowable
	i64 1356236609822008698, ; 17: 0x12d25202cfd2257a => androidx/core/app/SharedElementCallback$OnSharedElementsReadyListener
	i64 1362770524300979611, ; 18: 0x12e9889253552d9b => java/util/Iterator
	i64 1550860884384862055, ; 19: 0x1585c3c1edcecf67 => java/net/ProtocolException
	i64 1553961109733174352, ; 20: 0x1590c765646aec50 => crc646c316a5726d9f3c1/RetailShopProductListAdapter
	i64 1589519536433163389, ; 21: 0x160f1b98c4a46c7d => android/content/ContentValues
	i64 1603037949398414562, ; 22: 0x163f22858f5cf4e2 => android/content/DialogInterface$OnMultiChoiceClickListener
	i64 1610464427476120569, ; 23: 0x165984dd3b1df3f9 => androidx/core/app/SharedElementCallback
	i64 1747499027921055994, ; 24: 0x18405d1b749330fa => android/os/BaseBundle
	i64 1831728799718484971, ; 25: 0x196b9ba37012abeb => java/io/IOException
	i64 1877272793125324469, ; 26: 0x1a0d69a8bcbd86b5 => java/net/Proxy
	i64 1885389416169908981, ; 27: 0x1a2a3faf4e16aaf5 => android/provider/MediaStore$Images$Media
	i64 1891509142727870308, ; 28: 0x1a3ffd8b2db2c764 => android/database/Cursor
	i64 2164140653916027403, ; 29: 0x1e08927568a57a0b => java/io/InputStream
	i64 2226060781910726129, ; 30: 0x1ee48e7caa3795f1 => android/widget/AbsListView
	i64 2266689907793747123, ; 31: 0x1f74e67632025cb3 => java/net/HttpURLConnection
	i64 2319268360137032813, ; 32: 0x202fb24918c5446d => java/security/SecureRandom
	i64 2357387754135176159, ; 33: 0x20b71fac231543df => java/io/File
	i64 2600674482769555637, ; 34: 0x241773a9c1e6f0b5 => android/os/PowerManager
	i64 2603260641783996945, ; 35: 0x2420a3c2d34a6211 => android/view/inputmethod/InputMethodManager
	i64 2885364006830175112, ; 36: 0x280adf46e2cf7388 => android/content/IntentFilter
	i64 2972252214977986258, ; 37: 0x293f8fa450a17ed2 => android/content/Intent
	i64 3018462964962185242, ; 38: 0x29e3bc12fa3e501a => crc64298b90ab1d20ae8a/MyBootReceiver
	i64 3071747017624329461, ; 39: 0x2aa109a3415d1cf5 => android/os/Build
	i64 3093465012747984901, ; 40: 0x2aee320a09f16005 => android/app/Service
	i64 3107623337225989922, ; 41: 0x2b207ef5d711af22 => android/location/Location
	i64 3312753486604898190, ; 42: 0x2df943be8d858f8e => android/app/Dialog
	i64 3371417764163457944, ; 43: 0x2ec9ae980e679398 => android/widget/TextView$BufferType
	i64 3437476681661255010, ; 44: 0x2fb45ed4561b8d62 => android/app/TabActivity
	i64 3476617847597562063, ; 45: 0x303f6d8331d5f8cf => java/io/PrintWriter
	i64 3492966660860961054, ; 46: 0x307982abe8e6611e => android/widget/AdapterView
	i64 3530631042196079534, ; 47: 0x30ff523a0f1083ae => android/content/DialogInterface
	i64 3531967851957559493, ; 48: 0x3104120c5607a0c5 => androidx/core/content/ContextCompat
	i64 3656396631051491790, ; 49: 0x32be215d0fc259ce => java/net/InetSocketAddress
	i64 3664445150084014760, ; 50: 0x32dab972eda922a8 => android/text/InputFilter
	i64 3683994402414529095, ; 51: 0x33202d63ee720a47 => crc64298b90ab1d20ae8a/TodaysRoute
	i64 3880992763041431256, ; 52: 0x35dc0e5b08f23ed8 => android/widget/SpinnerAdapter
	i64 3926755326194143409, ; 53: 0x367ea32a11acd0b1 => androidx/core/content/FileProvider
	i64 3936478700004404583, ; 54: 0x36a12e8573a76d67 => java/net/SocketAddress
	i64 3957166361670620563, ; 55: 0x36eaadd708809593 => javax/security/cert/Certificate
	i64 4032644632170534830, ; 56: 0x37f6d4ed55e917ae => android/text/GetChars
	i64 4175515025192399737, ; 57: 0x39f268cae6e63379 => android/widget/ArrayAdapter
	i64 4216519898928517408, ; 58: 0x3a8416820c001120 => android/os/IInterface
	i64 4296024618409823481, ; 59: 0x3b9e8ba10ea68cf9 => android/app/ActivityGroup
	i64 4305371449952891808, ; 60: 0x3bbfc085dc8cf3a0 => java/lang/Class
	i64 4328468547648071486, ; 61: 0x3c11cf35fc03a73e => android/net/Uri
	i64 4406641945990788791, ; 62: 0x3d278980a31df6b7 => android/content/BroadcastReceiver
	i64 4413535772071861656, ; 63: 0x3d40076699d29d98 => android/provider/MediaStore
	i64 4590799101254748484, ; 64: 0x3fb5cb75a178c944 => javax/net/ssl/TrustManagerFactory
	i64 4642866827801349258, ; 65: 0x406ec6c7ea8b4c8a => android/text/Spannable
	i64 4756101769800025001, ; 66: 0x4201115c588983a9 => javax/net/SocketFactory
	i64 5214467817578676657, ; 67: 0x485d82da477bc1b1 => java/lang/Error
	i64 5517358870160988903, ; 68: 0x4c9198a9024bdae7 => android/text/NoCopySpan
	i64 5602058331881754211, ; 69: 0x4dbe825f32d44e63 => android/location/LocationManager
	i64 5681742043617578465, ; 70: 0x4ed99a49173a4de1 => android/location/LocationListener
	i64 5793982059409158284, ; 71: 0x50685bfc3611b08c => java/net/URLConnection
	i64 5856823971975629766, ; 72: 0x51479e5f29998bc6 => android/widget/LinearLayout$LayoutParams
	i64 5866051512250042973, ; 73: 0x516866c54dce8a5d => android/database/ContentObserver
	i64 5890385405214755341, ; 74: 0x51beda5143f88a0d => android/widget/FrameLayout
	i64 5890643136130363191, ; 75: 0x51bfc4b8eba4af37 => android/widget/TabHost$TabSpec
	i64 5928119462157283979, ; 76: 0x5244e93e07f6f28b => android/widget/Adapter
	i64 5991054489085362647, ; 77: 0x53248050dbf141d7 => javax/security/cert/X509Certificate
	i64 6000768439507874839, ; 78: 0x5347031a303df417 => java/lang/Enum
	i64 6103765426076812607, ; 79: 0x54b4ee50dd9e853f => crc64298b90ab1d20ae8a/DisplayVitrineCameraActivity
	i64 6116679261601087867, ; 80: 0x54e2cf6180bb417b => android/widget/LinearLayout
	i64 6485460119900587756, ; 81: 0x5a00fb9e1ba306ec => android/view/DragEvent
	i64 6740334783866200195, ; 82: 0x5d8a7ac62b8de083 => javax/net/ssl/SSLSession
	i64 6845548855192212761, ; 83: 0x5f00466a1e505519 => androidx/core/content/LocusIdCompat
	i64 6875961628645093091, ; 84: 0x5f6c52abbc9b6ee3 => android/content/ContentResolver
	i64 7208753437855659137, ; 85: 0x640aa30ee0c33481 => mono/android/content/DialogInterface_OnMultiChoiceClickListenerImplementor
	i64 7282188369651690282, ; 86: 0x650f87bd5091eb2a => android/os/Parcelable
	i64 7291810569935423650, ; 87: 0x6531b714667088a2 => android/os/Build$VERSION
	i64 7437796681088239247, ; 88: 0x67385cac9fd8068f => java/io/FileDescriptor
	i64 7550243207664062643, ; 89: 0x68c7da31c89f50b3 => crc64298b90ab1d20ae8a/DatabaseSyncService
	i64 7620119821450638162, ; 90: 0x69c01a9abf7a7352 => java/io/InterruptedIOException
	i64 7658195837123306865, ; 91: 0x6a476089fc1c2571 => java/lang/Character
	i64 7681872310366473403, ; 92: 0x6a9b7e2a7d4d8cbb => android/widget/ListAdapter
	i64 7933543037734065265, ; 93: 0x6e199b5bee699471 => java/util/HashMap
	i64 8190305621607579207, ; 94: 0x71a9cf9199cdfe47 => java/nio/channels/spi/AbstractInterruptibleChannel
	i64 8416619862292774857, ; 95: 0x74cdd72bed753fc9 => java/lang/IllegalArgumentException
	i64 8427024478828076046, ; 96: 0x74f2ce1d7e119c0e => android/text/TextWatcher
	i64 8462361838522003613, ; 97: 0x75705941b1ecf09d => android/os/IBinder
	i64 8487642170263250902, ; 98: 0x75ca29959b2aa7d6 => android/content/ContextWrapper
	i64 8587172038193766563, ; 99: 0x772bc378d1b4e0a3 => java/lang/Runnable
	i64 8720029361076671000, ; 100: 0x7903c480d1f39218 => android/provider/Settings$Secure
	i64 8892370647053528048, ; 101: 0x7b680bfd5cbc6bf0 => crc64298b90ab1d20ae8a/SalesOrderActivity
	i64 8950391188589719199, ; 102: 0x7c362d5d64ad2e9f => java/lang/Boolean
	i64 8972627133644507939, ; 103: 0x7c852cd6cae98b23 => android/view/LayoutInflater
	i64 9154019302997878316, ; 104: 0x7f099c0e5641e62c => javax/net/ssl/KeyManager
	i64 9187009981601112352, ; 105: 0x7f7ed0e7454d6120 => android/view/ViewGroup$MarginLayoutParams
	i64 9197654194669573918, ; 106: 0x7fa4a1c24029571e => crc64298b90ab1d20ae8a/CustomerProductList
	i64 9217569019755338609, ; 107: 0x7feb622fcb299b71 => java/security/Principal
	i64 9261475547774197607, ; 108: 0x80875ef0f90c1f67 => android/widget/ImageView
	i64 9355021183435591253, ; 109: 0x81d3b63388eece55 => android/graphics/drawable/Drawable
	i64 9374080444557732201, ; 110: 0x82176c7f91cca969 => android/os/IBinder$DeathRecipient
	i64 9617120008938227925, ; 111: 0x8576dfb1c71088d5 => android/telephony/TelephonyManager
	i64 9667515047141612341, ; 112: 0x8629e9b6f59e9b35 => java/lang/Thread
	i64 9785570804745343508, ; 113: 0x87cd54ccfd479214 => java/net/URL
	i64 9866983915955550238, ; 114: 0x88ee91981305f81e => java/lang/SecurityException
	i64 9869939015140501507, ; 115: 0x88f9113db837e803 => android/app/Activity
	i64 9977296435420958008, ; 116: 0x8a767a3efc098d38 => java/lang/NullPointerException
	i64 10031469607122277866, ; 117: 0x8b36f0756ba695ea => crc646c316a5726d9f3c1/SalesOrderPreviewAdapter
	i64 10108115512948409299, ; 118: 0x8c473d8101e7c3d3 => android/widget/TabHost
	i64 10201100597782994469, ; 119: 0x8d9196f228e15e25 => android/app/DownloadManager
	i64 10266059374509936169, ; 120: 0x8e785e9bf4bbce29 => java/lang/Long
	i64 10499957734077086001, ; 121: 0x91b757ed9047b931 => android/view/ViewGroup$LayoutParams
	i64 10589642565195629679, ; 122: 0x92f5f7ce84d7846f => java/lang/UnsupportedOperationException
	i64 10655658609895831411, ; 123: 0x93e0810cb2ac7773 => android/content/res/ColorStateList
	i64 10677029292148801270, ; 124: 0x942c6d9266f83ef6 => crc64298b90ab1d20ae8a/ExitShopActivity
	i64 10722894652849872693, ; 125: 0x94cf5fdfdb0d5f35 => java/lang/Short
	i64 10808978037618020601, ; 126: 0x96013441bd3890f9 => android/database/DataSetObserver
	i64 10809640897693515055, ; 127: 0x96038f1fe08e7d2f => android/os/PowerManager$WakeLock
	i64 11005920483369566278, ; 128: 0x98bce25e25704046 => java/util/Random
	i64 11034393799968168840, ; 129: 0x99220ab2c6e01b88 => crc64298b90ab1d20ae8a/ShopEntryVitrineCameraActivity
	i64 11112718717483603117, ; 130: 0x9a384ecbbc71d4ad => android/os/Handler
	i64 11240308327273157113, ; 131: 0x9bfd98deb4fb51f9 => android/graphics/RectF
	i64 11373498917253313136, ; 132: 0x9dd6c901802c7270 => android/graphics/Bitmap$CompressFormat
	i64 11393831178655295976, ; 133: 0x9e1f05170284e9e8 => javax/net/ssl/SSLContext
	i64 11482682312365990619, ; 134: 0x9f5aaeb9a93c62db => android/content/ContentProvider
	i64 11573301743732151818, ; 135: 0xa09ca09e3190560a => mono/java/lang/RunnableImplementor
	i64 11712899692065226922, ; 136: 0xa28c94365b5480aa => java/util/ArrayList
	i64 11763058807128842702, ; 137: 0xa33ec7a966f1e1ce => java/security/cert/Certificate
	i64 11954228872253987625, ; 138: 0xa5e5f3d2b66adb29 => android/view/View
	i64 12010988764752805573, ; 139: 0xa6af9aa5044c7ec5 => android/provider/MediaStore$Images
	i64 12016049636675011370, ; 140: 0xa6c1957b1579c32a => android/widget/EditText
	i64 12228984007958404582, ; 141: 0xa9b61429ce4b1de6 => android/content/Context
	i64 12426529965699990912, ; 142: 0xac73e72a4c4b8580 => java/lang/IndexOutOfBoundsException
	i64 12458575303368155603, ; 143: 0xace5c03ae4b6a1d3 => android/content/res/Resources
	i64 12476375190645835422, ; 144: 0xad24fd221af1069e => android/os/Looper
	i64 12488842103917764438, ; 145: 0xad5147b98bf5df56 => java/lang/IllegalStateException
	i64 12532121860257401396, ; 146: 0xadeb0a6f128cca34 => java/lang/Number
	i64 12610567535961206755, ; 147: 0xaf01bc5c3d0a6fe3 => android/widget/AbsSpinner
	i64 12740368432395772516, ; 148: 0xb0cee196808ce264 => mono/android/widget/AdapterView_OnItemSelectedListenerImplementor
	i64 12806567541869262104, ; 149: 0xb1ba1153c52a3518 => java/lang/Integer
	i64 12849974230675558933, ; 150: 0xb254477b10571215 => android/widget/ImageButton
	i64 12882710959019299141, ; 151: 0xb2c8955c98609145 => java/net/SocketTimeoutException
	i64 12937320536215432362, ; 152: 0xb38a987b9a7cacaa => crc646c316a5726d9f3c1/ProductExpirationPreviewAdapter
	i64 13023179417444670712, ; 153: 0xb4bba0ae2ef7dcf8 => crc646c316a5726d9f3c1/TodaysRouteAdapter
	i64 13120818853233679472, ; 154: 0xb616833add3ddc70 => android/widget/AdapterView$OnItemClickListener
	i64 13191394589072141775, ; 155: 0xb7113f7cdda7adcf => android/app/AlertDialog$Builder
	i64 13224292020065212532, ; 156: 0xb7861f86e6051874 => crc64298b90ab1d20ae8a/MainMenu
	i64 13291089739636035741, ; 157: 0xb8736fb2f0d8509d => android/content/ComponentName
	i64 13402779434266666368, ; 158: 0xba003ce26e602580 => mono/android/TypeManager
	i64 13431868251608748947, ; 159: 0xba6795027efc9393 => android/content/LocusId
	i64 13491848569179882038, ; 160: 0xbb3cacca71544236 => android/app/AlertDialog
	i64 13501119006686247638, ; 161: 0xbb5d9c345fcefad6 => android/content/IntentSender
	i64 13502560151794130917, ; 162: 0xbb62baeb1e089fe5 => javax/security/auth/Subject
	i64 13568881141907939254, ; 163: 0xbc4e5981eeeb5fb6 => crc64298b90ab1d20ae8a/ShopEntryCameraActivity
	i64 13770727111868296170, ; 164: 0xbf1b735909c02bea => java/io/StringWriter
	i64 13789203013919682202, ; 165: 0xbf5d1715346dae9a => java/lang/RuntimeException
	i64 13805562342397192842, ; 166: 0xbf9735ce2f182a8a => android/util/AttributeSet
	i64 13877554026709814142, ; 167: 0xc096f9dc61548b7e => android/view/View$OnClickListener
	i64 13959986462581077347, ; 168: 0xc1bbd5b97b683563 => java/net/UnknownServiceException
	i64 14008091590880237307, ; 169: 0xc266bd15778d96fb => android/os/Process
	i64 14031640676547298208, ; 170: 0xc2ba66da3d8603a0 => java/nio/channels/FileChannel
	i64 14084605320717201844, ; 171: 0xc37691ea6eecf1b4 => crc64298b90ab1d20ae8a/LocationAgentService
	i64 14160925941038085484, ; 172: 0xc485b71d9630756c => javax/net/ssl/KeyManagerFactory
	i64 14167891754637755728, ; 173: 0xc49e767c735e8550 => java/lang/Object
	i64 14180814796703042768, ; 174: 0xc4cc5feca7168cd0 => java/lang/ClassCastException
	i64 14189835856362889500, ; 175: 0xc4ec6c87c79c611c => crc64298b90ab1d20ae8a/CustomerProductSpinnerAdapter
	i64 14206023932851353817, ; 176: 0xc525ef800c4d78d9 => mono/android/runtime/OutputStreamAdapter
	i64 14296237994325564878, ; 177: 0xc66670b60c37ddce => android/content/SharedPreferences
	i64 14361620789319229198, ; 178: 0xc74eba044a3cdf0e => android/os/Parcel
	i64 14471250939593441059, ; 179: 0xc8d4360ebe013f23 => crc64298b90ab1d20ae8a/MainActivity
	i64 14480243895569103343, ; 180: 0xc8f4291a786a39ef => androidx/core/app/ActivityCompat
	i64 14501341618205132561, ; 181: 0xc93f1d5ecfb48711 => java/lang/Byte
	i64 14569564621816213537, ; 182: 0xca317dd4a7743021 => androidx/core/app/ActivityCompat$PermissionCompatDelegate
	i64 14607314095492182601, ; 183: 0xcab79ac6e85e3249 => crc64298b90ab1d20ae8a/AboutMeActivity
	i64 14649586819325063784, ; 184: 0xcb4dc998681d7268 => mono/android/view/View_OnClickListenerImplementor
	i64 14684559126920293129, ; 185: 0xcbca08b94b4deb09 => java/lang/CharSequence
	i64 14940408132235664607, ; 186: 0xcf56fe09e1439cdf => java/lang/Throwable
	i64 14940704599466395126, ; 187: 0xcf580bac893d45f6 => android/util/Log
	i64 15142650489578038267, ; 188: 0xd22580641d31a3fb => java/lang/StackTraceElement
	i64 15178898666151156562, ; 189: 0xd2a647ea65971b52 => javax/net/ssl/HostnameVerifier
	i64 15223859190564664402, ; 190: 0xd34603463c2c3452 => android/text/Editable
	i64 15227253633559603018, ; 191: 0xd35212809518a74a => android/widget/Spinner
	i64 15295416057057826855, ; 192: 0xd4443bdd84bcd827 => android/widget/AdapterView$OnItemSelectedListener
	i64 15298816789066891989, ; 193: 0xd45050d0241d42d5 => androidx/core/view/DragAndDropPermissionsCompat
	i64 15625622630723498443, ; 194: 0xd8d95cfe9a9431cb => mono/android/text/TextWatcherImplementor
	i64 15633873768898914415, ; 195: 0xd8f6ad5c6a84686f => java/io/Writer
	i64 15638057521195145919, ; 196: 0xd9058a7627f65abf => crc64298b90ab1d20ae8a/MerchDataEntryActivity
	i64 15645438581444444576, ; 197: 0xd91fc37f2a3001a0 => java/lang/Appendable
	i64 15692112419447079658, ; 198: 0xd9c5951ae8a382ea => crc64298b90ab1d20ae8a/ShopCameraActivity_PromotionTypeSpinnerAdapter
	i64 15767615218119146656, ; 199: 0xdad1d2801f08fca0 => javax/net/ssl/SSLSessionContext
	i64 15844004444849296506, ; 200: 0xdbe13618f2f3d47a => android/widget/ListView
	i64 15888603495552893685, ; 201: 0xdc7fa8b2a175c2f5 => javax/net/ssl/TrustManager
	i64 15896143924811176167, ; 202: 0xdc9a72ada0da98e7 => java/net/SocketException
	i64 16002286839467779661, ; 203: 0xde138b18c462a64d => android/os/Environment
	i64 16003229482241506982, ; 204: 0xde16e46ce4103ea6 => android/graphics/Bitmap
	i64 16039792319712272754, ; 205: 0xde98ca21df7d2172 => crc646c316a5726d9f3c1/JavaObjectWrapper_1
	i64 16314168557433322311, ; 206: 0xe26791dde7a8fb47 => android/view/ContextThemeWrapper
	i64 16410496677374749578, ; 207: 0xe3bdcbc812a66b8a => android/location/Criteria
	i64 16413651262945443612, ; 208: 0xe3c900dc43013f1c => android/content/DialogInterface$OnClickListener
	i64 16542847110558016359, ; 209: 0xe593ffcc9e686367 => android/app/Application
	i64 16603717322881265010, ; 210: 0xe66c40ef55566d72 => mono/android/runtime/JavaObject
	i64 16723123314454325679, ; 211: 0xe814780d351a69af => mono/android/runtime/InputStreamAdapter
	i64 16832017439803262409, ; 212: 0xe99756ae80a745c9 => android/view/ViewGroup
	i64 16878061737430998064, ; 213: 0xea3aebb9f4afdc30 => android/content/SharedPreferences$Editor
	i64 17125416866214736517, ; 214: 0xeda9b3e7cd367285 => java/io/OutputStream
	i64 17163176023095185573, ; 215: 0xee2fd9a89a1710a5 => android/graphics/Matrix
	i64 17407046679165606066, ; 216: 0xf19240ba6c4f18b2 => mono/android/widget/AdapterView_OnItemClickListenerImplementor
	i64 17485531046532206351, ; 217: 0xf2a915d8319dcf0f => android/database/CharArrayBuffer
	i64 17490083481060646178, ; 218: 0xf2b94242748c1d22 => java/util/Enumeration
	i64 17498018958444438720, ; 219: 0xf2d57388c321a4c0 => java/lang/Double
	i64 17519581955836770800, ; 220: 0xf3220ef752fe79f0 => java/security/KeyStore
	i64 17542023464739623563, ; 221: 0xf371c966495f528b => android/view/Display
	i64 17605619666541934257, ; 222: 0xf453b9cee2dcf6b1 => java/net/Proxy$Type
	i64 17645817129531856090, ; 223: 0xf4e28930113fd4da => java/io/Closeable
	i64 17786854109604896016, ; 224: 0xf6d79992eab25d10 => android/graphics/BitmapFactory
	i64 17789860076383322028, ; 225: 0xf6e2477c18bb3fac => crc64298b90ab1d20ae8a/ShopCameraActivity
	i64 17862149423782831372, ; 226: 0xf7e31a436186250c => crc64298b90ab1d20ae8a/ExpirationDateActivity
	i64 18213852175163534091, ; 227: 0xfcc49a0d5c192b0b => android/content/SharedPreferences$OnSharedPreferenceChangeListener
	i64 18216578448169670053, ; 228: 0xfcce4995423095a5 => javax/net/ssl/SSLSocketFactory
	i64 18323076787180290332, ; 229: 0xfe48a5421416411c => android/widget/BaseAdapter
	i64 18401512074820890716, ; 230: 0xff5f4dbc95c2b05c => java/lang/Float
	i64 18401692079723824300 ; 231: 0xff5ff1733e0ec4ac => java/util/Collection
], align 16

@module0_managed_to_java = internal dso_local constant [24 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554434, ; uint32_t type_token_id (0x2000002)
		i32 183; uint32_t java_map_index (0xb7)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554435, ; uint32_t type_token_id (0x2000003)
		i32 171; uint32_t java_map_index (0xab)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554436, ; uint32_t type_token_id (0x2000004)
		i32 89; uint32_t java_map_index (0x59)
	}, ; 2
	%struct.TypeMapModuleEntry {
		i32 33554438, ; uint32_t type_token_id (0x2000006)
		i32 106; uint32_t java_map_index (0x6a)
	}, ; 3
	%struct.TypeMapModuleEntry {
		i32 33554439, ; uint32_t type_token_id (0x2000007)
		i32 79; uint32_t java_map_index (0x4f)
	}, ; 4
	%struct.TypeMapModuleEntry {
		i32 33554440, ; uint32_t type_token_id (0x2000008)
		i32 124; uint32_t java_map_index (0x7c)
	}, ; 5
	%struct.TypeMapModuleEntry {
		i32 33554441, ; uint32_t type_token_id (0x2000009)
		i32 226; uint32_t java_map_index (0xe2)
	}, ; 6
	%struct.TypeMapModuleEntry {
		i32 33554446, ; uint32_t type_token_id (0x200000e)
		i32 179; uint32_t java_map_index (0xb3)
	}, ; 7
	%struct.TypeMapModuleEntry {
		i32 33554447, ; uint32_t type_token_id (0x200000f)
		i32 38; uint32_t java_map_index (0x26)
	}, ; 8
	%struct.TypeMapModuleEntry {
		i32 33554448, ; uint32_t type_token_id (0x2000010)
		i32 156; uint32_t java_map_index (0x9c)
	}, ; 9
	%struct.TypeMapModuleEntry {
		i32 33554449, ; uint32_t type_token_id (0x2000011)
		i32 196; uint32_t java_map_index (0xc4)
	}, ; 10
	%struct.TypeMapModuleEntry {
		i32 33554452, ; uint32_t type_token_id (0x2000014)
		i32 101; uint32_t java_map_index (0x65)
	}, ; 11
	%struct.TypeMapModuleEntry {
		i32 33554453, ; uint32_t type_token_id (0x2000015)
		i32 175; uint32_t java_map_index (0xaf)
	}, ; 12
	%struct.TypeMapModuleEntry {
		i32 33554454, ; uint32_t type_token_id (0x2000016)
		i32 225; uint32_t java_map_index (0xe1)
	}, ; 13
	%struct.TypeMapModuleEntry {
		i32 33554455, ; uint32_t type_token_id (0x2000017)
		i32 163; uint32_t java_map_index (0xa3)
	}, ; 14
	%struct.TypeMapModuleEntry {
		i32 33554456, ; uint32_t type_token_id (0x2000018)
		i32 129; uint32_t java_map_index (0x81)
	}, ; 15
	%struct.TypeMapModuleEntry {
		i32 33554457, ; uint32_t type_token_id (0x2000019)
		i32 51; uint32_t java_map_index (0x33)
	}, ; 16
	%struct.TypeMapModuleEntry {
		i32 33554465, ; uint32_t type_token_id (0x2000021)
		i32 205; uint32_t java_map_index (0xcd)
	}, ; 17
	%struct.TypeMapModuleEntry {
		i32 33554466, ; uint32_t type_token_id (0x2000022)
		i32 3; uint32_t java_map_index (0x3)
	}, ; 18
	%struct.TypeMapModuleEntry {
		i32 33554467, ; uint32_t type_token_id (0x2000023)
		i32 152; uint32_t java_map_index (0x98)
	}, ; 19
	%struct.TypeMapModuleEntry {
		i32 33554468, ; uint32_t type_token_id (0x2000024)
		i32 20; uint32_t java_map_index (0x14)
	}, ; 20
	%struct.TypeMapModuleEntry {
		i32 33554469, ; uint32_t type_token_id (0x2000025)
		i32 117; uint32_t java_map_index (0x75)
	}, ; 21
	%struct.TypeMapModuleEntry {
		i32 33554470, ; uint32_t type_token_id (0x2000026)
		i32 153; uint32_t java_map_index (0x99)
	}, ; 22
	%struct.TypeMapModuleEntry {
		i32 33554486, ; uint32_t type_token_id (0x2000036)
		i32 198; uint32_t java_map_index (0xc6)
	} ; 23
], align 16

@module1_managed_to_java = internal dso_local constant [8 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554447, ; uint32_t type_token_id (0x200000f)
		i32 48; uint32_t java_map_index (0x30)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554448, ; uint32_t type_token_id (0x2000010)
		i32 53; uint32_t java_map_index (0x35)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554449, ; uint32_t type_token_id (0x2000011)
		i32 83; uint32_t java_map_index (0x53)
	}, ; 2
	%struct.TypeMapModuleEntry {
		i32 33554450, ; uint32_t type_token_id (0x2000012)
		i32 180; uint32_t java_map_index (0xb4)
	}, ; 3
	%struct.TypeMapModuleEntry {
		i32 33554451, ; uint32_t type_token_id (0x2000013)
		i32 182; uint32_t java_map_index (0xb6)
	}, ; 4
	%struct.TypeMapModuleEntry {
		i32 33554453, ; uint32_t type_token_id (0x2000015)
		i32 23; uint32_t java_map_index (0x17)
	}, ; 5
	%struct.TypeMapModuleEntry {
		i32 33554454, ; uint32_t type_token_id (0x2000016)
		i32 17; uint32_t java_map_index (0x11)
	}, ; 6
	%struct.TypeMapModuleEntry {
		i32 33554457, ; uint32_t type_token_id (0x2000019)
		i32 193; uint32_t java_map_index (0xc1)
	} ; 7
], align 16

@module1_managed_to_java_duplicates = internal dso_local constant [3 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554452, ; uint32_t type_token_id (0x2000014)
		i32 182; uint32_t java_map_index (0xb6)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554455, ; uint32_t type_token_id (0x2000017)
		i32 17; uint32_t java_map_index (0x11)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554456, ; uint32_t type_token_id (0x2000018)
		i32 23; uint32_t java_map_index (0x17)
	} ; 2
], align 16

@module2_managed_to_java = internal dso_local constant [200 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554552, ; uint32_t type_token_id (0x2000078)
		i32 55; uint32_t java_map_index (0x37)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554554, ; uint32_t type_token_id (0x200007a)
		i32 77; uint32_t java_map_index (0x4d)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554556, ; uint32_t type_token_id (0x200007c)
		i32 162; uint32_t java_map_index (0xa2)
	}, ; 2
	%struct.TypeMapModuleEntry {
		i32 33554557, ; uint32_t type_token_id (0x200007d)
		i32 66; uint32_t java_map_index (0x42)
	}, ; 3
	%struct.TypeMapModuleEntry {
		i32 33554559, ; uint32_t type_token_id (0x200007f)
		i32 7; uint32_t java_map_index (0x7)
	}, ; 4
	%struct.TypeMapModuleEntry {
		i32 33554561, ; uint32_t type_token_id (0x2000081)
		i32 189; uint32_t java_map_index (0xbd)
	}, ; 5
	%struct.TypeMapModuleEntry {
		i32 33554563, ; uint32_t type_token_id (0x2000083)
		i32 104; uint32_t java_map_index (0x68)
	}, ; 6
	%struct.TypeMapModuleEntry {
		i32 33554565, ; uint32_t type_token_id (0x2000085)
		i32 82; uint32_t java_map_index (0x52)
	}, ; 7
	%struct.TypeMapModuleEntry {
		i32 33554567, ; uint32_t type_token_id (0x2000087)
		i32 199; uint32_t java_map_index (0xc7)
	}, ; 8
	%struct.TypeMapModuleEntry {
		i32 33554569, ; uint32_t type_token_id (0x2000089)
		i32 201; uint32_t java_map_index (0xc9)
	}, ; 9
	%struct.TypeMapModuleEntry {
		i32 33554571, ; uint32_t type_token_id (0x200008b)
		i32 172; uint32_t java_map_index (0xac)
	}, ; 10
	%struct.TypeMapModuleEntry {
		i32 33554572, ; uint32_t type_token_id (0x200008c)
		i32 133; uint32_t java_map_index (0x85)
	}, ; 11
	%struct.TypeMapModuleEntry {
		i32 33554573, ; uint32_t type_token_id (0x200008d)
		i32 228; uint32_t java_map_index (0xe4)
	}, ; 12
	%struct.TypeMapModuleEntry {
		i32 33554575, ; uint32_t type_token_id (0x200008f)
		i32 64; uint32_t java_map_index (0x40)
	}, ; 13
	%struct.TypeMapModuleEntry {
		i32 33554576, ; uint32_t type_token_id (0x2000090)
		i32 30; uint32_t java_map_index (0x1e)
	}, ; 14
	%struct.TypeMapModuleEntry {
		i32 33554579, ; uint32_t type_token_id (0x2000093)
		i32 147; uint32_t java_map_index (0x93)
	}, ; 15
	%struct.TypeMapModuleEntry {
		i32 33554581, ; uint32_t type_token_id (0x2000095)
		i32 46; uint32_t java_map_index (0x2e)
	}, ; 16
	%struct.TypeMapModuleEntry {
		i32 33554582, ; uint32_t type_token_id (0x2000096)
		i32 154; uint32_t java_map_index (0x9a)
	}, ; 17
	%struct.TypeMapModuleEntry {
		i32 33554585, ; uint32_t type_token_id (0x2000099)
		i32 216; uint32_t java_map_index (0xd8)
	}, ; 18
	%struct.TypeMapModuleEntry {
		i32 33554586, ; uint32_t type_token_id (0x200009a)
		i32 192; uint32_t java_map_index (0xc0)
	}, ; 19
	%struct.TypeMapModuleEntry {
		i32 33554590, ; uint32_t type_token_id (0x200009e)
		i32 148; uint32_t java_map_index (0x94)
	}, ; 20
	%struct.TypeMapModuleEntry {
		i32 33554597, ; uint32_t type_token_id (0x20000a5)
		i32 57; uint32_t java_map_index (0x39)
	}, ; 21
	%struct.TypeMapModuleEntry {
		i32 33554598, ; uint32_t type_token_id (0x20000a6)
		i32 229; uint32_t java_map_index (0xe5)
	}, ; 22
	%struct.TypeMapModuleEntry {
		i32 33554600, ; uint32_t type_token_id (0x20000a8)
		i32 4; uint32_t java_map_index (0x4)
	}, ; 23
	%struct.TypeMapModuleEntry {
		i32 33554601, ; uint32_t type_token_id (0x20000a9)
		i32 140; uint32_t java_map_index (0x8c)
	}, ; 24
	%struct.TypeMapModuleEntry {
		i32 33554602, ; uint32_t type_token_id (0x20000aa)
		i32 74; uint32_t java_map_index (0x4a)
	}, ; 25
	%struct.TypeMapModuleEntry {
		i32 33554603, ; uint32_t type_token_id (0x20000ab)
		i32 76; uint32_t java_map_index (0x4c)
	}, ; 26
	%struct.TypeMapModuleEntry {
		i32 33554605, ; uint32_t type_token_id (0x20000ad)
		i32 92; uint32_t java_map_index (0x5c)
	}, ; 27
	%struct.TypeMapModuleEntry {
		i32 33554607, ; uint32_t type_token_id (0x20000af)
		i32 150; uint32_t java_map_index (0x96)
	}, ; 28
	%struct.TypeMapModuleEntry {
		i32 33554608, ; uint32_t type_token_id (0x20000b0)
		i32 108; uint32_t java_map_index (0x6c)
	}, ; 29
	%struct.TypeMapModuleEntry {
		i32 33554609, ; uint32_t type_token_id (0x20000b1)
		i32 52; uint32_t java_map_index (0x34)
	}, ; 30
	%struct.TypeMapModuleEntry {
		i32 33554611, ; uint32_t type_token_id (0x20000b3)
		i32 80; uint32_t java_map_index (0x50)
	}, ; 31
	%struct.TypeMapModuleEntry {
		i32 33554612, ; uint32_t type_token_id (0x20000b4)
		i32 72; uint32_t java_map_index (0x48)
	}, ; 32
	%struct.TypeMapModuleEntry {
		i32 33554613, ; uint32_t type_token_id (0x20000b5)
		i32 200; uint32_t java_map_index (0xc8)
	}, ; 33
	%struct.TypeMapModuleEntry {
		i32 33554614, ; uint32_t type_token_id (0x20000b6)
		i32 191; uint32_t java_map_index (0xbf)
	}, ; 34
	%struct.TypeMapModuleEntry {
		i32 33554615, ; uint32_t type_token_id (0x20000b7)
		i32 118; uint32_t java_map_index (0x76)
	}, ; 35
	%struct.TypeMapModuleEntry {
		i32 33554616, ; uint32_t type_token_id (0x20000b8)
		i32 75; uint32_t java_map_index (0x4b)
	}, ; 36
	%struct.TypeMapModuleEntry {
		i32 33554617, ; uint32_t type_token_id (0x20000b9)
		i32 6; uint32_t java_map_index (0x6)
	}, ; 37
	%struct.TypeMapModuleEntry {
		i32 33554618, ; uint32_t type_token_id (0x20000ba)
		i32 43; uint32_t java_map_index (0x2b)
	}, ; 38
	%struct.TypeMapModuleEntry {
		i32 33554621, ; uint32_t type_token_id (0x20000bd)
		i32 166; uint32_t java_map_index (0xa6)
	}, ; 39
	%struct.TypeMapModuleEntry {
		i32 33554623, ; uint32_t type_token_id (0x20000bf)
		i32 187; uint32_t java_map_index (0xbb)
	}, ; 40
	%struct.TypeMapModuleEntry {
		i32 33554624, ; uint32_t type_token_id (0x20000c0)
		i32 190; uint32_t java_map_index (0xbe)
	}, ; 41
	%struct.TypeMapModuleEntry {
		i32 33554627, ; uint32_t type_token_id (0x20000c3)
		i32 56; uint32_t java_map_index (0x38)
	}, ; 42
	%struct.TypeMapModuleEntry {
		i32 33554630, ; uint32_t type_token_id (0x20000c6)
		i32 50; uint32_t java_map_index (0x32)
	}, ; 43
	%struct.TypeMapModuleEntry {
		i32 33554632, ; uint32_t type_token_id (0x20000c8)
		i32 68; uint32_t java_map_index (0x44)
	}, ; 44
	%struct.TypeMapModuleEntry {
		i32 33554634, ; uint32_t type_token_id (0x20000ca)
		i32 65; uint32_t java_map_index (0x41)
	}, ; 45
	%struct.TypeMapModuleEntry {
		i32 33554637, ; uint32_t type_token_id (0x20000cd)
		i32 11; uint32_t java_map_index (0xb)
	}, ; 46
	%struct.TypeMapModuleEntry {
		i32 33554640, ; uint32_t type_token_id (0x20000d0)
		i32 96; uint32_t java_map_index (0x60)
	}, ; 47
	%struct.TypeMapModuleEntry {
		i32 33554645, ; uint32_t type_token_id (0x20000d5)
		i32 194; uint32_t java_map_index (0xc2)
	}, ; 48
	%struct.TypeMapModuleEntry {
		i32 33554646, ; uint32_t type_token_id (0x20000d6)
		i32 111; uint32_t java_map_index (0x6f)
	}, ; 49
	%struct.TypeMapModuleEntry {
		i32 33554647, ; uint32_t type_token_id (0x20000d7)
		i32 63; uint32_t java_map_index (0x3f)
	}, ; 50
	%struct.TypeMapModuleEntry {
		i32 33554648, ; uint32_t type_token_id (0x20000d8)
		i32 139; uint32_t java_map_index (0x8b)
	}, ; 51
	%struct.TypeMapModuleEntry {
		i32 33554649, ; uint32_t type_token_id (0x20000d9)
		i32 27; uint32_t java_map_index (0x1b)
	}, ; 52
	%struct.TypeMapModuleEntry {
		i32 33554650, ; uint32_t type_token_id (0x20000da)
		i32 13; uint32_t java_map_index (0xd)
	}, ; 53
	%struct.TypeMapModuleEntry {
		i32 33554651, ; uint32_t type_token_id (0x20000db)
		i32 2; uint32_t java_map_index (0x2)
	}, ; 54
	%struct.TypeMapModuleEntry {
		i32 33554652, ; uint32_t type_token_id (0x20000dc)
		i32 100; uint32_t java_map_index (0x64)
	}, ; 55
	%struct.TypeMapModuleEntry {
		i32 33554653, ; uint32_t type_token_id (0x20000dd)
		i32 24; uint32_t java_map_index (0x18)
	}, ; 56
	%struct.TypeMapModuleEntry {
		i32 33554654, ; uint32_t type_token_id (0x20000de)
		i32 39; uint32_t java_map_index (0x27)
	}, ; 57
	%struct.TypeMapModuleEntry {
		i32 33554655, ; uint32_t type_token_id (0x20000df)
		i32 87; uint32_t java_map_index (0x57)
	}, ; 58
	%struct.TypeMapModuleEntry {
		i32 33554656, ; uint32_t type_token_id (0x20000e0)
		i32 5; uint32_t java_map_index (0x5)
	}, ; 59
	%struct.TypeMapModuleEntry {
		i32 33554657, ; uint32_t type_token_id (0x20000e1)
		i32 203; uint32_t java_map_index (0xcb)
	}, ; 60
	%struct.TypeMapModuleEntry {
		i32 33554658, ; uint32_t type_token_id (0x20000e2)
		i32 130; uint32_t java_map_index (0x82)
	}, ; 61
	%struct.TypeMapModuleEntry {
		i32 33554659, ; uint32_t type_token_id (0x20000e3)
		i32 110; uint32_t java_map_index (0x6e)
	}, ; 62
	%struct.TypeMapModuleEntry {
		i32 33554661, ; uint32_t type_token_id (0x20000e5)
		i32 97; uint32_t java_map_index (0x61)
	}, ; 63
	%struct.TypeMapModuleEntry {
		i32 33554663, ; uint32_t type_token_id (0x20000e7)
		i32 58; uint32_t java_map_index (0x3a)
	}, ; 64
	%struct.TypeMapModuleEntry {
		i32 33554665, ; uint32_t type_token_id (0x20000e9)
		i32 86; uint32_t java_map_index (0x56)
	}, ; 65
	%struct.TypeMapModuleEntry {
		i32 33554667, ; uint32_t type_token_id (0x20000eb)
		i32 144; uint32_t java_map_index (0x90)
	}, ; 66
	%struct.TypeMapModuleEntry {
		i32 33554668, ; uint32_t type_token_id (0x20000ec)
		i32 178; uint32_t java_map_index (0xb2)
	}, ; 67
	%struct.TypeMapModuleEntry {
		i32 33554669, ; uint32_t type_token_id (0x20000ed)
		i32 34; uint32_t java_map_index (0x22)
	}, ; 68
	%struct.TypeMapModuleEntry {
		i32 33554670, ; uint32_t type_token_id (0x20000ee)
		i32 127; uint32_t java_map_index (0x7f)
	}, ; 69
	%struct.TypeMapModuleEntry {
		i32 33554671, ; uint32_t type_token_id (0x20000ef)
		i32 169; uint32_t java_map_index (0xa9)
	}, ; 70
	%struct.TypeMapModuleEntry {
		i32 33554676, ; uint32_t type_token_id (0x20000f4)
		i32 61; uint32_t java_map_index (0x3d)
	}, ; 71
	%struct.TypeMapModuleEntry {
		i32 33554678, ; uint32_t type_token_id (0x20000f6)
		i32 207; uint32_t java_map_index (0xcf)
	}, ; 72
	%struct.TypeMapModuleEntry {
		i32 33554679, ; uint32_t type_token_id (0x20000f7)
		i32 70; uint32_t java_map_index (0x46)
	}, ; 73
	%struct.TypeMapModuleEntry {
		i32 33554681, ; uint32_t type_token_id (0x20000f9)
		i32 41; uint32_t java_map_index (0x29)
	}, ; 74
	%struct.TypeMapModuleEntry {
		i32 33554682, ; uint32_t type_token_id (0x20000fa)
		i32 69; uint32_t java_map_index (0x45)
	}, ; 75
	%struct.TypeMapModuleEntry {
		i32 33554685, ; uint32_t type_token_id (0x20000fd)
		i32 217; uint32_t java_map_index (0xd9)
	}, ; 76
	%struct.TypeMapModuleEntry {
		i32 33554686, ; uint32_t type_token_id (0x20000fe)
		i32 73; uint32_t java_map_index (0x49)
	}, ; 77
	%struct.TypeMapModuleEntry {
		i32 33554688, ; uint32_t type_token_id (0x2000100)
		i32 126; uint32_t java_map_index (0x7e)
	}, ; 78
	%struct.TypeMapModuleEntry {
		i32 33554690, ; uint32_t type_token_id (0x2000102)
		i32 28; uint32_t java_map_index (0x1c)
	}, ; 79
	%struct.TypeMapModuleEntry {
		i32 33554695, ; uint32_t type_token_id (0x2000107)
		i32 115; uint32_t java_map_index (0x73)
	}, ; 80
	%struct.TypeMapModuleEntry {
		i32 33554696, ; uint32_t type_token_id (0x2000108)
		i32 59; uint32_t java_map_index (0x3b)
	}, ; 81
	%struct.TypeMapModuleEntry {
		i32 33554697, ; uint32_t type_token_id (0x2000109)
		i32 160; uint32_t java_map_index (0xa0)
	}, ; 82
	%struct.TypeMapModuleEntry {
		i32 33554698, ; uint32_t type_token_id (0x200010a)
		i32 155; uint32_t java_map_index (0x9b)
	}, ; 83
	%struct.TypeMapModuleEntry {
		i32 33554699, ; uint32_t type_token_id (0x200010b)
		i32 209; uint32_t java_map_index (0xd1)
	}, ; 84
	%struct.TypeMapModuleEntry {
		i32 33554700, ; uint32_t type_token_id (0x200010c)
		i32 42; uint32_t java_map_index (0x2a)
	}, ; 85
	%struct.TypeMapModuleEntry {
		i32 33554701, ; uint32_t type_token_id (0x200010d)
		i32 119; uint32_t java_map_index (0x77)
	}, ; 86
	%struct.TypeMapModuleEntry {
		i32 33554702, ; uint32_t type_token_id (0x200010e)
		i32 40; uint32_t java_map_index (0x28)
	}, ; 87
	%struct.TypeMapModuleEntry {
		i32 33554704, ; uint32_t type_token_id (0x2000110)
		i32 44; uint32_t java_map_index (0x2c)
	}, ; 88
	%struct.TypeMapModuleEntry {
		i32 33554712, ; uint32_t type_token_id (0x2000118)
		i32 206; uint32_t java_map_index (0xce)
	}, ; 89
	%struct.TypeMapModuleEntry {
		i32 33554713, ; uint32_t type_token_id (0x2000119)
		i32 221; uint32_t java_map_index (0xdd)
	}, ; 90
	%struct.TypeMapModuleEntry {
		i32 33554714, ; uint32_t type_token_id (0x200011a)
		i32 81; uint32_t java_map_index (0x51)
	}, ; 91
	%struct.TypeMapModuleEntry {
		i32 33554715, ; uint32_t type_token_id (0x200011b)
		i32 103; uint32_t java_map_index (0x67)
	}, ; 92
	%struct.TypeMapModuleEntry {
		i32 33554717, ; uint32_t type_token_id (0x200011d)
		i32 138; uint32_t java_map_index (0x8a)
	}, ; 93
	%struct.TypeMapModuleEntry {
		i32 33554718, ; uint32_t type_token_id (0x200011e)
		i32 167; uint32_t java_map_index (0xa7)
	}, ; 94
	%struct.TypeMapModuleEntry {
		i32 33554720, ; uint32_t type_token_id (0x2000120)
		i32 184; uint32_t java_map_index (0xb8)
	}, ; 95
	%struct.TypeMapModuleEntry {
		i32 33554724, ; uint32_t type_token_id (0x2000124)
		i32 212; uint32_t java_map_index (0xd4)
	}, ; 96
	%struct.TypeMapModuleEntry {
		i32 33554725, ; uint32_t type_token_id (0x2000125)
		i32 121; uint32_t java_map_index (0x79)
	}, ; 97
	%struct.TypeMapModuleEntry {
		i32 33554726, ; uint32_t type_token_id (0x2000126)
		i32 105; uint32_t java_map_index (0x69)
	}, ; 98
	%struct.TypeMapModuleEntry {
		i32 33554731, ; uint32_t type_token_id (0x200012b)
		i32 35; uint32_t java_map_index (0x23)
	}, ; 99
	%struct.TypeMapModuleEntry {
		i32 33554754, ; uint32_t type_token_id (0x2000142)
		i32 211; uint32_t java_map_index (0xd3)
	}, ; 100
	%struct.TypeMapModuleEntry {
		i32 33554756, ; uint32_t type_token_id (0x2000144)
		i32 231; uint32_t java_map_index (0xe7)
	}, ; 101
	%struct.TypeMapModuleEntry {
		i32 33554758, ; uint32_t type_token_id (0x2000146)
		i32 93; uint32_t java_map_index (0x5d)
	}, ; 102
	%struct.TypeMapModuleEntry {
		i32 33554767, ; uint32_t type_token_id (0x200014f)
		i32 136; uint32_t java_map_index (0x88)
	}, ; 103
	%struct.TypeMapModuleEntry {
		i32 33554769, ; uint32_t type_token_id (0x2000151)
		i32 210; uint32_t java_map_index (0xd2)
	}, ; 104
	%struct.TypeMapModuleEntry {
		i32 33554770, ; uint32_t type_token_id (0x2000152)
		i32 16; uint32_t java_map_index (0x10)
	}, ; 105
	%struct.TypeMapModuleEntry {
		i32 33554771, ; uint32_t type_token_id (0x2000153)
		i32 12; uint32_t java_map_index (0xc)
	}, ; 106
	%struct.TypeMapModuleEntry {
		i32 33554783, ; uint32_t type_token_id (0x200015f)
		i32 176; uint32_t java_map_index (0xb0)
	}, ; 107
	%struct.TypeMapModuleEntry {
		i32 33554793, ; uint32_t type_token_id (0x2000169)
		i32 204; uint32_t java_map_index (0xcc)
	}, ; 108
	%struct.TypeMapModuleEntry {
		i32 33554794, ; uint32_t type_token_id (0x200016a)
		i32 132; uint32_t java_map_index (0x84)
	}, ; 109
	%struct.TypeMapModuleEntry {
		i32 33554795, ; uint32_t type_token_id (0x200016b)
		i32 224; uint32_t java_map_index (0xe0)
	}, ; 110
	%struct.TypeMapModuleEntry {
		i32 33554796, ; uint32_t type_token_id (0x200016c)
		i32 215; uint32_t java_map_index (0xd7)
	}, ; 111
	%struct.TypeMapModuleEntry {
		i32 33554797, ; uint32_t type_token_id (0x200016d)
		i32 131; uint32_t java_map_index (0x83)
	}, ; 112
	%struct.TypeMapModuleEntry {
		i32 33554800, ; uint32_t type_token_id (0x2000170)
		i32 109; uint32_t java_map_index (0x6d)
	}, ; 113
	%struct.TypeMapModuleEntry {
		i32 33554803, ; uint32_t type_token_id (0x2000173)
		i32 62; uint32_t java_map_index (0x3e)
	}, ; 114
	%struct.TypeMapModuleEntry {
		i32 33554805, ; uint32_t type_token_id (0x2000175)
		i32 157; uint32_t java_map_index (0x9d)
	}, ; 115
	%struct.TypeMapModuleEntry {
		i32 33554806, ; uint32_t type_token_id (0x2000176)
		i32 134; uint32_t java_map_index (0x86)
	}, ; 116
	%struct.TypeMapModuleEntry {
		i32 33554808, ; uint32_t type_token_id (0x2000178)
		i32 84; uint32_t java_map_index (0x54)
	}, ; 117
	%struct.TypeMapModuleEntry {
		i32 33554810, ; uint32_t type_token_id (0x200017a)
		i32 21; uint32_t java_map_index (0x15)
	}, ; 118
	%struct.TypeMapModuleEntry {
		i32 33554811, ; uint32_t type_token_id (0x200017b)
		i32 141; uint32_t java_map_index (0x8d)
	}, ; 119
	%struct.TypeMapModuleEntry {
		i32 33554813, ; uint32_t type_token_id (0x200017d)
		i32 98; uint32_t java_map_index (0x62)
	}, ; 120
	%struct.TypeMapModuleEntry {
		i32 33554814, ; uint32_t type_token_id (0x200017e)
		i32 208; uint32_t java_map_index (0xd0)
	}, ; 121
	%struct.TypeMapModuleEntry {
		i32 33554817, ; uint32_t type_token_id (0x2000181)
		i32 8; uint32_t java_map_index (0x8)
	}, ; 122
	%struct.TypeMapModuleEntry {
		i32 33554818, ; uint32_t type_token_id (0x2000182)
		i32 22; uint32_t java_map_index (0x16)
	}, ; 123
	%struct.TypeMapModuleEntry {
		i32 33554821, ; uint32_t type_token_id (0x2000185)
		i32 85; uint32_t java_map_index (0x55)
	}, ; 124
	%struct.TypeMapModuleEntry {
		i32 33554822, ; uint32_t type_token_id (0x2000186)
		i32 47; uint32_t java_map_index (0x2f)
	}, ; 125
	%struct.TypeMapModuleEntry {
		i32 33554824, ; uint32_t type_token_id (0x2000188)
		i32 37; uint32_t java_map_index (0x25)
	}, ; 126
	%struct.TypeMapModuleEntry {
		i32 33554825, ; uint32_t type_token_id (0x2000189)
		i32 36; uint32_t java_map_index (0x24)
	}, ; 127
	%struct.TypeMapModuleEntry {
		i32 33554826, ; uint32_t type_token_id (0x200018a)
		i32 161; uint32_t java_map_index (0xa1)
	}, ; 128
	%struct.TypeMapModuleEntry {
		i32 33554827, ; uint32_t type_token_id (0x200018b)
		i32 213; uint32_t java_map_index (0xd5)
	}, ; 129
	%struct.TypeMapModuleEntry {
		i32 33554829, ; uint32_t type_token_id (0x200018d)
		i32 227; uint32_t java_map_index (0xe3)
	}, ; 130
	%struct.TypeMapModuleEntry {
		i32 33554831, ; uint32_t type_token_id (0x200018f)
		i32 177; uint32_t java_map_index (0xb1)
	}, ; 131
	%struct.TypeMapModuleEntry {
		i32 33554833, ; uint32_t type_token_id (0x2000191)
		i32 159; uint32_t java_map_index (0x9f)
	}, ; 132
	%struct.TypeMapModuleEntry {
		i32 33554836, ; uint32_t type_token_id (0x2000194)
		i32 123; uint32_t java_map_index (0x7b)
	}, ; 133
	%struct.TypeMapModuleEntry {
		i32 33554837, ; uint32_t type_token_id (0x2000195)
		i32 143; uint32_t java_map_index (0x8f)
	}, ; 134
	%struct.TypeMapModuleEntry {
		i32 33554840, ; uint32_t type_token_id (0x2000198)
		i32 218; uint32_t java_map_index (0xda)
	}, ; 135
	%struct.TypeMapModuleEntry {
		i32 33554842, ; uint32_t type_token_id (0x200019a)
		i32 18; uint32_t java_map_index (0x12)
	}, ; 136
	%struct.TypeMapModuleEntry {
		i32 33554844, ; uint32_t type_token_id (0x200019c)
		i32 128; uint32_t java_map_index (0x80)
	}, ; 137
	%struct.TypeMapModuleEntry {
		i32 33554845, ; uint32_t type_token_id (0x200019d)
		i32 10; uint32_t java_map_index (0xa)
	}, ; 138
	%struct.TypeMapModuleEntry {
		i32 33554847, ; uint32_t type_token_id (0x200019f)
		i32 1; uint32_t java_map_index (0x1)
	}, ; 139
	%struct.TypeMapModuleEntry {
		i32 33554849, ; uint32_t type_token_id (0x20001a1)
		i32 107; uint32_t java_map_index (0x6b)
	}, ; 140
	%struct.TypeMapModuleEntry {
		i32 33554851, ; uint32_t type_token_id (0x20001a3)
		i32 220; uint32_t java_map_index (0xdc)
	}, ; 141
	%struct.TypeMapModuleEntry {
		i32 33554852, ; uint32_t type_token_id (0x20001a4)
		i32 32; uint32_t java_map_index (0x20)
	}, ; 142
	%struct.TypeMapModuleEntry {
		i32 33554853, ; uint32_t type_token_id (0x20001a5)
		i32 137; uint32_t java_map_index (0x89)
	}, ; 143
	%struct.TypeMapModuleEntry {
		i32 33554855, ; uint32_t type_token_id (0x20001a7)
		i32 170; uint32_t java_map_index (0xaa)
	}, ; 144
	%struct.TypeMapModuleEntry {
		i32 33554857, ; uint32_t type_token_id (0x20001a9)
		i32 94; uint32_t java_map_index (0x5e)
	}, ; 145
	%struct.TypeMapModuleEntry {
		i32 33554859, ; uint32_t type_token_id (0x20001ab)
		i32 14; uint32_t java_map_index (0xe)
	}, ; 146
	%struct.TypeMapModuleEntry {
		i32 33554860, ; uint32_t type_token_id (0x20001ac)
		i32 31; uint32_t java_map_index (0x1f)
	}, ; 147
	%struct.TypeMapModuleEntry {
		i32 33554862, ; uint32_t type_token_id (0x20001ae)
		i32 49; uint32_t java_map_index (0x31)
	}, ; 148
	%struct.TypeMapModuleEntry {
		i32 33554863, ; uint32_t type_token_id (0x20001af)
		i32 19; uint32_t java_map_index (0x13)
	}, ; 149
	%struct.TypeMapModuleEntry {
		i32 33554864, ; uint32_t type_token_id (0x20001b0)
		i32 26; uint32_t java_map_index (0x1a)
	}, ; 150
	%struct.TypeMapModuleEntry {
		i32 33554865, ; uint32_t type_token_id (0x20001b1)
		i32 222; uint32_t java_map_index (0xde)
	}, ; 151
	%struct.TypeMapModuleEntry {
		i32 33554866, ; uint32_t type_token_id (0x20001b2)
		i32 54; uint32_t java_map_index (0x36)
	}, ; 152
	%struct.TypeMapModuleEntry {
		i32 33554868, ; uint32_t type_token_id (0x20001b4)
		i32 202; uint32_t java_map_index (0xca)
	}, ; 153
	%struct.TypeMapModuleEntry {
		i32 33554869, ; uint32_t type_token_id (0x20001b5)
		i32 151; uint32_t java_map_index (0x97)
	}, ; 154
	%struct.TypeMapModuleEntry {
		i32 33554870, ; uint32_t type_token_id (0x20001b6)
		i32 168; uint32_t java_map_index (0xa8)
	}, ; 155
	%struct.TypeMapModuleEntry {
		i32 33554871, ; uint32_t type_token_id (0x20001b7)
		i32 113; uint32_t java_map_index (0x71)
	}, ; 156
	%struct.TypeMapModuleEntry {
		i32 33554872, ; uint32_t type_token_id (0x20001b8)
		i32 71; uint32_t java_map_index (0x47)
	}, ; 157
	%struct.TypeMapModuleEntry {
		i32 33554875, ; uint32_t type_token_id (0x20001bb)
		i32 33; uint32_t java_map_index (0x21)
	}, ; 158
	%struct.TypeMapModuleEntry {
		i32 33554876, ; uint32_t type_token_id (0x20001bc)
		i32 88; uint32_t java_map_index (0x58)
	}, ; 159
	%struct.TypeMapModuleEntry {
		i32 33554877, ; uint32_t type_token_id (0x20001bd)
		i32 15; uint32_t java_map_index (0xf)
	}, ; 160
	%struct.TypeMapModuleEntry {
		i32 33554878, ; uint32_t type_token_id (0x20001be)
		i32 223; uint32_t java_map_index (0xdf)
	}, ; 161
	%struct.TypeMapModuleEntry {
		i32 33554880, ; uint32_t type_token_id (0x20001c0)
		i32 29; uint32_t java_map_index (0x1d)
	}, ; 162
	%struct.TypeMapModuleEntry {
		i32 33554882, ; uint32_t type_token_id (0x20001c2)
		i32 90; uint32_t java_map_index (0x5a)
	}, ; 163
	%struct.TypeMapModuleEntry {
		i32 33554883, ; uint32_t type_token_id (0x20001c3)
		i32 25; uint32_t java_map_index (0x19)
	}, ; 164
	%struct.TypeMapModuleEntry {
		i32 33554884, ; uint32_t type_token_id (0x20001c4)
		i32 214; uint32_t java_map_index (0xd6)
	}, ; 165
	%struct.TypeMapModuleEntry {
		i32 33554886, ; uint32_t type_token_id (0x20001c6)
		i32 45; uint32_t java_map_index (0x2d)
	}, ; 166
	%struct.TypeMapModuleEntry {
		i32 33554887, ; uint32_t type_token_id (0x20001c7)
		i32 164; uint32_t java_map_index (0xa4)
	}, ; 167
	%struct.TypeMapModuleEntry {
		i32 33554888, ; uint32_t type_token_id (0x20001c8)
		i32 195; uint32_t java_map_index (0xc3)
	}, ; 168
	%struct.TypeMapModuleEntry {
		i32 33554890, ; uint32_t type_token_id (0x20001ca)
		i32 102; uint32_t java_map_index (0x66)
	}, ; 169
	%struct.TypeMapModuleEntry {
		i32 33554891, ; uint32_t type_token_id (0x20001cb)
		i32 181; uint32_t java_map_index (0xb5)
	}, ; 170
	%struct.TypeMapModuleEntry {
		i32 33554892, ; uint32_t type_token_id (0x20001cc)
		i32 91; uint32_t java_map_index (0x5b)
	}, ; 171
	%struct.TypeMapModuleEntry {
		i32 33554893, ; uint32_t type_token_id (0x20001cd)
		i32 60; uint32_t java_map_index (0x3c)
	}, ; 172
	%struct.TypeMapModuleEntry {
		i32 33554894, ; uint32_t type_token_id (0x20001ce)
		i32 174; uint32_t java_map_index (0xae)
	}, ; 173
	%struct.TypeMapModuleEntry {
		i32 33554895, ; uint32_t type_token_id (0x20001cf)
		i32 219; uint32_t java_map_index (0xdb)
	}, ; 174
	%struct.TypeMapModuleEntry {
		i32 33554896, ; uint32_t type_token_id (0x20001d0)
		i32 78; uint32_t java_map_index (0x4e)
	}, ; 175
	%struct.TypeMapModuleEntry {
		i32 33554898, ; uint32_t type_token_id (0x20001d2)
		i32 67; uint32_t java_map_index (0x43)
	}, ; 176
	%struct.TypeMapModuleEntry {
		i32 33554899, ; uint32_t type_token_id (0x20001d3)
		i32 9; uint32_t java_map_index (0x9)
	}, ; 177
	%struct.TypeMapModuleEntry {
		i32 33554900, ; uint32_t type_token_id (0x20001d4)
		i32 230; uint32_t java_map_index (0xe6)
	}, ; 178
	%struct.TypeMapModuleEntry {
		i32 33554901, ; uint32_t type_token_id (0x20001d5)
		i32 197; uint32_t java_map_index (0xc5)
	}, ; 179
	%struct.TypeMapModuleEntry {
		i32 33554903, ; uint32_t type_token_id (0x20001d7)
		i32 185; uint32_t java_map_index (0xb9)
	}, ; 180
	%struct.TypeMapModuleEntry {
		i32 33554906, ; uint32_t type_token_id (0x20001da)
		i32 95; uint32_t java_map_index (0x5f)
	}, ; 181
	%struct.TypeMapModuleEntry {
		i32 33554907, ; uint32_t type_token_id (0x20001db)
		i32 145; uint32_t java_map_index (0x91)
	}, ; 182
	%struct.TypeMapModuleEntry {
		i32 33554908, ; uint32_t type_token_id (0x20001dc)
		i32 142; uint32_t java_map_index (0x8e)
	}, ; 183
	%struct.TypeMapModuleEntry {
		i32 33554909, ; uint32_t type_token_id (0x20001dd)
		i32 149; uint32_t java_map_index (0x95)
	}, ; 184
	%struct.TypeMapModuleEntry {
		i32 33554910, ; uint32_t type_token_id (0x20001de)
		i32 99; uint32_t java_map_index (0x63)
	}, ; 185
	%struct.TypeMapModuleEntry {
		i32 33554912, ; uint32_t type_token_id (0x20001e0)
		i32 120; uint32_t java_map_index (0x78)
	}, ; 186
	%struct.TypeMapModuleEntry {
		i32 33554913, ; uint32_t type_token_id (0x20001e1)
		i32 116; uint32_t java_map_index (0x74)
	}, ; 187
	%struct.TypeMapModuleEntry {
		i32 33554914, ; uint32_t type_token_id (0x20001e2)
		i32 146; uint32_t java_map_index (0x92)
	}, ; 188
	%struct.TypeMapModuleEntry {
		i32 33554916, ; uint32_t type_token_id (0x20001e4)
		i32 173; uint32_t java_map_index (0xad)
	}, ; 189
	%struct.TypeMapModuleEntry {
		i32 33554917, ; uint32_t type_token_id (0x20001e5)
		i32 165; uint32_t java_map_index (0xa5)
	}, ; 190
	%struct.TypeMapModuleEntry {
		i32 33554918, ; uint32_t type_token_id (0x20001e6)
		i32 114; uint32_t java_map_index (0x72)
	}, ; 191
	%struct.TypeMapModuleEntry {
		i32 33554919, ; uint32_t type_token_id (0x20001e7)
		i32 125; uint32_t java_map_index (0x7d)
	}, ; 192
	%struct.TypeMapModuleEntry {
		i32 33554920, ; uint32_t type_token_id (0x20001e8)
		i32 188; uint32_t java_map_index (0xbc)
	}, ; 193
	%struct.TypeMapModuleEntry {
		i32 33554921, ; uint32_t type_token_id (0x20001e9)
		i32 0; uint32_t java_map_index (0x0)
	}, ; 194
	%struct.TypeMapModuleEntry {
		i32 33554923, ; uint32_t type_token_id (0x20001eb)
		i32 112; uint32_t java_map_index (0x70)
	}, ; 195
	%struct.TypeMapModuleEntry {
		i32 33554924, ; uint32_t type_token_id (0x20001ec)
		i32 135; uint32_t java_map_index (0x87)
	}, ; 196
	%struct.TypeMapModuleEntry {
		i32 33554925, ; uint32_t type_token_id (0x20001ed)
		i32 186; uint32_t java_map_index (0xba)
	}, ; 197
	%struct.TypeMapModuleEntry {
		i32 33554926, ; uint32_t type_token_id (0x20001ee)
		i32 122; uint32_t java_map_index (0x7a)
	}, ; 198
	%struct.TypeMapModuleEntry {
		i32 33554941, ; uint32_t type_token_id (0x20001fd)
		i32 158; uint32_t java_map_index (0x9e)
	} ; 199
], align 16

@module2_managed_to_java_duplicates = internal dso_local constant [77 x %struct.TypeMapModuleEntry] [
	%struct.TypeMapModuleEntry {
		i32 33554553, ; uint32_t type_token_id (0x2000079)
		i32 55; uint32_t java_map_index (0x37)
	}, ; 0
	%struct.TypeMapModuleEntry {
		i32 33554555, ; uint32_t type_token_id (0x200007b)
		i32 77; uint32_t java_map_index (0x4d)
	}, ; 1
	%struct.TypeMapModuleEntry {
		i32 33554558, ; uint32_t type_token_id (0x200007e)
		i32 66; uint32_t java_map_index (0x42)
	}, ; 2
	%struct.TypeMapModuleEntry {
		i32 33554560, ; uint32_t type_token_id (0x2000080)
		i32 7; uint32_t java_map_index (0x7)
	}, ; 3
	%struct.TypeMapModuleEntry {
		i32 33554562, ; uint32_t type_token_id (0x2000082)
		i32 189; uint32_t java_map_index (0xbd)
	}, ; 4
	%struct.TypeMapModuleEntry {
		i32 33554564, ; uint32_t type_token_id (0x2000084)
		i32 104; uint32_t java_map_index (0x68)
	}, ; 5
	%struct.TypeMapModuleEntry {
		i32 33554566, ; uint32_t type_token_id (0x2000086)
		i32 82; uint32_t java_map_index (0x52)
	}, ; 6
	%struct.TypeMapModuleEntry {
		i32 33554568, ; uint32_t type_token_id (0x2000088)
		i32 199; uint32_t java_map_index (0xc7)
	}, ; 7
	%struct.TypeMapModuleEntry {
		i32 33554570, ; uint32_t type_token_id (0x200008a)
		i32 201; uint32_t java_map_index (0xc9)
	}, ; 8
	%struct.TypeMapModuleEntry {
		i32 33554574, ; uint32_t type_token_id (0x200008e)
		i32 228; uint32_t java_map_index (0xe4)
	}, ; 9
	%struct.TypeMapModuleEntry {
		i32 33554578, ; uint32_t type_token_id (0x2000092)
		i32 30; uint32_t java_map_index (0x1e)
	}, ; 10
	%struct.TypeMapModuleEntry {
		i32 33554580, ; uint32_t type_token_id (0x2000094)
		i32 147; uint32_t java_map_index (0x93)
	}, ; 11
	%struct.TypeMapModuleEntry {
		i32 33554583, ; uint32_t type_token_id (0x2000097)
		i32 154; uint32_t java_map_index (0x9a)
	}, ; 12
	%struct.TypeMapModuleEntry {
		i32 33554587, ; uint32_t type_token_id (0x200009b)
		i32 192; uint32_t java_map_index (0xc0)
	}, ; 13
	%struct.TypeMapModuleEntry {
		i32 33554596, ; uint32_t type_token_id (0x20000a4)
		i32 46; uint32_t java_map_index (0x2e)
	}, ; 14
	%struct.TypeMapModuleEntry {
		i32 33554599, ; uint32_t type_token_id (0x20000a7)
		i32 229; uint32_t java_map_index (0xe5)
	}, ; 15
	%struct.TypeMapModuleEntry {
		i32 33554604, ; uint32_t type_token_id (0x20000ac)
		i32 76; uint32_t java_map_index (0x4c)
	}, ; 16
	%struct.TypeMapModuleEntry {
		i32 33554606, ; uint32_t type_token_id (0x20000ae)
		i32 92; uint32_t java_map_index (0x5c)
	}, ; 17
	%struct.TypeMapModuleEntry {
		i32 33554610, ; uint32_t type_token_id (0x20000b2)
		i32 52; uint32_t java_map_index (0x34)
	}, ; 18
	%struct.TypeMapModuleEntry {
		i32 33554619, ; uint32_t type_token_id (0x20000bb)
		i32 46; uint32_t java_map_index (0x2e)
	}, ; 19
	%struct.TypeMapModuleEntry {
		i32 33554620, ; uint32_t type_token_id (0x20000bc)
		i32 229; uint32_t java_map_index (0xe5)
	}, ; 20
	%struct.TypeMapModuleEntry {
		i32 33554622, ; uint32_t type_token_id (0x20000be)
		i32 166; uint32_t java_map_index (0xa6)
	}, ; 21
	%struct.TypeMapModuleEntry {
		i32 33554625, ; uint32_t type_token_id (0x20000c1)
		i32 190; uint32_t java_map_index (0xbe)
	}, ; 22
	%struct.TypeMapModuleEntry {
		i32 33554628, ; uint32_t type_token_id (0x20000c4)
		i32 56; uint32_t java_map_index (0x38)
	}, ; 23
	%struct.TypeMapModuleEntry {
		i32 33554631, ; uint32_t type_token_id (0x20000c7)
		i32 50; uint32_t java_map_index (0x32)
	}, ; 24
	%struct.TypeMapModuleEntry {
		i32 33554633, ; uint32_t type_token_id (0x20000c9)
		i32 68; uint32_t java_map_index (0x44)
	}, ; 25
	%struct.TypeMapModuleEntry {
		i32 33554635, ; uint32_t type_token_id (0x20000cb)
		i32 65; uint32_t java_map_index (0x41)
	}, ; 26
	%struct.TypeMapModuleEntry {
		i32 33554638, ; uint32_t type_token_id (0x20000ce)
		i32 11; uint32_t java_map_index (0xb)
	}, ; 27
	%struct.TypeMapModuleEntry {
		i32 33554641, ; uint32_t type_token_id (0x20000d1)
		i32 96; uint32_t java_map_index (0x60)
	}, ; 28
	%struct.TypeMapModuleEntry {
		i32 33554660, ; uint32_t type_token_id (0x20000e4)
		i32 110; uint32_t java_map_index (0x6e)
	}, ; 29
	%struct.TypeMapModuleEntry {
		i32 33554662, ; uint32_t type_token_id (0x20000e6)
		i32 97; uint32_t java_map_index (0x61)
	}, ; 30
	%struct.TypeMapModuleEntry {
		i32 33554664, ; uint32_t type_token_id (0x20000e8)
		i32 58; uint32_t java_map_index (0x3a)
	}, ; 31
	%struct.TypeMapModuleEntry {
		i32 33554666, ; uint32_t type_token_id (0x20000ea)
		i32 86; uint32_t java_map_index (0x56)
	}, ; 32
	%struct.TypeMapModuleEntry {
		i32 33554677, ; uint32_t type_token_id (0x20000f5)
		i32 61; uint32_t java_map_index (0x3d)
	}, ; 33
	%struct.TypeMapModuleEntry {
		i32 33554680, ; uint32_t type_token_id (0x20000f8)
		i32 70; uint32_t java_map_index (0x46)
	}, ; 34
	%struct.TypeMapModuleEntry {
		i32 33554687, ; uint32_t type_token_id (0x20000ff)
		i32 73; uint32_t java_map_index (0x49)
	}, ; 35
	%struct.TypeMapModuleEntry {
		i32 33554689, ; uint32_t type_token_id (0x2000101)
		i32 126; uint32_t java_map_index (0x7e)
	}, ; 36
	%struct.TypeMapModuleEntry {
		i32 33554691, ; uint32_t type_token_id (0x2000103)
		i32 28; uint32_t java_map_index (0x1c)
	}, ; 37
	%struct.TypeMapModuleEntry {
		i32 33554703, ; uint32_t type_token_id (0x200010f)
		i32 40; uint32_t java_map_index (0x28)
	}, ; 38
	%struct.TypeMapModuleEntry {
		i32 33554716, ; uint32_t type_token_id (0x200011c)
		i32 103; uint32_t java_map_index (0x67)
	}, ; 39
	%struct.TypeMapModuleEntry {
		i32 33554719, ; uint32_t type_token_id (0x200011f)
		i32 167; uint32_t java_map_index (0xa7)
	}, ; 40
	%struct.TypeMapModuleEntry {
		i32 33554727, ; uint32_t type_token_id (0x2000127)
		i32 212; uint32_t java_map_index (0xd4)
	}, ; 41
	%struct.TypeMapModuleEntry {
		i32 33554757, ; uint32_t type_token_id (0x2000145)
		i32 231; uint32_t java_map_index (0xe7)
	}, ; 42
	%struct.TypeMapModuleEntry {
		i32 33554763, ; uint32_t type_token_id (0x200014b)
		i32 93; uint32_t java_map_index (0x5d)
	}, ; 43
	%struct.TypeMapModuleEntry {
		i32 33554768, ; uint32_t type_token_id (0x2000150)
		i32 136; uint32_t java_map_index (0x88)
	}, ; 44
	%struct.TypeMapModuleEntry {
		i32 33554772, ; uint32_t type_token_id (0x2000154)
		i32 12; uint32_t java_map_index (0xc)
	}, ; 45
	%struct.TypeMapModuleEntry {
		i32 33554801, ; uint32_t type_token_id (0x2000171)
		i32 109; uint32_t java_map_index (0x6d)
	}, ; 46
	%struct.TypeMapModuleEntry {
		i32 33554804, ; uint32_t type_token_id (0x2000174)
		i32 62; uint32_t java_map_index (0x3e)
	}, ; 47
	%struct.TypeMapModuleEntry {
		i32 33554807, ; uint32_t type_token_id (0x2000177)
		i32 134; uint32_t java_map_index (0x86)
	}, ; 48
	%struct.TypeMapModuleEntry {
		i32 33554809, ; uint32_t type_token_id (0x2000179)
		i32 84; uint32_t java_map_index (0x54)
	}, ; 49
	%struct.TypeMapModuleEntry {
		i32 33554812, ; uint32_t type_token_id (0x200017c)
		i32 141; uint32_t java_map_index (0x8d)
	}, ; 50
	%struct.TypeMapModuleEntry {
		i32 33554815, ; uint32_t type_token_id (0x200017f)
		i32 208; uint32_t java_map_index (0xd0)
	}, ; 51
	%struct.TypeMapModuleEntry {
		i32 33554819, ; uint32_t type_token_id (0x2000183)
		i32 22; uint32_t java_map_index (0x16)
	}, ; 52
	%struct.TypeMapModuleEntry {
		i32 33554823, ; uint32_t type_token_id (0x2000187)
		i32 47; uint32_t java_map_index (0x2f)
	}, ; 53
	%struct.TypeMapModuleEntry {
		i32 33554828, ; uint32_t type_token_id (0x200018c)
		i32 213; uint32_t java_map_index (0xd5)
	}, ; 54
	%struct.TypeMapModuleEntry {
		i32 33554830, ; uint32_t type_token_id (0x200018e)
		i32 227; uint32_t java_map_index (0xe3)
	}, ; 55
	%struct.TypeMapModuleEntry {
		i32 33554832, ; uint32_t type_token_id (0x2000190)
		i32 177; uint32_t java_map_index (0xb1)
	}, ; 56
	%struct.TypeMapModuleEntry {
		i32 33554841, ; uint32_t type_token_id (0x2000199)
		i32 218; uint32_t java_map_index (0xda)
	}, ; 57
	%struct.TypeMapModuleEntry {
		i32 33554843, ; uint32_t type_token_id (0x200019b)
		i32 18; uint32_t java_map_index (0x12)
	}, ; 58
	%struct.TypeMapModuleEntry {
		i32 33554846, ; uint32_t type_token_id (0x200019e)
		i32 10; uint32_t java_map_index (0xa)
	}, ; 59
	%struct.TypeMapModuleEntry {
		i32 33554848, ; uint32_t type_token_id (0x20001a0)
		i32 1; uint32_t java_map_index (0x1)
	}, ; 60
	%struct.TypeMapModuleEntry {
		i32 33554850, ; uint32_t type_token_id (0x20001a2)
		i32 107; uint32_t java_map_index (0x6b)
	}, ; 61
	%struct.TypeMapModuleEntry {
		i32 33554854, ; uint32_t type_token_id (0x20001a6)
		i32 137; uint32_t java_map_index (0x89)
	}, ; 62
	%struct.TypeMapModuleEntry {
		i32 33554856, ; uint32_t type_token_id (0x20001a8)
		i32 170; uint32_t java_map_index (0xaa)
	}, ; 63
	%struct.TypeMapModuleEntry {
		i32 33554858, ; uint32_t type_token_id (0x20001aa)
		i32 94; uint32_t java_map_index (0x5e)
	}, ; 64
	%struct.TypeMapModuleEntry {
		i32 33554861, ; uint32_t type_token_id (0x20001ad)
		i32 31; uint32_t java_map_index (0x1f)
	}, ; 65
	%struct.TypeMapModuleEntry {
		i32 33554867, ; uint32_t type_token_id (0x20001b3)
		i32 54; uint32_t java_map_index (0x36)
	}, ; 66
	%struct.TypeMapModuleEntry {
		i32 33554873, ; uint32_t type_token_id (0x20001b9)
		i32 71; uint32_t java_map_index (0x47)
	}, ; 67
	%struct.TypeMapModuleEntry {
		i32 33554879, ; uint32_t type_token_id (0x20001bf)
		i32 223; uint32_t java_map_index (0xdf)
	}, ; 68
	%struct.TypeMapModuleEntry {
		i32 33554881, ; uint32_t type_token_id (0x20001c1)
		i32 29; uint32_t java_map_index (0x1d)
	}, ; 69
	%struct.TypeMapModuleEntry {
		i32 33554885, ; uint32_t type_token_id (0x20001c5)
		i32 214; uint32_t java_map_index (0xd6)
	}, ; 70
	%struct.TypeMapModuleEntry {
		i32 33554889, ; uint32_t type_token_id (0x20001c9)
		i32 195; uint32_t java_map_index (0xc3)
	}, ; 71
	%struct.TypeMapModuleEntry {
		i32 33554897, ; uint32_t type_token_id (0x20001d1)
		i32 78; uint32_t java_map_index (0x4e)
	}, ; 72
	%struct.TypeMapModuleEntry {
		i32 33554902, ; uint32_t type_token_id (0x20001d6)
		i32 197; uint32_t java_map_index (0xc5)
	}, ; 73
	%struct.TypeMapModuleEntry {
		i32 33554904, ; uint32_t type_token_id (0x20001d8)
		i32 185; uint32_t java_map_index (0xb9)
	}, ; 74
	%struct.TypeMapModuleEntry {
		i32 33554911, ; uint32_t type_token_id (0x20001df)
		i32 99; uint32_t java_map_index (0x63)
	}, ; 75
	%struct.TypeMapModuleEntry {
		i32 33554915, ; uint32_t type_token_id (0x20001e3)
		i32 146; uint32_t java_map_index (0x92)
	} ; 76
], align 16

; Java to managed map
@map_java = dso_local local_unnamed_addr constant [232 x %struct.TypeMapJava] [
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554921, ; uint32_t type_token_id (0x20001e9)
		i32 226; uint32_t java_name_index (0xe2)
	}, ; 0
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 171; uint32_t java_name_index (0xab)
	}, ; 1
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554651, ; uint32_t type_token_id (0x20000db)
		i32 86; uint32_t java_name_index (0x56)
	}, ; 2
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554466, ; uint32_t type_token_id (0x2000022)
		i32 19; uint32_t java_name_index (0x13)
	}, ; 3
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554600, ; uint32_t type_token_id (0x20000a8)
		i32 55; uint32_t java_name_index (0x37)
	}, ; 4
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554656, ; uint32_t type_token_id (0x20000e0)
		i32 91; uint32_t java_name_index (0x5b)
	}, ; 5
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554617, ; uint32_t type_token_id (0x20000b9)
		i32 69; uint32_t java_name_index (0x45)
	}, ; 6
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554559, ; uint32_t type_token_id (0x200007f)
		i32 36; uint32_t java_name_index (0x24)
	}, ; 7
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554817, ; uint32_t type_token_id (0x2000181)
		i32 154; uint32_t java_name_index (0x9a)
	}, ; 8
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554899, ; uint32_t type_token_id (0x20001d3)
		i32 209; uint32_t java_name_index (0xd1)
	}, ; 9
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 170; uint32_t java_name_index (0xaa)
	}, ; 10
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 78; uint32_t java_name_index (0x4e)
	}, ; 11
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554771, ; uint32_t type_token_id (0x2000153)
		i32 138; uint32_t java_name_index (0x8a)
	}, ; 12
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554650, ; uint32_t type_token_id (0x20000da)
		i32 85; uint32_t java_name_index (0x55)
	}, ; 13
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554859, ; uint32_t type_token_id (0x20001ab)
		i32 178; uint32_t java_name_index (0xb2)
	}, ; 14
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554877, ; uint32_t type_token_id (0x20001bd)
		i32 192; uint32_t java_name_index (0xc0)
	}, ; 15
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554770, ; uint32_t type_token_id (0x2000152)
		i32 137; uint32_t java_name_index (0x89)
	}, ; 16
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 30; uint32_t java_name_index (0x1e)
	}, ; 17
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 168; uint32_t java_name_index (0xa8)
	}, ; 18
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554863, ; uint32_t type_token_id (0x20001af)
		i32 181; uint32_t java_name_index (0xb5)
	}, ; 19
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554468, ; uint32_t type_token_id (0x2000024)
		i32 21; uint32_t java_name_index (0x15)
	}, ; 20
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554810, ; uint32_t type_token_id (0x200017a)
		i32 150; uint32_t java_name_index (0x96)
	}, ; 21
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 155; uint32_t java_name_index (0x9b)
	}, ; 22
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554453, ; uint32_t type_token_id (0x2000015)
		i32 29; uint32_t java_name_index (0x1d)
	}, ; 23
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554653, ; uint32_t type_token_id (0x20000dd)
		i32 88; uint32_t java_name_index (0x58)
	}, ; 24
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554883, ; uint32_t type_token_id (0x20001c3)
		i32 196; uint32_t java_name_index (0xc4)
	}, ; 25
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554864, ; uint32_t type_token_id (0x20001b0)
		i32 182; uint32_t java_name_index (0xb6)
	}, ; 26
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554649, ; uint32_t type_token_id (0x20000d9)
		i32 84; uint32_t java_name_index (0x54)
	}, ; 27
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 111; uint32_t java_name_index (0x6f)
	}, ; 28
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554880, ; uint32_t type_token_id (0x20001c0)
		i32 194; uint32_t java_name_index (0xc2)
	}, ; 29
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554576, ; uint32_t type_token_id (0x2000090)
		i32 46; uint32_t java_name_index (0x2e)
	}, ; 30
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554860, ; uint32_t type_token_id (0x20001ac)
		i32 179; uint32_t java_name_index (0xb3)
	}, ; 31
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554852, ; uint32_t type_token_id (0x20001a4)
		i32 174; uint32_t java_name_index (0xae)
	}, ; 32
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554875, ; uint32_t type_token_id (0x20001bb)
		i32 190; uint32_t java_name_index (0xbe)
	}, ; 33
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554669, ; uint32_t type_token_id (0x20000ed)
		i32 100; uint32_t java_name_index (0x64)
	}, ; 34
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554731, ; uint32_t type_token_id (0x200012b)
		i32 131; uint32_t java_name_index (0x83)
	}, ; 35
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554825, ; uint32_t type_token_id (0x2000189)
		i32 159; uint32_t java_name_index (0x9f)
	}, ; 36
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554824, ; uint32_t type_token_id (0x2000188)
		i32 158; uint32_t java_name_index (0x9e)
	}, ; 37
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554447, ; uint32_t type_token_id (0x200000f)
		i32 8; uint32_t java_name_index (0x8)
	}, ; 38
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554654, ; uint32_t type_token_id (0x20000de)
		i32 89; uint32_t java_name_index (0x59)
	}, ; 39
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554702, ; uint32_t type_token_id (0x200010e)
		i32 119; uint32_t java_name_index (0x77)
	}, ; 40
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554681, ; uint32_t type_token_id (0x20000f9)
		i32 106; uint32_t java_name_index (0x6a)
	}, ; 41
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554700, ; uint32_t type_token_id (0x200010c)
		i32 117; uint32_t java_name_index (0x75)
	}, ; 42
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554618, ; uint32_t type_token_id (0x20000ba)
		i32 70; uint32_t java_name_index (0x46)
	}, ; 43
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554704, ; uint32_t type_token_id (0x2000110)
		i32 120; uint32_t java_name_index (0x78)
	}, ; 44
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554886, ; uint32_t type_token_id (0x20001c6)
		i32 198; uint32_t java_name_index (0xc6)
	}, ; 45
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554581, ; uint32_t type_token_id (0x2000095)
		i32 48; uint32_t java_name_index (0x30)
	}, ; 46
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 157; uint32_t java_name_index (0x9d)
	}, ; 47
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554447, ; uint32_t type_token_id (0x200000f)
		i32 24; uint32_t java_name_index (0x18)
	}, ; 48
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554862, ; uint32_t type_token_id (0x20001ae)
		i32 180; uint32_t java_name_index (0xb4)
	}, ; 49
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 75; uint32_t java_name_index (0x4b)
	}, ; 50
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554457, ; uint32_t type_token_id (0x2000019)
		i32 17; uint32_t java_name_index (0x11)
	}, ; 51
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 62; uint32_t java_name_index (0x3e)
	}, ; 52
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554448, ; uint32_t type_token_id (0x2000010)
		i32 25; uint32_t java_name_index (0x19)
	}, ; 53
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554866, ; uint32_t type_token_id (0x20001b2)
		i32 184; uint32_t java_name_index (0xb8)
	}, ; 54
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554552, ; uint32_t type_token_id (0x2000078)
		i32 32; uint32_t java_name_index (0x20)
	}, ; 55
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 74; uint32_t java_name_index (0x4a)
	}, ; 56
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554597, ; uint32_t type_token_id (0x20000a5)
		i32 53; uint32_t java_name_index (0x35)
	}, ; 57
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 96; uint32_t java_name_index (0x60)
	}, ; 58
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554696, ; uint32_t type_token_id (0x2000108)
		i32 113; uint32_t java_name_index (0x71)
	}, ; 59
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554893, ; uint32_t type_token_id (0x20001cd)
		i32 204; uint32_t java_name_index (0xcc)
	}, ; 60
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554676, ; uint32_t type_token_id (0x20000f4)
		i32 103; uint32_t java_name_index (0x67)
	}, ; 61
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554803, ; uint32_t type_token_id (0x2000173)
		i32 146; uint32_t java_name_index (0x92)
	}, ; 62
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554647, ; uint32_t type_token_id (0x20000d7)
		i32 82; uint32_t java_name_index (0x52)
	}, ; 63
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554575, ; uint32_t type_token_id (0x200008f)
		i32 45; uint32_t java_name_index (0x2d)
	}, ; 64
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 77; uint32_t java_name_index (0x4d)
	}, ; 65
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554557, ; uint32_t type_token_id (0x200007d)
		i32 35; uint32_t java_name_index (0x23)
	}, ; 66
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554898, ; uint32_t type_token_id (0x20001d2)
		i32 208; uint32_t java_name_index (0xd0)
	}, ; 67
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 76; uint32_t java_name_index (0x4c)
	}, ; 68
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554682, ; uint32_t type_token_id (0x20000fa)
		i32 107; uint32_t java_name_index (0x6b)
	}, ; 69
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 105; uint32_t java_name_index (0x69)
	}, ; 70
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554872, ; uint32_t type_token_id (0x20001b8)
		i32 189; uint32_t java_name_index (0xbd)
	}, ; 71
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554612, ; uint32_t type_token_id (0x20000b4)
		i32 64; uint32_t java_name_index (0x40)
	}, ; 72
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554686, ; uint32_t type_token_id (0x20000fe)
		i32 109; uint32_t java_name_index (0x6d)
	}, ; 73
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554602, ; uint32_t type_token_id (0x20000aa)
		i32 57; uint32_t java_name_index (0x39)
	}, ; 74
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554616, ; uint32_t type_token_id (0x20000b8)
		i32 68; uint32_t java_name_index (0x44)
	}, ; 75
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 58; uint32_t java_name_index (0x3a)
	}, ; 76
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554554, ; uint32_t type_token_id (0x200007a)
		i32 33; uint32_t java_name_index (0x21)
	}, ; 77
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554896, ; uint32_t type_token_id (0x20001d0)
		i32 207; uint32_t java_name_index (0xcf)
	}, ; 78
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554439, ; uint32_t type_token_id (0x2000007)
		i32 4; uint32_t java_name_index (0x4)
	}, ; 79
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554611, ; uint32_t type_token_id (0x20000b3)
		i32 63; uint32_t java_name_index (0x3f)
	}, ; 80
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554714, ; uint32_t type_token_id (0x200011a)
		i32 123; uint32_t java_name_index (0x7b)
	}, ; 81
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 39; uint32_t java_name_index (0x27)
	}, ; 82
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554449, ; uint32_t type_token_id (0x2000011)
		i32 26; uint32_t java_name_index (0x1a)
	}, ; 83
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554808, ; uint32_t type_token_id (0x2000178)
		i32 149; uint32_t java_name_index (0x95)
	}, ; 84
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554821, ; uint32_t type_token_id (0x2000185)
		i32 156; uint32_t java_name_index (0x9c)
	}, ; 85
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 97; uint32_t java_name_index (0x61)
	}, ; 86
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554655, ; uint32_t type_token_id (0x20000df)
		i32 90; uint32_t java_name_index (0x5a)
	}, ; 87
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554876, ; uint32_t type_token_id (0x20001bc)
		i32 191; uint32_t java_name_index (0xbf)
	}, ; 88
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554436, ; uint32_t type_token_id (0x2000004)
		i32 2; uint32_t java_name_index (0x2)
	}, ; 89
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554882, ; uint32_t type_token_id (0x20001c2)
		i32 195; uint32_t java_name_index (0xc3)
	}, ; 90
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554892, ; uint32_t type_token_id (0x20001cc)
		i32 203; uint32_t java_name_index (0xcb)
	}, ; 91
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 59; uint32_t java_name_index (0x3b)
	}, ; 92
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554758, ; uint32_t type_token_id (0x2000146)
		i32 134; uint32_t java_name_index (0x86)
	}, ; 93
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554857, ; uint32_t type_token_id (0x20001a9)
		i32 177; uint32_t java_name_index (0xb1)
	}, ; 94
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554906, ; uint32_t type_token_id (0x20001da)
		i32 213; uint32_t java_name_index (0xd5)
	}, ; 95
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 79; uint32_t java_name_index (0x4f)
	}, ; 96
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 95; uint32_t java_name_index (0x5f)
	}, ; 97
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554813, ; uint32_t type_token_id (0x200017d)
		i32 152; uint32_t java_name_index (0x98)
	}, ; 98
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 217; uint32_t java_name_index (0xd9)
	}, ; 99
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554652, ; uint32_t type_token_id (0x20000dc)
		i32 87; uint32_t java_name_index (0x57)
	}, ; 100
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554452, ; uint32_t type_token_id (0x2000014)
		i32 11; uint32_t java_name_index (0xb)
	}, ; 101
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554890, ; uint32_t type_token_id (0x20001ca)
		i32 201; uint32_t java_name_index (0xc9)
	}, ; 102
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554715, ; uint32_t type_token_id (0x200011b)
		i32 124; uint32_t java_name_index (0x7c)
	}, ; 103
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 38; uint32_t java_name_index (0x26)
	}, ; 104
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554726, ; uint32_t type_token_id (0x2000126)
		i32 130; uint32_t java_name_index (0x82)
	}, ; 105
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554438, ; uint32_t type_token_id (0x2000006)
		i32 3; uint32_t java_name_index (0x3)
	}, ; 106
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 172; uint32_t java_name_index (0xac)
	}, ; 107
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554608, ; uint32_t type_token_id (0x20000b0)
		i32 61; uint32_t java_name_index (0x3d)
	}, ; 108
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554800, ; uint32_t type_token_id (0x2000170)
		i32 145; uint32_t java_name_index (0x91)
	}, ; 109
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 94; uint32_t java_name_index (0x5e)
	}, ; 110
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554646, ; uint32_t type_token_id (0x20000d6)
		i32 81; uint32_t java_name_index (0x51)
	}, ; 111
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554923, ; uint32_t type_token_id (0x20001eb)
		i32 227; uint32_t java_name_index (0xe3)
	}, ; 112
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554871, ; uint32_t type_token_id (0x20001b7)
		i32 188; uint32_t java_name_index (0xbc)
	}, ; 113
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554918, ; uint32_t type_token_id (0x20001e6)
		i32 223; uint32_t java_name_index (0xdf)
	}, ; 114
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554695, ; uint32_t type_token_id (0x2000107)
		i32 112; uint32_t java_name_index (0x70)
	}, ; 115
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554913, ; uint32_t type_token_id (0x20001e1)
		i32 219; uint32_t java_name_index (0xdb)
	}, ; 116
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554469, ; uint32_t type_token_id (0x2000025)
		i32 22; uint32_t java_name_index (0x16)
	}, ; 117
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554615, ; uint32_t type_token_id (0x20000b7)
		i32 67; uint32_t java_name_index (0x43)
	}, ; 118
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554701, ; uint32_t type_token_id (0x200010d)
		i32 118; uint32_t java_name_index (0x76)
	}, ; 119
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554912, ; uint32_t type_token_id (0x20001e0)
		i32 218; uint32_t java_name_index (0xda)
	}, ; 120
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554725, ; uint32_t type_token_id (0x2000125)
		i32 129; uint32_t java_name_index (0x81)
	}, ; 121
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554926, ; uint32_t type_token_id (0x20001ee)
		i32 230; uint32_t java_name_index (0xe6)
	}, ; 122
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554836, ; uint32_t type_token_id (0x2000194)
		i32 165; uint32_t java_name_index (0xa5)
	}, ; 123
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554440, ; uint32_t type_token_id (0x2000008)
		i32 5; uint32_t java_name_index (0x5)
	}, ; 124
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554919, ; uint32_t type_token_id (0x20001e7)
		i32 224; uint32_t java_name_index (0xe0)
	}, ; 125
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554688, ; uint32_t type_token_id (0x2000100)
		i32 110; uint32_t java_name_index (0x6e)
	}, ; 126
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554670, ; uint32_t type_token_id (0x20000ee)
		i32 101; uint32_t java_name_index (0x65)
	}, ; 127
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554844, ; uint32_t type_token_id (0x200019c)
		i32 169; uint32_t java_name_index (0xa9)
	}, ; 128
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554456, ; uint32_t type_token_id (0x2000018)
		i32 16; uint32_t java_name_index (0x10)
	}, ; 129
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554658, ; uint32_t type_token_id (0x20000e2)
		i32 93; uint32_t java_name_index (0x5d)
	}, ; 130
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554797, ; uint32_t type_token_id (0x200016d)
		i32 144; uint32_t java_name_index (0x90)
	}, ; 131
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554794, ; uint32_t type_token_id (0x200016a)
		i32 141; uint32_t java_name_index (0x8d)
	}, ; 132
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554572, ; uint32_t type_token_id (0x200008c)
		i32 43; uint32_t java_name_index (0x2b)
	}, ; 133
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554806, ; uint32_t type_token_id (0x2000176)
		i32 148; uint32_t java_name_index (0x94)
	}, ; 134
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554924, ; uint32_t type_token_id (0x20001ec)
		i32 228; uint32_t java_name_index (0xe4)
	}, ; 135
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554767, ; uint32_t type_token_id (0x200014f)
		i32 135; uint32_t java_name_index (0x87)
	}, ; 136
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554853, ; uint32_t type_token_id (0x20001a5)
		i32 175; uint32_t java_name_index (0xaf)
	}, ; 137
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554717, ; uint32_t type_token_id (0x200011d)
		i32 125; uint32_t java_name_index (0x7d)
	}, ; 138
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554648, ; uint32_t type_token_id (0x20000d8)
		i32 83; uint32_t java_name_index (0x53)
	}, ; 139
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554601, ; uint32_t type_token_id (0x20000a9)
		i32 56; uint32_t java_name_index (0x38)
	}, ; 140
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554811, ; uint32_t type_token_id (0x200017b)
		i32 151; uint32_t java_name_index (0x97)
	}, ; 141
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554908, ; uint32_t type_token_id (0x20001dc)
		i32 215; uint32_t java_name_index (0xd7)
	}, ; 142
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554837, ; uint32_t type_token_id (0x2000195)
		i32 166; uint32_t java_name_index (0xa6)
	}, ; 143
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554667, ; uint32_t type_token_id (0x20000eb)
		i32 98; uint32_t java_name_index (0x62)
	}, ; 144
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554907, ; uint32_t type_token_id (0x20001db)
		i32 214; uint32_t java_name_index (0xd6)
	}, ; 145
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554914, ; uint32_t type_token_id (0x20001e2)
		i32 220; uint32_t java_name_index (0xdc)
	}, ; 146
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554579, ; uint32_t type_token_id (0x2000093)
		i32 47; uint32_t java_name_index (0x2f)
	}, ; 147
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554590, ; uint32_t type_token_id (0x200009e)
		i32 52; uint32_t java_name_index (0x34)
	}, ; 148
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554909, ; uint32_t type_token_id (0x20001dd)
		i32 216; uint32_t java_name_index (0xd8)
	}, ; 149
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554607, ; uint32_t type_token_id (0x20000af)
		i32 60; uint32_t java_name_index (0x3c)
	}, ; 150
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554869, ; uint32_t type_token_id (0x20001b5)
		i32 186; uint32_t java_name_index (0xba)
	}, ; 151
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554467, ; uint32_t type_token_id (0x2000023)
		i32 20; uint32_t java_name_index (0x14)
	}, ; 152
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554470, ; uint32_t type_token_id (0x2000026)
		i32 23; uint32_t java_name_index (0x17)
	}, ; 153
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 49; uint32_t java_name_index (0x31)
	}, ; 154
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554698, ; uint32_t type_token_id (0x200010a)
		i32 115; uint32_t java_name_index (0x73)
	}, ; 155
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554448, ; uint32_t type_token_id (0x2000010)
		i32 9; uint32_t java_name_index (0x9)
	}, ; 156
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554805, ; uint32_t type_token_id (0x2000175)
		i32 147; uint32_t java_name_index (0x93)
	}, ; 157
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554941, ; uint32_t type_token_id (0x20001fd)
		i32 231; uint32_t java_name_index (0xe7)
	}, ; 158
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554833, ; uint32_t type_token_id (0x2000191)
		i32 164; uint32_t java_name_index (0xa4)
	}, ; 159
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554697, ; uint32_t type_token_id (0x2000109)
		i32 114; uint32_t java_name_index (0x72)
	}, ; 160
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554826, ; uint32_t type_token_id (0x200018a)
		i32 160; uint32_t java_name_index (0xa0)
	}, ; 161
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554556, ; uint32_t type_token_id (0x200007c)
		i32 34; uint32_t java_name_index (0x22)
	}, ; 162
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554455, ; uint32_t type_token_id (0x2000017)
		i32 15; uint32_t java_name_index (0xf)
	}, ; 163
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554887, ; uint32_t type_token_id (0x20001c7)
		i32 199; uint32_t java_name_index (0xc7)
	}, ; 164
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554917, ; uint32_t type_token_id (0x20001e5)
		i32 222; uint32_t java_name_index (0xde)
	}, ; 165
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 71; uint32_t java_name_index (0x47)
	}, ; 166
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 126; uint32_t java_name_index (0x7e)
	}, ; 167
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554870, ; uint32_t type_token_id (0x20001b6)
		i32 187; uint32_t java_name_index (0xbb)
	}, ; 168
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554671, ; uint32_t type_token_id (0x20000ef)
		i32 102; uint32_t java_name_index (0x66)
	}, ; 169
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554855, ; uint32_t type_token_id (0x20001a7)
		i32 176; uint32_t java_name_index (0xb0)
	}, ; 170
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554435, ; uint32_t type_token_id (0x2000003)
		i32 1; uint32_t java_name_index (0x1)
	}, ; 171
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554571, ; uint32_t type_token_id (0x200008b)
		i32 42; uint32_t java_name_index (0x2a)
	}, ; 172
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554916, ; uint32_t type_token_id (0x20001e4)
		i32 221; uint32_t java_name_index (0xdd)
	}, ; 173
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554894, ; uint32_t type_token_id (0x20001ce)
		i32 205; uint32_t java_name_index (0xcd)
	}, ; 174
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554453, ; uint32_t type_token_id (0x2000015)
		i32 12; uint32_t java_name_index (0xc)
	}, ; 175
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554783, ; uint32_t type_token_id (0x200015f)
		i32 139; uint32_t java_name_index (0x8b)
	}, ; 176
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 163; uint32_t java_name_index (0xa3)
	}, ; 177
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554668, ; uint32_t type_token_id (0x20000ec)
		i32 99; uint32_t java_name_index (0x63)
	}, ; 178
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554446, ; uint32_t type_token_id (0x200000e)
		i32 7; uint32_t java_name_index (0x7)
	}, ; 179
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554450, ; uint32_t type_token_id (0x2000012)
		i32 27; uint32_t java_name_index (0x1b)
	}, ; 180
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554891, ; uint32_t type_token_id (0x20001cb)
		i32 202; uint32_t java_name_index (0xca)
	}, ; 181
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 28; uint32_t java_name_index (0x1c)
	}, ; 182
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554434, ; uint32_t type_token_id (0x2000002)
		i32 0; uint32_t java_name_index (0x0)
	}, ; 183
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554720, ; uint32_t type_token_id (0x2000120)
		i32 127; uint32_t java_name_index (0x7f)
	}, ; 184
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 212; uint32_t java_name_index (0xd4)
	}, ; 185
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554925, ; uint32_t type_token_id (0x20001ed)
		i32 229; uint32_t java_name_index (0xe5)
	}, ; 186
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554623, ; uint32_t type_token_id (0x20000bf)
		i32 72; uint32_t java_name_index (0x48)
	}, ; 187
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554920, ; uint32_t type_token_id (0x20001e8)
		i32 225; uint32_t java_name_index (0xe1)
	}, ; 188
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 37; uint32_t java_name_index (0x25)
	}, ; 189
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 73; uint32_t java_name_index (0x49)
	}, ; 190
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554614, ; uint32_t type_token_id (0x20000b6)
		i32 66; uint32_t java_name_index (0x42)
	}, ; 191
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 51; uint32_t java_name_index (0x33)
	}, ; 192
	%struct.TypeMapJava {
		i32 1, ; uint32_t module_index (0x1)
		i32 33554457, ; uint32_t type_token_id (0x2000019)
		i32 31; uint32_t java_name_index (0x1f)
	}, ; 193
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554645, ; uint32_t type_token_id (0x20000d5)
		i32 80; uint32_t java_name_index (0x50)
	}, ; 194
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554888, ; uint32_t type_token_id (0x20001c8)
		i32 200; uint32_t java_name_index (0xc8)
	}, ; 195
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554449, ; uint32_t type_token_id (0x2000011)
		i32 10; uint32_t java_name_index (0xa)
	}, ; 196
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 211; uint32_t java_name_index (0xd3)
	}, ; 197
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554486, ; uint32_t type_token_id (0x2000036)
		i32 14; uint32_t java_name_index (0xe)
	}, ; 198
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 40; uint32_t java_name_index (0x28)
	}, ; 199
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554613, ; uint32_t type_token_id (0x20000b5)
		i32 65; uint32_t java_name_index (0x41)
	}, ; 200
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 41; uint32_t java_name_index (0x29)
	}, ; 201
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554868, ; uint32_t type_token_id (0x20001b4)
		i32 185; uint32_t java_name_index (0xb9)
	}, ; 202
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554657, ; uint32_t type_token_id (0x20000e1)
		i32 92; uint32_t java_name_index (0x5c)
	}, ; 203
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554793, ; uint32_t type_token_id (0x2000169)
		i32 140; uint32_t java_name_index (0x8c)
	}, ; 204
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 18; uint32_t java_name_index (0x12)
	}, ; 205
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554712, ; uint32_t type_token_id (0x2000118)
		i32 121; uint32_t java_name_index (0x79)
	}, ; 206
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554678, ; uint32_t type_token_id (0x20000f6)
		i32 104; uint32_t java_name_index (0x68)
	}, ; 207
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 153; uint32_t java_name_index (0x99)
	}, ; 208
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554699, ; uint32_t type_token_id (0x200010b)
		i32 116; uint32_t java_name_index (0x74)
	}, ; 209
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554769, ; uint32_t type_token_id (0x2000151)
		i32 136; uint32_t java_name_index (0x88)
	}, ; 210
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554754, ; uint32_t type_token_id (0x2000142)
		i32 132; uint32_t java_name_index (0x84)
	}, ; 211
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554724, ; uint32_t type_token_id (0x2000124)
		i32 128; uint32_t java_name_index (0x80)
	}, ; 212
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 161; uint32_t java_name_index (0xa1)
	}, ; 213
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554884, ; uint32_t type_token_id (0x20001c4)
		i32 197; uint32_t java_name_index (0xc5)
	}, ; 214
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554796, ; uint32_t type_token_id (0x200016c)
		i32 143; uint32_t java_name_index (0x8f)
	}, ; 215
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554585, ; uint32_t type_token_id (0x2000099)
		i32 50; uint32_t java_name_index (0x32)
	}, ; 216
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554685, ; uint32_t type_token_id (0x20000fd)
		i32 108; uint32_t java_name_index (0x6c)
	}, ; 217
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 167; uint32_t java_name_index (0xa7)
	}, ; 218
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554895, ; uint32_t type_token_id (0x20001cf)
		i32 206; uint32_t java_name_index (0xce)
	}, ; 219
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554851, ; uint32_t type_token_id (0x20001a3)
		i32 173; uint32_t java_name_index (0xad)
	}, ; 220
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554713, ; uint32_t type_token_id (0x2000119)
		i32 122; uint32_t java_name_index (0x7a)
	}, ; 221
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554865, ; uint32_t type_token_id (0x20001b1)
		i32 183; uint32_t java_name_index (0xb7)
	}, ; 222
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 193; uint32_t java_name_index (0xc1)
	}, ; 223
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554795, ; uint32_t type_token_id (0x200016b)
		i32 142; uint32_t java_name_index (0x8e)
	}, ; 224
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554454, ; uint32_t type_token_id (0x2000016)
		i32 13; uint32_t java_name_index (0xd)
	}, ; 225
	%struct.TypeMapJava {
		i32 0, ; uint32_t module_index (0x0)
		i32 33554441, ; uint32_t type_token_id (0x2000009)
		i32 6; uint32_t java_name_index (0x6)
	}, ; 226
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 0, ; uint32_t type_token_id (0x0)
		i32 162; uint32_t java_name_index (0xa2)
	}, ; 227
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554573, ; uint32_t type_token_id (0x200008d)
		i32 44; uint32_t java_name_index (0x2c)
	}, ; 228
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554598, ; uint32_t type_token_id (0x20000a6)
		i32 54; uint32_t java_name_index (0x36)
	}, ; 229
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554900, ; uint32_t type_token_id (0x20001d4)
		i32 210; uint32_t java_name_index (0xd2)
	}, ; 230
	%struct.TypeMapJava {
		i32 2, ; uint32_t module_index (0x2)
		i32 33554756, ; uint32_t type_token_id (0x2000144)
		i32 133; uint32_t java_name_index (0x85)
	} ; 231
], align 16

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
], align 16

; Strings
@.str.0 = private unnamed_addr constant [38 x i8] c"crc64298b90ab1d20ae8a/AboutMeActivity\00", align 16
@.str.1 = private unnamed_addr constant [43 x i8] c"crc64298b90ab1d20ae8a/LocationAgentService\00", align 16
@.str.2 = private unnamed_addr constant [42 x i8] c"crc64298b90ab1d20ae8a/DatabaseSyncService\00", align 16
@.str.3 = private unnamed_addr constant [42 x i8] c"crc64298b90ab1d20ae8a/CustomerProductList\00", align 16
@.str.4 = private unnamed_addr constant [51 x i8] c"crc64298b90ab1d20ae8a/DisplayVitrineCameraActivity\00", align 16
@.str.5 = private unnamed_addr constant [39 x i8] c"crc64298b90ab1d20ae8a/ExitShopActivity\00", align 16
@.str.6 = private unnamed_addr constant [45 x i8] c"crc64298b90ab1d20ae8a/ExpirationDateActivity\00", align 16
@.str.7 = private unnamed_addr constant [35 x i8] c"crc64298b90ab1d20ae8a/MainActivity\00", align 16
@.str.8 = private unnamed_addr constant [37 x i8] c"crc64298b90ab1d20ae8a/MyBootReceiver\00", align 16
@.str.9 = private unnamed_addr constant [31 x i8] c"crc64298b90ab1d20ae8a/MainMenu\00", align 16
@.str.10 = private unnamed_addr constant [45 x i8] c"crc64298b90ab1d20ae8a/MerchDataEntryActivity\00", align 16
@.str.11 = private unnamed_addr constant [41 x i8] c"crc64298b90ab1d20ae8a/SalesOrderActivity\00", align 16
@.str.12 = private unnamed_addr constant [52 x i8] c"crc64298b90ab1d20ae8a/CustomerProductSpinnerAdapter\00", align 16
@.str.13 = private unnamed_addr constant [41 x i8] c"crc64298b90ab1d20ae8a/ShopCameraActivity\00", align 16
@.str.14 = private unnamed_addr constant [69 x i8] c"crc64298b90ab1d20ae8a/ShopCameraActivity_PromotionTypeSpinnerAdapter\00", align 16
@.str.15 = private unnamed_addr constant [46 x i8] c"crc64298b90ab1d20ae8a/ShopEntryCameraActivity\00", align 16
@.str.16 = private unnamed_addr constant [53 x i8] c"crc64298b90ab1d20ae8a/ShopEntryVitrineCameraActivity\00", align 16
@.str.17 = private unnamed_addr constant [34 x i8] c"crc64298b90ab1d20ae8a/TodaysRoute\00", align 16
@.str.18 = private unnamed_addr constant [42 x i8] c"crc646c316a5726d9f3c1/JavaObjectWrapper_1\00", align 16
@.str.19 = private unnamed_addr constant [39 x i8] c"crc646c316a5726d9f3c1/PhotoListAdapter\00", align 16
@.str.20 = private unnamed_addr constant [54 x i8] c"crc646c316a5726d9f3c1/ProductExpirationPreviewAdapter\00", align 16
@.str.21 = private unnamed_addr constant [51 x i8] c"crc646c316a5726d9f3c1/RetailShopProductListAdapter\00", align 16
@.str.22 = private unnamed_addr constant [47 x i8] c"crc646c316a5726d9f3c1/SalesOrderPreviewAdapter\00", align 16
@.str.23 = private unnamed_addr constant [41 x i8] c"crc646c316a5726d9f3c1/TodaysRouteAdapter\00", align 16
@.str.24 = private unnamed_addr constant [36 x i8] c"androidx/core/content/ContextCompat\00", align 16
@.str.25 = private unnamed_addr constant [35 x i8] c"androidx/core/content/FileProvider\00", align 16
@.str.26 = private unnamed_addr constant [36 x i8] c"androidx/core/content/LocusIdCompat\00", align 16
@.str.27 = private unnamed_addr constant [33 x i8] c"androidx/core/app/ActivityCompat\00", align 16
@.str.28 = private unnamed_addr constant [58 x i8] c"androidx/core/app/ActivityCompat$PermissionCompatDelegate\00", align 16
@.str.29 = private unnamed_addr constant [40 x i8] c"androidx/core/app/SharedElementCallback\00", align 16
@.str.30 = private unnamed_addr constant [70 x i8] c"androidx/core/app/SharedElementCallback$OnSharedElementsReadyListener\00", align 16
@.str.31 = private unnamed_addr constant [48 x i8] c"androidx/core/view/DragAndDropPermissionsCompat\00", align 16
@.str.32 = private unnamed_addr constant [32 x i8] c"javax/security/cert/Certificate\00", align 16
@.str.33 = private unnamed_addr constant [36 x i8] c"javax/security/cert/X509Certificate\00", align 16
@.str.34 = private unnamed_addr constant [28 x i8] c"javax/security/auth/Subject\00", align 16
@.str.35 = private unnamed_addr constant [24 x i8] c"javax/net/SocketFactory\00", align 16
@.str.36 = private unnamed_addr constant [33 x i8] c"javax/net/ssl/HttpsURLConnection\00", align 16
@.str.37 = private unnamed_addr constant [31 x i8] c"javax/net/ssl/HostnameVerifier\00", align 16
@.str.38 = private unnamed_addr constant [25 x i8] c"javax/net/ssl/KeyManager\00", align 16
@.str.39 = private unnamed_addr constant [25 x i8] c"javax/net/ssl/SSLSession\00", align 16
@.str.40 = private unnamed_addr constant [32 x i8] c"javax/net/ssl/SSLSessionContext\00", align 16
@.str.41 = private unnamed_addr constant [27 x i8] c"javax/net/ssl/TrustManager\00", align 16
@.str.42 = private unnamed_addr constant [32 x i8] c"javax/net/ssl/KeyManagerFactory\00", align 16
@.str.43 = private unnamed_addr constant [25 x i8] c"javax/net/ssl/SSLContext\00", align 16
@.str.44 = private unnamed_addr constant [31 x i8] c"javax/net/ssl/SSLSocketFactory\00", align 16
@.str.45 = private unnamed_addr constant [34 x i8] c"javax/net/ssl/TrustManagerFactory\00", align 16
@.str.46 = private unnamed_addr constant [27 x i8] c"android/widget/AbsListView\00", align 16
@.str.47 = private unnamed_addr constant [26 x i8] c"android/widget/AbsSpinner\00", align 16
@.str.48 = private unnamed_addr constant [27 x i8] c"android/widget/AdapterView\00", align 16
@.str.49 = private unnamed_addr constant [47 x i8] c"android/widget/AdapterView$OnItemClickListener\00", align 16
@.str.50 = private unnamed_addr constant [63 x i8] c"mono/android/widget/AdapterView_OnItemClickListenerImplementor\00", align 16
@.str.51 = private unnamed_addr constant [50 x i8] c"android/widget/AdapterView$OnItemSelectedListener\00", align 16
@.str.52 = private unnamed_addr constant [66 x i8] c"mono/android/widget/AdapterView_OnItemSelectedListenerImplementor\00", align 16
@.str.53 = private unnamed_addr constant [28 x i8] c"android/widget/ArrayAdapter\00", align 16
@.str.54 = private unnamed_addr constant [27 x i8] c"android/widget/BaseAdapter\00", align 16
@.str.55 = private unnamed_addr constant [22 x i8] c"android/widget/Button\00", align 16
@.str.56 = private unnamed_addr constant [24 x i8] c"android/widget/EditText\00", align 16
@.str.57 = private unnamed_addr constant [27 x i8] c"android/widget/FrameLayout\00", align 16
@.str.58 = private unnamed_addr constant [23 x i8] c"android/widget/Adapter\00", align 16
@.str.59 = private unnamed_addr constant [27 x i8] c"android/widget/ListAdapter\00", align 16
@.str.60 = private unnamed_addr constant [27 x i8] c"android/widget/ImageButton\00", align 16
@.str.61 = private unnamed_addr constant [25 x i8] c"android/widget/ImageView\00", align 16
@.str.62 = private unnamed_addr constant [30 x i8] c"android/widget/SpinnerAdapter\00", align 16
@.str.63 = private unnamed_addr constant [28 x i8] c"android/widget/LinearLayout\00", align 16
@.str.64 = private unnamed_addr constant [41 x i8] c"android/widget/LinearLayout$LayoutParams\00", align 16
@.str.65 = private unnamed_addr constant [24 x i8] c"android/widget/ListView\00", align 16
@.str.66 = private unnamed_addr constant [23 x i8] c"android/widget/Spinner\00", align 16
@.str.67 = private unnamed_addr constant [23 x i8] c"android/widget/TabHost\00", align 16
@.str.68 = private unnamed_addr constant [31 x i8] c"android/widget/TabHost$TabSpec\00", align 16
@.str.69 = private unnamed_addr constant [24 x i8] c"android/widget/TextView\00", align 16
@.str.70 = private unnamed_addr constant [35 x i8] c"android/widget/TextView$BufferType\00", align 16
@.str.71 = private unnamed_addr constant [26 x i8] c"android/util/AttributeSet\00", align 16
@.str.72 = private unnamed_addr constant [17 x i8] c"android/util/Log\00", align 16
@.str.73 = private unnamed_addr constant [22 x i8] c"android/text/Editable\00", align 16
@.str.74 = private unnamed_addr constant [22 x i8] c"android/text/GetChars\00", align 16
@.str.75 = private unnamed_addr constant [25 x i8] c"android/text/InputFilter\00", align 16
@.str.76 = private unnamed_addr constant [24 x i8] c"android/text/NoCopySpan\00", align 16
@.str.77 = private unnamed_addr constant [23 x i8] c"android/text/Spannable\00", align 16
@.str.78 = private unnamed_addr constant [21 x i8] c"android/text/Spanned\00", align 16
@.str.79 = private unnamed_addr constant [25 x i8] c"android/text/TextWatcher\00", align 16
@.str.80 = private unnamed_addr constant [41 x i8] c"mono/android/text/TextWatcherImplementor\00", align 16
@.str.81 = private unnamed_addr constant [35 x i8] c"android/telephony/TelephonyManager\00", align 16
@.str.82 = private unnamed_addr constant [28 x i8] c"android/provider/MediaStore\00", align 16
@.str.83 = private unnamed_addr constant [35 x i8] c"android/provider/MediaStore$Images\00", align 16
@.str.84 = private unnamed_addr constant [41 x i8] c"android/provider/MediaStore$Images$Media\00", align 16
@.str.85 = private unnamed_addr constant [26 x i8] c"android/provider/Settings\00", align 16
@.str.86 = private unnamed_addr constant [41 x i8] c"android/provider/Settings$NameValueTable\00", align 16
@.str.87 = private unnamed_addr constant [33 x i8] c"android/provider/Settings$Secure\00", align 16
@.str.88 = private unnamed_addr constant [22 x i8] c"android/os/BaseBundle\00", align 16
@.str.89 = private unnamed_addr constant [17 x i8] c"android/os/Build\00", align 16
@.str.90 = private unnamed_addr constant [25 x i8] c"android/os/Build$VERSION\00", align 16
@.str.91 = private unnamed_addr constant [18 x i8] c"android/os/Bundle\00", align 16
@.str.92 = private unnamed_addr constant [23 x i8] c"android/os/Environment\00", align 16
@.str.93 = private unnamed_addr constant [19 x i8] c"android/os/Handler\00", align 16
@.str.94 = private unnamed_addr constant [34 x i8] c"android/os/IBinder$DeathRecipient\00", align 16
@.str.95 = private unnamed_addr constant [19 x i8] c"android/os/IBinder\00", align 16
@.str.96 = private unnamed_addr constant [22 x i8] c"android/os/IInterface\00", align 16
@.str.97 = private unnamed_addr constant [22 x i8] c"android/os/Parcelable\00", align 16
@.str.98 = private unnamed_addr constant [18 x i8] c"android/os/Looper\00", align 16
@.str.99 = private unnamed_addr constant [18 x i8] c"android/os/Parcel\00", align 16
@.str.100 = private unnamed_addr constant [24 x i8] c"android/os/PowerManager\00", align 16
@.str.101 = private unnamed_addr constant [33 x i8] c"android/os/PowerManager$WakeLock\00", align 16
@.str.102 = private unnamed_addr constant [19 x i8] c"android/os/Process\00", align 16
@.str.103 = private unnamed_addr constant [16 x i8] c"android/net/Uri\00", align 16
@.str.104 = private unnamed_addr constant [26 x i8] c"android/location/Criteria\00", align 16
@.str.105 = private unnamed_addr constant [34 x i8] c"android/location/LocationListener\00", align 16
@.str.106 = private unnamed_addr constant [26 x i8] c"android/location/Location\00", align 16
@.str.107 = private unnamed_addr constant [33 x i8] c"android/location/LocationManager\00", align 16
@.str.108 = private unnamed_addr constant [33 x i8] c"android/database/CharArrayBuffer\00", align 16
@.str.109 = private unnamed_addr constant [33 x i8] c"android/database/ContentObserver\00", align 16
@.str.110 = private unnamed_addr constant [33 x i8] c"android/database/DataSetObserver\00", align 16
@.str.111 = private unnamed_addr constant [24 x i8] c"android/database/Cursor\00", align 16
@.str.112 = private unnamed_addr constant [21 x i8] c"android/app/Activity\00", align 16
@.str.113 = private unnamed_addr constant [26 x i8] c"android/app/ActivityGroup\00", align 16
@.str.114 = private unnamed_addr constant [24 x i8] c"android/app/AlertDialog\00", align 16
@.str.115 = private unnamed_addr constant [32 x i8] c"android/app/AlertDialog$Builder\00", align 16
@.str.116 = private unnamed_addr constant [24 x i8] c"android/app/Application\00", align 16
@.str.117 = private unnamed_addr constant [19 x i8] c"android/app/Dialog\00", align 16
@.str.118 = private unnamed_addr constant [28 x i8] c"android/app/DownloadManager\00", align 16
@.str.119 = private unnamed_addr constant [20 x i8] c"android/app/Service\00", align 16
@.str.120 = private unnamed_addr constant [24 x i8] c"android/app/TabActivity\00", align 16
@.str.121 = private unnamed_addr constant [33 x i8] c"android/view/ContextThemeWrapper\00", align 16
@.str.122 = private unnamed_addr constant [21 x i8] c"android/view/Display\00", align 16
@.str.123 = private unnamed_addr constant [23 x i8] c"android/view/DragEvent\00", align 16
@.str.124 = private unnamed_addr constant [28 x i8] c"android/view/LayoutInflater\00", align 16
@.str.125 = private unnamed_addr constant [18 x i8] c"android/view/View\00", align 16
@.str.126 = private unnamed_addr constant [34 x i8] c"android/view/View$OnClickListener\00", align 16
@.str.127 = private unnamed_addr constant [50 x i8] c"mono/android/view/View_OnClickListenerImplementor\00", align 16
@.str.128 = private unnamed_addr constant [23 x i8] c"android/view/ViewGroup\00", align 16
@.str.129 = private unnamed_addr constant [36 x i8] c"android/view/ViewGroup$LayoutParams\00", align 16
@.str.130 = private unnamed_addr constant [42 x i8] c"android/view/ViewGroup$MarginLayoutParams\00", align 16
@.str.131 = private unnamed_addr constant [44 x i8] c"android/view/inputmethod/InputMethodManager\00", align 16
@.str.132 = private unnamed_addr constant [40 x i8] c"mono/android/runtime/InputStreamAdapter\00", align 16
@.str.133 = private unnamed_addr constant [21 x i8] c"java/util/Collection\00", align 16
@.str.134 = private unnamed_addr constant [18 x i8] c"java/util/HashMap\00", align 16
@.str.135 = private unnamed_addr constant [20 x i8] c"java/util/ArrayList\00", align 16
@.str.136 = private unnamed_addr constant [32 x i8] c"mono/android/runtime/JavaObject\00", align 16
@.str.137 = private unnamed_addr constant [35 x i8] c"android/runtime/JavaProxyThrowable\00", align 16
@.str.138 = private unnamed_addr constant [18 x i8] c"java/util/HashSet\00", align 16
@.str.139 = private unnamed_addr constant [41 x i8] c"mono/android/runtime/OutputStreamAdapter\00", align 16
@.str.140 = private unnamed_addr constant [24 x i8] c"android/graphics/Bitmap\00", align 16
@.str.141 = private unnamed_addr constant [39 x i8] c"android/graphics/Bitmap$CompressFormat\00", align 16
@.str.142 = private unnamed_addr constant [31 x i8] c"android/graphics/BitmapFactory\00", align 16
@.str.143 = private unnamed_addr constant [24 x i8] c"android/graphics/Matrix\00", align 16
@.str.144 = private unnamed_addr constant [23 x i8] c"android/graphics/RectF\00", align 16
@.str.145 = private unnamed_addr constant [35 x i8] c"android/graphics/drawable/Drawable\00", align 16
@.str.146 = private unnamed_addr constant [34 x i8] c"android/content/BroadcastReceiver\00", align 16
@.str.147 = private unnamed_addr constant [30 x i8] c"android/content/ComponentName\00", align 16
@.str.148 = private unnamed_addr constant [32 x i8] c"android/content/ContentProvider\00", align 16
@.str.149 = private unnamed_addr constant [32 x i8] c"android/content/ContentResolver\00", align 16
@.str.150 = private unnamed_addr constant [30 x i8] c"android/content/ContentValues\00", align 16
@.str.151 = private unnamed_addr constant [24 x i8] c"android/content/Context\00", align 16
@.str.152 = private unnamed_addr constant [31 x i8] c"android/content/ContextWrapper\00", align 16
@.str.153 = private unnamed_addr constant [48 x i8] c"android/content/DialogInterface$OnClickListener\00", align 16
@.str.154 = private unnamed_addr constant [64 x i8] c"mono/android/content/DialogInterface_OnClickListenerImplementor\00", align 16
@.str.155 = private unnamed_addr constant [59 x i8] c"android/content/DialogInterface$OnMultiChoiceClickListener\00", align 16
@.str.156 = private unnamed_addr constant [75 x i8] c"mono/android/content/DialogInterface_OnMultiChoiceClickListenerImplementor\00", align 16
@.str.157 = private unnamed_addr constant [32 x i8] c"android/content/DialogInterface\00", align 16
@.str.158 = private unnamed_addr constant [23 x i8] c"android/content/Intent\00", align 16
@.str.159 = private unnamed_addr constant [29 x i8] c"android/content/IntentFilter\00", align 16
@.str.160 = private unnamed_addr constant [29 x i8] c"android/content/IntentSender\00", align 16
@.str.161 = private unnamed_addr constant [41 x i8] c"android/content/SharedPreferences$Editor\00", align 16
@.str.162 = private unnamed_addr constant [67 x i8] c"android/content/SharedPreferences$OnSharedPreferenceChangeListener\00", align 16
@.str.163 = private unnamed_addr constant [34 x i8] c"android/content/SharedPreferences\00", align 16
@.str.164 = private unnamed_addr constant [24 x i8] c"android/content/LocusId\00", align 16
@.str.165 = private unnamed_addr constant [35 x i8] c"android/content/res/ColorStateList\00", align 16
@.str.166 = private unnamed_addr constant [30 x i8] c"android/content/res/Resources\00", align 16
@.str.167 = private unnamed_addr constant [22 x i8] c"java/util/Enumeration\00", align 16
@.str.168 = private unnamed_addr constant [19 x i8] c"java/util/Iterator\00", align 16
@.str.169 = private unnamed_addr constant [17 x i8] c"java/util/Random\00", align 16
@.str.170 = private unnamed_addr constant [28 x i8] c"java/util/function/Consumer\00", align 16
@.str.171 = private unnamed_addr constant [30 x i8] c"java/util/concurrent/Executor\00", align 16
@.str.172 = private unnamed_addr constant [24 x i8] c"java/security/Principal\00", align 16
@.str.173 = private unnamed_addr constant [23 x i8] c"java/security/KeyStore\00", align 16
@.str.174 = private unnamed_addr constant [27 x i8] c"java/security/SecureRandom\00", align 16
@.str.175 = private unnamed_addr constant [31 x i8] c"java/security/cert/Certificate\00", align 16
@.str.176 = private unnamed_addr constant [30 x i8] c"java/nio/channels/FileChannel\00", align 16
@.str.177 = private unnamed_addr constant [51 x i8] c"java/nio/channels/spi/AbstractInterruptibleChannel\00", align 16
@.str.178 = private unnamed_addr constant [26 x i8] c"java/net/ConnectException\00", align 16
@.str.179 = private unnamed_addr constant [27 x i8] c"java/net/HttpURLConnection\00", align 16
@.str.180 = private unnamed_addr constant [27 x i8] c"java/net/InetSocketAddress\00", align 16
@.str.181 = private unnamed_addr constant [27 x i8] c"java/net/ProtocolException\00", align 16
@.str.182 = private unnamed_addr constant [15 x i8] c"java/net/Proxy\00", align 1
@.str.183 = private unnamed_addr constant [20 x i8] c"java/net/Proxy$Type\00", align 16
@.str.184 = private unnamed_addr constant [23 x i8] c"java/net/SocketAddress\00", align 16
@.str.185 = private unnamed_addr constant [25 x i8] c"java/net/SocketException\00", align 16
@.str.186 = private unnamed_addr constant [32 x i8] c"java/net/SocketTimeoutException\00", align 16
@.str.187 = private unnamed_addr constant [33 x i8] c"java/net/UnknownServiceException\00", align 16
@.str.188 = private unnamed_addr constant [13 x i8] c"java/net/URL\00", align 1
@.str.189 = private unnamed_addr constant [23 x i8] c"java/net/URLConnection\00", align 16
@.str.190 = private unnamed_addr constant [13 x i8] c"java/io/File\00", align 1
@.str.191 = private unnamed_addr constant [23 x i8] c"java/io/FileDescriptor\00", align 16
@.str.192 = private unnamed_addr constant [24 x i8] c"java/io/FileInputStream\00", align 16
@.str.193 = private unnamed_addr constant [18 x i8] c"java/io/Closeable\00", align 16
@.str.194 = private unnamed_addr constant [20 x i8] c"java/io/InputStream\00", align 16
@.str.195 = private unnamed_addr constant [31 x i8] c"java/io/InterruptedIOException\00", align 16
@.str.196 = private unnamed_addr constant [20 x i8] c"java/io/IOException\00", align 16
@.str.197 = private unnamed_addr constant [21 x i8] c"java/io/OutputStream\00", align 16
@.str.198 = private unnamed_addr constant [20 x i8] c"java/io/PrintWriter\00", align 16
@.str.199 = private unnamed_addr constant [21 x i8] c"java/io/StringWriter\00", align 16
@.str.200 = private unnamed_addr constant [15 x i8] c"java/io/Writer\00", align 1
@.str.201 = private unnamed_addr constant [18 x i8] c"java/lang/Boolean\00", align 16
@.str.202 = private unnamed_addr constant [15 x i8] c"java/lang/Byte\00", align 1
@.str.203 = private unnamed_addr constant [20 x i8] c"java/lang/Character\00", align 16
@.str.204 = private unnamed_addr constant [16 x i8] c"java/lang/Class\00", align 16
@.str.205 = private unnamed_addr constant [29 x i8] c"java/lang/ClassCastException\00", align 16
@.str.206 = private unnamed_addr constant [17 x i8] c"java/lang/Double\00", align 16
@.str.207 = private unnamed_addr constant [15 x i8] c"java/lang/Enum\00", align 1
@.str.208 = private unnamed_addr constant [16 x i8] c"java/lang/Error\00", align 16
@.str.209 = private unnamed_addr constant [20 x i8] c"java/lang/Exception\00", align 16
@.str.210 = private unnamed_addr constant [16 x i8] c"java/lang/Float\00", align 16
@.str.211 = private unnamed_addr constant [21 x i8] c"java/lang/Appendable\00", align 16
@.str.212 = private unnamed_addr constant [23 x i8] c"java/lang/CharSequence\00", align 16
@.str.213 = private unnamed_addr constant [35 x i8] c"java/lang/IllegalArgumentException\00", align 16
@.str.214 = private unnamed_addr constant [32 x i8] c"java/lang/IllegalStateException\00", align 16
@.str.215 = private unnamed_addr constant [36 x i8] c"java/lang/IndexOutOfBoundsException\00", align 16
@.str.216 = private unnamed_addr constant [18 x i8] c"java/lang/Integer\00", align 16
@.str.217 = private unnamed_addr constant [19 x i8] c"java/lang/Runnable\00", align 16
@.str.218 = private unnamed_addr constant [15 x i8] c"java/lang/Long\00", align 1
@.str.219 = private unnamed_addr constant [31 x i8] c"java/lang/NullPointerException\00", align 16
@.str.220 = private unnamed_addr constant [17 x i8] c"java/lang/Number\00", align 16
@.str.221 = private unnamed_addr constant [17 x i8] c"java/lang/Object\00", align 16
@.str.222 = private unnamed_addr constant [27 x i8] c"java/lang/RuntimeException\00", align 16
@.str.223 = private unnamed_addr constant [28 x i8] c"java/lang/SecurityException\00", align 16
@.str.224 = private unnamed_addr constant [16 x i8] c"java/lang/Short\00", align 16
@.str.225 = private unnamed_addr constant [28 x i8] c"java/lang/StackTraceElement\00", align 16
@.str.226 = private unnamed_addr constant [17 x i8] c"java/lang/String\00", align 16
@.str.227 = private unnamed_addr constant [17 x i8] c"java/lang/Thread\00", align 16
@.str.228 = private unnamed_addr constant [35 x i8] c"mono/java/lang/RunnableImplementor\00", align 16
@.str.229 = private unnamed_addr constant [20 x i8] c"java/lang/Throwable\00", align 16
@.str.230 = private unnamed_addr constant [40 x i8] c"java/lang/UnsupportedOperationException\00", align 16
@.str.231 = private unnamed_addr constant [25 x i8] c"mono/android/TypeManager\00", align 16

;TypeMapModule
@.TypeMapModule.0_assembly_name = private unnamed_addr constant [10 x i8] c"MerchPlus\00", align 1
@.TypeMapModule.1_assembly_name = private unnamed_addr constant [22 x i8] c"Xamarin.AndroidX.Core\00", align 16
@.TypeMapModule.2_assembly_name = private unnamed_addr constant [13 x i8] c"Mono.Android\00", align 1

; Metadata
!llvm.module.flags = !{!0, !1}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.2xx @ 0d97e20b84d8e87c3502469ee395805907905fe3"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
