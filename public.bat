RMDIR /S /Q bint
RMDIR /S /Q bin
MKDIR bin
SET NAME=NyarukoWindowsUserManager_v1.0.0
SET CONFIGURATION=Release

SET PLATFORM=x64
MKDIR bint
COPY SystemRes\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY UserInfo\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY GroupMgr\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY UserMgr\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY LICENSE bint\
COPY README.md bint\
CD bint
DIR /B > filelist.txt
MAKECAB /F filelist.txt /D CompressionType=LZX /D CompressionMemory=21 /D maxdisksize=1024000000
CD ..
MOVE bint\disk1\1.cab bin\%NAME%_%PLATFORM%.cab
RMDIR /S /Q bint

SET PLATFORM=x86
MKDIR bint
COPY SystemRes\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY UserInfo\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY GroupMgr\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY UserMgr\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY LICENSE bint\
COPY README.md bint\
CD bint
DIR /B > filelist.txt
MAKECAB /F filelist.txt /D CompressionType=LZX /D CompressionMemory=21 /D maxdisksize=1024000000
CD ..
MOVE bint\disk1\1.cab bin\%NAME%_%PLATFORM%.cab
RMDIR /S /Q bint

SET PLATFORM=ARM64
MKDIR bint
COPY SystemRes\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY UserInfo\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY GroupMgr\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY UserMgr\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY LICENSE bint\
COPY README.md bint\
CD bint
DIR /B > filelist.txt
MAKECAB /F filelist.txt /D CompressionType=LZX /D CompressionMemory=21 /D maxdisksize=1024000000
CD ..
MOVE bint\disk1\1.cab bin\%NAME%_%PLATFORM%.cab
RMDIR /S /Q bint

SET PLATFORM=Itanium
MKDIR bint
COPY SystemRes\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY UserInfo\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY GroupMgr\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY UserMgr\bin\%PLATFORM%\%CONFIGURATION%\* bint\
COPY LICENSE bint\
COPY README.md bint\
CD bint
DIR /B > filelist.txt
MAKECAB /F filelist.txt /D CompressionType=LZX /D CompressionMemory=21 /D maxdisksize=1024000000
CD ..
MOVE bint\disk1\1.cab bin\%NAME%_%PLATFORM%.cab
RMDIR /S /Q bint
DIR bin
openssl sha256 bin/*.cab
