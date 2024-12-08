; ModuleID = 'obj\Release\90\android\typemaps.armeabi-v7a.ll'
source_filename = "obj\Release\90\android\typemaps.armeabi-v7a.ll"
target datalayout = "e-m:e-p:32:32-Fi8-i64:64-v128:64:128-a:0:32-n32-S64"
target triple = "armv7-unknown-linux-android"


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
		i32 73; java_map_index
	}, 
	; 1
	%struct.TypeMapModuleEntry {
		i32 33554442, ; type_token_id
		i32 125; java_map_index
	}, 
	; 2
	%struct.TypeMapModuleEntry {
		i32 33554443, ; type_token_id
		i32 93; java_map_index
	}, 
	; 3
	%struct.TypeMapModuleEntry {
		i32 33554445, ; type_token_id
		i32 158; java_map_index
	}, 
	; 4
	%struct.TypeMapModuleEntry {
		i32 33554447, ; type_token_id
		i32 44; java_map_index
	}, 
	; 5
	%struct.TypeMapModuleEntry {
		i32 33554449, ; type_token_id
		i32 136; java_map_index
	}, 
	; 6
	%struct.TypeMapModuleEntry {
		i32 33554451, ; type_token_id
		i32 52; java_map_index
	}, 
	; 7
	%struct.TypeMapModuleEntry {
		i32 33554453, ; type_token_id
		i32 137; java_map_index
	}, 
	; 8
	%struct.TypeMapModuleEntry {
		i32 33554455, ; type_token_id
		i32 115; java_map_index
	}, 
	; 9
	%struct.TypeMapModuleEntry {
		i32 33554457, ; type_token_id
		i32 76; java_map_index
	}, 
	; 10
	%struct.TypeMapModuleEntry {
		i32 33554460, ; type_token_id
		i32 112; java_map_index
	}
], align 4; end of 'module0_managed_to_java' array


; module0_managed_to_java_duplicates
@module0_managed_to_java_duplicates = internal constant [9 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554444, ; type_token_id
		i32 93; java_map_index
	}, 
	; 1
	%struct.TypeMapModuleEntry {
		i32 33554446, ; type_token_id
		i32 158; java_map_index
	}, 
	; 2
	%struct.TypeMapModuleEntry {
		i32 33554448, ; type_token_id
		i32 44; java_map_index
	}, 
	; 3
	%struct.TypeMapModuleEntry {
		i32 33554450, ; type_token_id
		i32 136; java_map_index
	}, 
	; 4
	%struct.TypeMapModuleEntry {
		i32 33554452, ; type_token_id
		i32 52; java_map_index
	}, 
	; 5
	%struct.TypeMapModuleEntry {
		i32 33554454, ; type_token_id
		i32 137; java_map_index
	}, 
	; 6
	%struct.TypeMapModuleEntry {
		i32 33554456, ; type_token_id
		i32 115; java_map_index
	}, 
	; 7
	%struct.TypeMapModuleEntry {
		i32 33554458, ; type_token_id
		i32 76; java_map_index
	}, 
	; 8
	%struct.TypeMapModuleEntry {
		i32 33554459, ; type_token_id
		i32 73; java_map_index
	}
], align 4; end of 'module0_managed_to_java_duplicates' array


; module1_managed_to_java
@module1_managed_to_java = internal constant [1 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554434, ; type_token_id
		i32 92; java_map_index
	}
], align 4; end of 'module1_managed_to_java' array


; module2_managed_to_java
@module2_managed_to_java = internal constant [153 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554523, ; type_token_id
		i32 37; java_map_index
	}, 
	; 1
	%struct.TypeMapModuleEntry {
		i32 33554525, ; type_token_id
		i32 138; java_map_index
	}, 
	; 2
	%struct.TypeMapModuleEntry {
		i32 33554527, ; type_token_id
		i32 156; java_map_index
	}, 
	; 3
	%struct.TypeMapModuleEntry {
		i32 33554537, ; type_token_id
		i32 103; java_map_index
	}, 
	; 4
	%struct.TypeMapModuleEntry {
		i32 33554538, ; type_token_id
		i32 63; java_map_index
	}, 
	; 5
	%struct.TypeMapModuleEntry {
		i32 33554539, ; type_token_id
		i32 145; java_map_index
	}, 
	; 6
	%struct.TypeMapModuleEntry {
		i32 33554541, ; type_token_id
		i32 69; java_map_index
	}, 
	; 7
	%struct.TypeMapModuleEntry {
		i32 33554542, ; type_token_id
		i32 78; java_map_index
	}, 
	; 8
	%struct.TypeMapModuleEntry {
		i32 33554544, ; type_token_id
		i32 153; java_map_index
	}, 
	; 9
	%struct.TypeMapModuleEntry {
		i32 33554545, ; type_token_id
		i32 40; java_map_index
	}, 
	; 10
	%struct.TypeMapModuleEntry {
		i32 33554550, ; type_token_id
		i32 6; java_map_index
	}, 
	; 11
	%struct.TypeMapModuleEntry {
		i32 33554551, ; type_token_id
		i32 41; java_map_index
	}, 
	; 12
	%struct.TypeMapModuleEntry {
		i32 33554553, ; type_token_id
		i32 126; java_map_index
	}, 
	; 13
	%struct.TypeMapModuleEntry {
		i32 33554554, ; type_token_id
		i32 22; java_map_index
	}, 
	; 14
	%struct.TypeMapModuleEntry {
		i32 33554556, ; type_token_id
		i32 140; java_map_index
	}, 
	; 15
	%struct.TypeMapModuleEntry {
		i32 33554558, ; type_token_id
		i32 102; java_map_index
	}, 
	; 16
	%struct.TypeMapModuleEntry {
		i32 33554559, ; type_token_id
		i32 28; java_map_index
	}, 
	; 17
	%struct.TypeMapModuleEntry {
		i32 33554560, ; type_token_id
		i32 133; java_map_index
	}, 
	; 18
	%struct.TypeMapModuleEntry {
		i32 33554562, ; type_token_id
		i32 135; java_map_index
	}, 
	; 19
	%struct.TypeMapModuleEntry {
		i32 33554563, ; type_token_id
		i32 53; java_map_index
	}, 
	; 20
	%struct.TypeMapModuleEntry {
		i32 33554565, ; type_token_id
		i32 108; java_map_index
	}, 
	; 21
	%struct.TypeMapModuleEntry {
		i32 33554566, ; type_token_id
		i32 117; java_map_index
	}, 
	; 22
	%struct.TypeMapModuleEntry {
		i32 33554570, ; type_token_id
		i32 131; java_map_index
	}, 
	; 23
	%struct.TypeMapModuleEntry {
		i32 33554572, ; type_token_id
		i32 89; java_map_index
	}, 
	; 24
	%struct.TypeMapModuleEntry {
		i32 33554573, ; type_token_id
		i32 65; java_map_index
	}, 
	; 25
	%struct.TypeMapModuleEntry {
		i32 33554575, ; type_token_id
		i32 99; java_map_index
	}, 
	; 26
	%struct.TypeMapModuleEntry {
		i32 33554577, ; type_token_id
		i32 143; java_map_index
	}, 
	; 27
	%struct.TypeMapModuleEntry {
		i32 33554579, ; type_token_id
		i32 162; java_map_index
	}, 
	; 28
	%struct.TypeMapModuleEntry {
		i32 33554581, ; type_token_id
		i32 13; java_map_index
	}, 
	; 29
	%struct.TypeMapModuleEntry {
		i32 33554583, ; type_token_id
		i32 161; java_map_index
	}, 
	; 30
	%struct.TypeMapModuleEntry {
		i32 33554585, ; type_token_id
		i32 121; java_map_index
	}, 
	; 31
	%struct.TypeMapModuleEntry {
		i32 33554587, ; type_token_id
		i32 109; java_map_index
	}, 
	; 32
	%struct.TypeMapModuleEntry {
		i32 33554589, ; type_token_id
		i32 50; java_map_index
	}, 
	; 33
	%struct.TypeMapModuleEntry {
		i32 33554591, ; type_token_id
		i32 59; java_map_index
	}, 
	; 34
	%struct.TypeMapModuleEntry {
		i32 33554593, ; type_token_id
		i32 23; java_map_index
	}, 
	; 35
	%struct.TypeMapModuleEntry {
		i32 33554595, ; type_token_id
		i32 64; java_map_index
	}, 
	; 36
	%struct.TypeMapModuleEntry {
		i32 33554596, ; type_token_id
		i32 118; java_map_index
	}, 
	; 37
	%struct.TypeMapModuleEntry {
		i32 33554604, ; type_token_id
		i32 81; java_map_index
	}, 
	; 38
	%struct.TypeMapModuleEntry {
		i32 33554608, ; type_token_id
		i32 3; java_map_index
	}, 
	; 39
	%struct.TypeMapModuleEntry {
		i32 33554609, ; type_token_id
		i32 15; java_map_index
	}, 
	; 40
	%struct.TypeMapModuleEntry {
		i32 33554612, ; type_token_id
		i32 129; java_map_index
	}, 
	; 41
	%struct.TypeMapModuleEntry {
		i32 33554613, ; type_token_id
		i32 88; java_map_index
	}, 
	; 42
	%struct.TypeMapModuleEntry {
		i32 33554617, ; type_token_id
		i32 12; java_map_index
	}, 
	; 43
	%struct.TypeMapModuleEntry {
		i32 33554619, ; type_token_id
		i32 104; java_map_index
	}, 
	; 44
	%struct.TypeMapModuleEntry {
		i32 33554620, ; type_token_id
		i32 4; java_map_index
	}, 
	; 45
	%struct.TypeMapModuleEntry {
		i32 33554621, ; type_token_id
		i32 110; java_map_index
	}, 
	; 46
	%struct.TypeMapModuleEntry {
		i32 33554623, ; type_token_id
		i32 66; java_map_index
	}, 
	; 47
	%struct.TypeMapModuleEntry {
		i32 33554624, ; type_token_id
		i32 75; java_map_index
	}, 
	; 48
	%struct.TypeMapModuleEntry {
		i32 33554625, ; type_token_id
		i32 29; java_map_index
	}, 
	; 49
	%struct.TypeMapModuleEntry {
		i32 33554626, ; type_token_id
		i32 160; java_map_index
	}, 
	; 50
	%struct.TypeMapModuleEntry {
		i32 33554628, ; type_token_id
		i32 155; java_map_index
	}, 
	; 51
	%struct.TypeMapModuleEntry {
		i32 33554629, ; type_token_id
		i32 24; java_map_index
	}, 
	; 52
	%struct.TypeMapModuleEntry {
		i32 33554631, ; type_token_id
		i32 163; java_map_index
	}, 
	; 53
	%struct.TypeMapModuleEntry {
		i32 33554634, ; type_token_id
		i32 82; java_map_index
	}, 
	; 54
	%struct.TypeMapModuleEntry {
		i32 33554635, ; type_token_id
		i32 120; java_map_index
	}, 
	; 55
	%struct.TypeMapModuleEntry {
		i32 33554637, ; type_token_id
		i32 43; java_map_index
	}, 
	; 56
	%struct.TypeMapModuleEntry {
		i32 33554640, ; type_token_id
		i32 77; java_map_index
	}, 
	; 57
	%struct.TypeMapModuleEntry {
		i32 33554641, ; type_token_id
		i32 144; java_map_index
	}, 
	; 58
	%struct.TypeMapModuleEntry {
		i32 33554642, ; type_token_id
		i32 105; java_map_index
	}, 
	; 59
	%struct.TypeMapModuleEntry {
		i32 33554643, ; type_token_id
		i32 134; java_map_index
	}, 
	; 60
	%struct.TypeMapModuleEntry {
		i32 33554647, ; type_token_id
		i32 85; java_map_index
	}, 
	; 61
	%struct.TypeMapModuleEntry {
		i32 33554648, ; type_token_id
		i32 94; java_map_index
	}, 
	; 62
	%struct.TypeMapModuleEntry {
		i32 33554653, ; type_token_id
		i32 150; java_map_index
	}, 
	; 63
	%struct.TypeMapModuleEntry {
		i32 33554654, ; type_token_id
		i32 87; java_map_index
	}, 
	; 64
	%struct.TypeMapModuleEntry {
		i32 33554655, ; type_token_id
		i32 57; java_map_index
	}, 
	; 65
	%struct.TypeMapModuleEntry {
		i32 33554657, ; type_token_id
		i32 147; java_map_index
	}, 
	; 66
	%struct.TypeMapModuleEntry {
		i32 33554658, ; type_token_id
		i32 97; java_map_index
	}, 
	; 67
	%struct.TypeMapModuleEntry {
		i32 33554660, ; type_token_id
		i32 149; java_map_index
	}, 
	; 68
	%struct.TypeMapModuleEntry {
		i32 33554663, ; type_token_id
		i32 91; java_map_index
	}, 
	; 69
	%struct.TypeMapModuleEntry {
		i32 33554685, ; type_token_id
		i32 146; java_map_index
	}, 
	; 70
	%struct.TypeMapModuleEntry {
		i32 33554687, ; type_token_id
		i32 113; java_map_index
	}, 
	; 71
	%struct.TypeMapModuleEntry {
		i32 33554689, ; type_token_id
		i32 55; java_map_index
	}, 
	; 72
	%struct.TypeMapModuleEntry {
		i32 33554691, ; type_token_id
		i32 60; java_map_index
	}, 
	; 73
	%struct.TypeMapModuleEntry {
		i32 33554700, ; type_token_id
		i32 31; java_map_index
	}, 
	; 74
	%struct.TypeMapModuleEntry {
		i32 33554702, ; type_token_id
		i32 130; java_map_index
	}, 
	; 75
	%struct.TypeMapModuleEntry {
		i32 33554704, ; type_token_id
		i32 124; java_map_index
	}, 
	; 76
	%struct.TypeMapModuleEntry {
		i32 33554705, ; type_token_id
		i32 159; java_map_index
	}, 
	; 77
	%struct.TypeMapModuleEntry {
		i32 33554721, ; type_token_id
		i32 25; java_map_index
	}, 
	; 78
	%struct.TypeMapModuleEntry {
		i32 33554731, ; type_token_id
		i32 128; java_map_index
	}, 
	; 79
	%struct.TypeMapModuleEntry {
		i32 33554732, ; type_token_id
		i32 122; java_map_index
	}, 
	; 80
	%struct.TypeMapModuleEntry {
		i32 33554733, ; type_token_id
		i32 106; java_map_index
	}, 
	; 81
	%struct.TypeMapModuleEntry {
		i32 33554735, ; type_token_id
		i32 45; java_map_index
	}, 
	; 82
	%struct.TypeMapModuleEntry {
		i32 33554737, ; type_token_id
		i32 14; java_map_index
	}, 
	; 83
	%struct.TypeMapModuleEntry {
		i32 33554738, ; type_token_id
		i32 19; java_map_index
	}, 
	; 84
	%struct.TypeMapModuleEntry {
		i32 33554740, ; type_token_id
		i32 62; java_map_index
	}, 
	; 85
	%struct.TypeMapModuleEntry {
		i32 33554742, ; type_token_id
		i32 142; java_map_index
	}, 
	; 86
	%struct.TypeMapModuleEntry {
		i32 33554743, ; type_token_id
		i32 17; java_map_index
	}, 
	; 87
	%struct.TypeMapModuleEntry {
		i32 33554745, ; type_token_id
		i32 46; java_map_index
	}, 
	; 88
	%struct.TypeMapModuleEntry {
		i32 33554747, ; type_token_id
		i32 2; java_map_index
	}, 
	; 89
	%struct.TypeMapModuleEntry {
		i32 33554749, ; type_token_id
		i32 79; java_map_index
	}, 
	; 90
	%struct.TypeMapModuleEntry {
		i32 33554750, ; type_token_id
		i32 90; java_map_index
	}, 
	; 91
	%struct.TypeMapModuleEntry {
		i32 33554752, ; type_token_id
		i32 101; java_map_index
	}, 
	; 92
	%struct.TypeMapModuleEntry {
		i32 33554754, ; type_token_id
		i32 152; java_map_index
	}, 
	; 93
	%struct.TypeMapModuleEntry {
		i32 33554756, ; type_token_id
		i32 39; java_map_index
	}, 
	; 94
	%struct.TypeMapModuleEntry {
		i32 33554758, ; type_token_id
		i32 26; java_map_index
	}, 
	; 95
	%struct.TypeMapModuleEntry {
		i32 33554760, ; type_token_id
		i32 67; java_map_index
	}, 
	; 96
	%struct.TypeMapModuleEntry {
		i32 33554762, ; type_token_id
		i32 9; java_map_index
	}, 
	; 97
	%struct.TypeMapModuleEntry {
		i32 33554764, ; type_token_id
		i32 42; java_map_index
	}, 
	; 98
	%struct.TypeMapModuleEntry {
		i32 33554766, ; type_token_id
		i32 83; java_map_index
	}, 
	; 99
	%struct.TypeMapModuleEntry {
		i32 33554768, ; type_token_id
		i32 98; java_map_index
	}, 
	; 100
	%struct.TypeMapModuleEntry {
		i32 33554770, ; type_token_id
		i32 68; java_map_index
	}, 
	; 101
	%struct.TypeMapModuleEntry {
		i32 33554772, ; type_token_id
		i32 5; java_map_index
	}, 
	; 102
	%struct.TypeMapModuleEntry {
		i32 33554774, ; type_token_id
		i32 10; java_map_index
	}, 
	; 103
	%struct.TypeMapModuleEntry {
		i32 33554776, ; type_token_id
		i32 61; java_map_index
	}, 
	; 104
	%struct.TypeMapModuleEntry {
		i32 33554778, ; type_token_id
		i32 86; java_map_index
	}, 
	; 105
	%struct.TypeMapModuleEntry {
		i32 33554779, ; type_token_id
		i32 72; java_map_index
	}, 
	; 106
	%struct.TypeMapModuleEntry {
		i32 33554780, ; type_token_id
		i32 74; java_map_index
	}, 
	; 107
	%struct.TypeMapModuleEntry {
		i32 33554781, ; type_token_id
		i32 139; java_map_index
	}, 
	; 108
	%struct.TypeMapModuleEntry {
		i32 33554782, ; type_token_id
		i32 49; java_map_index
	}, 
	; 109
	%struct.TypeMapModuleEntry {
		i32 33554783, ; type_token_id
		i32 132; java_map_index
	}, 
	; 110
	%struct.TypeMapModuleEntry {
		i32 33554784, ; type_token_id
		i32 111; java_map_index
	}, 
	; 111
	%struct.TypeMapModuleEntry {
		i32 33554785, ; type_token_id
		i32 107; java_map_index
	}, 
	; 112
	%struct.TypeMapModuleEntry {
		i32 33554786, ; type_token_id
		i32 32; java_map_index
	}, 
	; 113
	%struct.TypeMapModuleEntry {
		i32 33554787, ; type_token_id
		i32 34; java_map_index
	}, 
	; 114
	%struct.TypeMapModuleEntry {
		i32 33554788, ; type_token_id
		i32 18; java_map_index
	}, 
	; 115
	%struct.TypeMapModuleEntry {
		i32 33554789, ; type_token_id
		i32 0; java_map_index
	}, 
	; 116
	%struct.TypeMapModuleEntry {
		i32 33554790, ; type_token_id
		i32 70; java_map_index
	}, 
	; 117
	%struct.TypeMapModuleEntry {
		i32 33554791, ; type_token_id
		i32 38; java_map_index
	}, 
	; 118
	%struct.TypeMapModuleEntry {
		i32 33554792, ; type_token_id
		i32 127; java_map_index
	}, 
	; 119
	%struct.TypeMapModuleEntry {
		i32 33554794, ; type_token_id
		i32 35; java_map_index
	}, 
	; 120
	%struct.TypeMapModuleEntry {
		i32 33554795, ; type_token_id
		i32 16; java_map_index
	}, 
	; 121
	%struct.TypeMapModuleEntry {
		i32 33554796, ; type_token_id
		i32 54; java_map_index
	}, 
	; 122
	%struct.TypeMapModuleEntry {
		i32 33554797, ; type_token_id
		i32 56; java_map_index
	}, 
	; 123
	%struct.TypeMapModuleEntry {
		i32 33554798, ; type_token_id
		i32 148; java_map_index
	}, 
	; 124
	%struct.TypeMapModuleEntry {
		i32 33554799, ; type_token_id
		i32 51; java_map_index
	}, 
	; 125
	%struct.TypeMapModuleEntry {
		i32 33554803, ; type_token_id
		i32 48; java_map_index
	}, 
	; 126
	%struct.TypeMapModuleEntry {
		i32 33554805, ; type_token_id
		i32 58; java_map_index
	}, 
	; 127
	%struct.TypeMapModuleEntry {
		i32 33554807, ; type_token_id
		i32 8; java_map_index
	}, 
	; 128
	%struct.TypeMapModuleEntry {
		i32 33554808, ; type_token_id
		i32 27; java_map_index
	}, 
	; 129
	%struct.TypeMapModuleEntry {
		i32 33554809, ; type_token_id
		i32 71; java_map_index
	}, 
	; 130
	%struct.TypeMapModuleEntry {
		i32 33554810, ; type_token_id
		i32 80; java_map_index
	}, 
	; 131
	%struct.TypeMapModuleEntry {
		i32 33554812, ; type_token_id
		i32 20; java_map_index
	}, 
	; 132
	%struct.TypeMapModuleEntry {
		i32 33554813, ; type_token_id
		i32 151; java_map_index
	}, 
	; 133
	%struct.TypeMapModuleEntry {
		i32 33554814, ; type_token_id
		i32 36; java_map_index
	}, 
	; 134
	%struct.TypeMapModuleEntry {
		i32 33554815, ; type_token_id
		i32 123; java_map_index
	}, 
	; 135
	%struct.TypeMapModuleEntry {
		i32 33554817, ; type_token_id
		i32 11; java_map_index
	}, 
	; 136
	%struct.TypeMapModuleEntry {
		i32 33554818, ; type_token_id
		i32 96; java_map_index
	}, 
	; 137
	%struct.TypeMapModuleEntry {
		i32 33554819, ; type_token_id
		i32 47; java_map_index
	}, 
	; 138
	%struct.TypeMapModuleEntry {
		i32 33554821, ; type_token_id
		i32 21; java_map_index
	}, 
	; 139
	%struct.TypeMapModuleEntry {
		i32 33554823, ; type_token_id
		i32 1; java_map_index
	}, 
	; 140
	%struct.TypeMapModuleEntry {
		i32 33554825, ; type_token_id
		i32 95; java_map_index
	}, 
	; 141
	%struct.TypeMapModuleEntry {
		i32 33554827, ; type_token_id
		i32 7; java_map_index
	}, 
	; 142
	%struct.TypeMapModuleEntry {
		i32 33554829, ; type_token_id
		i32 100; java_map_index
	}, 
	; 143
	%struct.TypeMapModuleEntry {
		i32 33554830, ; type_token_id
		i32 164; java_map_index
	}, 
	; 144
	%struct.TypeMapModuleEntry {
		i32 33554832, ; type_token_id
		i32 119; java_map_index
	}, 
	; 145
	%struct.TypeMapModuleEntry {
		i32 33554834, ; type_token_id
		i32 141; java_map_index
	}, 
	; 146
	%struct.TypeMapModuleEntry {
		i32 33554836, ; type_token_id
		i32 157; java_map_index
	}, 
	; 147
	%struct.TypeMapModuleEntry {
		i32 33554837, ; type_token_id
		i32 30; java_map_index
	}, 
	; 148
	%struct.TypeMapModuleEntry {
		i32 33554839, ; type_token_id
		i32 84; java_map_index
	}, 
	; 149
	%struct.TypeMapModuleEntry {
		i32 33554841, ; type_token_id
		i32 33; java_map_index
	}, 
	; 150
	%struct.TypeMapModuleEntry {
		i32 33554842, ; type_token_id
		i32 116; java_map_index
	}, 
	; 151
	%struct.TypeMapModuleEntry {
		i32 33554843, ; type_token_id
		i32 154; java_map_index
	}, 
	; 152
	%struct.TypeMapModuleEntry {
		i32 33554866, ; type_token_id
		i32 114; java_map_index
	}
], align 4; end of 'module2_managed_to_java' array


; module2_managed_to_java_duplicates
@module2_managed_to_java_duplicates = internal constant [77 x %struct.TypeMapModuleEntry] [
	; 0
	%struct.TypeMapModuleEntry {
		i32 33554524, ; type_token_id
		i32 37; java_map_index
	}, 
	; 1
	%struct.TypeMapModuleEntry {
		i32 33554526, ; type_token_id
		i32 138; java_map_index
	}, 
	; 2
	%struct.TypeMapModuleEntry {
		i32 33554543, ; type_token_id
		i32 78; java_map_index
	}, 
	; 3
	%struct.TypeMapModuleEntry {
		i32 33554546, ; type_token_id
		i32 40; java_map_index
	}, 
	; 4
	%struct.TypeMapModuleEntry {
		i32 33554552, ; type_token_id
		i32 41; java_map_index
	}, 
	; 5
	%struct.TypeMapModuleEntry {
		i32 33554555, ; type_token_id
		i32 22; java_map_index
	}, 
	; 6
	%struct.TypeMapModuleEntry {
		i32 33554557, ; type_token_id
		i32 140; java_map_index
	}, 
	; 7
	%struct.TypeMapModuleEntry {
		i32 33554561, ; type_token_id
		i32 133; java_map_index
	}, 
	; 8
	%struct.TypeMapModuleEntry {
		i32 33554564, ; type_token_id
		i32 53; java_map_index
	}, 
	; 9
	%struct.TypeMapModuleEntry {
		i32 33554567, ; type_token_id
		i32 117; java_map_index
	}, 
	; 10
	%struct.TypeMapModuleEntry {
		i32 33554568, ; type_token_id
		i32 108; java_map_index
	}, 
	; 11
	%struct.TypeMapModuleEntry {
		i32 33554571, ; type_token_id
		i32 131; java_map_index
	}, 
	; 12
	%struct.TypeMapModuleEntry {
		i32 33554576, ; type_token_id
		i32 99; java_map_index
	}, 
	; 13
	%struct.TypeMapModuleEntry {
		i32 33554578, ; type_token_id
		i32 143; java_map_index
	}, 
	; 14
	%struct.TypeMapModuleEntry {
		i32 33554580, ; type_token_id
		i32 162; java_map_index
	}, 
	; 15
	%struct.TypeMapModuleEntry {
		i32 33554582, ; type_token_id
		i32 13; java_map_index
	}, 
	; 16
	%struct.TypeMapModuleEntry {
		i32 33554584, ; type_token_id
		i32 161; java_map_index
	}, 
	; 17
	%struct.TypeMapModuleEntry {
		i32 33554586, ; type_token_id
		i32 121; java_map_index
	}, 
	; 18
	%struct.TypeMapModuleEntry {
		i32 33554588, ; type_token_id
		i32 109; java_map_index
	}, 
	; 19
	%struct.TypeMapModuleEntry {
		i32 33554590, ; type_token_id
		i32 50; java_map_index
	}, 
	; 20
	%struct.TypeMapModuleEntry {
		i32 33554592, ; type_token_id
		i32 59; java_map_index
	}, 
	; 21
	%struct.TypeMapModuleEntry {
		i32 33554594, ; type_token_id
		i32 23; java_map_index
	}, 
	; 22
	%struct.TypeMapModuleEntry {
		i32 33554597, ; type_token_id
		i32 118; java_map_index
	}, 
	; 23
	%struct.TypeMapModuleEntry {
		i32 33554600, ; type_token_id
		i32 126; java_map_index
	}, 
	; 24
	%struct.TypeMapModuleEntry {
		i32 33554610, ; type_token_id
		i32 3; java_map_index
	}, 
	; 25
	%struct.TypeMapModuleEntry {
		i32 33554611, ; type_token_id
		i32 135; java_map_index
	}, 
	; 26
	%struct.TypeMapModuleEntry {
		i32 33554618, ; type_token_id
		i32 12; java_map_index
	}, 
	; 27
	%struct.TypeMapModuleEntry {
		i32 33554622, ; type_token_id
		i32 110; java_map_index
	}, 
	; 28
	%struct.TypeMapModuleEntry {
		i32 33554627, ; type_token_id
		i32 160; java_map_index
	}, 
	; 29
	%struct.TypeMapModuleEntry {
		i32 33554636, ; type_token_id
		i32 120; java_map_index
	}, 
	; 30
	%struct.TypeMapModuleEntry {
		i32 33554644, ; type_token_id
		i32 134; java_map_index
	}, 
	; 31
	%struct.TypeMapModuleEntry {
		i32 33554645, ; type_token_id
		i32 105; java_map_index
	}, 
	; 32
	%struct.TypeMapModuleEntry {
		i32 33554656, ; type_token_id
		i32 150; java_map_index
	}, 
	; 33
	%struct.TypeMapModuleEntry {
		i32 33554659, ; type_token_id
		i32 97; java_map_index
	}, 
	; 34
	%struct.TypeMapModuleEntry {
		i32 33554661, ; type_token_id
		i32 149; java_map_index
	}, 
	; 35
	%struct.TypeMapModuleEntry {
		i32 33554690, ; type_token_id
		i32 55; java_map_index
	}, 
	; 36
	%struct.TypeMapModuleEntry {
		i32 33554696, ; type_token_id
		i32 60; java_map_index
	}, 
	; 37
	%struct.TypeMapModuleEntry {
		i32 33554701, ; type_token_id
		i32 31; java_map_index
	}, 
	; 38
	%struct.TypeMapModuleEntry {
		i32 33554706, ; type_token_id
		i32 159; java_map_index
	}, 
	; 39
	%struct.TypeMapModuleEntry {
		i32 33554734, ; type_token_id
		i32 106; java_map_index
	}, 
	; 40
	%struct.TypeMapModuleEntry {
		i32 33554736, ; type_token_id
		i32 45; java_map_index
	}, 
	; 41
	%struct.TypeMapModuleEntry {
		i32 33554739, ; type_token_id
		i32 19; java_map_index
	}, 
	; 42
	%struct.TypeMapModuleEntry {
		i32 33554741, ; type_token_id
		i32 62; java_map_index
	}, 
	; 43
	%struct.TypeMapModuleEntry {
		i32 33554744, ; type_token_id
		i32 17; java_map_index
	}, 
	; 44
	%struct.TypeMapModuleEntry {
		i32 33554746, ; type_token_id
		i32 46; java_map_index
	}, 
	; 45
	%struct.TypeMapModuleEntry {
		i32 33554748, ; type_token_id
		i32 2; java_map_index
	}, 
	; 46
	%struct.TypeMapModuleEntry {
		i32 33554751, ; type_token_id
		i32 90; java_map_index
	}, 
	; 47
	%struct.TypeMapModuleEntry {
		i32 33554753, ; type_token_id
		i32 101; java_map_index
	}, 
	; 48
	%struct.TypeMapModuleEntry {
		i32 33554755, ; type_token_id
		i32 152; java_map_index
	}, 
	; 49
	%struct.TypeMapModuleEntry {
		i32 33554757, ; type_token_id
		i32 39; java_map_index
	}, 
	; 50
	%struct.TypeMapModuleEntry {
		i32 33554759, ; type_token_id
		i32 26; java_map_index
	}, 
	; 51
	%struct.TypeMapModuleEntry {
		i32 33554761, ; type_token_id
		i32 67; java_map_index
	}, 
	; 52
	%struct.TypeMapModuleEntry {
		i32 33554763, ; type_token_id
		i32 9; java_map_index
	}, 
	; 53
	%struct.TypeMapModuleEntry {
		i32 33554765, ; type_token_id
		i32 42; java_map_index
	}, 
	; 54
	%struct.TypeMapModuleEntry {
		i32 33554767, ; type_token_id
		i32 83; java_map_index
	}, 
	; 55
	%struct.TypeMapModuleEntry {
		i32 33554769, ; type_token_id
		i32 98; java_map_index
	}, 
	; 56
	%struct.TypeMapModuleEntry {
		i32 33554771, ; type_token_id
		i32 68; java_map_index
	}, 
	; 57
	%struct.TypeMapModuleEntry {
		i32 33554773, ; type_token_id
		i32 5; java_map_index
	}, 
	; 58
	%struct.TypeMapModuleEntry {
		i32 33554775, ; type_token_id
		i32 10; java_map_index
	}, 
	; 59
	%struct.TypeMapModuleEntry {
		i32 33554777, ; type_token_id
		i32 61; java_map_index
	}, 
	; 60
	%struct.TypeMapModuleEntry {
		i32 33554800, ; type_token_id
		i32 51; java_map_index
	}, 
	; 61
	%struct.TypeMapModuleEntry {
		i32 33554801, ; type_token_id
		i32 32; java_map_index
	}, 
	; 62
	%struct.TypeMapModuleEntry {
		i32 33554804, ; type_token_id
		i32 48; java_map_index
	}, 
	; 63
	%struct.TypeMapModuleEntry {
		i32 33554806, ; type_token_id
		i32 58; java_map_index
	}, 
	; 64
	%struct.TypeMapModuleEntry {
		i32 33554811, ; type_token_id
		i32 80; java_map_index
	}, 
	; 65
	%struct.TypeMapModuleEntry {
		i32 33554816, ; type_token_id
		i32 123; java_map_index
	}, 
	; 66
	%struct.TypeMapModuleEntry {
		i32 33554820, ; type_token_id
		i32 47; java_map_index
	}, 
	; 67
	%struct.TypeMapModuleEntry {
		i32 33554822, ; type_token_id
		i32 21; java_map_index
	}, 
	; 68
	%struct.TypeMapModuleEntry {
		i32 33554824, ; type_token_id
		i32 1; java_map_index
	}, 
	; 69
	%struct.TypeMapModuleEntry {
		i32 33554826, ; type_token_id
		i32 95; java_map_index
	}, 
	; 70
	%struct.TypeMapModuleEntry {
		i32 33554828, ; type_token_id
		i32 7; java_map_index
	}, 
	; 71
	%struct.TypeMapModuleEntry {
		i32 33554831, ; type_token_id
		i32 164; java_map_index
	}, 
	; 72
	%struct.TypeMapModuleEntry {
		i32 33554833, ; type_token_id
		i32 119; java_map_index
	}, 
	; 73
	%struct.TypeMapModuleEntry {
		i32 33554835, ; type_token_id
		i32 141; java_map_index
	}, 
	; 74
	%struct.TypeMapModuleEntry {
		i32 33554838, ; type_token_id
		i32 30; java_map_index
	}, 
	; 75
	%struct.TypeMapModuleEntry {
		i32 33554840, ; type_token_id
		i32 84; java_map_index
	}, 
	; 76
	%struct.TypeMapModuleEntry {
		i32 33554844, ; type_token_id
		i32 154; java_map_index
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
], align 4; end of 'map_modules' array


; Java to managed map

; map_java
@map_java = local_unnamed_addr constant [165 x %struct.TypeMapJava] [
	; 0
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554789, ; type_token_id
		i32 127; java_name_index
	}, 
	; 1
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 151; java_name_index
	}, 
	; 2
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554747, ; type_token_id
		i32 100; java_name_index
	}, 
	; 3
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554608, ; type_token_id
		i32 50; java_name_index
	}, 
	; 4
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554620, ; type_token_id
		i32 56; java_name_index
	}, 
	; 5
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 113; java_name_index
	}, 
	; 6
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554550, ; type_token_id
		i32 22; java_name_index
	}, 
	; 7
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 153; java_name_index
	}, 
	; 8
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554807, ; type_token_id
		i32 139; java_name_index
	}, 
	; 9
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 108; java_name_index
	}, 
	; 10
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 114; java_name_index
	}, 
	; 11
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554817, ; type_token_id
		i32 147; java_name_index
	}, 
	; 12
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 54; java_name_index
	}, 
	; 13
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 40; java_name_index
	}, 
	; 14
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554737, ; type_token_id
		i32 94; java_name_index
	}, 
	; 15
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554609, ; type_token_id
		i32 51; java_name_index
	}, 
	; 16
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554795, ; type_token_id
		i32 132; java_name_index
	}, 
	; 17
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 98; java_name_index
	}, 
	; 18
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554788, ; type_token_id
		i32 126; java_name_index
	}, 
	; 19
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 95; java_name_index
	}, 
	; 20
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554812, ; type_token_id
		i32 143; java_name_index
	}, 
	; 21
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 150; java_name_index
	}, 
	; 22
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 25; java_name_index
	}, 
	; 23
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 46; java_name_index
	}, 
	; 24
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554629, ; type_token_id
		i32 63; java_name_index
	}, 
	; 25
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554721, ; type_token_id
		i32 89; java_name_index
	}, 
	; 26
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554758, ; type_token_id
		i32 106; java_name_index
	}, 
	; 27
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554808, ; type_token_id
		i32 140; java_name_index
	}, 
	; 28
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554559, ; type_token_id
		i32 28; java_name_index
	}, 
	; 29
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554625, ; type_token_id
		i32 60; java_name_index
	}, 
	; 30
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 159; java_name_index
	}, 
	; 31
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554700, ; type_token_id
		i32 85; java_name_index
	}, 
	; 32
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 124; java_name_index
	}, 
	; 33
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554841, ; type_token_id
		i32 161; java_name_index
	}, 
	; 34
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554787, ; type_token_id
		i32 125; java_name_index
	}, 
	; 35
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554794, ; type_token_id
		i32 131; java_name_index
	}, 
	; 36
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554814, ; type_token_id
		i32 145; java_name_index
	}, 
	; 37
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 12; java_name_index
	}, 
	; 38
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554791, ; type_token_id
		i32 129; java_name_index
	}, 
	; 39
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554756, ; type_token_id
		i32 105; java_name_index
	}, 
	; 40
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 21; java_name_index
	}, 
	; 41
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 23; java_name_index
	}, 
	; 42
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 109; java_name_index
	}, 
	; 43
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554637, ; type_token_id
		i32 67; java_name_index
	}, 
	; 44
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 4; java_name_index
	}, 
	; 45
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554735, ; type_token_id
		i32 93; java_name_index
	}, 
	; 46
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 99; java_name_index
	}, 
	; 47
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 149; java_name_index
	}, 
	; 48
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 137; java_name_index
	}, 
	; 49
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554782, ; type_token_id
		i32 120; java_name_index
	}, 
	; 50
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 44; java_name_index
	}, 
	; 51
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 136; java_name_index
	}, 
	; 52
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 6; java_name_index
	}, 
	; 53
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 31; java_name_index
	}, 
	; 54
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554796, ; type_token_id
		i32 133; java_name_index
	}, 
	; 55
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554689, ; type_token_id
		i32 83; java_name_index
	}, 
	; 56
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554797, ; type_token_id
		i32 134; java_name_index
	}, 
	; 57
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554655, ; type_token_id
		i32 76; java_name_index
	}, 
	; 58
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 138; java_name_index
	}, 
	; 59
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 45; java_name_index
	}, 
	; 60
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554691, ; type_token_id
		i32 84; java_name_index
	}, 
	; 61
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554776, ; type_token_id
		i32 115; java_name_index
	}, 
	; 62
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 96; java_name_index
	}, 
	; 63
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554538, ; type_token_id
		i32 16; java_name_index
	}, 
	; 64
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554595, ; type_token_id
		i32 47; java_name_index
	}, 
	; 65
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554573, ; type_token_id
		i32 36; java_name_index
	}, 
	; 66
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554623, ; type_token_id
		i32 58; java_name_index
	}, 
	; 67
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 107; java_name_index
	}, 
	; 68
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 112; java_name_index
	}, 
	; 69
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554541, ; type_token_id
		i32 18; java_name_index
	}, 
	; 70
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554790, ; type_token_id
		i32 128; java_name_index
	}, 
	; 71
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554809, ; type_token_id
		i32 141; java_name_index
	}, 
	; 72
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554779, ; type_token_id
		i32 117; java_name_index
	}, 
	; 73
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 33554441, ; type_token_id
		i32 0; java_name_index
	}, 
	; 74
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554780, ; type_token_id
		i32 118; java_name_index
	}, 
	; 75
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554624, ; type_token_id
		i32 59; java_name_index
	}, 
	; 76
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 9; java_name_index
	}, 
	; 77
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554640, ; type_token_id
		i32 68; java_name_index
	}, 
	; 78
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 19; java_name_index
	}, 
	; 79
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554749, ; type_token_id
		i32 101; java_name_index
	}, 
	; 80
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 142; java_name_index
	}, 
	; 81
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554604, ; type_token_id
		i32 49; java_name_index
	}, 
	; 82
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554634, ; type_token_id
		i32 65; java_name_index
	}, 
	; 83
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 110; java_name_index
	}, 
	; 84
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554839, ; type_token_id
		i32 160; java_name_index
	}, 
	; 85
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554647, ; type_token_id
		i32 72; java_name_index
	}, 
	; 86
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554778, ; type_token_id
		i32 116; java_name_index
	}, 
	; 87
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554654, ; type_token_id
		i32 75; java_name_index
	}, 
	; 88
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554613, ; type_token_id
		i32 53; java_name_index
	}, 
	; 89
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554572, ; type_token_id
		i32 35; java_name_index
	}, 
	; 90
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 102; java_name_index
	}, 
	; 91
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554663, ; type_token_id
		i32 80; java_name_index
	}, 
	; 92
	%struct.TypeMapJava {
		i32 1, ; module_index
		i32 33554434, ; type_token_id
		i32 11; java_name_index
	}, 
	; 93
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 33554443, ; type_token_id
		i32 2; java_name_index
	}, 
	; 94
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554648, ; type_token_id
		i32 73; java_name_index
	}, 
	; 95
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 152; java_name_index
	}, 
	; 96
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554818, ; type_token_id
		i32 148; java_name_index
	}, 
	; 97
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 78; java_name_index
	}, 
	; 98
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 111; java_name_index
	}, 
	; 99
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 37; java_name_index
	}, 
	; 100
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554829, ; type_token_id
		i32 154; java_name_index
	}, 
	; 101
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554752, ; type_token_id
		i32 103; java_name_index
	}, 
	; 102
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554558, ; type_token_id
		i32 27; java_name_index
	}, 
	; 103
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554537, ; type_token_id
		i32 15; java_name_index
	}, 
	; 104
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554619, ; type_token_id
		i32 55; java_name_index
	}, 
	; 105
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554642, ; type_token_id
		i32 70; java_name_index
	}, 
	; 106
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554733, ; type_token_id
		i32 92; java_name_index
	}, 
	; 107
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554785, ; type_token_id
		i32 123; java_name_index
	}, 
	; 108
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554565, ; type_token_id
		i32 32; java_name_index
	}, 
	; 109
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554587, ; type_token_id
		i32 43; java_name_index
	}, 
	; 110
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 57; java_name_index
	}, 
	; 111
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554784, ; type_token_id
		i32 122; java_name_index
	}, 
	; 112
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 10; java_name_index
	}, 
	; 113
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 82; java_name_index
	}, 
	; 114
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554866, ; type_token_id
		i32 164; java_name_index
	}, 
	; 115
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 8; java_name_index
	}, 
	; 116
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554842, ; type_token_id
		i32 162; java_name_index
	}, 
	; 117
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 33; java_name_index
	}, 
	; 118
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 48; java_name_index
	}, 
	; 119
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 156; java_name_index
	}, 
	; 120
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 66; java_name_index
	}, 
	; 121
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 42; java_name_index
	}, 
	; 122
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554732, ; type_token_id
		i32 91; java_name_index
	}, 
	; 123
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554815, ; type_token_id
		i32 146; java_name_index
	}, 
	; 124
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554704, ; type_token_id
		i32 87; java_name_index
	}, 
	; 125
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 33554442, ; type_token_id
		i32 1; java_name_index
	}, 
	; 126
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554553, ; type_token_id
		i32 24; java_name_index
	}, 
	; 127
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554792, ; type_token_id
		i32 130; java_name_index
	}, 
	; 128
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554731, ; type_token_id
		i32 90; java_name_index
	}, 
	; 129
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554612, ; type_token_id
		i32 52; java_name_index
	}, 
	; 130
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554702, ; type_token_id
		i32 86; java_name_index
	}, 
	; 131
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554570, ; type_token_id
		i32 34; java_name_index
	}, 
	; 132
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554783, ; type_token_id
		i32 121; java_name_index
	}, 
	; 133
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 29; java_name_index
	}, 
	; 134
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 71; java_name_index
	}, 
	; 135
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554562, ; type_token_id
		i32 30; java_name_index
	}, 
	; 136
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 5; java_name_index
	}, 
	; 137
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 7; java_name_index
	}, 
	; 138
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 13; java_name_index
	}, 
	; 139
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554781, ; type_token_id
		i32 119; java_name_index
	}, 
	; 140
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 26; java_name_index
	}, 
	; 141
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554834, ; type_token_id
		i32 157; java_name_index
	}, 
	; 142
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554742, ; type_token_id
		i32 97; java_name_index
	}, 
	; 143
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 38; java_name_index
	}, 
	; 144
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554641, ; type_token_id
		i32 69; java_name_index
	}, 
	; 145
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554539, ; type_token_id
		i32 17; java_name_index
	}, 
	; 146
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554685, ; type_token_id
		i32 81; java_name_index
	}, 
	; 147
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554657, ; type_token_id
		i32 77; java_name_index
	}, 
	; 148
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554798, ; type_token_id
		i32 135; java_name_index
	}, 
	; 149
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 79; java_name_index
	}, 
	; 150
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554653, ; type_token_id
		i32 74; java_name_index
	}, 
	; 151
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554813, ; type_token_id
		i32 144; java_name_index
	}, 
	; 152
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554754, ; type_token_id
		i32 104; java_name_index
	}, 
	; 153
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554544, ; type_token_id
		i32 20; java_name_index
	}, 
	; 154
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554843, ; type_token_id
		i32 163; java_name_index
	}, 
	; 155
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554628, ; type_token_id
		i32 62; java_name_index
	}, 
	; 156
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554527, ; type_token_id
		i32 14; java_name_index
	}, 
	; 157
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554836, ; type_token_id
		i32 158; java_name_index
	}, 
	; 158
	%struct.TypeMapJava {
		i32 0, ; module_index
		i32 0, ; type_token_id
		i32 3; java_name_index
	}, 
	; 159
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554705, ; type_token_id
		i32 88; java_name_index
	}, 
	; 160
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 61; java_name_index
	}, 
	; 161
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 41; java_name_index
	}, 
	; 162
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 39; java_name_index
	}, 
	; 163
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 33554631, ; type_token_id
		i32 64; java_name_index
	}, 
	; 164
	%struct.TypeMapJava {
		i32 2, ; module_index
		i32 0, ; type_token_id
		i32 155; java_name_index
	}
], align 4; end of 'map_java' array

@map_java_hashes = local_unnamed_addr constant [165 x i32] [
	i32 12341354, ; 0: 0xbc506a => java/lang/Object
	i32 13389226, ; 1: 0xcc4daa => java/lang/reflect/GenericDeclaration
	i32 32078366, ; 2: 0x1e97a1e => java/security/cert/Certificate
	i32 74282880, ; 3: 0x46d7780 => android/view/ViewGroup
	i32 118977103, ; 4: 0x717724f => android/util/DisplayMetrics
	i32 133154022, ; 5: 0x7efc4e6 => java/nio/channels/SeekableByteChannel
	i32 139280357, ; 6: 0x84d3fe5 => android/view/KeyEvent
	i32 151062962, ; 7: 0x90109b2 => java/lang/reflect/TypeVariable
	i32 176697843, ; 8: 0xa8831f3 => java/lang/IllegalArgumentException
	i32 182338948, ; 9: 0xade4584 => java/nio/channels/Channel
	i32 229694295, ; 10: 0xdb0db57 => java/nio/channels/WritableByteChannel
	i32 257094054, ; 11: 0xf52f1a6 => java/lang/ReflectiveOperationException
	i32 268673672, ; 12: 0x1003a288 => android/view/accessibility/AccessibilityEventSource
	i32 307048059, ; 13: 0x124d2e7b => android/view/MenuItem$OnActionExpandListener
	i32 362231028, ; 14: 0x159734f4 => java/net/URI
	i32 366534601, ; 15: 0x15d8dfc9 => android/view/ViewGroup$LayoutParams
	i32 393371378, ; 16: 0x17725ef2 => mono/java/lang/RunnableImplementor
	i32 412395228, ; 17: 0x1894a6dc => java/security/KeyStore$LoadStoreParameter
	i32 412771173, ; 18: 0x189a6365 => java/lang/Long
	i32 419359493, ; 19: 0x18feeb05 => java/util/Iterator
	i32 443233435, ; 20: 0x1a6b349b => java/lang/LinkageError
	i32 466997013, ; 21: 0x1bd5cf15 => java/lang/reflect/AnnotatedElement
	i32 509491678, ; 22: 0x1e5e39de => android/view/LayoutInflater$Factory
	i32 517025718, ; 23: 0x1ed12fb6 => android/view/ViewParent
	i32 517668398, ; 24: 0x1edafe2e => android/os/Parcel
	i32 531198748, ; 25: 0x1fa9731c => mono/android/runtime/OutputStreamAdapter
	i32 581097368, ; 26: 0x22a2d798 => java/nio/channels/FileChannel
	i32 584231583, ; 27: 0x22d2aa9f => java/lang/IllegalStateException
	i32 590702782, ; 28: 0x233568be => android/view/ViewTreeObserver
	i32 591810476, ; 29: 0x23464fac => android/os/Bundle
	i32 606085292, ; 30: 0x242020ac => java/io/Serializable
	i32 692920175, ; 31: 0x294d1f6f => java/util/ArrayList
	i32 780408360, ; 32: 0x2e841628 => java/lang/CharSequence
	i32 780987551, ; 33: 0x2e8cec9f => java/io/PrintWriter
	i32 793918146, ; 34: 0x2f523ac2 => java/lang/Integer
	i32 806800039, ; 35: 0x3016caa7 => java/lang/Thread
	i32 838682992, ; 36: 0x31fd4970 => java/lang/NullPointerException
	i32 876646173, ; 37: 0x34408f1d => javax/net/ssl/TrustManager
	i32 893363610, ; 38: 0x353fa59a => java/lang/Short
	i32 925357775, ; 39: 0x3727d6cf => java/nio/ByteBuffer
	i32 968142514, ; 40: 0x39b4aeb2 => android/view/View$OnCreateContextMenuListener
	i32 988336100, ; 41: 0x3ae8cfe4 => android/view/KeyEvent$Callback
	i32 998009430, ; 42: 0x3b7c6a56 => java/nio/channels/GatheringByteChannel
	i32 1007618603, ; 43: 0x3c0f0a2b => android/location/Location
	i32 1022922355, ; 44: 0x3cf88e73 => com/google/android/gms/tasks/OnCanceledListener
	i32 1026507328, ; 45: 0x3d2f4240 => java/net/SocketAddress
	i32 1062235695, ; 46: 0x3f506e2f => java/security/KeyStore$ProtectionParameter
	i32 1073016658, ; 47: 0x3ff4ef52 => java/lang/annotation/Annotation
	i32 1149267780, ; 48: 0x44806f44 => java/lang/Cloneable
	i32 1175636112, ; 49: 0x4612c890 => java/lang/ClassNotFoundException
	i32 1185273701, ; 50: 0x46a5d765 => android/view/SubMenu
	i32 1196063310, ; 51: 0x474a7a4e => java/lang/Appendable
	i32 1235275791, ; 52: 0x49a0d00f => com/google/android/gms/tasks/OnFailureListener
	i32 1270186925, ; 53: 0x4bb583ad => android/view/Window$Callback
	i32 1298454265, ; 54: 0x4d64d6f9 => java/lang/Throwable
	i32 1335098580, ; 55: 0x4f93fcd4 => java/util/Collection
	i32 1368421702, ; 56: 0x51907546 => java/lang/ClassCastException
	i32 1386757446, ; 57: 0x52a83d46 => android/content/ComponentName
	i32 1388906712, ; 58: 0x52c908d8 => java/lang/Comparable
	i32 1433059198, ; 59: 0x556abf7e => android/view/ViewManager
	i32 1475682991, ; 60: 0x57f522af => java/util/HashMap
	i32 1489594546, ; 61: 0x58c968b2 => java/nio/channels/spi/AbstractInterruptibleChannel
	i32 1492815417, ; 62: 0x58fa8e39 => java/util/concurrent/Executor
	i32 1506774891, ; 63: 0x59cf8f6b => android/widget/Button
	i32 1548306256, ; 64: 0x5c494750 => android/view/WindowManager$LayoutParams
	i32 1584672329, ; 65: 0x5e742e49 => android/view/Display
	i32 1586851388, ; 66: 0x5e956e3c => android/os/Handler
	i32 1595725058, ; 67: 0x5f1cd502 => java/nio/channels/ByteChannel
	i32 1605789814, ; 68: 0x5fb66876 => java/nio/channels/ScatteringByteChannel
	i32 1646348278, ; 69: 0x622147f6 => android/view/View
	i32 1649695927, ; 70: 0x62545cb7 => java/lang/RuntimeException
	i32 1657134862, ; 71: 0x62c5df0e => java/lang/IndexOutOfBoundsException
	i32 1680835779, ; 72: 0x642f84c3 => java/lang/Byte
	i32 1706022050, ; 73: 0x65afd4a2 => com/google/android/gms/tasks/Task
	i32 1718265030, ; 74: 0x666aa4c6 => java/lang/Character
	i32 1758490869, ; 75: 0x68d070f5 => android/os/BaseBundle
	i32 1763217261, ; 76: 0x69188f6d => com/google/android/gms/tasks/SuccessContinuation
	i32 1772705556, ; 77: 0x69a95714 => android/graphics/Point
	i32 1807220671, ; 78: 0x6bb7ffbf => android/view/View$OnClickListener
	i32 1828773851, ; 79: 0x6d00dfdb => java/security/cert/CertificateFactory
	i32 1851730788, ; 80: 0x6e5f2b64 => java/lang/Runnable
	i32 1866304377, ; 81: 0x6f3d8b79 => android/view/SearchEvent
	i32 1869790738, ; 82: 0x6f72be12 => android/location/Criteria
	i32 1889248750, ; 83: 0x709ba5ee => java/nio/channels/InterruptibleChannel
	i32 1944129628, ; 84: 0x73e1105c => java/io/OutputStream
	i32 1985929388, ; 85: 0x765ee0ac => android/app/Activity
	i32 1987841337, ; 86: 0x767c0d39 => java/lang/Boolean
	i32 2008064836, ; 87: 0x77b0a344 => android/content/Intent
	i32 2014726135, ; 88: 0x781647f7 => android/view/accessibility/AccessibilityRecord
	i32 2027782872, ; 89: 0x78dd82d8 => android/view/ContextThemeWrapper
	i32 2057114326, ; 90: 0x7a9d12d6 => java/security/cert/X509Extension
	i32 2114237978, ; 91: 0x7e04b61a => android/content/res/Configuration
	i32 2178351224, ; 92: 0x81d70078 => crc64e2e365b8ee8526bd/MainActivity
	i32 2226859649, ; 93: 0x84bb2e81 => com/google/android/gms/tasks/CancellationToken
	i32 2284656609, ; 94: 0x882d17e1 => android/app/Application
	i32 2316381801, ; 95: 0x8a112e69 => java/lang/reflect/Type
	i32 2411404453, ; 96: 0x8fbb1ca5 => java/lang/UnsupportedOperationException
	i32 2462006028, ; 97: 0x92bf3b0c => android/content/ComponentCallbacks
	i32 2520212266, ; 98: 0x9637632a => java/nio/channels/ReadableByteChannel
	i32 2541780716, ; 99: 0x97807eec => android/view/ContextMenu$ContextMenuInfo
	i32 2558143838, ; 100: 0x987a2d5e => java/io/FileInputStream
	i32 2561967928, ; 101: 0x98b48738 => java/security/cert/X509Certificate
	i32 2681988174, ; 102: 0x9fdbe44e => android/view/MotionEvent
	i32 2687778660, ; 103: 0xa0343f64 => android/widget/TextView
	i32 2692535938, ; 104: 0xa07cd682 => android/util/Log
	i32 2721599187, ; 105: 0xa2384ed3 => android/graphics/drawable/Drawable
	i32 2741050037, ; 106: 0xa3611ab5 => java/net/ProxySelector
	i32 2762684487, ; 107: 0xa4ab3847 => java/lang/Float
	i32 2866910344, ; 108: 0xaae19488 => android/view/ActionMode
	i32 2932874700, ; 109: 0xaed01dcc => android/view/InputEvent
	i32 2933762856, ; 110: 0xaeddab28 => android/util/AttributeSet
	i32 2942792700, ; 111: 0xaf6773fc => java/lang/Exception
	i32 2959362348, ; 112: 0xb064492c => crc6495d4f5d63cc5c882/AwaitableTaskCompleteListener_1
	i32 2980510762, ; 113: 0xb1a6fc2a => mono/android/runtime/JavaArray
	i32 2983720033, ; 114: 0xb1d7f461 => mono/android/TypeManager
	i32 2994062909, ; 115: 0xb275c63d => com/google/android/gms/tasks/OnTokenCanceledListener
	i32 3032808825, ; 116: 0xb4c4fd79 => java/io/StringWriter
	i32 3183271055, ; 117: 0xbdbcdc8f => android/view/ActionMode$Callback
	i32 3214744068, ; 118: 0xbf9d1a04 => android/view/WindowManager
	i32 3264154243, ; 119: 0xc28f0a83 => java/io/Flushable
	i32 3268587150, ; 120: 0xc2d2ae8e => android/location/LocationListener
	i32 3281925794, ; 121: 0xc39e36a2 => android/view/MenuItem
	i32 3319735188, ; 122: 0xc5df2394 => java/net/Proxy
	i32 3423467887, ; 123: 0xcc0df96f => java/lang/Number
	i32 3576242387, ; 124: 0xd52920d3 => android/runtime/JavaProxyThrowable
	i32 3646270682, ; 125: 0xd955acda => com/google/android/gms/tasks/TaskCompletionSource
	i32 3665774669, ; 126: 0xda7f484d => android/view/LayoutInflater
	i32 3666243682, ; 127: 0xda867062 => java/lang/String
	i32 3669061717, ; 128: 0xdab17055 => java/net/InetSocketAddress
	i32 3673444347, ; 129: 0xdaf44ffb => android/view/accessibility/AccessibilityEvent
	i32 3683323802, ; 130: 0xdb8b0f9a => mono/android/runtime/JavaObject
	i32 3684070586, ; 131: 0xdb9674ba => android/view/ActionProvider
	i32 3702230909, ; 132: 0xdcab8f7d => java/lang/Double
	i32 3702422870, ; 133: 0xdcae7d56 => android/view/ViewTreeObserver$OnPreDrawListener
	i32 3746020715, ; 134: 0xdf47bd6b => android/graphics/drawable/Drawable$Callback
	i32 3763853270, ; 135: 0xe057d7d6 => android/view/Window
	i32 3833943420, ; 136: 0xe485557c => com/google/android/gms/tasks/OnCompleteListener
	i32 3844928680, ; 137: 0xe52cf4a8 => com/google/android/gms/tasks/OnSuccessListener
	i32 3846932217, ; 138: 0xe54b86f9 => javax/net/ssl/X509TrustManager
	i32 3882570516, ; 139: 0xe76b5314 => java/lang/Class
	i32 3893629743, ; 140: 0xe814132f => android/view/LayoutInflater$Factory2
	i32 3900581163, ; 141: 0xe87e252b => java/io/InputStream
	i32 3912451735, ; 142: 0xe9334697 => java/security/KeyStore
	i32 3919758710, ; 143: 0xe9a2c576 => android/view/ContextMenu
	i32 3933245259, ; 144: 0xea708f4b => android/graphics/Rect
	i32 3960999444, ; 145: 0xec180e14 => android/widget/Toast
	i32 3969984744, ; 146: 0xeca128e8 => mono/android/runtime/InputStreamAdapter
	i32 3993327007, ; 147: 0xee05559f => android/content/ContextWrapper
	i32 4020308495, ; 148: 0xefa10a0f => java/lang/Error
	i32 4044525863, ; 149: 0xf1129127 => android/content/ComponentCallbacks2
	i32 4051772911, ; 150: 0xf18125ef => android/content/Context
	i32 4056674536, ; 151: 0xf1cbf0e8 => java/lang/NoClassDefFoundError
	i32 4089459037, ; 152: 0xf3c0315d => java/nio/Buffer
	i32 4098107575, ; 153: 0xf44428b7 => mono/android/view/View_OnClickListenerImplementor
	i32 4101363546, ; 154: 0xf475d75a => java/io/Writer
	i32 4118878202, ; 155: 0xf58117fa => android/os/Looper
	i32 4148593869, ; 156: 0xf74684cd => javax/net/ssl/TrustManagerFactory
	i32 4157808693, ; 157: 0xf7d32035 => java/io/IOException
	i32 4179857161, ; 158: 0xf9238f09 => com/google/android/gms/tasks/Continuation
	i32 4232707919, ; 159: 0xfc49ff4f => java/util/HashSet
	i32 4236724582, ; 160: 0xfc874966 => android/os/Parcelable
	i32 4237386260, ; 161: 0xfc916214 => android/view/MenuItem$OnMenuItemClickListener
	i32 4248811056, ; 162: 0xfd3fb630 => android/view/Menu
	i32 4268805981, ; 163: 0xfe70cf5d => android/location/LocationManager
	i32 4277523103 ; 164: 0xfef5d29f => java/io/Closeable
], align 4

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
], align 4

!llvm.module.flags = !{!0, !1, !2}
!llvm.ident = !{!3}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!2 = !{i32 1, !"min_enum_size", i32 4}
!3 = !{!"Xamarin.Android remotes/origin/d17-5 @ 45b0e144f73b2c8747d8b5ec8cbd3b55beca67f0"}
