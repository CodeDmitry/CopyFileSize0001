# FileSize0001

A Windows utility to get size of the file on right click or as a second argument.

**Background**:
I have created this program because I find myself constantly in need of checking the exact file size of a files.  

Unforuntately, Windows shell makes it difficult to get exact size in bytes, it gives a "MB"/"Kb" estimate which sometimes overshoots and sometimes undershoots.

Prior to making this program, I resorted to running a script each time I needed to get the size of a file, which felt a little tedious.

This utility makes it easy to get exact file size into your clipboard without resorting to scripts or other cleverness.

**How to use**:

1. Compile the given file using the file `Program.cs`(be sure to import the `PresentationCore` assembly as we require the System.Windows.Clipboard component inside a C# Console Application, with output type set to Windows Application, 
*this sounds odd, but it basically doesn't want forms, nor does it want consoles, it just wants 
to be a right click utility with no windows nor consoles*. 

2. Once built, you may move the generated executable into a more stable location such as program files.

3. Open RegEdit and go to `HKEY_CLASSES_ROOT->*->shell`, right click the text shell key(it has a folder icon next to it) and click `New->Key`
name this key "Get File Size", then right click this new subkey and click "New->Key" and name it "command". Click on the new "command" subkey,
and the right side of the screen should have a row named "(Default)", double click on this row and enter the following:

```
"C:\Users\16472\source\repos\FileSize0001\FileSize0001\bin\Release\FileSize0001.exe" "%1"
```

The above consists of two sets of quotes, `"..." "..."`, the first one should contain the path of your generated executable, while the second one should remain unchanged(keep it as "%1", as that will tell our program where the path of the file to get size of is located).

4. Verify that right clicking any file has a "Get File Size" option, and clicking on that option copies the exact size of the file into your system clipboard(like ctrl c would).

**Why the Design**

The program is supposed to be fast to write, but at the same time I needed to make sure that every step of the program could be tested(as there are multiple components it relies on that I could imagine failing, which they did, The services provided by the Clipboard component do not work unless STAThread attribute is added to main, which took a bit to figure out), this meant that
I had to resist the urge to make a one-liner. In order to maximize testability, it is good practice to make the testable parts of your program
not be inside static methods. 

I want to have it all be a single file to make it easy to add to github without being
overwhelming(I do not believe it is okay to have source code under 4000 bytes, but have over 1,000,000 bytes of visual studio project caches/boilerplate).

The program had to make sense to somebody with practically zero understanding of C#, so it has to be as dumb as possible. It fails, as STAThread attribute is a little on the advanced side, but other than that it is fairly straightforward. 

The program does NOT pass arguments to the Run method nor constructor, and opts to use Environment.GetCommandLineArgs, as I felt it saved the burden
of needing to document the argument of Run.

Initially I wanted Program to subclass something analogous to Java's Runnable to make the Run method make more sense, but I have had trouble finding 
such an analog. 

The return codes 25, 26, 27 were chosen to be distinct and deliberate, 1 is too close to 0, I want there to be as little of doubt as possible
that it was my program that set the error code, and that the error code came from these specific states(25 + state id). Also the error codes must be hardcoded to make it easy to ctrl-f for them inside the code, as 25, 26, 27 only occur once inside the file.

The name is meant to be generic enough to not collide with system or other peoples' utilities, but not too generic for people's minds to wrap their mind around, the program must not fit in like any other normal program, as nor stand out so much as to look like it is malware(because it isn't).

It is not perfect, but I felt the design decisions I have made are a reasonable compromise to get the utility published within 30 minutes(Having not written C# in a fair while).
 
