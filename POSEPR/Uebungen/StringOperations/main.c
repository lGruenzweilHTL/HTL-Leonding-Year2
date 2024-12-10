#include <stdio.h>
#include <string.h>

int sgn(int a) {
    if (a < 0) return -1;
    if (a == 0) return 0;
    return 1;
}

int mystrlen(char *str) {
    int i = 0;
    while (str[i] != '\0') {
        i++;
    }

    return i;
}
void mystrcpy(char *s1, char *s2) {
    for (size_t i = 0; i <= mystrlen(s1); i++) {
        s1[i] = s2[i];
    }
}
int mystrcmp(char *s1, char *s2) {
    int cmp = 0;

    int l1 = mystrlen(s1);
    int diff = l1 - mystrlen(s2);
    if (diff != 0) {
        return diff;
    }

    for (size_t i = 0; i < l1; i++) {
        int diff = s1[i] - s2[i];

        if (diff != 0) {
            return diff;
        }
    }

    return 0;
}

int main(int argc, char **argv) {
    char str1[] = "Testing";
    char str2[] = "Test";

    // Test mystrlen
    if (mystrlen(str1) == strlen(str1) && mystrlen(str2) == strlen(str2)) {
        printf("\"mystrlen\" completed successfully.\n");
    }
    else {
        printf("\"mystrlen\" failed.\n");
    }

    // Test mystrcmp
    if (sgn(mystrcmp(str1, str2)) == sgn(strcmp(str1, str2))) {
        printf("\"mystrcmp\" completed successfully.\n");
    }
    else {
        printf("\"mystrcmp\" failed.\n");
    }

    // Test mystrcpy
    char mysrc[] = "src";
    char cpsrc[] = "src";
    char mydest[] = "dest";
    char cpdest[] = "dest";

    mystrcpy(mydest, mysrc);
    strcpy(cpdest, cpsrc);

    if (mystrcmp(mydest, cpdest) == 0 && mystrcmp(mysrc, cpsrc) == 0) {
        printf("\"mystrcpy\" completed successfully.\n");
    }
    else {
        printf("\"mystrcpy\" failed.\n");
    }
}
