# FileGen
Lightweight file/folder generator for windows.

## Command Line Arguments
To use FileGen via the command line, you must run it using the following format:
```
filegen path -action count
```
- In place of "path" enter the absolute path of the directory you want to create files/folders in.
- In place of "action" enter:
 - "f" to just generate files
 - "d" to just generate folders
 - "fd" or "df" to generate files and folders
- In place of "count" enter the number of files/folders to be created.

### Example
```
filegen "D:\Henta-I mean work stuff\" -fd 420
```

### Notes
- The name of the .exe (FileGen) can be entered in any case (FileGen, filegen, fiLeGen, etc.), with or without the .exe extension.
- The path will require surrounding quotes if it includes spaces (otherwise they're optional).
- To create files and folders, the order of "f" and "d" does not matter ("-fd" or "-df").

## The GUI
Right now, there is only one thing to say here: **DO NOT DELETE FileGen.exe!** or else the GUI version will not work.

## To Do
- GUI (via WPF)
- Better error handling
- Allow custom names/extensions
- Allow random generation count
- Allow random naming/extensions
- Allow nested generation
- Allow random text in files
- Help command
- Cross-platform (Unix/Linux)
