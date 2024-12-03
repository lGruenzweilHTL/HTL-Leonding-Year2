#include <stdio.h>

int readValues(float *num1, float *num2) {
    printf("Enter value 1: ");
    scanf("%f", num1);

    printf("Enter value 2: ");
    scanf("%f", num2);

    return 0;
}

int print(float value) {
    printf("%f\n", value);
    return 0;
}

int add(float num1, float num2, float *result) {
    *result = num1 + num2;
}
int sub(float num1, float num2, float *result) {
    *result = num1 - num2;
}
float mul(float num1, float num2) {
    return num1 * num2;
}
float div(float num1, float num2) {
    return num1 / num2;
}

int main(int argc, char **argv) {
    float num1;
    float num2;
    readValues(&num1, &num2);

    char op;
    printf("Enter operation [+, -, *, /]: ");
    getchar();
    scanf("%c", &op);

    float result = 0;
    int exitCode = 0; // ok = 0, wrong = 1
    if (op == '+') {
        exitCode |= add(num1, num2, &result);
    }
    else if (op == '-') {
        exitCode |= sub(num1, num2, &result);
    }
    else if (op == '*') {
        result = mul(num1, num2);
    }
    else if (op == '/') {
        result = div(num1, num2);
    }

    if (exitCode != 0) {
        printf("Something went wrong!");
    }

    print(result);
    return exitCode;
}