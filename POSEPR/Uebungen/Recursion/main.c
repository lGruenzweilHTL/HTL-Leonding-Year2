#include <stdio.h>

int factorial(int n) {
    return n <= 1 ? 1 : factorial(n - 1) * n;
}

int multiply(int a, int b) {
    if (b == 0) {
        return 0;
    }

    return a + multiply(a, b - 1);
}

int divide(int a, int b) {
    if (a < b) {
        return 0;
    }

    return 1 + divide(a - b, b);
}

int fib(int n) {
    return n <= 2 ? n : fib(n - 1) + fib(n - 2);
}

int main(void) {
    int num1, num2;
    printf("Input a number for factorial: ");
    scanf("%d", &num1);

    int fac = factorial(num1);
    printf("Factorial of %d is: %d\n", num1, fac);

    printf("Input first number for multiplication: ");
    scanf("%d", &num1);
    printf("Input second number: ");
    scanf("%d", &num2);

    int mul = multiply(num1, num2);
    printf("%dx%d = %d\n", num1, num2, mul);

    printf("Input first number for division: ");
    scanf("%d", &num1);
    printf("Input second number: ");
    scanf("%d", &num2);
    int div = divide(num1, num2);
    printf("%d/%d = %d\n", num1, num2, div);

    printf("Input fibonacci number: ");
    scanf("%d", &num1);
    int fibo = fib(num1);
    printf("The %d. fibonacci number is: %d\n", num1, fibo);
}