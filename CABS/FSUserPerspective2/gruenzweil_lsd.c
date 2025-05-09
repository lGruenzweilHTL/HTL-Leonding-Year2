#include <stdio.h>
#include <stdlib.h>

int main(void) {
    FILE *fp = _popen("ls -l", "r");

    if (fp == NULL) {
        perror("popen");
        return 1;
    }

    char buffer[256];
    while (fgets(buffer, sizeof(buffer), fp) != NULL) {
        printf("%s", buffer);
    }
    _pclose(fp);
    return 0;
}