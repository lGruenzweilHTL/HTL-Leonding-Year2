#include <stdio.h>
#include <stdlib.h>
#include <sys/stat.h>
#include <fcntl.h>

int main(int argc, char **argv) {
    if (argc != 2) {
        printf("usage: ./isdir <filename>\n");
        exit(1);
    }

    char *filename = argv[1];

    // Check if the file exists
    int fd = open(filename, O_RDONLY);
    if (fd < 0) {
        perror("Error opening file");
        printf("%s does not exist\n", filename);
        exit(1);
    }

    // Check if the file is a directory (and if it is empty)
    struct stat fileStat;
    if (fstat(fd, &fileStat) < 0) {
        perror("Error getting file status");
        close(fd);
        exit(1);
    }

    if (S_ISDIR(fileStat.st_mode)) {
        if (fileStat.st_size == 0) {
            printf("%s is a directory and is empty\n", filename);
        } else {
            printf("%s is a directory and contains files\n", filename);
        }
    } else {
        printf("%s is no directory\n", filename);
    }

    close(fd);
    return 0;
}