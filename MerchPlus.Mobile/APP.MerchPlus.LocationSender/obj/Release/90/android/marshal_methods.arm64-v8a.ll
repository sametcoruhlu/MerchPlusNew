; ModuleID = 'obj\Release\90\android\marshal_methods.arm64-v8a.ll'
source_filename = "obj\Release\90\android\marshal_methods.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.MonoClass = type opaque

%struct.MarshalMethodsManagedClass = type {
	i32,; uint32_t token
	%struct.MonoClass*; MonoClass* klass
}

%struct.MarshalMethodName = type {
	i64,; uint64_t id
	i8*; char* name
}

%class._JNIEnv = type opaque

%class._jobject = type {
	i8; uint8_t b
}

%class._jclass = type {
	i8; uint8_t b
}

%class._jstring = type {
	i8; uint8_t b
}

%class._jthrowable = type {
	i8; uint8_t b
}

%class._jarray = type {
	i8; uint8_t b
}

%class._jobjectArray = type {
	i8; uint8_t b
}

%class._jbooleanArray = type {
	i8; uint8_t b
}

%class._jbyteArray = type {
	i8; uint8_t b
}

%class._jcharArray = type {
	i8; uint8_t b
}

%class._jshortArray = type {
	i8; uint8_t b
}

%class._jintArray = type {
	i8; uint8_t b
}

%class._jlongArray = type {
	i8; uint8_t b
}

%class._jfloatArray = type {
	i8; uint8_t b
}

%class._jdoubleArray = type {
	i8; uint8_t b
}

; assembly_image_cache
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 8
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [42 x i64] [
	i64 120698629574877762, ; 0: Mono.Android => 0x1accec39cafe242 => 1
	i64 870603111519317375, ; 1: SQLitePCLRaw.lib.e_sqlite3.android => 0xc1500ead2756d7f => 18
	i64 1000557547492888992, ; 2: Mono.Security.dll => 0xde2b1c9cba651a0 => 12
	i64 1301485588176585670, ; 3: SQLitePCLRaw.core => 0x120fce3f338e43c6 => 17
	i64 1425944114962822056, ; 4: System.Runtime.Serialization.dll => 0x13c9f89e19eaf3a8 => 11
	i64 1518315023656898250, ; 5: SQLitePCLRaw.provider.e_sqlite3 => 0x151223783a354eca => 19
	i64 1731380447121279447, ; 6: Newtonsoft.Json => 0x18071957e9b889d7 => 14
	i64 2133195048986300728, ; 7: Newtonsoft.Json.dll => 0x1d9aa1984b735138 => 14
	i64 2337758774805907496, ; 8: System.Runtime.CompilerServices.Unsafe => 0x207163383edbc828 => 7
	i64 2592350477072141967, ; 9: System.Xml.dll => 0x23f9e10627330e8f => 8
	i64 2624866290265602282, ; 10: mscorlib.dll => 0x246d65fbde2db8ea => 2
	i64 2783046991838674048, ; 11: System.Runtime.CompilerServices.Unsafe.dll => 0x269f5e7e6dc37c80 => 7
	i64 3531994851595924923, ; 12: System.Numerics => 0x31042a9aade235bb => 6
	i64 3949716950318689872, ; 13: APP.MerchPlus.LocationSender.dll => 0x36d036a3cb15ae50 => 13
	i64 4247996603072512073, ; 14: Xamarin.GooglePlayServices.Tasks => 0x3af3ea6755340049 => 20
	i64 4337444564132831293, ; 15: SQLitePCLRaw.batteries_v2.dll => 0x3c31b2d9ae16203d => 16
	i64 5219803938844960741, ; 16: APP.MerchPlus.LocationSender => 0x48707806d409e7e5 => 13
	i64 5507995362134886206, ; 17: System.Core.dll => 0x4c705499688c873e => 4
	i64 6183170893902868313, ; 18: SQLitePCLRaw.batteries_v2 => 0x55cf092b0c9d6f59 => 16
	i64 6876862101832370452, ; 19: System.Xml.Linq => 0x5f6f85a57d108914 => 9
	i64 7488575175965059935, ; 20: System.Xml.Linq.dll => 0x67ecc3724534ab5f => 9
	i64 7820441508502274321, ; 21: System.Data => 0x6c87ca1e14ff8111 => 10
	i64 8167236081217502503, ; 22: Java.Interop.dll => 0x7157d9f1a9b8fd27 => 0
	i64 8626175481042262068, ; 23: Java.Interop => 0x77b654e585b55834 => 0
	i64 9662334977499516867, ; 24: System.Numerics.dll => 0x8617827802b0cfc3 => 6
	i64 9808709177481450983, ; 25: Mono.Android.dll => 0x881f890734e555e7 => 1
	i64 9998632235833408227, ; 26: Mono.Security => 0x8ac2470b209ebae3 => 12
	i64 11023048688141570732, ; 27: System.Core => 0x98f9bc61168392ac => 4
	i64 11037814507248023548, ; 28: System.Xml => 0x992e31d0412bf7fc => 8
	i64 11739066727115742305, ; 29: SQLite-net.dll => 0xa2e98afdf8575c61 => 15
	i64 11806260347154423189, ; 30: SQLite-net => 0xa3d8433bc5eb5d95 => 15
	i64 12102847907131387746, ; 31: System.Buffers => 0xa7f5f40c43256f62 => 3
	i64 12279246230491828964, ; 32: SQLitePCLRaw.provider.e_sqlite3.dll => 0xaa68a5636e0512e4 => 19
	i64 13370592475155966277, ; 33: System.Runtime.Serialization => 0xb98de304062ea945 => 11
	i64 13647894001087880694, ; 34: System.Data.dll => 0xbd670f48cb071df6 => 10
	i64 15609085926864131306, ; 35: System.dll => 0xd89e9cf3334914ea => 5
	i64 15930129725311349754, ; 36: Xamarin.GooglePlayServices.Tasks.dll => 0xdd1330956f12f3fa => 20
	i64 16154507427712707110, ; 37: System => 0xe03056ea4e39aa26 => 5
	i64 16755018182064898362, ; 38: SQLitePCLRaw.core.dll => 0xe885c843c330813a => 17
	i64 16833383113903931215, ; 39: mscorlib => 0xe99c30c1484d7f4f => 2
	i64 17838668724098252521, ; 40: System.Buffers.dll => 0xf78faeb0f5bf3ee9 => 3
	i64 18370042311372477656 ; 41: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0xfeef80274e4094d8 => 18
], align 8
@assembly_image_cache_indices = local_unnamed_addr constant [42 x i32] [
	i32 1, i32 18, i32 12, i32 17, i32 11, i32 19, i32 14, i32 14, ; 0..7
	i32 7, i32 8, i32 2, i32 7, i32 6, i32 13, i32 20, i32 16, ; 8..15
	i32 13, i32 4, i32 16, i32 9, i32 9, i32 10, i32 0, i32 0, ; 16..23
	i32 6, i32 1, i32 12, i32 4, i32 8, i32 15, i32 15, i32 3, ; 24..31
	i32 19, i32 11, i32 10, i32 5, i32 20, i32 5, i32 17, i32 2, ; 32..39
	i32 3, i32 18 ; 40..41
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 8; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 8

; Function attributes: "frame-pointer"="non-leaf" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 8
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 8
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="non-leaf" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="non-leaf" "target-cpu"="generic" "target-features"="+neon,+outline-atomics" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2, !3, !4, !5}
!llvm.ident = !{!6}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"branch-target-enforcement", i32 0}
!3 = !{i32 1, !"sign-return-address", i32 0}
!4 = !{i32 1, !"sign-return-address-all", i32 0}
!5 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
!6 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
