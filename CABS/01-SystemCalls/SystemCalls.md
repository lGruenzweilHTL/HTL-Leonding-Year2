# Understand System Calls
## fork
The command 'fork' creates a new process by duplicating the calling process.
The calling process is called the parent and the new process is called the child process.

## stat
The command 'stat' is used to display a file status. Use the -f attribute to display the file system status

## kill
'kill' is used to send a signal to a process. The signals you can send are:
![Kill signals](killSignals.png)

## mmap
The command 'mmap' is used to map or unmap files or devices into memory.

## chmod
'chmod' is used to change file mode bits on files. 
You can change who has access to the file and the rwx (read-write-execute) permisions

## waitpid
'waitpid' waits for a process to change state.

# System Call Fails
## fork
#### ENOMEM
fork() failed to allocate the necessary kernel structures because memory is tight.

#### ENOSYS
fork() is not supported on this plattform (for example, hardware without a Memory-Management unit).

## exec


## unlink


## read


## mount


## chmod


## kill


# Assembler
## Exercise
Rewrite the following C-Code into a Stack-based assembler program
```c
int x = 3;
int y = 4;
int z = 12;
int k = z * (x + y);
```

## Additional Info
- local variables start at address 32 of the local stack frame
- 32-bit architecture, i.e., an integer takes 4 bytes

## Solution
```asm
LA 0,32
LIT 3
STO

LA 0,36
LIT 4
STO

LA 0,40
LIT 12
STO

LA 0,44
LV 0,40
LV 0,32
LV 0,36
ADD
MUL
STO
```

