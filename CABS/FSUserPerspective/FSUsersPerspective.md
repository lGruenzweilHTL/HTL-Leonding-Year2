# Unit 08 - File Systems - Users Perspective Exercises 01

## 1. File Naming

a) **Unix**

| **Aspect**              | **Details**                                                                 |
|-------------------------|------------------------------------------------------------------------------|
| **Max file name length**| Typically 255 bytes.                                                        |
| **Invalid characters**  | Only the **forward slash (`/`)** and **null byte (`\0`)** are disallowed.  |
| **Case sensitivity**    | **Case-sensitive**: `file.txt` != `File.txt`.                                |
| **File extension rules**| Extensions are **optional** and not enforced by the OS.                |

**Valid files names**:
- my_document.txt
- hello world! @#$.md
- résumé.pdf
- .hiddenfile

**Invalid file names**:
- my/file.txt ==> contains /, which is a directory separator.
- null\0byte ==> null character not allowed.

b) **NTFS**

| **Aspect**              | **Details**                                                                 |
|-------------------------|------------------------------------------------------------------------------|
| **Max file name length**| 255 characters (each part); 32,767 total path length.                      |
| **Invalid characters**  | `\ / : * ? " < > |` are **not allowed**.                                   |
| **Case sensitivity**    | **not case-sensitive** (by default): `File.txt` = `file.txt`.                |
| **File extension rules**| Extensions are used (e.g., `.exe`, `.txt`), but not technically required.  |

**Valid file names**:
- Report2025.docx
- MyFile123.TXT
- Data_Set.csv

**Invalid file names**:
- my&lt;file&gt;.txt ==> contains < and >.
- sales|data.xlsx ==> contains |.
- con.txt ==> con, prn, aux, nul, com1... are reserved names in Windows.

c) FAT-16

| **Aspect**              | **Details**                                                                |
|-------------------------|-----------------------------------------------------------------------------|
| **Max file name length**| **8.3 format**: 8 characters name + 3 character extension (e.g., `FILE.TXT`). |
| **Valid characters**    | Alphanumeric + limited symbols (`$`, `%`, `-`, `_`, `@`, `~`, etc.).       |
| **Invalid characters**  | `\ / : * ? " < > | + = , ; [ ]` and lowercase letters (originally).         |
| **Case sensitivity**    | **Case-insensitive**.                                                      |
| **File extension rules**| Strict: Must follow `name.ext` format (extension optional but 3-char max). |

**Valid file names**:
- README.TXT
- DATA1.DAT
- FILE1234.EXE

**Invalid file names**:
- mydocument.txt ==> exceeds 8.3 format.
- sales-report.xlsx ==> hyphen (-) is valid, but name too long.
- data@2025.xls ==> @ allowed, but name must still respect 8.3.
- hello world.txt ==> space is invalid.


## 2. ASCII and Binary Files

a) Assembler file

Assembler files are **ASCII**, because they are written in plain text.

b) Object file (result of a compilation)

Object files are **binary**, because they contain machine instructions, not any text.

c) XML file

XML files are **ASCII**, because they are written in plain text and are human-readable.

d) xlsx file

xlsx files are essentially zip-files, meaning they are **binary**.

## 3. Change Directory

We are in the directory `/Users/admin/scripts/web/servers`

1) cd /
    We are now in the **root-directory**, denoted by `/`

2) cd ..
    We are in the **previous directory**, which is `/Users/admin/scripts/web`

3) cd ../../web/./../..
    We are in `/Users/admin`

## 4. What is the difference between File Attributes and Meta Data?

Metadata is any data **except the actual content**.
**File Attributes** are a subset of metadata, used by the system to manage and access files.

## 5. Directories: What is a directory used for? What do you know about the implementation of Directories? Which information is held by a directory?

### Usage
Directories are used to order files. They are purely a user-comfort feature.

### Implementation and Information
Directories are implemented as a **file**. The only information they hold are the files inside of them.
