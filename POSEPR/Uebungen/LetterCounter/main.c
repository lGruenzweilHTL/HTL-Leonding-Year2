#include <stdio.h>
#include <ctype.h>

void printResult(int letters[26]) {
    printf("\n");
    for (size_t i = 0; i < 26; i++) {
        if (letters[i] > 0) {
            printf("%c: %d\n", (char)(65 + i), letters[i]);
        }
    }
}


int main(void) {
    char buf[80];

    printf("Enter your text: ");
    scanf("%s", &buf);

    int letters[26];

    // Initialize array
    for (size_t i = 0; i < 26; i++) letters[i] = 0;

    // Count letters
    for (size_t i = 0; i < 80; i++) {
        if (buf[i] == '\0') break;

        int idx = tolower(buf[i]) - 97;

        if (idx >= 0 && idx < 26) letters[idx]++;
    }

    printResult(letters);
}