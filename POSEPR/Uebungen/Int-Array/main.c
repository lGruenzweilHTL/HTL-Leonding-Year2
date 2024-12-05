#include <stdio.h>
#include <stdbool.h>
#include <stdlib.h>

// Function to check if a value exists in the array
bool contains(int *arr, int arrLen, int value) {
    for (size_t i = 0; i < arrLen; i++) {
        if (arr[i] == value) {
            return true;
        }
    }
    return false;
}

// Function to remove duplicates by setting duplicate values to -1
void removeDuplicates(int *arr, int arrLen) {
    int duplicates[arrLen];
    for (size_t i = 0; i < arrLen; i++) {
        int value = arr[i];

        if (contains(duplicates, arrLen, value)) {
            arr[i] = -1;
        }
        else {
            duplicates[i] = value;
        }
    }
}

// Function to calculate the sum of array elements
int sum(int *arr, int arrLen) {
    int total = 0;
    for (size_t i = 0; i < arrLen; i++) {
        total += arr[i];
    }
    return total;
}

// Function to calculate the average of the array elements
float calcAvg(int *arr, int arrLen) {
    int total = sum(arr, arrLen);
    return (float)total / arrLen;
}

// Function to sort the array
void sort(int *arr, int arrLen) {
    for (size_t i = 0; i < arrLen; i++) {
        int min = i;
        for (size_t j = i + 1; j < arrLen; j++) {
            if (arr[j] < arr[min]) {
                min = j;
            }
        }
        int temp = arr[min];
        arr[min] = arr[i];
        arr[i] = temp;
    }
}

// Function to print the array
void printArr(int *arr, int arrLen) {
    for (size_t i = 0; i < arrLen; i++) {
        printf("%d ", arr[i]);
    }
    printf("\n");
}

int main(int argc, char **argv) {
    int amt = 5;

    int arr[amt];

    for (size_t i = 0; i < amt; i++) {
        printf("Enter a number: ");
        scanf("%d", &arr[i]);
    }

    printf("This is your array:\n");
    printArr(arr, amt);

    printf("The average of your array is %.2f\n", calcAvg(arr, amt));

    printf("Sorting your array...\n");
    sort(arr, amt);
    printf("This is the sorted array:\n");
    printArr(arr, amt);

    printf("Removing duplicates...\n");
    removeDuplicates(arr, amt);
    printf("Sorted array after removing duplicates:\n");
    printArr(arr, amt);

    return 0;
}
