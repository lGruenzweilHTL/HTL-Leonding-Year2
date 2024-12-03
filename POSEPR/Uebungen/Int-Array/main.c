#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

// Function to calculate the length of the array
int len(int **arr) {
    int count = 0;
    while (arr[count] != NULL) {  // Count elements until a NULL pointer is encountered
        count++;
    }
    return count;
}

// Function to check if a value exists in the array
bool contains(int **arr, int value) {
    for (size_t i = 0; i < len(arr); i++) {
        if (*arr[i] == value) {
            return true;
        }
    }
    return false;
}

// Function to dynamically allocate memory for the array and read values
int **readValues(int amt) {
    int **buf = malloc((amt + 1) * sizeof(int *));  // Allocate memory for 'amt' pointers + 1 for the NULL terminator
    if (buf == NULL) {
        printf("Memory allocation failed!\n");
        return NULL;
    }

    for (size_t i = 0; i < amt; i++) {
        buf[i] = malloc(sizeof(int));  // Allocate memory for each integer
        if (buf[i] == NULL) {
            printf("Memory allocation failed for element %zu!\n", i);
            return NULL;
        }

        printf("Enter a number: ");
        scanf("%d", buf[i]);
    }

    buf[amt] = NULL;  // Add a NULL pointer at the end to mark the end of the array

    return buf;
}

// Function to remove duplicates by setting duplicate values to 0
void removeDuplicates(int **arr) {
    int duplicates[len(arr)];
    for (size_t i = 0; i < len(arr); i++) {
        int value = *arr[i];

        if (contains(&duplicates, value)) {
            *arr[i] = -1;
        }
        else {
            duplicates[i] = value;
        }
    }
}

// Function to calculate the sum of array elements
int sum(int **arr) {
    int total = 0;
    for (size_t i = 0; i < len(arr); i++) {
        total += *arr[i];
    }
    return total;
}

// Function to calculate the average of the array elements
float calcAvg(int **arr) {
    int total = sum(arr);
    int count = len(arr);
    return (float)total / count;
}

// Function to swap the values of two elements
void swap(int *a, int *b) {
    int temp = *a;
    *a = *b;
    *b = temp;
}

// Function to sort the array
void sort(int **arr) {
    int arrLen = len(arr);
    for (size_t i = 0; i < arrLen; i++) {
        int min = i;
        for (size_t j = i + 1; j < arrLen; j++) {
            if (*arr[j] < *arr[min]) {
                min = j;
            }
        }
        swap(arr[min], arr[i]);
    }
}

// Function to print the array
void printArr(int **arr) {
    for (size_t i = 0; i < len(arr); i++) {
        printf("%d ", *arr[i]);
    }
    printf("\n");
}

int main(int argc, char **argv) {
    int amt = 5;
    int **arr = readValues(amt);

    if (arr == NULL) {
        return -1; // If memory allocation failed, exit the program
    }

    printf("This is your array:\n");
    printArr(arr);

    printf("The average of your array is %.2f\n", calcAvg(arr));

    printf("Sorting your array...\n");
    sort(arr);
    printf("This is the sorted array:\n");
    printArr(arr);

    printf("Removing duplicates...\n");
    removeDuplicates(arr);
    printf("Array after removing duplicates:\n");
    printArr(arr);

    // Free allocated memory for each individual integer
    for (size_t i = 0; i < amt; i++) {
        free(arr[i]);  // Free each integer memory
    }

    // Free the array of pointers itself
    free(arr);

    return 0;
}
