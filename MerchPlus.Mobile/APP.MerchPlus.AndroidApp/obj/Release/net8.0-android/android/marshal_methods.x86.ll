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

@assembly_image_cache = dso_local local_unnamed_addr global [69 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [138 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 33
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 32
	i32 39109920, ; 2: Newtonsoft.Json.dll => 0x254c520 => 1
	i32 117431740, ; 3: System.Runtime.InteropServices => 0x6ffddbc => 49
	i32 122350210, ; 4: System.Threading.Channels.dll => 0x74aea82 => 57
	i32 142721839, ; 5: System.Net.WebHeaderCollection => 0x881c32f => 40
	i32 205061960, ; 6: System.ComponentModel => 0xc38ff48 => 18
	i32 230752869, ; 7: Microsoft.CSharp.dll => 0xdc10265 => 10
	i32 246610117, ; 8: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 46
	i32 347068432, ; 9: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 5
	i32 375677976, ; 10: System.Net.ServicePoint.dll => 0x16646418 => 37
	i32 385762202, ; 11: System.Memory.dll => 0x16fe439a => 30
	i32 395744057, ; 12: _Microsoft.Android.Resource.Designer => 0x17969339 => 0
	i32 438127167, ; 13: entMerchPlus.dll => 0x1a1d4a3f => 8
	i32 442565967, ; 14: System.Collections => 0x1a61054f => 14
	i32 459347974, ; 15: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 52
	i32 465846621, ; 16: mscorlib => 0x1bc4415d => 63
	i32 469710990, ; 17: System.dll => 0x1bff388e => 62
	i32 498788369, ; 18: System.ObjectModel => 0x1dbae811 => 41
	i32 672442732, ; 19: System.Collections.Concurrent => 0x2814a96c => 11
	i32 683518922, ; 20: System.Net.Security => 0x28bdabca => 36
	i32 690569205, ; 21: System.Xml.Linq.dll => 0x29293ff5 => 59
	i32 748832960, ; 22: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 3
	i32 759454413, ; 23: System.Net.Requests => 0x2d445acd => 35
	i32 775507847, ; 24: System.IO.Compression => 0x2e394f87 => 27
	i32 804715423, ; 25: System.Data.Common => 0x2ff6fb9f => 20
	i32 823281589, ; 26: System.Private.Uri.dll => 0x311247b5 => 42
	i32 830298997, ; 27: System.IO.Compression.Brotli => 0x317d5b75 => 26
	i32 904024072, ; 28: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 955402788, ; 29: Newtonsoft.Json => 0x38f24a24 => 1
	i32 975874589, ; 30: System.Xml.XDocument => 0x3a2aaa1d => 61
	i32 992768348, ; 31: System.Collections.dll => 0x3b2c715c => 14
	i32 1019214401, ; 32: System.Drawing => 0x3cbffa41 => 24
	i32 1036536393, ; 33: System.Drawing.Primitives.dll => 0x3dc84a49 => 23
	i32 1044663988, ; 34: System.Linq.Expressions.dll => 0x3e444eb4 => 28
	i32 1082857460, ; 35: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1098259244, ; 36: System => 0x41761b2c => 62
	i32 1199003181, ; 37: MerchPlus => 0x4777562d => 9
	i32 1292207520, ; 38: SQLitePCLRaw.core.dll => 0x4d0585a0 => 4
	i32 1324164729, ; 39: System.Linq => 0x4eed2679 => 29
	i32 1408764838, ; 40: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 51
	i32 1452070440, ; 41: System.Formats.Asn1.dll => 0x568cd628 => 25
	i32 1458022317, ; 42: System.Net.Security.dll => 0x56e7a7ad => 36
	i32 1462112819, ; 43: System.IO.Compression.dll => 0x57261233 => 27
	i32 1480492111, ; 44: System.IO.Compression.Brotli.dll => 0x583e844f => 26
	i32 1543031311, ; 45: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 56
	i32 1604827217, ; 46: System.Net.WebClient => 0x5fa7b851 => 39
	i32 1639515021, ; 47: System.Net.Http.dll => 0x61b9038d => 31
	i32 1639986890, ; 48: System.Text.RegularExpressions => 0x61c036ca => 56
	i32 1641389582, ; 49: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 50: System.Runtime => 0x62c6282e => 53
	i32 1677501392, ; 51: System.Net.Primitives.dll => 0x63fca3d0 => 34
	i32 1679769178, ; 52: System.Security.Cryptography => 0x641f3e5a => 54
	i32 1711441057, ; 53: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 5
	i32 1763938596, ; 54: System.Diagnostics.TraceSource.dll => 0x69239124 => 22
	i32 1780572499, ; 55: Mono.Android.Runtime.dll => 0x6a216153 => 67
	i32 1824175904, ; 56: System.Text.Encoding.Extensions => 0x6cbab720 => 55
	i32 1824722060, ; 57: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 51
	i32 1858542181, ; 58: System.Linq.Expressions => 0x6ec71a65 => 28
	i32 1870277092, ; 59: System.Reflection.Primitives => 0x6f7a29e4 => 47
	i32 1910275211, ; 60: System.Collections.NonGeneric.dll => 0x71dc7c8b => 12
	i32 1939592360, ; 61: System.Private.Xml.Linq => 0x739bd4a8 => 43
	i32 2045470958, ; 62: System.Private.Xml => 0x79eb68ee => 44
	i32 2070888862, ; 63: System.Diagnostics.TraceSource => 0x7b6f419e => 22
	i32 2079903147, ; 64: System.Runtime.dll => 0x7bf8cdab => 53
	i32 2103459038, ; 65: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 6
	i32 2127167465, ; 66: System.Console => 0x7ec9ffe9 => 19
	i32 2142473426, ; 67: System.Collections.Specialized => 0x7fb38cd2 => 13
	i32 2193016926, ; 68: System.ObjectModel.dll => 0x82b6c85e => 41
	i32 2201231467, ; 69: System.Net.Http => 0x8334206b => 31
	i32 2295906218, ; 70: System.Net.Sockets => 0x88d8bfaa => 38
	i32 2305521784, ; 71: System.Private.CoreLib.dll => 0x896b7878 => 65
	i32 2340441535, ; 72: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 48
	i32 2353062107, ; 73: System.Net.Primitives => 0x8c40e0db => 34
	i32 2368005991, ; 74: System.Xml.ReaderWriter.dll => 0x8d24e767 => 60
	i32 2435356389, ; 75: System.Console.dll => 0x912896e5 => 19
	i32 2458678730, ; 76: System.Net.Sockets.dll => 0x928c75ca => 38
	i32 2465273461, ; 77: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 3
	i32 2471841756, ; 78: netstandard.dll => 0x93554fdc => 64
	i32 2475788418, ; 79: Java.Interop.dll => 0x93918882 => 66
	i32 2483903535, ; 80: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 81: System.Net.ServicePoint => 0x94147f61 => 37
	i32 2491576599, ; 82: MerchPlus.dll => 0x94827117 => 9
	i32 2538310050, ; 83: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 46
	i32 2562349572, ; 84: Microsoft.CSharp => 0x98ba5a04 => 10
	i32 2585220780, ; 85: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 55
	i32 2617129537, ; 86: System.Private.Xml.dll => 0x9bfe3a41 => 44
	i32 2664396074, ; 87: System.Xml.XDocument.dll => 0x9ecf752a => 61
	i32 2665622720, ; 88: System.Drawing.Primitives => 0x9ee22cc0 => 23
	i32 2676780864, ; 89: System.Data.Common.dll => 0x9f8c6f40 => 20
	i32 2724373263, ; 90: System.Runtime.Numerics.dll => 0xa262a30f => 50
	i32 2735172069, ; 91: System.Threading.Channels => 0xa30769e5 => 57
	i32 2905242038, ; 92: mscorlib.dll => 0xad2a79b6 => 63
	i32 2909740682, ; 93: System.Private.CoreLib => 0xad6f1e8a => 65
	i32 2959614098, ; 94: System.ComponentModel.dll => 0xb0682092 => 18
	i32 3038032645, ; 95: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 0
	i32 3059408633, ; 96: Mono.Android.Runtime => 0xb65adef9 => 67
	i32 3059793426, ; 97: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3103600923, ; 98: System.Formats.Asn1 => 0xb8fd311b => 25
	i32 3159123045, ; 99: System.Reflection.Primitives.dll => 0xbc4c6465 => 47
	i32 3220365878, ; 100: System.Threading => 0xbff2e236 => 58
	i32 3286872994, ; 101: SQLite-net.dll => 0xc3e9b3a2 => 2
	i32 3316684772, ; 102: System.Net.Requests.dll => 0xc5b097e4 => 35
	i32 3360279109, ; 103: SQLitePCLRaw.core => 0xc849ca45 => 4
	i32 3362522851, ; 104: Xamarin.AndroidX.Core => 0xc86c06e3 => 7
	i32 3366347497, ; 105: Java.Interop => 0xc8a662e9 => 66
	i32 3430777524, ; 106: netstandard => 0xcc7d82b4 => 64
	i32 3471940407, ; 107: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 108: Mono.Android => 0xcf3163e6 => 68
	i32 3509114376, ; 109: System.Xml.Linq => 0xd128d608 => 59
	i32 3608519521, ; 110: System.Linq.dll => 0xd715a361 => 29
	i32 3624195450, ; 111: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 48
	i32 3660523487, ; 112: System.Net.NetworkInformation => 0xda2f27df => 33
	i32 3672681054, ; 113: Mono.Android.dll => 0xdae8aa5e => 68
	i32 3732100267, ; 114: System.Net.NameResolution => 0xde7354ab => 32
	i32 3748608112, ; 115: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 21
	i32 3754567612, ; 116: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 6
	i32 3792276235, ; 117: System.Collections.NonGeneric => 0xe2098b0b => 12
	i32 3802395368, ; 118: System.Collections.Specialized.dll => 0xe2a3f2e8 => 13
	i32 3823082795, ; 119: System.Security.Cryptography.dll => 0xe3df9d2b => 54
	i32 3849253459, ; 120: System.Runtime.InteropServices.dll => 0xe56ef253 => 49
	i32 3876362041, ; 121: SQLite-net => 0xe70c9739 => 2
	i32 3885497537, ; 122: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 40
	i32 3896106733, ; 123: System.Collections.Concurrent.dll => 0xe839deed => 11
	i32 3896760992, ; 124: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 7
	i32 3928044579, ; 125: System.Xml.ReaderWriter => 0xea213423 => 60
	i32 4025784931, ; 126: System.Memory => 0xeff49a63 => 30
	i32 4054681211, ; 127: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 45
	i32 4068434129, ; 128: System.Private.Xml.Linq.dll => 0xf27f60d1 => 43
	i32 4073602200, ; 129: System.Threading.dll => 0xf2ce3c98 => 58
	i32 4099507663, ; 130: System.Drawing.dll => 0xf45985cf => 24
	i32 4100113165, ; 131: System.Private.Uri => 0xf462c30d => 42
	i32 4147896353, ; 132: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 45
	i32 4157155888, ; 133: entMerchPlus => 0xf7c92a30 => 8
	i32 4181436372, ; 134: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 52
	i32 4196529839, ; 135: System.Net.WebClient.dll => 0xfa21f6af => 39
	i32 4213026141, ; 136: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 21
	i32 4274976490 ; 137: System.Runtime.Numerics => 0xfecef6ea => 50
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [138 x i32] [
	i32 33, ; 0
	i32 32, ; 1
	i32 1, ; 2
	i32 49, ; 3
	i32 57, ; 4
	i32 40, ; 5
	i32 18, ; 6
	i32 10, ; 7
	i32 46, ; 8
	i32 5, ; 9
	i32 37, ; 10
	i32 30, ; 11
	i32 0, ; 12
	i32 8, ; 13
	i32 14, ; 14
	i32 52, ; 15
	i32 63, ; 16
	i32 62, ; 17
	i32 41, ; 18
	i32 11, ; 19
	i32 36, ; 20
	i32 59, ; 21
	i32 3, ; 22
	i32 35, ; 23
	i32 27, ; 24
	i32 20, ; 25
	i32 42, ; 26
	i32 26, ; 27
	i32 16, ; 28
	i32 1, ; 29
	i32 61, ; 30
	i32 14, ; 31
	i32 24, ; 32
	i32 23, ; 33
	i32 28, ; 34
	i32 17, ; 35
	i32 62, ; 36
	i32 9, ; 37
	i32 4, ; 38
	i32 29, ; 39
	i32 51, ; 40
	i32 25, ; 41
	i32 36, ; 42
	i32 27, ; 43
	i32 26, ; 44
	i32 56, ; 45
	i32 39, ; 46
	i32 31, ; 47
	i32 56, ; 48
	i32 15, ; 49
	i32 53, ; 50
	i32 34, ; 51
	i32 54, ; 52
	i32 5, ; 53
	i32 22, ; 54
	i32 67, ; 55
	i32 55, ; 56
	i32 51, ; 57
	i32 28, ; 58
	i32 47, ; 59
	i32 12, ; 60
	i32 43, ; 61
	i32 44, ; 62
	i32 22, ; 63
	i32 53, ; 64
	i32 6, ; 65
	i32 19, ; 66
	i32 13, ; 67
	i32 41, ; 68
	i32 31, ; 69
	i32 38, ; 70
	i32 65, ; 71
	i32 48, ; 72
	i32 34, ; 73
	i32 60, ; 74
	i32 19, ; 75
	i32 38, ; 76
	i32 3, ; 77
	i32 64, ; 78
	i32 66, ; 79
	i32 15, ; 80
	i32 37, ; 81
	i32 9, ; 82
	i32 46, ; 83
	i32 10, ; 84
	i32 55, ; 85
	i32 44, ; 86
	i32 61, ; 87
	i32 23, ; 88
	i32 20, ; 89
	i32 50, ; 90
	i32 57, ; 91
	i32 63, ; 92
	i32 65, ; 93
	i32 18, ; 94
	i32 0, ; 95
	i32 67, ; 96
	i32 16, ; 97
	i32 25, ; 98
	i32 47, ; 99
	i32 58, ; 100
	i32 2, ; 101
	i32 35, ; 102
	i32 4, ; 103
	i32 7, ; 104
	i32 66, ; 105
	i32 64, ; 106
	i32 17, ; 107
	i32 68, ; 108
	i32 59, ; 109
	i32 29, ; 110
	i32 48, ; 111
	i32 33, ; 112
	i32 68, ; 113
	i32 32, ; 114
	i32 21, ; 115
	i32 6, ; 116
	i32 12, ; 117
	i32 13, ; 118
	i32 54, ; 119
	i32 49, ; 120
	i32 2, ; 121
	i32 40, ; 122
	i32 11, ; 123
	i32 7, ; 124
	i32 60, ; 125
	i32 30, ; 126
	i32 45, ; 127
	i32 43, ; 128
	i32 58, ; 129
	i32 24, ; 130
	i32 42, ; 131
	i32 45, ; 132
	i32 8, ; 133
	i32 52, ; 134
	i32 39, ; 135
	i32 21, ; 136
	i32 50 ; 137
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
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nofree norecurse nosync nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { "no-trapping-math"="true" noreturn nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

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
