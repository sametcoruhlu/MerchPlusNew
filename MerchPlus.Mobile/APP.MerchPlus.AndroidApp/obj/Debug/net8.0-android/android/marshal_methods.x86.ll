; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [244 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [482 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 210
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 231
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 39109920, ; 6: Newtonsoft.Json.dll => 0x254c520 => 173
	i32 39485524, ; 7: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 42639949, ; 8: System.Threading.Thread => 0x28aa24d => 145
	i32 45981941, ; 9: Xamarin.KotlinX.AtomicFU.Jvm => 0x2bda0f5 => 235
	i32 66541672, ; 10: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 68219467, ; 11: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 82292897, ; 12: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 117431740, ; 13: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 122350210, ; 14: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 142721839, ; 15: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 16: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 17: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 18: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 188
	i32 166070894, ; 19: Xamarin.KotlinX.AtomicFU.dll => 0x9e60a6e => 234
	i32 176265551, ; 20: System.ServiceProcess => 0xa81994f => 132
	i32 184328833, ; 21: System.ValueTuple.dll => 0xafca281 => 151
	i32 205061960, ; 22: System.ComponentModel => 0xc38ff48 => 18
	i32 220171995, ; 23: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230216969, ; 24: Xamarin.AndroidX.Legacy.Support.Core.Utils.dll => 0xdb8d509 => 205
	i32 230752869, ; 25: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 26: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 27: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 28: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 261689757, ; 29: Xamarin.AndroidX.ConstraintLayout.dll => 0xf99119d => 191
	i32 276479776, ; 30: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 280482487, ; 31: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 204
	i32 291076382, ; 32: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 298918909, ; 33: System.Net.Ping.dll => 0x11d123fd => 69
	i32 318968648, ; 34: Xamarin.AndroidX.Activity.dll => 0x13031348 => 179
	i32 321597661, ; 35: System.Numerics => 0x132b30dd => 83
	i32 342366114, ; 36: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 206
	i32 347068432, ; 37: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 177
	i32 360082299, ; 38: System.ServiceModel.Web => 0x15766b7b => 131
	i32 367780167, ; 39: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 40: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 41: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 42: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 43: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 44: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395744057, ; 45: _Microsoft.Android.Resource.Designer => 0x17969339 => 240
	i32 403441872, ; 46: WindowsBase => 0x180c08d0 => 165
	i32 425531652, ; 47: Xamarin.AndroidX.Lifecycle.Runtime.Android => 0x195d1904 => 211
	i32 438127167, ; 48: entMerchPlus.dll => 0x1a1d4a3f => 239
	i32 441335492, ; 49: Xamarin.AndroidX.ConstraintLayout.Core => 0x1a4e3ec4 => 192
	i32 442565967, ; 50: System.Collections => 0x1a61054f => 12
	i32 450948140, ; 51: Xamarin.AndroidX.Fragment.dll => 0x1ae0ec2c => 203
	i32 451504562, ; 52: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 53: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 54: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 55: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 56: System.dll => 0x1bff388e => 164
	i32 476646585, ; 57: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 204
	i32 486930444, ; 58: Xamarin.AndroidX.LocalBroadcastManager.dll => 0x1d05f80c => 216
	i32 498788369, ; 59: System.ObjectModel => 0x1dbae811 => 84
	i32 526420162, ; 60: System.Transactions.dll => 0x1f6088c2 => 150
	i32 530272170, ; 61: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 540030774, ; 62: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 63: System.Runtime.Extensions => 0x2080b118 => 103
	i32 546455878, ; 64: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 549171840, ; 65: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 577335427, ; 66: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 601371474, ; 67: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 68: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 69: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 621990341, ; 70: Xamarin.AndroidX.Lifecycle.Runtime.Android.dll => 0x2512d1c5 => 211
	i32 627609679, ; 71: Xamarin.AndroidX.CustomView => 0x2568904f => 197
	i32 639843206, ; 72: Xamarin.AndroidX.Emoji2.ViewsHelper.dll => 0x26233b86 => 202
	i32 643868501, ; 73: System.Net => 0x2660a755 => 81
	i32 662205335, ; 74: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 75: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 227
	i32 666292255, ; 76: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 185
	i32 672442732, ; 77: System.Collections.Concurrent => 0x2814a96c => 8
	i32 679221896, ; 78: Xamarin.KotlinX.AtomicFU => 0x287c1a88 => 234
	i32 683518922, ; 79: System.Net.Security => 0x28bdabca => 73
	i32 690569205, ; 80: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 81: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 236
	i32 693804605, ; 82: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 83: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 84: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 232
	i32 700358131, ; 85: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 722857257, ; 86: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 735137430, ; 87: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 748832960, ; 88: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 175
	i32 752232764, ; 89: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 759454413, ; 90: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 91: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 92: System.IO.Compression => 0x2e394f87 => 46
	i32 804715423, ; 93: System.Data.Common => 0x2ff6fb9f => 22
	i32 823281589, ; 94: System.Private.Uri.dll => 0x311247b5 => 86
	i32 830298997, ; 95: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 96: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 97: System.Net.Quic => 0x31b69d60 => 71
	i32 843511501, ; 98: Xamarin.AndroidX.Print => 0x3246f6cd => 217
	i32 873119928, ; 99: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 100: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 101: System.Net.Http.Json => 0x3463c971 => 63
	i32 904024072, ; 102: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 103: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 928116545, ; 104: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 231
	i32 952186615, ; 105: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 955402788, ; 106: Newtonsoft.Json => 0x38f24a24 => 173
	i32 967690846, ; 107: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 206
	i32 975236339, ; 108: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 109: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 110: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 111: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 992768348, ; 112: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 113: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 1001831731, ; 114: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1012816738, ; 115: Xamarin.AndroidX.SavedState.dll => 0x3c5e5b62 => 221
	i32 1019214401, ; 116: System.Drawing => 0x3cbffa41 => 36
	i32 1035644815, ; 117: Xamarin.AndroidX.AppCompat => 0x3dbaaf8f => 183
	i32 1036536393, ; 118: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1044663988, ; 119: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1052210849, ; 120: Xamarin.AndroidX.Lifecycle.ViewModel.dll => 0x3eb776a1 => 212
	i32 1082857460, ; 121: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 122: Xamarin.Kotlin.StdLib => 0x409e66d8 => 233
	i32 1098259244, ; 123: System => 0x41761b2c => 164
	i32 1170634674, ; 124: System.Web.dll => 0x45c677b2 => 153
	i32 1175144683, ; 125: Xamarin.AndroidX.VectorDrawable.Animated => 0x460b48eb => 226
	i32 1199003181, ; 126: MerchPlus => 0x4777562d => 0
	i32 1204270330, ; 127: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 185
	i32 1208641965, ; 128: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1219128291, ; 129: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1246548578, ; 130: Xamarin.AndroidX.Collection.Jvm.dll => 0x4a4cd262 => 189
	i32 1253011324, ; 131: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1264511973, ; 132: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 222
	i32 1264890200, ; 133: Xamarin.KotlinX.Coroutines.Core.dll => 0x4b64b158 => 237
	i32 1267360935, ; 134: Xamarin.AndroidX.VectorDrawable => 0x4b8a64a7 => 225
	i32 1275534314, ; 135: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 236
	i32 1278448581, ; 136: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 182
	i32 1292207520, ; 137: SQLitePCLRaw.core.dll => 0x4d0585a0 => 176
	i32 1293217323, ; 138: Xamarin.AndroidX.DrawerLayout.dll => 0x4d14ee2b => 199
	i32 1309188875, ; 139: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1324164729, ; 140: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 141: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1364015309, ; 142: System.IO => 0x514d38cd => 57
	i32 1376866003, ; 143: Xamarin.AndroidX.SavedState => 0x52114ed3 => 221
	i32 1379779777, ; 144: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1402170036, ; 145: System.Configuration.dll => 0x53936ab4 => 19
	i32 1406073936, ; 146: Xamarin.AndroidX.CoordinatorLayout => 0x53cefc50 => 193
	i32 1408764838, ; 147: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 148: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1422545099, ; 149: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1434145427, ; 150: System.Runtime.Handles => 0x557b5293 => 104
	i32 1437299793, ; 151: Xamarin.AndroidX.Lifecycle.Common.Jvm => 0x55ab7451 => 207
	i32 1439761251, ; 152: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1441095154, ; 153: Xamarin.AndroidX.Lifecycle.ViewModel.Android => 0x55e55df2 => 213
	i32 1452070440, ; 154: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 155: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 156: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 157: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1461234159, ; 158: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 159: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 160: System.IO.Compression.dll => 0x57261233 => 46
	i32 1469204771, ; 161: Xamarin.AndroidX.AppCompat.AppCompatResources => 0x57924923 => 184
	i32 1479771757, ; 162: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 163: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 164: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1536373174, ; 165: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 166: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 167: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 168: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1565862583, ; 169: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 170: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 171: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 172: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1582372066, ; 173: Xamarin.AndroidX.DocumentFile.dll => 0x5e5114e2 => 198
	i32 1592978981, ; 174: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1601112923, ; 175: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1604827217, ; 176: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 177: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622152042, ; 178: Xamarin.AndroidX.Loader.dll => 0x60b0136a => 215
	i32 1622358360, ; 179: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1624863272, ; 180: Xamarin.AndroidX.ViewPager2 => 0x60d97228 => 229
	i32 1635184631, ; 181: Xamarin.AndroidX.Emoji2.ViewsHelper => 0x6176eff7 => 202
	i32 1636350590, ; 182: Xamarin.AndroidX.CursorAdapter => 0x6188ba7e => 196
	i32 1639515021, ; 183: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 184: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 185: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 186: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 187: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 223
	i32 1658251792, ; 188: Xamarin.Google.Android.Material.dll => 0x62d6ea10 => 230
	i32 1670060433, ; 189: Xamarin.AndroidX.ConstraintLayout => 0x638b1991 => 191
	i32 1675553242, ; 190: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 191: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 192: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 193: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1691477237, ; 194: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 195: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1701541528, ; 196: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1711441057, ; 197: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 177
	i32 1726116996, ; 198: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 199: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1729485958, ; 200: Xamarin.AndroidX.CardView.dll => 0x6715dc86 => 187
	i32 1744735666, ; 201: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 202: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 203: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 204: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 205: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 206: System.Reflection.Extensions => 0x6942234e => 93
	i32 1776026572, ; 207: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 208: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 209: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1788241197, ; 210: Xamarin.AndroidX.Fragment => 0x6a96652d => 203
	i32 1808609942, ; 211: Xamarin.AndroidX.Loader => 0x6bcd3296 => 215
	i32 1813058853, ; 212: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 233
	i32 1813201214, ; 213: Xamarin.Google.Android.Material => 0x6c13413e => 230
	i32 1818787751, ; 214: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 215: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 216: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1858542181, ; 217: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 218: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1879696579, ; 219: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 220: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 186
	i32 1888955245, ; 221: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 222: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1898237753, ; 223: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 224: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1910275211, ; 225: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 226: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1956758971, ; 227: System.Resources.Writer => 0x74a1c5bb => 100
	i32 2011961780, ; 228: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2019465201, ; 229: Xamarin.AndroidX.Lifecycle.ViewModel => 0x785e97f1 => 212
	i32 2045470958, ; 230: System.Private.Xml => 0x79eb68ee => 88
	i32 2055257422, ; 231: Xamarin.AndroidX.Lifecycle.LiveData.Core.dll => 0x7a80bd4e => 208
	i32 2060060697, ; 232: System.Windows.dll => 0x7aca0819 => 154
	i32 2070888862, ; 233: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2079903147, ; 234: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 235: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2103459038, ; 236: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 178
	i32 2127167465, ; 237: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 238: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 239: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 240: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2193016926, ; 241: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2201107256, ; 242: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 238
	i32 2201231467, ; 243: System.Net.Http => 0x8334206b => 64
	i32 2217644978, ; 244: Xamarin.AndroidX.VectorDrawable.Animated.dll => 0x842e93b2 => 226
	i32 2222056684, ; 245: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2244775296, ; 246: Xamarin.AndroidX.LocalBroadcastManager => 0x85cc8d80 => 216
	i32 2252106437, ; 247: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2256313426, ; 248: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 249: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2279755925, ; 250: Xamarin.AndroidX.RecyclerView.dll => 0x87e25095 => 219
	i32 2293034957, ; 251: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 252: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 253: System.Net.Mail => 0x88ffe49e => 66
	i32 2305521784, ; 254: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2315684594, ; 255: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 180
	i32 2320631194, ; 256: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2340441535, ; 257: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 258: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2353062107, ; 259: System.Net.Primitives => 0x8c40e0db => 70
	i32 2368005991, ; 260: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2378619854, ; 261: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 262: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2401565422, ; 263: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2403452196, ; 264: Xamarin.AndroidX.Emoji2.dll => 0x8f41c524 => 201
	i32 2421380589, ; 265: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2435356389, ; 266: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 267: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 268: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 269: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 270: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2465273461, ; 271: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 175
	i32 2465532216, ; 272: Xamarin.AndroidX.ConstraintLayout.Core.dll => 0x92f50938 => 192
	i32 2471841756, ; 273: netstandard.dll => 0x93554fdc => 167
	i32 2475788418, ; 274: Java.Interop.dll => 0x93918882 => 168
	i32 2483903535, ; 275: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 276: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 277: System.AppContext.dll => 0x94798bc5 => 6
	i32 2491576599, ; 278: MerchPlus.dll => 0x94827117 => 0
	i32 2501346920, ; 279: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 280: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 210
	i32 2538310050, ; 281: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2562349572, ; 282: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 283: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2581819634, ; 284: Xamarin.AndroidX.VectorDrawable.dll => 0x99e370f2 => 225
	i32 2585220780, ; 285: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 286: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 287: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2605712449, ; 288: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 238
	i32 2617129537, ; 289: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 290: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2620871830, ; 291: Xamarin.AndroidX.CursorAdapter.dll => 0x9c375496 => 196
	i32 2624644809, ; 292: Xamarin.AndroidX.DynamicAnimation => 0x9c70e6c9 => 200
	i32 2627185994, ; 293: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2629843544, ; 294: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2663698177, ; 295: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 296: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 297: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2671474046, ; 298: Xamarin.KotlinX.Coroutines.Core => 0x9f3b757e => 237
	i32 2676780864, ; 299: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 300: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 301: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 302: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 223
	i32 2715334215, ; 303: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2717744543, ; 304: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 305: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2724373263, ; 306: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2732626843, ; 307: Xamarin.AndroidX.Activity => 0xa2e0939b => 179
	i32 2735172069, ; 308: System.Threading.Channels => 0xa30769e5 => 139
	i32 2737747696, ; 309: Xamarin.AndroidX.AppCompat.AppCompatResources.dll => 0xa32eb6f0 => 184
	i32 2740948882, ; 310: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 311: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2765824710, ; 312: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2766642685, ; 313: Xamarin.AndroidX.Lifecycle.ViewModel.Android.dll => 0xa4e79dfd => 213
	i32 2770495804, ; 314: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 232
	i32 2778768386, ; 315: Xamarin.AndroidX.ViewPager.dll => 0xa5a0a402 => 228
	i32 2779977773, ; 316: Xamarin.AndroidX.ResourceInspection.Annotation.dll => 0xa5b3182d => 220
	i32 2780199943, ; 317: Xamarin.AndroidX.Lifecycle.Common.Jvm.dll => 0xa5b67c07 => 207
	i32 2803228030, ; 318: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2810250172, ; 319: Xamarin.AndroidX.CoordinatorLayout.dll => 0xa78103bc => 193
	i32 2819470561, ; 320: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 321: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2821294376, ; 322: Xamarin.AndroidX.ResourceInspection.Annotation => 0xa8298928 => 220
	i32 2824502124, ; 323: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2849599387, ; 324: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2853208004, ; 325: Xamarin.AndroidX.ViewPager => 0xaa107fc4 => 228
	i32 2855708567, ; 326: Xamarin.AndroidX.Transition => 0xaa36a797 => 224
	i32 2861098320, ; 327: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2875220617, ; 328: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2887636118, ; 329: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 330: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 331: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 332: System.Reflection => 0xacf080de => 97
	i32 2905242038, ; 333: mscorlib.dll => 0xad2a79b6 => 166
	i32 2909740682, ; 334: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2916838712, ; 335: Xamarin.AndroidX.ViewPager2.dll => 0xaddb6d38 => 229
	i32 2919462931, ; 336: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 337: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 181
	i32 2936416060, ; 338: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 339: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 340: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2959614098, ; 341: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 342: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2972252294, ; 343: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2978675010, ; 344: Xamarin.AndroidX.DrawerLayout => 0xb18af942 => 199
	i32 2996846495, ; 345: Xamarin.AndroidX.Lifecycle.Process.dll => 0xb2a03f9f => 209
	i32 3016983068, ; 346: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 222
	i32 3023353419, ; 347: WindowsBase.dll => 0xb434b64b => 165
	i32 3024354802, ; 348: Xamarin.AndroidX.Legacy.Support.Core.Utils => 0xb443fdf2 => 205
	i32 3038032645, ; 349: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 240
	i32 3059408633, ; 350: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 351: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 352: System.Threading.Tasks => 0xb755818f => 144
	i32 3090735792, ; 353: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 354: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 355: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 356: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3121463068, ; 357: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 358: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 359: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3147165239, ; 360: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3159123045, ; 361: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 362: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3192346100, ; 363: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 364: System.Web => 0xbe592c0c => 153
	i32 3204380047, ; 365: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 366: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3211777861, ; 367: Xamarin.AndroidX.DocumentFile => 0xbf6fd745 => 198
	i32 3220365878, ; 368: System.Threading => 0xbff2e236 => 148
	i32 3226221578, ; 369: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3251039220, ; 370: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3258312781, ; 371: Xamarin.AndroidX.CardView => 0xc235e84d => 187
	i32 3265493905, ; 372: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 373: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3277815716, ; 374: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 375: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 376: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3286872994, ; 377: SQLite-net.dll => 0xc3e9b3a2 => 174
	i32 3290767353, ; 378: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 379: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 380: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3316684772, ; 381: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317135071, ; 382: Xamarin.AndroidX.CustomView.dll => 0xc5b776df => 197
	i32 3317144872, ; 383: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 384: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 186
	i32 3345895724, ; 385: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 218
	i32 3358260929, ; 386: System.Text.Json => 0xc82afec1 => 137
	i32 3360279109, ; 387: SQLitePCLRaw.core => 0xc849ca45 => 176
	i32 3362522851, ; 388: Xamarin.AndroidX.Core => 0xc86c06e3 => 194
	i32 3366347497, ; 389: Java.Interop => 0xc8a662e9 => 168
	i32 3374999561, ; 390: Xamarin.AndroidX.RecyclerView => 0xc92a6809 => 219
	i32 3395150330, ; 391: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 392: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3429136800, ; 393: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 394: netstandard => 0xcc7d82b4 => 167
	i32 3441283291, ; 395: Xamarin.AndroidX.DynamicAnimation.dll => 0xcd1dd0db => 200
	i32 3445260447, ; 396: System.Formats.Tar => 0xcd5a809f => 39
	i32 3471940407, ; 397: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 398: Mono.Android => 0xcf3163e6 => 171
	i32 3485117614, ; 399: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3486566296, ; 400: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 401: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 190
	i32 3509114376, ; 402: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 403: System.Security.dll => 0xd1854eb4 => 130
	i32 3530912306, ; 404: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 405: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3560100363, ; 406: System.Threading.Timer => 0xd432d20b => 147
	i32 3570554715, ; 407: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3598340787, ; 408: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 409: System.Linq.dll => 0xd715a361 => 61
	i32 3624195450, ; 410: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3627220390, ; 411: Xamarin.AndroidX.Print.dll => 0xd832fda6 => 217
	i32 3633644679, ; 412: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 181
	i32 3638274909, ; 413: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3641597786, ; 414: Xamarin.AndroidX.Lifecycle.LiveData.Core => 0xd90e5f5a => 208
	i32 3645089577, ; 415: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3660523487, ; 416: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3672681054, ; 417: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3684561358, ; 418: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 190
	i32 3700866549, ; 419: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3706696989, ; 420: Xamarin.AndroidX.Core.Core.Ktx.dll => 0xdcefb51d => 195
	i32 3716563718, ; 421: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 422: Xamarin.AndroidX.Annotation => 0xdda814c6 => 180
	i32 3732100267, ; 423: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 424: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 425: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 426: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3754567612, ; 427: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 178
	i32 3786282454, ; 428: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 188
	i32 3792276235, ; 429: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3802395368, ; 430: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 431: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3823082795, ; 432: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 433: System.Numerics.dll => 0xe4436460 => 83
	i32 3844307129, ; 434: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3849253459, ; 435: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3870376305, ; 436: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 437: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 438: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3876362041, ; 439: SQLite-net => 0xe70c9739 => 174
	i32 3885497537, ; 440: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3885922214, ; 441: Xamarin.AndroidX.Transition.dll => 0xe79e77a6 => 224
	i32 3888767677, ; 442: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 218
	i32 3896106733, ; 443: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 444: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 194
	i32 3901907137, ; 445: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3910130544, ; 446: Xamarin.AndroidX.Collection.Jvm => 0xe90fdb70 => 189
	i32 3920810846, ; 447: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 448: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 227
	i32 3928044579, ; 449: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 450: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3945713374, ; 451: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 452: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 3955647286, ; 453: Xamarin.AndroidX.AppCompat.dll => 0xebc66336 => 183
	i32 3959773229, ; 454: Xamarin.AndroidX.Lifecycle.Process => 0xec05582d => 209
	i32 4003436829, ; 455: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 456: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 182
	i32 4025784931, ; 457: System.Memory => 0xeff49a63 => 62
	i32 4054681211, ; 458: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4068434129, ; 459: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4073602200, ; 460: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4099507663, ; 461: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 462: System.Private.Uri => 0xf462c30d => 86
	i32 4101593132, ; 463: Xamarin.AndroidX.Emoji2 => 0xf479582c => 201
	i32 4127667938, ; 464: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 465: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 466: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4151237749, ; 467: System.Core => 0xf76edc75 => 21
	i32 4157155888, ; 468: entMerchPlus => 0xf7c92a30 => 239
	i32 4159265925, ; 469: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 470: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 471: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 472: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4182413190, ; 473: Xamarin.AndroidX.Lifecycle.ViewModelSavedState.dll => 0xf94a8f86 => 214
	i32 4185676441, ; 474: System.Security => 0xf97c5a99 => 130
	i32 4196529839, ; 475: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 476: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4228543782, ; 477: Xamarin.KotlinX.AtomicFU.Jvm.dll => 0xfc0a7526 => 235
	i32 4256097574, ; 478: Xamarin.AndroidX.Core.Core.Ktx => 0xfdaee526 => 195
	i32 4260525087, ; 479: System.Buffers => 0xfdf2741f => 7
	i32 4274976490, ; 480: System.Runtime.Numerics => 0xfecef6ea => 110
	i32 4292120959 ; 481: Xamarin.AndroidX.Lifecycle.ViewModelSavedState => 0xffd4917f => 214
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [482 x i32] [
	i32 68, ; 0
	i32 67, ; 1
	i32 108, ; 2
	i32 210, ; 3
	i32 231, ; 4
	i32 48, ; 5
	i32 173, ; 6
	i32 80, ; 7
	i32 145, ; 8
	i32 235, ; 9
	i32 30, ; 10
	i32 124, ; 11
	i32 102, ; 12
	i32 107, ; 13
	i32 139, ; 14
	i32 77, ; 15
	i32 124, ; 16
	i32 13, ; 17
	i32 188, ; 18
	i32 234, ; 19
	i32 132, ; 20
	i32 151, ; 21
	i32 18, ; 22
	i32 26, ; 23
	i32 205, ; 24
	i32 1, ; 25
	i32 59, ; 26
	i32 42, ; 27
	i32 91, ; 28
	i32 191, ; 29
	i32 147, ; 30
	i32 204, ; 31
	i32 54, ; 32
	i32 69, ; 33
	i32 179, ; 34
	i32 83, ; 35
	i32 206, ; 36
	i32 177, ; 37
	i32 131, ; 38
	i32 55, ; 39
	i32 149, ; 40
	i32 74, ; 41
	i32 145, ; 42
	i32 62, ; 43
	i32 146, ; 44
	i32 240, ; 45
	i32 165, ; 46
	i32 211, ; 47
	i32 239, ; 48
	i32 192, ; 49
	i32 12, ; 50
	i32 203, ; 51
	i32 125, ; 52
	i32 152, ; 53
	i32 113, ; 54
	i32 166, ; 55
	i32 164, ; 56
	i32 204, ; 57
	i32 216, ; 58
	i32 84, ; 59
	i32 150, ; 60
	i32 60, ; 61
	i32 51, ; 62
	i32 103, ; 63
	i32 114, ; 64
	i32 40, ; 65
	i32 120, ; 66
	i32 52, ; 67
	i32 44, ; 68
	i32 119, ; 69
	i32 211, ; 70
	i32 197, ; 71
	i32 202, ; 72
	i32 81, ; 73
	i32 136, ; 74
	i32 227, ; 75
	i32 185, ; 76
	i32 8, ; 77
	i32 234, ; 78
	i32 73, ; 79
	i32 155, ; 80
	i32 236, ; 81
	i32 154, ; 82
	i32 92, ; 83
	i32 232, ; 84
	i32 45, ; 85
	i32 109, ; 86
	i32 129, ; 87
	i32 175, ; 88
	i32 25, ; 89
	i32 72, ; 90
	i32 55, ; 91
	i32 46, ; 92
	i32 22, ; 93
	i32 86, ; 94
	i32 43, ; 95
	i32 160, ; 96
	i32 71, ; 97
	i32 217, ; 98
	i32 3, ; 99
	i32 42, ; 100
	i32 63, ; 101
	i32 16, ; 102
	i32 53, ; 103
	i32 231, ; 104
	i32 105, ; 105
	i32 173, ; 106
	i32 206, ; 107
	i32 34, ; 108
	i32 158, ; 109
	i32 85, ; 110
	i32 32, ; 111
	i32 12, ; 112
	i32 51, ; 113
	i32 56, ; 114
	i32 221, ; 115
	i32 36, ; 116
	i32 183, ; 117
	i32 35, ; 118
	i32 58, ; 119
	i32 212, ; 120
	i32 17, ; 121
	i32 233, ; 122
	i32 164, ; 123
	i32 153, ; 124
	i32 226, ; 125
	i32 0, ; 126
	i32 185, ; 127
	i32 29, ; 128
	i32 52, ; 129
	i32 189, ; 130
	i32 5, ; 131
	i32 222, ; 132
	i32 237, ; 133
	i32 225, ; 134
	i32 236, ; 135
	i32 182, ; 136
	i32 176, ; 137
	i32 199, ; 138
	i32 85, ; 139
	i32 61, ; 140
	i32 112, ; 141
	i32 57, ; 142
	i32 221, ; 143
	i32 99, ; 144
	i32 19, ; 145
	i32 193, ; 146
	i32 111, ; 147
	i32 101, ; 148
	i32 102, ; 149
	i32 104, ; 150
	i32 207, ; 151
	i32 71, ; 152
	i32 213, ; 153
	i32 38, ; 154
	i32 32, ; 155
	i32 103, ; 156
	i32 73, ; 157
	i32 9, ; 158
	i32 123, ; 159
	i32 46, ; 160
	i32 184, ; 161
	i32 9, ; 162
	i32 43, ; 163
	i32 4, ; 164
	i32 31, ; 165
	i32 138, ; 166
	i32 92, ; 167
	i32 93, ; 168
	i32 49, ; 169
	i32 141, ; 170
	i32 112, ; 171
	i32 140, ; 172
	i32 198, ; 173
	i32 115, ; 174
	i32 157, ; 175
	i32 76, ; 176
	i32 79, ; 177
	i32 215, ; 178
	i32 37, ; 179
	i32 229, ; 180
	i32 202, ; 181
	i32 196, ; 182
	i32 64, ; 183
	i32 138, ; 184
	i32 15, ; 185
	i32 116, ; 186
	i32 223, ; 187
	i32 230, ; 188
	i32 191, ; 189
	i32 48, ; 190
	i32 70, ; 191
	i32 80, ; 192
	i32 126, ; 193
	i32 94, ; 194
	i32 121, ; 195
	i32 26, ; 196
	i32 177, ; 197
	i32 97, ; 198
	i32 28, ; 199
	i32 187, ; 200
	i32 149, ; 201
	i32 169, ; 202
	i32 4, ; 203
	i32 98, ; 204
	i32 33, ; 205
	i32 93, ; 206
	i32 21, ; 207
	i32 41, ; 208
	i32 170, ; 209
	i32 203, ; 210
	i32 215, ; 211
	i32 233, ; 212
	i32 230, ; 213
	i32 2, ; 214
	i32 134, ; 215
	i32 111, ; 216
	i32 58, ; 217
	i32 95, ; 218
	i32 39, ; 219
	i32 186, ; 220
	i32 25, ; 221
	i32 94, ; 222
	i32 89, ; 223
	i32 99, ; 224
	i32 10, ; 225
	i32 87, ; 226
	i32 100, ; 227
	i32 7, ; 228
	i32 212, ; 229
	i32 88, ; 230
	i32 208, ; 231
	i32 154, ; 232
	i32 33, ; 233
	i32 116, ; 234
	i32 82, ; 235
	i32 178, ; 236
	i32 20, ; 237
	i32 11, ; 238
	i32 162, ; 239
	i32 3, ; 240
	i32 84, ; 241
	i32 238, ; 242
	i32 64, ; 243
	i32 226, ; 244
	i32 143, ; 245
	i32 216, ; 246
	i32 157, ; 247
	i32 41, ; 248
	i32 117, ; 249
	i32 219, ; 250
	i32 131, ; 251
	i32 75, ; 252
	i32 66, ; 253
	i32 172, ; 254
	i32 180, ; 255
	i32 143, ; 256
	i32 106, ; 257
	i32 151, ; 258
	i32 70, ; 259
	i32 156, ; 260
	i32 121, ; 261
	i32 127, ; 262
	i32 152, ; 263
	i32 201, ; 264
	i32 141, ; 265
	i32 20, ; 266
	i32 14, ; 267
	i32 135, ; 268
	i32 75, ; 269
	i32 59, ; 270
	i32 175, ; 271
	i32 192, ; 272
	i32 167, ; 273
	i32 168, ; 274
	i32 15, ; 275
	i32 74, ; 276
	i32 6, ; 277
	i32 0, ; 278
	i32 23, ; 279
	i32 210, ; 280
	i32 91, ; 281
	i32 1, ; 282
	i32 136, ; 283
	i32 225, ; 284
	i32 134, ; 285
	i32 69, ; 286
	i32 146, ; 287
	i32 238, ; 288
	i32 88, ; 289
	i32 96, ; 290
	i32 196, ; 291
	i32 200, ; 292
	i32 31, ; 293
	i32 45, ; 294
	i32 109, ; 295
	i32 158, ; 296
	i32 35, ; 297
	i32 237, ; 298
	i32 22, ; 299
	i32 114, ; 300
	i32 57, ; 301
	i32 223, ; 302
	i32 144, ; 303
	i32 118, ; 304
	i32 120, ; 305
	i32 110, ; 306
	i32 179, ; 307
	i32 139, ; 308
	i32 184, ; 309
	i32 54, ; 310
	i32 105, ; 311
	i32 133, ; 312
	i32 213, ; 313
	i32 232, ; 314
	i32 228, ; 315
	i32 220, ; 316
	i32 207, ; 317
	i32 159, ; 318
	i32 193, ; 319
	i32 163, ; 320
	i32 132, ; 321
	i32 220, ; 322
	i32 161, ; 323
	i32 140, ; 324
	i32 228, ; 325
	i32 224, ; 326
	i32 169, ; 327
	i32 40, ; 328
	i32 81, ; 329
	i32 56, ; 330
	i32 37, ; 331
	i32 97, ; 332
	i32 166, ; 333
	i32 172, ; 334
	i32 229, ; 335
	i32 82, ; 336
	i32 181, ; 337
	i32 98, ; 338
	i32 30, ; 339
	i32 159, ; 340
	i32 18, ; 341
	i32 127, ; 342
	i32 119, ; 343
	i32 199, ; 344
	i32 209, ; 345
	i32 222, ; 346
	i32 165, ; 347
	i32 205, ; 348
	i32 240, ; 349
	i32 170, ; 350
	i32 16, ; 351
	i32 144, ; 352
	i32 125, ; 353
	i32 118, ; 354
	i32 38, ; 355
	i32 115, ; 356
	i32 47, ; 357
	i32 142, ; 358
	i32 117, ; 359
	i32 34, ; 360
	i32 95, ; 361
	i32 53, ; 362
	i32 129, ; 363
	i32 153, ; 364
	i32 24, ; 365
	i32 161, ; 366
	i32 198, ; 367
	i32 148, ; 368
	i32 104, ; 369
	i32 89, ; 370
	i32 187, ; 371
	i32 60, ; 372
	i32 142, ; 373
	i32 100, ; 374
	i32 5, ; 375
	i32 13, ; 376
	i32 174, ; 377
	i32 122, ; 378
	i32 135, ; 379
	i32 28, ; 380
	i32 72, ; 381
	i32 197, ; 382
	i32 24, ; 383
	i32 186, ; 384
	i32 218, ; 385
	i32 137, ; 386
	i32 176, ; 387
	i32 194, ; 388
	i32 168, ; 389
	i32 219, ; 390
	i32 101, ; 391
	i32 123, ; 392
	i32 163, ; 393
	i32 167, ; 394
	i32 200, ; 395
	i32 39, ; 396
	i32 17, ; 397
	i32 171, ; 398
	i32 137, ; 399
	i32 150, ; 400
	i32 190, ; 401
	i32 155, ; 402
	i32 130, ; 403
	i32 19, ; 404
	i32 65, ; 405
	i32 147, ; 406
	i32 47, ; 407
	i32 79, ; 408
	i32 61, ; 409
	i32 106, ; 410
	i32 217, ; 411
	i32 181, ; 412
	i32 49, ; 413
	i32 208, ; 414
	i32 14, ; 415
	i32 68, ; 416
	i32 171, ; 417
	i32 190, ; 418
	i32 78, ; 419
	i32 195, ; 420
	i32 108, ; 421
	i32 180, ; 422
	i32 67, ; 423
	i32 63, ; 424
	i32 27, ; 425
	i32 160, ; 426
	i32 178, ; 427
	i32 188, ; 428
	i32 10, ; 429
	i32 11, ; 430
	i32 78, ; 431
	i32 126, ; 432
	i32 83, ; 433
	i32 66, ; 434
	i32 107, ; 435
	i32 65, ; 436
	i32 128, ; 437
	i32 122, ; 438
	i32 174, ; 439
	i32 77, ; 440
	i32 224, ; 441
	i32 218, ; 442
	i32 8, ; 443
	i32 194, ; 444
	i32 2, ; 445
	i32 189, ; 446
	i32 44, ; 447
	i32 227, ; 448
	i32 156, ; 449
	i32 128, ; 450
	i32 23, ; 451
	i32 133, ; 452
	i32 183, ; 453
	i32 209, ; 454
	i32 29, ; 455
	i32 182, ; 456
	i32 62, ; 457
	i32 90, ; 458
	i32 87, ; 459
	i32 148, ; 460
	i32 36, ; 461
	i32 86, ; 462
	i32 201, ; 463
	i32 50, ; 464
	i32 6, ; 465
	i32 90, ; 466
	i32 21, ; 467
	i32 239, ; 468
	i32 162, ; 469
	i32 96, ; 470
	i32 50, ; 471
	i32 113, ; 472
	i32 214, ; 473
	i32 130, ; 474
	i32 76, ; 475
	i32 27, ; 476
	i32 235, ; 477
	i32 195, ; 478
	i32 7, ; 479
	i32 110, ; 480
	i32 214 ; 481
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

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
