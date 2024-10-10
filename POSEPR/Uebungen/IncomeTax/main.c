#include <stdio.h>

double calculateIncomeTax(double income) {
    double tax = 0.0;
    double taxBrackets[] = {11000, 18000, 31000, 60000, 90000, 1000000};
    double taxAmounts[] = {0.0, 0.20, 0.35, 0.42, 0.48, 0.50, 0.55};
    int i;

    for (i = 5; i >= 0; i--) {
        if (income > taxBrackets[i]) {
            tax += (income - taxBrackets[i]) * taxAmounts[i + 1];
            income = taxBrackets[i];
        }
    }

    return tax;
}

int main() {
    double income;
    printf("Geben Sie Ihr Einkommen ein: ");
    scanf("%lf", &income);

    double steuer = calculateIncomeTax(income);

    printf("Income Tax: %.2f\n", steuer);

    return 0;
}
