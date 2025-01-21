#include <stdio.h>

int mystrlen(char *s) {
    int i = 0;
    while (s[i] != '\0') i++;
    return i;
}
int mystrcmp(char *s1, char *s2) {
    int l1 = mystrlen(s1);
    int diff = l1 - mystrlen(s2);
    if (diff != 0) return diff;

    for (size_t i = 0; i < l1; i++) {
        diff = s1[i] - s2[i];
        if (diff != 0) return diff;
    }

    return 0;
}

void mystrmrg(char *s1, char *s2, char *result) {
    int i = 0,
    j = 0,
    k = 0;

    while (s1[i] != '\0' && s2[j] != '\0') {
        if (s1[i] < s2[j]) result[k++] = s1[i++];
        else result[k++] = s2[j++];
    }

    while (s1[i] != '\0') result[k++] = s1[i++];
    while (s2[j] != '\0') result[k++] = s2[j++];
}

int mystrsearch(char *str, char *sub, int start) {
    int sublen = mystrlen(sub);
    int subIdx = 0;
    int subStart = -1;
    for (size_t i = start; i < mystrlen(str); i++) {
        if (str[i] == sub[subIdx]) {
            if (++subIdx == sublen) {
                return subStart;
            }
        }
        else {
            subIdx = 0;
            subStart = i + 1;
        }
    }

    return -1;
}

int main(int argc, char **argv) {
    int len = mystrlen("long");
    printf("%d\n", len);

    int cmp = mystrcmp("test", "tests");
    printf("%d\n", cmp);

    char mrg[100];
    mystrmrg("aab", "abc", mrg);
    printf("%s\n", mrg);

    int search = mystrsearch("Pretty long string", " l", 0);
    printf("%d", search);
}