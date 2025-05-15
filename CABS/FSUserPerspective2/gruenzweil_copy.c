#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <unistd.h>

int main(int argc, char **argv) {
    if (argc != 3) {
        printf("Invalid arg count. Must be source file and target file\n");
        exit(1);
    }

    char *src = argv[1];
    char *target = argv[2];

    int srcFile = open(src, O_RDONLY);
    if (srcFile < 0) {
        perror("Error opening source file");
        exit(1);
    }

    int targetFile = open(target, O_WRONLY | O_CREAT | O_TRUNC, 0644);
    if (targetFile < 0) {
        perror("Error opening target file");
        close(srcFile);
        exit(1);
    }

    char buffer[4096];
    size_t bytesRead;
    while ((bytesRead = read(srcFile, buffer, sizeof(buffer))) > 0) {
        if (write(targetFile, buffer, bytesRead) != bytesRead) {
            perror("Error writing to target file");
            close(srcFile);
            close(targetFile);
            exit(1);
        }
    }

    if (bytesRead < 0) {
        perror("Error reading source file");
    }

    close(srcFile);
    close(targetFile);

    if (bytesRead >= 0) {
        printf("File copied successfully\n");
    }

    return 0;
}