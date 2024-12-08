; ModuleID = 'compressed_assemblies.x86_64.ll'
source_filename = "compressed_assemblies.x86_64.ll"
target datalayout = "e-m:e-p270:32:32-p271:32:32-p272:64:64-i64:64-f80:128-n8:16:32:64-S128"
target triple = "x86_64-unknown-linux-android21"

%struct.CompressedAssemblies = type {
	i32, ; uint32_t count
	ptr ; CompressedAssemblyDescriptor descriptors
}

%struct.CompressedAssemblyDescriptor = type {
	i32, ; uint32_t uncompressed_file_size
	i8, ; bool loaded
	ptr ; uint8_t data
}

@compressed_assemblies = dso_local local_unnamed_addr global %struct.CompressedAssemblies {
	i32 87, ; uint32_t count (0x57)
	ptr @compressed_assembly_descriptors; CompressedAssemblyDescriptor* descriptors
}, align 8

@compressed_assembly_descriptors = internal dso_local global [87 x %struct.CompressedAssemblyDescriptor] [
	%struct.CompressedAssemblyDescriptor {
		i32 146944, ; uint32_t uncompressed_file_size (0x23e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_0; uint8_t* data (0x0)
	}, ; 0
	%struct.CompressedAssemblyDescriptor {
		i32 100352, ; uint32_t uncompressed_file_size (0x18800)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_1; uint8_t* data (0x0)
	}, ; 1
	%struct.CompressedAssemblyDescriptor {
		i32 224256, ; uint32_t uncompressed_file_size (0x36c00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_2; uint8_t* data (0x0)
	}, ; 2
	%struct.CompressedAssemblyDescriptor {
		i32 18880, ; uint32_t uncompressed_file_size (0x49c0)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_3; uint8_t* data (0x0)
	}, ; 3
	%struct.CompressedAssemblyDescriptor {
		i32 648704, ; uint32_t uncompressed_file_size (0x9e600)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_4; uint8_t* data (0x0)
	}, ; 4
	%struct.CompressedAssemblyDescriptor {
		i32 712464, ; uint32_t uncompressed_file_size (0xadf10)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_5; uint8_t* data (0x0)
	}, ; 5
	%struct.CompressedAssemblyDescriptor {
		i32 107520, ; uint32_t uncompressed_file_size (0x1a400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_6; uint8_t* data (0x0)
	}, ; 6
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uint32_t uncompressed_file_size (0x1600)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_7; uint8_t* data (0x0)
	}, ; 7
	%struct.CompressedAssemblyDescriptor {
		i32 51200, ; uint32_t uncompressed_file_size (0xc800)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_8; uint8_t* data (0x0)
	}, ; 8
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size (0x1400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_9; uint8_t* data (0x0)
	}, ; 9
	%struct.CompressedAssemblyDescriptor {
		i32 36864, ; uint32_t uncompressed_file_size (0x9000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_10; uint8_t* data (0x0)
	}, ; 10
	%struct.CompressedAssemblyDescriptor {
		i32 17408, ; uint32_t uncompressed_file_size (0x4400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_11; uint8_t* data (0x0)
	}, ; 11
	%struct.CompressedAssemblyDescriptor {
		i32 15872, ; uint32_t uncompressed_file_size (0x3e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_12; uint8_t* data (0x0)
	}, ; 12
	%struct.CompressedAssemblyDescriptor {
		i32 6656, ; uint32_t uncompressed_file_size (0x1a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_13; uint8_t* data (0x0)
	}, ; 13
	%struct.CompressedAssemblyDescriptor {
		i32 12288, ; uint32_t uncompressed_file_size (0x3000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_14; uint8_t* data (0x0)
	}, ; 14
	%struct.CompressedAssemblyDescriptor {
		i32 125952, ; uint32_t uncompressed_file_size (0x1ec00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_15; uint8_t* data (0x0)
	}, ; 15
	%struct.CompressedAssemblyDescriptor {
		i32 4608, ; uint32_t uncompressed_file_size (0x1200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_16; uint8_t* data (0x0)
	}, ; 16
	%struct.CompressedAssemblyDescriptor {
		i32 10752, ; uint32_t uncompressed_file_size (0x2a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_17; uint8_t* data (0x0)
	}, ; 17
	%struct.CompressedAssemblyDescriptor {
		i32 517120, ; uint32_t uncompressed_file_size (0x7e400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_18; uint8_t* data (0x0)
	}, ; 18
	%struct.CompressedAssemblyDescriptor {
		i32 15360, ; uint32_t uncompressed_file_size (0x3c00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_19; uint8_t* data (0x0)
	}, ; 19
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uint32_t uncompressed_file_size (0x4600)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_20; uint8_t* data (0x0)
	}, ; 20
	%struct.CompressedAssemblyDescriptor {
		i32 34816, ; uint32_t uncompressed_file_size (0x8800)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_21; uint8_t* data (0x0)
	}, ; 21
	%struct.CompressedAssemblyDescriptor {
		i32 4096, ; uint32_t uncompressed_file_size (0x1000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_22; uint8_t* data (0x0)
	}, ; 22
	%struct.CompressedAssemblyDescriptor {
		i32 60416, ; uint32_t uncompressed_file_size (0xec00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_23; uint8_t* data (0x0)
	}, ; 23
	%struct.CompressedAssemblyDescriptor {
		i32 19968, ; uint32_t uncompressed_file_size (0x4e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_24; uint8_t* data (0x0)
	}, ; 24
	%struct.CompressedAssemblyDescriptor {
		i32 31232, ; uint32_t uncompressed_file_size (0x7a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_25; uint8_t* data (0x0)
	}, ; 25
	%struct.CompressedAssemblyDescriptor {
		i32 418816, ; uint32_t uncompressed_file_size (0x66400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_26; uint8_t* data (0x0)
	}, ; 26
	%struct.CompressedAssemblyDescriptor {
		i32 36864, ; uint32_t uncompressed_file_size (0x9000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_27; uint8_t* data (0x0)
	}, ; 27
	%struct.CompressedAssemblyDescriptor {
		i32 6144, ; uint32_t uncompressed_file_size (0x1800)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_28; uint8_t* data (0x0)
	}, ; 28
	%struct.CompressedAssemblyDescriptor {
		i32 362496, ; uint32_t uncompressed_file_size (0x58800)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_29; uint8_t* data (0x0)
	}, ; 29
	%struct.CompressedAssemblyDescriptor {
		i32 18944, ; uint32_t uncompressed_file_size (0x4a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_30; uint8_t* data (0x0)
	}, ; 30
	%struct.CompressedAssemblyDescriptor {
		i32 23552, ; uint32_t uncompressed_file_size (0x5c00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_31; uint8_t* data (0x0)
	}, ; 31
	%struct.CompressedAssemblyDescriptor {
		i32 66560, ; uint32_t uncompressed_file_size (0x10400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_32; uint8_t* data (0x0)
	}, ; 32
	%struct.CompressedAssemblyDescriptor {
		i32 81408, ; uint32_t uncompressed_file_size (0x13e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_33; uint8_t* data (0x0)
	}, ; 33
	%struct.CompressedAssemblyDescriptor {
		i32 136704, ; uint32_t uncompressed_file_size (0x21600)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_34; uint8_t* data (0x0)
	}, ; 34
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uint32_t uncompressed_file_size (0x1600)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_35; uint8_t* data (0x0)
	}, ; 35
	%struct.CompressedAssemblyDescriptor {
		i32 24064, ; uint32_t uncompressed_file_size (0x5e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_36; uint8_t* data (0x0)
	}, ; 36
	%struct.CompressedAssemblyDescriptor {
		i32 9216, ; uint32_t uncompressed_file_size (0x2400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_37; uint8_t* data (0x0)
	}, ; 37
	%struct.CompressedAssemblyDescriptor {
		i32 12800, ; uint32_t uncompressed_file_size (0x3200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_38; uint8_t* data (0x0)
	}, ; 38
	%struct.CompressedAssemblyDescriptor {
		i32 70144, ; uint32_t uncompressed_file_size (0x11200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_39; uint8_t* data (0x0)
	}, ; 39
	%struct.CompressedAssemblyDescriptor {
		i32 41472, ; uint32_t uncompressed_file_size (0xa200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_40; uint8_t* data (0x0)
	}, ; 40
	%struct.CompressedAssemblyDescriptor {
		i32 1325568, ; uint32_t uncompressed_file_size (0x143a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_41; uint8_t* data (0x0)
	}, ; 41
	%struct.CompressedAssemblyDescriptor {
		i32 4608, ; uint32_t uncompressed_file_size (0x1200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_42; uint8_t* data (0x0)
	}, ; 42
	%struct.CompressedAssemblyDescriptor {
		i32 4608, ; uint32_t uncompressed_file_size (0x1200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_43; uint8_t* data (0x0)
	}, ; 43
	%struct.CompressedAssemblyDescriptor {
		i32 4608, ; uint32_t uncompressed_file_size (0x1200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_44; uint8_t* data (0x0)
	}, ; 44
	%struct.CompressedAssemblyDescriptor {
		i32 4096, ; uint32_t uncompressed_file_size (0x1000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_45; uint8_t* data (0x0)
	}, ; 45
	%struct.CompressedAssemblyDescriptor {
		i32 8192, ; uint32_t uncompressed_file_size (0x2000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_46; uint8_t* data (0x0)
	}, ; 46
	%struct.CompressedAssemblyDescriptor {
		i32 6656, ; uint32_t uncompressed_file_size (0x1a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_47; uint8_t* data (0x0)
	}, ; 47
	%struct.CompressedAssemblyDescriptor {
		i32 5120, ; uint32_t uncompressed_file_size (0x1400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_48; uint8_t* data (0x0)
	}, ; 48
	%struct.CompressedAssemblyDescriptor {
		i32 11264, ; uint32_t uncompressed_file_size (0x2c00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_49; uint8_t* data (0x0)
	}, ; 49
	%struct.CompressedAssemblyDescriptor {
		i32 161792, ; uint32_t uncompressed_file_size (0x27800)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_50; uint8_t* data (0x0)
	}, ; 50
	%struct.CompressedAssemblyDescriptor {
		i32 4096, ; uint32_t uncompressed_file_size (0x1000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_51; uint8_t* data (0x0)
	}, ; 51
	%struct.CompressedAssemblyDescriptor {
		i32 19968, ; uint32_t uncompressed_file_size (0x4e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_52; uint8_t* data (0x0)
	}, ; 52
	%struct.CompressedAssemblyDescriptor {
		i32 10752, ; uint32_t uncompressed_file_size (0x2a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_53; uint8_t* data (0x0)
	}, ; 53
	%struct.CompressedAssemblyDescriptor {
		i32 4096, ; uint32_t uncompressed_file_size (0x1000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_54; uint8_t* data (0x0)
	}, ; 54
	%struct.CompressedAssemblyDescriptor {
		i32 4608, ; uint32_t uncompressed_file_size (0x1200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_55; uint8_t* data (0x0)
	}, ; 55
	%struct.CompressedAssemblyDescriptor {
		i32 4608, ; uint32_t uncompressed_file_size (0x1200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_56; uint8_t* data (0x0)
	}, ; 56
	%struct.CompressedAssemblyDescriptor {
		i32 4096, ; uint32_t uncompressed_file_size (0x1000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_57; uint8_t* data (0x0)
	}, ; 57
	%struct.CompressedAssemblyDescriptor {
		i32 54272, ; uint32_t uncompressed_file_size (0xd400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_58; uint8_t* data (0x0)
	}, ; 58
	%struct.CompressedAssemblyDescriptor {
		i32 2560, ; uint32_t uncompressed_file_size (0xa00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_59; uint8_t* data (0x0)
	}, ; 59
	%struct.CompressedAssemblyDescriptor {
		i32 21504, ; uint32_t uncompressed_file_size (0x5400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_60; uint8_t* data (0x0)
	}, ; 60
	%struct.CompressedAssemblyDescriptor {
		i32 17408, ; uint32_t uncompressed_file_size (0x4400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_61; uint8_t* data (0x0)
	}, ; 61
	%struct.CompressedAssemblyDescriptor {
		i32 100864, ; uint32_t uncompressed_file_size (0x18a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_62; uint8_t* data (0x0)
	}, ; 62
	%struct.CompressedAssemblyDescriptor {
		i32 2006528, ; uint32_t uncompressed_file_size (0x1e9e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_63; uint8_t* data (0x0)
	}, ; 63
	%struct.CompressedAssemblyDescriptor {
		i32 86016, ; uint32_t uncompressed_file_size (0x15000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_64; uint8_t* data (0x0)
	}, ; 64
	%struct.CompressedAssemblyDescriptor {
		i32 299008, ; uint32_t uncompressed_file_size (0x49000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_65; uint8_t* data (0x0)
	}, ; 65
	%struct.CompressedAssemblyDescriptor {
		i32 21504, ; uint32_t uncompressed_file_size (0x5400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_66; uint8_t* data (0x0)
	}, ; 66
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uint32_t uncompressed_file_size (0x4200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_67; uint8_t* data (0x0)
	}, ; 67
	%struct.CompressedAssemblyDescriptor {
		i32 100864, ; uint32_t uncompressed_file_size (0x18a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_68; uint8_t* data (0x0)
	}, ; 68
	%struct.CompressedAssemblyDescriptor {
		i32 1992192, ; uint32_t uncompressed_file_size (0x1e6600)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_69; uint8_t* data (0x0)
	}, ; 69
	%struct.CompressedAssemblyDescriptor {
		i32 86016, ; uint32_t uncompressed_file_size (0x15000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_70; uint8_t* data (0x0)
	}, ; 70
	%struct.CompressedAssemblyDescriptor {
		i32 298496, ; uint32_t uncompressed_file_size (0x48e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_71; uint8_t* data (0x0)
	}, ; 71
	%struct.CompressedAssemblyDescriptor {
		i32 34816, ; uint32_t uncompressed_file_size (0x8800)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_72; uint8_t* data (0x0)
	}, ; 72
	%struct.CompressedAssemblyDescriptor {
		i32 4608, ; uint32_t uncompressed_file_size (0x1200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_73; uint8_t* data (0x0)
	}, ; 73
	%struct.CompressedAssemblyDescriptor {
		i32 8704, ; uint32_t uncompressed_file_size (0x2200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_74; uint8_t* data (0x0)
	}, ; 74
	%struct.CompressedAssemblyDescriptor {
		i32 21504, ; uint32_t uncompressed_file_size (0x5400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_75; uint8_t* data (0x0)
	}, ; 75
	%struct.CompressedAssemblyDescriptor {
		i32 16896, ; uint32_t uncompressed_file_size (0x4200)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_76; uint8_t* data (0x0)
	}, ; 76
	%struct.CompressedAssemblyDescriptor {
		i32 100864, ; uint32_t uncompressed_file_size (0x18a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_77; uint8_t* data (0x0)
	}, ; 77
	%struct.CompressedAssemblyDescriptor {
		i32 1992192, ; uint32_t uncompressed_file_size (0x1e6600)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_78; uint8_t* data (0x0)
	}, ; 78
	%struct.CompressedAssemblyDescriptor {
		i32 86016, ; uint32_t uncompressed_file_size (0x15000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_79; uint8_t* data (0x0)
	}, ; 79
	%struct.CompressedAssemblyDescriptor {
		i32 298496, ; uint32_t uncompressed_file_size (0x48e00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_80; uint8_t* data (0x0)
	}, ; 80
	%struct.CompressedAssemblyDescriptor {
		i32 21504, ; uint32_t uncompressed_file_size (0x5400)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_81; uint8_t* data (0x0)
	}, ; 81
	%struct.CompressedAssemblyDescriptor {
		i32 17920, ; uint32_t uncompressed_file_size (0x4600)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_82; uint8_t* data (0x0)
	}, ; 82
	%struct.CompressedAssemblyDescriptor {
		i32 100864, ; uint32_t uncompressed_file_size (0x18a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_83; uint8_t* data (0x0)
	}, ; 83
	%struct.CompressedAssemblyDescriptor {
		i32 2054656, ; uint32_t uncompressed_file_size (0x1f5a00)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_84; uint8_t* data (0x0)
	}, ; 84
	%struct.CompressedAssemblyDescriptor {
		i32 86016, ; uint32_t uncompressed_file_size (0x15000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_85; uint8_t* data (0x0)
	}, ; 85
	%struct.CompressedAssemblyDescriptor {
		i32 299008, ; uint32_t uncompressed_file_size (0x49000)
		i8 0, ; bool loaded
		ptr @__compressedAssemblyData_86; uint8_t* data (0x0)
	} ; 86
], align 16

@__compressedAssemblyData_0 = internal dso_local global [146944 x i8] zeroinitializer, align 16
@__compressedAssemblyData_1 = internal dso_local global [100352 x i8] zeroinitializer, align 16
@__compressedAssemblyData_2 = internal dso_local global [224256 x i8] zeroinitializer, align 16
@__compressedAssemblyData_3 = internal dso_local global [18880 x i8] zeroinitializer, align 16
@__compressedAssemblyData_4 = internal dso_local global [648704 x i8] zeroinitializer, align 16
@__compressedAssemblyData_5 = internal dso_local global [712464 x i8] zeroinitializer, align 16
@__compressedAssemblyData_6 = internal dso_local global [107520 x i8] zeroinitializer, align 16
@__compressedAssemblyData_7 = internal dso_local global [5632 x i8] zeroinitializer, align 16
@__compressedAssemblyData_8 = internal dso_local global [51200 x i8] zeroinitializer, align 16
@__compressedAssemblyData_9 = internal dso_local global [5120 x i8] zeroinitializer, align 16
@__compressedAssemblyData_10 = internal dso_local global [36864 x i8] zeroinitializer, align 16
@__compressedAssemblyData_11 = internal dso_local global [17408 x i8] zeroinitializer, align 16
@__compressedAssemblyData_12 = internal dso_local global [15872 x i8] zeroinitializer, align 16
@__compressedAssemblyData_13 = internal dso_local global [6656 x i8] zeroinitializer, align 16
@__compressedAssemblyData_14 = internal dso_local global [12288 x i8] zeroinitializer, align 16
@__compressedAssemblyData_15 = internal dso_local global [125952 x i8] zeroinitializer, align 16
@__compressedAssemblyData_16 = internal dso_local global [4608 x i8] zeroinitializer, align 16
@__compressedAssemblyData_17 = internal dso_local global [10752 x i8] zeroinitializer, align 16
@__compressedAssemblyData_18 = internal dso_local global [517120 x i8] zeroinitializer, align 16
@__compressedAssemblyData_19 = internal dso_local global [15360 x i8] zeroinitializer, align 16
@__compressedAssemblyData_20 = internal dso_local global [17920 x i8] zeroinitializer, align 16
@__compressedAssemblyData_21 = internal dso_local global [34816 x i8] zeroinitializer, align 16
@__compressedAssemblyData_22 = internal dso_local global [4096 x i8] zeroinitializer, align 16
@__compressedAssemblyData_23 = internal dso_local global [60416 x i8] zeroinitializer, align 16
@__compressedAssemblyData_24 = internal dso_local global [19968 x i8] zeroinitializer, align 16
@__compressedAssemblyData_25 = internal dso_local global [31232 x i8] zeroinitializer, align 16
@__compressedAssemblyData_26 = internal dso_local global [418816 x i8] zeroinitializer, align 16
@__compressedAssemblyData_27 = internal dso_local global [36864 x i8] zeroinitializer, align 16
@__compressedAssemblyData_28 = internal dso_local global [6144 x i8] zeroinitializer, align 16
@__compressedAssemblyData_29 = internal dso_local global [362496 x i8] zeroinitializer, align 16
@__compressedAssemblyData_30 = internal dso_local global [18944 x i8] zeroinitializer, align 16
@__compressedAssemblyData_31 = internal dso_local global [23552 x i8] zeroinitializer, align 16
@__compressedAssemblyData_32 = internal dso_local global [66560 x i8] zeroinitializer, align 16
@__compressedAssemblyData_33 = internal dso_local global [81408 x i8] zeroinitializer, align 16
@__compressedAssemblyData_34 = internal dso_local global [136704 x i8] zeroinitializer, align 16
@__compressedAssemblyData_35 = internal dso_local global [5632 x i8] zeroinitializer, align 16
@__compressedAssemblyData_36 = internal dso_local global [24064 x i8] zeroinitializer, align 16
@__compressedAssemblyData_37 = internal dso_local global [9216 x i8] zeroinitializer, align 16
@__compressedAssemblyData_38 = internal dso_local global [12800 x i8] zeroinitializer, align 16
@__compressedAssemblyData_39 = internal dso_local global [70144 x i8] zeroinitializer, align 16
@__compressedAssemblyData_40 = internal dso_local global [41472 x i8] zeroinitializer, align 16
@__compressedAssemblyData_41 = internal dso_local global [1325568 x i8] zeroinitializer, align 16
@__compressedAssemblyData_42 = internal dso_local global [4608 x i8] zeroinitializer, align 16
@__compressedAssemblyData_43 = internal dso_local global [4608 x i8] zeroinitializer, align 16
@__compressedAssemblyData_44 = internal dso_local global [4608 x i8] zeroinitializer, align 16
@__compressedAssemblyData_45 = internal dso_local global [4096 x i8] zeroinitializer, align 16
@__compressedAssemblyData_46 = internal dso_local global [8192 x i8] zeroinitializer, align 16
@__compressedAssemblyData_47 = internal dso_local global [6656 x i8] zeroinitializer, align 16
@__compressedAssemblyData_48 = internal dso_local global [5120 x i8] zeroinitializer, align 16
@__compressedAssemblyData_49 = internal dso_local global [11264 x i8] zeroinitializer, align 16
@__compressedAssemblyData_50 = internal dso_local global [161792 x i8] zeroinitializer, align 16
@__compressedAssemblyData_51 = internal dso_local global [4096 x i8] zeroinitializer, align 16
@__compressedAssemblyData_52 = internal dso_local global [19968 x i8] zeroinitializer, align 16
@__compressedAssemblyData_53 = internal dso_local global [10752 x i8] zeroinitializer, align 16
@__compressedAssemblyData_54 = internal dso_local global [4096 x i8] zeroinitializer, align 16
@__compressedAssemblyData_55 = internal dso_local global [4608 x i8] zeroinitializer, align 16
@__compressedAssemblyData_56 = internal dso_local global [4608 x i8] zeroinitializer, align 16
@__compressedAssemblyData_57 = internal dso_local global [4096 x i8] zeroinitializer, align 16
@__compressedAssemblyData_58 = internal dso_local global [54272 x i8] zeroinitializer, align 16
@__compressedAssemblyData_59 = internal dso_local global [2560 x i8] zeroinitializer, align 16
@__compressedAssemblyData_60 = internal dso_local global [21504 x i8] zeroinitializer, align 16
@__compressedAssemblyData_61 = internal dso_local global [17408 x i8] zeroinitializer, align 16
@__compressedAssemblyData_62 = internal dso_local global [100864 x i8] zeroinitializer, align 16
@__compressedAssemblyData_63 = internal dso_local global [2006528 x i8] zeroinitializer, align 16
@__compressedAssemblyData_64 = internal dso_local global [86016 x i8] zeroinitializer, align 16
@__compressedAssemblyData_65 = internal dso_local global [299008 x i8] zeroinitializer, align 16
@__compressedAssemblyData_66 = internal dso_local global [21504 x i8] zeroinitializer, align 16
@__compressedAssemblyData_67 = internal dso_local global [16896 x i8] zeroinitializer, align 16
@__compressedAssemblyData_68 = internal dso_local global [100864 x i8] zeroinitializer, align 16
@__compressedAssemblyData_69 = internal dso_local global [1992192 x i8] zeroinitializer, align 16
@__compressedAssemblyData_70 = internal dso_local global [86016 x i8] zeroinitializer, align 16
@__compressedAssemblyData_71 = internal dso_local global [298496 x i8] zeroinitializer, align 16
@__compressedAssemblyData_72 = internal dso_local global [34816 x i8] zeroinitializer, align 16
@__compressedAssemblyData_73 = internal dso_local global [4608 x i8] zeroinitializer, align 16
@__compressedAssemblyData_74 = internal dso_local global [8704 x i8] zeroinitializer, align 16
@__compressedAssemblyData_75 = internal dso_local global [21504 x i8] zeroinitializer, align 16
@__compressedAssemblyData_76 = internal dso_local global [16896 x i8] zeroinitializer, align 16
@__compressedAssemblyData_77 = internal dso_local global [100864 x i8] zeroinitializer, align 16
@__compressedAssemblyData_78 = internal dso_local global [1992192 x i8] zeroinitializer, align 16
@__compressedAssemblyData_79 = internal dso_local global [86016 x i8] zeroinitializer, align 16
@__compressedAssemblyData_80 = internal dso_local global [298496 x i8] zeroinitializer, align 16
@__compressedAssemblyData_81 = internal dso_local global [21504 x i8] zeroinitializer, align 16
@__compressedAssemblyData_82 = internal dso_local global [17920 x i8] zeroinitializer, align 16
@__compressedAssemblyData_83 = internal dso_local global [100864 x i8] zeroinitializer, align 16
@__compressedAssemblyData_84 = internal dso_local global [2054656 x i8] zeroinitializer, align 16
@__compressedAssemblyData_85 = internal dso_local global [86016 x i8] zeroinitializer, align 16
@__compressedAssemblyData_86 = internal dso_local global [299008 x i8] zeroinitializer, align 16

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
