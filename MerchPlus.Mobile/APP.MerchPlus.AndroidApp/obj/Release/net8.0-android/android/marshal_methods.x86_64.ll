; ModuleID = 'marshal_methods.x86_64.ll'
source_filename = "marshal_methods.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [69 x ptr] zeroinitializer, align 16

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [138 x i64] [
	i64 120698629574877762, ; 0: Mono.Android => 0x1accec39cafe242 => 68
	i64 196720943101637631, ; 1: System.Linq.Expressions.dll => 0x2bae4a7cd73f3ff => 28
	i64 560278790331054453, ; 2: System.Reflection.Primitives => 0x7c6829760de3975 => 47
	i64 799765834175365804, ; 3: System.ComponentModel.dll => 0xb1956c9f18442ac => 18
	i64 805076839809229382, ; 4: entMerchPlus.dll => 0xb2c351ecdb92246 => 8
	i64 870603111519317375, ; 5: SQLitePCLRaw.lib.e_sqlite3.android => 0xc1500ead2756d7f => 5
	i64 1010599046655515943, ; 6: System.Reflection.Primitives.dll => 0xe065e7a82401d27 => 47
	i64 1268860745194512059, ; 7: System.Drawing.dll => 0x119be62002c19ebb => 24
	i64 1301485588176585670, ; 8: SQLitePCLRaw.core => 0x120fce3f338e43c6 => 4
	i64 1476839205573959279, ; 9: System.Net.Primitives.dll => 0x147ec96ece9b1e6f => 34
	i64 1513467482682125403, ; 10: Mono.Android.Runtime => 0x1500eaa8245f6c5b => 67
	i64 1518315023656898250, ; 11: SQLitePCLRaw.provider.e_sqlite3 => 0x151223783a354eca => 6
	i64 1694164115776082462, ; 12: MerchPlus => 0x1782e149880c121e => 9
	i64 1731380447121279447, ; 13: Newtonsoft.Json => 0x18071957e9b889d7 => 1
	i64 1743969030606105336, ; 14: System.Memory.dll => 0x1833d297e88f2af8 => 30
	i64 1767386781656293639, ; 15: System.Private.Uri.dll => 0x188704e9f5582107 => 42
	i64 1825687700144851180, ; 16: System.Runtime.InteropServices.RuntimeInformation.dll => 0x1956254a55ef08ec => 48
	i64 1875417405349196092, ; 17: System.Drawing.Primitives => 0x1a06d2319b6c713c => 23
	i64 2102659300918482391, ; 18: System.Drawing.Primitives.dll => 0x1d2e257e6aead5d7 => 23
	i64 2133195048986300728, ; 19: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 1
	i64 2287834202362508563, ; 20: System.Collections.Concurrent => 0x1fc00515e8ce7513 => 11
	i64 2497223385847772520, ; 21: System.Runtime => 0x22a7eb7046413568 => 53
	i64 2624866290265602282, ; 22: mscorlib.dll => 0x246d65fbde2db8ea => 63
	i64 2632269733008246987, ; 23: System.Net.NameResolution => 0x2487b36034f808cb => 32
	i64 3325875462027654285, ; 24: System.Runtime.Numerics => 0x2e27e21c8958b48d => 50
	i64 3328853167529574890, ; 25: System.Net.Sockets.dll => 0x2e327651a008c1ea => 38
	i64 3496051521505210536, ; 26: entMerchPlus => 0x30847855ffc830a8 => 8
	i64 3551103847008531295, ; 27: System.Private.CoreLib.dll => 0x31480e226177735f => 65
	i64 3571415421602489686, ; 28: System.Runtime.dll => 0x319037675df7e556 => 53
	i64 3647754201059316852, ; 29: System.Xml.ReaderWriter => 0x329f6d1e86145474 => 60
	i64 3716579019761409177, ; 30: netstandard.dll => 0x3393f0ed5c8c5c99 => 64
	i64 3933965368022646939, ; 31: System.Net.Requests => 0x369840a8bfadc09b => 35
	i64 3966267475168208030, ; 32: System.Memory => 0x370b03412596249e => 30
	i64 4009997192427317104, ; 33: System.Runtime.Serialization.Primitives => 0x37a65f335cf1a770 => 52
	i64 4073500526318903918, ; 34: System.Private.Xml.dll => 0x3887fb25779ae26e => 44
	i64 4154383907710350974, ; 35: System.ComponentModel => 0x39a7562737acb67e => 18
	i64 4187479170553454871, ; 36: System.Linq.Expressions => 0x3a1cea1e912fa117 => 28
	i64 4205801962323029395, ; 37: System.ComponentModel.TypeConverter => 0x3a5e0299f7e7ad93 => 17
	i64 4337444564132831293, ; 38: SQLitePCLRaw.batteries_v2.dll => 0x3c31b2d9ae16203d => 3
	i64 4809057822547766521, ; 39: System.Drawing => 0x42bd349c3145ecf9 => 24
	i64 4814660307502931973, ; 40: System.Net.NameResolution.dll => 0x42d11c0a5ee2a005 => 32
	i64 5103417709280584325, ; 41: System.Collections.Specialized => 0x46d2fb5e161b6285 => 13
	i64 5182934613077526976, ; 42: System.Collections.Specialized.dll => 0x47ed7b91fa9009c0 => 13
	i64 5278787618751394462, ; 43: System.Net.WebClient.dll => 0x4942055efc68329e => 39
	i64 5423376490970181369, ; 44: System.Runtime.InteropServices.RuntimeInformation => 0x4b43b42f2b7b6ef9 => 48
	i64 5570799893513421663, ; 45: System.IO.Compression.Brotli => 0x4d4f74fcdfa6c35f => 26
	i64 5573260873512690141, ; 46: System.Security.Cryptography.dll => 0x4d58333c6e4ea1dd => 54
	i64 5979151488806146654, ; 47: System.Formats.Asn1 => 0x52fa3699a489d25e => 25
	i64 6183170893902868313, ; 48: SQLitePCLRaw.batteries_v2 => 0x55cf092b0c9d6f59 => 3
	i64 6284145129771520194, ; 49: System.Reflection.Emit.ILGeneration => 0x5735c4b3610850c2 => 45
	i64 6357457916754632952, ; 50: _Microsoft.Android.Resource.Designer => 0x583a3a4ac2a7a0f8 => 0
	i64 6786606130239981554, ; 51: System.Diagnostics.TraceSource => 0x5e2ede51877147f2 => 22
	i64 6814185388980153342, ; 52: System.Xml.XDocument.dll => 0x5e90d98217d1abfe => 61
	i64 6876862101832370452, ; 53: System.Xml.Linq => 0x5f6f85a57d108914 => 59
	i64 7083547580668757502, ; 54: System.Private.Xml.Linq.dll => 0x624dd0fe8f56c5fe => 43
	i64 7270811800166795866, ; 55: System.Linq => 0x64e71ccf51a90a5a => 29
	i64 7377312882064240630, ; 56: System.ComponentModel.TypeConverter.dll => 0x66617afac45a2ff6 => 17
	i64 7488575175965059935, ; 57: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 59
	i64 7489048572193775167, ; 58: System.ObjectModel => 0x67ee71ff6b419e3f => 41
	i64 7654504624184590948, ; 59: System.Net.Http => 0x6a3a4366801b8264 => 31
	i64 7714652370974252055, ; 60: System.Private.CoreLib => 0x6b0ff375198b9c17 => 65
	i64 7735176074855944702, ; 61: Microsoft.CSharp => 0x6b58dda848e391fe => 10
	i64 8064050204834738623, ; 62: System.Collections.dll => 0x6fe942efa61731bf => 14
	i64 8087206902342787202, ; 63: System.Diagnostics.DiagnosticSource => 0x703b87d46f3aa082 => 21
	i64 8167236081217502503, ; 64: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 66
	i64 8185542183669246576, ; 65: System.Collections => 0x7198e33f4794aa70 => 14
	i64 8368701292315763008, ; 66: System.Security.Cryptography => 0x7423997c6fd56140 => 54
	i64 8410671156615598628, ; 67: System.Reflection.Emit.Lightweight.dll => 0x74b8b4daf4b25224 => 46
	i64 8563666267364444763, ; 68: System.Private.Uri => 0x76d841191140ca5b => 42
	i64 8626175481042262068, ; 69: Java.Interop => 0x77b654e585b55834 => 66
	i64 8638972117149407195, ; 70: Microsoft.CSharp.dll => 0x77e3cb5e8b31d7db => 10
	i64 8725526185868997716, ; 71: System.Diagnostics.DiagnosticSource.dll => 0x79174bd613173454 => 21
	i64 8941376889969657626, ; 72: System.Xml.XDocument => 0x7c1626e87187471a => 61
	i64 9659729154652888475, ; 73: System.Text.RegularExpressions => 0x860e407c9991dd9b => 56
	i64 9702891218465930390, ; 74: System.Collections.NonGeneric.dll => 0x86a79827b2eb3c96 => 12
	i64 9808709177481450983, ; 75: Mono.Android.dll => 0x881f890734e555e7 => 68
	i64 10038780035334861115, ; 76: System.Net.Http.dll => 0x8b50e941206af13b => 31
	i64 10051358222726253779, ; 77: System.Private.Xml => 0x8b7d990c97ccccd3 => 44
	i64 10236703004850800690, ; 78: System.Net.ServicePoint.dll => 0x8e101325834e4832 => 37
	i64 10245369515835430794, ; 79: System.Reflection.Emit.Lightweight => 0x8e2edd4ad7fc978a => 46
	i64 10364469296367737616, ; 80: System.Reflection.Emit.ILGeneration.dll => 0x8fd5fde967711b10 => 45
	i64 10430153318873392755, ; 81: Xamarin.AndroidX.Core => 0x90bf592ea44f6673 => 7
	i64 10785150219063592792, ; 82: System.Net.Primitives => 0x95ac8cfb68830758 => 34
	i64 11485890710487134646, ; 83: System.Runtime.InteropServices => 0x9f6614bf0f8b71b6 => 49
	i64 11597940890313164233, ; 84: netstandard => 0xa0f429ca8d1805c9 => 64
	i64 11739066727115742305, ; 85: SQLite-net.dll => 0xa2e98afdf8575c61 => 2
	i64 11806260347154423189, ; 86: SQLite-net => 0xa3d8433bc5eb5d95 => 2
	i64 12040886584167504988, ; 87: System.Net.ServicePoint => 0xa719d28d8e121c5c => 37
	i64 12201331334810686224, ; 88: System.Runtime.Serialization.Primitives.dll => 0xa953d6341e3bd310 => 52
	i64 12279246230491828964, ; 89: SQLitePCLRaw.provider.e_sqlite3.dll => 0xaa68a5636e0512e4 => 6
	i64 12475113361194491050, ; 90: _Microsoft.Android.Resource.Designer.dll => 0xad2081818aba1caa => 0
	i64 12517810545449516888, ; 91: System.Diagnostics.TraceSource.dll => 0xadb8325e6f283f58 => 22
	i64 12550732019250633519, ; 92: System.IO.Compression => 0xae2d28465e8e1b2f => 27
	i64 12708922737231849740, ; 93: System.Text.Encoding.Extensions => 0xb05f29e50e96e90c => 55
	i64 12859557719246324186, ; 94: System.Net.WebHeaderCollection.dll => 0xb276539ce04f41da => 40
	i64 13068258254871114833, ; 95: System.Runtime.Serialization.Formatters.dll => 0xb55bc7a4eaa8b451 => 51
	i64 13343850469010654401, ; 96: Mono.Android.Runtime.dll => 0xb92ee14d854f44c1 => 67
	i64 13431476299110033919, ; 97: System.Net.WebClient => 0xba663087f18829ff => 39
	i64 13717397318615465333, ; 98: System.ComponentModel.Primitives.dll => 0xbe5dfc2ef2f87d75 => 16
	i64 13881769479078963060, ; 99: System.Console.dll => 0xc0a5f3cade5c6774 => 19
	i64 14125464355221830302, ; 100: System.Threading.dll => 0xc407bafdbc707a9e => 58
	i64 14254574811015963973, ; 101: System.Text.Encoding.Extensions.dll => 0xc5d26c4442d66545 => 55
	i64 14461014870687870182, ; 102: System.Net.Requests.dll => 0xc8afd8683afdece6 => 35
	i64 14622043554576106986, ; 103: System.Runtime.Serialization.Formatters => 0xcaebef2458cc85ea => 51
	i64 14984936317414011727, ; 104: System.Net.WebHeaderCollection => 0xcff5302fe54ff34f => 40
	i64 14987728460634540364, ; 105: System.IO.Compression.dll => 0xcfff1ba06622494c => 27
	i64 15015154896917945444, ; 106: System.Net.Security.dll => 0xd0608bd33642dc64 => 36
	i64 15076659072870671916, ; 107: System.ObjectModel.dll => 0xd13b0d8c1620662c => 41
	i64 15115185479366240210, ; 108: System.IO.Compression.Brotli.dll => 0xd1c3ed1c1bc467d2 => 26
	i64 15133485256822086103, ; 109: System.Linq.dll => 0xd204f0a9127dd9d7 => 29
	i64 15370334346939861994, ; 110: Xamarin.AndroidX.Core.dll => 0xd54e65a72c560bea => 7
	i64 15527772828719725935, ; 111: System.Console => 0xd77dbb1e38cd3d6f => 19
	i64 15557562860424774966, ; 112: System.Net.Sockets => 0xd7e790fe7a6dc536 => 38
	i64 15609085926864131306, ; 113: System.dll => 0xd89e9cf3334914ea => 62
	i64 15661133872274321916, ; 114: System.Xml.ReaderWriter.dll => 0xd9578647d4bfb1fc => 60
	i64 15847085070278954535, ; 115: System.Threading.Channels.dll => 0xdbec27e8f35f8e27 => 57
	i64 16018552496348375205, ; 116: System.Net.NetworkInformation.dll => 0xde4d54a020caa8a5 => 33
	i64 16154507427712707110, ; 117: System => 0xe03056ea4e39aa26 => 62
	i64 16219561732052121626, ; 118: System.Net.Security => 0xe1177575db7c781a => 36
	i64 16454459195343277943, ; 119: System.Net.NetworkInformation => 0xe459fb756d988f77 => 33
	i64 16755018182064898362, ; 120: SQLitePCLRaw.core.dll => 0xe885c843c330813a => 4
	i64 16833383113903931215, ; 121: mscorlib => 0xe99c30c1484d7f4f => 63
	i64 16856067890322379635, ; 122: System.Data.Common.dll => 0xe9ecc87060889373 => 20
	i64 16890310621557459193, ; 123: System.Text.RegularExpressions.dll => 0xea66700587f088f9 => 56
	i64 17008137082415910100, ; 124: System.Collections.NonGeneric => 0xec090a90408c8cd4 => 12
	i64 17062143951396181894, ; 125: System.ComponentModel.Primitives => 0xecc8e986518c9786 => 16
	i64 17118171214553292978, ; 126: System.Threading.Channels => 0xed8ff6060fc420b2 => 57
	i64 17201328579425343169, ; 127: System.ComponentModel.EventBasedAsync => 0xeeb76534d96c16c1 => 15
	i64 17230721278011714856, ; 128: System.Private.Xml.Linq => 0xef1fd1b5c7a72d28 => 43
	i64 17260702271250283638, ; 129: System.Data.Common => 0xef8a5543bba6bc76 => 20
	i64 17551279108174733181, ; 130: MerchPlus.dll => 0xf392ab5bb47d5f7d => 9
	i64 17712670374920797664, ; 131: System.Runtime.InteropServices.dll => 0xf5d00bdc38bd3de0 => 49
	i64 17777860260071588075, ; 132: System.Runtime.Numerics.dll => 0xf6b7a5b72419c0eb => 50
	i64 18025913125965088385, ; 133: System.Threading => 0xfa28e87b91334681 => 58
	i64 18146411883821974900, ; 134: System.Formats.Asn1.dll => 0xfbd50176eb22c574 => 25
	i64 18146811631844267958, ; 135: System.ComponentModel.EventBasedAsync.dll => 0xfbd66d08820117b6 => 15
	i64 18245806341561545090, ; 136: System.Collections.Concurrent.dll => 0xfd3620327d587182 => 11
	i64 18370042311372477656 ; 137: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0xfeef80274e4094d8 => 5
], align 16

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [138 x i32] [
	i32 68, ; 0
	i32 28, ; 1
	i32 47, ; 2
	i32 18, ; 3
	i32 8, ; 4
	i32 5, ; 5
	i32 47, ; 6
	i32 24, ; 7
	i32 4, ; 8
	i32 34, ; 9
	i32 67, ; 10
	i32 6, ; 11
	i32 9, ; 12
	i32 1, ; 13
	i32 30, ; 14
	i32 42, ; 15
	i32 48, ; 16
	i32 23, ; 17
	i32 23, ; 18
	i32 1, ; 19
	i32 11, ; 20
	i32 53, ; 21
	i32 63, ; 22
	i32 32, ; 23
	i32 50, ; 24
	i32 38, ; 25
	i32 8, ; 26
	i32 65, ; 27
	i32 53, ; 28
	i32 60, ; 29
	i32 64, ; 30
	i32 35, ; 31
	i32 30, ; 32
	i32 52, ; 33
	i32 44, ; 34
	i32 18, ; 35
	i32 28, ; 36
	i32 17, ; 37
	i32 3, ; 38
	i32 24, ; 39
	i32 32, ; 40
	i32 13, ; 41
	i32 13, ; 42
	i32 39, ; 43
	i32 48, ; 44
	i32 26, ; 45
	i32 54, ; 46
	i32 25, ; 47
	i32 3, ; 48
	i32 45, ; 49
	i32 0, ; 50
	i32 22, ; 51
	i32 61, ; 52
	i32 59, ; 53
	i32 43, ; 54
	i32 29, ; 55
	i32 17, ; 56
	i32 59, ; 57
	i32 41, ; 58
	i32 31, ; 59
	i32 65, ; 60
	i32 10, ; 61
	i32 14, ; 62
	i32 21, ; 63
	i32 66, ; 64
	i32 14, ; 65
	i32 54, ; 66
	i32 46, ; 67
	i32 42, ; 68
	i32 66, ; 69
	i32 10, ; 70
	i32 21, ; 71
	i32 61, ; 72
	i32 56, ; 73
	i32 12, ; 74
	i32 68, ; 75
	i32 31, ; 76
	i32 44, ; 77
	i32 37, ; 78
	i32 46, ; 79
	i32 45, ; 80
	i32 7, ; 81
	i32 34, ; 82
	i32 49, ; 83
	i32 64, ; 84
	i32 2, ; 85
	i32 2, ; 86
	i32 37, ; 87
	i32 52, ; 88
	i32 6, ; 89
	i32 0, ; 90
	i32 22, ; 91
	i32 27, ; 92
	i32 55, ; 93
	i32 40, ; 94
	i32 51, ; 95
	i32 67, ; 96
	i32 39, ; 97
	i32 16, ; 98
	i32 19, ; 99
	i32 58, ; 100
	i32 55, ; 101
	i32 35, ; 102
	i32 51, ; 103
	i32 40, ; 104
	i32 27, ; 105
	i32 36, ; 106
	i32 41, ; 107
	i32 26, ; 108
	i32 29, ; 109
	i32 7, ; 110
	i32 19, ; 111
	i32 38, ; 112
	i32 62, ; 113
	i32 60, ; 114
	i32 57, ; 115
	i32 33, ; 116
	i32 62, ; 117
	i32 36, ; 118
	i32 33, ; 119
	i32 4, ; 120
	i32 63, ; 121
	i32 20, ; 122
	i32 56, ; 123
	i32 12, ; 124
	i32 16, ; 125
	i32 57, ; 126
	i32 15, ; 127
	i32 43, ; 128
	i32 20, ; 129
	i32 9, ; 130
	i32 49, ; 131
	i32 50, ; 132
	i32 58, ; 133
	i32 25, ; 134
	i32 15, ; 135
	i32 11, ; 136
	i32 5 ; 137
], align 16

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 8

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 8

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 8

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 8, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 16

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "target-cpu"="x86-64" "target-features"="+crc32,+cx16,+cx8,+fxsr,+mmx,+popcnt,+sse,+sse2,+sse3,+sse4.1,+sse4.2,+ssse3,+x87" "tune-cpu"="generic" }

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
