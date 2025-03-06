#include <stdio.h>

long ggt(long a, long b) {
    if (a == b)  return a;
    if (a > b) return ggt(a - b, b);
    if (a < b) return ggt(a, b - a);
}

int main(void) {
    long a,b;

    printf("Input first number: ");
    scanf("%ld", &a);

    printf("Input second number: ");
    scanf("%ld", &b);

    printf("\nThe GGT of %ld and %ld is %ld\n", a, b, ggt(a, b));

    return 0;
}