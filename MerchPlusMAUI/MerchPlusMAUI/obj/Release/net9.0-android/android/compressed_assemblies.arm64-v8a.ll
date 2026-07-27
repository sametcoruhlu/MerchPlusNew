; ModuleID = 'compressed_assemblies.arm64-v8a.ll'
source_filename = "compressed_assemblies.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android21"

%struct.CompressedAssemblies = type {
	i32, ; uint32_t count
	ptr ; CompressedAssemblyDescriptor descriptors
}

%struct.CompressedAssemblyDescriptor = type {
	i32, ; uint32_t uncompressed_file_size
	i1, ; bool loaded
	ptr ; uint8_t data
}

@compressed_assemblies = dso_local local_unnamed_addr global %struct.CompressedAssemblies {
	i32 339, ; uint32_t count
	ptr @compressed_assembly_descriptors; CompressedAssemblyDescriptor* descriptors
}, align 8

@compressed_assembly_descriptors = internal dso_local global [339 x %struct.CompressedAssemblyDescriptor] [
	%struct.CompressedAssemblyDescriptor {
		i32 1268224, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_0; uint8_t* data
	}, ; 0: MerchPlusMAUI
	%struct.CompressedAssemblyDescriptor {
		i32 214672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_1; uint8_t* data
	}, ; 1: CommunityToolkit.Maui
	%struct.CompressedAssemblyDescriptor {
		i32 109208, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_2; uint8_t* data
	}, ; 2: CommunityToolkit.Maui.Core
	%struct.CompressedAssemblyDescriptor {
		i32 117392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_3; uint8_t* data
	}, ; 3: CommunityToolkit.Mvvm
	%struct.CompressedAssemblyDescriptor {
		i32 156608, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_4; uint8_t* data
	}, ; 4: GoogleGson
	%struct.CompressedAssemblyDescriptor {
		i32 43800, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_5; uint8_t* data
	}, ; 5: Microsoft.Extensions.Configuration
	%struct.CompressedAssemblyDescriptor {
		i32 27936, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_6; uint8_t* data
	}, ; 6: Microsoft.Extensions.Configuration.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 42656, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_7; uint8_t* data
	}, ; 7: Microsoft.Extensions.Configuration.Binder
	%struct.CompressedAssemblyDescriptor {
		i32 24736, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_8; uint8_t* data
	}, ; 8: Microsoft.Extensions.Configuration.CommandLine
	%struct.CompressedAssemblyDescriptor {
		i32 21280, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_9; uint8_t* data
	}, ; 9: Microsoft.Extensions.Configuration.EnvironmentVariables
	%struct.CompressedAssemblyDescriptor {
		i32 27936, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_10; uint8_t* data
	}, ; 10: Microsoft.Extensions.Configuration.FileExtensions
	%struct.CompressedAssemblyDescriptor {
		i32 26784, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_11; uint8_t* data
	}, ; 11: Microsoft.Extensions.Configuration.Json
	%struct.CompressedAssemblyDescriptor {
		i32 25360, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_12; uint8_t* data
	}, ; 12: Microsoft.Extensions.Configuration.UserSecrets
	%struct.CompressedAssemblyDescriptor {
		i32 92952, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_13; uint8_t* data
	}, ; 13: Microsoft.Extensions.DependencyInjection
	%struct.CompressedAssemblyDescriptor {
		i32 63768, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_14; uint8_t* data
	}, ; 14: Microsoft.Extensions.DependencyInjection.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 35488, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_15; uint8_t* data
	}, ; 15: Microsoft.Extensions.Diagnostics
	%struct.CompressedAssemblyDescriptor {
		i32 30368, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_16; uint8_t* data
	}, ; 16: Microsoft.Extensions.Diagnostics.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 22176, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_17; uint8_t* data
	}, ; 17: Microsoft.Extensions.FileProviders.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 44808, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_18; uint8_t* data
	}, ; 18: Microsoft.Extensions.FileProviders.Physical
	%struct.CompressedAssemblyDescriptor {
		i32 45848, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_19; uint8_t* data
	}, ; 19: Microsoft.Extensions.FileSystemGlobbing
	%struct.CompressedAssemblyDescriptor {
		i32 72480, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_20; uint8_t* data
	}, ; 20: Microsoft.Extensions.Hosting
	%struct.CompressedAssemblyDescriptor {
		i32 51360, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_21; uint8_t* data
	}, ; 21: Microsoft.Extensions.Hosting.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 88736, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_22; uint8_t* data
	}, ; 22: Microsoft.Extensions.Http
	%struct.CompressedAssemblyDescriptor {
		i32 50976, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_23; uint8_t* data
	}, ; 23: Microsoft.Extensions.Logging
	%struct.CompressedAssemblyDescriptor {
		i32 65320, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_24; uint8_t* data
	}, ; 24: Microsoft.Extensions.Logging.Abstractions
	%struct.CompressedAssemblyDescriptor {
		i32 27928, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_25; uint8_t* data
	}, ; 25: Microsoft.Extensions.Logging.Configuration
	%struct.CompressedAssemblyDescriptor {
		i32 71448, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_26; uint8_t* data
	}, ; 26: Microsoft.Extensions.Logging.Console
	%struct.CompressedAssemblyDescriptor {
		i32 20248, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_27; uint8_t* data
	}, ; 27: Microsoft.Extensions.Logging.Debug
	%struct.CompressedAssemblyDescriptor {
		i32 25880, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_28; uint8_t* data
	}, ; 28: Microsoft.Extensions.Logging.EventLog
	%struct.CompressedAssemblyDescriptor {
		i32 34592, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_29; uint8_t* data
	}, ; 29: Microsoft.Extensions.Logging.EventSource
	%struct.CompressedAssemblyDescriptor {
		i32 64776, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_30; uint8_t* data
	}, ; 30: Microsoft.Extensions.Options
	%struct.CompressedAssemblyDescriptor {
		i32 22688, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_31; uint8_t* data
	}, ; 31: Microsoft.Extensions.Options.ConfigurationExtensions
	%struct.CompressedAssemblyDescriptor {
		i32 43680, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_32; uint8_t* data
	}, ; 32: Microsoft.Extensions.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 496640, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_33; uint8_t* data
	}, ; 33: Microsoft.Maui.Controls.Compatibility
	%struct.CompressedAssemblyDescriptor {
		i32 1725480, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_34; uint8_t* data
	}, ; 34: Microsoft.Maui.Controls
	%struct.CompressedAssemblyDescriptor {
		i32 120864, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_35; uint8_t* data
	}, ; 35: Microsoft.Maui.Controls.Xaml
	%struct.CompressedAssemblyDescriptor {
		i32 666560, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_36; uint8_t* data
	}, ; 36: Microsoft.Maui
	%struct.CompressedAssemblyDescriptor {
		i32 245288, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_37; uint8_t* data
	}, ; 37: Microsoft.Maui.Essentials
	%struct.CompressedAssemblyDescriptor {
		i32 205864, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_38; uint8_t* data
	}, ; 38: Microsoft.Maui.Graphics
	%struct.CompressedAssemblyDescriptor {
		i32 712464, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_39; uint8_t* data
	}, ; 39: Newtonsoft.Json
	%struct.CompressedAssemblyDescriptor {
		i32 107520, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_40; uint8_t* data
	}, ; 40: SQLite-net
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_41; uint8_t* data
	}, ; 41: SQLitePCLRaw.batteries_v2
	%struct.CompressedAssemblyDescriptor {
		i32 51200, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_42; uint8_t* data
	}, ; 42: SQLitePCLRaw.core
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_43; uint8_t* data
	}, ; 43: SQLitePCLRaw.lib.e_sqlite3.android
	%struct.CompressedAssemblyDescriptor {
		i32 36864, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_44; uint8_t* data
	}, ; 44: SQLitePCLRaw.provider.e_sqlite3
	%struct.CompressedAssemblyDescriptor {
		i32 51872, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_45; uint8_t* data
	}, ; 45: System.Diagnostics.EventLog
	%struct.CompressedAssemblyDescriptor {
		i32 1089928, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_46; uint8_t* data
	}, ; 46: Xamarin.Android.Glide
	%struct.CompressedAssemblyDescriptor {
		i32 14728, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_47; uint8_t* data
	}, ; 47: Xamarin.Android.Glide.Annotations
	%struct.CompressedAssemblyDescriptor {
		i32 23984, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_48; uint8_t* data
	}, ; 48: Xamarin.Android.Glide.DiskLruCache
	%struct.CompressedAssemblyDescriptor {
		i32 57736, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_49; uint8_t* data
	}, ; 49: Xamarin.Android.Glide.GifDecoder
	%struct.CompressedAssemblyDescriptor {
		i32 160704, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_50; uint8_t* data
	}, ; 50: Xamarin.AndroidX.Activity
	%struct.CompressedAssemblyDescriptor {
		i32 27528, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_51; uint8_t* data
	}, ; 51: Xamarin.AndroidX.Activity.Ktx
	%struct.CompressedAssemblyDescriptor {
		i32 15800, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_52; uint8_t* data
	}, ; 52: Xamarin.AndroidX.Annotation
	%struct.CompressedAssemblyDescriptor {
		i32 35208, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_53; uint8_t* data
	}, ; 53: Xamarin.AndroidX.Annotation.Experimental
	%struct.CompressedAssemblyDescriptor {
		i32 196024, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_54; uint8_t* data
	}, ; 54: Xamarin.AndroidX.Annotation.Jvm
	%struct.CompressedAssemblyDescriptor {
		i32 1105920, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_55; uint8_t* data
	}, ; 55: Xamarin.AndroidX.AppCompat
	%struct.CompressedAssemblyDescriptor {
		i32 95664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_56; uint8_t* data
	}, ; 56: Xamarin.AndroidX.AppCompat.AppCompatResources
	%struct.CompressedAssemblyDescriptor {
		i32 36280, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_57; uint8_t* data
	}, ; 57: Xamarin.AndroidX.Arch.Core.Common
	%struct.CompressedAssemblyDescriptor {
		i32 27056, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_58; uint8_t* data
	}, ; 58: Xamarin.AndroidX.Arch.Core.Runtime
	%struct.CompressedAssemblyDescriptor {
		i32 314800, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_59; uint8_t* data
	}, ; 59: Xamarin.AndroidX.Browser
	%struct.CompressedAssemblyDescriptor {
		i32 32648, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_60; uint8_t* data
	}, ; 60: Xamarin.AndroidX.CardView
	%struct.CompressedAssemblyDescriptor {
		i32 78232, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_61; uint8_t* data
	}, ; 61: Xamarin.AndroidX.Collection
	%struct.CompressedAssemblyDescriptor {
		i32 32688, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_62; uint8_t* data
	}, ; 62: Xamarin.AndroidX.Collection.Ktx
	%struct.CompressedAssemblyDescriptor {
		i32 35208, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_63; uint8_t* data
	}, ; 63: Xamarin.AndroidX.Concurrent.Futures
	%struct.CompressedAssemblyDescriptor {
		i32 647104, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_64; uint8_t* data
	}, ; 64: Xamarin.AndroidX.ConstraintLayout
	%struct.CompressedAssemblyDescriptor {
		i32 956336, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_65; uint8_t* data
	}, ; 65: Xamarin.AndroidX.ConstraintLayout.Core
	%struct.CompressedAssemblyDescriptor {
		i32 105408, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_66; uint8_t* data
	}, ; 66: Xamarin.AndroidX.CoordinatorLayout
	%struct.CompressedAssemblyDescriptor {
		i32 1901056, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_67; uint8_t* data
	}, ; 67: Xamarin.AndroidX.Core
	%struct.CompressedAssemblyDescriptor {
		i32 215944, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_68; uint8_t* data
	}, ; 68: Xamarin.AndroidX.Core.Core.Ktx
	%struct.CompressedAssemblyDescriptor {
		i32 31128, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_69; uint8_t* data
	}, ; 69: Xamarin.AndroidX.Core.SplashScreen
	%struct.CompressedAssemblyDescriptor {
		i32 59840, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_70; uint8_t* data
	}, ; 70: Xamarin.AndroidX.CursorAdapter
	%struct.CompressedAssemblyDescriptor {
		i32 66480, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_71; uint8_t* data
	}, ; 71: Xamarin.AndroidX.CustomView
	%struct.CompressedAssemblyDescriptor {
		i32 24984, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_72; uint8_t* data
	}, ; 72: Xamarin.AndroidX.CustomView.PoolingContainer
	%struct.CompressedAssemblyDescriptor {
		i32 30616, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_73; uint8_t* data
	}, ; 73: Xamarin.AndroidX.DocumentFile
	%struct.CompressedAssemblyDescriptor {
		i32 62384, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_74; uint8_t* data
	}, ; 74: Xamarin.AndroidX.DrawerLayout
	%struct.CompressedAssemblyDescriptor {
		i32 56728, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_75; uint8_t* data
	}, ; 75: Xamarin.AndroidX.DynamicAnimation
	%struct.CompressedAssemblyDescriptor {
		i32 261560, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_76; uint8_t* data
	}, ; 76: Xamarin.AndroidX.Emoji2
	%struct.CompressedAssemblyDescriptor {
		i32 25496, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_77; uint8_t* data
	}, ; 77: Xamarin.AndroidX.Emoji2.ViewsHelper
	%struct.CompressedAssemblyDescriptor {
		i32 69016, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_78; uint8_t* data
	}, ; 78: Xamarin.AndroidX.ExifInterface
	%struct.CompressedAssemblyDescriptor {
		i32 325056, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_79; uint8_t* data
	}, ; 79: Xamarin.AndroidX.Fragment
	%struct.CompressedAssemblyDescriptor {
		i32 27016, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_80; uint8_t* data
	}, ; 80: Xamarin.AndroidX.Fragment.Ktx
	%struct.CompressedAssemblyDescriptor {
		i32 24456, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_81; uint8_t* data
	}, ; 81: Xamarin.AndroidX.Interpolator
	%struct.CompressedAssemblyDescriptor {
		i32 20888, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_82; uint8_t* data
	}, ; 82: Xamarin.AndroidX.Legacy.Support.Core.Utils
	%struct.CompressedAssemblyDescriptor {
		i32 67504, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_83; uint8_t* data
	}, ; 83: Xamarin.AndroidX.Lifecycle.Common
	%struct.CompressedAssemblyDescriptor {
		i32 28056, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_84; uint8_t* data
	}, ; 84: Xamarin.AndroidX.Lifecycle.LiveData
	%struct.CompressedAssemblyDescriptor {
		i32 33160, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_85; uint8_t* data
	}, ; 85: Xamarin.AndroidX.Lifecycle.LiveData.Core
	%struct.CompressedAssemblyDescriptor {
		i32 19848, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_86; uint8_t* data
	}, ; 86: Xamarin.AndroidX.Lifecycle.LiveData.Core.Ktx
	%struct.CompressedAssemblyDescriptor {
		i32 22976, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_87; uint8_t* data
	}, ; 87: Xamarin.AndroidX.Lifecycle.Process
	%struct.CompressedAssemblyDescriptor {
		i32 31680, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_88; uint8_t* data
	}, ; 88: Xamarin.AndroidX.Lifecycle.Runtime
	%struct.CompressedAssemblyDescriptor {
		i32 30640, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_89; uint8_t* data
	}, ; 89: Xamarin.AndroidX.Lifecycle.Runtime.Ktx
	%struct.CompressedAssemblyDescriptor {
		i32 67992, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_90; uint8_t* data
	}, ; 90: Xamarin.AndroidX.Lifecycle.ViewModel
	%struct.CompressedAssemblyDescriptor {
		i32 19392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_91; uint8_t* data
	}, ; 91: Xamarin.AndroidX.Lifecycle.ViewModel.Ktx
	%struct.CompressedAssemblyDescriptor {
		i32 39816, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_92; uint8_t* data
	}, ; 92: Xamarin.AndroidX.Lifecycle.ViewModelSavedState
	%struct.CompressedAssemblyDescriptor {
		i32 65472, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_93; uint8_t* data
	}, ; 93: Xamarin.AndroidX.Loader
	%struct.CompressedAssemblyDescriptor {
		i32 21384, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_94; uint8_t* data
	}, ; 94: Xamarin.AndroidX.LocalBroadcastManager
	%struct.CompressedAssemblyDescriptor {
		i32 184768, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_95; uint8_t* data
	}, ; 95: Xamarin.AndroidX.Navigation.Common
	%struct.CompressedAssemblyDescriptor {
		i32 57240, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_96; uint8_t* data
	}, ; 96: Xamarin.AndroidX.Navigation.Fragment
	%struct.CompressedAssemblyDescriptor {
		i32 106904, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_97; uint8_t* data
	}, ; 97: Xamarin.AndroidX.Navigation.Runtime
	%struct.CompressedAssemblyDescriptor {
		i32 57752, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_98; uint8_t* data
	}, ; 98: Xamarin.AndroidX.Navigation.UI
	%struct.CompressedAssemblyDescriptor {
		i32 25024, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_99; uint8_t* data
	}, ; 99: Xamarin.AndroidX.Print
	%struct.CompressedAssemblyDescriptor {
		i32 49032, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_100; uint8_t* data
	}, ; 100: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller
	%struct.CompressedAssemblyDescriptor {
		i32 606616, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_101; uint8_t* data
	}, ; 101: Xamarin.AndroidX.RecyclerView
	%struct.CompressedAssemblyDescriptor {
		i32 28568, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_102; uint8_t* data
	}, ; 102: Xamarin.AndroidX.ResourceInspection.Annotation
	%struct.CompressedAssemblyDescriptor {
		i32 34184, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_103; uint8_t* data
	}, ; 103: Xamarin.AndroidX.SavedState
	%struct.CompressedAssemblyDescriptor {
		i32 18312, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_104; uint8_t* data
	}, ; 104: Xamarin.AndroidX.SavedState.SavedState.Ktx
	%struct.CompressedAssemblyDescriptor {
		i32 45960, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_105; uint8_t* data
	}, ; 105: Xamarin.AndroidX.Security.SecurityCrypto
	%struct.CompressedAssemblyDescriptor {
		i32 47000, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_106; uint8_t* data
	}, ; 106: Xamarin.AndroidX.SlidingPaneLayout
	%struct.CompressedAssemblyDescriptor {
		i32 30656, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_107; uint8_t* data
	}, ; 107: Xamarin.AndroidX.Startup.StartupRuntime
	%struct.CompressedAssemblyDescriptor {
		i32 70024, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_108; uint8_t* data
	}, ; 108: Xamarin.AndroidX.SwipeRefreshLayout
	%struct.CompressedAssemblyDescriptor {
		i32 19848, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_109; uint8_t* data
	}, ; 109: Xamarin.AndroidX.Tracing.Tracing
	%struct.CompressedAssemblyDescriptor {
		i32 144384, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_110; uint8_t* data
	}, ; 110: Xamarin.AndroidX.Transition
	%struct.CompressedAssemblyDescriptor {
		i32 33688, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_111; uint8_t* data
	}, ; 111: Xamarin.AndroidX.VectorDrawable
	%struct.CompressedAssemblyDescriptor {
		i32 47040, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_112; uint8_t* data
	}, ; 112: Xamarin.AndroidX.VectorDrawable.Animated
	%struct.CompressedAssemblyDescriptor {
		i32 111040, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_113; uint8_t* data
	}, ; 113: Xamarin.AndroidX.VersionedParcelable
	%struct.CompressedAssemblyDescriptor {
		i32 86920, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_114; uint8_t* data
	}, ; 114: Xamarin.AndroidX.ViewPager
	%struct.CompressedAssemblyDescriptor {
		i32 56768, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_115; uint8_t* data
	}, ; 115: Xamarin.AndroidX.ViewPager2
	%struct.CompressedAssemblyDescriptor {
		i32 156088, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_116; uint8_t* data
	}, ; 116: Xamarin.AndroidX.Window
	%struct.CompressedAssemblyDescriptor {
		i32 24472, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_117; uint8_t* data
	}, ; 117: Xamarin.AndroidX.Window.Extensions.Core.Core
	%struct.CompressedAssemblyDescriptor {
		i32 2371072, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_118; uint8_t* data
	}, ; 118: Xamarin.Google.Android.Material
	%struct.CompressedAssemblyDescriptor {
		i32 91528, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_119; uint8_t* data
	}, ; 119: Jsr305Binding
	%struct.CompressedAssemblyDescriptor {
		i32 4096512, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_120; uint8_t* data
	}, ; 120: Xamarin.Google.Crypto.Tink.Android
	%struct.CompressedAssemblyDescriptor {
		i32 87944, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_121; uint8_t* data
	}, ; 121: Xamarin.Google.ErrorProne.Annotations
	%struct.CompressedAssemblyDescriptor {
		i32 23488, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_122; uint8_t* data
	}, ; 122: Xamarin.Google.Guava.ListenableFuture
	%struct.CompressedAssemblyDescriptor {
		i32 142336, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_123; uint8_t* data
	}, ; 123: Xamarin.Jetbrains.Annotations
	%struct.CompressedAssemblyDescriptor {
		i32 2214912, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_124; uint8_t* data
	}, ; 124: Xamarin.Kotlin.StdLib
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_125; uint8_t* data
	}, ; 125: Xamarin.Kotlin.StdLib.Common
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_126; uint8_t* data
	}, ; 126: Xamarin.Kotlin.StdLib.Jdk7
	%struct.CompressedAssemblyDescriptor {
		i32 5632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_127; uint8_t* data
	}, ; 127: Xamarin.Kotlin.StdLib.Jdk8
	%struct.CompressedAssemblyDescriptor {
		i32 27072, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_128; uint8_t* data
	}, ; 128: Xamarin.KotlinX.Coroutines.Android
	%struct.CompressedAssemblyDescriptor {
		i32 539584, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_129; uint8_t* data
	}, ; 129: Xamarin.KotlinX.Coroutines.Core.Jvm
	%struct.CompressedAssemblyDescriptor {
		i32 15408, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_130; uint8_t* data
	}, ; 130: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_131; uint8_t* data
	}, ; 131: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_132; uint8_t* data
	}, ; 132: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_133; uint8_t* data
	}, ; 133: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_134; uint8_t* data
	}, ; 134: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_135; uint8_t* data
	}, ; 135: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_136; uint8_t* data
	}, ; 136: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_137; uint8_t* data
	}, ; 137: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15400, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_138; uint8_t* data
	}, ; 138: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_139; uint8_t* data
	}, ; 139: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_140; uint8_t* data
	}, ; 140: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_141; uint8_t* data
	}, ; 141: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_142; uint8_t* data
	}, ; 142: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_143; uint8_t* data
	}, ; 143: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_144; uint8_t* data
	}, ; 144: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15408, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_145; uint8_t* data
	}, ; 145: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_146; uint8_t* data
	}, ; 146: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_147; uint8_t* data
	}, ; 147: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_148; uint8_t* data
	}, ; 148: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_149; uint8_t* data
	}, ; 149: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15400, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_150; uint8_t* data
	}, ; 150: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_151; uint8_t* data
	}, ; 151: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15408, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_152; uint8_t* data
	}, ; 152: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15400, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_153; uint8_t* data
	}, ; 153: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_154; uint8_t* data
	}, ; 154: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15384, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_155; uint8_t* data
	}, ; 155: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15400, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_156; uint8_t* data
	}, ; 156: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_157; uint8_t* data
	}, ; 157: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_158; uint8_t* data
	}, ; 158: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15400, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_159; uint8_t* data
	}, ; 159: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_160; uint8_t* data
	}, ; 160: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_161; uint8_t* data
	}, ; 161: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_162; uint8_t* data
	}, ; 162: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 15400, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_163; uint8_t* data
	}, ; 163: Microsoft.Maui.Controls.resources
	%struct.CompressedAssemblyDescriptor {
		i32 36864, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_164; uint8_t* data
	}, ; 164: entMerchPlus
	%struct.CompressedAssemblyDescriptor {
		i32 703488, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_165; uint8_t* data
	}, ; 165: _Microsoft.Android.Resource.Designer
	%struct.CompressedAssemblyDescriptor {
		i32 307984, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_166; uint8_t* data
	}, ; 166: Microsoft.CSharp
	%struct.CompressedAssemblyDescriptor {
		i32 430352, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_167; uint8_t* data
	}, ; 167: Microsoft.VisualBasic.Core
	%struct.CompressedAssemblyDescriptor {
		i32 17680, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_168; uint8_t* data
	}, ; 168: Microsoft.VisualBasic
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_169; uint8_t* data
	}, ; 169: Microsoft.Win32.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 33592, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_170; uint8_t* data
	}, ; 170: Microsoft.Win32.Registry
	%struct.CompressedAssemblyDescriptor {
		i32 15672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_171; uint8_t* data
	}, ; 171: System.AppContext
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_172; uint8_t* data
	}, ; 172: System.Buffers
	%struct.CompressedAssemblyDescriptor {
		i32 89872, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_173; uint8_t* data
	}, ; 173: System.Collections.Concurrent
	%struct.CompressedAssemblyDescriptor {
		i32 255760, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_174; uint8_t* data
	}, ; 174: System.Collections.Immutable
	%struct.CompressedAssemblyDescriptor {
		i32 48440, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_175; uint8_t* data
	}, ; 175: System.Collections.NonGeneric
	%struct.CompressedAssemblyDescriptor {
		i32 48392, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_176; uint8_t* data
	}, ; 176: System.Collections.Specialized
	%struct.CompressedAssemblyDescriptor {
		i32 126736, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_177; uint8_t* data
	}, ; 177: System.Collections
	%struct.CompressedAssemblyDescriptor {
		i32 102672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_178; uint8_t* data
	}, ; 178: System.ComponentModel.Annotations
	%struct.CompressedAssemblyDescriptor {
		i32 17168, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_179; uint8_t* data
	}, ; 179: System.ComponentModel.DataAnnotations
	%struct.CompressedAssemblyDescriptor {
		i32 26888, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_180; uint8_t* data
	}, ; 180: System.ComponentModel.EventBasedAsync
	%struct.CompressedAssemblyDescriptor {
		i32 42296, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_181; uint8_t* data
	}, ; 181: System.ComponentModel.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 315664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_182; uint8_t* data
	}, ; 182: System.ComponentModel.TypeConverter
	%struct.CompressedAssemblyDescriptor {
		i32 16656, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_183; uint8_t* data
	}, ; 183: System.ComponentModel
	%struct.CompressedAssemblyDescriptor {
		i32 19728, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_184; uint8_t* data
	}, ; 184: System.Configuration
	%struct.CompressedAssemblyDescriptor {
		i32 50992, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_185; uint8_t* data
	}, ; 185: System.Console
	%struct.CompressedAssemblyDescriptor {
		i32 23816, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_186; uint8_t* data
	}, ; 186: System.Core
	%struct.CompressedAssemblyDescriptor {
		i32 1016624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_187; uint8_t* data
	}, ; 187: System.Data.Common
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_188; uint8_t* data
	}, ; 188: System.Data.DataSetExtensions
	%struct.CompressedAssemblyDescriptor {
		i32 25360, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_189; uint8_t* data
	}, ; 189: System.Data
	%struct.CompressedAssemblyDescriptor {
		i32 16688, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_190; uint8_t* data
	}, ; 190: System.Diagnostics.Contracts
	%struct.CompressedAssemblyDescriptor {
		i32 16136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_191; uint8_t* data
	}, ; 191: System.Diagnostics.Debug
	%struct.CompressedAssemblyDescriptor {
		i32 184584, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_192; uint8_t* data
	}, ; 192: System.Diagnostics.DiagnosticSource
	%struct.CompressedAssemblyDescriptor {
		i32 29496, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_193; uint8_t* data
	}, ; 193: System.Diagnostics.FileVersionInfo
	%struct.CompressedAssemblyDescriptor {
		i32 127248, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_194; uint8_t* data
	}, ; 194: System.Diagnostics.Process
	%struct.CompressedAssemblyDescriptor {
		i32 26376, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_195; uint8_t* data
	}, ; 195: System.Diagnostics.StackTrace
	%struct.CompressedAssemblyDescriptor {
		i32 32048, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_196; uint8_t* data
	}, ; 196: System.Diagnostics.TextWriterTraceListener
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_197; uint8_t* data
	}, ; 197: System.Diagnostics.Tools
	%struct.CompressedAssemblyDescriptor {
		i32 59144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_198; uint8_t* data
	}, ; 198: System.Diagnostics.TraceSource
	%struct.CompressedAssemblyDescriptor {
		i32 16656, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_199; uint8_t* data
	}, ; 199: System.Diagnostics.Tracing
	%struct.CompressedAssemblyDescriptor {
		i32 64784, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_200; uint8_t* data
	}, ; 200: System.Drawing.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 20752, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_201; uint8_t* data
	}, ; 201: System.Drawing
	%struct.CompressedAssemblyDescriptor {
		i32 16696, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_202; uint8_t* data
	}, ; 202: System.Dynamic.Runtime
	%struct.CompressedAssemblyDescriptor {
		i32 96560, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_203; uint8_t* data
	}, ; 203: System.Formats.Asn1
	%struct.CompressedAssemblyDescriptor {
		i32 121616, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_204; uint8_t* data
	}, ; 204: System.Formats.Tar
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_205; uint8_t* data
	}, ; 205: System.Globalization.Calendars
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_206; uint8_t* data
	}, ; 206: System.Globalization.Extensions
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_207; uint8_t* data
	}, ; 207: System.Globalization
	%struct.CompressedAssemblyDescriptor {
		i32 41232, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_208; uint8_t* data
	}, ; 208: System.IO.Compression.Brotli
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_209; uint8_t* data
	}, ; 209: System.IO.Compression.FileSystem
	%struct.CompressedAssemblyDescriptor {
		i32 38160, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_210; uint8_t* data
	}, ; 210: System.IO.Compression.ZipFile
	%struct.CompressedAssemblyDescriptor {
		i32 110344, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_211; uint8_t* data
	}, ; 211: System.IO.Compression
	%struct.CompressedAssemblyDescriptor {
		i32 32568, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_212; uint8_t* data
	}, ; 212: System.IO.FileSystem.AccessControl
	%struct.CompressedAssemblyDescriptor {
		i32 48400, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_213; uint8_t* data
	}, ; 213: System.IO.FileSystem.DriveInfo
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_214; uint8_t* data
	}, ; 214: System.IO.FileSystem.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 55088, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_215; uint8_t* data
	}, ; 215: System.IO.FileSystem.Watcher
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_216; uint8_t* data
	}, ; 216: System.IO.FileSystem
	%struct.CompressedAssemblyDescriptor {
		i32 43832, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_217; uint8_t* data
	}, ; 217: System.IO.IsolatedStorage
	%struct.CompressedAssemblyDescriptor {
		i32 48952, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_218; uint8_t* data
	}, ; 218: System.IO.MemoryMappedFiles
	%struct.CompressedAssemblyDescriptor {
		i32 78640, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_219; uint8_t* data
	}, ; 219: System.IO.Pipelines
	%struct.CompressedAssemblyDescriptor {
		i32 23816, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_220; uint8_t* data
	}, ; 220: System.IO.Pipes.AccessControl
	%struct.CompressedAssemblyDescriptor {
		i32 67888, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_221; uint8_t* data
	}, ; 221: System.IO.Pipes
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_222; uint8_t* data
	}, ; 222: System.IO.UnmanagedMemoryStream
	%struct.CompressedAssemblyDescriptor {
		i32 16136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_223; uint8_t* data
	}, ; 223: System.IO
	%struct.CompressedAssemblyDescriptor {
		i32 575752, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_224; uint8_t* data
	}, ; 224: System.Linq.Expressions
	%struct.CompressedAssemblyDescriptor {
		i32 223496, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_225; uint8_t* data
	}, ; 225: System.Linq.Parallel
	%struct.CompressedAssemblyDescriptor {
		i32 76552, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_226; uint8_t* data
	}, ; 226: System.Linq.Queryable
	%struct.CompressedAssemblyDescriptor {
		i32 149264, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_227; uint8_t* data
	}, ; 227: System.Linq
	%struct.CompressedAssemblyDescriptor {
		i32 56120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_228; uint8_t* data
	}, ; 228: System.Memory
	%struct.CompressedAssemblyDescriptor {
		i32 56592, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_229; uint8_t* data
	}, ; 229: System.Net.Http.Json
	%struct.CompressedAssemblyDescriptor {
		i32 676664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_230; uint8_t* data
	}, ; 230: System.Net.Http
	%struct.CompressedAssemblyDescriptor {
		i32 131896, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_231; uint8_t* data
	}, ; 231: System.Net.HttpListener
	%struct.CompressedAssemblyDescriptor {
		i32 174904, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_232; uint8_t* data
	}, ; 232: System.Net.Mail
	%struct.CompressedAssemblyDescriptor {
		i32 51976, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_233; uint8_t* data
	}, ; 233: System.Net.NameResolution
	%struct.CompressedAssemblyDescriptor {
		i32 66320, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_234; uint8_t* data
	}, ; 234: System.Net.NetworkInformation
	%struct.CompressedAssemblyDescriptor {
		i32 56080, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_235; uint8_t* data
	}, ; 235: System.Net.Ping
	%struct.CompressedAssemblyDescriptor {
		i32 107280, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_236; uint8_t* data
	}, ; 236: System.Net.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 173360, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_237; uint8_t* data
	}, ; 237: System.Net.Quic
	%struct.CompressedAssemblyDescriptor {
		i32 162104, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_238; uint8_t* data
	}, ; 238: System.Net.Requests
	%struct.CompressedAssemblyDescriptor {
		i32 253752, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_239; uint8_t* data
	}, ; 239: System.Net.Security
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_240; uint8_t* data
	}, ; 240: System.Net.ServicePoint
	%struct.CompressedAssemblyDescriptor {
		i32 235280, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_241; uint8_t* data
	}, ; 241: System.Net.Sockets
	%struct.CompressedAssemblyDescriptor {
		i32 70928, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_242; uint8_t* data
	}, ; 242: System.Net.WebClient
	%struct.CompressedAssemblyDescriptor {
		i32 33584, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_243; uint8_t* data
	}, ; 243: System.Net.WebHeaderCollection
	%struct.CompressedAssemblyDescriptor {
		i32 23864, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_244; uint8_t* data
	}, ; 244: System.Net.WebProxy
	%struct.CompressedAssemblyDescriptor {
		i32 51976, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_245; uint8_t* data
	}, ; 245: System.Net.WebSockets.Client
	%struct.CompressedAssemblyDescriptor {
		i32 103176, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_246; uint8_t* data
	}, ; 246: System.Net.WebSockets
	%struct.CompressedAssemblyDescriptor {
		i32 17680, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_247; uint8_t* data
	}, ; 247: System.Net
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_248; uint8_t* data
	}, ; 248: System.Numerics.Vectors
	%struct.CompressedAssemblyDescriptor {
		i32 15672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_249; uint8_t* data
	}, ; 249: System.Numerics
	%struct.CompressedAssemblyDescriptor {
		i32 41776, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_250; uint8_t* data
	}, ; 250: System.ObjectModel
	%struct.CompressedAssemblyDescriptor {
		i32 852272, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_251; uint8_t* data
	}, ; 251: System.Private.DataContractSerialization
	%struct.CompressedAssemblyDescriptor {
		i32 103216, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_252; uint8_t* data
	}, ; 252: System.Private.Uri
	%struct.CompressedAssemblyDescriptor {
		i32 153872, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_253; uint8_t* data
	}, ; 253: System.Private.Xml.Linq
	%struct.CompressedAssemblyDescriptor {
		i32 3099920, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_254; uint8_t* data
	}, ; 254: System.Private.Xml
	%struct.CompressedAssemblyDescriptor {
		i32 38704, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_255; uint8_t* data
	}, ; 255: System.Reflection.DispatchProxy
	%struct.CompressedAssemblyDescriptor {
		i32 16136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_256; uint8_t* data
	}, ; 256: System.Reflection.Emit.ILGeneration
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_257; uint8_t* data
	}, ; 257: System.Reflection.Emit.Lightweight
	%struct.CompressedAssemblyDescriptor {
		i32 130352, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_258; uint8_t* data
	}, ; 258: System.Reflection.Emit
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_259; uint8_t* data
	}, ; 259: System.Reflection.Extensions
	%struct.CompressedAssemblyDescriptor {
		i32 501520, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_260; uint8_t* data
	}, ; 260: System.Reflection.Metadata
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_261; uint8_t* data
	}, ; 261: System.Reflection.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 24336, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_262; uint8_t* data
	}, ; 262: System.Reflection.TypeExtensions
	%struct.CompressedAssemblyDescriptor {
		i32 16656, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_263; uint8_t* data
	}, ; 263: System.Reflection
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_264; uint8_t* data
	}, ; 264: System.Resources.Reader
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_265; uint8_t* data
	}, ; 265: System.Resources.ResourceManager
	%struct.CompressedAssemblyDescriptor {
		i32 26896, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_266; uint8_t* data
	}, ; 266: System.Resources.Writer
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_267; uint8_t* data
	}, ; 267: System.Runtime.CompilerServices.Unsafe
	%struct.CompressedAssemblyDescriptor {
		i32 17720, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_268; uint8_t* data
	}, ; 268: System.Runtime.CompilerServices.VisualC
	%struct.CompressedAssemblyDescriptor {
		i32 18224, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_269; uint8_t* data
	}, ; 269: System.Runtime.Extensions
	%struct.CompressedAssemblyDescriptor {
		i32 15672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_270; uint8_t* data
	}, ; 270: System.Runtime.Handles
	%struct.CompressedAssemblyDescriptor {
		i32 38672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_271; uint8_t* data
	}, ; 271: System.Runtime.InteropServices.JavaScript
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_272; uint8_t* data
	}, ; 272: System.Runtime.InteropServices.RuntimeInformation
	%struct.CompressedAssemblyDescriptor {
		i32 64816, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_273; uint8_t* data
	}, ; 273: System.Runtime.InteropServices
	%struct.CompressedAssemblyDescriptor {
		i32 17680, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_274; uint8_t* data
	}, ; 274: System.Runtime.Intrinsics
	%struct.CompressedAssemblyDescriptor {
		i32 16136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_275; uint8_t* data
	}, ; 275: System.Runtime.Loader
	%struct.CompressedAssemblyDescriptor {
		i32 143632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_276; uint8_t* data
	}, ; 276: System.Runtime.Numerics
	%struct.CompressedAssemblyDescriptor {
		i32 66360, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_277; uint8_t* data
	}, ; 277: System.Runtime.Serialization.Formatters
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_278; uint8_t* data
	}, ; 278: System.Runtime.Serialization.Json
	%struct.CompressedAssemblyDescriptor {
		i32 23824, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_279; uint8_t* data
	}, ; 279: System.Runtime.Serialization.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 17168, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_280; uint8_t* data
	}, ; 280: System.Runtime.Serialization.Xml
	%struct.CompressedAssemblyDescriptor {
		i32 17168, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_281; uint8_t* data
	}, ; 281: System.Runtime.Serialization
	%struct.CompressedAssemblyDescriptor {
		i32 44816, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_282; uint8_t* data
	}, ; 282: System.Runtime
	%struct.CompressedAssemblyDescriptor {
		i32 58632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_283; uint8_t* data
	}, ; 283: System.Security.AccessControl
	%struct.CompressedAssemblyDescriptor {
		i32 54024, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_284; uint8_t* data
	}, ; 284: System.Security.Claims
	%struct.CompressedAssemblyDescriptor {
		i32 17680, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_285; uint8_t* data
	}, ; 285: System.Security.Cryptography.Algorithms
	%struct.CompressedAssemblyDescriptor {
		i32 16696, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_286; uint8_t* data
	}, ; 286: System.Security.Cryptography.Cng
	%struct.CompressedAssemblyDescriptor {
		i32 16176, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_287; uint8_t* data
	}, ; 287: System.Security.Cryptography.Csp
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_288; uint8_t* data
	}, ; 288: System.Security.Cryptography.Encoding
	%struct.CompressedAssemblyDescriptor {
		i32 15672, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_289; uint8_t* data
	}, ; 289: System.Security.Cryptography.OpenSsl
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_290; uint8_t* data
	}, ; 290: System.Security.Cryptography.Primitives
	%struct.CompressedAssemblyDescriptor {
		i32 17200, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_291; uint8_t* data
	}, ; 291: System.Security.Cryptography.X509Certificates
	%struct.CompressedAssemblyDescriptor {
		i32 705296, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_292; uint8_t* data
	}, ; 292: System.Security.Cryptography
	%struct.CompressedAssemblyDescriptor {
		i32 38152, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_293; uint8_t* data
	}, ; 293: System.Security.Principal.Windows
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_294; uint8_t* data
	}, ; 294: System.Security.Principal
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_295; uint8_t* data
	}, ; 295: System.Security.SecureString
	%struct.CompressedAssemblyDescriptor {
		i32 18744, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_296; uint8_t* data
	}, ; 296: System.Security
	%struct.CompressedAssemblyDescriptor {
		i32 17200, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_297; uint8_t* data
	}, ; 297: System.ServiceModel.Web
	%struct.CompressedAssemblyDescriptor {
		i32 16176, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_298; uint8_t* data
	}, ; 298: System.ServiceProcess
	%struct.CompressedAssemblyDescriptor {
		i32 741168, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_299; uint8_t* data
	}, ; 299: System.Text.Encoding.CodePages
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_300; uint8_t* data
	}, ; 300: System.Text.Encoding.Extensions
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_301; uint8_t* data
	}, ; 301: System.Text.Encoding
	%struct.CompressedAssemblyDescriptor {
		i32 70416, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_302; uint8_t* data
	}, ; 302: System.Text.Encodings.Web
	%struct.CompressedAssemblyDescriptor {
		i32 617736, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_303; uint8_t* data
	}, ; 303: System.Text.Json
	%struct.CompressedAssemblyDescriptor {
		i32 369424, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_304; uint8_t* data
	}, ; 304: System.Text.RegularExpressions
	%struct.CompressedAssemblyDescriptor {
		i32 57096, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_305; uint8_t* data
	}, ; 305: System.Threading.Channels
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_306; uint8_t* data
	}, ; 306: System.Threading.Overlapped
	%struct.CompressedAssemblyDescriptor {
		i32 186120, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_307; uint8_t* data
	}, ; 307: System.Threading.Tasks.Dataflow
	%struct.CompressedAssemblyDescriptor {
		i32 16136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_308; uint8_t* data
	}, ; 308: System.Threading.Tasks.Extensions
	%struct.CompressedAssemblyDescriptor {
		i32 61712, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_309; uint8_t* data
	}, ; 309: System.Threading.Tasks.Parallel
	%struct.CompressedAssemblyDescriptor {
		i32 17200, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_310; uint8_t* data
	}, ; 310: System.Threading.Tasks
	%struct.CompressedAssemblyDescriptor {
		i32 16184, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_311; uint8_t* data
	}, ; 311: System.Threading.Thread
	%struct.CompressedAssemblyDescriptor {
		i32 16136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_312; uint8_t* data
	}, ; 312: System.Threading.ThreadPool
	%struct.CompressedAssemblyDescriptor {
		i32 15632, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_313; uint8_t* data
	}, ; 313: System.Threading.Timer
	%struct.CompressedAssemblyDescriptor {
		i32 45360, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_314; uint8_t* data
	}, ; 314: System.Threading
	%struct.CompressedAssemblyDescriptor {
		i32 175928, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_315; uint8_t* data
	}, ; 315: System.Transactions.Local
	%struct.CompressedAssemblyDescriptor {
		i32 16696, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_316; uint8_t* data
	}, ; 316: System.Transactions
	%struct.CompressedAssemblyDescriptor {
		i32 15664, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_317; uint8_t* data
	}, ; 317: System.ValueTuple
	%struct.CompressedAssemblyDescriptor {
		i32 30992, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_318; uint8_t* data
	}, ; 318: System.Web.HttpUtility
	%struct.CompressedAssemblyDescriptor {
		i32 15624, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_319; uint8_t* data
	}, ; 319: System.Web
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_320; uint8_t* data
	}, ; 320: System.Windows
	%struct.CompressedAssemblyDescriptor {
		i32 16184, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_321; uint8_t* data
	}, ; 321: System.Xml.Linq
	%struct.CompressedAssemblyDescriptor {
		i32 22328, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_322; uint8_t* data
	}, ; 322: System.Xml.ReaderWriter
	%struct.CompressedAssemblyDescriptor {
		i32 16696, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_323; uint8_t* data
	}, ; 323: System.Xml.Serialization
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_324; uint8_t* data
	}, ; 324: System.Xml.XDocument
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_325; uint8_t* data
	}, ; 325: System.Xml.XPath.XDocument
	%struct.CompressedAssemblyDescriptor {
		i32 16144, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_326; uint8_t* data
	}, ; 326: System.Xml.XPath
	%struct.CompressedAssemblyDescriptor {
		i32 16136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_327; uint8_t* data
	}, ; 327: System.Xml.XmlDocument
	%struct.CompressedAssemblyDescriptor {
		i32 18192, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_328; uint8_t* data
	}, ; 328: System.Xml.XmlSerializer
	%struct.CompressedAssemblyDescriptor {
		i32 23824, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_329; uint8_t* data
	}, ; 329: System.Xml
	%struct.CompressedAssemblyDescriptor {
		i32 50960, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_330; uint8_t* data
	}, ; 330: System
	%struct.CompressedAssemblyDescriptor {
		i32 16656, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_331; uint8_t* data
	}, ; 331: WindowsBase
	%struct.CompressedAssemblyDescriptor {
		i32 60176, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_332; uint8_t* data
	}, ; 332: mscorlib
	%struct.CompressedAssemblyDescriptor {
		i32 101136, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_333; uint8_t* data
	}, ; 333: netstandard
	%struct.CompressedAssemblyDescriptor {
		i32 240160, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_334; uint8_t* data
	}, ; 334: Java.Interop
	%struct.CompressedAssemblyDescriptor {
		i32 82968, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_335; uint8_t* data
	}, ; 335: Mono.Android.Export
	%struct.CompressedAssemblyDescriptor {
		i32 19512, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_336; uint8_t* data
	}, ; 336: Mono.Android.Runtime
	%struct.CompressedAssemblyDescriptor {
		i32 37450296, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_337; uint8_t* data
	}, ; 337: Mono.Android
	%struct.CompressedAssemblyDescriptor {
		i32 4804920, ; uint32_t uncompressed_file_size
		i1 false, ; bool loaded
		ptr @__compressedAssemblyData_338; uint8_t* data
	} ; 338: System.Private.CoreLib
], align 8

@__compressedAssemblyData_0 = internal dso_local global [1268224 x i8] zeroinitializer, align 1
@__compressedAssemblyData_1 = internal dso_local global [214672 x i8] zeroinitializer, align 1
@__compressedAssemblyData_2 = internal dso_local global [109208 x i8] zeroinitializer, align 1
@__compressedAssemblyData_3 = internal dso_local global [117392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_4 = internal dso_local global [156608 x i8] zeroinitializer, align 1
@__compressedAssemblyData_5 = internal dso_local global [43800 x i8] zeroinitializer, align 1
@__compressedAssemblyData_6 = internal dso_local global [27936 x i8] zeroinitializer, align 1
@__compressedAssemblyData_7 = internal dso_local global [42656 x i8] zeroinitializer, align 1
@__compressedAssemblyData_8 = internal dso_local global [24736 x i8] zeroinitializer, align 1
@__compressedAssemblyData_9 = internal dso_local global [21280 x i8] zeroinitializer, align 1
@__compressedAssemblyData_10 = internal dso_local global [27936 x i8] zeroinitializer, align 1
@__compressedAssemblyData_11 = internal dso_local global [26784 x i8] zeroinitializer, align 1
@__compressedAssemblyData_12 = internal dso_local global [25360 x i8] zeroinitializer, align 1
@__compressedAssemblyData_13 = internal dso_local global [92952 x i8] zeroinitializer, align 1
@__compressedAssemblyData_14 = internal dso_local global [63768 x i8] zeroinitializer, align 1
@__compressedAssemblyData_15 = internal dso_local global [35488 x i8] zeroinitializer, align 1
@__compressedAssemblyData_16 = internal dso_local global [30368 x i8] zeroinitializer, align 1
@__compressedAssemblyData_17 = internal dso_local global [22176 x i8] zeroinitializer, align 1
@__compressedAssemblyData_18 = internal dso_local global [44808 x i8] zeroinitializer, align 1
@__compressedAssemblyData_19 = internal dso_local global [45848 x i8] zeroinitializer, align 1
@__compressedAssemblyData_20 = internal dso_local global [72480 x i8] zeroinitializer, align 1
@__compressedAssemblyData_21 = internal dso_local global [51360 x i8] zeroinitializer, align 1
@__compressedAssemblyData_22 = internal dso_local global [88736 x i8] zeroinitializer, align 1
@__compressedAssemblyData_23 = internal dso_local global [50976 x i8] zeroinitializer, align 1
@__compressedAssemblyData_24 = internal dso_local global [65320 x i8] zeroinitializer, align 1
@__compressedAssemblyData_25 = internal dso_local global [27928 x i8] zeroinitializer, align 1
@__compressedAssemblyData_26 = internal dso_local global [71448 x i8] zeroinitializer, align 1
@__compressedAssemblyData_27 = internal dso_local global [20248 x i8] zeroinitializer, align 1
@__compressedAssemblyData_28 = internal dso_local global [25880 x i8] zeroinitializer, align 1
@__compressedAssemblyData_29 = internal dso_local global [34592 x i8] zeroinitializer, align 1
@__compressedAssemblyData_30 = internal dso_local global [64776 x i8] zeroinitializer, align 1
@__compressedAssemblyData_31 = internal dso_local global [22688 x i8] zeroinitializer, align 1
@__compressedAssemblyData_32 = internal dso_local global [43680 x i8] zeroinitializer, align 1
@__compressedAssemblyData_33 = internal dso_local global [496640 x i8] zeroinitializer, align 1
@__compressedAssemblyData_34 = internal dso_local global [1725480 x i8] zeroinitializer, align 1
@__compressedAssemblyData_35 = internal dso_local global [120864 x i8] zeroinitializer, align 1
@__compressedAssemblyData_36 = internal dso_local global [666560 x i8] zeroinitializer, align 1
@__compressedAssemblyData_37 = internal dso_local global [245288 x i8] zeroinitializer, align 1
@__compressedAssemblyData_38 = internal dso_local global [205864 x i8] zeroinitializer, align 1
@__compressedAssemblyData_39 = internal dso_local global [712464 x i8] zeroinitializer, align 1
@__compressedAssemblyData_40 = internal dso_local global [107520 x i8] zeroinitializer, align 1
@__compressedAssemblyData_41 = internal dso_local global [5632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_42 = internal dso_local global [51200 x i8] zeroinitializer, align 1
@__compressedAssemblyData_43 = internal dso_local global [5632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_44 = internal dso_local global [36864 x i8] zeroinitializer, align 1
@__compressedAssemblyData_45 = internal dso_local global [51872 x i8] zeroinitializer, align 1
@__compressedAssemblyData_46 = internal dso_local global [1089928 x i8] zeroinitializer, align 1
@__compressedAssemblyData_47 = internal dso_local global [14728 x i8] zeroinitializer, align 1
@__compressedAssemblyData_48 = internal dso_local global [23984 x i8] zeroinitializer, align 1
@__compressedAssemblyData_49 = internal dso_local global [57736 x i8] zeroinitializer, align 1
@__compressedAssemblyData_50 = internal dso_local global [160704 x i8] zeroinitializer, align 1
@__compressedAssemblyData_51 = internal dso_local global [27528 x i8] zeroinitializer, align 1
@__compressedAssemblyData_52 = internal dso_local global [15800 x i8] zeroinitializer, align 1
@__compressedAssemblyData_53 = internal dso_local global [35208 x i8] zeroinitializer, align 1
@__compressedAssemblyData_54 = internal dso_local global [196024 x i8] zeroinitializer, align 1
@__compressedAssemblyData_55 = internal dso_local global [1105920 x i8] zeroinitializer, align 1
@__compressedAssemblyData_56 = internal dso_local global [95664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_57 = internal dso_local global [36280 x i8] zeroinitializer, align 1
@__compressedAssemblyData_58 = internal dso_local global [27056 x i8] zeroinitializer, align 1
@__compressedAssemblyData_59 = internal dso_local global [314800 x i8] zeroinitializer, align 1
@__compressedAssemblyData_60 = internal dso_local global [32648 x i8] zeroinitializer, align 1
@__compressedAssemblyData_61 = internal dso_local global [78232 x i8] zeroinitializer, align 1
@__compressedAssemblyData_62 = internal dso_local global [32688 x i8] zeroinitializer, align 1
@__compressedAssemblyData_63 = internal dso_local global [35208 x i8] zeroinitializer, align 1
@__compressedAssemblyData_64 = internal dso_local global [647104 x i8] zeroinitializer, align 1
@__compressedAssemblyData_65 = internal dso_local global [956336 x i8] zeroinitializer, align 1
@__compressedAssemblyData_66 = internal dso_local global [105408 x i8] zeroinitializer, align 1
@__compressedAssemblyData_67 = internal dso_local global [1901056 x i8] zeroinitializer, align 1
@__compressedAssemblyData_68 = internal dso_local global [215944 x i8] zeroinitializer, align 1
@__compressedAssemblyData_69 = internal dso_local global [31128 x i8] zeroinitializer, align 1
@__compressedAssemblyData_70 = internal dso_local global [59840 x i8] zeroinitializer, align 1
@__compressedAssemblyData_71 = internal dso_local global [66480 x i8] zeroinitializer, align 1
@__compressedAssemblyData_72 = internal dso_local global [24984 x i8] zeroinitializer, align 1
@__compressedAssemblyData_73 = internal dso_local global [30616 x i8] zeroinitializer, align 1
@__compressedAssemblyData_74 = internal dso_local global [62384 x i8] zeroinitializer, align 1
@__compressedAssemblyData_75 = internal dso_local global [56728 x i8] zeroinitializer, align 1
@__compressedAssemblyData_76 = internal dso_local global [261560 x i8] zeroinitializer, align 1
@__compressedAssemblyData_77 = internal dso_local global [25496 x i8] zeroinitializer, align 1
@__compressedAssemblyData_78 = internal dso_local global [69016 x i8] zeroinitializer, align 1
@__compressedAssemblyData_79 = internal dso_local global [325056 x i8] zeroinitializer, align 1
@__compressedAssemblyData_80 = internal dso_local global [27016 x i8] zeroinitializer, align 1
@__compressedAssemblyData_81 = internal dso_local global [24456 x i8] zeroinitializer, align 1
@__compressedAssemblyData_82 = internal dso_local global [20888 x i8] zeroinitializer, align 1
@__compressedAssemblyData_83 = internal dso_local global [67504 x i8] zeroinitializer, align 1
@__compressedAssemblyData_84 = internal dso_local global [28056 x i8] zeroinitializer, align 1
@__compressedAssemblyData_85 = internal dso_local global [33160 x i8] zeroinitializer, align 1
@__compressedAssemblyData_86 = internal dso_local global [19848 x i8] zeroinitializer, align 1
@__compressedAssemblyData_87 = internal dso_local global [22976 x i8] zeroinitializer, align 1
@__compressedAssemblyData_88 = internal dso_local global [31680 x i8] zeroinitializer, align 1
@__compressedAssemblyData_89 = internal dso_local global [30640 x i8] zeroinitializer, align 1
@__compressedAssemblyData_90 = internal dso_local global [67992 x i8] zeroinitializer, align 1
@__compressedAssemblyData_91 = internal dso_local global [19392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_92 = internal dso_local global [39816 x i8] zeroinitializer, align 1
@__compressedAssemblyData_93 = internal dso_local global [65472 x i8] zeroinitializer, align 1
@__compressedAssemblyData_94 = internal dso_local global [21384 x i8] zeroinitializer, align 1
@__compressedAssemblyData_95 = internal dso_local global [184768 x i8] zeroinitializer, align 1
@__compressedAssemblyData_96 = internal dso_local global [57240 x i8] zeroinitializer, align 1
@__compressedAssemblyData_97 = internal dso_local global [106904 x i8] zeroinitializer, align 1
@__compressedAssemblyData_98 = internal dso_local global [57752 x i8] zeroinitializer, align 1
@__compressedAssemblyData_99 = internal dso_local global [25024 x i8] zeroinitializer, align 1
@__compressedAssemblyData_100 = internal dso_local global [49032 x i8] zeroinitializer, align 1
@__compressedAssemblyData_101 = internal dso_local global [606616 x i8] zeroinitializer, align 1
@__compressedAssemblyData_102 = internal dso_local global [28568 x i8] zeroinitializer, align 1
@__compressedAssemblyData_103 = internal dso_local global [34184 x i8] zeroinitializer, align 1
@__compressedAssemblyData_104 = internal dso_local global [18312 x i8] zeroinitializer, align 1
@__compressedAssemblyData_105 = internal dso_local global [45960 x i8] zeroinitializer, align 1
@__compressedAssemblyData_106 = internal dso_local global [47000 x i8] zeroinitializer, align 1
@__compressedAssemblyData_107 = internal dso_local global [30656 x i8] zeroinitializer, align 1
@__compressedAssemblyData_108 = internal dso_local global [70024 x i8] zeroinitializer, align 1
@__compressedAssemblyData_109 = internal dso_local global [19848 x i8] zeroinitializer, align 1
@__compressedAssemblyData_110 = internal dso_local global [144384 x i8] zeroinitializer, align 1
@__compressedAssemblyData_111 = internal dso_local global [33688 x i8] zeroinitializer, align 1
@__compressedAssemblyData_112 = internal dso_local global [47040 x i8] zeroinitializer, align 1
@__compressedAssemblyData_113 = internal dso_local global [111040 x i8] zeroinitializer, align 1
@__compressedAssemblyData_114 = internal dso_local global [86920 x i8] zeroinitializer, align 1
@__compressedAssemblyData_115 = internal dso_local global [56768 x i8] zeroinitializer, align 1
@__compressedAssemblyData_116 = internal dso_local global [156088 x i8] zeroinitializer, align 1
@__compressedAssemblyData_117 = internal dso_local global [24472 x i8] zeroinitializer, align 1
@__compressedAssemblyData_118 = internal dso_local global [2371072 x i8] zeroinitializer, align 1
@__compressedAssemblyData_119 = internal dso_local global [91528 x i8] zeroinitializer, align 1
@__compressedAssemblyData_120 = internal dso_local global [4096512 x i8] zeroinitializer, align 1
@__compressedAssemblyData_121 = internal dso_local global [87944 x i8] zeroinitializer, align 1
@__compressedAssemblyData_122 = internal dso_local global [23488 x i8] zeroinitializer, align 1
@__compressedAssemblyData_123 = internal dso_local global [142336 x i8] zeroinitializer, align 1
@__compressedAssemblyData_124 = internal dso_local global [2214912 x i8] zeroinitializer, align 1
@__compressedAssemblyData_125 = internal dso_local global [5632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_126 = internal dso_local global [5632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_127 = internal dso_local global [5632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_128 = internal dso_local global [27072 x i8] zeroinitializer, align 1
@__compressedAssemblyData_129 = internal dso_local global [539584 x i8] zeroinitializer, align 1
@__compressedAssemblyData_130 = internal dso_local global [15408 x i8] zeroinitializer, align 1
@__compressedAssemblyData_131 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_132 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_133 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_134 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_135 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_136 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_137 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_138 = internal dso_local global [15400 x i8] zeroinitializer, align 1
@__compressedAssemblyData_139 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_140 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_141 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_142 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_143 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_144 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_145 = internal dso_local global [15408 x i8] zeroinitializer, align 1
@__compressedAssemblyData_146 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_147 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_148 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_149 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_150 = internal dso_local global [15400 x i8] zeroinitializer, align 1
@__compressedAssemblyData_151 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_152 = internal dso_local global [15408 x i8] zeroinitializer, align 1
@__compressedAssemblyData_153 = internal dso_local global [15400 x i8] zeroinitializer, align 1
@__compressedAssemblyData_154 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_155 = internal dso_local global [15384 x i8] zeroinitializer, align 1
@__compressedAssemblyData_156 = internal dso_local global [15400 x i8] zeroinitializer, align 1
@__compressedAssemblyData_157 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_158 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_159 = internal dso_local global [15400 x i8] zeroinitializer, align 1
@__compressedAssemblyData_160 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_161 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_162 = internal dso_local global [15392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_163 = internal dso_local global [15400 x i8] zeroinitializer, align 1
@__compressedAssemblyData_164 = internal dso_local global [36864 x i8] zeroinitializer, align 1
@__compressedAssemblyData_165 = internal dso_local global [703488 x i8] zeroinitializer, align 1
@__compressedAssemblyData_166 = internal dso_local global [307984 x i8] zeroinitializer, align 1
@__compressedAssemblyData_167 = internal dso_local global [430352 x i8] zeroinitializer, align 1
@__compressedAssemblyData_168 = internal dso_local global [17680 x i8] zeroinitializer, align 1
@__compressedAssemblyData_169 = internal dso_local global [15664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_170 = internal dso_local global [33592 x i8] zeroinitializer, align 1
@__compressedAssemblyData_171 = internal dso_local global [15672 x i8] zeroinitializer, align 1
@__compressedAssemblyData_172 = internal dso_local global [15632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_173 = internal dso_local global [89872 x i8] zeroinitializer, align 1
@__compressedAssemblyData_174 = internal dso_local global [255760 x i8] zeroinitializer, align 1
@__compressedAssemblyData_175 = internal dso_local global [48440 x i8] zeroinitializer, align 1
@__compressedAssemblyData_176 = internal dso_local global [48392 x i8] zeroinitializer, align 1
@__compressedAssemblyData_177 = internal dso_local global [126736 x i8] zeroinitializer, align 1
@__compressedAssemblyData_178 = internal dso_local global [102672 x i8] zeroinitializer, align 1
@__compressedAssemblyData_179 = internal dso_local global [17168 x i8] zeroinitializer, align 1
@__compressedAssemblyData_180 = internal dso_local global [26888 x i8] zeroinitializer, align 1
@__compressedAssemblyData_181 = internal dso_local global [42296 x i8] zeroinitializer, align 1
@__compressedAssemblyData_182 = internal dso_local global [315664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_183 = internal dso_local global [16656 x i8] zeroinitializer, align 1
@__compressedAssemblyData_184 = internal dso_local global [19728 x i8] zeroinitializer, align 1
@__compressedAssemblyData_185 = internal dso_local global [50992 x i8] zeroinitializer, align 1
@__compressedAssemblyData_186 = internal dso_local global [23816 x i8] zeroinitializer, align 1
@__compressedAssemblyData_187 = internal dso_local global [1016624 x i8] zeroinitializer, align 1
@__compressedAssemblyData_188 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_189 = internal dso_local global [25360 x i8] zeroinitializer, align 1
@__compressedAssemblyData_190 = internal dso_local global [16688 x i8] zeroinitializer, align 1
@__compressedAssemblyData_191 = internal dso_local global [16136 x i8] zeroinitializer, align 1
@__compressedAssemblyData_192 = internal dso_local global [184584 x i8] zeroinitializer, align 1
@__compressedAssemblyData_193 = internal dso_local global [29496 x i8] zeroinitializer, align 1
@__compressedAssemblyData_194 = internal dso_local global [127248 x i8] zeroinitializer, align 1
@__compressedAssemblyData_195 = internal dso_local global [26376 x i8] zeroinitializer, align 1
@__compressedAssemblyData_196 = internal dso_local global [32048 x i8] zeroinitializer, align 1
@__compressedAssemblyData_197 = internal dso_local global [15664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_198 = internal dso_local global [59144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_199 = internal dso_local global [16656 x i8] zeroinitializer, align 1
@__compressedAssemblyData_200 = internal dso_local global [64784 x i8] zeroinitializer, align 1
@__compressedAssemblyData_201 = internal dso_local global [20752 x i8] zeroinitializer, align 1
@__compressedAssemblyData_202 = internal dso_local global [16696 x i8] zeroinitializer, align 1
@__compressedAssemblyData_203 = internal dso_local global [96560 x i8] zeroinitializer, align 1
@__compressedAssemblyData_204 = internal dso_local global [121616 x i8] zeroinitializer, align 1
@__compressedAssemblyData_205 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_206 = internal dso_local global [15632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_207 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_208 = internal dso_local global [41232 x i8] zeroinitializer, align 1
@__compressedAssemblyData_209 = internal dso_local global [15624 x i8] zeroinitializer, align 1
@__compressedAssemblyData_210 = internal dso_local global [38160 x i8] zeroinitializer, align 1
@__compressedAssemblyData_211 = internal dso_local global [110344 x i8] zeroinitializer, align 1
@__compressedAssemblyData_212 = internal dso_local global [32568 x i8] zeroinitializer, align 1
@__compressedAssemblyData_213 = internal dso_local global [48400 x i8] zeroinitializer, align 1
@__compressedAssemblyData_214 = internal dso_local global [15632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_215 = internal dso_local global [55088 x i8] zeroinitializer, align 1
@__compressedAssemblyData_216 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_217 = internal dso_local global [43832 x i8] zeroinitializer, align 1
@__compressedAssemblyData_218 = internal dso_local global [48952 x i8] zeroinitializer, align 1
@__compressedAssemblyData_219 = internal dso_local global [78640 x i8] zeroinitializer, align 1
@__compressedAssemblyData_220 = internal dso_local global [23816 x i8] zeroinitializer, align 1
@__compressedAssemblyData_221 = internal dso_local global [67888 x i8] zeroinitializer, align 1
@__compressedAssemblyData_222 = internal dso_local global [15664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_223 = internal dso_local global [16136 x i8] zeroinitializer, align 1
@__compressedAssemblyData_224 = internal dso_local global [575752 x i8] zeroinitializer, align 1
@__compressedAssemblyData_225 = internal dso_local global [223496 x i8] zeroinitializer, align 1
@__compressedAssemblyData_226 = internal dso_local global [76552 x i8] zeroinitializer, align 1
@__compressedAssemblyData_227 = internal dso_local global [149264 x i8] zeroinitializer, align 1
@__compressedAssemblyData_228 = internal dso_local global [56120 x i8] zeroinitializer, align 1
@__compressedAssemblyData_229 = internal dso_local global [56592 x i8] zeroinitializer, align 1
@__compressedAssemblyData_230 = internal dso_local global [676664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_231 = internal dso_local global [131896 x i8] zeroinitializer, align 1
@__compressedAssemblyData_232 = internal dso_local global [174904 x i8] zeroinitializer, align 1
@__compressedAssemblyData_233 = internal dso_local global [51976 x i8] zeroinitializer, align 1
@__compressedAssemblyData_234 = internal dso_local global [66320 x i8] zeroinitializer, align 1
@__compressedAssemblyData_235 = internal dso_local global [56080 x i8] zeroinitializer, align 1
@__compressedAssemblyData_236 = internal dso_local global [107280 x i8] zeroinitializer, align 1
@__compressedAssemblyData_237 = internal dso_local global [173360 x i8] zeroinitializer, align 1
@__compressedAssemblyData_238 = internal dso_local global [162104 x i8] zeroinitializer, align 1
@__compressedAssemblyData_239 = internal dso_local global [253752 x i8] zeroinitializer, align 1
@__compressedAssemblyData_240 = internal dso_local global [15624 x i8] zeroinitializer, align 1
@__compressedAssemblyData_241 = internal dso_local global [235280 x i8] zeroinitializer, align 1
@__compressedAssemblyData_242 = internal dso_local global [70928 x i8] zeroinitializer, align 1
@__compressedAssemblyData_243 = internal dso_local global [33584 x i8] zeroinitializer, align 1
@__compressedAssemblyData_244 = internal dso_local global [23864 x i8] zeroinitializer, align 1
@__compressedAssemblyData_245 = internal dso_local global [51976 x i8] zeroinitializer, align 1
@__compressedAssemblyData_246 = internal dso_local global [103176 x i8] zeroinitializer, align 1
@__compressedAssemblyData_247 = internal dso_local global [17680 x i8] zeroinitializer, align 1
@__compressedAssemblyData_248 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_249 = internal dso_local global [15672 x i8] zeroinitializer, align 1
@__compressedAssemblyData_250 = internal dso_local global [41776 x i8] zeroinitializer, align 1
@__compressedAssemblyData_251 = internal dso_local global [852272 x i8] zeroinitializer, align 1
@__compressedAssemblyData_252 = internal dso_local global [103216 x i8] zeroinitializer, align 1
@__compressedAssemblyData_253 = internal dso_local global [153872 x i8] zeroinitializer, align 1
@__compressedAssemblyData_254 = internal dso_local global [3099920 x i8] zeroinitializer, align 1
@__compressedAssemblyData_255 = internal dso_local global [38704 x i8] zeroinitializer, align 1
@__compressedAssemblyData_256 = internal dso_local global [16136 x i8] zeroinitializer, align 1
@__compressedAssemblyData_257 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_258 = internal dso_local global [130352 x i8] zeroinitializer, align 1
@__compressedAssemblyData_259 = internal dso_local global [15632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_260 = internal dso_local global [501520 x i8] zeroinitializer, align 1
@__compressedAssemblyData_261 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_262 = internal dso_local global [24336 x i8] zeroinitializer, align 1
@__compressedAssemblyData_263 = internal dso_local global [16656 x i8] zeroinitializer, align 1
@__compressedAssemblyData_264 = internal dso_local global [15664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_265 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_266 = internal dso_local global [26896 x i8] zeroinitializer, align 1
@__compressedAssemblyData_267 = internal dso_local global [15632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_268 = internal dso_local global [17720 x i8] zeroinitializer, align 1
@__compressedAssemblyData_269 = internal dso_local global [18224 x i8] zeroinitializer, align 1
@__compressedAssemblyData_270 = internal dso_local global [15672 x i8] zeroinitializer, align 1
@__compressedAssemblyData_271 = internal dso_local global [38672 x i8] zeroinitializer, align 1
@__compressedAssemblyData_272 = internal dso_local global [15624 x i8] zeroinitializer, align 1
@__compressedAssemblyData_273 = internal dso_local global [64816 x i8] zeroinitializer, align 1
@__compressedAssemblyData_274 = internal dso_local global [17680 x i8] zeroinitializer, align 1
@__compressedAssemblyData_275 = internal dso_local global [16136 x i8] zeroinitializer, align 1
@__compressedAssemblyData_276 = internal dso_local global [143632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_277 = internal dso_local global [66360 x i8] zeroinitializer, align 1
@__compressedAssemblyData_278 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_279 = internal dso_local global [23824 x i8] zeroinitializer, align 1
@__compressedAssemblyData_280 = internal dso_local global [17168 x i8] zeroinitializer, align 1
@__compressedAssemblyData_281 = internal dso_local global [17168 x i8] zeroinitializer, align 1
@__compressedAssemblyData_282 = internal dso_local global [44816 x i8] zeroinitializer, align 1
@__compressedAssemblyData_283 = internal dso_local global [58632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_284 = internal dso_local global [54024 x i8] zeroinitializer, align 1
@__compressedAssemblyData_285 = internal dso_local global [17680 x i8] zeroinitializer, align 1
@__compressedAssemblyData_286 = internal dso_local global [16696 x i8] zeroinitializer, align 1
@__compressedAssemblyData_287 = internal dso_local global [16176 x i8] zeroinitializer, align 1
@__compressedAssemblyData_288 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_289 = internal dso_local global [15672 x i8] zeroinitializer, align 1
@__compressedAssemblyData_290 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_291 = internal dso_local global [17200 x i8] zeroinitializer, align 1
@__compressedAssemblyData_292 = internal dso_local global [705296 x i8] zeroinitializer, align 1
@__compressedAssemblyData_293 = internal dso_local global [38152 x i8] zeroinitializer, align 1
@__compressedAssemblyData_294 = internal dso_local global [15664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_295 = internal dso_local global [15632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_296 = internal dso_local global [18744 x i8] zeroinitializer, align 1
@__compressedAssemblyData_297 = internal dso_local global [17200 x i8] zeroinitializer, align 1
@__compressedAssemblyData_298 = internal dso_local global [16176 x i8] zeroinitializer, align 1
@__compressedAssemblyData_299 = internal dso_local global [741168 x i8] zeroinitializer, align 1
@__compressedAssemblyData_300 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_301 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_302 = internal dso_local global [70416 x i8] zeroinitializer, align 1
@__compressedAssemblyData_303 = internal dso_local global [617736 x i8] zeroinitializer, align 1
@__compressedAssemblyData_304 = internal dso_local global [369424 x i8] zeroinitializer, align 1
@__compressedAssemblyData_305 = internal dso_local global [57096 x i8] zeroinitializer, align 1
@__compressedAssemblyData_306 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_307 = internal dso_local global [186120 x i8] zeroinitializer, align 1
@__compressedAssemblyData_308 = internal dso_local global [16136 x i8] zeroinitializer, align 1
@__compressedAssemblyData_309 = internal dso_local global [61712 x i8] zeroinitializer, align 1
@__compressedAssemblyData_310 = internal dso_local global [17200 x i8] zeroinitializer, align 1
@__compressedAssemblyData_311 = internal dso_local global [16184 x i8] zeroinitializer, align 1
@__compressedAssemblyData_312 = internal dso_local global [16136 x i8] zeroinitializer, align 1
@__compressedAssemblyData_313 = internal dso_local global [15632 x i8] zeroinitializer, align 1
@__compressedAssemblyData_314 = internal dso_local global [45360 x i8] zeroinitializer, align 1
@__compressedAssemblyData_315 = internal dso_local global [175928 x i8] zeroinitializer, align 1
@__compressedAssemblyData_316 = internal dso_local global [16696 x i8] zeroinitializer, align 1
@__compressedAssemblyData_317 = internal dso_local global [15664 x i8] zeroinitializer, align 1
@__compressedAssemblyData_318 = internal dso_local global [30992 x i8] zeroinitializer, align 1
@__compressedAssemblyData_319 = internal dso_local global [15624 x i8] zeroinitializer, align 1
@__compressedAssemblyData_320 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_321 = internal dso_local global [16184 x i8] zeroinitializer, align 1
@__compressedAssemblyData_322 = internal dso_local global [22328 x i8] zeroinitializer, align 1
@__compressedAssemblyData_323 = internal dso_local global [16696 x i8] zeroinitializer, align 1
@__compressedAssemblyData_324 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_325 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_326 = internal dso_local global [16144 x i8] zeroinitializer, align 1
@__compressedAssemblyData_327 = internal dso_local global [16136 x i8] zeroinitializer, align 1
@__compressedAssemblyData_328 = internal dso_local global [18192 x i8] zeroinitializer, align 1
@__compressedAssemblyData_329 = internal dso_local global [23824 x i8] zeroinitializer, align 1
@__compressedAssemblyData_330 = internal dso_local global [50960 x i8] zeroinitializer, align 1
@__compressedAssemblyData_331 = internal dso_local global [16656 x i8] zeroinitializer, align 1
@__compressedAssemblyData_332 = internal dso_local global [60176 x i8] zeroinitializer, align 1
@__compressedAssemblyData_333 = internal dso_local global [101136 x i8] zeroinitializer, align 1
@__compressedAssemblyData_334 = internal dso_local global [240160 x i8] zeroinitializer, align 1
@__compressedAssemblyData_335 = internal dso_local global [82968 x i8] zeroinitializer, align 1
@__compressedAssemblyData_336 = internal dso_local global [19512 x i8] zeroinitializer, align 1
@__compressedAssemblyData_337 = internal dso_local global [37450296 x i8] zeroinitializer, align 1
@__compressedAssemblyData_338 = internal dso_local global [4804920 x i8] zeroinitializer, align 1

; Metadata
!llvm.module.flags = !{!0, !1, !7, !8, !9, !10}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!".NET for Android remotes/origin/release/9.0.1xx @ 1dcfb6f8779c33b6f768c996495cb90ecd729329"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"branch-target-enforcement", i32 0}
!8 = !{i32 1, !"sign-return-address", i32 0}
!9 = !{i32 1, !"sign-return-address-all", i32 0}
!10 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
