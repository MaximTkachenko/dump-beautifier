# [WIP] dump-beautifier
Parses .NET application dump and generates html document to show process internals using [D3.js](https://d3js.org/)

1. Create dump using [procdump](https://docs.microsoft.com/en-us/sysinternals/downloads/procdump) tool `procdump.exe -ma process_name c:\dumps` or `procdump.exe -ma 55 c:\dumps`
2. If dump is created on a remote machine you need to copy `mscordacwks.dll` (or `mscordaccore.dll` in case of dotnet core) locally to folder with dump
2. Run `dotnet DumpBeautifier.dll %path to dump file%`
3. Open generated `%dump file name%-beautified.html` in the dump folder

As an idea:
- crossplatform web-ui for https://github.com/fremag/MemoScope.Net
