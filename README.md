# dump-beautifier
Parses .NET application dump and generates html document to show process internals

1. Create dump using [procdump](https://docs.microsoft.com/en-us/sysinternals/downloads/procdump) tool `procdump.exe -ma process_name c:\dumps` or `procdump.exe -ma 55 c:\dumps`
2. Run `dotnet dump-b.dll dump_file.dmp`
3. Open generated `dump_file-beautified.html` in dump folder