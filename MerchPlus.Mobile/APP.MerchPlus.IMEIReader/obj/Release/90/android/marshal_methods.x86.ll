; ModuleID = 'obj\Release\90\android\marshal_methods.x86.ll'
source_filename = "obj\Release\90\android\marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android"


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
@assembly_image_cache = local_unnamed_addr global [0 x %struct.MonoImage*] zeroinitializer, align 4
; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = local_unnamed_addr constant [42 x i32] [
	i32 39109920, ; 0: Newtonsoft.Json.dll => 0x254c520 => 15
	i32 321597661, ; 1: System.Numerics => 0x132b30dd => 6
	i32 347068432, ; 2: SQLitePCLRaw.lib.e_sqlite3.android.dll => 0x14afd810 => 19
	i32 438127167, ; 3: entMerchPlus.dll => 0x1a1d4a3f => 14
	i32 465846621, ; 4: mscorlib => 0x1bc4415d => 2
	i32 469710990, ; 5: System.dll => 0x1bff388e => 5
	i32 690569205, ; 6: System.Xml.Linq.dll => 0x29293ff5 => 9
	i32 748832960, ; 7: SQLitePCLRaw.batteries_v2 => 0x2ca248c0 => 17
	i32 955402788, ; 8: Newtonsoft.Json => 0x38f24a24 => 15
	i32 1098259244, ; 9: System => 0x41761b2c => 5
	i32 1292207520, ; 10: SQLitePCLRaw.core.dll => 0x4d0585a0 => 18
	i32 1392814044, ; 11: APP.MerchPlus.IMEIReader => 0x5304a7dc => 13
	i32 1411638395, ; 12: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 7
	i32 1592978981, ; 13: System.Runtime.Serialization.dll => 0x5ef2ee25 => 11
	i32 1711441057, ; 14: SQLitePCLRaw.lib.e_sqlite3.android => 0x660284a1 => 19
	i32 1776026572, ; 15: System.Core.dll => 0x69dc03cc => 4
	i32 2011961780, ; 16: System.Buffers.dll => 0x77ec19b4 => 3
	i32 2103459038, ; 17: SQLitePCLRaw.provider.e_sqlite3.dll => 0x7d603cde => 20
	i32 2465273461, ; 18: SQLitePCLRaw.batteries_v2.dll => 0x92f11675 => 17
	i32 2475788418, ; 19: Java.Interop.dll => 0x93918882 => 0
	i32 2819470561, ; 20: System.Xml.dll => 0xa80db4e1 => 8
	i32 2905242038, ; 21: mscorlib.dll => 0xad2a79b6 => 2
	i32 3111772706, ; 22: System.Runtime.Serialization => 0xb979e222 => 11
	i32 3204380047, ; 23: System.Data.dll => 0xbefef58f => 10
	i32 3247949154, ; 24: Mono.Security => 0xc197c562 => 12
	i32 3286872994, ; 25: SQLite-net.dll => 0xc3e9b3a2 => 16
	i32 3317144872, ; 26: System.Data => 0xc5b79d28 => 10
	i32 3360279109, ; 27: SQLitePCLRaw.core => 0xc849ca45 => 18
	i32 3366347497, ; 28: Java.Interop => 0xc8a662e9 => 0
	i32 3395150330, ; 29: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 7
	i32 3429136800, ; 30: System.Xml => 0xcc6479a0 => 8
	i32 3476120550, ; 31: Mono.Android => 0xcf3163e6 => 1
	i32 3509114376, ; 32: System.Xml.Linq => 0xd128d608 => 9
	i32 3672681054, ; 33: Mono.Android.dll => 0xdae8aa5e => 1
	i32 3754567612, ; 34: SQLitePCLRaw.provider.e_sqlite3 => 0xdfca27bc => 20
	i32 3829621856, ; 35: System.Numerics.dll => 0xe4436460 => 6
	i32 3876362041, ; 36: SQLite-net => 0xe70c9739 => 16
	i32 4105002889, ; 37: Mono.Security.dll => 0xf4ad5f89 => 12
	i32 4110460865, ; 38: APP.MerchPlus.IMEIReader.dll => 0xf500a7c1 => 13
	i32 4151237749, ; 39: System.Core => 0xf76edc75 => 4
	i32 4157155888, ; 40: entMerchPlus => 0xf7c92a30 => 14
	i32 4260525087 ; 41: System.Buffers => 0xfdf2741f => 3
], align 4
@assembly_image_cache_indices = local_unnamed_addr constant [42 x i32] [
	i32 15, i32 6, i32 19, i32 14, i32 2, i32 5, i32 9, i32 17, ; 0..7
	i32 15, i32 5, i32 18, i32 13, i32 7, i32 11, i32 19, i32 4, ; 8..15
	i32 3, i32 20, i32 17, i32 0, i32 8, i32 2, i32 11, i32 10, ; 16..23
	i32 12, i32 16, i32 10, i32 18, i32 0, i32 7, i32 8, i32 1, ; 24..31
	i32 9, i32 1, i32 20, i32 6, i32 16, i32 12, i32 13, i32 4, ; 32..39
	i32 14, i32 3 ; 40..41
], align 4

@marshal_methods_number_of_classes = local_unnamed_addr constant i32 0, align 4

; marshal_methods_class_cache
@marshal_methods_class_cache = global [0 x %struct.MarshalMethodsManagedClass] [
], align 4; end of 'marshal_methods_class_cache' array


@get_function_pointer = internal unnamed_addr global void (i32, i32, i32, i8**)* null, align 4

; Function attributes: "frame-pointer"="none" "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn writeonly
define void @xamarin_app_init (void (i32, i32, i32, i8**)* %fn) local_unnamed_addr #0
{
	store void (i32, i32, i32, i8**)* %fn, void (i32, i32, i32, i8**)** @get_function_pointer, align 4
	ret void
}

; Names of classes in which marshal methods reside
@mm_class_names = local_unnamed_addr constant [0 x i8*] zeroinitializer, align 4
@__MarshalMethodName_name.0 = internal constant [1 x i8] c"\00", align 1

; mm_method_names
@mm_method_names = local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	; 0
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		i8* getelementptr inbounds ([1 x i8], [1 x i8]* @__MarshalMethodName_name.0, i32 0, i32 0); name
	}
], align 8; end of 'mm_method_names' array


attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable willreturn writeonly "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #1 = { "min-legal-vector-width"="0" mustprogress "no-trapping-math"="true" nounwind sspstrong "stack-protector-buffer-size"="8" uwtable "frame-pointer"="none" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" "stackrealign" }
attributes #2 = { nounwind }

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"NumRegisterParameters", i32 0}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
!llvm.linker.options = !{}
