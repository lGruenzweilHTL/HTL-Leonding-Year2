# Unit 08a: FileSystems Introduction Exercise

## NTFS vs ext3

### User's Perspective

1. **OS Compatibility**:
    NTFS works on Windows, while ext3 is used on Linux systems.

2. **File sizes**:
    NTFS supports large files up to 16 TiB. ext3 only supports files up to 2 TiB.

3. **Access Control**:
    NTFS uses detailed **Access Control Lists** (ACLs). ext3 uses basic Unix-style permission (rwx)

### Implementer's Perspective

1. **Journaling**:
    NTFS has a more advanced Journaling system than ext3.

2. **Metadata Structure**:
    NTFS uses a **Master File Table** (MTF), while ext3 uses the traditional inode structure.

3. **Compression and Encryption**:
    NTFS has **built in** file-level compression and **EFS encryption**. ext3 **doesn't have any** built in compression or encryption.

## NTFS File Attributes

1. **Timestamps**: Created, Modified, Accessed, Changed.
2. **Permissions**: ACLs (Access Control Lists) for fine-grained access.
3. **Compression Status**: Whether the file is stored compressed.
4. **Encryption Status**: Indicates if the file is encrypted with EFS.
5. **Alternate Data Streams (ADS)**: Stores additional data invisibly alongside a file.

## Smallest Data Unit

The smallest accessible unit of data on disks (HDD or SSD, also on Floppy Disk or USB Stick, etc.) are called **Blocks**.

They can be **read** and **written to**. (read/write)