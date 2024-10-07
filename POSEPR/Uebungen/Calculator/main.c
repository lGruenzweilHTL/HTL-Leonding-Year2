#include <stdio.h>
#include <stdlib.h>

int main(int argc, char *argv[]){
    // Declare
    char op;
    double num1, num2;
    
    // Read
  
    if (argc == 4){ // Program name + 3 args
      op = *argv[2];
      num1 = atoi(argv[1]);
      num2 = atoi(argv[3]);
    } else {
      printf("Enter operation [+, -, *, /]: ");
      scanf("%c", &op);

      printf("Enter first number: ");
      scanf("%lf", &num1);

      printf("Enter second number: ");
      scanf("%lf", &num2);
    }
    // Calculate
    double result;
    if (op == '+'){
        result = num1 + num2;
    }
    else if (op == '-'){
        result = num1 - num2;
    }
    else if (op == '*'){
        result = num1 * num2;
    }
    else if (op == '/'){
        if (num2 == 0){
            printf("Can't divide by zero\n");
            return -1;
        }
        result = num1 / num2;
    }
    else{
        printf("Invalid operator\n");
        return -1;
    }

    printf("Result: %lf\n", result);
    return 0;
}
