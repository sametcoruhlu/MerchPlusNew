; ModuleID = 'obj\Release\90\android\typemaps.arm64-v8a.ll'
source_filename = "obj\Release\90\android\typemaps.arm64-v8a.ll"
target datalayout = "e-m:e-i8:8:32-i16:16:32-i64:64-i128:128-n32:64-S128"
target triple = "aarch64-unknown-linux-android"


%struct.MonoImage = type opaque

%struct.TypeMapJava = type {
	i32,; uint32_t module_index
	i32,; uint32_t type_token_id
	i32; uint32_t java_name_index
}

%struct.TypeMapModule = type {
	[16 x i8],; uint8_t module_uuid[16]
	i32,; uint32_t entry_count
	i32,; uint32_t duplicate_count
	%struct.TypeMapModuleEntry*,; TypeMapModuleEntry* map
	%struct.TypeMapModuleEntry*,; TypeMapModuleEntry* duplicate_map
	i8*,; char* assembly_name
	%struct.MonoImage*,; MonoImage* image
	i32,; uint32_t java_name_width
	i8*; uint8_t* java_map
}

%struct.TypeMapModuleEntry = type {
	i32,; uint32_t type_token_id
	i32; uint32_t java_map_index
}

@map_module_count = local_unnamed_addr constant i32 3, align 4

@java_type_count = local_unnamed_addr constant i32 165, align 4

; Map modules data

; module0_managed_to_java
@module0_managed_to_java = internal constant [11 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554441, ; type_token_id
		i32 162; java_map_index
	}, 
	; 1
	%struct.TypeMapModuleEntry {
		i32 33554442, ; type_token_id
		i32 101; java_map_index
	}, 
	; 2
	%struct.TypeMapModuleEntry {
		i32 33554443, ; type_token_id
		i32 55; java_map_index
	}, 
	; 3
	%struct.TypeMapModuleEntry {
		i32 33554445, ; type_token_id
		i32 73; java_map_index
	}, 
	; 4
	%struct.TypeMapModuleEntry {
		i32 33554447, ; type_token_id
		i32 8; java_map_index
	}, 
	; 5
	%struct.TypeMapModuleEntry {
		i32 33554449, ; type_token_id
		i32 38; java_map_index
	}, 
	; 6
	%struct.TypeMapModuleEntry {
		i32 33554451, ; type_token_id
		i32 49; java_map_index
	}, 
	; 7
	%struct.TypeMapModuleEntry {
		i32 33554453, ; type_token_id
		i32 35; java_map_index
	}, 
	; 8
	%struct.TypeMapModuleEntry {
		i32 33554455, ; type_token_id
		i32 17; java_map_index
	}, 
	; 9
	%struct.TypeMapModuleEntry {
		i32 33554457, ; type_token_id
		i32 84; java_map_index
	}, 
	; 10
	%struct.TypeMapModuleEntry {
		i32 33554460, ; type_token_id
		i32 113; java_map_index
	}
], align 4; end of 'module0_managed_to_java' array


; module0_managed_to_java_duplicates
@module0_managed_to_java_duplicates = internal constant [9 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554444, ; type_token_id
		i32 55; java_map_index
	}, 
	; 1
	%struct.TypeMapModuleEntry {
		i32 33554446, ; type_token_id
		i32 73; java_map_index
	}, 
	; 2
	%struct.TypeMapModuleEntry {
		i32 33554448, ; type_token_id
		i32 8; java_map_index
	}, 
	; 3
	%struct.TypeMapModuleEntry {
		i32 33554450, ; type_token_id
		i32 38; java_map_index
	}, 
	; 4
	%struct.TypeMapModuleEntry {
		i32 33554452, ; type_token_id
		i32 49; java_map_index
	}, 
	; 5
	%struct.TypeMapModuleEntry {
		i32 33554454, ; type_token_id
		i32 35; java_map_index
	}, 
	; 6
	%struct.TypeMapModuleEntry {
		i32 33554456, ; type_token_id
		i32 17; java_map_index
	}, 
	; 7
	%struct.TypeMapModuleEntry {
		i32 33554458, ; type_token_id
		i32 84; java_map_index
	}, 
	; 8
	%struct.TypeMapModuleEntry {
		i32 33554459, ; type_token_id
		i32 162; java_map_index
	}
], align 4; end of 'module0_managed_to_java_duplicates' array


; module1_managed_to_java
@module1_managed_to_java = internal constant [1 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554434, ; type_token_id
		i32 41; java_map_index
	}
], align 4; end of 'module1_managed_to_java' array


; module2_managed_to_java
@module2_managed_to_java = internal constant [153 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554523, ; type_token_id
		i32 142; java_map_index
	}, 
	; 1
	%struct.TypeMapModuleEntry {
		i32 33554525, ; type_token_id
		i32 87; java_map_index
	}, 
	; 2
	%struct.TypeMapModuleEntry {
		i32 33554527, ; type_token_id
		i32 40; java_map_index
	}, 
	; 3
	%struct.TypeMapModuleEntry {
		i32 33554537, ; type_token_id
		i32 5; java_map_index
	}, 
	; 4
	%struct.TypeMapModuleEntry {
		i32 33554538, ; type_token_id
		i32 3; java_map_index
	}, 
	; 5
	%struct.TypeMapModuleEntry {
		i32 33554539, ; type_token_id
		i32 75; java_map_index
	}, 
	; 6
	%struct.TypeMapModuleEntry {
		i32 33554541, ; type_token_id
		i32 99; java_map_index
	}, 
	; 7
	%struct.TypeMapModuleEntry {
		i32 33554542, ; type_token_id
		i32 123; java_map_index
	}, 
	; 8
	%struct.TypeMapModuleEntry {
		i32 33554544, ; type_token_id
		i32 134; java_map_index
	}, 
	; 9
	%struct.TypeMapModuleEntry {
		i32 33554545, ; type_token_id
		i32 68; java_map_index
	}, 
	; 10
	%struct.TypeMapModuleEntry {
		i32 33554550, ; type_token_id
		i32 155; java_map_index
	}, 
	; 11
	%struct.TypeMapModuleEntry {
		i32 33554551, ; type_token_id
		i32 111; java_map_index
	}, 
	; 12
	%struct.TypeMapModuleEntry {
		i32 33554553, ; type_token_id
		i32 72; java_map_index
	}, 
	; 13
	%struct.TypeMapModuleEntry {
		i32 33554554, ; type_token_id
		i32 43; java_map_index
	}, 
	; 14
	%struct.TypeMapModuleEntry {
		i32 33554556, ; type_token_id
		i32 70; java_map_index
	}, 
	; 15
	%struct.TypeMapModuleEntry {
		i32 33554558, ; type_token_id
		i32 7; java_map_index
	}, 
	; 16
	%struct.TypeMapModuleEntry {
		i32 33554559, ; type_token_id
		i32 144; java_map_index
	}, 
	; 17
	%struct.TypeMapModuleEntry {
		i32 33554560, ; type_token_id
		i32 13; java_map_index
	}, 
	; 18
	%struct.TypeMapModuleEntry {
		i32 33554562, ; type_token_id
		i32 26; java_map_index
	}, 
	; 19
	%struct.TypeMapModuleEntry {
		i32 33554563, ; type_token_id
		i32 54; java_map_index
	}, 
	; 20
	%struct.TypeMapModuleEntry {
		i32 33554565, ; type_token_id
		i32 53; java_map_index
	}, 
	; 21
	%struct.TypeMapModuleEntry {
		i32 33554566, ; type_token_id
		i32 42; java_map_index
	}, 
	; 22
	%struct.TypeMapModuleEntry {
		i32 33554570, ; type_token_id
		i32 6; java_map_index
	}, 
	; 23
	%struct.TypeMapModuleEntry {
		i32 33554572, ; type_token_id
		i32 145; java_map_index
	}, 
	; 24
	%struct.TypeMapModuleEntry {
		i32 33554573, ; type_token_id
		i32 159; java_map_index
	}, 
	; 25
	%struct.TypeMapModuleEntry {
		i32 33554575, ; type_token_id
		i32 62; java_map_index
	}, 
	; 26
	%struct.TypeMapModuleEntry {
		i32 33554577, ; type_token_id
		i32 45; java_map_index
	}, 
	; 27
	%struct.TypeMapModuleEntry {
		i32 33554579, ; type_token_id
		i32 44; java_map_index
	}, 
	; 28
	%struct.TypeMapModuleEntry {
		i32 33554581, ; type_token_id
		i32 67; java_map_index
	}, 
	; 29
	%struct.TypeMapModuleEntry {
		i32 33554583, ; type_token_id
		i32 33; java_map_index
	}, 
	; 30
	%struct.TypeMapModuleEntry {
		i32 33554585, ; type_token_id
		i32 29; java_map_index
	}, 
	; 31
	%struct.TypeMapModuleEntry {
		i32 33554587, ; type_token_id
		i32 23; java_map_index
	}, 
	; 32
	%struct.TypeMapModuleEntry {
		i32 33554589, ; type_token_id
		i32 25; java_map_index
	}, 
	; 33
	%struct.TypeMapModuleEntry {
		i32 33554591, ; type_token_id
		i32 115; java_map_index
	}, 
	; 34
	%struct.TypeMapModuleEntry {
		i32 33554593, ; type_token_id
		i32 130; java_map_index
	}, 
	; 35
	%struct.TypeMapModuleEntry {
		i32 33554595, ; type_token_id
		i32 59; java_map_index
	}, 
	; 36
	%struct.TypeMapModuleEntry {
		i32 33554596, ; type_token_id
		i32 88; java_map_index
	}, 
	; 37
	%struct.TypeMapModuleEntry {
		i32 33554604, ; type_token_id
		i32 148; java_map_index
	}, 
	; 38
	%struct.TypeMapModuleEntry {
		i32 33554608, ; type_token_id
		i32 153; java_map_index
	}, 
	; 39
	%struct.TypeMapModuleEntry {
		i32 33554609, ; type_token_id
		i32 86; java_map_index
	}, 
	; 40
	%struct.TypeMapModuleEntry {
		i32 33554612, ; type_token_id
		i32 76; java_map_index
	}, 
	; 41
	%struct.TypeMapModuleEntry {
		i32 33554613, ; type_token_id
		i32 52; java_map_index
	}, 
	; 42
	%struct.TypeMapModuleEntry {
		i32 33554617, ; type_token_id
		i32 110; java_map_index
	}, 
	; 43
	%struct.TypeMapModuleEntry {
		i32 33554619, ; type_token_id
		i32 137; java_map_index
	}, 
	; 44
	%struct.TypeMapModuleEntry {
		i32 33554620, ; type_token_id
		i32 119; java_map_index
	}, 
	; 45
	%struct.TypeMapModuleEntry {
		i32 33554621, ; type_token_id
		i32 122; java_map_index
	}, 
	; 46
	%struct.TypeMapModuleEntry {
		i32 33554623, ; type_token_id
		i32 92; java_map_index
	}, 
	; 47
	%struct.TypeMapModuleEntry {
		i32 33554624, ; type_token_id
		i32 19; java_map_index
	}, 
	; 48
	%struct.TypeMapModuleEntry {
		i32 33554625, ; type_token_id
		i32 4; java_map_index
	}, 
	; 49
	%struct.TypeMapModuleEntry {
		i32 33554626, ; type_token_id
		i32 56; java_map_index
	}, 
	; 50
	%struct.TypeMapModuleEntry {
		i32 33554628, ; type_token_id
		i32 105; java_map_index
	}, 
	; 51
	%struct.TypeMapModuleEntry {
		i32 33554629, ; type_token_id
		i32 131; java_map_index
	}, 
	; 52
	%struct.TypeMapModuleEntry {
		i32 33554631, ; type_token_id
		i32 48; java_map_index
	}, 
	; 53
	%struct.TypeMapModuleEntry {
		i32 33554634, ; type_token_id
		i32 147; java_map_index
	}, 
	; 54
	%struct.TypeMapModuleEntry {
		i32 33554635, ; type_token_id
		i32 50; java_map_index
	}, 
	; 55
	%struct.TypeMapModuleEntry {
		i32 33554637, ; type_token_id
		i32 28; java_map_index
	}, 
	; 56
	%struct.TypeMapModuleEntry {
		i32 33554640, ; type_token_id
		i32 114; java_map_index
	}, 
	; 57
	%struct.TypeMapModuleEntry {
		i32 33554641, ; type_token_id
		i32 117; java_map_index
	}, 
	; 58
	%struct.TypeMapModuleEntry {
		i32 33554642, ; type_token_id
		i32 74; java_map_index
	}, 
	; 59
	%struct.TypeMapModuleEntry {
		i32 33554643, ; type_token_id
		i32 138; java_map_index
	}, 
	; 60
	%struct.TypeMapModuleEntry {
		i32 33554647, ; type_token_id
		i32 79; java_map_index
	}, 
	; 61
	%struct.TypeMapModuleEntry {
		i32 33554648, ; type_token_id
		i32 149; java_map_index
	}, 
	; 62
	%struct.TypeMapModuleEntry {
		i32 33554653, ; type_token_id
		i32 100; java_map_index
	}, 
	; 63
	%struct.TypeMapModuleEntry {
		i32 33554654, ; type_token_id
		i32 27; java_map_index
	}, 
	; 64
	%struct.TypeMapModuleEntry {
		i32 33554655, ; type_token_id
		i32 112; java_map_index
	}, 
	; 65
	%struct.TypeMapModuleEntry {
		i32 33554657, ; type_token_id
		i32 65; java_map_index
	}, 
	; 66
	%struct.TypeMapModuleEntry {
		i32 33554658, ; type_token_id
		i32 103; java_map_index
	}, 
	; 67
	%struct.TypeMapModuleEntry {
		i32 33554660, ; type_token_id
		i32 18; java_map_index
	}, 
	; 68
	%struct.TypeMapModuleEntry {
		i32 33554663, ; type_token_id
		i32 69; java_map_index
	}, 
	; 69
	%struct.TypeMapModuleEntry {
		i32 33554685, ; type_token_id
		i32 152; java_map_index
	}, 
	; 70
	%struct.TypeMapModuleEntry {
		i32 33554687, ; type_token_id
		i32 95; java_map_index
	}, 
	; 71
	%struct.TypeMapModuleEntry {
		i32 33554689, ; type_token_id
		i32 164; java_map_index
	}, 
	; 72
	%struct.TypeMapModuleEntry {
		i32 33554691, ; type_token_id
		i32 61; java_map_index
	}, 
	; 73
	%struct.TypeMapModuleEntry {
		i32 33554700, ; type_token_id
		i32 96; java_map_index
	}, 
	; 74
	%struct.TypeMapModuleEntry {
		i32 33554702, ; type_token_id
		i32 151; java_map_index
	}, 
	; 75
	%struct.TypeMapModuleEntry {
		i32 33554704, ; type_token_id
		i32 15; java_map_index
	}, 
	; 76
	%struct.TypeMapModuleEntry {
		i32 33554705, ; type_token_id
		i32 12; java_map_index
	}, 
	; 77
	%struct.TypeMapModuleEntry {
		i32 33554721, ; type_token_id
		i32 128; java_map_index
	}, 
	; 78
	%struct.TypeMapModuleEntry {
		i32 33554731, ; type_token_id
		i32 32; java_map_index
	}, 
	; 79
	%struct.TypeMapModuleEntry {
		i32 33554732, ; type_token_id
		i32 22; java_map_index
	}, 
	; 80
	%struct.TypeMapModuleEntry {
		i32 33554733, ; type_token_id
		i32 39; java_map_index
	}, 
	; 81
	%struct.TypeMapModuleEntry {
		i32 33554735, ; type_token_id
		i32 34; java_map_index
	}, 
	; 82
	%struct.TypeMapModuleEntry {
		i32 33554737, ; type_token_id
		i32 2; java_map_index
	}, 
	; 83
	%struct.TypeMapModuleEntry {
		i32 33554738, ; type_token_id
		i32 16; java_map_index
	}, 
	; 84
	%struct.TypeMapModuleEntry {
		i32 33554740, ; type_token_id
		i32 1; java_map_index
	}, 
	; 85
	%struct.TypeMapModuleEntry {
		i32 33554742, ; type_token_id
		i32 158; java_map_index
	}, 
	; 86
	%struct.TypeMapModuleEntry {
		i32 33554743, ; type_token_id
		i32 141; java_map_index
	}, 
	; 87
	%struct.TypeMapModuleEntry {
		i32 33554745, ; type_token_id
		i32 37; java_map_index
	}, 
	; 88
	%struct.TypeMapModuleEntry {
		i32 33554747, ; type_token_id
		i32 97; java_map_index
	}, 
	; 89
	%struct.TypeMapModuleEntry {
		i32 33554749, ; type_token_id
		i32 51; java_map_index
	}, 
	; 90
	%struct.TypeMapModuleEntry {
		i32 33554750, ; type_token_id
		i32 108; java_map_index
	}, 
	; 91
	%struct.TypeMapModuleEntry {
		i32 33554752, ; type_token_id
		i32 118; java_map_index
	}, 
	; 92
	%struct.TypeMapModuleEntry {
		i32 33554754, ; type_token_id
		i32 160; java_map_index
	}, 
	; 93
	%struct.TypeMapModuleEntry {
		i32 33554756, ; type_token_id
		i32 146; java_map_index
	}, 
	; 94
	%struct.TypeMapModuleEntry {
		i32 33554758, ; type_token_id
		i32 125; java_map_index
	}, 
	; 95
	%struct.TypeMapModuleEntry {
		i32 33554760, ; type_token_id
		i32 143; java_map_index
	}, 
	; 96
	%struct.TypeMapModuleEntry {
		i32 33554762, ; type_token_id
		i32 30; java_map_index
	}, 
	; 97
	%struct.TypeMapModuleEntry {
		i32 33554764, ; type_token_id
		i32 21; java_map_index
	}, 
	; 98
	%struct.TypeMapModuleEntry {
		i32 33554766, ; type_token_id
		i32 129; java_map_index
	}, 
	; 99
	%struct.TypeMapModuleEntry {
		i32 33554768, ; type_token_id
		i32 83; java_map_index
	}, 
	; 100
	%struct.TypeMapModuleEntry {
		i32 33554770, ; type_token_id
		i32 10; java_map_index
	}, 
	; 101
	%struct.TypeMapModuleEntry {
		i32 33554772, ; type_token_id
		i32 98; java_map_index
	}, 
	; 102
	%struct.TypeMapModuleEntry {
		i32 33554774, ; type_token_id
		i32 93; java_map_index
	}, 
	; 103
	%struct.TypeMapModuleEntry {
		i32 33554776, ; type_token_id
		i32 63; java_map_index
	}, 
	; 104
	%struct.TypeMapModuleEntry {
		i32 33554778, ; type_token_id
		i32 71; java_map_index
	}, 
	; 105
	%struct.TypeMapModuleEntry {
		i32 33554779, ; type_token_id
		i32 132; java_map_index
	}, 
	; 106
	%struct.TypeMapModuleEntry {
		i32 33554780, ; type_token_id
		i32 60; java_map_index
	}, 
	; 107
	%struct.TypeMapModuleEntry {
		i32 33554781, ; type_token_id
		i32 36; java_map_index
	}, 
	; 108
	%struct.TypeMapModuleEntry {
		i32 33554782, ; type_token_id
		i32 102; java_map_index
	}, 
	; 109
	%struct.TypeMapModuleEntry {
		i32 33554783, ; type_token_id
		i32 156; java_map_index
	}, 
	; 110
	%struct.TypeMapModuleEntry {
		i32 33554784, ; type_token_id
		i32 11; java_map_index
	}, 
	; 111
	%struct.TypeMapModuleEntry {
		i32 33554785, ; type_token_id
		i32 163; java_map_index
	}, 
	; 112
	%struct.TypeMapModuleEntry {
		i32 33554786, ; type_token_id
		i32 135; java_map_index
	}, 
	; 113
	%struct.TypeMapModuleEntry {
		i32 33554787, ; type_token_id
		i32 109; java_map_index
	}, 
	; 114
	%struct.TypeMapModuleEntry {
		i32 33554788, ; type_token_id
		i32 85; java_map_index
	}, 
	; 115
	%struct.TypeMapModuleEntry {
		i32 33554789, ; type_token_id
		i32 126; java_map_index
	}, 
	; 116
	%struct.TypeMapModuleEntry {
		i32 33554790, ; type_token_id
		i32 121; java_map_index
	}, 
	; 117
	%struct.TypeMapModuleEntry {
		i32 33554791, ; type_token_id
		i32 90; java_map_index
	}, 
	; 118
	%struct.TypeMapModuleEntry {
		i32 33554792, ; type_token_id
		i32 0; java_map_index
	}, 
	; 119
	%struct.TypeMapModuleEntry {
		i32 33554794, ; type_token_id
		i32 77; java_map_index
	}, 
	; 120
	%struct.TypeMapModuleEntry {
		i32 33554795, ; type_token_id
		i32 94; java_map_index
	}, 
	; 121
	%struct.TypeMapModuleEntry {
		i32 33554796, ; type_token_id
		i32 136; java_map_index
	}, 
	; 122
	%struct.TypeMapModuleEntry {
		i32 33554797, ; type_token_id
		i32 127; java_map_index
	}, 
	; 123
	%struct.TypeMapModuleEntry {
		i32 33554798, ; type_token_id
		i32 47; java_map_index
	}, 
	; 124
	%struct.TypeMapModuleEntry {
		i32 33554799, ; type_token_id
		i32 140; java_map_index
	}, 
	; 125
	%struct.TypeMapModuleEntry {
		i32 33554803, ; type_token_id
		i32 133; java_map_index
	}, 
	; 126
	%struct.TypeMapModuleEntry {
		i32 33554805, ; type_token_id
		i32 91; java_map_index
	}, 
	; 127
	%struct.TypeMapModuleEntry {
		i32 33554807, ; type_token_id
		i32 64; java_map_index
	}, 
	; 128
	%struct.TypeMapModuleEntry {
		i32 33554808, ; type_token_id
		i32 106; java_map_index
	}, 
	; 129
	%struct.TypeMapModuleEntry {
		i32 33554809, ; type_token_id
		i32 104; java_map_index
	}, 
	; 130
	%struct.TypeMapModuleEntry {
		i32 33554810, ; type_token_id
		i32 66; java_map_index
	}, 
	; 131
	%struct.TypeMapModuleEntry {
		i32 33554812, ; type_token_id
		i32 81; java_map_index
	}, 
	; 132
	%struct.TypeMapModuleEntry {
		i32 33554813, ; type_token_id
		i32 78; java_map_index
	}, 
	; 133
	%struct.TypeMapModuleEntry {
		i32 33554814, ; type_token_id
		i32 82; java_map_index
	}, 
	; 134
	%struct.TypeMapModuleEntry {
		i32 33554815, ; type_token_id
		i32 107; java_map_index
	}, 
	; 135
	%struct.TypeMapModuleEntry {
		i32 33554817, ; type_token_id
		i32 157; java_map_index
	}, 
	; 136
	%struct.TypeMapModuleEntry {
		i32 33554818, ; type_token_id
		i32 89; java_map_index
	}, 
	; 137
	%struct.TypeMapModuleEntry {
		i32 33554819, ; type_token_id
		i32 46; java_map_index
	}, 
	; 138
	%struct.TypeMapModuleEntry {
		i32 33554821, ; type_token_id
		i32 124; java_map_index
	}, 
	; 139
	%struct.TypeMapModuleEntry {
		i32 33554823, ; type_token_id
		i32 58; java_map_index
	}, 
	; 140
	%struct.TypeMapModuleEntry {
		i32 33554825, ; type_token_id
		i32 9; java_map_index
	}, 
	; 141
	%struct.TypeMapModuleEntry {
		i32 33554827, ; type_token_id
		i32 150; java_map_index
	}, 
	; 142
	%struct.TypeMapModuleEntry {
		i32 33554829, ; type_token_id
		i32 14; java_map_index
	}, 
	; 143
	%struct.TypeMapModuleEntry {
		i32 33554830, ; type_token_id
		i32 161; java_map_index
	}, 
	; 144
	%struct.TypeMapModuleEntry {
		i32 33554832, ; type_token_id
		i32 57; java_map_index
	}, 
	; 145
	%struct.TypeMapModuleEntry {
		i32 33554834, ; type_token_id
		i32 24; java_map_index
	}, 
	; 146
	%struct.TypeMapModuleEntry {
		i32 33554836, ; type_token_id
		i32 20; java_map_index
	}, 
	; 147
	%struct.TypeMapModuleEntry {
		i32 33554837, ; type_token_id
		i32 80; java_map_index
	}, 
	; 148
	%struct.TypeMapModuleEntry {
		i32 33554839, ; type_token_id
		i32 154; java_map_index
	}, 
	; 149
	%struct.TypeMapModuleEntry {
		i32 33554841, ; type_token_id
		i32 31; java_map_index
	}, 
	; 150
	%struct.TypeMapModuleEntry {
		i32 33554842, ; type_token_id
		i32 120; java_map_index
	}, 
	; 151
	%struct.TypeMapModuleEntry {
		i32 33554843, ; type_token_id
		i32 139; java_map_index
	}, 
	; 152
	%struct.TypeMapModuleEntry {
		i32 33554866, ; type_token_id
		i32 116; java_map_index
	}
], align 4; end of 'module2_managed_to_java' array


; module2_managed_to_java_duplicates
@module2_managed_to_java_duplicates = internal constant [77 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554524, ; type_token_id
		i32 142; java_map_index
	}, 
	; 1
	%struct.TypeMapModuleEntry {
		i32 33554526, ; type_token_id
		i32 87; java_map_index
	}, 
	; 2
	%struct.TypeMapModuleEntry {
		i32 33554543, ; type_token_id
		i32 123; java_map_index
	}, 
	; 3
	%struct.TypeMapModuleEntry {
		i32 33554546, ; type_token_id
		i32 68; java_map_index
	}, 
	; 4
	%struct.TypeMapModuleEntry {
		i32 33554552, ; type_token_id
		i32 111; java_map_index
	}, 
	; 5
	%struct.TypeMapModuleEntry {
		i32 33554555, ; type_token_id
		i32 43; java_map_index
	}, 
	; 6
	%struct.TypeMapModuleEntry {
		i32 33554557, ; type_token_id
		i32 70; java_map_index
	}, 
	; 7
	%struct.TypeMapModuleEntry {
		i32 33554561, ; type_token_id
		i32 13; java_map_index
	}, 
	; 8
	%struct.TypeMapModuleEntry {
		i32 33554564, ; type_token_id
		i32 54; java_map_index
	}, 
	; 9
	%struct.TypeMapModuleEntry {
		i32 33554567, ; type_token_id
		i32 42; java_map_index
	}, 
	; 10
	%struct.TypeMapModuleEntry {
		i32 33554568, ; type_token_id
		i32 53; java_map_index
	}, 
	; 11
	%struct.TypeMapModuleEntry {
		i32 33554571, ; type_token_id
		i32 6; java_map_index
	}, 
	; 12
	%struct.TypeMapModuleEntry {
		i32 33554576, ; type_token_id
		i32 62; java_map_index
	}, 
	; 13
	%struct.TypeMapModuleEntry {
		i32 33554578, ; type_token_id
		i32 45; java_map_index
	}, 
	; 14
	%struct.TypeMapModuleEntry {
		i32 33554580, ; type_token_id
		i32 44; java_map_index
	}, 
	; 15
	%struct.TypeMapModuleEntry {
		i32 33554582, ; type_token_id
		i32 67; java_map_index
	}, 
	; 16
	%struct.TypeMapModuleEntry {
		i32 33554584, ; type_token_id
		i32 33; java_map_index
	}, 
	; 17
	%struct.TypeMapModuleEntry {
		i32 33554586, ; type_token_id
		i32 29; java_map_index
	}, 
	; 18
	%struct.TypeMapModuleEntry {
		i32 33554588, ; type_token_id
		i32 23; java_map_index
	}, 
	; 19
	%struct.TypeMapModuleEntry {
		i32 33554590, ; type_token_id
		i32 25; java_map_index
	}, 
	; 20
	%struct.TypeMapModuleEntry {
		i32 33554592, ; type_token_id
		i32 115; java_map_index
	}, 
	; 21
	%struct.TypeMapModuleEntry {
		i32 33554594, ; type_token_id
		i32 130; java_map_index
	}, 
	; 22
	%struct.TypeMapModuleEntry {
		i32 33554597, ; type_token_id
		i32 88; java_map_index
	}, 
	; 23
	%struct.TypeMapModuleEntry {
		i32 33554600, ; type_token_id
		i32 72; java_map_index
	}, 
	; 24
	%struct.TypeMapModuleEntry {
		i32 33554610, ; type_token_id
		i32 153; java_map_index
	}, 
	; 25
	%struct.TypeMapModuleEntry {
		i32 33554611, ; type_token_id
		i32 26; java_map_index
	}, 
	; 26
	%struct.TypeMapModuleEntry {
		i32 33554618, ; type_token_id
		i32 110; java_map_index
	}, 
	; 27
	%struct.TypeMapModuleEntry {
		i32 33554622, ; type_token_id
		i32 122; java_map_index
	}, 
	; 28
	%struct.TypeMapModuleEntry {
		i32 33554627, ; type_token_id
		i32 56; java_map_index
	}, 
	; 29
	%struct.TypeMapModuleEntry {
		i32 33554636, ; type_token_id
		i32 50; java_map_index
	}, 
	; 30
	%struct.TypeMapModuleEntry {
		i32 33554644, ; type_token_id
		i32 138; java_map_index
	}, 
	; 31
	%struct.TypeMapModuleEntry {
		i32 33554645, ; type_token_id
		i32 74; java_map_index
	}, 
	; 32
	%struct.TypeMapModuleEntry {
		i32 33554656, ; type_token_id
		i32 100; java_map_index
	}, 
	; 33
	%struct.TypeMapModuleEntry {
		i32 33554659, ; type_token_id
		i32 103; java_map_index
	}, 
	; 34
	%struct.TypeMapModuleEntry {
		i32 33554661, ; type_token_id
		i32 18; java_map_index
	}, 
	; 35
	%struct.TypeMapModuleEntry {
		i32 33554690, ; type_token_id
		i32 164; java_map_index
	}, 
	; 36
	%struct.TypeMapModuleEntry {
		i32 33554696, ; type_token_id
		i32 61; java_map_index
	}, 
	; 37
	%struct.TypeMapModuleEntry {
		i32 33554701, ; type_token_id
		i32 96; java_map_index
	}, 
	; 38
	%struct.TypeMapModuleEntry {
		i32 33554706, ; type_token_id
		i32 12; java_map_index
	}, 
	; 39
	%struct.TypeMapModuleEntry {
		i32 33554734, ; type_token_id
		i32 39; java_map_index
	}, 
	; 40
	%struct.TypeMapModuleEntry {
		i32 33554736, ; type_token_id
		i32 34; java_map_index
	}, 
	; 41
	%struct.TypeMapModuleEntry {
		i32 33554739, ; type_token_id
		i32 16; java_map_index
	}, 
	; 42
	%struct.TypeMapModuleEntry {
		i32 33554741, ; type_token_id
		i32 1; java_map_index
	}, 
	; 43
	%struct.TypeMapModuleEntry {
		i32 33554744, ; type_token_id
		i32 141; java_map_index
	}, 
	; 44
	%struct.TypeMapModuleEntry {
		i32 33554746, ; type_token_id
		i32 37; java_map_index
	}, 
	; 45
	%struct.TypeMapModuleEntry {
		i32 33554748, ; type_token_id
		i32 97; java_map_index
	}, 
	; 46
	%struct.TypeMapModuleEntry {
		i32 33554751, ; type_token_id
		i32 108; java_map_index
	}, 
	; 47
	%struct.TypeMapModuleEntry {
		i32 33554753, ; type_token_id
		i32 118; java_map_index
	}, 
	; 48
	%struct.TypeMapModuleEntry {
		i32 33554755, ; type_token_id
		i32 160; java_map_index
	}, 
	; 49
	%struct.TypeMapModuleEntry {
		i32 33554757, ; type_token_id
		i32 146; java_map_index
	}, 
	; 50
	%struct.TypeMapModuleEntry {
		i32 33554759, ; type_token_id
		i32 125; java_map_index
	}, 
	; 51
	%struct.TypeMapModuleEntry {
		i32 33554761, ; type_token_id
		i32 143; java_map_index
	}, 
	; 52
	%struct.TypeMapModuleEntry {
		i32 33554763, ; type_token_id
		i32 30; java_map_index
	}, 
	; 53
	%struct.TypeMapModuleEntry {
		i32 33554765, ; type_token_id
		i32 21; java_map_index
	}, 
	; 54
	%struct.TypeMapModuleEntry {
		i32 33554767, ; type_token_id
		i32 129; java_map_index
	}, 
	; 55
	%struct.TypeMapModuleEntry {
		i32 33554769, ; type_token_id
		i32 83; java_map_index
	}, 
	; 56
	%struct.TypeMapModuleEntry {
		i32 33554771, ; type_token_id
		i32 10; java_map_index
	}, 
	; 57
	%struct.TypeMapModuleEntry {
		i32 33554773, ; type_token_id
		i32 98; java_map_index
	}, 
	; 58
	%struct.TypeMapModuleEntry {
		i32 33554775, ; type_token_id
		i32 93; java_map_index
	}, 
	; 59
	%struct.TypeMapModuleEntry {
		i32 33554777, ; type_token_id
		i32 63; java_map_index
	}, 
	; 60
	%struct.TypeMapModuleEntry {
		i32 33554800, ; type_token_id
		i32 140; java_map_index
	}, 
	; 61
	%struct.TypeMapModuleEntry {
		i32 33554801, ; type_token_id
		i32 135; java_map_index
	}, 
	; 62
	%struct.TypeMapModuleEntry {
		i32 33554804, ; type_token_id
		i32 133; java_map_index
	}, 
	; 63
	%struct.TypeMapModuleEntry {
		i32 33554806, ; type_token_id
		i32 91; java_map_index
	}, 
	; 64
	%struct.TypeMapModuleEntry {
		i32 33554811, ; type_token_id
		i32 66; java_map_index
	}, 
	; 65
	%struct.TypeMapModuleEntry {
		i32 33554816, ; type_token_id
		i32 107; java_map_index
	}, 
	; 66
	%struct.TypeMapModuleEntry {
		i32 33554820, ; type_token_id
		i32 46; java_map_index
	}, 
	; 67
	%struct.TypeMapModuleEntry {
		i32 33554822, ; type_token_id
		i32 124; java_map_index
	}, 
	; 68
	%struct.TypeMapModuleEntry {
		i32 33554824, ; type_token_id
		i32 58; java_map_index
	}, 
	; 69
	%struct.TypeMapModuleEntry {
		i32 33554826, ; type_token_id
		i32 9; java_map_index
	}, 
	; 70
	%struct.TypeMapModuleEntry {
		i32 33554828, ; type_token_id
		i32 150; java_map_index
	}, 
	; 71
	%struct.TypeMapModuleEntry {
		i32 33554831, ; type_token_id
		i32 161; java_map_index
	}, 
	; 72
	%struct.TypeMapModuleEntry {
		i32 33554833, ; type_token_id
		i32 57; java_map_index
	}, 
	; 73
	%struct.TypeMapModuleEntry {
		i32 33554835, ; type_token_id
		i32 24; java_map_index
	}, 
	; 74
	%struct.TypeMapModuleEntry {
		i32 33554838, ; type_token_id
		i32 80; java_map_index
	}, 
	; 75
	%struct.TypeMapModuleEntry {
		i32 33554840, ; type_token_id
		i32 154; java_map_index
	}, 
	; 76
	%struct.TypeMapModuleEntry {
		i32 33554844, ; type_token_id
		i32 139; java_map_index
	}
], align 4; end of 'module2_managed_to_java_duplicates' array

; Map modules
@__TypeMapModule_assembly_name.0 = internal constant [33 x i8] c"Xamarin.GooglePlayServices.Tasks\00", align 1
@__TypeMapModule_assembly_name.1 = internal constant [29 x i8] c"APP.MerchPlus.LocationSender\00", align 1
@__TypeMapModule_assembly_name.2 = internal constant [13 x i8] c"Mono.Android\00", align 1

; map_modules
@map_modules = global [3 x %struct.TypeMapModule] [
	; 0
	%struct.TypeMapModule {
		[16 x i8] c"q\D3c\89\B2}\CCC\8A\EB\DE\D1\F5t\B0\DF", ; module_uuid: 8963d371-7db2-43cc-8aeb-ded1f574b0df
		i32 11, ; entry_count
		i32 9, ; duplicate_count
		%struct.TypeMapModuleEntry* getelementptr inbounds ([11 x %struct.TypeMapModuleEntry], [11 x %struct.TypeMapModuleEntry]* @module0_managed_to_java, i32 0, i32 0), ; map
		%struct.TypeMapModuleEntry* getelementptr inbounds ([9 x %struct.TypeMapModuleEntry], [9 x %struct.TypeMapModuleEntry]* @module0_managed_to_java_duplicates, i32 0, i32 0), ; duplicate_map
		i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__TypeMapModule_assembly_name.0, i32 0, i32 0), ; assembly_name: Xamarin.GooglePlayServices.Tasks
		%struct.MonoImage* null, ; image
		i32 0, ; java_name_width
		i8* null; java_map
	}, 
	; 1
	%struct.TypeMapModule {
		[16 x i8] c"\EB\F0\C5e]\81\85K\80\82\C0R(f\11_", ; module_uuid: 65c5f0eb-815d-4b85-8082-c0522866115f
		i32 1, ; entry_count
		i32 0, ; duplicate_count
		%struct.TypeMapModuleEntry* getelementptr inbounds ([1 x %struct.TypeMapModuleEntry], [1 x %struct.TypeMapModuleEntry]* @module1_managed_to_java, i32 0, i32 0), ; map
		%struct.TypeMapModuleEntry* null, ; duplicate_map
		i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__TypeMapModule_assembly_name.1, i32 0, i32 0), ; assembly_name: APP.MerchPlus.LocationSender
		%struct.MonoImage* null, ; image
		i32 0, ; java_name_width
		i8* null; java_map
	}, 
	; 2
	%struct.TypeMapModule {
		[16 x i8] c"\EF\B0\DE\09\0AF\EBA\89\1A\B2\18\BDQ\E0\07", ; module_uuid: 09deb0ef-460a-41eb-891a-b218bd51e007
		i32 153, ; entry_count
		i32 77, ; duplicate_count
		%struct.TypeMapModuleEntry* getelementptr inbounds ([153 x %struct.TypeMapModuleEntry], [153 x %struct.TypeMapModuleEntry]* @module2_managed_to_java, i32 0, i32 0), ; map
		%struct.TypeMapModuleEntry* getelementptr inbounds ([77 x %struct.TypeMapModuleEntry], [77 x %struct.TypeMapModuleEntry]* @module2_managed_to_java_duplicates, i32 0, i32 0), ; duplicate_map
		i8* getelementptr inbounds ([13 x i8], [13 x i8]* @__TypeMapModule_assembly_name.2, i32 0, i32 0), ; assembly_name: Mono.Android
		%struct.MonoImage* null, ; image
		i32 0, ; java_name_width
		i8* null; java_map
	}
], align 8; end of 'map_modules' array


; Java to managed map

; map_java
@map_java = local_unnamed_addr constant [165 x %struct.TypeMapJava] [
	; 0
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554792, ; type_token_id
		i32 130; java_name_index
	}, 
	; 1
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 96; java_name_index
	}, 
	; 2
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554737, ; type_token_id
		i32 94; java_name_index
	}, 
	; 3
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554538, ; type_token_id
		i32 16; java_name_index
	}, 
	; 4
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554625, ; type_token_id
		i32 60; java_name_index
	}, 
	; 5
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554537, ; type_token_id
		i32 15; java_name_index
	}, 
	; 6
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554570, ; type_token_id
		i32 34; java_name_index
	}, 
	; 7
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554558, ; type_token_id
		i32 27; java_name_index
	}, 
	; 8
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 4; java_name_index
	}, 
	; 9
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 152; java_name_index
	}, 
	; 10
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 112; java_name_index
	}, 
	; 11
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554784, ; type_token_id
		i32 122; java_name_index
	}, 
	; 12
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554705, ; type_token_id
		i32 88; java_name_index
	}, 
	; 13
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 29; java_name_index
	}, 
	; 14
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554829, ; type_token_id
		i32 154; java_name_index
	}, 
	; 15
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554704, ; type_token_id
		i32 87; java_name_index
	}, 
	; 16
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 95; java_name_index
	}, 
	; 17
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 8; java_name_index
	}, 
	; 18
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 79; java_name_index
	}, 
	; 19
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554624, ; type_token_id
		i32 59; java_name_index
	}, 
	; 20
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554836, ; type_token_id
		i32 158; java_name_index
	}, 
	; 21
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 109; java_name_index
	}, 
	; 22
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554732, ; type_token_id
		i32 91; java_name_index
	}, 
	; 23
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554587, ; type_token_id
		i32 43; java_name_index
	}, 
	; 24
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554834, ; type_token_id
		i32 157; java_name_index
	}, 
	; 25
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 44; java_name_index
	}, 
	; 26
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554562, ; type_token_id
		i32 30; java_name_index
	}, 
	; 27
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554654, ; type_token_id
		i32 75; java_name_index
	}, 
	; 28
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554637, ; type_token_id
		i32 67; java_name_index
	}, 
	; 29
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 42; java_name_index
	}, 
	; 30
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 108; java_name_index
	}, 
	; 31
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554841, ; type_token_id
		i32 161; java_name_index
	}, 
	; 32
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554731, ; type_token_id
		i32 90; java_name_index
	}, 
	; 33
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 41; java_name_index
	}, 
	; 34
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554735, ; type_token_id
		i32 93; java_name_index
	}, 
	; 35
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 7; java_name_index
	}, 
	; 36
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554781, ; type_token_id
		i32 119; java_name_index
	}, 
	; 37
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 99; java_name_index
	}, 
	; 38
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 5; java_name_index
	}, 
	; 39
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554733, ; type_token_id
		i32 92; java_name_index
	}, 
	; 40
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554527, ; type_token_id
		i32 14; java_name_index
	}, 
	; 41
	%struct.TypeMapJava {
		i32 1, ; module_index
		i32 33554434, ; type_token_id
		i32 11; java_name_index
	}, 
	; 42
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 33; java_name_index
	}, 
	; 43
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 25; java_name_index
	}, 
	; 44
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 39; java_name_index
	}, 
	; 45
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 38; java_name_index
	}, 
	; 46
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 149; java_name_index
	}, 
	; 47
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554798, ; type_token_id
		i32 135; java_name_index
	}, 
	; 48
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554631, ; type_token_id
		i32 64; java_name_index
	}, 
	; 49
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 6; java_name_index
	}, 
	; 50
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 66; java_name_index
	}, 
	; 51
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554749, ; type_token_id
		i32 101; java_name_index
	}, 
	; 52
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554613, ; type_token_id
		i32 53; java_name_index
	}, 
	; 53
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554565, ; type_token_id
		i32 32; java_name_index
	}, 
	; 54
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 31; java_name_index
	}, 
	; 55
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 33554443, ; type_token_id
		i32 2; java_name_index
	}, 
	; 56
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 61; java_name_index
	}, 
	; 57
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 156; java_name_index
	}, 
	; 58
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 151; java_name_index
	}, 
	; 59
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554595, ; type_token_id
		i32 47; java_name_index
	}, 
	; 60
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554780, ; type_token_id
		i32 118; java_name_index
	}, 
	; 61
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554691, ; type_token_id
		i32 84; java_name_index
	}, 
	; 62
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 37; java_name_index
	}, 
	; 63
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554776, ; type_token_id
		i32 115; java_name_index
	}, 
	; 64
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554807, ; type_token_id
		i32 139; java_name_index
	}, 
	; 65
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554657, ; type_token_id
		i32 77; java_name_index
	}, 
	; 66
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 142; java_name_index
	}, 
	; 67
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 40; java_name_index
	}, 
	; 68
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 21; java_name_index
	}, 
	; 69
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554663, ; type_token_id
		i32 80; java_name_index
	}, 
	; 70
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 26; java_name_index
	}, 
	; 71
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554778, ; type_token_id
		i32 116; java_name_index
	}, 
	; 72
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554553, ; type_token_id
		i32 24; java_name_index
	}, 
	; 73
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 3; java_name_index
	}, 
	; 74
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554642, ; type_token_id
		i32 70; java_name_index
	}, 
	; 75
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554539, ; type_token_id
		i32 17; java_name_index
	}, 
	; 76
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554612, ; type_token_id
		i32 52; java_name_index
	}, 
	; 77
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554794, ; type_token_id
		i32 131; java_name_index
	}, 
	; 78
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554813, ; type_token_id
		i32 144; java_name_index
	}, 
	; 79
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554647, ; type_token_id
		i32 72; java_name_index
	}, 
	; 80
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 159; java_name_index
	}, 
	; 81
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554812, ; type_token_id
		i32 143; java_name_index
	}, 
	; 82
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554814, ; type_token_id
		i32 145; java_name_index
	}, 
	; 83
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 111; java_name_index
	}, 
	; 84
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 9; java_name_index
	}, 
	; 85
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554788, ; type_token_id
		i32 126; java_name_index
	}, 
	; 86
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554609, ; type_token_id
		i32 51; java_name_index
	}, 
	; 87
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 13; java_name_index
	}, 
	; 88
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 48; java_name_index
	}, 
	; 89
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554818, ; type_token_id
		i32 148; java_name_index
	}, 
	; 90
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554791, ; type_token_id
		i32 129; java_name_index
	}, 
	; 91
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 138; java_name_index
	}, 
	; 92
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554623, ; type_token_id
		i32 58; java_name_index
	}, 
	; 93
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 114; java_name_index
	}, 
	; 94
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554795, ; type_token_id
		i32 132; java_name_index
	}, 
	; 95
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 82; java_name_index
	}, 
	; 96
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554700, ; type_token_id
		i32 85; java_name_index
	}, 
	; 97
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554747, ; type_token_id
		i32 100; java_name_index
	}, 
	; 98
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 113; java_name_index
	}, 
	; 99
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554541, ; type_token_id
		i32 18; java_name_index
	}, 
	; 100
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554653, ; type_token_id
		i32 74; java_name_index
	}, 
	; 101
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 33554442, ; type_token_id
		i32 1; java_name_index
	}, 
	; 102
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554782, ; type_token_id
		i32 120; java_name_index
	}, 
	; 103
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 78; java_name_index
	}, 
	; 104
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554809, ; type_token_id
		i32 141; java_name_index
	}, 
	; 105
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554628, ; type_token_id
		i32 62; java_name_index
	}, 
	; 106
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554808, ; type_token_id
		i32 140; java_name_index
	}, 
	; 107
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554815, ; type_token_id
		i32 146; java_name_index
	}, 
	; 108
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 102; java_name_index
	}, 
	; 109
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554787, ; type_token_id
		i32 125; java_name_index
	}, 
	; 110
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 54; java_name_index
	}, 
	; 111
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 23; java_name_index
	}, 
	; 112
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554655, ; type_token_id
		i32 76; java_name_index
	}, 
	; 113
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 10; java_name_index
	}, 
	; 114
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554640, ; type_token_id
		i32 68; java_name_index
	}, 
	; 115
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 45; java_name_index
	}, 
	; 116
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554866, ; type_token_id
		i32 164; java_name_index
	}, 
	; 117
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554641, ; type_token_id
		i32 69; java_name_index
	}, 
	; 118
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554752, ; type_token_id
		i32 103; java_name_index
	}, 
	; 119
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554620, ; type_token_id
		i32 56; java_name_index
	}, 
	; 120
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554842, ; type_token_id
		i32 162; java_name_index
	}, 
	; 121
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554790, ; type_token_id
		i32 128; java_name_index
	}, 
	; 122
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 57; java_name_index
	}, 
	; 123
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 19; java_name_index
	}, 
	; 124
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 150; java_name_index
	}, 
	; 125
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554758, ; type_token_id
		i32 106; java_name_index
	}, 
	; 126
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554789, ; type_token_id
		i32 127; java_name_index
	}, 
	; 127
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554797, ; type_token_id
		i32 134; java_name_index
	}, 
	; 128
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554721, ; type_token_id
		i32 89; java_name_index
	}, 
	; 129
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 110; java_name_index
	}, 
	; 130
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 46; java_name_index
	}, 
	; 131
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554629, ; type_token_id
		i32 63; java_name_index
	}, 
	; 132
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554779, ; type_token_id
		i32 117; java_name_index
	}, 
	; 133
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 137; java_name_index
	}, 
	; 134
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554544, ; type_token_id
		i32 20; java_name_index
	}, 
	; 135
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 124; java_name_index
	}, 
	; 136
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554796, ; type_token_id
		i32 133; java_name_index
	}, 
	; 137
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554619, ; type_token_id
		i32 55; java_name_index
	}, 
	; 138
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 71; java_name_index
	}, 
	; 139
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554843, ; type_token_id
		i32 163; java_name_index
	}, 
	; 140
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 136; java_name_index
	}, 
	; 141
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 98; java_name_index
	}, 
	; 142
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 12; java_name_index
	}, 
	; 143
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 107; java_name_index
	}, 
	; 144
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554559, ; type_token_id
		i32 28; java_name_index
	}, 
	; 145
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554572, ; type_token_id
		i32 35; java_name_index
	}, 
	; 146
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554756, ; type_token_id
		i32 105; java_name_index
	}, 
	; 147
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554634, ; type_token_id
		i32 65; java_name_index
	}, 
	; 148
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554604, ; type_token_id
		i32 49; java_name_index
	}, 
	; 149
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554648, ; type_token_id
		i32 73; java_name_index
	}, 
	; 150
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 153; java_name_index
	}, 
	; 151
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554702, ; type_token_id
		i32 86; java_name_index
	}, 
	; 152
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554685, ; type_token_id
		i32 81; java_name_index
	}, 
	; 153
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554608, ; type_token_id
		i32 50; java_name_index
	}, 
	; 154
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554839, ; type_token_id
		i32 160; java_name_index
	}, 
	; 155
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554550, ; type_token_id
		i32 22; java_name_index
	}, 
	; 156
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554783, ; type_token_id
		i32 121; java_name_index
	}, 
	; 157
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554817, ; type_token_id
		i32 147; java_name_index
	}, 
	; 158
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554742, ; type_token_id
		i32 97; java_name_index
	}, 
	; 159
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554573, ; type_token_id
		i32 36; java_name_index
	}, 
	; 160
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554754, ; type_token_id
		i32 104; java_name_index
	}, 
	; 161
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 155; java_name_index
	}, 
	; 162
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 33554441, ; type_token_id
		i32 0; java_name_index
	}, 
	; 163
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554785, ; type_token_id
		i32 123; java_name_index
	}, 
	; 164
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554689, ; type_token_id
		i32 83; java_name_index
	}
], align 4; end of 'map_java' array

@map_java_hashes = local_unnamed_addr constant [165 x i64] [
	i64 128182020419974451, ; 0: 0x1c764de51b97533 => java/lang/String
	i64 146042335049966109, ; 1: 0x206d8baded2621d => java/util/concurrent/Executor
	i64 229629845183097875, ; 2: 0x32fcf20a7f76c13 => java/net/URI
	i64 318564728890166633, ; 3: 0x46bc4eedf778d69 => android/widget/Button
	i64 361870449891484378, ; 4: 0x5059f41c47e22da => android/os/Bundle
	i64 363417747702605178, ; 5: 0x50b1e841ce2e57a => android/widget/TextView
	i64 582128946798849863, ; 6: 0x81423315f6aef47 => android/view/ActionProvider
	i64 705175846315662030, ; 7: 0x9c949b22fd49ace => android/view/MotionEvent
	i64 721515218677255058, ; 8: 0xa035644c54bcb92 => com/google/android/gms/tasks/OnCanceledListener
	i64 753844298871422754, ; 9: 0xa763165522f3f22 => java/lang/reflect/Type
	i64 809480176342503793, ; 10: 0xb3bd9ee7b9d9171 => java/nio/channels/ScatteringByteChannel
	i64 870874870088288028, ; 11: 0xc15f8148b6d471c => java/lang/Exception
	i64 1217044833273073796, ; 12: 0x10e3cfd3e2f75084 => java/util/HashSet
	i64 1247566983430333137, ; 13: 0x11503f8fc0dee2d1 => android/view/ViewTreeObserver$OnPreDrawListener
	i64 1317579852464953526, ; 14: 0x1248fbe51d6298b6 => java/io/FileInputStream
	i64 1320822650197077237, ; 15: 0x12548133cc496cf5 => android/runtime/JavaProxyThrowable
	i64 1362770524300979611, ; 16: 0x12e9889253552d9b => java/util/Iterator
	i64 1396711346358256981, ; 17: 0x13621d91c5710555 => com/google/android/gms/tasks/OnTokenCanceledListener
	i64 1608247358826116468, ; 18: 0x1651a473b5ca7574 => android/content/ComponentCallbacks2
	i64 1747499027921055994, ; 19: 0x18405d1b749330fa => android/os/BaseBundle
	i64 1831728799718484971, ; 20: 0x196b9ba37012abeb => java/io/IOException
	i64 1850333256545206711, ; 21: 0x19adb44b187801b7 => java/nio/channels/GatheringByteChannel
	i64 1877272793125324469, ; 22: 0x1a0d69a8bcbd86b5 => java/net/Proxy
	i64 2111352555338672611, ; 23: 0x1d4d07f6709329e3 => android/view/InputEvent
	i64 2164140653916027403, ; 24: 0x1e08927568a57a0b => java/io/InputStream
	i64 2349580542494205303, ; 25: 0x209b630e06896577 => android/view/SubMenu
	i64 2542726837267699812, ; 26: 0x2349949628319864 => android/view/Window
	i64 2972252214977986258, ; 27: 0x293f8fa450a17ed2 => android/content/Intent
	i64 3107623337225989922, ; 28: 0x2b207ef5d711af22 => android/location/Location
	i64 3171452383522110633, ; 29: 0x2c034323c5e6bca9 => android/view/MenuItem
	i64 3277568047959927566, ; 30: 0x2d7c42c639d26b0e => java/nio/channels/Channel
	i64 3476617847597562063, ; 31: 0x303f6d8331d5f8cf => java/io/PrintWriter
	i64 3656396631051491790, ; 32: 0x32be215d0fc259ce => java/net/InetSocketAddress
	i64 3668991680153232620, ; 33: 0x32eae07e7365a4ec => android/view/MenuItem$OnMenuItemClickListener
	i64 3936478700004404583, ; 34: 0x36a12e8573a76d67 => java/net/SocketAddress
	i64 3987338666338875411, ; 35: 0x3755df64264ef813 => com/google/android/gms/tasks/OnSuccessListener
	i64 4305371449952891808, ; 36: 0x3bbfc085dc8cf3a0 => java/lang/Class
	i64 4346110607894790856, ; 37: 0x3c507c923a2042c8 => java/security/KeyStore$ProtectionParameter
	i64 4365920733137736669, ; 38: 0x3c96ddc6703013dd => com/google/android/gms/tasks/OnCompleteListener
	i64 4480257524133297879, ; 39: 0x3e2d127b143c7ed7 => java/net/ProxySelector
	i64 4590799101254748484, ; 40: 0x3fb5cb75a178c944 => javax/net/ssl/TrustManagerFactory
	i64 4645159943685975061, ; 41: 0x4076ec5b9046ac15 => crc64e2e365b8ee8526bd/MainActivity
	i64 4657718675205956216, ; 42: 0x40a38a751b5aa278 => android/view/ActionMode$Callback
	i64 4668430364331232817, ; 43: 0x40c998ae949c4e31 => android/view/LayoutInflater$Factory
	i64 4721440782757463965, ; 44: 0x4185ed606c4d079d => android/view/Menu
	i64 4919249705507088493, ; 45: 0x4444af8b3a31e86d => android/view/ContextMenu
	i64 5207893280228753221, ; 46: 0x48462758842dbf45 => java/lang/annotation/Annotation
	i64 5214467817578676657, ; 47: 0x485d82da477bc1b1 => java/lang/Error
	i64 5602058331881754211, ; 48: 0x4dbe825f32d44e63 => android/location/LocationManager
	i64 5670648573481752791, ; 49: 0x4eb230d54c7498d7 => com/google/android/gms/tasks/OnFailureListener
	i64 5681742043617578465, ; 50: 0x4ed99a49173a4de1 => android/location/LocationListener
	i64 5854524328054243249, ; 51: 0x513f72db968e3fb1 => java/security/cert/CertificateFactory
	i64 5902220174995442397, ; 52: 0x51e8e5fa54bf4add => android/view/accessibility/AccessibilityRecord
	i64 6193589699106797389, ; 53: 0x55f40d042bc7774d => android/view/ActionMode
	i64 6804602249961354267, ; 54: 0x5e6ecdb1aac5341b => android/view/Window$Callback
	i64 6963894354111060225, ; 55: 0x60a4b904ce235901 => com/google/android/gms/tasks/CancellationToken
	i64 7282188369651690282, ; 56: 0x650f87bd5091eb2a => android/os/Parcelable
	i64 7349164247998023060, ; 57: 0x65fd79f22dec4594 => java/io/Flushable
	i64 7394638939512291408, ; 58: 0x669f08efdfe6c050 => java/lang/reflect/GenericDeclaration
	i64 7538647566659605431, ; 59: 0x689ea805399bd3b7 => android/view/WindowManager$LayoutParams
	i64 7658195837123306865, ; 60: 0x6a476089fc1c2571 => java/lang/Character
	i64 7933543037734065265, ; 61: 0x6e199b5bee699471 => java/util/HashMap
	i64 8058145963905469793, ; 62: 0x6fd4490f6ed54d61 => android/view/ContextMenu$ContextMenuInfo
	i64 8190305621607579207, ; 63: 0x71a9cf9199cdfe47 => java/nio/channels/spi/AbstractInterruptibleChannel
	i64 8416619862292774857, ; 64: 0x74cdd72bed753fc9 => java/lang/IllegalArgumentException
	i64 8487642170263250902, ; 65: 0x75ca29959b2aa7d6 => android/content/ContextWrapper
	i64 8587172038193766563, ; 66: 0x772bc378d1b4e0a3 => java/lang/Runnable
	i64 8712284566595978930, ; 67: 0x78e840a7561246b2 => android/view/MenuItem$OnActionExpandListener
	i64 8717832878576044787, ; 68: 0x78fbf6d062bf0af3 => android/view/View$OnCreateContextMenuListener
	i64 8784751650103882924, ; 69: 0x79e9b5150877f4ac => android/content/res/Configuration
	i64 8785370249272798009, ; 70: 0x79ebe7b1e030ab39 => android/view/LayoutInflater$Factory2
	i64 8950391188589719199, ; 71: 0x7c362d5d64ad2e9f => java/lang/Boolean
	i64 8972627133644507939, ; 72: 0x7c852cd6cae98b23 => android/view/LayoutInflater
	i64 9065084893191776779, ; 73: 0x7dcda6ae7574aa0b => com/google/android/gms/tasks/Continuation
	i64 9355021183435591253, ; 74: 0x81d3b63388eece55 => android/graphics/drawable/Drawable
	i64 9478593896738967145, ; 75: 0x838abaede94fce69 => android/widget/Toast
	i64 9653796168998115956, ; 76: 0x85f92c77bb28da74 => android/view/accessibility/AccessibilityEvent
	i64 9667515047141612341, ; 77: 0x8629e9b6f59e9b35 => java/lang/Thread
	i64 9854108034008202699, ; 78: 0x88c0d30c3161bdcb => java/lang/NoClassDefFoundError
	i64 9869939015140501507, ; 79: 0x88f9113db837e803 => android/app/Activity
	i64 9903613304640537389, ; 80: 0x8970b3d437ee832d => java/io/Serializable
	i64 9942049212981248440, ; 81: 0x89f941157f3301b8 => java/lang/LinkageError
	i64 9977296435420958008, ; 82: 0x8a767a3efc098d38 => java/lang/NullPointerException
	i64 10092484235993399888, ; 83: 0x8c0fb4f0bb9dc250 => java/nio/channels/ReadableByteChannel
	i64 10199805429783807289, ; 84: 0x8d8cfcff5c5d4539 => com/google/android/gms/tasks/SuccessContinuation
	i64 10266059374509936169, ; 85: 0x8e785e9bf4bbce29 => java/lang/Long
	i64 10499957734077086001, ; 86: 0x91b757ed9047b931 => android/view/ViewGroup$LayoutParams
	i64 10541139640792710788, ; 87: 0x9249a6a7693cfe84 => javax/net/ssl/X509TrustManager
	i64 10556732314164660806, ; 88: 0x92810c1b96ac0a46 => android/view/WindowManager
	i64 10589642565195629679, ; 89: 0x92f5f7ce84d7846f => java/lang/UnsupportedOperationException
	i64 10722894652849872693, ; 90: 0x94cf5fdfdb0d5f35 => java/lang/Short
	i64 10876136888617769900, ; 91: 0x96efcce03e959bac => java/lang/Comparable
	i64 11112718717483603117, ; 92: 0x9a384ecbbc71d4ad => android/os/Handler
	i64 11172923279375061947, ; 93: 0x9b0e3286a1d7d3bb => java/nio/channels/WritableByteChannel
	i64 11573301743732151818, ; 94: 0xa09ca09e3190560a => mono/java/lang/RunnableImplementor
	i64 11585998938245262039, ; 95: 0xa0c9bca62a296ad7 => mono/android/runtime/JavaArray
	i64 11712899692065226922, ; 96: 0xa28c94365b5480aa => java/util/ArrayList
	i64 11763058807128842702, ; 97: 0xa33ec7a966f1e1ce => java/security/cert/Certificate
	i64 11805384242711821632, ; 98: 0xa3d5266bcfd4fd40 => java/nio/channels/SeekableByteChannel
	i64 11954228872253987625, ; 99: 0xa5e5f3d2b66adb29 => android/view/View
	i64 12228984007958404582, ; 100: 0xa9b61429ce4b1de6 => android/content/Context
	i64 12243761174021165547, ; 101: 0xa9ea93eae44051eb => com/google/android/gms/tasks/TaskCompletionSource
	i64 12246361600639158634, ; 102: 0xa9f3d0fde514c96a => java/lang/ClassNotFoundException
	i64 12319111449616420196, ; 103: 0xaaf646980ed58d64 => android/content/ComponentCallbacks
	i64 12426529965699990912, ; 104: 0xac73e72a4c4b8580 => java/lang/IndexOutOfBoundsException
	i64 12476375190645835422, ; 105: 0xad24fd221af1069e => android/os/Looper
	i64 12488842103917764438, ; 106: 0xad5147b98bf5df56 => java/lang/IllegalStateException
	i64 12532121860257401396, ; 107: 0xadeb0a6f128cca34 => java/lang/Number
	i64 12562071772572039222, ; 108: 0xae5571b86f1e3836 => java/security/cert/X509Extension
	i64 12806567541869262104, ; 109: 0xb1ba1153c52a3518 => java/lang/Integer
	i64 12807185847019254324, ; 110: 0xb1bc43ac27fe3234 => android/view/accessibility/AccessibilityEventSource
	i64 13069688267367477916, ; 111: 0xb560dc3ba587729c => android/view/KeyEvent$Callback
	i64 13291089739636035741, ; 112: 0xb8736fb2f0d8509d => android/content/ComponentName
	i64 13328282303989574758, ; 113: 0xb8f79223a36c6c66 => crc6495d4f5d63cc5c882/AwaitableTaskCompleteListener_1
	i64 13335777145830317192, ; 114: 0xb91232a87cfd1c88 => android/graphics/Point
	i64 13402681668680117407, ; 115: 0xb9ffe3f79b516c9f => android/view/ViewManager
	i64 13402779434266666368, ; 116: 0xba003ce26e602580 => mono/android/TypeManager
	i64 13493236796125990997, ; 117: 0xbb419b603751d055 => android/graphics/Rect
	i64 13499562364224885079, ; 118: 0xbb5814724edb5157 => java/security/cert/X509Certificate
	i64 13556576098032765635, ; 119: 0xbc22a222a5cb4ac3 => android/util/DisplayMetrics
	i64 13770727111868296170, ; 120: 0xbf1b735909c02bea => java/io/StringWriter
	i64 13789203013919682202, ; 121: 0xbf5d1715346dae9a => java/lang/RuntimeException
	i64 13805562342397192842, ; 122: 0xbf9735ce2f182a8a => android/util/AttributeSet
	i64 13877554026709814142, ; 123: 0xc096f9dc61548b7e => android/view/View$OnClickListener
	i64 14006488896635186732, ; 124: 0xc2610b712264ae2c => java/lang/reflect/AnnotatedElement
	i64 14031640676547298208, ; 125: 0xc2ba66da3d8603a0 => java/nio/channels/FileChannel
	i64 14167891754637755728, ; 126: 0xc49e767c735e8550 => java/lang/Object
	i64 14180814796703042768, ; 127: 0xc4cc5feca7168cd0 => java/lang/ClassCastException
	i64 14206023932851353817, ; 128: 0xc525ef800c4d78d9 => mono/android/runtime/OutputStreamAdapter
	i64 14257601591339809660, ; 129: 0xc5dd2d1b78da7b7c => java/nio/channels/InterruptibleChannel
	i64 14328901088261815086, ; 130: 0xc6da7b9f3b8dbb2e => android/view/ViewParent
	i64 14361620789319229198, ; 131: 0xc74eba044a3cdf0e => android/os/Parcel
	i64 14501341618205132561, ; 132: 0xc93f1d5ecfb48711 => java/lang/Byte
	i64 14517395435871339070, ; 133: 0xc978263b82fd023e => java/lang/Cloneable
	i64 14649586819325063784, ; 134: 0xcb4dc998681d7268 => mono/android/view/View_OnClickListenerImplementor
	i64 14684559126920293129, ; 135: 0xcbca08b94b4deb09 => java/lang/CharSequence
	i64 14940408132235664607, ; 136: 0xcf56fe09e1439cdf => java/lang/Throwable
	i64 14940704599466395126, ; 137: 0xcf580bac893d45f6 => android/util/Log
	i64 14959579543312699232, ; 138: 0xcf9b1a55dc5a8f60 => android/graphics/drawable/Drawable$Callback
	i64 15633873768898914415, ; 139: 0xd8f6ad5c6a84686f => java/io/Writer
	i64 15645438581444444576, ; 140: 0xd91fc37f2a3001a0 => java/lang/Appendable
	i64 15773716038598610423, ; 141: 0xdae77f2a148d4df7 => java/security/KeyStore$LoadStoreParameter
	i64 15888603495552893685, ; 142: 0xdc7fa8b2a175c2f5 => javax/net/ssl/TrustManager
	i64 15949051304247903982, ; 143: 0xdd5669a8d62d46ee => java/nio/channels/ByteChannel
	i64 16170603982710859185, ; 144: 0xe06986a590ff65b1 => android/view/ViewTreeObserver
	i64 16314168557433322311, ; 145: 0xe26791dde7a8fb47 => android/view/ContextThemeWrapper
	i64 16395504991077438774, ; 146: 0xe38888ec1a509536 => java/nio/ByteBuffer
	i64 16410496677374749578, ; 147: 0xe3bdcbc812a66b8a => android/location/Criteria
	i64 16480206070542885578, ; 148: 0xe4b5741af3f1e2ca => android/view/SearchEvent
	i64 16542847110558016359, ; 149: 0xe593ffcc9e686367 => android/app/Application
	i64 16543572318389959461, ; 150: 0xe596935f399e7b25 => java/lang/reflect/TypeVariable
	i64 16603717322881265010, ; 151: 0xe66c40ef55566d72 => mono/android/runtime/JavaObject
	i64 16723123314454325679, ; 152: 0xe814780d351a69af => mono/android/runtime/InputStreamAdapter
	i64 16832017439803262409, ; 153: 0xe99756ae80a745c9 => android/view/ViewGroup
	i64 17125416866214736517, ; 154: 0xeda9b3e7cd367285 => java/io/OutputStream
	i64 17312589003384955623, ; 155: 0xf042abf8f91822e7 => android/view/KeyEvent
	i64 17498018958444438720, ; 156: 0xf2d57388c321a4c0 => java/lang/Double
	i64 17499752140262191063, ; 157: 0xf2db9bda99b5afd7 => java/lang/ReflectiveOperationException
	i64 17519581955836770800, ; 158: 0xf3220ef752fe79f0 => java/security/KeyStore
	i64 17542023464739623563, ; 159: 0xf371c966495f528b => android/view/Display
	i64 17608877981747935975, ; 160: 0xf45f4d3a83487ae7 => java/nio/Buffer
	i64 17645817129531856090, ; 161: 0xf4e28930113fd4da => java/io/Closeable
	i64 17815335875642952963, ; 162: 0xf73cc996d5f88d03 => com/google/android/gms/tasks/Task
	i64 18401512074820890716, ; 163: 0xff5f4dbc95c2b05c => java/lang/Float
	i64 18401692079723824300 ; 164: 0xff5ff1733e0ec4ac => java/util/Collection
], align 8

; java_type_names
@__java_type_names.0 = internal constant [34 x i8] c"com/google/android/gms/tasks/Task\00", align 1
@__java_type_names.1 = internal constant [50 x i8] c"com/google/android/gms/tasks/TaskCompletionSource\00", align 1
@__java_type_names.2 = internal constant [47 x i8] c"com/google/android/gms/tasks/CancellationToken\00", align 1
@__java_type_names.3 = internal constant [42 x i8] c"com/google/android/gms/tasks/Continuation\00", align 1
@__java_type_names.4 = internal constant [48 x i8] c"com/google/android/gms/tasks/OnCanceledListener\00", align 1
@__java_type_names.5 = internal constant [48 x i8] c"com/google/android/gms/tasks/OnCompleteListener\00", align 1
@__java_type_names.6 = internal constant [47 x i8] c"com/google/android/gms/tasks/OnFailureListener\00", align 1
@__java_type_names.7 = internal constant [47 x i8] c"com/google/android/gms/tasks/OnSuccessListener\00", align 1
@__java_type_names.8 = internal constant [53 x i8] c"com/google/android/gms/tasks/OnTokenCanceledListener\00", align 1
@__java_type_names.9 = internal constant [49 x i8] c"com/google/android/gms/tasks/SuccessContinuation\00", align 1
@__java_type_names.10 = internal constant [54 x i8] c"crc6495d4f5d63cc5c882/AwaitableTaskCompleteListener_1\00", align 1
@__java_type_names.11 = internal constant [35 x i8] c"crc64e2e365b8ee8526bd/MainActivity\00", align 1
@__java_type_names.12 = internal constant [27 x i8] c"javax/net/ssl/TrustManager\00", align 1
@__java_type_names.13 = internal constant [31 x i8] c"javax/net/ssl/X509TrustManager\00", align 1
@__java_type_names.14 = internal constant [34 x i8] c"javax/net/ssl/TrustManagerFactory\00", align 1
@__java_type_names.15 = internal constant [24 x i8] c"android/widget/TextView\00", align 1
@__java_type_names.16 = internal constant [22 x i8] c"android/widget/Button\00", align 1
@__java_type_names.17 = internal constant [21 x i8] c"android/widget/Toast\00", align 1
@__java_type_names.18 = internal constant [18 x i8] c"android/view/View\00", align 1
@__java_type_names.19 = internal constant [34 x i8] c"android/view/View$OnClickListener\00", align 1
@__java_type_names.20 = internal constant [50 x i8] c"mono/android/view/View_OnClickListenerImplementor\00", align 1
@__java_type_names.21 = internal constant [46 x i8] c"android/view/View$OnCreateContextMenuListener\00", align 1
@__java_type_names.22 = internal constant [22 x i8] c"android/view/KeyEvent\00", align 1
@__java_type_names.23 = internal constant [31 x i8] c"android/view/KeyEvent$Callback\00", align 1
@__java_type_names.24 = internal constant [28 x i8] c"android/view/LayoutInflater\00", align 1
@__java_type_names.25 = internal constant [36 x i8] c"android/view/LayoutInflater$Factory\00", align 1
@__java_type_names.26 = internal constant [37 x i8] c"android/view/LayoutInflater$Factory2\00", align 1
@__java_type_names.27 = internal constant [25 x i8] c"android/view/MotionEvent\00", align 1
@__java_type_names.28 = internal constant [30 x i8] c"android/view/ViewTreeObserver\00", align 1
@__java_type_names.29 = internal constant [48 x i8] c"android/view/ViewTreeObserver$OnPreDrawListener\00", align 1
@__java_type_names.30 = internal constant [20 x i8] c"android/view/Window\00", align 1
@__java_type_names.31 = internal constant [29 x i8] c"android/view/Window$Callback\00", align 1
@__java_type_names.32 = internal constant [24 x i8] c"android/view/ActionMode\00", align 1
@__java_type_names.33 = internal constant [33 x i8] c"android/view/ActionMode$Callback\00", align 1
@__java_type_names.34 = internal constant [28 x i8] c"android/view/ActionProvider\00", align 1
@__java_type_names.35 = internal constant [33 x i8] c"android/view/ContextThemeWrapper\00", align 1
@__java_type_names.36 = internal constant [21 x i8] c"android/view/Display\00", align 1
@__java_type_names.37 = internal constant [41 x i8] c"android/view/ContextMenu$ContextMenuInfo\00", align 1
@__java_type_names.38 = internal constant [25 x i8] c"android/view/ContextMenu\00", align 1
@__java_type_names.39 = internal constant [18 x i8] c"android/view/Menu\00", align 1
@__java_type_names.40 = internal constant [45 x i8] c"android/view/MenuItem$OnActionExpandListener\00", align 1
@__java_type_names.41 = internal constant [46 x i8] c"android/view/MenuItem$OnMenuItemClickListener\00", align 1
@__java_type_names.42 = internal constant [22 x i8] c"android/view/MenuItem\00", align 1
@__java_type_names.43 = internal constant [24 x i8] c"android/view/InputEvent\00", align 1
@__java_type_names.44 = internal constant [21 x i8] c"android/view/SubMenu\00", align 1
@__java_type_names.45 = internal constant [25 x i8] c"android/view/ViewManager\00", align 1
@__java_type_names.46 = internal constant [24 x i8] c"android/view/ViewParent\00", align 1
@__java_type_names.47 = internal constant [40 x i8] c"android/view/WindowManager$LayoutParams\00", align 1
@__java_type_names.48 = internal constant [27 x i8] c"android/view/WindowManager\00", align 1
@__java_type_names.49 = internal constant [25 x i8] c"android/view/SearchEvent\00", align 1
@__java_type_names.50 = internal constant [23 x i8] c"android/view/ViewGroup\00", align 1
@__java_type_names.51 = internal constant [36 x i8] c"android/view/ViewGroup$LayoutParams\00", align 1
@__java_type_names.52 = internal constant [46 x i8] c"android/view/accessibility/AccessibilityEvent\00", align 1
@__java_type_names.53 = internal constant [47 x i8] c"android/view/accessibility/AccessibilityRecord\00", align 1
@__java_type_names.54 = internal constant [52 x i8] c"android/view/accessibility/AccessibilityEventSource\00", align 1
@__java_type_names.55 = internal constant [17 x i8] c"android/util/Log\00", align 1
@__java_type_names.56 = internal constant [28 x i8] c"android/util/DisplayMetrics\00", align 1
@__java_type_names.57 = internal constant [26 x i8] c"android/util/AttributeSet\00", align 1
@__java_type_names.58 = internal constant [19 x i8] c"android/os/Handler\00", align 1
@__java_type_names.59 = internal constant [22 x i8] c"android/os/BaseBundle\00", align 1
@__java_type_names.60 = internal constant [18 x i8] c"android/os/Bundle\00", align 1
@__java_type_names.61 = internal constant [22 x i8] c"android/os/Parcelable\00", align 1
@__java_type_names.62 = internal constant [18 x i8] c"android/os/Looper\00", align 1
@__java_type_names.63 = internal constant [18 x i8] c"android/os/Parcel\00", align 1
@__java_type_names.64 = internal constant [33 x i8] c"android/location/LocationManager\00", align 1
@__java_type_names.65 = internal constant [26 x i8] c"android/location/Criteria\00", align 1
@__java_type_names.66 = internal constant [34 x i8] c"android/location/LocationListener\00", align 1
@__java_type_names.67 = internal constant [26 x i8] c"android/location/Location\00", align 1
@__java_type_names.68 = internal constant [23 x i8] c"android/graphics/Point\00", align 1
@__java_type_names.69 = internal constant [22 x i8] c"android/graphics/Rect\00", align 1
@__java_type_names.70 = internal constant [35 x i8] c"android/graphics/drawable/Drawable\00", align 1
@__java_type_names.71 = internal constant [44 x i8] c"android/graphics/drawable/Drawable$Callback\00", align 1
@__java_type_names.72 = internal constant [21 x i8] c"android/app/Activity\00", align 1
@__java_type_names.73 = internal constant [24 x i8] c"android/app/Application\00", align 1
@__java_type_names.74 = internal constant [24 x i8] c"android/content/Context\00", align 1
@__java_type_names.75 = internal constant [23 x i8] c"android/content/Intent\00", align 1
@__java_type_names.76 = internal constant [30 x i8] c"android/content/ComponentName\00", align 1
@__java_type_names.77 = internal constant [31 x i8] c"android/content/ContextWrapper\00", align 1
@__java_type_names.78 = internal constant [35 x i8] c"android/content/ComponentCallbacks\00", align 1
@__java_type_names.79 = internal constant [36 x i8] c"android/content/ComponentCallbacks2\00", align 1
@__java_type_names.80 = internal constant [34 x i8] c"android/content/res/Configuration\00", align 1
@__java_type_names.81 = internal constant [40 x i8] c"mono/android/runtime/InputStreamAdapter\00", align 1
@__java_type_names.82 = internal constant [31 x i8] c"mono/android/runtime/JavaArray\00", align 1
@__java_type_names.83 = internal constant [21 x i8] c"java/util/Collection\00", align 1
@__java_type_names.84 = internal constant [18 x i8] c"java/util/HashMap\00", align 1
@__java_type_names.85 = internal constant [20 x i8] c"java/util/ArrayList\00", align 1
@__java_type_names.86 = internal constant [32 x i8] c"mono/android/runtime/JavaObject\00", align 1
@__java_type_names.87 = internal constant [35 x i8] c"android/runtime/JavaProxyThrowable\00", align 1
@__java_type_names.88 = internal constant [18 x i8] c"java/util/HashSet\00", align 1
@__java_type_names.89 = internal constant [41 x i8] c"mono/android/runtime/OutputStreamAdapter\00", align 1
@__java_type_names.90 = internal constant [27 x i8] c"java/net/InetSocketAddress\00", align 1
@__java_type_names.91 = internal constant [15 x i8] c"java/net/Proxy\00", align 1
@__java_type_names.92 = internal constant [23 x i8] c"java/net/ProxySelector\00", align 1
@__java_type_names.93 = internal constant [23 x i8] c"java/net/SocketAddress\00", align 1
@__java_type_names.94 = internal constant [13 x i8] c"java/net/URI\00", align 1
@__java_type_names.95 = internal constant [19 x i8] c"java/util/Iterator\00", align 1
@__java_type_names.96 = internal constant [30 x i8] c"java/util/concurrent/Executor\00", align 1
@__java_type_names.97 = internal constant [23 x i8] c"java/security/KeyStore\00", align 1
@__java_type_names.98 = internal constant [42 x i8] c"java/security/KeyStore$LoadStoreParameter\00", align 1
@__java_type_names.99 = internal constant [43 x i8] c"java/security/KeyStore$ProtectionParameter\00", align 1
@__java_type_names.100 = internal constant [31 x i8] c"java/security/cert/Certificate\00", align 1
@__java_type_names.101 = internal constant [38 x i8] c"java/security/cert/CertificateFactory\00", align 1
@__java_type_names.102 = internal constant [33 x i8] c"java/security/cert/X509Extension\00", align 1
@__java_type_names.103 = internal constant [35 x i8] c"java/security/cert/X509Certificate\00", align 1
@__java_type_names.104 = internal constant [16 x i8] c"java/nio/Buffer\00", align 1
@__java_type_names.105 = internal constant [20 x i8] c"java/nio/ByteBuffer\00", align 1
@__java_type_names.106 = internal constant [30 x i8] c"java/nio/channels/FileChannel\00", align 1
@__java_type_names.107 = internal constant [30 x i8] c"java/nio/channels/ByteChannel\00", align 1
@__java_type_names.108 = internal constant [26 x i8] c"java/nio/channels/Channel\00", align 1
@__java_type_names.109 = internal constant [39 x i8] c"java/nio/channels/GatheringByteChannel\00", align 1
@__java_type_names.110 = internal constant [39 x i8] c"java/nio/channels/InterruptibleChannel\00", align 1
@__java_type_names.111 = internal constant [38 x i8] c"java/nio/channels/ReadableByteChannel\00", align 1
@__java_type_names.112 = internal constant [40 x i8] c"java/nio/channels/ScatteringByteChannel\00", align 1
@__java_type_names.113 = internal constant [38 x i8] c"java/nio/channels/SeekableByteChannel\00", align 1
@__java_type_names.114 = internal constant [38 x i8] c"java/nio/channels/WritableByteChannel\00", align 1
@__java_type_names.115 = internal constant [51 x i8] c"java/nio/channels/spi/AbstractInterruptibleChannel\00", align 1
@__java_type_names.116 = internal constant [18 x i8] c"java/lang/Boolean\00", align 1
@__java_type_names.117 = internal constant [15 x i8] c"java/lang/Byte\00", align 1
@__java_type_names.118 = internal constant [20 x i8] c"java/lang/Character\00", align 1
@__java_type_names.119 = internal constant [16 x i8] c"java/lang/Class\00", align 1
@__java_type_names.120 = internal constant [33 x i8] c"java/lang/ClassNotFoundException\00", align 1
@__java_type_names.121 = internal constant [17 x i8] c"java/lang/Double\00", align 1
@__java_type_names.122 = internal constant [20 x i8] c"java/lang/Exception\00", align 1
@__java_type_names.123 = internal constant [16 x i8] c"java/lang/Float\00", align 1
@__java_type_names.124 = internal constant [23 x i8] c"java/lang/CharSequence\00", align 1
@__java_type_names.125 = internal constant [18 x i8] c"java/lang/Integer\00", align 1
@__java_type_names.126 = internal constant [15 x i8] c"java/lang/Long\00", align 1
@__java_type_names.127 = internal constant [17 x i8] c"java/lang/Object\00", align 1
@__java_type_names.128 = internal constant [27 x i8] c"java/lang/RuntimeException\00", align 1
@__java_type_names.129 = internal constant [16 x i8] c"java/lang/Short\00", align 1
@__java_type_names.130 = internal constant [17 x i8] c"java/lang/String\00", align 1
@__java_type_names.131 = internal constant [17 x i8] c"java/lang/Thread\00", align 1
@__java_type_names.132 = internal constant [35 x i8] c"mono/java/lang/RunnableImplementor\00", align 1
@__java_type_names.133 = internal constant [20 x i8] c"java/lang/Throwable\00", align 1
@__java_type_names.134 = internal constant [29 x i8] c"java/lang/ClassCastException\00", align 1
@__java_type_names.135 = internal constant [16 x i8] c"java/lang/Error\00", align 1
@__java_type_names.136 = internal constant [21 x i8] c"java/lang/Appendable\00", align 1
@__java_type_names.137 = internal constant [20 x i8] c"java/lang/Cloneable\00", align 1
@__java_type_names.138 = internal constant [21 x i8] c"java/lang/Comparable\00", align 1
@__java_type_names.139 = internal constant [35 x i8] c"java/lang/IllegalArgumentException\00", align 1
@__java_type_names.140 = internal constant [32 x i8] c"java/lang/IllegalStateException\00", align 1
@__java_type_names.141 = internal constant [36 x i8] c"java/lang/IndexOutOfBoundsException\00", align 1
@__java_type_names.142 = internal constant [19 x i8] c"java/lang/Runnable\00", align 1
@__java_type_names.143 = internal constant [23 x i8] c"java/lang/LinkageError\00", align 1
@__java_type_names.144 = internal constant [31 x i8] c"java/lang/NoClassDefFoundError\00", align 1
@__java_type_names.145 = internal constant [31 x i8] c"java/lang/NullPointerException\00", align 1
@__java_type_names.146 = internal constant [17 x i8] c"java/lang/Number\00", align 1
@__java_type_names.147 = internal constant [39 x i8] c"java/lang/ReflectiveOperationException\00", align 1
@__java_type_names.148 = internal constant [40 x i8] c"java/lang/UnsupportedOperationException\00", align 1
@__java_type_names.149 = internal constant [32 x i8] c"java/lang/annotation/Annotation\00", align 1
@__java_type_names.150 = internal constant [35 x i8] c"java/lang/reflect/AnnotatedElement\00", align 1
@__java_type_names.151 = internal constant [37 x i8] c"java/lang/reflect/GenericDeclaration\00", align 1
@__java_type_names.152 = internal constant [23 x i8] c"java/lang/reflect/Type\00", align 1
@__java_type_names.153 = internal constant [31 x i8] c"java/lang/reflect/TypeVariable\00", align 1
@__java_type_names.154 = internal constant [24 x i8] c"java/io/FileInputStream\00", align 1
@__java_type_names.155 = internal constant [18 x i8] c"java/io/Closeable\00", align 1
@__java_type_names.156 = internal constant [18 x i8] c"java/io/Flushable\00", align 1
@__java_type_names.157 = internal constant [20 x i8] c"java/io/InputStream\00", align 1
@__java_type_names.158 = internal constant [20 x i8] c"java/io/IOException\00", align 1
@__java_type_names.159 = internal constant [21 x i8] c"java/io/Serializable\00", align 1
@__java_type_names.160 = internal constant [21 x i8] c"java/io/OutputStream\00", align 1
@__java_type_names.161 = internal constant [20 x i8] c"java/io/PrintWriter\00", align 1
@__java_type_names.162 = internal constant [21 x i8] c"java/io/StringWriter\00", align 1
@__java_type_names.163 = internal constant [15 x i8] c"java/io/Writer\00", align 1
@__java_type_names.164 = internal constant [25 x i8] c"mono/android/TypeManager\00", align 1

@java_type_names = local_unnamed_addr constant [165 x i8*] [
	i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__java_type_names.0, i32 0, i32 0),
	i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__java_type_names.1, i32 0, i32 0),
	i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__java_type_names.2, i32 0, i32 0),
	i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__java_type_names.3, i32 0, i32 0),
	i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__java_type_names.4, i32 0, i32 0),
	i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__java_type_names.5, i32 0, i32 0),
	i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__java_type_names.6, i32 0, i32 0),
	i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__java_type_names.7, i32 0, i32 0),
	i8* getelementptr inbounds ([53 x i8], [53 x i8]* @__java_type_names.8, i32 0, i32 0),
	i8* getelementptr inbounds ([49 x i8], [49 x i8]* @__java_type_names.9, i32 0, i32 0),
	i8* getelementptr inbounds ([54 x i8], [54 x i8]* @__java_type_names.10, i32 0, i32 0),
	i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__java_type_names.11, i32 0, i32 0),
	i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__java_type_names.12, i32 0, i32 0),
	i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__java_type_names.13, i32 0, i32 0),
	i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__java_type_names.14, i32 0, i32 0),
	i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__java_type_names.15, i32 0, i32 0),
	i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__java_type_names.16, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.17, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.18, i32 0, i32 0),
	i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__java_type_names.19, i32 0, i32 0),
	i8* getelementptr inbounds ([50 x i8], [50 x i8]* @__java_type_names.20, i32 0, i32 0),
	i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__java_type_names.21, i32 0, i32 0),
	i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__java_type_names.22, i32 0, i32 0),
	i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__java_type_names.23, i32 0, i32 0),
	i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__java_type_names.24, i32 0, i32 0),
	i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__java_type_names.25, i32 0, i32 0),
	i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__java_type_names.26, i32 0, i32 0),
	i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__java_type_names.27, i32 0, i32 0),
	i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__java_type_names.28, i32 0, i32 0),
	i8* getelementptr inbounds ([48 x i8], [48 x i8]* @__java_type_names.29, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.30, i32 0, i32 0),
	i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__java_type_names.31, i32 0, i32 0),
	i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__java_type_names.32, i32 0, i32 0),
	i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__java_type_names.33, i32 0, i32 0),
	i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__java_type_names.34, i32 0, i32 0),
	i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__java_type_names.35, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.36, i32 0, i32 0),
	i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__java_type_names.37, i32 0, i32 0),
	i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__java_type_names.38, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.39, i32 0, i32 0),
	i8* getelementptr inbounds ([45 x i8], [45 x i8]* @__java_type_names.40, i32 0, i32 0),
	i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__java_type_names.41, i32 0, i32 0),
	i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__java_type_names.42, i32 0, i32 0),
	i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__java_type_names.43, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.44, i32 0, i32 0),
	i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__java_type_names.45, i32 0, i32 0),
	i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__java_type_names.46, i32 0, i32 0),
	i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__java_type_names.47, i32 0, i32 0),
	i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__java_type_names.48, i32 0, i32 0),
	i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__java_type_names.49, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.50, i32 0, i32 0),
	i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__java_type_names.51, i32 0, i32 0),
	i8* getelementptr inbounds ([46 x i8], [46 x i8]* @__java_type_names.52, i32 0, i32 0),
	i8* getelementptr inbounds ([47 x i8], [47 x i8]* @__java_type_names.53, i32 0, i32 0),
	i8* getelementptr inbounds ([52 x i8], [52 x i8]* @__java_type_names.54, i32 0, i32 0),
	i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__java_type_names.55, i32 0, i32 0),
	i8* getelementptr inbounds ([28 x i8], [28 x i8]* @__java_type_names.56, i32 0, i32 0),
	i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__java_type_names.57, i32 0, i32 0),
	i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__java_type_names.58, i32 0, i32 0),
	i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__java_type_names.59, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.60, i32 0, i32 0),
	i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__java_type_names.61, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.62, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.63, i32 0, i32 0),
	i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__java_type_names.64, i32 0, i32 0),
	i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__java_type_names.65, i32 0, i32 0),
	i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__java_type_names.66, i32 0, i32 0),
	i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__java_type_names.67, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.68, i32 0, i32 0),
	i8* getelementptr inbounds ([22 x i8], [22 x i8]* @__java_type_names.69, i32 0, i32 0),
	i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__java_type_names.70, i32 0, i32 0),
	i8* getelementptr inbounds ([44 x i8], [44 x i8]* @__java_type_names.71, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.72, i32 0, i32 0),
	i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__java_type_names.73, i32 0, i32 0),
	i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__java_type_names.74, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.75, i32 0, i32 0),
	i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__java_type_names.76, i32 0, i32 0),
	i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__java_type_names.77, i32 0, i32 0),
	i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__java_type_names.78, i32 0, i32 0),
	i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__java_type_names.79, i32 0, i32 0),
	i8* getelementptr inbounds ([34 x i8], [34 x i8]* @__java_type_names.80, i32 0, i32 0),
	i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__java_type_names.81, i32 0, i32 0),
	i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__java_type_names.82, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.83, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.84, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.85, i32 0, i32 0),
	i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__java_type_names.86, i32 0, i32 0),
	i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__java_type_names.87, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.88, i32 0, i32 0),
	i8* getelementptr inbounds ([41 x i8], [41 x i8]* @__java_type_names.89, i32 0, i32 0),
	i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__java_type_names.90, i32 0, i32 0),
	i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__java_type_names.91, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.92, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.93, i32 0, i32 0),
	i8* getelementptr inbounds ([13 x i8], [13 x i8]* @__java_type_names.94, i32 0, i32 0),
	i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__java_type_names.95, i32 0, i32 0),
	i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__java_type_names.96, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.97, i32 0, i32 0),
	i8* getelementptr inbounds ([42 x i8], [42 x i8]* @__java_type_names.98, i32 0, i32 0),
	i8* getelementptr inbounds ([43 x i8], [43 x i8]* @__java_type_names.99, i32 0, i32 0),
	i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__java_type_names.100, i32 0, i32 0),
	i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__java_type_names.101, i32 0, i32 0),
	i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__java_type_names.102, i32 0, i32 0),
	i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__java_type_names.103, i32 0, i32 0),
	i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__java_type_names.104, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.105, i32 0, i32 0),
	i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__java_type_names.106, i32 0, i32 0),
	i8* getelementptr inbounds ([30 x i8], [30 x i8]* @__java_type_names.107, i32 0, i32 0),
	i8* getelementptr inbounds ([26 x i8], [26 x i8]* @__java_type_names.108, i32 0, i32 0),
	i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__java_type_names.109, i32 0, i32 0),
	i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__java_type_names.110, i32 0, i32 0),
	i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__java_type_names.111, i32 0, i32 0),
	i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__java_type_names.112, i32 0, i32 0),
	i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__java_type_names.113, i32 0, i32 0),
	i8* getelementptr inbounds ([38 x i8], [38 x i8]* @__java_type_names.114, i32 0, i32 0),
	i8* getelementptr inbounds ([51 x i8], [51 x i8]* @__java_type_names.115, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.116, i32 0, i32 0),
	i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__java_type_names.117, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.118, i32 0, i32 0),
	i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__java_type_names.119, i32 0, i32 0),
	i8* getelementptr inbounds ([33 x i8], [33 x i8]* @__java_type_names.120, i32 0, i32 0),
	i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__java_type_names.121, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.122, i32 0, i32 0),
	i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__java_type_names.123, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.124, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.125, i32 0, i32 0),
	i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__java_type_names.126, i32 0, i32 0),
	i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__java_type_names.127, i32 0, i32 0),
	i8* getelementptr inbounds ([27 x i8], [27 x i8]* @__java_type_names.128, i32 0, i32 0),
	i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__java_type_names.129, i32 0, i32 0),
	i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__java_type_names.130, i32 0, i32 0),
	i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__java_type_names.131, i32 0, i32 0),
	i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__java_type_names.132, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.133, i32 0, i32 0),
	i8* getelementptr inbounds ([29 x i8], [29 x i8]* @__java_type_names.134, i32 0, i32 0),
	i8* getelementptr inbounds ([16 x i8], [16 x i8]* @__java_type_names.135, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.136, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.137, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.138, i32 0, i32 0),
	i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__java_type_names.139, i32 0, i32 0),
	i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__java_type_names.140, i32 0, i32 0),
	i8* getelementptr inbounds ([36 x i8], [36 x i8]* @__java_type_names.141, i32 0, i32 0),
	i8* getelementptr inbounds ([19 x i8], [19 x i8]* @__java_type_names.142, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.143, i32 0, i32 0),
	i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__java_type_names.144, i32 0, i32 0),
	i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__java_type_names.145, i32 0, i32 0),
	i8* getelementptr inbounds ([17 x i8], [17 x i8]* @__java_type_names.146, i32 0, i32 0),
	i8* getelementptr inbounds ([39 x i8], [39 x i8]* @__java_type_names.147, i32 0, i32 0),
	i8* getelementptr inbounds ([40 x i8], [40 x i8]* @__java_type_names.148, i32 0, i32 0),
	i8* getelementptr inbounds ([32 x i8], [32 x i8]* @__java_type_names.149, i32 0, i32 0),
	i8* getelementptr inbounds ([35 x i8], [35 x i8]* @__java_type_names.150, i32 0, i32 0),
	i8* getelementptr inbounds ([37 x i8], [37 x i8]* @__java_type_names.151, i32 0, i32 0),
	i8* getelementptr inbounds ([23 x i8], [23 x i8]* @__java_type_names.152, i32 0, i32 0),
	i8* getelementptr inbounds ([31 x i8], [31 x i8]* @__java_type_names.153, i32 0, i32 0),
	i8* getelementptr inbounds ([24 x i8], [24 x i8]* @__java_type_names.154, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.155, i32 0, i32 0),
	i8* getelementptr inbounds ([18 x i8], [18 x i8]* @__java_type_names.156, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.157, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.158, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.159, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.160, i32 0, i32 0),
	i8* getelementptr inbounds ([20 x i8], [20 x i8]* @__java_type_names.161, i32 0, i32 0),
	i8* getelementptr inbounds ([21 x i8], [21 x i8]* @__java_type_names.162, i32 0, i32 0),
	i8* getelementptr inbounds ([15 x i8], [15 x i8]* @__java_type_names.163, i32 0, i32 0),
	i8* getelementptr inbounds ([25 x i8], [25 x i8]* @__java_type_names.164, i32 0, i32 0)
], align 8

!llvm.module.flags = !{!0, !1, !2, !3, !4, !5}
!llvm.ident = !{!6}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"branch-target-enforcement", i32 0}
!3 = !{i32 1, !"sign-return-address", i32 0}
!4 = !{i32 1, !"sign-return-address-all", i32 0}
!5 = !{i32 1, !"sign-return-address-with-bkey", i32 0}
!6 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
